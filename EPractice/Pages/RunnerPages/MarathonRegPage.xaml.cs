using EPractice.Classes;
using EPractice.DBConnection;
using EPractice.Windows;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;
using System.Data.Entity;

namespace EPractice.Pages.RunnerPages
{
    /// <summary>
    /// Логика взаимодействия для MarathonRegPage.xaml
    /// </summary>
    public partial class MarathonRegPage : Page
    {
        public class EventTypeViewModel
        {
            public string EventTypeId { get; set; }
            public string EventTypeName { get; set; }
            public decimal Cost { get; set; }
            public bool IsSelected { get; set; }
            public string EventTypeNameWithPrice => $"{EventTypeName} (${Cost})";
        }

        public class RaceKitOptionViewModel
        {
            public string RaceKitOptionId { get; set; }
            public string RaceKitOption { get; set; }
            public decimal Cost { get; set; }
            public bool IsSelected { get; set; }
            public string RaceKitOptionWithPrice => $"{RaceKitOption} (${Cost})";
        }
        public MarathonRegPage()
        {
            InitializeComponent();
            LoadData();
            CalculateTotalCost();
        }

        private void EventCheckBox_Changed(object sender, RoutedEventArgs e)
        {
            CalculateTotalCost();
        }


        private void LoadData()
        {
            try
            {
                var eventTypes = Connection.marathonEntities.Event
                    .Join(Connection.marathonEntities.EventType,
                        e => e.EventTypeId,
                        et => et.EventTypeId,
                        (e, et) => new EventTypeViewModel
                        {
                            EventTypeId = et.EventTypeId,
                            EventTypeName = et.EventTypeName,
                            Cost = e.Cost ?? 0,
                            IsSelected = false
                        })
                    .Distinct()
                    .ToList();

                EventsList.ItemsSource = eventTypes;

                var kitOptions = Connection.marathonEntities.RaceKitOption
                    .Select(r => new RaceKitOptionViewModel
                    {
                        RaceKitOptionId = r.RaceKitOptionId,
                        RaceKitOption = r.RaceKitOption1,
                        Cost = r.Cost,
                        IsSelected = r.RaceKitOptionId == "A" // Выбран по умолчанию вариант A
                    })
                    .ToList();

                KitOptionsList.ItemsSource = kitOptions;

                CmbxFond.ItemsSource = Connection.marathonEntities.Charity.ToList();
                CmbxFond.DisplayMemberPath = "CharityName";
                CmbxFond.SelectedValuePath = "CharityId";
                CmbxFond.SelectedIndex = 0;

                CalculateTotalCost();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке данных: {ex.Message}", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private decimal CalculateTotalCost()
        {
            decimal total = 0;

            if (EventsList.ItemsSource is IEnumerable<EventTypeViewModel> eventTypes)
            {
                total += eventTypes.Where(e => e.IsSelected).Sum(e => e.Cost);
            }

            if (KitOptionsList.ItemsSource is IEnumerable<RaceKitOptionViewModel> kitOptions)
            {
                var selectedKit = kitOptions.FirstOrDefault(k => k.IsSelected);
                if (selectedKit != null)
                {
                    total += selectedKit.Cost;
                }
            }

            TotalCostText.Text = $"${total}";

            return total;
        }


        private void RegBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var currentUser = Connection.marathonEntities.User
                                .Include(x => x.Runner)
                                .FirstOrDefault(u => u.Email == CurrentUser.Email);

                if (currentUser == null || currentUser.Runner == null || currentUser.Runner.Count == 0)
                {
                    var result = MessageBox.Show("Для регистрации на марафон требуется профиль бегуна. Хотите создать его сейчас?", "Требуется профиль", MessageBoxButton.YesNo, MessageBoxImage.Question);

                    if (result == MessageBoxResult.Yes)
                    {
                        NavigationService.Navigate(new RunnerRegPage());
                    }
                    return;
                }

                var runner = currentUser.Runner.FirstOrDefault();
                if (runner == null)
                {
                    MessageBox.Show("Профиль бегуна не найден", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                var selectedEvents = ((IEnumerable<EventTypeViewModel>)EventsList.ItemsSource)
                        .Where(ev => ev.IsSelected)
                        .ToList();

                if (selectedEvents.Count == 0)
                {
                    MessageBox.Show("Пожалуйста, выберите хотя бы один вид марафона", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (!decimal.TryParse(TxtSum.Text.Trim('$'), out decimal sponsorshipAmount) || sponsorshipAmount < 0)
                {
                    MessageBox.Show("Пожалуйста, введите корректную сумму взноса", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (CmbxFond.SelectedItem == null)
                {
                    MessageBox.Show("Пожалуйста, выберите благотворительный фонд", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                var selectedKit = ((IEnumerable<RaceKitOptionViewModel>)KitOptionsList.ItemsSource)
                    .FirstOrDefault(k => k.IsSelected);

                if (selectedKit == null)
                {
                    MessageBox.Show("Пожалуйста, выберите вариант комплекта", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                var existingRegistration = Connection.marathonEntities.Registration
                    .FirstOrDefault(r => r.RunnerId == runner.RunnerId && (r.RegistrationStatusId == 1 || r.RegistrationStatusId == 2));

                if (existingRegistration != null)
                {
                    MessageBox.Show("У вас уже есть активная регистрация", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                var registration = new Registration
                {
                    RegistrationDateTime = DateTime.Now,
                    RaceKitOptionId = selectedKit.RaceKitOptionId,
                    RegistrationStatusId = 1,
                    CharityId = (int)CmbxFond.SelectedValue,
                    SponsorshipTarget = sponsorshipAmount,
                    Cost = CalculateTotalCost(),
                    RunnerId = runner.RunnerId 
                };

                Connection.marathonEntities.Registration.Add(registration);
                Connection.marathonEntities.SaveChanges();

                foreach (var eventType in selectedEvents)
                {
                    var eventObj = Connection.marathonEntities.Event
                        .FirstOrDefault(ev => ev.EventTypeId == eventType.EventTypeId);

                    if (eventObj != null)
                    {
                        var registrationEvent = new RegistrationEvent
                        {
                            RegistrationId = registration.RegistrationId,
                            EventId = eventObj.EventId
                        };
                        Connection.marathonEntities.RegistrationEvent.Add(registrationEvent);
                    }
                }

                Connection.marathonEntities.SaveChanges();

                MessageBox.Show("Регистрация успешно завершена!", "Успех",
                    MessageBoxButton.OK, MessageBoxImage.Information);

                NavigationService.Navigate(new MarathonRegConfirmPage());
                CalculateTotalCost();
            }
            catch (DbUpdateException ex)
            {
                MessageBox.Show($"Ошибка при сохранении данных: {ex.InnerException?.Message}",
                               "Ошибка базы данных",
                               MessageBoxButton.OK,
                               MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла непредвиденная ошибка: {ex.Message}",
                               "Ошибка",
                               MessageBoxButton.OK,
                               MessageBoxImage.Error);
            }
        }

        private void GoBackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void NameTxt_GotFocus(object sender, RoutedEventArgs e)
        {
            if (TxtSum.Text == "$500")
            {
                TxtSum.Text = string.Empty;
                TxtSum.Foreground = Brushes.Black;
            }
        }

        private void NameTxt_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TxtSum.Text))
            {
                TxtSum.Text = "$500";
                TxtSum.Foreground = Brushes.Gray;
            }
            else if (!TxtSum.Text.StartsWith("$"))
            {
                TxtSum.Text = "$" + TxtSum.Text.Trim('$');
            }
        }

        private void Fond_Click(object sender, RoutedEventArgs e)
        {
            if (CmbxFond.SelectedItem is Charity selectedCharity)
            {
                CharityWindow fondWindow = new CharityWindow(selectedCharity);
                fondWindow.Show();
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите благотворительный фонд",
                    "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void EventCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            CalculateTotalCost();
        }

        private void KitOptionRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            CalculateTotalCost();
        }

        private void TxtSum_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
