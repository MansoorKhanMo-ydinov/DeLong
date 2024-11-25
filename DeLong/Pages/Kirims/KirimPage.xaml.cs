using System.Windows;
using System.Windows.Controls;
using DeLong.DbContexts;
using DeLong.Entities.Incomes;
using DeLong.Windows.Kirims;

namespace DeLong.Pages.Kirims
{
    public partial class KirimPage : Page
    {
        private readonly AppdbContext _dbContext;

        public KirimPage()
        {
            InitializeComponent();
            _dbContext = new AppdbContext(); // DbContextni ishga tushiramiz
            LoadData();
        }

        // Method to load data into the DataGrid
        private void LoadData()
        {
            try
            {
                var kirims = _dbContext.Kirims.ToList(); // Ma'lumotlarni olib kelish
                kirimDataGrid.ItemsSource = kirims; // DataGridga bog'lash
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Kirim ma'lumotlarini yuklashda xato: {ex.Message}", "Xato", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Search Button Click Event
        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string searchQuery = txtSearch.Text.Trim();

            try
            {
                var filteredKirims = _dbContext.Kirims
                    .Where(k => k.Ombornomi.Contains(searchQuery) || k.JamiSoni.ToString().Contains(searchQuery))
                    .ToList();

                kirimDataGrid.ItemsSource = filteredKirims;

                if (!filteredKirims.Any())
                {
                    MessageBox.Show($"Qidiruv bo'yicha hech narsa topilmadi: {searchQuery}", "Natija", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Qidiruvda xato: {ex.Message}", "Xato", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Add Kirim Button Click Event
        private void AddKirimButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                AddKirimWindow addKirimWindow = new AddKirimWindow(_dbContext);
                bool? result = addKirimWindow.ShowDialog();
                if (result == true)
                {
                    LoadData(); // Ma'lumotlarni qayta yuklash
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Kirim qo'shishda xato: {ex.Message}", "Xato", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Edit Button Click Event
        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var dataContext = button?.DataContext as Kirim;

            if (dataContext != null)
            {
                try
                {
                    AddKirimWindow editKirimWindow = new AddKirimWindow(_dbContext);
                    bool? result = editKirimWindow.ShowDialog();
                    if (result == true)
                    {
                        LoadData(); // Ma'lumotlarni qayta yuklash
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Tahrirlashda xato: {ex.Message}", "Xato", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Tahrirlash uchun ma'lumot tanlanmagan!", "Ogohlantirish", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        // Delete Button Click Event
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var dataContext = button?.DataContext as Kirim;

            if (dataContext != null)
            {
                MessageBoxResult result = MessageBox.Show("Haqiqatan ham ushbu ma'lumotni o'chirmoqchimisiz?", "O'chirish", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        _dbContext.Kirims.Remove(dataContext);
                        _dbContext.SaveChanges(); // O'zgarishlarni saqlash
                        LoadData(); // Ma'lumotlarni qayta yuklash
                        MessageBox.Show("Ma'lumot muvaffaqiyatli o'chirildi.", "O'chirish", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"O'chirishda xato: {ex.Message}", "Xato", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("O'chirish uchun ma'lumot tanlanmagan!", "Ogohlantirish", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
