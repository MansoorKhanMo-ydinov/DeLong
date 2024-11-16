using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using DeLong.Entities.Incomes;
using DeLong.Entities.Informs;
using DeLong.Windows.Incomes;

namespace DeLong.Pages.Incomes
{
    public partial class KirimPage : Page
    {
        public ObservableCollection<Kirim> KirimList { get; set; }
        public ObservableCollection<Inform> InformList{get;set;}


        public KirimPage()
        {
            InitializeComponent();
            DataContext = this;

            // Ma'lumotlarni yaratish
            KirimList = new ObservableCollection<Kirim>();

            new Kirim
            {
                Id = 1,
                Ombornomi = "Ombor1",
                Yetkazuvchi = "Yetkazuvchi1",
                Jaminarxi = 1000,
                JamiSoni = 10,
                Role = new()
                {
                    Name = "Admin",
                    Id = 1,
                    Password = "Admin",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                Inform = new List<Inform>()
            };
            // Kirimlarni DataGridga bog'lash
            kirimDataGrid.ItemsSource = KirimList;
        }

        private void ShowDetails_Click(object sender, RoutedEventArgs e)
        {
            // 'Batafsil' tugmasi bosilganda, detalni ko'rsatish
            var button = sender as Button;
            var kirim = button?.DataContext as Kirim;

            if (kirim != null)
            {
                DetailDataGrid.ItemsSource = kirim.Inform;
                DetailDataGrid.Visibility = Visibility.Visible;  // Ko'rsatish
            }
        }

        private void Add_Button(object sender, RoutedEventArgs e)
        {
            AddKirimWindow addKirimWindow= new AddKirimWindow();
            addKirimWindow.ShowDialog();
        }
    }
}
