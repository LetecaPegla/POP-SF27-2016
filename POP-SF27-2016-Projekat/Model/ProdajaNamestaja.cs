﻿using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Data;

namespace POP_SF27_2016_Projekat.Model
{
    #region Storage
    public class ProdajaNamestaja : INotifyPropertyChanged
    {
        #region Fields
        private int id;
        private ObservableCollection<UredjeniParRacunNamestaj> listProdatiNamestaji;
        private ObservableCollection<UredjeniParRacunDodatnaUsluga> listProdateDodatneUsluge;
        private DateTime? datumProdaje;
        private string kupac;
        private string brojRacuna;
        private double pdv;
        public static ObservableCollection<ProdajaNamestaja> prodajaNamestajaCollection;
        #endregion

        #region Properties
        public int Id
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
                OnPropertyChanged("Id");
            }
        }
        public ObservableCollection<UredjeniParRacunNamestaj> ListProdatiNamestaji
        {
            get
            {
                return listProdatiNamestaji;
            }
            set
            {
                listProdatiNamestaji = value;
                OnPropertyChanged("ListProdatiNamestaji");
            }
        }
        public ObservableCollection<UredjeniParRacunDodatnaUsluga> ListProdateDodatneUsluge
        {
            get
            {
                return listProdateDodatneUsluge;
            }
            set
            {
                listProdateDodatneUsluge = value;
                OnPropertyChanged("ListProdateDodatneUsluge");
            }
        }
        public DateTime? DatumProdaje
        {
            get
            {
                return datumProdaje;
            }
            set
            {
                datumProdaje = value;
                OnPropertyChanged("DatumProdaje");
            }
        }
        public string Kupac
        {
            get
            {
                return kupac;
            }
            set
            {
                kupac = value;
                OnPropertyChanged("Kupac");
            }
        }
        public string BrojRacuna
        {
            get
            {
                return brojRacuna;
            }
            set
            {
                brojRacuna = value;
                OnPropertyChanged("BrojRacuna");
            }
        }
        public double PDV
        {
            get
            {
                return pdv;
            }
            set
            {
                pdv = value;
                OnPropertyChanged("PDV");
            }
        }
        
        public double UkupnaCena
        {
            get
            {
                double cena = 0;
                foreach(UredjeniParRacunNamestaj par in ListProdatiNamestaji)
                {
                    cena += par.UkupnaCena;
                }

                foreach(UredjeniParRacunDodatnaUsluga par in ListProdateDodatneUsluge)
                {
                    cena += par.Cena;
                }

                return cena * (1 + PDV / 100);
            }
        }
        #endregion

        #region Constructors
        public ProdajaNamestaja()
        {
            this.ListProdatiNamestaji = new ObservableCollection<UredjeniParRacunNamestaj>();
            this.ListProdateDodatneUsluge = new ObservableCollection<UredjeniParRacunDodatnaUsluga>();
            this.DatumProdaje = DateTime.Now;
            this.Kupac = "";
            this.BrojRacuna = "";
            this.PDV = 20;
        }
        public ProdajaNamestaja(ObservableCollection<UredjeniParRacunNamestaj> listProdatiNamestaji, ObservableCollection<UredjeniParRacunDodatnaUsluga> listProdateDodatneUsluge, DateTime? datumProdaje, string kupac, string brojRacuna, double pdv)
        {
            this.Id = prodajaNamestajaCollection.Count;
            this.ListProdatiNamestaji = listProdatiNamestaji;
            this.ListProdateDodatneUsluge = listProdateDodatneUsluge;
            this.DatumProdaje = datumProdaje;
            this.BrojRacuna = brojRacuna;
            this.Kupac = kupac;
            this.PDV = pdv;
        }
        #endregion

        #region Methods
        public static void Init()
        {
            prodajaNamestajaCollection = GetAll();
        }

        public static ProdajaNamestaja GetById(int id)
        {
            foreach (ProdajaNamestaja prodaja in prodajaNamestajaCollection)
            {
                if (prodaja.Id == id)
                {
                    return prodaja;
                }
            }
            return null;
        }

        public static void Add(ProdajaNamestaja prodajaNamestajaToAdd)
        {
            /* Kada predjemo na rad sa bazom podataka ovde se nece ucitavati 
             * cela lista vec ce se samo slati komanda za dodavanje novog. */
            if(prodajaNamestajaToAdd == null)
            {
                return;
            }
            prodajaNamestajaCollection.Add(prodajaNamestajaToAdd);
        }

        public override string ToString()
        { 
            return $"{Id}, {DatumProdaje}, {Kupac}, {BrojRacuna}";
        }
        #endregion

        #region Data binding
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        #endregion

        #region DAO
        public static ObservableCollection<ProdajaNamestaja> GetAll()
        {
            ObservableCollection<ProdajaNamestaja> prodaje = new ObservableCollection<ProdajaNamestaja>();

            using (SqlConnection con = new SqlConnection(Properties.Resources.connectionString))
            {
                con.Open();

                SqlCommand cmd = con.CreateCommand();
                SqlDataAdapter da = new SqlDataAdapter();
                DataSet ds = new DataSet();

                cmd.CommandText = "SELECT * FROM Prodaja;";
                da.SelectCommand = cmd;
                da.Fill(ds, "Prodaja");

                foreach (DataRow row in ds.Tables["Prodaja"].Rows)
                {
                    ProdajaNamestaja prodaja = new ProdajaNamestaja()
                    {
                        Id = Convert.ToInt32(row["Id"]),
                        DatumProdaje = Convert.ToDateTime(row["DatumProdaje"].ToString()),
                        Kupac = row["Kupac"].ToString(), 
                        BrojRacuna = row["BrojRacuna"].ToString(),
                        PDV = Convert.ToDouble(row["PDV"])
                    };

                    SqlCommand cmd2 = con.CreateCommand();
                    cmd2.CommandText = "SELECT * FROM ProdajaNamestaj WHERE IdProdaje=" + prodaja.Id + ";";
                    da.SelectCommand = cmd2;
                    da.Fill(ds, "ProdajaNamestaj");

                    foreach (DataRow row2 in ds.Tables["ProdajaNamestaj"].Rows)
                    {
                        UredjeniParRacunNamestaj par = new UredjeniParRacunNamestaj()
                        {
                            NazivNamestaja = row2["NazivNamestaja"].ToString(),
                            JedinicnaCena = Convert.ToDouble(row2["JedinicnaCena"]),
                            BrojNamestaja = Convert.ToInt32(row2["BrojNamestaja"]),
                            Popust = Convert.ToDouble(row2["Popust"])
                        };
                        prodaja.listProdatiNamestaji.Add(par);
                    }
                    ds.Tables["ProdajaNamestaj"].Clear();


                    cmd2.CommandText = "SELECT * FROM ProdajaUsluga WHERE IdProdaje=" + prodaja.Id + ";";
                    da.SelectCommand = cmd2;
                    da.Fill(ds, "ProdajaUsluga");

                    foreach (DataRow row2 in ds.Tables["ProdajaUsluga"].Rows)
                    {
                        UredjeniParRacunDodatnaUsluga par = new UredjeniParRacunDodatnaUsluga()
                        {
                            NazivUsluge = row2["NazivUsluge"].ToString(),
                            Cena = Convert.ToDouble(row2["Cena"])
                        };
                        prodaja.listProdateDodatneUsluge.Add(par);
                    }
                    ds.Tables["ProdajaUsluga"].Clear();

                    prodaje.Add(prodaja);
                }
            }
            return prodaje;
        }
        
        public static void Create(ProdajaNamestaja prodaja)
        {
            using (var con = new SqlConnection(Properties.Resources.connectionString))
            {
                con.Open();

                SqlCommand cmd = con.CreateCommand();

                cmd.CommandText = "INSERT INTO Prodaja (DatumProdaje, Kupac, BrojRacuna, PDV) VALUES (@DatumProdaje, @Kupac, @BrojRacuna, @PDV);";
                cmd.CommandText += "SELECT SCOPE_IDENTITY();";
                cmd.Parameters.AddWithValue("DatumProdaje", prodaja.DatumProdaje);
                cmd.Parameters.AddWithValue("Kupac", prodaja.Kupac);
                cmd.Parameters.AddWithValue("BrojRacuna", prodaja.BrojRacuna);
                cmd.Parameters.AddWithValue("PDV", prodaja.PDV);

                prodaja.Id = int.Parse(cmd.ExecuteScalar().ToString());

                foreach (UredjeniParRacunNamestaj par in prodaja.listProdatiNamestaji)
                {
                    cmd.Parameters.Clear();
                    cmd.CommandText = "INSERT INTO ProdajaNamestaj (IdProdaje, NazivNamestaja, JedinicnaCena, BrojNamestaja, Popust) VALUES (" + prodaja.Id + ", @NazivNamestaja, " + par.JedinicnaCena + ", " + par.BrojNamestaja + ", " + par.Popust + ");";
                    cmd.Parameters.AddWithValue("NazivNamestaja", par.NazivNamestaja);
                    cmd.ExecuteNonQuery();
                }

                foreach (UredjeniParRacunDodatnaUsluga par in prodaja.listProdateDodatneUsluge)
                {
                    cmd.Parameters.Clear();
                    cmd.CommandText = "INSERT INTO ProdajaUsluga (IdProdaje, NazivUsluge, Cena) VALUES (" + prodaja.Id + ", @NazivUsluge, " + par.Cena + ");";
                    cmd.Parameters.AddWithValue("NazivUsluge", par.NazivUsluge);
                    cmd.ExecuteNonQuery();
                }
            }

            ProdajaNamestaja.Add(prodaja);
        }
        #endregion
    }

    public class UredjeniParRacunNamestaj : INotifyPropertyChanged
    {
        #region Fields
        private string nazivNamestaja;
        private double jedinicnaCena;
        private int brojNamestaja;
        private double popust;
        #endregion

        #region Property
        public string NazivNamestaja
        {
            get
            {
                return nazivNamestaja;
            }
            set
            {
                this.nazivNamestaja = value;
                OnPropertyChanged("NazivNamestaja");
            }
        }
        public double JedinicnaCena
        {
            get
            {
                return jedinicnaCena;
            }
            set
            {
                this.jedinicnaCena = value;
                OnPropertyChanged("JedinicnaCena");
            }
        }
        public int BrojNamestaja
        {
            get
            {
                return brojNamestaja;
            }
            set
            {
                brojNamestaja = value;
                OnPropertyChanged("BrojNamestaja");
            }
        }
        public double Popust
        {
            get
            {
                return popust;
            }
            set
            {
                this.popust = value;
                OnPropertyChanged("Popust");
            }
        }
        public double UkupnaCena
        {
            get
            {
                return JedinicnaCena * BrojNamestaja * (1 - Popust/100);
            }
        }
        #endregion

        #region Constructors
        public UredjeniParRacunNamestaj()
        {
            //brojNamestaja = 0;
        }
        public UredjeniParRacunNamestaj(string nazivNamestaja, double jedinicnaCena, int brojNamestaja, double popust)
        {
            this.NazivNamestaja = nazivNamestaja;
            this.JedinicnaCena = jedinicnaCena;
            this.BrojNamestaja = brojNamestaja;
            this.Popust = popust;
        }
        #endregion

        #region Methods
        public void Copy(UredjeniParRacunNamestaj source)
        {
            this.NazivNamestaja = source.NazivNamestaja;
            this.JedinicnaCena = source.JedinicnaCena;
            this.BrojNamestaja = source.BrojNamestaja;
            this.Popust = source.Popust;
        }

        public override string ToString()
        {
            return $"{NazivNamestaja}, {BrojNamestaja}";
        }
        #endregion

        #region Data binding
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        #endregion
    }

    public class UredjeniParRacunDodatnaUsluga : INotifyPropertyChanged
    {
        #region Fields
        private string nazivUsluge;
        private double cena;
        #endregion

        #region Property
        public string NazivUsluge
        {
            get
            {
                return nazivUsluge;
            }
            set
            {
                this.nazivUsluge = value;
                OnPropertyChanged("NazivUsluge");
            }
        }
        public double Cena
        {
            get
            {
                return cena;
            }
            set
            {
                this.cena = value;
                OnPropertyChanged("Cena");
            }
        }
        #endregion

        #region Constructors
        public UredjeniParRacunDodatnaUsluga()
        {

        }
        public UredjeniParRacunDodatnaUsluga(string nazivUsluge, double cena)
        {
            this.NazivUsluge = nazivUsluge;
            this.Cena = cena;
        }
        #endregion

        #region Methods
        public void Copy(UredjeniParRacunDodatnaUsluga source)
        {
            this.NazivUsluge = source.NazivUsluge;
            this.Cena = source.Cena;
        }
        #endregion

        #region Data binding
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        #endregion
    }
    #endregion

    #region Runtime
    public class ProdajaNamestajaRuntime : INotifyPropertyChanged
    {
        #region Fields
        private int id;
        public ObservableCollection<UredjeniParRacun> listUredjeniPar;
        private DateTime? datumProdaje;
        private string kupac;
        private string brojRacuna;
        public ObservableCollection<DodatnaUsluga> listDodatnaUsluga;
        public double pdv = 20.0;
        #endregion

        #region Properties
        public int Id
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
                OnPropertyChanged("Id");
            }
        }
        public ObservableCollection<UredjeniParRacun> ListUredjeniPar
        {
            get
            {
                return listUredjeniPar;
            }
            set
            {
                listUredjeniPar = value;
                OnPropertyChanged("ListUredjeniPar");
                OnPropertyChanged("UkupnaCena");
            }
        }
        public DateTime? DatumProdaje
        {
            get
            {
                return datumProdaje;
            }
            set
            {
                datumProdaje = value;
                OnPropertyChanged("DatumProdaje");
            }
        }
        public string Kupac
        {
            get
            {
                return kupac;
            }
            set
            {
                kupac = value;
                OnPropertyChanged("Kupac");
            }
        }
        public string BrojRacuna
        {
            get
            {
                return brojRacuna;
            }
            set
            {
                brojRacuna = value;
                OnPropertyChanged("BrojRacuna");
            }
        }
        public ObservableCollection<DodatnaUsluga> ListDodatnaUsluga
        {
            get
            {
                return listDodatnaUsluga;
            }
            set
            {
                this.listDodatnaUsluga = value;
                OnPropertyChanged("ListDodatnaUsluga");
                OnPropertyChanged("UkupnaCena");
            }
        }
        public double PDV
        {
            get
            {
                return pdv;
            }
            set
            {
                pdv = value;
                OnPropertyChanged("PDV");
            }
        }
        public double UkupnaCena
        {
            get
            {
                double cena = 0;
                foreach (DodatnaUsluga dodatnaUsluga in ListDodatnaUsluga)
                {
                    cena += dodatnaUsluga.Cena;
                }
                foreach (UredjeniParRacun uredjeniPar in ListUredjeniPar)
                {
                    cena += uredjeniPar.UkupnaCena;
                }
                return cena * (100 + PDV) / 100;
            }
        }
        #endregion

        #region Constructors
        public ProdajaNamestajaRuntime()
        {
            this.ListUredjeniPar = new ObservableCollection<UredjeniParRacun>();
            this.ListDodatnaUsluga = new ObservableCollection<DodatnaUsluga>();
            this.DatumProdaje = DateTime.Now;
            this.Kupac = "";
            this.BrojRacuna = "";
            this.PDV = 20;
        }

        #endregion

        #region Methods
        public void AddNamestajPar(UredjeniParRacun par)
        {
            if(par != null)
            {
                ListUredjeniPar.Add(par);
                OnPropertyChanged("UkupnaCena");
            }
        }
        public void EditNamestajPar(UredjeniParRacun source, UredjeniParRacun target)
        {
            if(source != null && target != null)
            {
                target.Copy(source);
                OnPropertyChanged("UkupnaCena");
            }
        }
        public void DeleteNamestajPar(UredjeniParRacun par)
        {
            if(par != null)
            {
                ListUredjeniPar.Remove(par);
                OnPropertyChanged("UkupnaCena");
            }
        }
        public void AddDodatnaUsluga(DodatnaUsluga usluga)
        {
            if(usluga != null)
            {
                ListDodatnaUsluga.Add(usluga);
                OnPropertyChanged("UkupnaCena");
            }
        }
        public void DeleteDodatnaUsluga(DodatnaUsluga usluga)
        {
            if(usluga != null)
            {
                ListDodatnaUsluga.Remove(usluga);
                OnPropertyChanged("UkupnaCena");
            }
        }

        public override string ToString()
        {
            return $"{Id}, {DatumProdaje}, {Kupac}, {BrojRacuna}";
        }
        #endregion

        #region Data binding
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        #endregion
    }

    public class UredjeniParRacun : INotifyPropertyChanged
    {
        #region Fields
        private int namestajId;
        private Namestaj namestaj;
        private int brojNamestaja;
        #endregion

        #region Property
        public int NamestajId
        {
            get
            {
                return namestajId;
            }
            set
            {
                namestajId = value;
                OnPropertyChanged("NamestajId");
                OnPropertyChanged("Popust");
            }
        }
        
        public Namestaj Namestaj
        {
            get
            {
                return Namestaj.GetById(namestajId);
            }
            set
            {
                namestaj = value;
                NamestajId = namestaj.Id;
                OnPropertyChanged("Namestaj");
                OnPropertyChanged("Popust");
                /* Ovo dole ce svejedno biti pozvano kad se setuje BrojNamestaja jer se uvek oba propertija setuju u paru */
                //OnPropertyChanged("Cena");
                //OnPropertyChanged("UkupnaCena");
            }
        }
        public int BrojNamestaja
        {
            get
            {
                return brojNamestaja;
            }
            set
            {
                brojNamestaja = value;
                OnPropertyChanged("BrojNamestaja");
                OnPropertyChanged("Cena");
                OnPropertyChanged("UkupnaCena");
            }
        }
        
        public double Cena
        {
            get
            {
                return Namestaj.JedinicnaCena * BrojNamestaja;
            }
        }
        
        public double UkupnaCena
        {
            get
            {
                return Namestaj.JedinicnaCena * BrojNamestaja * (1.0 - (Akcija.GetPopustByNamestaj(Namestaj)) / 100);
            }
        }
        
        public double Popust
        {
            get
            {
                return Akcija.GetPopustByNamestaj(Namestaj);
            }
        }

        #endregion

        #region Constructors
        public UredjeniParRacun()
        {
        }
        public UredjeniParRacun(int namestajId, int brojNamestaja)
        {
            this.NamestajId = namestajId;
            this.BrojNamestaja = brojNamestaja;
        }
        #endregion

        #region Methods
        public void Copy(UredjeniParRacun source)
        {
            this.Namestaj = source.Namestaj;
            this.BrojNamestaja = source.BrojNamestaja;
        }

        public override string ToString()
        {
            return $"{NamestajId}, {BrojNamestaja}";
        }
        #endregion

        #region Data binding
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        #endregion
    }
    #endregion
}
