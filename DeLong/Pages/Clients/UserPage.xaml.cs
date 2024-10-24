using System.Windows;
using System.Windows.Controls;
using DeLong.DbContexts;
using DeLong.Entities.Users;
using DeLong.Windows.Users;
using Microsoft.EntityFrameworkCore;

namespace DeLong.Pages.Clients
{
    public partial class UserPage : Page
    {
        private readonly AppdbContext _context; // DbContext ni private sifatida e'lon qilamiz

        public UserPage()
        {
            InitializeComponent();
            _context = new AppdbContext(); // AppDbContext ni yaratish
            LoadUsers();
        }

        // Foydalanuvchilarni yuklash
        private async void LoadUsers()
        {
            try
            {
                // Ma'lumotlar bazasidan foydalanuvchilarni yuklash
                var users = await _context.Users.ToListAsync();
                userDataGrid.ItemsSource = users; // DataGridga foydalanuvchilarni ulash
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Foydalanuvchilarni yuklashda xato: {ex.Message}");
            }
        }

        private async void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var user = button.DataContext as User;

            if (user != null)
            {
                try
                {
                    _context.Users.Remove(user);
                    await _context.SaveChangesAsync();
                    LoadUsers(); // Foydalanuvchilarni yangilash
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Foydalanuvchini o'chirishda xato: {ex.Message}");
                }
            }
        }
        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            // Qidirish funksiyasini amalga oshirish mumkin
            // Masalan, qidirish maydonidan foydalanuvchilarni filtrlash
            string searchText = txtSearch.Text.ToLower();
            var filteredUsers = _context.Users
                .Where(u => u.FIO.ToLower().Contains(searchText) || u.Telefon.Contains(searchText))
                .ToList();
            userDataGrid.ItemsSource = filteredUsers; // Filtrlangan foydalanuvchilarni ko'rsatish
        }

        private void AddUserButton_Click(object sender, RoutedEventArgs e)
        {
            AddUserWindow userForm = new AddUserWindow(_context); // _context ni o'tkazing
            if (userForm.ShowDialog() == true)
            {
                // Foydalanuvchilarni yangidan yuklash
                LoadUsers(); // Foydalanuvchilarni yangidan yuklash
            }
        }

        private async void EditButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button; // Tugma ob'ektini olish
            var user = button.DataContext as User; // Tugma orqali foydalanuvchini olish

            if (user != null)
            {
                // UserEditWindow ni ochish
                UserEditWindow editWindow = new(_context,user);
                // Agar foydalanuvchi muvaffaqiyatli tahrir qilinsa
                if (editWindow.ShowDialog() == true)
                {
                    try
                    {
                        // Yangilangan foydalanuvchini ma'lumotlar bazasida yangilash
                        _context.Users.Update(editWindow.UpdatedUser);
                        await _context.SaveChangesAsync(); // O'zgarishlarni saqlash
                        LoadUsers(); // Foydalanuvchilar ro'yxatini yangilash
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Foydalanuvchini tahrirlashda xato: {ex.Message}");
                    }
                }
            }
        }


    }
}
