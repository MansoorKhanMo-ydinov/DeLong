using System.Windows;
using System.Windows.Controls;
using DeLong.DbContexts;
using DeLong.Entities.Warehouses;
using DeLong.Windows.Warehouses;
using Microsoft.EntityFrameworkCore;

namespace DeLong.Pages.Warehouses
{
    public partial class WarehousePage : Page
    {
        private readonly AppdbContext _context;

        public WarehousePage()
        {
            InitializeComponent();
            _context = new AppdbContext(); 
            LoadWarehouseData(); 
        }
        private async void LoadWarehouseData()
        {
            try
            {
                var warehouses = await _context.Warehouses.ToListAsync();
                warehouseDataGrid.ItemsSource = warehouses;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ombor ma'lumotlarini yuklashda xatolik: {ex.Message}", "Xato", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void SearchWarehouseButton_Click(object sender, RoutedEventArgs e)
        {
            string searchQuery = txtSearchWarehouse.Text.Trim();
            if (string.IsNullOrWhiteSpace(searchQuery))
            {
                MessageBox.Show("Iltimos, qidirish maydonini to'ldiring.", "Xato", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var filteredWarehouses = _context.Warehouses
                .Where(w => w.Name.Contains(searchQuery) || w.Name.Contains(searchQuery))
                .ToList();

            warehouseDataGrid.ItemsSource = filteredWarehouses;
        }
        private void AddWarehouseButton_Click(object sender, RoutedEventArgs e)
        {
            AddWarehouseWindow addWarehouseWindow = new AddWarehouseWindow(_context);
            if (addWarehouseWindow.ShowDialog() == true)
            {
                LoadWarehouseData(); 
            }
        }
        private async void EditWarehouseButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is Warehouse warehouse)
            {
                var editWindow = new EditWarehouseWindow(_context, warehouse);

                if (editWindow.ShowDialog() == true)
                {
                    try
                    {
                        _context.Warehouses.Update(editWindow.UpdatedWarehouse);
                        await _context.SaveChangesAsync();
                        LoadWarehouseData();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Foydalanuvchini tahrirlashda xato: {ex.Message}");
                    }
                }
            }
        }
        private async void DeleteWarehouseButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is Warehouse selectedWarehouse)
            {
                MessageBoxResult result = MessageBox.Show("Bu ombor elementini o‘chirishga ishonchingiz komilmi?", "O'chirish tasdig'i", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        _context.Warehouses.Remove(selectedWarehouse);
                        await _context.SaveChangesAsync();
                        LoadWarehouseData();
                        MessageBox.Show("Ombor elementi o‘chirildi.", "Muvaffaqiyat", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ombor elementini o‘chirishda xato: {ex.Message}", "Xato", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("O'chirish uchun hech qanday element tanlanmagan.", "Xato", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
