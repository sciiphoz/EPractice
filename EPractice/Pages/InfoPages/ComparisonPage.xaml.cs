using EPractice.DBConnection;
using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;

namespace EPractice.Pages.InfoPages
{
    /// <summary>
    /// Логика взаимодействия для ComparisonPage.xaml
    /// </summary>
    public partial class ComparisonPage : Page
    {
        MarathonEntities db = new MarathonEntities();
        public ComparisonPage()
        {
            InitializeComponent();
            gridDistance.ItemsSource = db.HowLong.Where(x => x.Length != null).ToList();
            gridSpeed.ItemsSource = db.HowLong.Where(x => x.Speed != null).ToList();
        }

        private void gridDistance_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var element = gridDistance.SelectedItem as HowLong;
            if (element != null)
            {
                txtName.Text = element.Name;

                if (element.Length != null &&
                    float.TryParse(
                        element.Length.Replace("m", "").Trim(),
                        NumberStyles.Any,
                        CultureInfo.InvariantCulture,
                        out float lengthMeters
                    ))
                {
                    float marathonLength = 42195;
                    float count = marathonLength / lengthMeters;

                    txtInfo.Text = $"Длина {element.Name} {element.Length}. " +
                                  $"Это займет {Math.Round(count, 1)} из них, " +
                                  "чтобы покрыть расстояние в 42 км марафона";
                }

                imgInfo.Source = LoadImageFromResources(element.Name.ToLower().Replace(" ", "-") + ".jpg");
            }
        }

        private void gridSpeed_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var element = gridSpeed.SelectedItem as HowLong;
            if (element != null)
            {
                txtName.Text = element.Name;

                if (element.Speed != null &&
                    float.TryParse(
                        element.Speed.Replace("km/h", "").Trim(),
                        NumberStyles.Any,
                        CultureInfo.InvariantCulture,
                        out float speedKmh
                    ))
                {
                    float marathonLength = 42.195f;
                    float hours = marathonLength / speedKmh;

                    TimeSpan time = TimeSpan.FromHours(hours);
                    string timeString = time.TotalHours >= 1
                        ? $"{Math.Floor(time.TotalHours)} часов {time.Minutes} минут"
                        : $"{time.Minutes} минут";

                    txtInfo.Text = $"Максимальная скорость {element.Name} {element.Speed}. " +
                                  $"Это займет {timeString} чтобы завершить 42km марафон";
                }

                imgInfo.Source = LoadImageFromResources(element.Name.ToLower().Replace(" ", "-") + ".jpg");
            }
        }

        private BitmapImage LoadImageFromResources(string imageName)
        {
            try
            {
                return new BitmapImage(new Uri($"pack://application:,,,/Images/ComparisonImages/{imageName}"));
            }
            catch
            {
                return new BitmapImage(new Uri("pack://application:,,,/Images/ComparisonImages/default.jpg"));
            }
        }

        private void GoBackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.GoBack();
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new MainWindow());
        }
    }
}
