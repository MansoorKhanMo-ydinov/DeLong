using System.Windows;
using DeLong.DbContexts;
using DeLong.Entities.Kirims;
using Microsoft.EntityFrameworkCore;

namespace DeLong.Windows.Kirims
{
    public partial class EditKirimWindow : Window
    {
        private readonly AppdbContext _dbContext; // AppDbContext obyektini qo'shish
        public Kirim UpdatedKirim { get; private set; }

        private readonly Kirim _originalKirim; // Asl kirim obyektini saqlash

        public EditKirimWindow(AppdbContext dbContext, Kirim kirim)
        {
            InitializeComponent();

            _dbContext = dbContext;
            _originalKirim = kirim;

            // Kirim ma'lumotlarini formaga yuklash
            dpSana.SelectedDate = kirim.Sana;
            txtOmborNomi.Text = kirim.OmborNomi;
            txtJamiNarxi.Text = kirim.JamiNarxi.ToString();
            txtJamiSoni.Text = kirim.JamiSoni.ToString();
            cbRole.SelectedItem = kirim.Roles;
        }

        private void EditKirimButton_Click(object sender, RoutedEventArgs e)
        {
            // Jami narxi va jami sonini raqam formatida o'qish
            if (decimal.TryParse(txtJamiNarxi.Text, out decimal jamiNarxi) &&
                int.TryParse(txtJamiSoni.Text, out int jamiSoni) &&
                dpSana.SelectedDate.HasValue)
            {
                // Kirimni yangilash
                _originalKirim.Sana = dpSana.SelectedDate.Value;
                _originalKirim.OmborNomi = txtOmborNomi.Text;
                _originalKirim.JamiNarxi = jamiNarxi;
                _originalKirim.JamiSoni = jamiSoni;
                try
                {
                    // Ma'lumotlar bazasiga o'zgarishlarni saqlash
                    _dbContext.Entry(_originalKirim).State = EntityState.Modified;
                    _dbContext.SaveChanges();

                    MessageBox.Show("Kirim muvaffaqiyatli yangilandi.", "Muvaffaqiyat", MessageBoxButton.OK, MessageBoxImage.Information);

                    this.DialogResult = true; // Oynani yopishdan oldin natijani ko'rsatish
                    this.Close(); // Oynani yopish
                }
                catch (DbUpdateException ex)
                {
                    MessageBox.Show($"Xatolik yuz berdi: {ex.Message}", "Xato", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Iltimos, to'g'ri qiymatlar kiriting!", "Xato", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
