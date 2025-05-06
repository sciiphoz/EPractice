using EPractice.Classes;
using EPractice.DBConnection;
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

namespace EPractice.Pages.RunnerPages
{
    /// <summary>
    /// Логика взаимодействия для MySponsorsPage.xaml
    /// </summary>
    public partial class MySponsorsPage : Page
    {
        public MySponsorsPage()
        {
            InitializeComponent();
            LoadSponsorsData();
        }

        private void LoadSponsorsData()
        {
            try
            {
                var runner = Connection.marathonEntities.Runner
                    .FirstOrDefault(r => r.Email == CurrentUser.Email);

                if (runner == null) return;

                var registration = runner.Registration.FirstOrDefault();
                if (registration == null)
                {
                    NoSponsorsText.Visibility = Visibility.Visible;
                    SponsorsGrid.Visibility = Visibility.Collapsed;
                    return;
                }

                var charity = registration.Charity;
                if (charity != null)
                {
                    CharityNameText.Text = charity.CharityName;
                    CharityDescriptionText.Text = charity.CharityDescription;

                    if (!string.IsNullOrEmpty(charity.CharityLogo))
                    {
                        try
                        {
                            string logoPath = System.IO.Path.Combine(
                                AppDomain.CurrentDomain.BaseDirectory,
                                "CharityImages",
                                charity.CharityLogo);

                            if (System.IO.File.Exists(logoPath))
                            {
                                var logoImage = new BitmapImage(new Uri(logoPath));
                                CharityLogoImage.Source = logoImage;
                            }
                            else
                            {
                                var uri = new Uri($"pack://application:,,,/Images/CharityImages/{charity.CharityLogo}");
                                CharityLogoImage.Source = new BitmapImage(uri);
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Не удалось загрузить логотип: {ex.Message}");
                        }
                    }
                }

                var sponsorships = Connection.marathonEntities.Sponsorship
                    .Where(s => s.RegistrationId == registration.RegistrationId)
                    .ToList();

                if (sponsorships.Any())
                {
                    var sponsorList = new List<SponsorInfo>();
                    foreach (var s in sponsorships)
                    {
                        var sponsor = new SponsorInfo
                        {
                            SponsorName = s.SponsorName,
                            Amount = s.Amount
                        };

                        if (charity != null && !string.IsNullOrEmpty(charity.CharityLogo))
                        {
                            try
                            {
                                string logoPath = System.IO.Path.Combine(
                                    AppDomain.CurrentDomain.BaseDirectory,
                                    "CharityImages",
                                    charity.CharityLogo);

                                if (System.IO.File.Exists(logoPath))
                                {
                                    sponsor.LogoImage = new BitmapImage(new Uri(logoPath));
                                }
                                else
                                {
                                    var uri = new Uri($"pack://application:,,,/Images/CharityImages/{charity.CharityLogo}");
                                    sponsor.LogoImage = new BitmapImage(uri);
                                }
                            }
                            catch
                            {
                                sponsor.LogoImage = null;
                            }
                        }

                        sponsorList.Add(sponsor);
                    }

                    SponsorsGrid.ItemsSource = sponsorList;
                    NoSponsorsText.Visibility = Visibility.Collapsed;

                    decimal totalAmount = sponsorships.Sum(s => s.Amount);
                    TotalAmountText.Text = $"${totalAmount}";
                }
                else
                {
                    SponsorsGrid.Visibility = Visibility.Collapsed;
                    NoSponsorsText.Visibility = Visibility.Visible;
                    TotalAmountText.Text = "$0";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке данных спонсоров: {ex.Message}");
            }
        }

        private void GoBackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }

    public class SponsorInfo
    {
        public BitmapImage LogoImage { get; set; }
        public string SponsorName { get; set; }
        public decimal Amount { get; set; }
    }
}
