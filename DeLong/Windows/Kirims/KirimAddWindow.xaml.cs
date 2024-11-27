using System.Windows;
using DeLong.DbContexts;
using DeLong.Entities.Kirims;  // Replace with the correct namespace for Kirim entity
using Microsoft.EntityFrameworkCore;

namespace DeLong.Windows.Kirims
{
    public partial class KirimAddWindow : Window
    {
        private readonly AppdbContext _dbContext; // AppDbContext for database interaction
        public Kirim NewKirim { get; private set; } // New Kirim (Entry) object

        public KirimAddWindow(AppdbContext dbContext)
        {
            InitializeComponent();
            _dbContext = dbContext; // Getting the DbContext via constructor
        }

        // "Add To Table" button click handler
        private void AddToTable_Click(object sender, RoutedEventArgs e)
        {
            
            // Validate the inputs before adding to the table
            string omborNomi = txtOmborNomi.Text.Trim();
            DateTime dateKirishSummasi = txtSana.SelectedDate ?? DateTime.Now;
            string yetkazuvchi = txtYetkazuvchi.Text.Trim();

            // Validate required fields
            if (string.IsNullOrWhiteSpace(omborNomi) ||
                string.IsNullOrWhiteSpace(yetkazuvchi) ||
                dateKirishSummasi == null)
            {
                MessageBox.Show("Iltimos, barcha maydonlarni to'ldiring.", "Xato", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Create a new Kirim (entry) object
            NewKirim = new Kirim
            {
                OmborNomi = omborNomi,
                Sana = dateKirishSummasi,
                Yetkazuvchi = yetkazuvchi
            };

            // Add the new Kirim to DataGrid
            var items = (informDataGrid.ItemsSource as System.Collections.ObjectModel.ObservableCollection<Kirim>) ?? new System.Collections.ObjectModel.ObservableCollection<Kirim>();
            items.Add(NewKirim);
            informDataGrid.ItemsSource = items;

            // Clear the form fields after adding to the table
            txtOmborNomi.Clear();
            txtYetkazuvchi.Clear();
            txtSana.SelectedDate = null;
        }

        // Save button click handler
        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (NewKirim == null)
            {
                MessageBox.Show("Iltimos, ma'lumotlarni jadvalga qo'shganingizdan so'ng saqlang.", "Xato", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                // Add new Kirim to the database
                _dbContext.Kirims.Add(NewKirim); // Assuming Kirim is your entity
                await _dbContext.SaveChangesAsync(); // Asynchronously save changes to the database

                // Show success message
                MessageBox.Show("Kirim muvaffaqiyatli saqlandi.", "Muvaffaqiyat", MessageBoxButton.OK, MessageBoxImage.Information);

                // Close the window
                this.DialogResult = true;
                this.Close();
            }
            catch (DbUpdateException dbEx)
            {
                // Handle database errors
                MessageBox.Show($"Ma'lumotlar bazasi xatoligi: {dbEx.Message}", "Xato", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                // Handle general errors
                MessageBox.Show($"Xatolik yuz berdi: {ex.Message}", "Xato", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Cancel button click handler
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close(); // Simply close the window
        }
    }
}
