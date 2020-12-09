using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace MenuDemoLibrary
{   //[DebuggerDisplay("Kategoria {DebuggerDisplay}")];
    public class Kategoria
    {
        //public enum kategoriannimi
        //{
        //    Alkuruoat, Pääruoat, Jälkiruoat, Juomat
        //}
        private string _nimi;
        private List<Annos> annoslista = new List<Annos> ();
        static int idCount = 0;
        private int id;
        private int _ruokalistaId;
        private string _kuvaus;

        public List<Annos> Annoslista { get => annoslista; set => annoslista = value; }
        public string Nimi { get => _nimi; set => _nimi = value; }
        public int Id { get => id; set => id = value; }
        public int RuokalistaId { get => _ruokalistaId; set => _ruokalistaId = value; }
        public string Kuvaus { get => _kuvaus; set => _kuvaus = value; }

        public Kategoria(string nimi)
        {
            this.Nimi = nimi;
            idCount++;
            this.id = idCount;
            if (idCount > 3)
            {
                idCount = 0;
            }

        }
        public Kategoria(string nimi, int tunnus)
        {
            this.Nimi = nimi;
            this.Id = tunnus;

        }
        public Kategoria(int id, string nimi, string kuvaus, int ruokalistaId)
        {            
            this.Id = id;
            this.Nimi = nimi;
            this.Kuvaus = kuvaus;
            this.RuokalistaId = ruokalistaId;

        }
        public static void lisääAnnosKategoriaan  (Ravintola ravintola, Ruokalista ruokalista, Kategoria kategoria, Annos annos)
        {

            //kategoria.annoslista.Add(annos);
            //ruokalista.Kategoriat.Add(kategoria);
            //ravintola.ruokalistat.Add(ruokalista);

                            kategoria.Annoslista.Add(annos);

        }



    }


}
