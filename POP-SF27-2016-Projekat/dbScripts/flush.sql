USE POP;

DROP TABLE Salon;
DROP TABLE Korisnik;
DROP TABLE TipKorisnika;
DROP TABLE DodatnaUsluga;
DROP TABLE NaAkciji;
DROP TABLE Akcija;
DROP TABLE Namestaj;
DROP TABLE TipNamestaja;

CREATE TABLE Salon (
	Id INT PRIMARY KEY IDENTITY(0, 1),
	Naziv VARCHAR(120),
	Adresa VARCHAR(120),
	Telefon VARCHAR(80),
	Email VARCHAR(80),
	AdresaSajta VARCHAR(80),
	PIB INT,
	MaticniBroj INT,
	ZiroRacun VARCHAR(80)
);

CREATE TABLE TipKorisnika (
	Id INT PRIMARY KEY IDENTITY(0, 1),
	Naziv VARCHAR(80),
	DozvolaAkcija TINYINT,
	DozvolaDodatnaUsluga TINYINT,
	DozvolaKorisnik TINYINT,
	DozvolaNamestaj TINYINT,
	DozvolaProdajaNamestaja TINYINT,
	DozvolaSalon TINYINT,
	DozvolaTipKorisnika TINYINT,
	DozvolaTipNamestaja TINYINT,
	Obrisan BIT
);

CREATE TABLE Korisnik (
	Id INT PRIMARY KEY IDENTITY(0, 1),
	TipKorisnikaId INT FOREIGN KEY REFERENCES TipKorisnika(Id),
	Ime VARCHAR(80),
	Prezime VARCHAR(80),
	KorisnickoIme VARCHAR(80),
	Lozinka VARCHAR(80),
	Obrisan BIT
);

CREATE TABLE TipNamestaja (
	Id INT PRIMARY KEY IDENTITY(0, 1),
	Naziv VARCHAR(80),
	Obrisan BIT
);

CREATE TABLE Namestaj (
	Id INT PRIMARY KEY IDENTITY(0, 1),
	TipNamestajaId INT FOREIGN KEY REFERENCES TipNamestaja(Id),
	Naziv VARCHAR(80),
	Sifra VARCHAR(80),
	JedinicnaCena NUMERIC(9,2),
	KolicinaUMagacinu INT,
	Obrisan BIT
);

CREATE TABLE DodatnaUsluga (
	Id INT PRIMARY KEY IDENTITY(0, 1),
	Naziv VARCHAR(80),
	Cena NUMERIC(9,2),
	Obrisan BIT
);

CREATE TABLE Akcija (
	Id INT PRIMARY KEY IDENTITY(0, 1),
	Naziv VARCHAR(80),
	DatumPocetka DATETIME2,
	DatumKraja DATETIME2,
	Obrisan BIT
);

CREATE TABLE NaAkciji (
	IdAkcije INT FOREIGN KEY REFERENCES Akcija(Id),
	IdNamestaja INT FOREIGN KEY REFERENCES Namestaj(Id),
	Popust NUMERIC(9,2)
);



INSERT INTO Salon (Naziv, Adresa, Telefon, Email, AdresaSajta, PIB, MaticniBroj, ZiroRacun) 
VALUES('Forma FTNale', 'Trg FTN-a', '021/123123', 'kontakt@ftn.uns.ac.rs', 'www.ftn.com', 123, 231231, '840-00073555082-13');


INSERT INTO TipKorisnika (Naziv, DozvolaAkcija, DozvolaDodatnaUsluga, DozvolaKorisnik, DozvolaNamestaj, DozvolaProdajaNamestaja, DozvolaSalon, DozvolaTipKorisnika, DozvolaTipNamestaja, Obrisan) 
VALUES('Jadnik', 0, 0, 0, 0, 0, 0, 0, 0, 0);

