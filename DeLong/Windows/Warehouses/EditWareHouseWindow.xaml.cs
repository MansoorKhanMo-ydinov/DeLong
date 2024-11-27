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

        public EditWarehouseWindow(AppdbContext dbContext, Warehouse warehouse)
        {
            InitializeComponent();

            _dbContext = dbContext;
            _originalWarehouse = warehouse;

            // UI-ni ma'lumot bilan to'ldirish
            txtWarehouseID.Text = warehouse.Id.ToString();
            txtName.Text = warehouse.Name;
            txtAddress.Text = warehouse.Adres;
            txtCreatedAt.Text = warehouse.CreatedAt.ToString("yyyy-MM-dd HH:mm:ss"); // Formatlash
            txtUpdatedAt.Text = warehouse.UpdatedAt.ToString("yyyy-MM-dd HH:mm:ss") ?? string.Empty;
        }

        private void EditWarehouseButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Faqat kerakli maydonlarni o'zgartirish
                _originalWarehouse.Name = txtName.Text;
                _originalWarehouse.Adres = txtAddress.Text;

                // UpdatedAt maydoni avtomatik yangilanishi
                _originalWarehouse.UpdatedAt = DateTime.UtcNow;

                // Entity Framework uchun o'zgarishlarni belgilang
                _dbContext.Entry(_originalWarehouse).State = EntityState.Modified;
                _dbContext.SaveChanges();

                MessageBox.Show("Ombor muvaffaqiyatli yangilandi.", "Muvaffaqiyat", MessageBoxButton.OK, MessageBoxImage.Information);

                this.DialogResult = true;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Xatolik yuz berdi: {ex.Message}", "Xatolik", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
