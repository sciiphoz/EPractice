﻿using EPractice.Classes;
using EPractice.DBConnection;
using EPractice.Pages.SponsorPages;
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
    /// Логика взаимодействия для SponsorWindow.xaml
    /// </summary>
    public partial class SponsorWindow : Window
    {
        private int page;
        public SponsorWindow()
        {
            InitializeComponent();
            MainFrame.Navigate(new SponsorPage());
            page = 0;
            StartTimer();
        }
        public void ConfirmSponsor(Sponsorship sponsorship)
        {
            MainFrame.Navigate(new ConfirmSponsorPage(sponsorship));
            page = 1;
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
                    MainFrame.Navigate(new SponsorPage());
                    page = 0;
                    break;
                default:
                    break;
            }
        }

        public void OpenMainWindow()
        {
            page = 0;
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
        private void LogOutButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            CurrentUser.Email = null;
            Close();
        }
    }
}
