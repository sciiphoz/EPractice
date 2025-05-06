using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using EPractice.DBConnection;

namespace EPractice.Pages.AdminPages
{
    /// <summary>
    /// Логика взаимодействия для VolunteersPage.xaml
    /// </summary>
    public partial class VolunteersPage : Page
    {
        MarathonEntities _context = new MarathonEntities();
        public VolunteersPage()
        {
            InitializeComponent();
            VolunteersListView.ItemsSource = _context.Volunteer.ToList();
        }

        private void LoadVolunteers(string sortBy = "LastName")
        {
            try
            {
                var volunteers = new List<Volunteer>();

                switch (sortBy)
                {
                    case "FirstName":
                        volunteers = _context.Volunteer.OrderBy(v => v.FirstName).ToList();
                        break;
                    case "CountryCode":
                        volunteers = _context.Volunteer.OrderBy(v => v.CountryCode).ToList();
                        break;
                    case "Gender":
                        volunteers = _context.Volunteer.OrderBy(v => v.Gender).ToList();
                        break;
                    default:
                        volunteers = _context.Volunteer.OrderBy(v => v.LastName).ToList();
                        break;
                }

                if (VolunteersListView == null)
                {
                    Console.WriteLine();
                }
                else
                {
                    VolunteersListView.ItemsSource = volunteers;
                    VolunteerCountText.Text = $"Всего волонтеров: {volunteers.Count}";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке данных: {ex.Message}", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void GoBackButton_Click(object sender, RoutedEventArgs e)
        {
            if (NavigationService.CanGoBack)
            {
                NavigationService.GoBack();
            }
        }

        private void SortComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SortComboBox.SelectedItem is ComboBoxItem selectedItem)
            {
                string sortBy = selectedItem.Content.ToString();
                switch (sortBy)
                {
                    case "Фамилии":
                        LoadVolunteers("LastName");
                        break;
                    case "Имени":
                        LoadVolunteers("FirstName");
                        break;
                    case "Стране":
                        LoadVolunteers("CountryCode");
                        break;
                    case "Полу":
                        LoadVolunteers("Gender");
                        break;
                }
            }
        }

        private void ImportButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new VolunteerImportPage());
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            LoadVolunteers();
        }
    }
}
