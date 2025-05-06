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

namespace EPractice.Pages.AdminPages
{
    /// <summary>
    /// Логика взаимодействия для EquipmentPage.xaml
    /// </summary>
    public partial class EquipmentPage : Page
    {
        MarathonEntities _context = new MarathonEntities();
        public EquipmentPage()
        {
            InitializeComponent();
            LoadInventoryData();
        }

        private void LoadInventoryData()
        {
            try
            {
                using (var context = _context)
                {
                    int totalRunners = context.Registration.Count();
                    TotalRunnersText.Text = $"Всего зарегистрировано бегунов на марафон: {totalRunners}";

                    var kitSelection = new List<KitSelectionViewModel>
                    {
                        new KitSelectionViewModel
                        {
                            KitName = "Выбрало данный вариант",
                            TypeA = context.Registration.Count(r => r.RaceKitOptionId == "A"),
                            TypeB = context.Registration.Count(r => r.RaceKitOptionId == "B"),
                            TypeC = context.Registration.Count(r => r.RaceKitOptionId == "C"),
                            Total = totalRunners,
                            Remaining = 0 
                        }
                    };
                    KitSelectionGrid.ItemsSource = kitSelection;

                    var inventoryItems = new List<InventoryItemViewModel>();

                    var items = context.InventoryItem.ToList();

                    foreach (var item in items)
                    {
                        var viewModel = new InventoryItemViewModel
                        {
                            ItemName = item.ItemName,
                            Remaining = item.CurrentStock.ToString()
                        };

                        var kitItems = context.KitItem
                            .Where(ki => ki.InventoryItemId == item.InventoryItemId)
                            .ToList();

                        int totalNeeded = 0;

                        var typeA = kitItems.FirstOrDefault(ki => ki.RaceKitOptionId == "A");
                        if (typeA != null)
                        {
                            viewModel.TypeA = "✔";
                            totalNeeded += typeA.Quantity * kitSelection[0].TypeA;
                        }
                        else
                        {
                            viewModel.TypeA = "-";
                        }

                        var typeB = kitItems.FirstOrDefault(ki => ki.RaceKitOptionId == "B");
                        if (typeB != null)
                        {
                            viewModel.TypeB = "✔";
                            totalNeeded += typeB.Quantity * kitSelection[0].TypeB;
                        }
                        else
                        {
                            viewModel.TypeB = "-";
                        }

                        var typeC = kitItems.FirstOrDefault(ki => ki.RaceKitOptionId == "C");
                        if (typeC != null)
                        {
                            viewModel.TypeC = "✔";
                            totalNeeded += typeC.Quantity * kitSelection[0].TypeC;
                        }
                        else
                        {
                            viewModel.TypeC = "-";
                        }

                        viewModel.Total = totalNeeded.ToString();
                        inventoryItems.Add(viewModel);
                    }

                    InventoryItemsGrid.ItemsSource = inventoryItems;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке данных инвентаря: {ex.Message}");
            }
        }

        private void ReportButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var reportWindow = new EquipmentReportWindow();
                reportWindow.Owner = Window.GetWindow(this);
                reportWindow.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при генерации отчета: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ArrivalButton_Click(object sender, RoutedEventArgs e)
        {
            AdminWindow adminWindow = Window.GetWindow(this) as AdminWindow;
            adminWindow.OpenEquipmentArrival();
        }
    }


    public class KitSelectionViewModel
    {
        public string KitName { get; set; }
        public int TypeA { get; set; }
        public int TypeB { get; set; }
        public int TypeC { get; set; }
        public int Total { get; set; }
        public int Remaining { get; set; }
    }

    public class InventoryItemViewModel
    {
        public string ItemName { get; set; }
        public string TypeA { get; set; }
        public string TypeB { get; set; }
        public string TypeC { get; set; }
        public string Total { get; set; }
        public string Remaining { get; set; }
    }

    public class InventoryReportViewModel
    {
        public string ItemName { get; set; }
        public int Required { get; set; }
        public int CurrentStock { get; set; }
        public int MinimumStock { get; set; }
        public int ToOrder { get; set; }
    }
}

