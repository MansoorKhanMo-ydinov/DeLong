using System.Windows;
using DeLong.DbContexts;
using DeLong.Entities.Kirims;
using DeLong.Entities.Roles;
using Microsoft.EntityFrameworkCore;

namespace DeLong.Windows.Kirims
{
    public partial class AddKirimWindow : Window
    {
        private readonly AppdbContext _dbContext; // DbContext xususiyati
        public Kirim NewKirim { get; private set; } // Yangi kirim

        public AddKirimWindow(AppdbContext dbContext)
        {
            InitializeComponent();
            _dbContext = dbContext;
            LoadRoles(); // Rollarni yuklash
        }

        private void LoadRoles()
        {
            try
            {
                // Rollarni bazadan olish
                var roles = _dbContext.Roles.Select(r => r.Name).ToList();

                // Agar rollar mavjud bo'lsa, ComboBox'ga ulanish
                cbRole.ItemsSource = roles;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Rollarni yuklashda xatolik yuz berdi: {ex.Message}", "Xatolik", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void AddKirimButton_Click(object sender, RoutedEventArgs e)
        {
            // Kiritilgan qiymatlarni olish
            string selectedRole = cbRole.SelectedItem?.ToString();
            string omborNomi = txtOmborNomi.Text.Trim();
            string jamiNarxiText = txtJamiNarxi.Text.Trim();
            string jamiSoniText = txtJamiSoni.Text.Trim();
            DateTime? sana = dpSana.SelectedDate;

            // Kiritilgan qiymatlarni tekshirish
            if (string.IsNullOrWhiteSpace(omborNomi) || string.IsNullOrWhiteSpace(selectedRole) || !sana.HasValue)
            {
                MessageBox.Show("Iltimos, barcha majburiy maydonlarni to'ldiring.", "Xatolik", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!decimal.TryParse(jamiNarxiText, out decimal jamiNarxi))
            {
                MessageBox.Show("Jami narxi faqat raqam bo'lishi kerak.", "Xatolik", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!int.TryParse(jamiSoniText, out int jamiSoni))
            {
                MessageBox.Show("Jami soni faqat raqam bo'lishi kerak.", "Xatolik", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Kirim obyektini yaratish
            NewKirim = new Kirim
            {
                Sana = sana.Value,
                OmborNomi = omborNomi,
                JamiNarxi = jamiNarxi,
                JamiSoni = jamiSoni,
                Roles = new List<Role> // Tanlangan rolni qo'shish
                {
                    new Role { Name = selectedRole }
                }
            };

            try
            {
                // Yangi Kirimni saqlash
                _dbContext.Kirims.Add(NewKirim);
                await _dbContext.SaveChangesAsync();

                MessageBox.Show("Kirim muvaffaqiyatli qo'shildi.", "Muvaffaqiyat", MessageBoxButton.OK, MessageBoxImage.Information);

                // Oynani yopish
                DialogResult = true;
                Close();
            }
            catch (DbUpdateException dbEx)
            {
                MessageBox.Show($"Ma'lumotlar bazasi xatoligi: {dbEx.Message}", "Xatolik", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Xatolik yuz berdi: {ex.Message}", "Xatolik", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            
        }


        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            // Delete tugmasining ishlashi uchun kerakli kod
        }

        private void AddNewRowButton_Click(object sender, RoutedEventArgs e)
        {
            // Yangi satr qo'shish kodini shu yerga joylashtirasiz
        }
    }
}
