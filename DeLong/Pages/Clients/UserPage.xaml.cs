using System.Windows;
using System.Windows.Controls;
using DeLong.DbContexts;
using DeLong.Entities.Users;
using DeLong.Windows.Users;
using Microsoft.EntityFrameworkCore;

namespace DeLong.Pages.Clients;

public partial class UserPage : Page
{
    private readonly AppdbContext _context; // DbContext ni private sifatida e'lon qilamiz

    public UserPage()
    {
        InitializeComponent();
        _context = new AppdbContext(); // AppDbContext ni yaratish
        LoadUsers(); // Foydalanuvchilarni yuklash
    }

    // Foydalanuvchilarni yuklash
    private async void LoadUsers()
    {
        try
        {
            var users = await _context.Users.ToListAsync(); // Ma'lumotlar bazasidan foydalanuvchilarni yuklash
            userDataGrid.ItemsSource = users; // DataGridga foydalanuvchilarni ulash
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Foydalanuvchilarni yuklashda xato: {ex.Message}");
        }
    }

    // Foydalanuvchini o'chirish
    private async void DeleteButton_Click(object sender, RoutedEventArgs e)
    {
        if (sender is Button button && button.DataContext is User user)
        {
            if (MessageBox.Show("Ushbu foydalanuvchini o'chirishni xohlaysizmi?", "O'chirish tasdiqlash", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                try
                {
                    _context.Users.Remove(user);
                    await _context.SaveChangesAsync(); // O'zgarishlarni saqlash
                    LoadUsers(); // Foydalanuvchilarni yangilash
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Foydalanuvchini o'chirishda xato: {ex.Message}");
                }
            }
        }
    }

    // Qidirish funksiyasi
    private async void SearchButton_Click(object sender, RoutedEventArgs e)
    {
        string searchText = txtSearch.Text.ToLower();

        var filteredUsers = await _context.Users
            .Where(u => u.FIO.ToLower().Contains(searchText) || u.Telefon.Contains(searchText))
            .ToListAsync();

        userDataGrid.ItemsSource = filteredUsers; // Filtrlangan foydalanuvchilarni ko'rsatish
    }

    // Yangi foydalanuvchi qo'shish
    private void AddUserButton_Click(object sender, RoutedEventArgs e)
    {
        var userForm = new AddUserWindow(_context); // _context ni o'tkazing
        if (userForm.ShowDialog() == true)
        {
            LoadUsers(); // Foydalanuvchilarni yangidan yuklash
        }
    }

    // Foydalanuvchini tahrirlash
    private async void EditButton_Click(object sender, RoutedEventArgs e)
    {
        if (sender is Button button && button.DataContext is User user)
        {
            var editWindow = new UserEditWindow(_context, user); // UserEditWindow ni ochish

            if (editWindow.ShowDialog() == true)
            {
                try
                {
                    _context.Users.Update(editWindow.UpdatedUser); // Yangilangan foydalanuvchini ma'lumotlar bazasida yangilash
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
