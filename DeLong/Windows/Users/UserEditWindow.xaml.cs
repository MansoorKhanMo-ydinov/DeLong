using System.Windows;
using DeLong.Entities.Users;

namespace DeLong.Windows.Users
{
    /// <summary>
    /// Interaction logic for UserEditWindow.xaml
    /// </summary>
    public partial class UserEditWindow : Window
    {
        public User UpdatedUser { get; private set; } // Yangilangan foydalanuvchi

        public UserEditWindow(User user)
        {
            InitializeComponent();

            // Foydalanuvchini formaga yuklash
            txtFIO.Text = user.FIO;
            txtTelefon.Text = user.Telefon;
            txtAdres.Text = user.Adres;
            txtTelegramRaqam.Text = user.TelegramRaqam;
            txtINN.Text = user.INN.ToString();
        }

        private void EditUserButton_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(txtINN.Text, out int innValue))
            {
                // Yangilangan foydalanuvchini yaratish
                UpdatedUser = new User
                {
                    FIO = txtFIO.Text,
                    Telefon = txtTelefon.Text,
                    Adres = txtAdres.Text,
                    TelegramRaqam = txtTelegramRaqam.Text,
                    INN = innValue // INT tipidagi INN
                };

                this.DialogResult = true; // Oynani yopishdan oldin natijani ko'rsatish
                this.Close(); // Oynani yopish
            }
            else
            {
                MessageBox.Show("Iltimos, INN maydoniga to'g'ri raqam kiriting!", "Xato", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    }
}
