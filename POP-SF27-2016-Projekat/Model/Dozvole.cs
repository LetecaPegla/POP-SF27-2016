﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Serialization;

namespace POP_SF27_2016_Projekat.Model
{
    #region Enums
    [System.Flags]
    public enum Dozvola : byte
    {
        None = 0,
        Read = 1,
        Add = 1 << 1,
        Edit = 1 << 2,
        Delete = 1 << 3
    }
    #endregion

    public class Dozvole : INotifyPropertyChanged
    {
        private Dozvola akcija; // Dozvola za upravljanje akcijama
        private Dozvola dodatnaUsluga; // Dozvola za upravljanje dodatnim uslugama
        private Dozvola korisnik; // Dozvola za upravljanje korisnicima
        private Dozvola namestaj; // Dozvola za upravljanje namestajem
        private Dozvola prodajaNamestaja; // Dozvola za upravljanje prodajama
        private Dozvola salon; // Dozvola za upravljanje informacijama o salonu
        private Dozvola tipKorisnika; // Dozvola za upravljanjem tipovima korisnika
        private Dozvola tipNamestaja; // Dozvola za upravljanjem tipovima namestaja

        #region Properties
        public Dozvola Akcija
        {
            get
            {
                return akcija;
            }
            set
            {
                akcija = value;
                OnPropertyChanged("Akcija");
            }
        }
        public Dozvola DodatnaUsluga
        {
            get
            {
                return dodatnaUsluga;
            }
            set
            {
                dodatnaUsluga = value;
                OnPropertyChanged("DodatnaUsluga");
            }
        }
        public Dozvola Korisnik
        {
            get
            {
                return korisnik;
            }
            set
            {
                korisnik = value;
                OnPropertyChanged("Korisnik");
            }
        }
        public Dozvola Namestaj
        {
            get
            {
                return namestaj;
            }
            set
            {
                namestaj = value;
                OnPropertyChanged("Namestaj");
            }
        }
        public Dozvola ProdajaNamestaja
        {
            get
            {
                return prodajaNamestaja;
            }
            set
            {
                prodajaNamestaja = value;
                OnPropertyChanged("ProdajaNamestaja");
            }
        }
        #region SalonDozvole
        public Dozvola Salon
        {
            get
            {
                return salon;
            }
            set
            {
                salon = value;
                OnPropertyChanged("Salon");
            }
        }
        [XmlIgnore]
        public Visibility SalonRead
        {
            get
            {
                if((salon & Dozvola.Read) == Dozvola.Read)
                {
                    return Visibility.Visible;
                }
                else
                {
                    return Visibility.Hidden;
                }
            }
            set
            {
                if (value == Visibility.Visible)
                {
                    salon = salon | Dozvola.Read;
                }
                else
                {
                    salon = salon & ~Dozvola.Read;
                }
                OnPropertyChanged("SalonRead");
            }
        }
        [XmlIgnore]
        public Boolean SalonAdd
        {
            get
            {
                return (salon & Dozvola.Add) == Dozvola.Add;
            }
            set
            {
                if (value == true)
                {
                    salon = salon | Dozvola.Add;
                }
                else
                {
                    salon = salon & ~Dozvola.Add;
                }
                OnPropertyChanged("SalonAdd");
            }
        }
        [XmlIgnore]
        public Boolean SalonEdit
        {
            get
            {
                return (salon & Dozvola.Edit) == Dozvola.Edit;
            }
            set
            {
                if(value == true)
                {
                    salon = salon | Dozvola.Edit;
                }
                else
                {
                    salon = salon & ~Dozvola.Edit;
                }
                OnPropertyChanged("SalonEdit");
            }
        }
        [XmlIgnore]
        public Boolean SalonDelete
        {
            get
            {
                return (salon & Dozvola.Delete) == Dozvola.Delete;
            }
            set
            {
                if (value == true)
                {
                    salon = salon | Dozvola.Delete;
                }
                else
                {
                    salon = salon & ~Dozvola.Delete;
                }
                OnPropertyChanged("SalonDelete");
            }
        }
        #endregion
        public Dozvola TipKorisnika
        {
            get
            {
                return tipKorisnika;
            }
            set
            {
                tipKorisnika = value;
                OnPropertyChanged("TipKorisnika");
            }
        }
        public Dozvola TipNamestaja
        {
            get
            {
                return tipNamestaja;
            }
            set
            {
                tipNamestaja = value;
                OnPropertyChanged("TipNamestaja");
            }
        }
        #endregion

        #region Constructors
        public Dozvole()
        {
            this.Akcija = Dozvola.None;
            this.DodatnaUsluga = Dozvola.None;
            this.Korisnik = Dozvola.None;
            this.Namestaj = Dozvola.None;
            this.ProdajaNamestaja = Dozvola.None;
            this.Salon = Dozvola.None;
            this.TipKorisnika = Dozvola.None;
            this.TipNamestaja = Dozvola.None;
        }
        #endregion

        #region Methods
        public override string ToString()
        {
            return Akcija.ToString() + DodatnaUsluga.ToString()
                + Korisnik.ToString() + Namestaj.ToString()
                + TipNamestaja.ToString() + Salon.ToString()
                + TipKorisnika.ToString() + TipNamestaja.ToString();
        }
        #endregion

        #region DataBinding
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        #endregion
    }
}
