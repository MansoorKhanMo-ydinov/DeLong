using System.Windows;
using DeLong.DbContexts;
using DeLong.Entities.Warehouses;
using Microsoft.EntityFrameworkCore;

namespace DeLong.Windows.Warehouses
{
    public partial class EditWarehouseWindow : Window
    {
        private readonly AppdbContext _dbContext;
        public Warehouse UpdatedWarehouse { get; private set; }
        private readonly Warehouse _originalWarehouse; 

        public EditWarehouseWindow(AppdbContext dbContext,Warehouse warehouse)
        {
            InitializeComponent();

            _dbContext = dbContext;
            _originalWarehouse = warehouse;

            txtName.Text = warehouse.Name;
            txtAddress.Text = warehouse.Adres;
            txtCreatedAt.Text = warehouse.CreatedAt.ToString();
            txtUpdatedAt.Text = warehouse.UpdatedAt.ToString();
        }

        private void EditWarehouseButton_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(txtWarehouseID.Text, out int innValue))
            {
                _originalWarehouse.Adres = txtAddress.Text;
                _originalWarehouse.Name = txtName.Text;
                _originalWarehouse.CreatedAt = DateTime.Parse(txtCreatedAt.Text);
                _originalWarehouse.UpdatedAt = DateTime.UtcNow;
                try
                {
                    _dbContext.Entry(_originalWarehouse).State = EntityState.Modified;
                    _dbContext.SaveChanges();

                    MessageBox.Show("Foydalanuvchi muvaffaqiyatli yangilandi.", "Muvaffaqiyat", MessageBoxButton.OK, MessageBoxImage.Information);

                    this.DialogResult = true; 
                    this.Close(); 
                }
                catch (DbUpdateException ex)
                {
                    MessageBox.Show($"Xatolik yuz berdi: {ex.Message}", "Xato", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Iltimos, INN maydoniga to'g'ri raqam kiriting!", "Xato", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    }
}
