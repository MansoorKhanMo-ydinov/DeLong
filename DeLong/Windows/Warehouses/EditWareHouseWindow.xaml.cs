using System.Windows;
using DeLong.DbContexts;
using DeLong.Entities.Warehouses;
using Microsoft.EntityFrameworkCore;

namespace DeLong.Windows.Warehouses
{
    public partial class EditWarehouseWindow : Window
    {
        private readonly AppdbContext _dbContext;
        private readonly int _warehouseId;
        private Warehouse _warehouse;

        public EditWarehouseWindow(int warehouseId, AppdbContext dbContext)
        {
            InitializeComponent();
            _dbContext = dbContext;
            _warehouseId = warehouseId;
            LoadWarehouseData();
        }

        private void LoadWarehouseData()
        {
            // Fetch warehouse from database based on ID
            _warehouse = _dbContext.Warehouses
                .AsNoTracking()
                .FirstOrDefault(w => w.Id == _warehouseId);

            if (_warehouse == null)
            {
                MessageBox.Show("Warehouse not found.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Close();
                return;
            }

            // Populate fields with data
            txtWarehouseID.Text = _warehouse.Id.ToString();
            txtName.Text = _warehouse.Name;
            txtAddress.Text = _warehouse.Adres;
            txtCreatedAt.Text = _warehouse.CreatedAt.ToString("g");
        }

        private void EditWarehouseButton_Click(object sender, RoutedEventArgs e)
        {
            // Input validation
            if (string.IsNullOrWhiteSpace(txtName.Text) || string.IsNullOrWhiteSpace(txtAddress.Text))
            {
                MessageBox.Show("Please fill out all fields.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                // Reload warehouse from the database to avoid concurrency issues
                _warehouse = _dbContext.Warehouses.FirstOrDefault(w => w.Id == _warehouseId);
                if (_warehouse == null)
                {
                    MessageBox.Show("Warehouse no longer exists.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    Close();
                    return;
                }

                // Update fields
                _warehouse.Name = txtName.Text;
                _warehouse.Adres = txtAddress.Text;
                _warehouse.UpdatedAt = DateTime.Now;

                _dbContext.SaveChanges();
                MessageBox.Show("Warehouse updated successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
