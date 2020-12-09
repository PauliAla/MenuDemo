using System;
using System.Collections.Generic;
using System.Text;

namespace MenuDemoLibrary
{
    public class Allergeenit
    {
        public enum AllergeeniTyyppi { Laktoosi, Pähkinä, Gluteeni };
        static public Dictionary<AllergeeniTyyppi, string> AllergeeniMerkit = new Dictionary<AllergeeniTyyppi, string>()
        {{ AllergeeniTyyppi.Gluteeni, "G" }, { AllergeeniTyyppi.Laktoosi, "L" }, { AllergeeniTyyppi.Pähkinä, "P" }};

        public static string HaeAllergeeninTyyppi(AllergeeniTyyppi allergeenityyppi)
        {
            switch (allergeenityyppi)
            {
                case AllergeeniTyyppi.Gluteeni:
                    return "Gluteenia";
                case AllergeeniTyyppi.Laktoosi:
                    return "Laktoosia";
                case AllergeeniTyyppi.Pähkinä:
                    return "Pähkinää";
                default:
                    return "";
            }
        }
    }
}
