using System.Windows;
using System.Windows.Controls;
using DeLong.Entities.Incomes;

namespace DeLong.Pages.Kirims
{
    public partial class KirimPage : Page
    {
        public KirimPage()
        {
            InitializeComponent();
        }

        // Search Button Click Event
        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string searchQuery = txtSearch.Text.Trim();
            // Qidiruv so'rovini ishlash uchun logika
            // Masalan, DataGrid-ni qidiruv natijalari bilan yangilash
            MessageBox.Show($"Search for: {searchQuery}");
            // Bu yerda siz filtering yoki boshqa qidiruv usullarini qo'llashingiz mumkin
        }

        // Add Kirim Button Click Event
        private void AddKirimButton_Click(object sender, RoutedEventArgs e)
        {
            // Yangi Kirim qo'shish logikasi
            // Masalan, yangi kirim qo'shish uchun formani ochish yoki boshqa sahifaga o'tish
            MessageBox.Show("Add Kirim clicked!");
        }

        // Edit Button Click Event
        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var dataContext = button?.DataContext as Kirim; // Kirim modeli bilan ishlash
            if (dataContext != null)
            {
                // Tahrirlash uchun Kirim ma'lumotlarini olish
                MessageBox.Show($"Edit clicked for Kirim entry with Sana: {dataContext.Sana}");
                // Tahrirlash oynasini ochish yoki boshqa amallarni bajarish
            }
        }

        // Delete Button Click Event
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var dataContext = button?.DataContext as Kirim; // Kirim ma'lumotlari
            if (dataContext != null)
            {
                MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this entry?", "Delete Confirmation", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    // Kirimni o'chirish logikasi
                    MessageBox.Show($"Deleted Kirim entry with Sana: {dataContext.Sana}");
                    // Bu yerda o'chirish uchun model yoki ma'lumotlar bazasiga o'zgarishlarni yuborish
                }
            }
        }
    }
}
