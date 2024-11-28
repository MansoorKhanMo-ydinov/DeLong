using System.Windows;
using System.Windows.Controls;
using DeLong.DbContexts;  // Assuming your AppDbContext is here
using DeLong.Entities.Informs;
using DeLong.Entities.Kirims;
using DeLong.Windows.Kirims;  // Assuming your Kirim entity is here

namespace DeLong.Pages.Kirims
{
    /// <summary>
    /// Interaction logic for KirimPage.xaml
    /// </summary>
    public partial class KirimPage : Page
    {
        private readonly AppdbContext _dbContext;  // Database context to interact with the database

        public KirimPage()
        {
            InitializeComponent();
            _dbContext = new AppdbContext();  // Initialize the database context
            LoadKirimData();  // Load data from database
        }

        // Method to load Kirim data from the database and bind it to a DataGrid or ListView
        private void LoadKirimData()
        {
            try
            {
                var kirimList = _dbContext.Kirims.ToList();  // Fetch all Kirim records from the database
                kirimDataGrid.ItemsSource = kirimList;  // Bind the list to a DataGrid (you can replace this with a ListView or other control)
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Xatolik yuz berdi: {ex.Message}", "Xato", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void KirimDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedKirim = kirimDataGrid.SelectedItem as Kirim;
            if (selectedKirim != null)
            {
                // Handle selection (for example, open a detailed view or an edit window)
                MessageBox.Show($"Tanlangan Kirim: {selectedKirim.OmborNomi}", "Kirim Tanlandi", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        // Optional: Implement Delete functionality if needed
       
        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string searchTerm = txtSearch.Text.ToLower();
            Kirim kirim = new Kirim();
            // Assuming you have a collection of Kirim objects that the DataGrid is bound to
            var filteredData = kirim.Informs.Where(k =>
                k.TovarNomi.ToLower().Contains(searchTerm) ||
                k.KirishSummasi.ToString().Contains(searchTerm)).ToList();

            // Update the DataGrid with the filtered data
            kirimDataGrid.ItemsSource = filteredData;
        }

        private void AddKirimButton_Click(object sender, RoutedEventArgs e)
        {
            var addKirimWindow = new AddKirimWindow(_dbContext);  // Assuming you have a window to add new entries
            addKirimWindow.ShowDialog();  // Show the window as a dialog
            LoadKirimData();
        }

       
        private async void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedKirim = kirimDataGrid.SelectedItem as Kirim;
            if (selectedKirim != null)
            {
                var result = MessageBox.Show("Kirimni o'chirmoqchimisiz?", "Tasdiqlash", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        _dbContext.Kirims.Remove(selectedKirim);
                        await _dbContext.SaveChangesAsync();
                        MessageBox.Show("Kirim muvaffaqiyatli o'chirildi.", "Muvaffaqiyat", MessageBoxButton.OK, MessageBoxImage.Information);
                        LoadKirimData();  // Refresh the DataGrid after deletion
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Xatolik yuz berdi: {ex.Message}", "Xato", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Iltimos, o'chirish uchun biror Kirimni tanlang.", "Xato", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
