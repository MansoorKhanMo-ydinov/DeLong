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
        private AppdbContext _context;

        public UserPage()
        {
            InitializeComponent();
            _context = new AppdbContext();
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

        // Foydalanuvchi qo'shish
        private async void AddUserButton_Click(object sender, RoutedEventArgs e)
        {
            AddUserWindow userForm = new();
            if (userForm.ShowDialog() == true)
            {
                try
                {
                    // Yangi foydalanuvchini ma'lumotlar bazasiga qo'shish
                    _context.Users.Add(userForm.NewUser);
                    await _context.SaveChangesAsync();
                    LoadUsers(); // Foydalanuvchilarni yangidan yuklash
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Foydalanuvchini qo'shishda xato: {ex.Message}");
                }
            }
        }

        // Foydalanuvchini o'chirish
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

        // Tahrirlash tugmasi bosilganda
        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var user = button.DataContext as User;

            if (user != null)
            {
                UserEditWindow editWindow = new UserEditWindow(user); // Tahrirlash oynasiga foydalanuvchi yuboriladi
                if (editWindow.ShowDialog() == true)
                {
                    try
                    {
                        _context.Users.Update(editWindow.UpdatedUser);
                        _context.SaveChanges();
                        LoadUsers(); // Foydalanuvchilarni yangilash
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
