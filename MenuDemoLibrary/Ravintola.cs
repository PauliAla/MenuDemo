using System;
using System.Collections.Generic;
using System.Text;

namespace MenuDemoLibrary
{
    public class Ravintola
    {
        private string _ravintolanNimi;
        public List <Ruokalista> ruokalistat= new List<Ruokalista>();
        static int idCount = 0;
        private int id;

        public Ravintola (string nimi)
        {
            this._ravintolanNimi = nimi;
            //idCount++;      // nostetaan idCounttia yhdellä
            idCount = KaikenDatanKäsittelijä._ravintolat.Count+1;
            this.id = idCount;              // ja asetetaan se uuden luodun ravintolan tunnukseksi
            this.Ruokalistat = new List<Ruokalista>();
        }

 
        public Ravintola(int id, string nimi)
        {
            this._ravintolanNimi = nimi;
            //idCount++;      // nostetaan idCounttia yhdellä
            this.id = id;              // ja asetetaan se uuden luodun ravintolan tunnukseksi
 
        }

        public string RavintolanNimi { get => _ravintolanNimi; set => _ravintolanNimi = value; }
        public List<Ruokalista> Ruokalistat { get => ruokalistat; set => ruokalistat = value; }

        public int Id { get => id; set => id = value; }

        public static void tulostaRuokalistat(Ravintola ravintola)
        {
            foreach (Ruokalista ruokalista in ravintola.Ruokalistat)
            {
                Console.WriteLine($"{ruokalista.Nimi}, tunnus: {ruokalista.Ruokalistaid} ");
            }
        }

    }
}
