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

namespace EPractice.Pages.AdminPages
{
    /// <summary>
    /// Логика взаимодействия для EquipmentPage.xaml
    /// </summary>
    public partial class EquipmentPage : Page
    {
        public EquipmentPage()
        {
            InitializeComponent();
            LoadInventoryData();
        }

        private void LoadInventoryData()
        {
            try
            {
                using (var context = Connection.marathonEntities)
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
                            Remaining = 0 // Not applicable for this row
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
                MessageBox.Show($"Ошибка при генерации отчета: {ex.Message}", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private FlowDocument CreateInventoryReportDocument(MarathonEntities context)
        {
            FlowDocument document = new FlowDocument();

            Paragraph title = new Paragraph(new Run("ОТЧЕТ ПО ИНВЕНТАРЮ MARATHON SKILLS 2025"))
            {
                FontSize = 18,
                FontWeight = FontWeights.Bold,
                TextAlignment = TextAlignment.Center,
                Margin = new Thickness(0, 0, 0, 20)
            };
            document.Blocks.Add(title);

            Paragraph date = new Paragraph(new Run($"Дата формирования: {DateTime.Now:dd.MM.yyyy HH:mm}"))
            {
                FontSize = 12,
                TextAlignment = TextAlignment.Left,
                Margin = new Thickness(0, 0, 0, 10)
            };
            document.Blocks.Add(date);

            Table table = new Table();
            document.Blocks.Add(table);

            table.CellSpacing = 0;
            table.BorderBrush = Brushes.Black;
            table.BorderThickness = new Thickness(1, 1, 0, 0);

            for (int i = 0; i < 5; i++)
            {
                table.Columns.Add(new TableColumn());
            }

            TableRowGroup headerGroup = new TableRowGroup();
            table.RowGroups.Add(headerGroup);
            TableRow headerRow = new TableRow { Background = Brushes.LightGray };
            headerGroup.Rows.Add(headerRow);

            headerRow.Cells.Add(CreateHeaderCell("Наименование"));
            headerRow.Cells.Add(CreateHeaderCell("Требуется"));
            headerRow.Cells.Add(CreateHeaderCell("На складе"));
            headerRow.Cells.Add(CreateHeaderCell("Минимум"));
            headerRow.Cells.Add(CreateHeaderCell("К заказу"));

            var items = context.InventoryItem.ToList();
            TableRowGroup dataGroup = new TableRowGroup();
            table.RowGroups.Add(dataGroup);

            foreach (var item in items)
            {
                var kitItems = context.KitItem
                    .Where(ki => ki.InventoryItemId == item.InventoryItemId)
                    .ToList();

                int totalNeeded = 0;
                var registrations = context.Registration.ToList();

                foreach (var ki in kitItems)
                {
                    totalNeeded += ki.Quantity * registrations.Count(r => r.RaceKitOptionId == ki.RaceKitOptionId);
                }

                int toOrder = Math.Max(0, totalNeeded - item.CurrentStock);

                TableRow row = new TableRow();
                dataGroup.Rows.Add(row);

                row.Cells.Add(CreateDataCell(item.ItemName));
                row.Cells.Add(CreateDataCell(totalNeeded.ToString()));
                row.Cells.Add(CreateDataCell(item.CurrentStock.ToString()));
                row.Cells.Add(CreateDataCell(item.MinimumStock.ToString()));

                TableCell orderCell = CreateDataCell(toOrder.ToString());
                if (toOrder > 0)
                {
                    orderCell.Background = Brushes.LightPink;
                    orderCell.FontWeight = FontWeights.Bold;
                }
                row.Cells.Add(orderCell);
            }

            return document;
        }

        private TableCell CreateHeaderCell(string text)
        {
            return new TableCell(new Paragraph(new Run(text))
            {
                BorderBrush = Brushes.Black,
                BorderThickness = new Thickness(0, 0, 1, 1),
                Padding = new Thickness(6),
                TextAlignment = TextAlignment.Center,
                FontWeight = FontWeights.Bold
            });
        }

        private TableCell CreateDataCell(string text)
        {
            return new TableCell(new Paragraph(new Run(text))
            {
                BorderBrush = Brushes.Black,
                BorderThickness = new Thickness(0, 0, 1, 1),
                Padding = new Thickness(6),
                TextAlignment = TextAlignment.Left
            });
        }

        private void ArrivalButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new EquipmentArrivalPage());
            MessageBox.Show("Функция прихода инвентаря будет реализована позже", "Приход инвентаря", MessageBoxButton.OK, MessageBoxImage.Information);
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

