using EPractice.Pages;
using EPractice.Pages.InfoPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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
    /// Логика взаимодействия для AuthWindow.xaml
    /// </summary>
    public partial class AuthWindow : Window
    {
        private int page;
        public AuthWindow()
        {
            InitializeComponent();
            MainFrame.Navigate(new AuthPage());
            //MainFrame.Navigate(new ComparisonPage());
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
                    MainFrame.Navigate(new AuthPage());
                    page = 0;
                    break;
                default:
                    break;
            }
        }

        public void OpenMainWindow()
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
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
    }
}
