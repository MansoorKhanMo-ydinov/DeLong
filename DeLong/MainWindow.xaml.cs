using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;
using DeLong.Pages.Clients;

namespace DeLong;

public partial class MainWindow : Window
{
    private UserPage _userPage;
    public MainWindow()
    {
        InitializeComponent();
        _userPage = new UserPage();
        Navigator.Content = _userPage;
    }
    private void ExitApplication(object sender, MouseButtonEventArgs e)
    {
        if (MessageBox.Show("Ishonchingiz komilmi?", "Chiqish", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
        {
            Application.Current.Shutdown();
        }
    }

    private void Navigator_Navigated(object sender, NavigationEventArgs e)
    {

    }

    private void UsersButton_Click(object sender, MouseButtonEventArgs e)
    {
        Navigator.Navigate(new Pages.Clients.UserPage());

    }

    private void LanguageAPP(object sender, SelectionChangedEventArgs e)
    {
        if (languageComboBox.SelectedItem is ComboBoxItem selectedItem)
        {
            // Tanlangan til kodi orqali madaniyatni o'zgartirish
            string selectedLanguage = selectedItem.Tag.ToString();
            DeLong.Resourses.Resource.Culture = new CultureInfo(selectedLanguage);
            UpdateLanguage();
        }
    }
    private void UpdateLanguage()
    {
        _userPage.userDataGrid.Columns[0].Header = DeLong.Resourses.Resource.Add;
        _userPage.userDataGrid.Columns[1].Header = DeLong.Resourses.Resource.Add;
        _userPage.userDataGrid.Columns[2].Header = DeLong.Resourses.Resource.Add;
        _userPage.userDataGrid.Columns[3].Header = DeLong.Resourses.Resource.Add;
        _userPage.userDataGrid.Columns[4].Header = DeLong.Resourses.Resource.Add;
        _userPage.userDataGrid.Columns[5].Header = DeLong.Resourses.Resource.Add;
        _userPage.userDataGrid.Columns[6].Header = DeLong.Resourses.Resource.Add;
        _userPage.userDataGrid.Columns[7].Header = DeLong.Resourses.Resource.Add;
        _userPage.userDataGrid.Columns[8].Header = DeLong.Resourses.Resource.Add;
        _userPage.userDataGrid.Columns[9].Header = DeLong.Resourses.Resource.Add;
        _userPage.userDataGrid.Columns[10].Header = DeLong.Resourses.Resource.Add;
        _userPage.MySearch.Content = DeLong.Resourses.Resource.Search;
        _userPage.AddButton1.Content = DeLong.Resourses.Resource.Add;
        languageComboBox.Text = DeLong.Resourses.Resource.Language; 
        myProductLabel.Content = DeLong.Resourses.Resource.Product; 
        myExitLabel.Content = DeLong.Resourses.Resource.Exit;
        myMijozLabel.Content = DeLong.Resourses.Resource.Client;
        myKirimLabel.Content = DeLong.Resourses.Resource.Income;
        myChiqimLabel.Content = DeLong.Resourses.Resource.Expense;
    }

}
