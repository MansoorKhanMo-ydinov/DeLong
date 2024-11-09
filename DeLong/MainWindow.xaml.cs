using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;

namespace DeLong;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
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
        languageComboBox.Text = DeLong.Resourses.Resource.Language; 
        myProductLabel.Content = DeLong.Resourses.Resource.Product; 
        myExitLabel.Content = DeLong.Resourses.Resource.Exit;
        myMijozLabel.Content = DeLong.Resourses.Resource.Client;
        myKirimLabel.Content = DeLong.Resourses.Resource.Income;
        myChiqimLabel.Content = DeLong.Resourses.Resource.Expense;
    }

}
