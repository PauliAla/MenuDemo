using System;
using System.Collections.Generic;
using System.Text;

namespace MenuDemoLibrary
{
    public class Ruoka : Annos
    {
        private Boolean _laktoositon;
        private Boolean _gluteiiniton;
        

        public bool Laktoositon { get => _laktoositon; set => _laktoositon = value; }
        public bool Gluteiiniton { get => _gluteiiniton; set => _gluteiiniton = value; }

        

        public Ruoka (String nimi, float hinta, String kuvaus, bool laktoositon, bool gluteiiniton, bool pähkinätön)
        {
            this.Nimi = nimi;
            this.Hinta = hinta;
            this.Kuvaus = kuvaus;
            if (laktoositon == false)
            {
                this.Allergeenityypit.Add(Allergeenit.AllergeeniTyyppi.Laktoosi);
            }
            if (gluteiiniton== false)
            {
                this.Allergeenityypit.Add(Allergeenit.AllergeeniTyyppi.Gluteeni);
            }
            if (pähkinätön == false)
            {
                this.Allergeenityypit.Add(Allergeenit.AllergeeniTyyppi.Pähkinä);
            }
        }
        public Ruoka(int id, String nimi, float hinta, String kuvaus, bool laktoositon, bool gluteiiniton, bool pähkinätön)
        {
            this.Id = id;
            this.Nimi = nimi;
            this.Hinta = hinta;
            this.Kuvaus = kuvaus;
            if (laktoositon == false)
            {
                this.Allergeenityypit.Add(Allergeenit.AllergeeniTyyppi.Laktoosi);
            }
            if (gluteiiniton == false)
            {
                this.Allergeenityypit.Add(Allergeenit.AllergeeniTyyppi.Gluteeni);
            }
            if (pähkinätön == false)
            {
                this.Allergeenityypit.Add(Allergeenit.AllergeeniTyyppi.Pähkinä);
            }
        }

    }
}
