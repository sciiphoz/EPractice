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
using System.Windows.Shapes;

namespace EPractice.Windows
{
    /// <summary>
    /// Логика взаимодействия для EquipmentReportWindow.xaml
    /// </summary>
    public partial class EquipmentReportWindow : Window
    {
        private MarathonEntities _context;
        public EquipmentReportWindow()
        {
            InitializeComponent();
            _context = new MarathonEntities();
            LoadReportData();
        }
        private void LoadReportData()
        {
            try
            {
                var reportData = new System.Collections.Generic.List<InventoryReportItem>();

                // Get all inventory items
                var items = _context.InventoryItem.ToList();

                foreach (var item in items)
                {
                    // Calculate total needed
                    var kitItems = _context.KitItem
                        .Where(ki => ki.InventoryItemId == item.InventoryItemId)
                        .ToList();

                    int totalNeeded = 0;
                    var registrations = _context.Registration.ToList();

                    foreach (var ki in kitItems)
                    {
                        totalNeeded += ki.Quantity * registrations.Count(r => r.RaceKitOptionId == ki.RaceKitOptionId);
                    }

                    int toOrder = Math.Max(0, totalNeeded - item.CurrentStock);

                    reportData.Add(new InventoryReportItem
                    {
                        ItemName = item.ItemName,
                        CurrentStock = item.CurrentStock,
                        Required = totalNeeded,
                        ToOrder = toOrder,
                        ToOrderBackground = toOrder > 0 ? Brushes.LightPink : Brushes.Transparent
                    });
                }

                ReportDataGrid.ItemsSource = reportData;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке данных отчета: {ex.Message}", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void PrintButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                PrintDialog printDialog = new PrintDialog();
                if (printDialog.ShowDialog() == true)
                {
                    FlowDocument document = new FlowDocument();
                    document.PageWidth = printDialog.PrintableAreaWidth;
                    document.PagePadding = new Thickness(50);

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

                    table.Columns.Add(new TableColumn { Width = new GridLength(200) });
                    table.Columns.Add(new TableColumn { Width = new GridLength(100) });
                    table.Columns.Add(new TableColumn { Width = new GridLength(100) });
                    table.Columns.Add(new TableColumn { Width = new GridLength(100) });

                    TableRow headerRow = new TableRow { Background = Brushes.LightGray };
                    table.RowGroups.Add(new TableRowGroup());
                    table.RowGroups[0].Rows.Add(headerRow);

                    headerRow.Cells.Add(new TableCell(new Paragraph(new Run("Наименование")) { FontWeight = FontWeights.Bold }));
                    headerRow.Cells.Add(new TableCell(new Paragraph(new Run("Остаток")) { FontWeight = FontWeights.Bold }));
                    headerRow.Cells.Add(new TableCell(new Paragraph(new Run("Необходимо")) { FontWeight = FontWeights.Bold }));
                    headerRow.Cells.Add(new TableCell(new Paragraph(new Run("Надо заказать")) { FontWeight = FontWeights.Bold }));

                    foreach (InventoryReportItem item in ReportDataGrid.Items)
                    {
                        TableRow row = new TableRow();
                        table.RowGroups[0].Rows.Add(row);

                        row.Cells.Add(new TableCell(new Paragraph(new Run(item.ItemName))));
                        row.Cells.Add(new TableCell(new Paragraph(new Run(item.CurrentStock.ToString()))));
                        row.Cells.Add(new TableCell(new Paragraph(new Run(item.Required.ToString()))));

                        var orderCell = new TableCell(new Paragraph(new Run(item.ToOrder.ToString())));
                        if (item.ToOrder > 0)
                        {
                            orderCell.Background = Brushes.LightPink;
                        }
                        row.Cells.Add(orderCell);
                    }

                    printDialog.PrintDocument(((IDocumentPaginatorSource)document).DocumentPaginator, "Отчет по инвентарю");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при печати: {ex.Message}", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }

    public class InventoryReportItem
    {
        public string ItemName { get; set; }
        public int CurrentStock { get; set; }
        public int Required { get; set; }
        public int ToOrder { get; set; }
        public Brush ToOrderBackground { get; set; }
    }
}

