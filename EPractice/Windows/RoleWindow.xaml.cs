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
    /// Логика взаимодействия для RoleWindow.xaml
    /// </summary>
    public partial class RoleWindow : Window
    {
        public RoleWindow()
        {
            InitializeComponent();
        }

        private void RunnerButton_Click(object sender, RoutedEventArgs e)
        {
            RunnerWindow runnerWindow = new RunnerWindow();
            runnerWindow.Show();    
            DialogResult = true;
            Close();

            runnerWindow.OpenButtonPage();
        }
        private void ControllerButton_Click(object sender, RoutedEventArgs e)
        {
            ControllerWindow controllerWindow = new ControllerWindow();
            controllerWindow.Show();
            DialogResult = true;
            Close();

            controllerWindow.OpenButtonPage();
        }
        private void AdminButton_Click(object sender, RoutedEventArgs e)
        {
            AdminWindow adminWindow = new AdminWindow();
            adminWindow.Show();
            DialogResult = true;
            Close();

            adminWindow.OpenButtonPage();
        }
    }
}
