﻿using POP_SF27_2016_Projekat.Model;
using System.Windows;


namespace POP_SF27_2016_Projekat.GUI
{
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();

            tbWelcome.Text = "Welcome to\n" + Salon.SalonProperty.Naziv;
            tbUsername.Focus();
            tbUsername.Text = "username1";
            pbPassword.Password = "password1";
            this.KeyDown += LoginEnterKeyDown; // Izvrsi metod na okidanje KeyDown eventa
        }

        /* Kada pritisnemo Enter na tastaturi simuliramo pritiskanje Login dugmeta */
        private void LoginEnterKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if(e.Key == System.Windows.Input.Key.Enter)
            {
                btnLogin_Click(this, null);
            }
        }

        /* Na pritiskanje Login dugmeta */
        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            Korisnik.Login(tbUsername.Text, pbPassword.Password);
            if(Korisnik.Trenutni != null)
            {
                this.tbUsername.Text = "";
                this.pbPassword.Password = "";
                this.Hide(); // Sakrijemo Login prozor dok je glavni otvoren
                MainWindow mainProzor = new MainWindow();
                mainProzor.ShowDialog(); // Cekamo da se zatvori mainProzor
                Korisnik.Logout(); // Za svaki slucaj kad god se vratimo u ovaj prozor izlogovati korisnika
                this.Show(); // Prikazemo opet login prozor koji bi trebao da bude ociscen
            }
        }
    }
}
