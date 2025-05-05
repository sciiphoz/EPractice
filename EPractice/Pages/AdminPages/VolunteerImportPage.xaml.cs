using EPractice.DBConnection;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace EPractice.Pages.AdminPages
{
    /// <summary>
    /// Логика взаимодействия для VolunteerImportPage.xaml
    /// </summary>
    public partial class VolunteerImportPage : Page
    {
        public VolunteerImportPage()
        {
            InitializeComponent();
        }

        private void GoBackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void BrowseButton_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = "CSV files (*.csv)|*.csv",
                Title = "Выберите файл с волонтерами"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                FilePathTextBox.Text = openFileDialog.FileName;
                LogBorder.Visibility = Visibility.Collapsed;
                StatsPanel.Visibility = Visibility.Collapsed;
            }
        }

        private void ImportButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(FilePathTextBox.Text))
            {
                MessageBox.Show("Пожалуйста, выберите CSV файл", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!File.Exists(FilePathTextBox.Text))
            {
                MessageBox.Show("Выбранный файл не существует", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                LogBorder.Visibility = Visibility.Visible;
                StatsPanel.Visibility = Visibility.Visible;
                LogTextBlock.Text = "Начало загрузки волонтеров...\n";

                var lines = File.ReadAllLines(FilePathTextBox.Text);
                if (lines.Length == 0)
                {
                    LogTextBlock.Text += "Файл пуст\n";
                    return;
                }

                // Пропускаем заголовок
                var dataLines = lines.Skip(1).ToArray();
                int totalImported = 0;
                int totalUpdated = 0;
                int totalErrors = 0;

                foreach (var line in dataLines)
                {
                    try
                    {
                        var parts = line.Split(',');
                        if (parts.Length < 5)
                        {
                            LogTextBlock.Text += $"Ошибка: неверный формат строки - {line}\n";
                            totalErrors++;
                            continue;
                        }

                        var volunteerIdStr = parts[0].Trim();
                        var firstName = parts[1].Trim();
                        var lastName = parts[2].Trim();
                        var countryCode = parts[3].Trim();
                        var gender = parts[4].Trim();

                        if (!int.TryParse(volunteerIdStr, out int volunteerId))
                        {
                            LogTextBlock.Text += $"Ошибка: неверный ID волонтера - {volunteerIdStr}\n";
                            totalErrors++;
                            continue;
                        }

                        if (string.IsNullOrEmpty(firstName) ||
                            string.IsNullOrEmpty(lastName) ||
                            countryCode.Length != 3 ||
                            (gender != "Male" && gender != "Female" && gender != "Мужской" && gender != "Женский"))
                        {
                            LogTextBlock.Text += $"Ошибка: неверные данные в строке - {line}\n";
                            totalErrors++;
                            continue;
                        }

                        var existingVolunteer = Connection.marathonEntities.Volunteer
                            .FirstOrDefault(v => v.VolunteerId == volunteerId);

                        if (existingVolunteer != null)
                        {
                            existingVolunteer.FirstName = firstName;
                            existingVolunteer.LastName = lastName;
                            existingVolunteer.CountryCode = countryCode;
                            existingVolunteer.Gender = gender;
                            totalUpdated++;
                            LogTextBlock.Text += $"Обновлен: {firstName} {lastName} (ID: {volunteerId})\n";
                        }
                        else
                        {
                            var newVolunteer = new Volunteer
                            {
                                VolunteerId = volunteerId,
                                FirstName = firstName,
                                LastName = lastName,
                                CountryCode = countryCode,
                                Gender = gender
                            };
                            Connection.marathonEntities.Volunteer.Add(newVolunteer);
                            totalImported++;
                            LogTextBlock.Text += $"Добавлен: {firstName} {lastName} (ID: {volunteerId})\n";
                        }
                    }
                    catch (Exception ex)
                    {
                        LogTextBlock.Text += $"Ошибка обработки строки: {ex.Message}\n";
                        totalErrors++;
                    }
                }

                Connection.marathonEntities.SaveChanges();

                StatsTextBlock.Text = $"Итого: Добавлено {totalImported}, Обновлено {totalUpdated}, Ошибок {totalErrors}";
                LogTextBlock.Text += "Загрузка завершена!\n";

                MessageBox.Show("Импорт волонтеров завершен успешно!", "Успех",
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                LogTextBlock.Text += $"Критическая ошибка: {ex.Message}\n";
                MessageBox.Show($"Ошибка при импорте: {ex.Message}", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
