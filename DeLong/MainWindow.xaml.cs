using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;

namespace DeLong
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            // Dastlabki sahifani yuklash (agar kerak bo'lsa)
            // Navigator.Navigate(new Pages.Clients.User()); 
        }

        // Exit Application when "Exit" section is clicked
        private void ExitApplication(object sender, MouseButtonEventArgs e)
        {
            if (MessageBox.Show("Ishonchingiz komilmi?", "Chiqish", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                Application.Current.Shutdown();
            }
        }

        // Navigation event (future use when clicking on different sections)
        private void Navigator_Navigated(object sender, NavigationEventArgs e)
        {
            // Navigatsiya qilganingizda, bu funksiya foydalaniladi.
            // Hozircha uni bo'sh qoldirishingiz mumkin yoki qo'shimcha sahifalar tayyorlaganingizda o'zgartirishingiz mumkin.
        }

        // Users button click event to navigate to the User page
        private void UsersButton_Click(object sender, MouseButtonEventArgs e)
        {
            // Foydalanuvchilar sahifasini yuklash
            Navigator.Navigate(new Pages.Clients.UserPage());
        }

        private void LanguageAPP(object sender, SelectionChangedEventArgs e)
        {
            if (languageComboBox.SelectedItem is ComboBoxItem selectedItem)
            {
                // Tanlangan til kodi orqali madaniyatni o'zgartirish
                string selectedLanguage = selectedItem.Tag.ToString();
                DeLong.Resourses.Resource.Culture = new CultureInfo(selectedLanguage);

                // Interfeys matnlarini yangilash
                UpdateLanguage();
            }
        }
        private void UpdateLanguage()
        {
            // Har bir elementdagi kontentni resurslardan qayta yuklash
            languageComboBox.Text = DeLong.Resourses.Resource.Language; // Misol: Til uchun matn
            myProductLabel.Content = DeLong.Resourses.Resource.Product; // Misol: Mahsulot uchun matn
            myExitLabel.Content = DeLong.Resourses.Resource.Exit;
            myMijozLabel.Content = DeLong.Resourses.Resource.Client;
            myKirimLabel.Content = DeLong.Resourses.Resource.Income;
            myChiqimLabel.Content = DeLong.Resourses.Resource.Expense;

        }

    }
}
