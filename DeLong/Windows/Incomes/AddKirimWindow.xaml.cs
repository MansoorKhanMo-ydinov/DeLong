using System.Windows;
using DeLong.DbContexts;
using DeLong.Entities.Incomes;
using DeLong.Entities.Informs;

namespace DeLong.Windows.Incomes
{
    public partial class AddKirimWindow : Window
    {
        private readonly AppdbContext _context;

        public AddKirimWindow()
        {
            InitializeComponent();
            _context = new AppdbContext();
        }

        private void AddTovarButton_Click(object sender, RoutedEventArgs e)
        {
            // Jadvalga yangi mahsulot qo'shish
            var newTovar = new Inform
            {
                Id=1,
                TovarNomi = "Yangi Tovar",
                Soni = 0,
                SotibOlishNarxi = 0,
                KirimSummasi = 0,
                Foizi = 0,
                SotishNarxi = 0,
                SotishSummasi = 0
            };
            InformDataGrid.Items.Add(newTovar);
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            // Ma'lumotlarni Kirimga saqlash
            var kirim = new Kirim
            {
                Ombornomi = OmborNomiTextBox.Text,
                Yetkazuvchi = YetkazuvchiTextBox.Text,
                Inform = InformDataGrid.Items.OfType<Inform>().ToList(),
                Role = _context.Roles.FirstOrDefault(r => r.Name == "Admin") // Adminni olish
            };

            // Hisob-kitoblar
            kirim.Jaminarxi = kirim.Inform.Sum(i => i.Soni*i.SotishSummasi);
            kirim.JamiSoni = kirim.Inform.Sum(i => i.Soni);

            // Databazaga saqlash
            _context.Kirims.Add(kirim);
            _context.SaveChanges();

            MessageBox.Show("Kirim muvaffaqiyatli saqlandi!");
        }
    }
}
