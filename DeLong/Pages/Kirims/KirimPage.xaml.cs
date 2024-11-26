using System.Windows;
using System.Windows.Controls;
using DeLong.DbContexts;
using DeLong.Entities.Informs;
using Microsoft.EntityFrameworkCore;

namespace DeLong.Pages.Kirims
{

    public partial class KirimPage : Page
    {
        private readonly AppdbContext _context;
        public KirimPage()
        {
            InitializeComponent();
            _context = new AppdbContext();
            LoadKirimData();
        }

        // Kirim ma'lumotlarini yuklash
        private async void LoadKirimData()
        {
            try
            {
                var kirims = await _context.Kirims.ToListAsync();
                dgKirimlar.ItemsSource = kirims;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Foydalanuvchilarni yuklashda xato: {ex.Message}");
            }
        }

        // Search tugmasi bosilganda
        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string searchText = txtSearch.Text.ToLower();

            using (var context = new AppdbContext())
            {
                var searchResult = context.Kirims
                    .Where(k => k.OmborNomi.ToLower().Contains(searchText) ||
                                k.Yetkazuvchi.ToLower().Contains(searchText))
                    .ToList();
                dgKirimlar.ItemsSource = searchResult;
            }
        }

        

        private void InformButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedKirim = dgKirimlar.SelectedItem as Inform;

            if (selectedKirim == null)
            {
                MessageBox.Show("Iltimos, jadvaldan elementni tanlang.", "Ogohlantirish", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            //var informWindow = new InformDetailsWindow(selectedKirim); 
            //informWindow.ShowDialog();
        }

        // Edit tugmasi bosilganda
        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedKirim = dgKirimlar.SelectedItem as Inform;

            if (selectedKirim == null)
            {
                MessageBox.Show("Iltimos, jadvaldan elementni tanlang.", "Ogohlantirish", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Ma'lumotni tahrirlash oynasini ochish
            //var editWindow = new EditKirimWindow(selectedKirim); // Tahrirlash oynasi (alohida yaratiladi)
            //editWindow.ShowDialog();

            // Tahrirlanganidan so'ng ro'yxatni yangilash
            LoadKirimData();
        }

        // Delete tugmasi bosilganda
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedKirim = dgKirimlar.SelectedItem as Inform;

            if (selectedKirim == null)
            {
                MessageBox.Show("Iltimos, jadvaldan elementni tanlang.", "Ogohlantirish", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var result = MessageBox.Show("Ushbu kirimni o'chirmoqchimisiz?", "Tasdiqlash", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                using (var context = new AppdbContext())
                {
                    var kirimToRemove = context.Kirims.FirstOrDefault(k => k.Id == selectedKirim.Id);
                    if (kirimToRemove != null)
                    {
                        context.Kirims.Remove(kirimToRemove);
                        context.SaveChanges();
                        MessageBox.Show("Kirim muvaffaqiyatli o'chirildi.", "Ma'lumot", MessageBoxButton.OK, MessageBoxImage.Information);
                        LoadKirimData();
                    }
                }
            }
        }

        private void AddUserButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ClosePopupButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
