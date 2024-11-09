using System.Windows;
using DeLong.DbContexts;
using DeLong.Entities.Products; // Bu yerda Product klassini qo'llash
using Microsoft.EntityFrameworkCore;

namespace DeLong.Windows.Products
{
    public partial class AddProductWindow : Window
    {
        private readonly AppdbContext _dbContext; // AppDbContext uchun private xususiyat
        public Product NewProduct { get; private set; } // Yangi mahsulot

        public AddProductWindow(AppdbContext dbContext)
        {
            InitializeComponent();
            _dbContext = dbContext; // DbContext ni konstruktor orqali oling
        }

        // "Add Product" tugmasi bosilganda
        private async void AddProductButton_Click(object sender, RoutedEventArgs e)
        {
            string belgi = BelgiTextBox.Text.Trim();
            string soni = SoniTextBox.Text.Trim();
            string narxisumda = NarxiSumdaTextBox.Text.Trim();
            string narxidollorda = NarxiDollordaTextBox.Text.Trim();
            string jaminarxisumda = JamiNarxiSumdaTextBox.Text.Trim();
            string jaminarxidollorda = JamiNarxiDollardaTextBox.Text.Trim();


            // Majburiy maydonlarni tekshirish
            if (string.IsNullOrWhiteSpace(belgi) ||
                string.IsNullOrWhiteSpace(soni) ||
                string.IsNullOrWhiteSpace(narxisumda) ||
                string.IsNullOrWhiteSpace(narxidollorda) ||
                string.IsNullOrWhiteSpace(jaminarxisumda) || string.IsNullOrWhiteSpace(jaminarxidollorda))
            {
                MessageBox.Show("Iltimos, barcha maydonlarni to'ldiring.", "Xato", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Narx va miqdorni raqamga aylantirish
            if (!decimal.TryParse(narxisumda, out decimal price))
            {
                MessageBox.Show("Narx faqat raqam bo'lishi kerak.", "Xato", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (!int.TryParse(narxidollorda, out int quantity))
            {
                MessageBox.Show("Miqdor faqat raqam bo'lishi kerak.", "Xato", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                // Yangi mahsulotni ma'lumotlar bazasiga qo'shish
                _dbContext.Products.Add(NewProduct);
                await _dbContext.SaveChangesAsync(); // O'zgarishlarni asinxron saqlash

                // Mahsulotni muvaffaqiyatli qo'shilgani haqida xabar ko'rsatish
                MessageBox.Show("Mahsulot muvaffaqiyatli qo'shildi.", "Muvaffaqiyat", MessageBoxButton.OK, MessageBoxImage.Information);

                // Oynani yopish
                this.DialogResult = true;
                this.Close();
            }
            catch (DbUpdateException dbEx)
            {
                // Ma'lumotlar bazasi bilan bog'liq xatoliklar uchun maxsus xabar
                MessageBox.Show($"Ma'lumotlar bazasi xatoligi: {dbEx.Message}", "Xato", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                // Xato xabarini ko'rsatish
                MessageBox.Show($"Xatolik yuz berdi: {ex.Message}", "Xato", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
