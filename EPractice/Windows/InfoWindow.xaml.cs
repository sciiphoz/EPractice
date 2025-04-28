using EPractice.Pages.InfoPages;
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
using System.Windows.Shapes;

namespace EPractice.Windows
{
    /// <summary>
    /// Логика взаимодействия для InfoWindow.xaml
    /// </summary>
    public partial class InfoWindow : Window
    {
        private int page;
        public InfoWindow()
        {
            InitializeComponent();
            MainFrame.Navigate(new InfoButtonPage());
            page = 0;
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            switch (page)
            {
                case 0:
                    MainWindow mainWindow = new MainWindow();
                    mainWindow.Show();
                    Close();
                    break;
                case 1:
                    MainFrame.Navigate(new InfoButtonPage());
                    page = 0;
                    break;
            }
        }
        public void OpenBMI()
        {
            MainFrame.Navigate(new BMIPage());
            page = 1;
        }
        public void OpenBMR()
        {
            MainFrame.Navigate(new BMRPage());
            page = 1;
        }
        public void OpenMarathonSkills()
        {
            MainFrame.Navigate(new MarathonSkillsPage());
            page = 1;
        }
        public void OpenComparison()
        {
            MainFrame.Navigate(new ComparisonPage());
            page = 1;
        }
        public void OpenOrganizations()
        {
            MainFrame.Navigate(new OrgPage());
            page = 1;
        }
        public void OpenPastResults()
        {
            MainFrame.Navigate(new PastRes Page());
            page = 1;
        }
    }
}
