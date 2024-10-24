using System.Windows;
using DeLong.Entities.Users;

namespace DeLong.Windows.Users;

/// <summary>
/// Interaction logic for AddUserWindow.xaml
/// </summary>
public partial class AddUserWindow : Window
{
    public User NewUser { get; private set; } // Yangi foydalanuvchi uchun xususiyat

    public AddUserWindow()
    {
        InitializeComponent();
    }

    // "Add User" tugmasi bosilganda
    private void AddUserButton_Click(object sender, RoutedEventArgs e)
    {
        // Foydalanuvchi ma'lumotlarini olish
        string fio = txtFIO.Text.Trim();
        string telefon = txtTelefon.Text.Trim();
        string adres = txtAdres.Text.Trim();
        string telegramRaqam = txtTelegramRaqam.Text.Trim();
        string innText = txtINN.Text.Trim();

        // Ma'lumotlarni tekshirish
        if (string.IsNullOrWhiteSpace(fio) ||
            string.IsNullOrWhiteSpace(telefon) ||
            string.IsNullOrWhiteSpace(adres) ||
            string.IsNullOrWhiteSpace(telegramRaqam) ||
            string.IsNullOrWhiteSpace(innText))
        {
            MessageBox.Show("Iltimos, barcha maydonlarni to'ldiring.", "Xato", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        // INN qiymatini int ga aylantirish
        if (!int.TryParse(innText, out int inn))
        {
            MessageBox.Show("INN faqat raqam bo'lishi kerak.", "Xato", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        // Yangi foydalanuvchini yaratish
        NewUser = new User
        {
            FIO = fio,
            Telefon = telefon,
            Adres = adres,
            TelegramRaqam = telegramRaqam,
            INN = inn // INN endi int tipida
        };

        // Foydalanuvchini muvaffaqiyatli qo'shilgani haqida xabar ko'rsatish
        MessageBox.Show("Foydalanuvchi muvaffaqiyatli qo'shildi.", "Muvaffaqiyat", MessageBoxButton.OK, MessageBoxImage.Information);

        // Oynani yopish
        this.DialogResult = true;
        this.Close();
    }
}
