﻿using System.Windows;
using DeLong.DbContexts;
using DeLong.Entities.Warehouses;
using Microsoft.EntityFrameworkCore;

namespace DeLong.Windows.Users
{
    public partial class EditWarehouseWindow : Window
    {
        private readonly AppdbContext _dbContext; // AppDbContext obyektini qo'shish
        public Warehouse UpdatedWarehouse { get; private set; }
        private readonly Warehouse _originalWarehouse; // Asl foydalanuvchi obyektini saqlash

        public EditWarehouseWindow(AppdbContext dbContext, Warehouse warehouse)
        {
            EditWarehouseWindow.InitialComponent();

            _dbContext = dbContext;
            _originalWarehouse = warehouse;

            // Foydalanuvchi ma'lumotlarini formaga yuklash
            txtWarehouseID.Text = warehouse.;
            txtTelefon.Text = user.Telefon;
            txtAdres.Text = user.Adres;
            txtTelegramRaqam.Text = user.TelegramRaqam;
            txtINN.Text = user.INN.ToString();
            txtOKONX.Text = user.OKONX;
            txtXisobRaqam.Text = user.XisobRaqam.ToString();
            txtJSHSHIR.Text = user.JSHSHIR.ToString();
            txtBank.Text = user.Bank;
            txtFirmaAdres.Text = user.FirmaAdres;
        }

        private void EditUserButton_Click(object sender, RoutedEventArgs e)
        {
            // INN maydonini int formatida o'qish
            if (int.TryParse(txtINN.Text, out int innValue))
            {
                // Foydalanuvchini yangilash
                _originalUser.FIO = txtFIO.Text;
                _originalUser.Telefon = txtTelefon.Text;
                _originalUser.Adres = txtAdres.Text;
                _originalUser.TelegramRaqam = txtTelegramRaqam.Text;
                _originalUser.INN = innValue;
                _originalUser.OKONX = txtOKONX.Text;
                _originalUser.XisobRaqam = txtXisobRaqam.Text;
                _originalUser.JSHSHIR = txtJSHSHIR.Text;
                _originalUser.Bank = txtBank.Text;
                _originalUser.FirmaAdres = txtFirmaAdres.Text;

                try
                {
                    // Ma'lumotlar bazasiga o'zgarishlarni saqlash
                    _dbContext.Entry(_originalUser).State = EntityState.Modified;
                    _dbContext.SaveChanges();

                    MessageBox.Show("Foydalanuvchi muvaffaqiyatli yangilandi.", "Muvaffaqiyat", MessageBoxButton.OK, MessageBoxImage.Information);

                    this.DialogResult = true; // Oynani yopishdan oldin natijani ko'rsatish
                    this.Close(); // Oynani yopish
                }
                catch (DbUpdateException ex)
                {
                    MessageBox.Show($"Xatolik yuz berdi: {ex.Message}", "Xato", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Iltimos, INN maydoniga to'g'ri raqam kiriting!", "Xato", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
