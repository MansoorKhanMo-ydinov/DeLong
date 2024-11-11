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
            string searchText = txtSearch.Text.ToLower();
            var searchResults = await _context.Products
                .Where(p => p.Belgi.ToLower().Contains(searchText) ||
                            p.NarxiSumda.ToString().Contains(searchText) ||
                            p.NarxiDollorda.ToString().Contains(searchText))
                .ToListAsync();
            userDataGrid.ItemsSource = searchResults;
        }

        // Mahsulot qo'shish tugmasi bosilganda ishlaydi
        private async void AddUserButton_Click(object sender, RoutedEventArgs e)
        {
            var newProduct = new Product // yangi mahsulot yaratish
            {
                Belgi = "QIzil", // bu yerda default qiymat yozib ketishingiz mumkin
                NarxiSumda = 123333337,
                NarxiDollorda = 27376,
                JamiNarxiSumda = 893897938,
                JamiNarxiDollarda = 12345678,
            };

            _context.Products.Add(newProduct);
            await _context.SaveChangesAsync();
            await LoadDataAsync(); // jadvalni yangilash uchun
        }

        // Tahrirlash tugmasi bosilganda ishlaydi
        private async void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (userDataGrid.SelectedItem is Product selectedProduct)
            {
                selectedProduct.Belgi = "Yangilangan FIO"; // Bu yerda tahrirlash logikasini yozing
                _context.Products.Update(selectedProduct);
                await _context.SaveChangesAsync();
                await LoadDataAsync(); // jadvalni yangilash uchun
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
