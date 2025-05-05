using EPractice.DBConnection;
using System;
using System.Collections.Generic;
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

namespace EPractice.Pages.AdminPages
{
    /// <summary>
    /// Логика взаимодействия для VolunteersPage.xaml
    /// </summary>
    public partial class VolunteersPage : Page
    {
        public VolunteersPage()
        {
            InitializeComponent();
            LoadVolunteers();
        }

        private void LoadVolunteers(string sortBy = "LastName")
        {
            try
            {
                if (VolunteersListView == null) // Дополнительная проверка
                {
                    MessageBox.Show("ListView не инициализирован", "Ошибка",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                IQueryable<Volunteer> query = Connection.marathonEntities.Volunteer;

                switch (sortBy)
                {
                    case "FirstName":
                        query = query.OrderBy(v => v.FirstName);
                        break;
                    case "CountryCode":
                        query = query.OrderBy(v => v.CountryCode);
                        break;
                    case "Gender":
                        query = query.OrderBy(v => v.Gender);
                        break;
                    default: // LastName
                        query = query.OrderBy(v => v.LastName);
                        break;
                }

                var volunteers = query.ToList();
                VolunteersListView.ItemsSource = volunteers;
                VolunteerCountText.Text = $"Всего волонтеров: {volunteers.Count}";
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
