using System.Windows;
using DeLong.DbContexts;
using DeLong.Entities.Users;
using Microsoft.EntityFrameworkCore;

namespace DeLong.Windows.Users
{
    public partial class AddUserWindow : Window
    {
        private readonly AppdbContext _dbContext; // AppDbContext uchun private xususiyat
        public User NewUser { get; private set; } // Yangi foydalanuvchi

        public AddUserWindow(AppdbContext dbContext)
        {
            InitializeComponent();
            _dbContext = dbContext; // DbContext ni konstruktor orqali oling
        }

        // "Add User" tugmasi bosilganda
        private async void AddUserButton_Click(object sender, RoutedEventArgs e)
        {
            // Foydalanuvchi ma'lumotlarini olish
            string fio = txtFIO.Text.Trim();
            string telefon = txtTelefon.Text.Trim();
            string adres = txtAdres.Text.Trim();
            string telegramRaqam = txtTelegramRaqam.Text.Trim();
            string innText = txtINN.Text.Trim();

            // Ma'lumotlarni tekshirish
            if (string.IsNullOrWhiteSpace(fio) ||
                string.IsNullOrWhiteSpace(telefon) ||
                string.IsNullOrWhiteSpace(adres) ||
                string.IsNullOrWhiteSpace(telegramRaqam) ||
                string.IsNullOrWhiteSpace(innText))
            {
                MessageBox.Show("Iltimos, barcha maydonlarni to'ldiring.", "Xato", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // INN qiymatini int ga aylantirish
            if (!int.TryParse(innText, out int inn))
            {
                MessageBox.Show("INN faqat raqam bo'lishi kerak.", "Xato", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Yangi foydalanuvchini yaratish
            NewUser = new User
            {
                FIO = fio,
                Telefon = telefon,
                Adres = adres,
                TelegramRaqam = telegramRaqam,
                INN = inn // INN int bo'lishi kerak
            };

            try
            {
                // Yangi foydalanuvchini ma'lumotlar bazasiga qo'shish
                _dbContext.Users.Add(NewUser);
                await _dbContext.SaveChangesAsync(); // O'zgarishlarni asinxron saqlash

                // Foydalanuvchini muvaffaqiyatli qo'shilgani haqida xabar ko'rsatish
                MessageBox.Show("Foydalanuvchi muvaffaqiyatli qo'shildi.", "Muvaffaqiyat", MessageBoxButton.OK, MessageBoxImage.Information);

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
