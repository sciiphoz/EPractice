using EPractice.Windows;
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
    /// Логика взаимодействия для AdminButtonPage.xaml
    /// </summary>
    public partial class AdminButtonPage : Page
    {
        public AdminButtonPage()
        {
            InitializeComponent();
        }

        private void EquipBtn_Click(object sender, RoutedEventArgs e)
        {
            AdminWindow adminWindow = Window.GetWindow(this) as AdminWindow;
            adminWindow.OpenEquipment();
        }

        private void OrgsBtn_Click(object sender, RoutedEventArgs e)
        {
            AdminWindow adminWindow = Window.GetWindow(this) as AdminWindow;
            adminWindow.OpenOrganizations();
        }

        private void VolunteerBtn_Click(object sender, RoutedEventArgs e)
        {
            AdminWindow adminWindow = Window.GetWindow(this) as AdminWindow;
            adminWindow.OpenVolunteers();
        }
    }
}
