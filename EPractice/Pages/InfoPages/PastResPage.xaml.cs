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

namespace EPractice.Pages.InfoPages
{
    /// <summary>
    /// Логика взаимодействия для PastResPage.xaml
    /// </summary>
    public partial class PastResPage : Page
    {
        public PastResPage()
        {
            InitializeComponent();
            LoadFilters();
            LoadResults();
        }
        private void ResetFiltersButton_Click(object sender, RoutedEventArgs e)
        {
            MarathonComboBox.SelectedIndex = -1;
            EventTypeComboBox.SelectedIndex = -1;
            GenderComboBox.SelectedIndex = 0; 
            AgeCategoryComboBox.SelectedIndex = 0;

            LoadResults();
        }
        private void LoadFilters()
        {
            try
            {
                MarathonComboBox.ItemsSource = Connection.marathonEntities.Marathon.ToList();
                MarathonComboBox.SelectedIndex = -1;

                EventTypeComboBox.ItemsSource = Connection.marathonEntities.EventType.ToList();
                EventTypeComboBox.SelectedIndex = -1;

                GenderComboBox.SelectedIndex = 0;
                AgeCategoryComboBox.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке фильтров: {ex.Message}");
            }
        }

        private void LoadResults()
        {
            try
            {
                int? marathonId = MarathonComboBox.SelectedValue as int?;
                string eventTypeId = EventTypeComboBox.SelectedValue as string;
                string gender = (GenderComboBox.SelectedItem as ComboBoxItem)?.Tag.ToString();
                string ageCategory = (AgeCategoryComboBox.SelectedItem as ComboBoxItem)?.Tag.ToString();

                var results = GetFilteredResults(marathonId, eventTypeId, gender, ageCategory);

                UpdateStatistics(results);

                ResultsDataGrid.ItemsSource = results;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке результатов: {ex.Message}");
            }
        }

        private List<RaceResult> GetFilteredResults(int? marathonId, string eventTypeId, string gender, string ageCategory)
        {
            var query = Connection.marathonEntities.RegistrationEvent
                .Where(re => re.RaceTime.HasValue)
                .AsQueryable();

            if (marathonId.HasValue)
            {
                query = query.Where(re => re.Event.MarathonId == marathonId.Value);
            }

            if (!string.IsNullOrEmpty(eventTypeId))
            {
                query = query.Where(re => re.Event.EventTypeId == eventTypeId);
            }

            if (gender != "Any")
            {
                query = query.Where(re => re.Registration.Runner.Gender == gender);
            }

            var filteredResults = query.ToList();

            if (ageCategory != "Any")
            {
                filteredResults = filteredResults.Where(re =>
                {
                    if (!re.Registration.Runner.DateOfBirth.HasValue) return false;

                    var birthDate = re.Registration.Runner.DateOfBirth.Value;
                    var today = DateTime.Today;
                    var age = today.Year - birthDate.Year;

                    if (birthDate.Date > today.AddYears(-age))
                        age--;

                    switch (ageCategory)
                    {
                        case "Under18": return age < 18;
                        case "18-29": return age >= 18 && age <= 29;
                        case "30-39": return age >= 30 && age <= 39;
                        case "40-55": return age >= 40 && age <= 55;
                        case "56-70": return age >= 56 && age <= 70;
                        case "Over70": return age > 70;
                        default: return false;
                    }
                }).ToList();
            }

            var sortedResults = filteredResults
                .OrderBy(re => re.RaceTime)
                .ToList();

            var resultsWithPlaces = new List<RaceResult>();
            int place = 1;
            int? previousTime = null;
            int sameTimeCount = 0;

            foreach (var result in sortedResults)
            {
                if (previousTime.HasValue && result.RaceTime != previousTime)
                {
                    place += sameTimeCount;
                    sameTimeCount = 0;
                }

                resultsWithPlaces.Add(new RaceResult
                {
                    Place = place,
                    TimeInSeconds = result.RaceTime.Value,
                    RunnerName = $"{result.Registration.Runner.User.FirstName} {result.Registration.Runner.User.LastName}",
                    Country = result.Registration.Runner.Country.CountryCode
                });

                if (result.RaceTime == previousTime)
                {
                    sameTimeCount++;
                }
                else
                {
                    previousTime = result.RaceTime;
                }
            }

            return resultsWithPlaces;
        }

        private void UpdateStatistics(List<RaceResult> results)
        {
            TotalRunnersText.Text = $"Всего бегунов: {results.Count}";

            int finishedCount = results.Count;
            FinishedRunnersText.Text = $"Всего финишировало: {finishedCount}";

            if (finishedCount > 0)
            {
                double averageSeconds = results.Average(r => r.TimeInSeconds);
                TimeSpan averageTime = TimeSpan.FromSeconds(averageSeconds);
                AverageTimeText.Text = $"Среднее время: {averageTime.Hours}h {averageTime.Minutes}m {averageTime.Seconds}s";
            }
            else
            {
                AverageTimeText.Text = "Среднее время: N/A";
            }
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            LoadResults();
        }

        private void GoBackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }

    public class RaceResult
    {
        public int Place { get; set; }
        public int TimeInSeconds { get; set; }
        public string RunnerName { get; set; }
        public string Country { get; set; }

        public string FormattedTime =>
            $"{TimeSpan.FromSeconds(TimeInSeconds).Hours}h " +
            $"{TimeSpan.FromSeconds(TimeInSeconds).Minutes}m " +
            $"{TimeSpan.FromSeconds(TimeInSeconds).Seconds}s";
    }
}

