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

namespace EPractice.Pages.InfoPages
{
    /// <summary>
    /// Логика взаимодействия для BMIPage.xaml
    /// </summary>
    public partial class BMIPage : Page
    {
        public BMIPage()
        {
            InitializeComponent();
        }

        private string selectedGender = "Male";

        private void GenderSelected(object sender, MouseButtonEventArgs e)
        {
            if (sender is Border border && border.Child is Image image)
            {
                SelectGender(image.Tag.ToString());
            }
        }

        private void SelectGender(string gender)
        {
            selectedGender = gender;

            MaleBorder.BorderBrush = Brushes.Transparent;
            FemaleBorder.BorderBrush = Brushes.Transparent;

            if (gender == "Male")
            {
                MaleBorder.BorderBrush = Brushes.Black;
            }
            else
            {
                FemaleBorder.BorderBrush = Brushes.Black;
            }
        }

        private void CalculateButton_Click(object sender, RoutedEventArgs e)
        {
            if (!double.TryParse(HeightTextBox.Text, out double heightCm) ||
                !double.TryParse(WeightTextBox.Text, out double weightKg))
            {
                MessageBox.Show("Пожалуйста, введите корректные значения роста и веса");
                return;
            }

            if (heightCm <= 0 || weightKg <= 0)
            {
                MessageBox.Show("Рост и вес должны быть положительными числами");
                return;
            }

            double heightM = heightCm / 100;

            double bmi = weightKg / (heightM * heightM);
            bmi = Math.Round(bmi, 1);

            BMITextBlock.Text = bmi.ToString();

            string category;
            string iconName;

            if (bmi < 18.5)
            {
                category = "Недостаточный вес";
                iconName = "bmi-underweight-icon.png";
            }
            else if (bmi < 25)
            {
                category = "Здоровый вес";
                iconName = "bmi-healthy-icon.png";
            }
            else if (bmi < 30)
            {
                category = "Избыточный вес";
                iconName = "bmi-overweight-icon.png";
            }
            else
            {
                category = "Ожирение";
                iconName = "bmi-obese-icon.png";
            }

            BMICategoryText.Text = category;

            try
            {
                BMIResultIcon.Source = new BitmapImage(
                    new Uri($"pack://application:,,,/Images/BMIImages/{iconName}"));
            }
            catch
            {
                BMIResultIcon.Source = null;
            }

            UpdateBMIArrowPosition(bmi);
        }

        private void UpdateBMIArrowPosition(double bmi)
        {
            double scaleWidth = 400;

            double position;

            if (bmi < 18.5)
            {
                position = (bmi / 18.5) * 0.25 * scaleWidth;
            }
            else if (bmi < 25)
            {
                position = (0.25 + (bmi - 18.5) / (25 - 18.5) * 0.375) * scaleWidth;
            }
            else if (bmi < 30)
            {
                position = (0.625 + (bmi - 25) / (30 - 25) * 0.375) * scaleWidth;
            }
            else
            {
                position = (1.0 + (Math.Min(bmi, 40) - 30) / 10 * 0.25) * scaleWidth;
            }

            BMIArrow.Margin = new Thickness(position - 10, -10, 0, 0);
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            HeightTextBox.Text = "170";
            WeightTextBox.Text = "70";
            BMITextBlock.Text = "0.0";
            BMICategoryText.Text = "Категория";
            BMIResultIcon.Source = null;
            BMIArrow.Margin = new Thickness(0, -10, 0, 0);
            SelectGender("Male");
        }

        private void GoBackButton_Click(object sender, RoutedEventArgs e)
        {
            InfoWindow infoWindow = Window.GetWindow(this) as InfoWindow;

            infoWindow.OpenButtonsPage();
        }
    }
}
