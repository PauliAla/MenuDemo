using System;
using System.Collections.Generic;
using System.Text;

namespace MenuDemoLibrary
{
    public class Juoma : Annos
    {

        public Juoma (String nimi, float hinta, String kuvaus, bool alkoholiton)
        {
            this.Nimi = nimi;
            this.Hinta = hinta;
            this.Kuvaus = kuvaus;
            this.Alkoholiton = alkoholiton;
        }
        public Juoma(int id, String nimi, float hinta, String kuvaus, bool laktoositon, bool gluteiiniton, bool pähkinätön, bool alkoholiton)
        {
            this.Nimi = nimi;
            this.Hinta = hinta;
            this.Kuvaus = kuvaus;
            this.Alkoholiton = alkoholiton;
        }
    }
}
