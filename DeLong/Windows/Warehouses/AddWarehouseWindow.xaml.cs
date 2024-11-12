using System.Windows;
using DeLong.DbContexts;
using DeLong.Entities.Warehouses;
using Microsoft.EntityFrameworkCore;

namespace DeLong.Windows.Warehouses
{
    public partial class AddWarehouseWindow : Window
    {
        private readonly AppdbContext _dbContext; // AppDbContext uchun private xususiyat
        public Warehouse NewWarehouse { get; private set; } // Yangi ombor

        public AddWarehouseWindow(AppdbContext dbContext)
        {
            InitializeComponent();
            _dbContext = dbContext; // DbContext ni konstruktor orqali oling
        }

        // "Add Warehouse" tugmasi bosilganda
        private async void AddWarehouseButton_Click(object sender, RoutedEventArgs e)
        {
            // Ombor ma'lumotlarini olish
            string id = txtWarehouseID.Text.Trim();
            string name = txtName.Text.Trim();
            string address = txtAddress.Text.Trim();
            string createdAtText = txtCreatedAt.Text.Trim();
            string updatedAtText = txtUpdatedAt.Text.Trim();

            // Majburiy maydonlarni tekshirish
            if (string.IsNullOrWhiteSpace(id) || 
                string.IsNullOrWhiteSpace(name) ||
                string.IsNullOrWhiteSpace(address) ||
                string.IsNullOrWhiteSpace(createdAtText) ||
                string.IsNullOrWhiteSpace(updatedAtText))
            {
                MessageBox.Show("Iltimos, barcha maydonlarni to'ldiring.", "Xato", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // CreatedAt va UpdatedAt ni sana formatiga o'tkazish
            if (!DateTime.TryParse(createdAtText, out DateTime createdAt))
            {
                MessageBox.Show("Created At maydoniga to'g'ri sana formatini kiriting.", "Xato", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (!DateTime.TryParse(updatedAtText, out DateTime updatedAt))
            {
                MessageBox.Show("Updated At maydoniga to'g'ri sana formatini kiriting.", "Xato", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Yangi omborni yaratish
            NewWarehouse = new Warehouse
            {
                Id = int.Parse(id),
                Name = name,
                Adres = address,
                CreatedAt = createdAt,
                UpdatedAt = updatedAt
            };

            try
            {
                // Yangi omborni ma'lumotlar bazasiga qo'shish
                _dbContext.Warehouses.Add(NewWarehouse);
                await _dbContext.SaveChangesAsync(); // O'zgarishlarni asinxron saqlash

                // Omborni muvaffaqiyatli qo'shilgani haqida xabar ko'rsatish
                MessageBox.Show("Ombor muvaffaqiyatli qo'shildi.", "Muvaffaqiyat", MessageBoxButton.OK, MessageBoxImage.Information);

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
