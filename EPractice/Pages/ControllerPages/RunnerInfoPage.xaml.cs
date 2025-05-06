using EPractice.DBConnection;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace EPractice.Pages.ControllerPages
{
    /// <summary>
    /// Логика взаимодействия для RunnerInfoPage.xaml
    /// </summary>
    public partial class RunnerInfoPage : Page
    {
        public RunnerInfoPage()
        {
            InitializeComponent();
            LoadRunnersData();
        }

        private void LoadRunnersData()
        {
            try
            {
                if (StatusComboBox == null || DistanceComboBox == null || SortComboBox == null)
                {
                    return;
                }

                string statusFilter = (StatusComboBox.SelectedItem as ComboBoxItem)?.Tag?.ToString() ?? "All";
                string distanceFilter = (DistanceComboBox.SelectedItem as ComboBoxItem)?.Tag?.ToString() ?? "All";
                string sortField = (SortComboBox.SelectedItem as ComboBoxItem)?.Tag?.ToString() ?? "FirstName";

                var runners = GetFilteredRunners(statusFilter, distanceFilter, sortField);

                TotalRunnersText.Text = $"Всего бегунов: {runners.Count}";

                if (runners.Any())
                {
                    RunnersGrid.ItemsSource = runners;
                    NoResultsText.Visibility = Visibility.Collapsed;
                    RunnersGrid.Visibility = Visibility.Visible;
                }
                else
                {
                    RunnersGrid.Visibility = Visibility.Collapsed;
                    NoResultsText.Visibility = Visibility.Visible;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке данных бегунов: {ex.Message}");
            }
        }

        private List<RunnerInfo> GetFilteredRunners(string statusFilter, string distanceFilter, string sortField)
        {
            var query = Connection.marathonEntities.Runner.AsQueryable();

            if (statusFilter != "All")
            {
                query = query.Where(r => r.Registration.Any(reg =>
                    reg.RegistrationStatus.RegistrationStatus1 == statusFilter));
            }

            if (distanceFilter != "All")
            {
                query = query.Where(r => r.Registration.Any(reg =>
                    reg.RegistrationEvent.Any(re =>
                        re.Event.EventType.EventTypeName.Contains(distanceFilter))));
            }

            switch (sortField)
            {
                case "FirstName":
                    query = query.OrderBy(r => r.User.FirstName);
                    break;
                case "LastName":
                    query = query.OrderBy(r => r.User.LastName);
                    break;
                case "Email":
                    query = query.OrderBy(r => r.Email);
                    break;
                case "Status":
                    query = query.OrderBy(r => r.Registration.FirstOrDefault().RegistrationStatus.RegistrationStatus1);
                    break;
                default:
                    query = query.OrderBy(r => r.User.FirstName);
                    break;
            }

            return query.Select(r => new RunnerInfo
            {
                RunnerId = r.RunnerId,
                FirstName = r.User.FirstName,
                LastName = r.User.LastName,
                Email = r.Email,
                Status = r.Registration.FirstOrDefault().RegistrationStatus.RegistrationStatus1
            }).ToList();
        }

        private void ExportDetailsButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var runnerIds = (RunnersGrid.ItemsSource as IEnumerable<RunnerInfo>)?.Select(r => r.RunnerId).ToList();
                if (runnerIds == null || !runnerIds.Any())
                {
                    MessageBox.Show("Нет данных для экспорта", "Экспорт данных", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                var saveFileDialog = new Microsoft.Win32.SaveFileDialog
                {
                    Filter = "CSV файлы (*.csv)|*.csv",
                    DefaultExt = ".csv",
                    FileName = $"Marathon_Runners_Export_{DateTime.Now:yyyyMMdd_HHmmss}.csv"
                };

                if (saveFileDialog.ShowDialog() != true)
                    return;

                var runnersData = Connection.marathonEntities.Runner
                    .Where(r => runnerIds.Contains(r.RunnerId))
                    .Select(r => new
                    {
                        r.RunnerId,
                        r.User.FirstName,
                        r.User.LastName,
                        r.Email,
                        r.Gender,
                        Country = r.Country.CountryName,
                        r.DateOfBirth,
                        RegistrationStatus = r.Registration.FirstOrDefault().RegistrationStatus.RegistrationStatus1,
                        RaceKitOption = r.Registration.FirstOrDefault().RaceKitOption.RaceKitOption1,
                        MarathonTypes = r.Registration.FirstOrDefault().RegistrationEvent
                            .Select(re => re.Event.EventType.EventTypeName)
                            .Distinct()
                    })
                    .ToList();

                var csvContent = new StringBuilder();

                csvContent.AppendLine("Имя,Фамилия,Эл. адрес,Пол,Страна,Дата рождения,Возраст,Состояние регистрации,Выбранный комплект,Тип марафонов");

                foreach (var runner in runnersData)
                {
                    var marathonTypes = runner.MarathonTypes != null
                        ? string.Join(", ", runner.MarathonTypes)
                        : "не указано";

                    var age = runner.DateOfBirth.HasValue
                        ? (DateTime.Now.Year - runner.DateOfBirth.Value.Year -
                           (DateTime.Now.DayOfYear < runner.DateOfBirth.Value.DayOfYear ? 1 : 0)).ToString()
                        : "не указано";

                    csvContent.AppendLine(
                        $"\"{runner.FirstName ?? "не указано"}\"," +
                        $"\"{runner.LastName ?? "не указано"}\"," +
                        $"\"{runner.Email}\"," +
                        $"\"{runner.Gender}\"," +
                        $"\"{runner.Country ?? "не указано"}\"," +
                        $"\"{runner.DateOfBirth?.ToString("dd.MM.yyyy") ?? "не указано"}\"," +
                        $"\"{age}\"," +
                        $"\"{runner.RegistrationStatus ?? "не указано"}\"," +
                        $"\"{runner.RaceKitOption ?? "не указано"}\"," +
                        $"\"{marathonTypes}\"");
                }

                File.WriteAllText(saveFileDialog.FileName, csvContent.ToString(), Encoding.UTF8);

                MessageBox.Show($"Данные успешно экспортированы в файл:\n{saveFileDialog.FileName}",
                    "Экспорт завершен", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при экспорте данных: {ex.Message}",
                    "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ExportEmailsButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var runnerIds = (RunnersGrid.ItemsSource as IEnumerable<RunnerInfo>)?.Select(r => r.RunnerId).ToList();
                if (runnerIds == null || !runnerIds.Any())
                {
                    MessageBox.Show("Нет данных для экспорта", "Экспорт email", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                var runnersEmails = Connection.marathonEntities.Runner
                    .Where(r => runnerIds.Contains(r.RunnerId))
                    .Select(r => new
                    {
                        r.User.FirstName,
                        r.User.LastName,
                        r.Email
                    })
                    .ToList();

                var emailsList = new StringBuilder();
                foreach (var runner in runnersEmails)
                {
                    emailsList.Append($"\"{runner.LastName} {runner.FirstName}\" <{runner.Email}>; ");
                }

                if (emailsList.Length > 0)
                {
                    emailsList.Length -= 2;
                }

                var emailWindow = new Window
                {
                    Title = "Список email бегунов",
                    Width = 600,
                    Height = 400,
                    WindowStartupLocation = WindowStartupLocation.CenterScreen
                };

                var textBox = new TextBox
                {
                    Text = emailsList.ToString(),
                    TextWrapping = TextWrapping.Wrap,
                    VerticalScrollBarVisibility = ScrollBarVisibility.Auto,
                    HorizontalScrollBarVisibility = ScrollBarVisibility.Auto,
                    Margin = new Thickness(10),
                    IsReadOnly = true,
                    FontFamily = new FontFamily("Courier New"),
                    AcceptsReturn = true,
                    VerticalAlignment = VerticalAlignment.Stretch,
                    HorizontalAlignment = HorizontalAlignment.Stretch
                };

                var copyButton = new Button
                {
                    Content = "Копировать в буфер",
                    Margin = new Thickness(10),
                    HorizontalAlignment = HorizontalAlignment.Right,
                    Background = (Brush)new BrushConverter().ConvertFrom("#FFFDC100")
                };

                copyButton.Click += (s, args) =>
                {
                    Clipboard.SetText(textBox.Text);
                    MessageBox.Show("Список email скопирован в буфер обмена", "Копирование", MessageBoxButton.OK, MessageBoxImage.Information);
                };

                var stackPanel = new StackPanel();
                stackPanel.Children.Add(textBox);
                stackPanel.Children.Add(copyButton);

                emailWindow.Content = stackPanel;
                emailWindow.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при экспорте email: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Filters_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LoadRunnersData();
        }

        private void SortComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LoadRunnersData();
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            LoadRunnersData();
        }

        private void GoBackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new MainWindow());
        }
    }

    public class RunnerInfo
    {
        public int RunnerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Status { get; set; }
    }
}

