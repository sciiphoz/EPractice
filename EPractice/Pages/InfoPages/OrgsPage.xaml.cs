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

namespace EPractice.Pages.InfoPages
{
    /// <summary>
    /// Логика взаимодействия для OrgsPage.xaml
    /// </summary>
    public partial class OrgsPage : Page
    {
        public OrgsPage()
        {
            InitializeComponent();
            LvCharity.ItemsSource = Connection.marathonEntities.Charity.ToList();
        }
        private void GoBackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
