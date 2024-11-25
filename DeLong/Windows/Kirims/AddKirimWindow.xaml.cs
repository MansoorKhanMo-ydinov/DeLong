using System.Windows;
using DeLong.DbContexts;
using DeLong.Entities.Incomes;
using Microsoft.EntityFrameworkCore;

namespace DeLong.Windows.Kirims
{
    public partial class AddKirimWindow : Window
    {
        private readonly AppdbContext _dbContext;
        public Kirim NewKirim { get; private set; }

        public AddKirimWindow(AppdbContext dbContext)
        {
            InitializeComponent();
            _dbContext = dbContext;
        }

        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            string tr = txtTR.Text.Trim();
            string omborNomi = txtOmborNomi.Text.Trim();
            string jamiSoni = txtJamiSoni.Text.Trim();
            string jamiNarxi = txtJamiNarxi.Text.Trim();
            DateTime? sana = dpSana.SelectedDate;

            // Validate the form fields
            if (string.IsNullOrWhiteSpace(tr) ||
                string.IsNullOrWhiteSpace(omborNomi) ||
                string.IsNullOrWhiteSpace(jamiSoni) ||
                string.IsNullOrWhiteSpace(jamiNarxi) ||
                !sana.HasValue)
            {
                MessageBox.Show("Iltimos, barcha maydonlarni to'ldiring.", "Xato", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Try parsing numeric values
            if (!int.TryParse(jamiSoni, out int soni))
            {
                MessageBox.Show("Jami Soni raqam bo'lishi kerak.", "Xato", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            //if (!decimal.TryParse(jamiNarxi, out decimal narxi))
            //{
            //    MessageBox.Show("Jami Narxi raqam bo'lishi kerak.", "Xato", MessageBoxButton.OK, MessageBoxImage.Warning);
            //    return;
            //}

            // Create new Kirim object
            NewKirim = new Kirim
            {
                Id = int.Parse(tr),
                Ombornomi = omborNomi,
                JamiSoni = soni,
                Jaminarxi = decimal.Parse(jamiNarxi.ToString()),
                Sana = sana.Value
            };

            try
            {
                // Add new Kirim to the database
                _dbContext.Kirims.Add(NewKirim);
                await _dbContext.SaveChangesAsync();

                // Show success message
                MessageBox.Show("Kirim muvaffaqiyatli qo'shildi.", "Muvaffaqiyat", MessageBoxButton.OK, MessageBoxImage.Information);

                // Close the window
                this.DialogResult = true;
                this.Close();
            }
            catch (DbUpdateException dbEx)
            {
                MessageBox.Show($"Ma'lumotlar bazasi xatoligi: {dbEx.Message}", "Xato", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Xatolik yuz berdi: {ex.Message}", "Xato", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            // Tekshirish: maydonlar to'ldirilganmi
            if (string.IsNullOrWhiteSpace(txtOmborNomi.Text) || dpSana.SelectedDate == null)
            {
                MessageBox.Show("Iltimos, barcha maydonlarni to'ldiring!", "Xato", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Yangi kirim elementini yaratish
            var yangiItem = new
            {
                TR = dgInform.Items.Count + 1, // Avtomatik raqam
                MahsulotNomi = txtOmborNomi.Text,
                KirishSummasi = 1500, // Default qiymat, keyinchalik hisoblash mumkin
                Soni = 20,   
                Action="salom",
                SotishNarxi = 1800     // Default qiymat
            };

            // DataGridga qo'shish
            dgInform.Items.Add(yangiItem);

            // Tozalash
            txtOmborNomi.Text = string.Empty;
            dpSana.SelectedDate = null;

            // Muvaffaqiyatli xabar
            MessageBox.Show("Yangi kirim jadvalga qo'shildi!", "Ma'lumot", MessageBoxButton.OK, MessageBoxImage.Information);
        }

    }
}
