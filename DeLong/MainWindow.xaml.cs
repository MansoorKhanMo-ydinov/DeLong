using System.Windows;
using System.Windows.Input;
using System.Windows.Navigation;

namespace DeLong
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
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
    }
}
