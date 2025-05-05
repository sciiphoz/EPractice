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
using EPractice.Classes;
using EPractice.DBConnection;
using EPractice.Windows;

namespace EPractice.Pages
{
    /// <summary>
    /// Логика взаимодействия для AuthPage.xaml
    /// </summary>
    public partial class AuthPage : Page
    {
        public User user;
        public AuthPage()
        {
            InitializeComponent();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            AuthWindow authWindow = Window.GetWindow(this) as AuthWindow;
            authWindow.OpenMainWindow();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string login = EmailTB.Text;
            string password = PasswordTB.Password;

            var loginObj = Connection.marathonEntities.User.FirstOrDefault(log => log.Email == login && log.Password == password);

            if (loginObj == null)
            {
                MessageBox.Show("Пользователь не найден");
                return;
            }
            else if (loginObj != null)
            {
                CurrentUser.Email = loginObj.Email;

                if (loginObj.RoleId == "R")
                {
                    RunnerWindow runnerWindow = new RunnerWindow();
                    runnerWindow.Show();
                    Window.GetWindow(this).Close();
                }
                else if (loginObj.RoleId == "A")
                {
                    RunnerWindow runnerWindow = new RunnerWindow();
                    runnerWindow.Show();
                    Window.GetWindow(this).Close();
                }
                else if (loginObj.RoleId == "C")
                {
                    RunnerWindow runnerWindow = new RunnerWindow();
                    runnerWindow.Show();
                    Window.GetWindow(this).Close();
                }
            }
            EmailTB.Text = "";
            PasswordTB.Password = "";
        }
    }
}
