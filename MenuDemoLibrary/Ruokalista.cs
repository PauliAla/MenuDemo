using System;
using System.Collections.Generic;
using System.Text;

namespace MenuDemoLibrary
{
    public class Ruokalista
    {
        private string _nimi;
        private List<Kategoria> kategoriat = new List<Kategoria> ();
        static int ruokalistaidCount = 0;
        private string _kuvaus;
        private int ruokalistaid;
        private int id;
        private int _ravintolaId;

        public string Nimi { get => _nimi; set => _nimi = value; }
        public List<Kategoria> Kategoriat { get => kategoriat; set => kategoriat = value; }
        public int Ruokalistaid { get => ruokalistaid; set => ruokalistaid = value; }
        public int Id { get => id; set => id = value; }
        public string Kuvaus { get => _kuvaus; set => _kuvaus = value; }
        public int RavintolaId { get => _ravintolaId; set => _ravintolaId = value; }

        public Ruokalista (string nimi)
        {
            this.Nimi = nimi;
            
            ruokalistaidCount++;      // nostetaan idCounttia yhdellä
            this.Ruokalistaid = ruokalistaidCount;              // ja asetetaan se uuden luodun ruokalistan tunnukseksi
            this.Kategoriat = new List<Kategoria>();
            Kategoria alkuruoat = new Kategoria("Alkuruoat");
            Kategoria pääruoat = new Kategoria("Pääruoat");
            Kategoria jälkiruoat = new Kategoria("Jälkiruoat");
            Kategoria juomat = new Kategoria("Juomat");
            this.Kategoriat.Add(alkuruoat);
            this.Kategoriat.Add(pääruoat);
            this.Kategoriat.Add(jälkiruoat);
            this.Kategoriat.Add(juomat);
        }
        public Ruokalista(int id,string nimi,string kuvaus, int ravintolaId)
        {
            this.Id = id;
            this.Nimi = nimi;
            this.Kuvaus = kuvaus;
            this.RavintolaId = ravintolaId;
            
        }
        public static void tulostaKategorianNimet(Ravintola ravintola, int ruokalistantunnus)
        {
            foreach (Ruokalista ruokalista in ravintola.Ruokalistat)
            {
                if (ruokalista.Ruokalistaid == ruokalistantunnus)
                {
                    foreach (Kategoria kategoria in ruokalista.Kategoriat)
                    {
                        Console.WriteLine($"Tunnus: {kategoria.Id} , Nimi: {kategoria.Nimi} ");
                    }
                }
            }
        }
        public static void tulostaRuokalistanTiedot(Ravintola ravintola, int ruokalistaId)
        {

               Console.WriteLine($"  {ravintola.Ruokalistat[ruokalistaId-1].Id} ,  {ravintola.Ruokalistat[ruokalistaId - 1].Nimi} ,  {ravintola.Ruokalistat[ruokalistaId - 1].Kuvaus}, RavintolaId: {ravintola.Ruokalistat[ruokalistaId - 1].RavintolaId} ");
                    
                }
            }
        }
    
    
    

