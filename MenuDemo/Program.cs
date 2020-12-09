using System;
using MenuDemoLibrary;
using System.Collections.Generic;
//Console.OutputEncoding = System.Text.Encoding.UTF8; //dollarihommat
// hello world
// hello hello

namespace MenuDemo
{
    class Program
    {
        public List<Ravintola> ravintolat;
        public static List<Annos> annoksia;
        public AnnosDataManager dm = new AnnosDataManager();        
        


        static void Main(string[] args)
        {
            Console.WriteLine("");
            var test = AnnosDataManager.GetTest();
            //Console.WriteLine(AnnosDataManager.GetAllDishes());
            var annos = KaikenDatanKäsittelijä.haeAnnosIdllä(1009);
            Annos.tulostaAnnoksenTiedot(annos);
            List<Annos> annoslista = new List<Annos>();
            annoslista = KaikenDatanKäsittelijä.haeKaikkiAnnokset();
            foreach (Annos haettava in annoslista)
            {
                Console.WriteLine($"{haettava.Nimi}, {haettava.Kuvaus}, {haettava.Hinta}, {haettava.Id}");
            }
            //  Päämenu.Valinnat();
            
            Ravintola haettu = KaikenDatanKäsittelijä.haeRavintolaIdllä(1);
            Console.WriteLine($"{haettu.RavintolanNimi}");
            List <Ruokalista> rlistat = KaikenDatanKäsittelijä.haeRuokalistatRavintolanIdllä(1);
            foreach (Ruokalista haettava in rlistat)
            {
                Console.WriteLine($"{haettava.Nimi}, {haettava.Id}");
            }
            List<Kategoria> kategoriat = KaikenDatanKäsittelijä.haeKategoriatRuokalistanIdllä(1);
            foreach (Kategoria kategoria in kategoriat)
            {
                Console.WriteLine($"{kategoria.Nimi}, {kategoria.Id}");
            }
            List<int> tunnukset = KaikenDatanKäsittelijä.haeAnnostenTunnuksetKategoriasta(2);
            foreach (int intti in tunnukset)
            {
                Console.WriteLine($"{intti}");
            }
            List <Annos> annokset = KaikenDatanKäsittelijä.HaeKaikkiAnnoksetKategoriasta(2);
            foreach (Annos hajettava in annokset)
            {
                Console.WriteLine($"{hajettava.Nimi}");
            }

            Ravintola kaikkiData = KaikenDatanKäsittelijä.haeKaikkiRavintolanDataIdllä(1);
            foreach (Ruokalista ruokalista in kaikkiData.Ruokalistat)
            {
                Console.WriteLine($"{ruokalista.Nimi}, {ruokalista.Kuvaus}");
                foreach (Kategoria kategoria in ruokalista.Kategoriat)
                {
                    Console.WriteLine($"{kategoria.Nimi}, {kategoria.Kuvaus}, {kategoria.Id}");
                    foreach (Annos etsitty in kategoria.Annoslista)
                    {
                        Annos.tulostaAnnoksenTiedot(etsitty);
                    }
                }
            }
            //Ravintola tietokantaanlisättävä = new Ravintola(2, "Special Goodness");
            //KaikenDatanKäsittelijä.lisääRavintolaTietokantaan(tietokantaanlisättävä);
            //Ruokalista sinnelisättävä = new Ruokalista(2, "Yömenu", "Keskellä yötä purtavaa", 2);
            //KaikenDatanKäsittelijä.lisääRuokalistaTietokantaan(tietokantaanlisättävä, sinnelisättävä);
            Päämenu.Valinnat();


        }        
    }
}

        