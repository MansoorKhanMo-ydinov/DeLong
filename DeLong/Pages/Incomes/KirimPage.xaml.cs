using System.Windows;
using System.Windows.Controls;
using DeLong.Entities.Incomes;

namespace DeLong.Pages.Incomes
{
    /// <summary>
    /// Interaction logic for KirimPage.xaml
    /// </summary>
    public partial class KirimPage : Page
    {
        public KirimPage()
        {
            InitializeComponent();
        }

        private void ShowDetails_Click(object sender, RoutedEventArgs e)
        {
            // Show the detailed products inside Inform
            DetailDataGrid.Visibility = Visibility.Visible;

            // Populate DetailDataGrid with the InformDetails of the selected KirimEntry
            var selectedItem = kirimDataGrid.SelectedItem as Kirim;
            if (selectedItem != null)
            {
                DetailDataGrid.ItemsSource = selectedItem.Inform;
            }
        }

        private void CalculateTotals()
        {
            // Calculate and set total values based on InformDetails in each KirimEntry
            foreach (var entry in kirimDataGrid.ItemsSource as List<Kirim>)
            {
                entry.Jaminarxi = entry.Inform.Sum(x => x.SotibOlishNarxi).CompareTo(int.Parse(Name));
                entry.JamiSoni = entry.Inform.Sum(x => x.Soni);
            }
        }


    }
}
