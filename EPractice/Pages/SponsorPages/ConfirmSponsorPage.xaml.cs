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
    /// Логика взаимодействия для ConfirmSponsorPage.xaml
    /// </summary>
    public partial class ConfirmSponsorPage : Page
    {
        Sponsorship sponsorship;
        Charity charity;

        private SponsorWindow sponsorWindow;
        public ConfirmSponsorPage(Sponsorship sponsorshipData)
        {
            InitializeComponent();

            sponsorship = sponsorshipData;
            
            Registration begun = sponsorship.Registration;
            User beg = begun.Runner.User;
            Country BegunCountry = begun.Runner.Country;

            RunnerTxt.Content = $"{beg.FirstName} {beg.LastName}({begun.RegistrationId}) из {BegunCountry.CountryName}";
            MoneyTxt.Content = $"${sponsorship.Amount}";
            
            charity = sponsorship.Registration.Charity;
            
            CharityTxt.Content = charity.CharityName;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Window.GetWindow(this).Close();
        }
    }
}
