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

    private string _currentLanguage = "en"; // Tanlangan tilni saqlash uchun o'zgaruvchi

    // Til o'zgarganda tanlangan tilni qo'llab matnlarni yangilash
    private void LanguageAPP(object sender, SelectionChangedEventArgs e)
    {
        if (languageComboBox.SelectedItem is ComboBoxItem selectedItem)
        {
            string selectedLanguage = selectedItem.Tag.ToString();

            if (_currentLanguage != selectedLanguage) // Faqat til o'zgarishida yangilash
            {
                _currentLanguage = selectedLanguage;
                DeLong.Resourses.Resource.Culture = new CultureInfo(selectedLanguage);
                UpdateLanguage(); // Matnlarni yangilash
            }
        }
    }

    private void UpdateLanguage()
    {
        // Agar UserPage hali yaratilmagan bo'lsa, uni yarating
        if (_userPage == null)
        {
            _userPage = new UserPage();
        }

        // UserPage ichidagi elementlarning matnlarini yangilash
        _userPage.userDataGrid.Columns[0].Header = DeLong.Resourses.Resource.FIO;
        _userPage.userDataGrid.Columns[1].Header = DeLong.Resourses.Resource.Telefon;
        _userPage.userDataGrid.Columns[2].Header = DeLong.Resourses.Resource.Adres_;
        _userPage.userDataGrid.Columns[3].Header = DeLong.Resourses.Resource.Telegram_raqam;
        _userPage.userDataGrid.Columns[4].Header = DeLong.Resourses.Resource.INN;
        _userPage.userDataGrid.Columns[5].Header = DeLong.Resourses.Resource.OKONX;
        _userPage.userDataGrid.Columns[6].Header = DeLong.Resourses.Resource.Xisob_raqam;
        _userPage.userDataGrid.Columns[7].Header = DeLong.Resourses.Resource.JSHSHIR_;
        _userPage.userDataGrid.Columns[8].Header = DeLong.Resourses.Resource.Bank;
        _userPage.userDataGrid.Columns[9].Header = DeLong.Resourses.Resource.Firma_Adres;
        _userPage.userDataGrid.Columns[10].Header = DeLong.Resourses.Resource.Amallar;

        // UserPage dagi bosh elementlarni yangilash
        _userPage.MySearch.Content = DeLong.Resourses.Resource.Search;
        _userPage.AddButton1.Content = DeLong.Resourses.Resource.Add;

        // MainWindow elementlari uchun matnlarni yangilash
        languageComboBox.Text = DeLong.Resourses.Resource.Language;
        myProductLabel.Content = DeLong.Resourses.Resource.Product;
        myExitLabel.Content = DeLong.Resourses.Resource.Exit;
        myMijozLabel.Content = DeLong.Resourses.Resource.Client;
        myKirimLabel.Content = DeLong.Resourses.Resource.Income;
        myChiqimLabel.Content = DeLong.Resourses.Resource.Expense;

        // Sahifani o'zgartirish
        //Navigator.Navigate(_userPage);
    }


    private void Product_Button_Click(object sender, MouseButtonEventArgs e)
    {
        Navigator.Navigate(new Pages.Products.ProductPage());
    }

    private void UsersButton_Click(object sender, MouseButtonEventArgs e)
    {
        // UserPage sahifasini yaratish yoki ochish
        if (_userPage == null)
        {
            _userPage = new UserPage(); // UserPage faqat bir marta yaratiladi
        }

        // Tanlangan tilni qo'llash
        DeLong.Resourses.Resource.Culture = new CultureInfo(_currentLanguage); // Tanlangan tilni qo'llash
        UpdateLanguage(); // Matnlarni yangilash

        // UserPage sahifasiga o'tish
        Navigator.Navigate(_userPage);
    }

}
