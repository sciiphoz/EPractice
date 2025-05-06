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

namespace EPractice.Pages
{
    /// <summary>
    /// Логика взаимодействия для InteractiveMapPage.xaml
    /// </summary>
    public partial class InteractiveMapPage : Page
    {
        Uri drink = new Uri("/Images/InteractiveMap/map-icon-drinks.png", UriKind.Relative);
        Uri energy = new Uri("/Images/InteractiveMap/map-icon-energy-bars.png", UriKind.Relative);
        Uri med = new Uri("/Images/InteractiveMap/map-icon-medical.png", UriKind.Relative);
        Uri info = new Uri("/Images/InteractiveMap/map-icon-information.png", UriKind.Relative);
        Uri wc = new Uri("/Images/InteractiveMap/map-icon-toilets.png", UriKind.Relative);
        public InteractiveMapPage()
        {
            InitializeComponent();
        }
        private void btnCheck1_Click(object sender, RoutedEventArgs e)
        {
            txtCheckpoint.Text = "Точка №1";
            imgDrink.Source = new BitmapImage(drink);
            imgEnergy.Source = new BitmapImage(energy);
            imgWC.Source = null;
            imgMed.Source = null;
            imgInfo.Source = null;

        }

        private void btnCheck2_Click(object sender, RoutedEventArgs e)
        {
            txtCheckpoint.Text = "Точка №2";
            imgDrink.Source = new BitmapImage(drink);
            imgEnergy.Source = new BitmapImage(energy);
            imgWC.Source = new BitmapImage(wc);
            imgMed.Source = new BitmapImage(med);
            imgInfo.Source = new BitmapImage(info);
        }

        private void btnCheck3_Click(object sender, RoutedEventArgs e)
        {
            txtCheckpoint.Text = "Точка №3";
            imgDrink.Source = new BitmapImage(drink);
            imgEnergy.Source = new BitmapImage(energy);
            imgWC.Source = new BitmapImage(wc);
            imgMed.Source = null;
            imgInfo.Source = null;
        }

        private void btnCheck4_Click(object sender, RoutedEventArgs e)
        {
            txtCheckpoint.Text = "Точка №4";
            imgDrink.Source = new BitmapImage(drink);
            imgEnergy.Source = new BitmapImage(energy);
            imgWC.Source = new BitmapImage(wc);
            imgMed.Source = new BitmapImage(med);
            imgInfo.Source = null;
        }

        private void btnCheck5_Click(object sender, RoutedEventArgs e)
        {
            txtCheckpoint.Text = "Точка №5";
            imgDrink.Source = new BitmapImage(drink);
            imgEnergy.Source = new BitmapImage(energy);
            imgWC.Source = new BitmapImage(wc);
            imgMed.Source = new BitmapImage(med);
            imgInfo.Source = null;
        }

        private void btnCheck6_Click(object sender, RoutedEventArgs e)
        {
            txtCheckpoint.Text = "Точка №6";
            imgDrink.Source = new BitmapImage(drink);
            imgEnergy.Source = new BitmapImage(energy);
            imgWC.Source = new BitmapImage(wc);
            imgMed.Source = new BitmapImage(med);
            imgInfo.Source = new BitmapImage(info);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
