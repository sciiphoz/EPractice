using EPractice.Pages;
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
                default:
                    break;
            }
        }
    }
}
