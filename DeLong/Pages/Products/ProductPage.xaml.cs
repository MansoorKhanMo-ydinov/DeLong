using System.Windows;
using System.Windows.Controls;
using DeLong.DbContexts;
using DeLong.Entities.Products;
using DeLong.Windows.Products;
using Microsoft.EntityFrameworkCore;

namespace DeLong.Pages.Products
{
    public partial class ProductPage : Page
    {
        private readonly AppdbContext _context;

        public ProductPage()
        {
            InitializeComponent();
            _context = new AppdbContext();
            LoadDataAsync();
        }

        // Ma'lumotlarni yuklash funksiyasi
        private async Task LoadDataAsync()
        {
            var products = await _context.Products.ToListAsync();
            userDataGrid.ItemsSource = products;
        }

        // Qidirish tugmasi bosilganda ishlaydi
        private async void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string searchText = txtSearch.Text.ToLower();

            var filteredUsers = await _context.Products
                .Where(u => u.Belgi.ToLower().Contains(searchText))
                .ToListAsync();

            userDataGrid.ItemsSource = filteredUsers; // Filtrlangan foydalanuvchilarni ko'rsatish
        }

        // Mahsulot qo'shish tugmasi bosilganda ishlaydi
        private async void AddUserButton_Click(object sender, RoutedEventArgs e)
        {
            var userForm = new AddProductWindow(_context); // _context ni o'tkazing
            if (userForm.ShowDialog() == true)
            {
                LoadDataAsync(); // Foydalanuvchilarni yangidan yuklash
            }
        }

        // Tahrirlash tugmasi bosilganda ishlaydi
        private async void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is Product product)
            {
                var editWindow = new ProductEditWindow(_context, product); // UserEditWindow ni ochish

                if (editWindow.ShowDialog() == true)
                {
                    try
                    {
                        _context.Products.Update(editWindow.UpdatedProduct); // Yangilangan foydalanuvchini ma'lumotlar bazasida yangilash
                        await _context.SaveChangesAsync(); // O'zgarishlarni saqlash
                        LoadDataAsync(); // Foydalanuvchilar ro'yxatini yangilash
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Foydalanuvchini tahrirlashda xato: {ex.Message}");
                    }
                }
            }
        }

        // O'chirish tugmasi bosilganda ishlaydi
        private async void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (userDataGrid.SelectedItem is Product selectedProduct)
            {
                _context.Products.Remove(selectedProduct);
                await _context.SaveChangesAsync();
                await LoadDataAsync(); // jadvalni yangilash uchun
            }
        }
    }
}
