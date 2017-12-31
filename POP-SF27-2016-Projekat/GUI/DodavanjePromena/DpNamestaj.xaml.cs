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
    public partial class DpNamestaj : Window
    {
        private enum Operacija
        {
            DODAVANJE,
            IZMENA
        }
        private Operacija operacija;

        Namestaj namestaj;

        public DpNamestaj()
        {
            InitializeComponent();
            tblock.Text = "Nov namestaj:";
            operacija = Operacija.DODAVANJE;

            namestaj = new Namestaj();

            InitFields();
        }

        public DpNamestaj(Namestaj namestajParam)
        {
            InitializeComponent();
            tblock.Text = "Izmena namestaja:";
            operacija = Operacija.IZMENA;

            namestaj = new Namestaj();
            namestaj.Copy(namestajParam);

            InitFields();
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            if (namestaj.Naziv != "")
            {
                if (namestaj.Sifra != "")
                {
                    double jedinicnaCena;
                    if (double.TryParse(tbJedinicnaCena.Text, out jedinicnaCena))
                    {
                        int kolicinaUMagacinu;
                        if (int.TryParse(tbKolicinaUMagacinu.Text, out kolicinaUMagacinu))
                        {
                            if (namestaj.TipNamestaja != null)
                            {
                                if (operacija == Operacija.DODAVANJE)
                                {
                                    Namestaj.Create(namestaj);
                                }
                                else if (operacija == Operacija.IZMENA)
                                {
                                    Namestaj.Update(namestaj);
                                }
                                Close();
                                return;
                            }
                            else
                            {
                                cbTip.Focus();
                            }
                        }
                        else
                        {
                            tbJedinicnaCena.Focus();
                        }
                    }
                    else
                    {
                        tbJedinicnaCena.Focus();
                    }
                }
                else
                {
                    tbSifra.Focus();
                }
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

        private void InitFields()
        {
            tbNaziv.DataContext = namestaj;
            tbSifra.DataContext = namestaj;
            tbJedinicnaCena.DataContext = namestaj;
            tbKolicinaUMagacinu.DataContext = namestaj;
            cbTip.DataContext = namestaj;
        }
    }
}
