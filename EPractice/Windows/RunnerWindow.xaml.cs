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
using EPractice.Classes;
using EPractice.Pages;
using EPractice.Pages.ControllerPages;
using EPractice.Pages.RunnerPages;

namespace EPractice.Windows
{
    /// <summary>
    /// Логика взаимодействия для RunnerWindow.xaml
    /// </summary>
    public partial class RunnerWindow : Window
    {
        private int page;
        public RunnerWindow()
        {
            InitializeComponent();
            MainFrame.Navigate(new RegistrationPage());
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
                    MainFrame.Navigate(new RunnerInfoPage());
                    page = 0;
                    break;
                default:
                    break;
            }
        }
        public void OpenRegistrationPage()
        {
            MainFrame.Navigate(new RegistrationPage());
            page = 0;
        }
        public void OpenMySponsors()
        {
            MainFrame.Navigate(new MySponsorsPage());
            page = 1;
        }
        public void OpenResults()
        {
            MainFrame.Navigate(new ResultsPage());
            page = 1;
        }
        public void OpenMarathonReg()
        {
            MainFrame.Navigate(new MarathonRegPage());
            page = 1;
        }
        public void OpenMarathonRegConfirm()
        {
            MainFrame.Navigate(new MarathonRegConfirmPage());
            page = 1;
        }
        public void OpenRunnerRegisterPage()
        {
            MainFrame.Navigate(new RunnerRegPage());
            page = 0;
        }
        public void OpenRunnerInfo()
        {
            MainFrame.Navigate(new RunnerInfoPage());
            page = 1;
        }
        public void OpenAuth()
        {
            AuthWindow authWindow = new AuthWindow();
            authWindow.Show();
            Close();
        }

        public void OpenButtonPage()
        {
            MainFrame.Navigate(new RunnerButtonPage());
            page = 0;
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
