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
    /// Логика взаимодействия для EquipmentArrivalPage.xaml
    /// </summary>
    public partial class EquipmentArrivalPage : Page
    {
        MarathonEntities _context = new MarathonEntities();
        public EquipmentArrivalPage()
        {
            InitializeComponent();
            _context = new MarathonEntities();
            LoadInventoryData();
        }

        private void LoadInventoryData()
        {
            try
            {
                var inventoryItems = _context.InventoryItem.ToList()
                    .Select(i => new InventoryArrivalViewModel
                    {
                        InventoryItemId = i.InventoryItemId,
                        ItemName = i.ItemName,
                        CurrentStock = i.CurrentStock,
                        ArrivalQuantity = 0
                    }).ToList();

                InventoryItemsGrid.ItemsSource = inventoryItems;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке данных инвентаря: {ex.Message}");
            }
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, 0) && e.Text != "-")
            {
                e.Handled = true;
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                foreach (InventoryArrivalViewModel item in InventoryItemsGrid.Items)
                {
                    if (item.ArrivalQuantity == 0)
                        continue;

                    var inventoryItem = _context.InventoryItem.Find(item.InventoryItemId);
                    if (inventoryItem != null)
                    {
                        inventoryItem.CurrentStock += item.ArrivalQuantity;

                        if (inventoryItem.CurrentStock < 0)
                        {
                            var result = MessageBox.Show(
                                $"Отрицательный остаток для {item.ItemName}. Продолжить?",
                                "Подтверждение",
                                MessageBoxButton.YesNo,
                                MessageBoxImage.Warning);

                            if (result != MessageBoxResult.Yes)
                            {
                                return;
                            }
                        }
                    }
                }

                _context.SaveChanges();
                MessageBox.Show("Изменения сохранены успешно", "Успех",
                    MessageBoxButton.OK, MessageBoxImage.Information);

                LoadInventoryData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении изменений: {ex.Message}", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }

    public class InventoryArrivalViewModel
    {
        public int InventoryItemId { get; set; }
        public string ItemName { get; set; }
        public int CurrentStock { get; set; }
        public int ArrivalQuantity { get; set; }
    }
}

