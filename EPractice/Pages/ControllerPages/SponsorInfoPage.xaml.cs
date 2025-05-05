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

namespace EPractice.Pages.ControllerPages
{
    /// <summary>
    /// Логика взаимодействия для SponsorInfoPage.xaml
    /// </summary>
    public partial class SponsorInfoPage : Page
    {
        public SponsorInfoPage()
        {
            InitializeComponent();
            LoadSponsorsData();
        }

        private void LoadSponsorsData()
        {
            try
            {
                string sortField = (SortComboBox.SelectedItem as ComboBoxItem)?.Tag?.ToString() ?? "TotalAmount";

                var sponsorsQuery = Connection.marathonEntities.Sponsorship
                    .Where(s => s.Registration != null && s.Registration.Charity != null)
                    .GroupBy(s => s.Registration.Charity)
                    .Select(g => new
                    {
                        Charity = g.Key,
                        TotalAmount = g.Sum(s => s.Amount),
                        SponsorsCount = g.Count()
                    });

                var sponsorsData = sponsorsQuery.ToList()
                    .Select(x => new CharitySponsorsInfo
                    {
                        CharityId = x.Charity.CharityId,
                        CharityName = x.Charity.CharityName,
                        LogoPath = GetLogoPath(x.Charity.CharityLogo), 
                        TotalAmount = x.TotalAmount,
                        SponsorsCount = x.SponsorsCount
                    })
                    .ToList();

                if (sponsorsData != null && CharitiesCountText != null && TotalAmountText != null)
                {
                    CharitiesCountText.Text = sponsorsData.Count.ToString();
                    TotalAmountText.Text = sponsorsData.Sum(c => c.TotalAmount).ToString("C");

                    switch (sortField)
                    {
                        case "CharityName":
                            sponsorsData = sponsorsData.OrderBy(c => c.CharityName).ToList();
                            break;
                        case "SponsorsCount":
                            sponsorsData = sponsorsData.OrderByDescending(c => c.SponsorsCount).ToList();
                            break;
                        default:
                            sponsorsData = sponsorsData.OrderByDescending(c => c.TotalAmount).ToList();
                            break;
                    }

                    SponsorsGrid.ItemsSource = sponsorsData;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке данных спонсоров: {ex.Message}");
            }
        }

        private string GetLogoPath(string logoFileName)
        {
            if (string.IsNullOrEmpty(logoFileName))
                return "/Resources/default-charity.png";

            return $"/Resources/Charities/{logoFileName}";
        }
        private void SortComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LoadSponsorsData();
        }

        private void GoBackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new MainWindow());
        }
    }

    public class CharitySponsorsInfo
    {
        public int CharityId { get; set; }
        public string CharityName { get; set; }
        public string LogoPath { get; set; }
        public decimal TotalAmount { get; set; }
        public int SponsorsCount { get; set; }
    }
}

