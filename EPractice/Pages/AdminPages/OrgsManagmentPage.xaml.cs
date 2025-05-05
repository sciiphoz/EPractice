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
    /// Логика взаимодействия для OrgsManagmentPage.xaml
    /// </summary>
    public partial class OrgsManagmentPage : Page
    {
        public OrgsManagmentPage()
        {
            InitializeComponent();
            LoadCharities();
        }

        private void LoadCharities()
        {
            try
            {
                LvCharity.ItemsSource = Connection.marathonEntities.Charity.ToList();
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

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new OrgEditPage());
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button != null && button.Tag != null)
            {
                int charityId = (int)button.Tag;
                var charityToEdit = Connection.marathonEntities.Charity.FirstOrDefault(c => c.CharityId == charityId);
                if (charityToEdit != null)
                {
                    NavigationService.Navigate(new OrgEditPage(charityToEdit));
                }
            }
        }

    }
}
