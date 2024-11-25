using System.Windows;
using DeLong.DbContexts;
using DeLong.Entities.Kirims;
using DeLong.Entities.Roles;
using Microsoft.EntityFrameworkCore;

namespace DeLong.Windows.Kirims
{
    public partial class AddKirimWindow : Window
    {
        private readonly AppdbContext _dbContext; // DbContext uchun private xususiyat
        public Kirim NewKirim { get; private set; } // Yangi kirim

        public AddKirimWindow(AppdbContext dbContext)
        {
            InitializeComponent();
            _dbContext = dbContext; // DbContext ni konstruktor orqali olish
            LoadRoles(); // ComboBox uchun rollarni yuklash
        }

        private void LoadRoles()
        {
            // Rollar ro'yxatini qo'shish
            cbRole.ItemsSource = new[] { "Admin", "Manager", "User" };
        }

        private async void AddKirimButton_Click(object sender, RoutedEventArgs e)
        {
            // Maydonlardan ma'lumotlarni olish
            string role = cbRole.SelectedItem?.ToString();
            string omborNomi = txtOmborNomi.Text.Trim();
            string jamiNarxiText = txtJamiNarxi.Text.Trim();
            string jamiSoniText = txtJamiSoni.Text.Trim();
            DateTime? sana = dpSana.SelectedDate;

            // Majburiy maydonlarni tekshirish
            if (string.IsNullOrWhiteSpace(omborNomi) || string.IsNullOrWhiteSpace(role) || !sana.HasValue)
            {
                MessageBox.Show("Iltimos, barcha majburiy maydonlarni to'ldiring.", "Xato", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Jami narxi va jami sonini raqamga aylantirish
            if (!decimal.TryParse(jamiNarxiText, out decimal jamiNarxi))
            {
                MessageBox.Show("Jami Narxi faqat raqam bo'lishi kerak.", "Xato", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!int.TryParse(jamiSoniText, out int jamiSoni))
            {
                MessageBox.Show("Jami Soni faqat raqam bo'lishi kerak.", "Xato", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Yangi Kirim yaratish
            NewKirim = new Kirim
            {
                Sana = sana.Value,
                Roles = role,
                OmborNomi = omborNomi,
                JamiNarxi = jamiNarxi,
                JamiSoni = jamiSoni
            };

            try
            {
                // Yangi Kirimni ma'lumotlar bazasiga qo'shish
                _dbContext.Kirims.Add(NewKirim);
                await _dbContext.SaveChangesAsync(); // O'zgarishlarni asinxron saqlash

                // Kirim muvaffaqiyatli qo'shilgani haqida xabar
                MessageBox.Show("Kirim muvaffaqiyatli qo'shildi.", "Muvaffaqiyat", MessageBoxButton.OK, MessageBoxImage.Information);

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
                // Umumiy xato xabarini ko'rsatish
                MessageBox.Show($"Xatolik yuz berdi: {ex.Message}", "Xato", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            // Ma'lumotlar jadvalidagi tahrir qilish funksiyasi
            MessageBox.Show("Edit tugmasi bosildi. Bu funksiya hali amalga oshirilmagan.", "Ma'lumot", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            // Ma'lumotlar jadvalidagi o'chirish funksiyasi
            MessageBox.Show("Delete tugmasi bosildi. Bu funksiya hali amalga oshirilmagan.", "Ma'lumot", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
