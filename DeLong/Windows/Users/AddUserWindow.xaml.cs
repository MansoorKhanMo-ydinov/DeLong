using System.Windows;
using DeLong.Pages.Clients;

namespace DeLong.Windows.Users
{
    /// <summary>
    /// Interaction logic for AddUserWindow.xaml
    /// </summary>
    public partial class AddUserWindow : Window
    {
        public User NewUser { get; private set; } // Yangi foydalanuvchini saqlash uchun xususiyat
        public User UpdatedUser { get; private set; } // Yangilangan foydalanuvchi uchun xususiyat


        public AddUserWindow(User userData = null) // Parametr qo'shildi
        {
            InitializeComponent();

            if (userData != null)
            {
                // Agar foydalanuvchi tahrirlanayotgan bo'lsa, maydonlarni to'ldirish
                txtFIO.Text = userData.FIO;
                txtTelefon.Text = userData.Telefon;
                txtAdres.Text = userData.Adres;
                txtTelegramRaqam.Text = userData.TelegramRaqam;
                txtINN.Text = userData.INN;
            }
        }

        // "Add User" tugmasi bosilganda chaqiriladi
        private void AddUserButton_Click(object sender, RoutedEventArgs e)
        {
            // Foydalanuvchi ma'lumotlarini olish
            string fio = txtFIO.Text.Trim();
            string telefon = txtTelefon.Text.Trim();
            string adres = txtAdres.Text.Trim();
            string telegramRaqam = txtTelegramRaqam.Text.Trim();
            string inn = txtINN.Text.Trim();

            // Ma'lumotlarni tekshirish
            if (string.IsNullOrWhiteSpace(fio) ||
                string.IsNullOrWhiteSpace(telefon) ||
                string.IsNullOrWhiteSpace(adres) ||
                string.IsNullOrWhiteSpace(telegramRaqam) ||
                string.IsNullOrWhiteSpace(inn))
            {
                MessageBox.Show("Iltimos, barcha maydonlarni to'ldiring.", "Xato", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Yangi foydalanuvchini saqlash
            NewUser = new User // Yangi foydalanuvchini yaratish
            {
                FIO = fio,
                Telefon = telefon,
                Adres = adres,
                TelegramRaqam = telegramRaqam,
                INN = inn
            };

            // Yana bir xabar ko'rsatish
            MessageBox.Show("Foydalanuvchi muvaffaqiyatli qo'shildi.", "Muvaffaqiyat", MessageBoxButton.OK, MessageBoxImage.Information);

            // Dasturdan chiqish
            this.DialogResult = true; // Oyna muvaffaqiyatli yopiladi
            this.Close();
        }
    }
}
