using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using DeLong.DbContexts;
using DeLong.Entities.Informs;
using DeLong.Entities.Kirims;

namespace DeLong.Windows.Kirims
{
    public partial class AddKirimWindow : Window
    {
        private readonly AppdbContext _dbContext;
        private List<Inform> kirimItems = new List<Inform>(); // Kirim itemlarini saqlash

        public AddKirimWindow(AppdbContext dbContext)
        {
            InitializeComponent();
            _dbContext = dbContext;
        }

        // Inform jadvaliga yangi qator qo'shish
        private void AddInformRowButton_Click(object sender, RoutedEventArgs e)
        {
            string tovarNomi = txtTovarNomi.Text.Trim();
            if (string.IsNullOrWhiteSpace(tovarNomi))
            {
                MessageBox.Show("Tovar nomi kiritilmadi!", "Xato", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Kirish narxi va sotilish narxini tekshirish
            if (!decimal.TryParse(txtKirishNarxi.Text, out decimal kirishNarxi) || kirishNarxi <= 0)
            {
                MessageBox.Show("Iltimos, to'g'ri kirish narxini kiriting.", "Xato", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!decimal.TryParse(txtSotilishNarxi.Text, out decimal sotilishNarxi) || sotilishNarxi <= 0)
            {
                MessageBox.Show("Iltimos, to'g'ri sotilish narxini kiriting.", "Xato", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Soni tekshirish
            if (!int.TryParse(txtSoni.Text, out int soni) || soni <= 0)
            {
                MessageBox.Show("Iltimos, to'g'ri soni kiriting.", "Xato", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Jadvalga yangi kirim elementini qo'shish
            kirimItems.Add(new Inform
            {
                TovarNomi = tovarNomi,
                KirishSummasi = kirishNarxi,
                SotilishNarxi = sotilishNarxi,
                Soni = soni
            });

            // DataGrid'ni yangilash
            InformDataGrid.ItemsSource = null;
            InformDataGrid.ItemsSource = kirimItems;
        }

        // Saqlash tugmasi bosilganda
        private async void SaveKirimButton_Click(object sender, RoutedEventArgs e)
        {
            string omborNomi = txtOmborNomi.Text.Trim();
            DateTime sana = datePickerSana.SelectedDate ?? DateTime.Now;
            string yetkazuvchi = txtYetkazuvchi.Text.Trim();

            if (string.IsNullOrWhiteSpace(omborNomi) || string.IsNullOrWhiteSpace(yetkazuvchi) || kirimItems.Count == 0)
            {
                MessageBox.Show("Iltimos, barcha ma'lumotlarni to'ldiring va mahsulot qo'shing.", "Xato", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                // Kirimni yaratish
                Kirim kirim = new Kirim
                {
                    OmborNomi = omborNomi,
                    Sana = sana,
                    Yetkazuvchi = yetkazuvchi,
                    Informs = kirimItems
                };

                // Ma'lumotlar bazasiga qo'shish
                _dbContext.Kirims.Add(kirim);
                await _dbContext.SaveChangesAsync();

                // Muvaffaqiyatli xabar
                MessageBox.Show("Kirim muvaffaqiyatli saqlandi.", "Muvaffaqiyat", MessageBoxButton.OK, MessageBoxImage.Information);
                this.DialogResult = true;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Xatolik yuz berdi: {ex.Message}", "Xato", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Bekor qilish tugmasi
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var row = button?.DataContext as Inform; // YourDataModel - bu sizning jadval ma'lumotlarini saqlaydigan model

            if (row != null)
            {
                // Masalan, yangi ma'lumotlar bilan formani to'ldirish
                txtTovarNomi.Text = row.TovarNomi;
                txtKirishNarxi.Text = row.KirishSummasi.ToString();
                txtSotilishNarxi.Text = row.SotilishNarxi.ToString();
                txtSoni.Text = row.Soni.ToString();

                // Yangi qiymatlar bilan ma'lumotni tahrir qilish uchun boshqa amallarni bajaring
                // Siz shu yerda formani ko'rsating yoki boshqa kerakli harakatlarni qilishingiz mumkin
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var row = button?.DataContext as Inform; // YourDataModel - bu sizning jadval ma'lumotlarini saqlaydigan model

            if (row != null)
            {
                // Ma'lumotni olib tashlash
                // Masalan, ro'yxatdan o'chirish
                var dataCollection = InformDataGrid.ItemsSource as ObservableCollection<Inform>;
                if (dataCollection != null)
                {
                    dataCollection.Remove(row);
                }
            }
            }
    }
}
