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

namespace EPractice.Pages.RunnerPages
{
    /// <summary>
    /// Логика взаимодействия для RunnerButtonPage.xaml
    /// </summary>
    public partial class RunnerButtonPage : Page
    {
        public RunnerButtonPage()
        {
            InitializeComponent();
        }

        private void MarathonRegButton_Click(object sender, RoutedEventArgs e)
        {
            RunnerWindow runnerWindow = Window.GetWindow(this) as RunnerWindow;
            runnerWindow.OpenMarathonReg();
        }

        private void MyResultsButton_Click(object sender, RoutedEventArgs e)
        {
            RunnerWindow runnerWindow = Window.GetWindow(this) as RunnerWindow;
            runnerWindow.OpenResults();
        }

        private void MySponsorsButton_Click(object sender, RoutedEventArgs e)
        {
            RunnerWindow runnerWindow = Window.GetWindow(this) as RunnerWindow;
            runnerWindow.OpenMySponsors();
        }

        private void ContactsButton_Click(object sender, RoutedEventArgs e)
        {
            ContactsWindow contactsWindow = new ContactsWindow();
            contactsWindow.ShowDialog();
        }
    }
}
