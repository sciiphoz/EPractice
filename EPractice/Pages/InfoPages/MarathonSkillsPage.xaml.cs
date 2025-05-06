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
using EPractice.Windows;

namespace EPractice.Pages.InfoPages
{
    /// <summary>
    /// Логика взаимодействия для MarathonSkillsPage.xaml
    /// </summary>
    public partial class MarathonSkillsPage : Page
    {
        public MarathonSkillsPage()
        {
            InitializeComponent();
        }

        private void MapButton_Click(object sender, RoutedEventArgs e)
        {
            InfoWindow infoWindow = Window.GetWindow(this) as InfoWindow;
            infoWindow.OpenInteractiveMap();
        }
    }
}