INSERT INTO TipKorisnika (Naziv, DozvolaAkcija, DozvolaDodatnaUsluga, DozvolaKorisnik, DozvolaNamestaj, DozvolaProdajaNamestaja, DozvolaSalon, DozvolaTipKorisnika, DozvolaTipNamestaja, Obrisan) 
VALUES('Administrator', 15, 15, 15, 15, 12, 10, 15, 15, 0);

INSERT INTO TipKorisnika (Naziv, DozvolaAkcija, DozvolaDodatnaUsluga, DozvolaKorisnik, DozvolaNamestaj, DozvolaProdajaNamestaja, DozvolaSalon, DozvolaTipKorisnika, DozvolaTipNamestaja, Obrisan) 
VALUES('Prodavac', 8, 8, 8, 8, 12, 8, 8, 8, 0);


INSERT INTO Korisnik (TipKorisnikaId, Ime, Prezime, KorisnickoIme, Lozinka, Obrisan) 
VALUES(0, 'Jadnik', 'BezDozvola', 'jadnik', 'jadnik', 0);

INSERT INTO Korisnik (TipKorisnikaId, Ime, Prezime, KorisnickoIme, Lozinka, Obrisan) 
VALUES(1, 'Neko', 'Nekic', 'username1', 'password1', 0);

INSERT INTO Korisnik (TipKorisnikaId, Ime, Prezime, KorisnickoIme, Lozinka, Obrisan) 
VALUES(2, 'Pera', 'Peric', 'username2', 'password2', 0);

INSERT INTO Korisnik (TipKorisnikaId, Ime, Prezime, KorisnickoIme, Lozinka, Obrisan) 
VALUES(2, 'Mile', 'Kitic', 'mile', 'kitic123', 0);


INSERT INTO TipNamestaja (Naziv, Obrisan) 
VALUES('Regal', 0);

INSERT INTO TipNamestaja (Naziv, Obrisan) 
VALUES('Polica', 0);

INSERT INTO TipNamestaja (Naziv, Obrisan) 
VALUES('Sofa', 0);


INSERT INTO Namestaj (TipNamestajaId, Naziv, Sifra, JedinicnaCena, KolicinaUMagacinu, Obrisan) 
VALUES(0, 'Sofa1', 'sifra1', 11, 111, 0);

INSERT INTO Namestaj (TipNamestajaId, Naziv, Sifra, JedinicnaCena, KolicinaUMagacinu, Obrisan) 
VALUES(1, 'Sofa2', 'sifra2', 22, 222, 0);

INSERT INTO Namestaj (TipNamestajaId, Naziv, Sifra, JedinicnaCena, KolicinaUMagacinu, Obrisan) 
VALUES(2, 'Sofa5', 'sifra5', 55, 555, 0);


INSERT INTO DodatnaUsluga (Naziv, Cena, Obrisan) 
VALUES('DodatnaUsluga1', 150, 0);

INSERT INTO DodatnaUsluga (Naziv, Cena, Obrisan) 
VALUES('DodatnaUsluga2', 200, 0);

INSERT INTO DodatnaUsluga (Naziv, Cena, Obrisan) 
VALUES('DodatnaUsluga3', 350, 0);


INSERT INTO Akcija (Naziv, DatumPocetka, DatumKraja, Obrisan) 
VALUES('Akcija1', '2017-12-01T00:00:00', '2018-02-01T00:00:00', 0);

INSERT INTO Akcija (Naziv, DatumPocetka, DatumKraja, Obrisan) 
VALUES('Akcija2', '2018-01-01T00:00:00', '2018-03-01T00:00:00', 0);


INSERT INTO NaAkciji (IdAkcije, IdNamestaja, Popust) 
VALUES(0, 0, 20);

INSERT INTO NaAkciji (IdAkcije, IdNamestaja, Popust) 
VALUES(0, 1, 30);

INSERT INTO NaAkciji (IdAkcije, IdNamestaja, Popust) 
VALUES(1, 1, 40);

INSERT INTO NaAkciji (IdAkcije, IdNamestaja, Popust) 
VALUES(1, 2, 50);