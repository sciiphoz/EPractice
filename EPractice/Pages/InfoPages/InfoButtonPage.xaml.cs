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

namespace EPractice.Pages.InfoPages
{
    /// <summary>
    /// Логика взаимодействия для InfoButtonPage.xaml
    /// </summary>
    public partial class InfoButtonPage : Page
    {
        public InfoButtonPage()
        {
            InitializeComponent();
        }

        private void BMIButton_Click(object sender, RoutedEventArgs e)
        {
            InfoWindow infoWindow = Window.GetWindow(this) as InfoWindow;

            infoWindow.OpenBMI();
        }

        private void BMRButton_Click(object sender, RoutedEventArgs e)
        {
            InfoWindow infoWindow = Window.GetWindow(this) as InfoWindow;

            infoWindow.OpenBMR();
        }

        private void MarathonBtn_Click(object sender, RoutedEventArgs e)
        {
            InfoWindow infoWindow = Window.GetWindow(this) as InfoWindow;

            infoWindow.OpenMarathonSkills();
        }

        private void PastResultsBtn_Click(object sender, RoutedEventArgs e)
        {
            InfoWindow infoWindow = Window.GetWindow(this) as InfoWindow;

            infoWindow.OpenPastResults();
        }

        private void ComparisonBtn_Click(object sender, RoutedEventArgs e)
        {
            InfoWindow infoWindow = Window.GetWindow(this) as InfoWindow;

            infoWindow.OpenComparison();
        }

        private void OrgsListBtn_Click(object sender, RoutedEventArgs e)
        {
            InfoWindow infoWindow = Window.GetWindow(this) as InfoWindow;

            infoWindow.OpenOrganizations();
        }
    }
}
