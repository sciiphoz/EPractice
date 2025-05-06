using EPractice.DBConnection;
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

namespace EPractice.Pages.SponsorPages
{
    /// <summary>
    /// Логика взаимодействия для SponsorPage.xaml
    /// </summary>
    public partial class SponsorPage : Page
    {
        Charity charity;
        Registration registration;
        public SponsorPage()
        {
            InitializeComponent();

            RunnersCB.ItemsSource = Connection.marathonEntities.Registration.ToList();
        }

        private void BtnPay_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(SponsorName.Text) || SponsorName.Text == "Ваше имя")
            {
                MessageBox.Show("Пожалуйста, введите ваше имя.");
                SponsorName.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(CardHolderName.Text) || CardHolderName.Text == "Владелец карты")
            {
                MessageBox.Show("Пожалуйста, введите имя владельца карты.");
                CardHolderName.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(CardNumTxt.Text) || CardNumTxt.Text.Length != 19 || !IsCardNumberValid(CardNumTxt.Text))
            {
                MessageBox.Show("Номер карты некорректен. Он должен состоять из 16 цифр и соответствовать формату '0000 0000 0000 0000'.");
                CardNumTxt.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(CardMonth.Text) || string.IsNullOrWhiteSpace(CardYear.Text) || !IsExpirationDateValid(CardMonth.Text, CardYear.Text))
            {
                MessageBox.Show("Срок действия карты некорректен. Пожалуйста, введите действительный месяц и год.");
                CardMonth.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(CVCTxt.Text) || CVCTxt.Text.Length != 3 || !CVCTxt.Text.All(char.IsDigit))
            {
                MessageBox.Show("CVC код некорректен. Он должен состоять из 3 цифр.");
                CVCTxt.Focus();
                return;
            }

            if (!int.TryParse(MoneyTB.Text, out int amount) || amount <= 0)
            {
                MessageBox.Show("Пожалуйста, введите корректную сумму пожертвования (число больше 0).");
                MoneyTB.Focus();
                return;
            }

            Sponsorship sponsorship = new Sponsorship
            {
                SponsorName = SponsorName.Text,
                Amount = amount,
                RegistrationId = registration.RegistrationId
            };

            Connection.marathonEntities.Sponsorship.Add(sponsorship);
            Connection.marathonEntities.SaveChanges();

            SponsorWindow sponsorWindow = Window.GetWindow(this) as SponsorWindow;
            sponsorWindow.ConfirmSponsor(sponsorship);
        }

        private bool IsCardNumberValid(string cardNumber)
        {
            return cardNumber.Replace(" ", "").All(char.IsDigit);
        }
        private bool IsExpirationDateValid(string month, string year)
        {
            if (!int.TryParse(month, out int monthValue) || !int.TryParse(year, out int yearValue))
            {
                return false;
            }

            if (monthValue < 1 || monthValue > 12)
                return false;

            int fullYear = DateTime.Now.Year - (DateTime.Now.Year % 100) + yearValue;

            var currentDate = DateTime.Now;

            if (fullYear < currentDate.Year || (fullYear == currentDate.Year && monthValue < currentDate.Month))
            {
                return false;
            }

            if (yearValue < 25)
            {
                return false;
            }

            return true;
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            SponsorWindow sponsorWindow = Window.GetWindow(this) as SponsorWindow;

            sponsorWindow.OpenMainWindow();
        }

        private void BtnPlus_Click(object sender, RoutedEventArgs e)
        {
            int money = int.Parse(MoneyTB.Text);
            money += 10;
            MoneyTB.Text = money.ToString();
        }

        private void BtnMinus_Click(object sender, RoutedEventArgs e)
        {
            int money = int.Parse(MoneyTB.Text);
            if (money >= 10)
            {
                money -= 10;
                MoneyTB.Text = money.ToString();
            }
        }

        private void RunnersCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            registration = RunnersCB.SelectedItem as Registration;
            charity = registration.Charity;
            CharityName.Content = charity.CharityName;
        }
        private void Fond_Click(object sender, RoutedEventArgs e)
        {
            CharityWindow charityWindow = new CharityWindow(charity);

            charityWindow.Show();
        }
    }
}
