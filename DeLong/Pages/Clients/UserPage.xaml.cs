using System.Windows;
using System.Windows.Controls;
using DeLong.Windows.Users;

namespace DeLong.Pages.Clients
{
    public partial class UserPage : Page
    {
        private List<User> users; // Foydalanuvchilar ro'yxati

        public UserPage()
        {
            InitializeComponent();
            LoadUsers();
        }

        // Foydalanuvchilarni yuklash
        private void LoadUsers()
        {
            // Bu yerda ma'lumotlar bazasidan foydalanuvchilarni yuklashingiz mumkin
            users = new List<User>
            {
                new User { FIO = "Ali Valiyev", Telefon = "998901234567", Adres = "Toshkent", TelegramRaqam = "@ali", INN = "123456789" },
                new User { FIO = "Sara Akbarova", Telefon = "998901234568", Adres = "Samarqand", TelegramRaqam = "@sara", INN = "987654321" }
                // Boshqa foydalanuvchilarni qo'shishingiz mumkin
            };

            userDataGrid.ItemsSource = users; // DataGridga foydalanuvchilarni ulash
        }

        // Qidiruv tugmasi bosilganda
        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            var searchTerm = txtSearch.Text.ToLower();
            var filteredUsers = users.Where(u => u.FIO.ToLower().Contains(searchTerm)).ToList();
            userDataGrid.ItemsSource = filteredUsers; // Filtrlangan foydalanuvchilarni ko'rsatish
        }

        // Foydalanuvchi qo'shish
        private void AddUserButton_Click(object sender, RoutedEventArgs e)
        {
            // Foydalanuvchi qo'shish formasi uchun yangi oynani ochish
            AddUserWindow userForm = new();
            if (userForm.ShowDialog() == true)
            {
                users.Add(userForm.NewUser); // Yangi foydalanuvchini ro'yxatga qo'shish
                userDataGrid.Items.Refresh(); // DataGridni yangilash
            }
        }

        // Tahrirlash tugmasi bosilganda
        

        // O'chirish tugmasi bosilganda
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var user = button.DataContext as User;

            // Foydalanuvchini o'chirish
            if (user != null)
            {
                users.Remove(user); // Foydalanuvchini ro'yxatdan olib tashlash
                userDataGrid.Items.Refresh(); // DataGridni yangilash
            }
        }

       private void EditButton_Click(object sender, RoutedEventArgs e)
{
    var button = sender as Button; // Tugma ob'ektini olish
    var user = button.DataContext as User; // Tugma kontekstidan foydalanuvchini olish

    if (user != null)
    {
        // Foydalanuvchini tahrirlash formasi uchun yangi oynani ochish
        AddUserWindow userForm = new AddUserWindow(user); // Foydalanuvchi ob'ekti bilan yangi forma

        // Forma ochilganda va "OK" tugmasi bosilganda yangilash
        if (userForm.ShowDialog() == true)
        {
            // Yangilangan foydalanuvchini yangilash
            var index = users.IndexOf(user); // Foydalanuvchini ro'yxatdagi indeksini olish
            if (index != -1)
            {
                // Yangilangan foydalanuvchini ro'yxatga qo'shish
                users[index] = userForm.UpdatedUser; // Yangilangan foydalanuvchini qo'shish
                userDataGrid.Items.Refresh(); // DataGridni yangilash
            }
        }
    }
}

    }

    // Foydalanuvchi klassi
    public class User
    {
        public string FIO { get; set; }
        public string Telefon { get; set; }
        public string Adres { get; set; }
        public string TelegramRaqam { get; set; }
        public string INN { get; set; }
    }
}
