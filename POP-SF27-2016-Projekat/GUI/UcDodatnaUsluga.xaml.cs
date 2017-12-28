﻿using POP_SF27_2016_Projekat.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;
using System.ComponentModel;
using POP_SF27_2016_Projekat.GUI.DodavanjePromena;

namespace POP_SF27_2016_Projekat.GUI
{
    public partial class UcDodatnaUsluga : UserControl
    {
        ICollectionView view;

        public UcDodatnaUsluga()
        {
            view = CollectionViewSource.GetDefaultView(DodatnaUsluga.dodatnaUslugaCollection);
            InitializeComponent();

            view.Filter = Filter;
            dgDodatnaUsluga.ItemsSource = view;

            btnAdd.DataContext = Korisnik.Trenutni.TipKorisnika.Dozvole;
            btnEdit.DataContext = Korisnik.Trenutni.TipKorisnika.Dozvole;
            btnDelete.DataContext = Korisnik.Trenutni.TipKorisnika.Dozvole;
        }

        #region Filters
        private bool Filter(object obj)
        {
            if (((DodatnaUsluga)obj).Obrisan == false)
            {
                var text = ((ComboBoxItem)cbPretraga.SelectedItem).Content.ToString();
                if (text.Equals("Id"))
                {
                    return ((DodatnaUsluga)obj).Id.ToString().IndexOf(tbPretraga.Text, StringComparison.OrdinalIgnoreCase) >= 0;
                }
                else if (text.Equals("Naziv"))
                {
                    return ((DodatnaUsluga)obj).Naziv.IndexOf(tbPretraga.Text, StringComparison.OrdinalIgnoreCase) >= 0;
                }
                return false;
            }
            return false;
        }
        #endregion

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            DpDodatnaUsluga dpDodatnaUsluga = new DpDodatnaUsluga();
            dpDodatnaUsluga.ShowDialog(); // Cekamo da se zatvori prozor za dodavanje
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (dgDodatnaUsluga.SelectedItem != null)
            {
                DpDodatnaUsluga dpDodatnaUsluga = new DpDodatnaUsluga((DodatnaUsluga)dgDodatnaUsluga.SelectedItem);
                dpDodatnaUsluga.ShowDialog(); // Cekamo da se zatvori prozor za menjanje
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (dgDodatnaUsluga.SelectedItem != null)
            {
                DodatnaUsluga.Remove((DodatnaUsluga)dgDodatnaUsluga.SelectedItem);
                view.Refresh();
            }
        }

        private void cbPretraga_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            view.Refresh();
        }

        private void tbPretraga_TextChanged(object sender, TextChangedEventArgs e)
        {
            view.Refresh();
        }
    }
}
