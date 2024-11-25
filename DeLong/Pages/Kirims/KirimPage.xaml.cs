using System.Windows;
using System.Windows.Controls;
using DeLong.DbContexts;
using DeLong.Entities.Kirims;
using DeLong.Entities.Users;
using DeLong.Windows.Kirims;
using DeLong.Windows.Users;
using Microsoft.EntityFrameworkCore;

namespace DeLong.Pages.Kirims
{
    /// <summary>
    /// Interaction logic for KirimPage.xaml
    /// </summary>
    public partial class KirimPage : Page
    {
        private readonly AppdbContext _context;

        public KirimPage()
        {
            InitializeComponent();
            _context = new AppdbContext();
            LoadKirims();
        }
        private async void LoadKirims()
        {
            try
            {
                var users = await _context.Kirims.ToListAsync();
                kirimDataGrid.ItemsSource = users;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Foydalanuvchilarni yuklashda xato: {ex.Message}");
            }
        }
        private async void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is Kirim kirim)
            {
                if (MessageBox.Show("Ushbu foydalanuvchini o'chirishni xohlaysizmi?", "O'chirish tasdiqlash", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    try
                    {
                        _context.Kirims.Remove(kirim);
                        await _context.SaveChangesAsync();
                        LoadKirims();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Foydalanuvchini o'chirishda xato: {ex.Message}");
                    }
                }
            }
        }
        private async void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string searchText = txtSearch.Text.ToLower();

            var filteredUsers = await _context.Kirims
                .Where(u => u.Id.ToString().Contains(searchText) || u.Yetkazuvchi.Contains(searchText))
                .ToListAsync();

            kirimDataGrid.ItemsSource = filteredUsers;
        }
        private void AddUserButton_Click(object sender, RoutedEventArgs e)
        {
            var kirimForm = new AddKirimWindow(_context);
            if (kirimForm.ShowDialog() == true)
            {
                LoadKirims();
            }
        }
        private async void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is Kirim kirim)
            {
                var editWindow = new EditKirimWindow(_context, kirim);

                if (editWindow.ShowDialog() == true)
                {
                    try
                    {
                        _context.Kirims.Update(editWindow.UpdatedUser);
                        await _context.SaveChangesAsync();
                        LoadKirims();
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
