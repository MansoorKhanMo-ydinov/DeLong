using System.Windows;
using System.Windows.Controls;
using DeLong.DbContexts;
using DeLong.Entities.Products;
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
            
        }

        // Mahsulot qo'shish tugmasi bosilganda ishlaydi
        private async void AddUserButton_Click(object sender, RoutedEventArgs e)
        {
            
        }

        // Tahrirlash tugmasi bosilganda ishlaydi
        private async void EditButton_Click(object sender, RoutedEventArgs e)
        {
            
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
