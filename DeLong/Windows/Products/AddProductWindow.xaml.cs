using System.Windows;
using DeLong.DbContexts;
using DeLong.Entities.Products;
using Microsoft.EntityFrameworkCore;

namespace DeLong.Windows.Products
{
    public partial class AddProductWindow : Window
    {
        private readonly AppdbContext _dbContext; // DbContext for database access
        public Product NewProduct { get; private set; } // New product to be added

        public AddProductWindow(AppdbContext dbContext)
        {
            InitializeComponent();
            _dbContext = dbContext; // Initialize DbContext through the constructor
        }

        // "Add Product" button click event handler
        private async void AddProductButton_Click(object sender, RoutedEventArgs e)
        {
            // Collect product details from input fields
            string belgi = txtBelgi.Text.Trim();
            string narxiSumdaText = txtNarxisumda.Text.Trim();
            string narxiDollordaText = txtNarxiDollorda.Text.Trim();
            string jamiNarxiSumdaText = txtJamiNarxiSumda.Text.Trim();
            string jamiNarxiDollordaText = txtJaminarxiDollorda.Text.Trim();

            // Check if required fields are filled
            if (string.IsNullOrWhiteSpace(belgi) ||
                string.IsNullOrWhiteSpace(narxiSumdaText) ||
                string.IsNullOrWhiteSpace(narxiDollordaText) ||
                string.IsNullOrWhiteSpace(jamiNarxiSumdaText) ||
                string.IsNullOrWhiteSpace(jamiNarxiDollordaText))
            {
                MessageBox.Show("Please fill in all required fields.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Convert text fields to numerical values
            if (!decimal.TryParse(narxiSumdaText, out decimal narxiSumda))
            {
                MessageBox.Show("Narxi (sumda) must be a valid decimal number.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (!decimal.TryParse(narxiDollordaText, out decimal narxiDollorda))
            {
                MessageBox.Show("Narxi (dollorda) must be a valid decimal number.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (!decimal.TryParse(jamiNarxiSumdaText, out decimal jamiNarxiSumda))
            {
                MessageBox.Show("Jami Narxi (sumda) must be a valid decimal number.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (!decimal.TryParse(jamiNarxiDollordaText, out decimal jamiNarxiDollorda))
            {
                MessageBox.Show("Jami Narxi (dollorda) must be a valid decimal number.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Create a new product instance
            NewProduct = new Product
            {
                Belgi = belgi,
                NarxiSumda = narxiSumda,
                NarxiDollorda = narxiDollorda,
                JamiNarxiSumda = jamiNarxiSumda,
                JamiNarxiDollarda = jamiNarxiDollorda
            };

            try
            {
                // Add new product to the database
                _dbContext.Products.Add(NewProduct);
                await _dbContext.SaveChangesAsync(); // Asynchronously save changes

                // Show a success message
                MessageBox.Show("Product added successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                // Close the window with a successful result
                this.DialogResult = true;
                this.Close();
            }
            catch (DbUpdateException dbEx)
            {
                // Handle database update errors
                MessageBox.Show($"Database error: {dbEx.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                // Show any other errors
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
