using EPractice.Classes;
using EPractice.Pages.ControllerPages;
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
using System.Windows.Threading;

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
            StartTimer();
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
                default:
                    break;
            }
        }
        public void OpenButtonsPage()
        {
            MainFrame.Navigate(new InfoButtonPage());
            page = 0;
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
            MainFrame.Navigate(new OrgsPage());
            page = 1;
        }
        public void OpenPastResults()
        {
            MainFrame.Navigate(new PastResPage());
            page = 1;
        }

        private DispatcherTimer timer;
        private DateTime marathonStartDate;

        private void UpdateTimeLeft()
        {
            TimeSpan timeLeft = marathonStartDate - DateTime.Now;
            if (timeLeft.TotalMilliseconds <= 0)
            {
                TimeLeftTB.Text = "Марафон уже начался!";
                timer.Stop();
            }
            else
            {
                TimeLeftTB.Text = $"{timeLeft.Days} дней {timeLeft.Hours} часов и {timeLeft.Minutes} минут до старта марафона!";
            }
        }

        private void StartTimer()
        {
            marathonStartDate = new DateTime(2025, 05, 14, 14, 0, 0);

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            timer.Start();
            UpdateTimeLeft();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            UpdateTimeLeft();
        }
        private void LogOutButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            CurrentUser.Email = null;
            Close();
        }
    }
}
