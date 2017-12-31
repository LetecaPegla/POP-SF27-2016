﻿using POP_SF27_2016_Projekat.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace POP_SF27_2016_Projekat.GUI.DodavanjePromena
{
    public partial class DpTipNamestaja : Window
    {
        private enum Operacija
        {
            DODAVANJE,
            IZMENA
        }
        private Operacija operacija;

        private TipNamestaja tipNamestaja;

        public DpTipNamestaja()
        {
            InitializeComponent();
            tblock.Text = "Nov tip namestaja:";
            operacija = Operacija.DODAVANJE;

            tipNamestaja = new TipNamestaja();

            tbNaziv.DataContext = tipNamestaja;
        }

        public DpTipNamestaja(TipNamestaja tipNamestajaParam)
        {
            InitializeComponent();
            tblock.Text = "Izmena namestaja:";
            operacija = Operacija.IZMENA;

            tipNamestaja = new TipNamestaja();
            tipNamestaja.Copy(tipNamestajaParam);

            tbNaziv.DataContext = tipNamestaja;
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            if (tipNamestaja.Naziv != "")
            {
                if (operacija == Operacija.DODAVANJE)
                {
                    TipNamestaja.Create(tipNamestaja);
                }
                else if (operacija == Operacija.IZMENA)
                {
                    TipNamestaja.Update(tipNamestaja);
                }
                Close();
            }
            else
            {
                tbNaziv.Focus();
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
