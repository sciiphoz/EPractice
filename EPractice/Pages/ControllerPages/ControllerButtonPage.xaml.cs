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

namespace EPractice.Pages.ControllerPages
{
    /// <summary>
    /// Логика взаимодействия для ControllerButtonPage.xaml
    /// </summary>
    public partial class ControllerButtonPage : Page
    {
        public ControllerButtonPage()
        {
            InitializeComponent();
        }

        private void RunnersBtn_Click(object sender, RoutedEventArgs e)
        {
            ControllerWindow controllerWindow = Window.GetWindow(this) as ControllerWindow;
            controllerWindow.OpenRunnerInfo();
        }

        private void SponsorsBtn_Click(object sender, RoutedEventArgs e)
        {
            ControllerWindow controllerWindow = Window.GetWindow(this) as ControllerWindow;
            controllerWindow.OpenSponsorInfo();
        }
    }
}
