using System.Windows;
using DeLong.DbContexts;
using DeLong.Entities.Informs;

namespace DeLong.Windows.Kirims
{
    public partial class AddKirimWindow : Window
    {
        // Kirim mahsulotlari ro'yxati
        private List<Inform> _kirimItems = new List<Inform>();

        public AddKirimWindow()
        {
            InitializeComponent();
            dgInform.ItemsSource = _kirimItems;
        }

        // DataGrid'ga yangi mahsulot qo'shish
        private void AddRowToDataGrid(object sender, RoutedEventArgs e)
        {
            // Kirish ma'lumotlarini olish
            string mahsulotNomi = OmborNomiComboBox.Text; // Bu joyni mahsulot nomiga mos ravishda o'zgartirish mumkin
            decimal sotilishNarxi;
            int mahsulotSoni;

            // Tekshiruv: maydonlarni to'ldirilganligini tekshirish
            if (string.IsNullOrEmpty(mahsulotNomi) ||
                !decimal.TryParse(JamiNarxiTextBox.Text, out sotilishNarxi) ||
                !int.TryParse(JamiSoniTextBox.Text, out mahsulotSoni))
            {
                MessageBox.Show("Iltimos, barcha maydonlarni to'g'ri to'ldiring.", "Xato", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Yangi mahsulotni qo'shish
            var newInform = new Inform
            {
                TovarNomi = mahsulotNomi,
                SotilishNarxi = sotilishNarxi,
                Soni = mahsulotSoni
            };

            _kirimItems.Add(newInform);
            dgInform.Items.Refresh();

            // Maydonlarni tozalash
            OmborNomiComboBox.SelectedIndex = -1;
            JamiNarxiTextBox.Clear();
            JamiSoniTextBox.Clear();
        }

        // Kirimni ma'lumotlar bazasiga saqlash
        private void AddKirimPageButton_Click(object sender, RoutedEventArgs e)
        {
            // Kirish ma'lumotlarini olish
            string yetkazuvchi = YetkazuvchiTextBox.Text;
            DateTime sana = SanaDatePicker.SelectedDate ?? DateTime.Now;

            if (string.IsNullOrEmpty(yetkazuvchi) || !_kirimItems.Any())
            {
                MessageBox.Show("Yetkazuvchi nomini kiriting va jadvalga ma'lumot qo'shing.", "Xato", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                using (var context = new AppdbContext())
                {
                    // Kirim mahsulotlarini bazaga qo'shish
                    foreach (var item in _kirimItems)
                    {
                        context.Informs.Add(new Inform
                        {
                            TovarNomi = item.TovarNomi,
                            SotilishNarxi = item.SotilishNarxi,
                            Soni = item.Soni,
                            KirishSummasi = item.Soni * item.SotilishNarxi // Jami kirim narxi
                        });
                    }
                    context.SaveChanges();
                }

                MessageBox.Show("Kirim muvaffaqiyatli saqlandi!", "Muvaffaqiyat", MessageBoxButton.OK, MessageBoxImage.Information);

                // Forma va jadvalni tozalash
                YetkazuvchiTextBox.Clear();
                SanaDatePicker.SelectedDate = null;
                _kirimItems.Clear();
                dgInform.Items.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Xatolik yuz berdi: {ex.Message}", "Xato", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
