using System;
using System.Collections.Generic;
using System.Text;

namespace MenuDemoLibrary
{
    public class Annos
    {
        private string _nimi;
        private float _hinta;
        private string _kuvaus;
        private List<Allergeenit.AllergeeniTyyppi> _allergeenityypit = new List<Allergeenit.AllergeeniTyyppi>();
        private Boolean _alkoholiton;
        private int id;

        public string Nimi { get => _nimi; set => _nimi = value; }
        public float Hinta { get => _hinta; set => _hinta = value; }
        public string Kuvaus { get => _kuvaus; set => _kuvaus = value; }
        public bool Alkoholiton { get => _alkoholiton; set => _alkoholiton = value; }

        public List<Allergeenit.AllergeeniTyyppi> Allergeenityypit { get => _allergeenityypit; set => _allergeenityypit = value; }
        public int Id { get => id; set => id = value; }

        public static Annos luoAnnos()
        {
            Annos annos = new Annos();
            while (true) {
                Console.WriteLine("\n1. Ruoka vai 2. Juoma?");
                int valinta = int.Parse(Console.ReadLine());

                if (valinta == 1)
                {
               
                annos = LuoRuoka();
                    break;
                } else if (valinta == 2)
                {
                annos = LuoJuoma();
                    break;
                } else
                {
                    Console.WriteLine("valitse 1 tai 2.");
                }
            }
            return annos;

        }


        public static Annos LuoRuoka()
        {
            Console.WriteLine("Anna ruoan nimi: ");
            string nimi = Console.ReadLine();
            Console.WriteLine("Anna ruoan hinta: ");
            float hinta = float.Parse(Console.ReadLine());
            Console.WriteLine("Anna ruoan kuvaus: ");
            string kuvaus = Console.ReadLine();
            bool laktoositon = false;
            bool gluteiiniton = false;
            bool pähkinätön = false;

                Console.WriteLine("Onko ruoka laktoositon: k. Kyllä e. Ei");
                laktoositon = KaikenDatanKäsittelijä.otaKylläTaiEiKäyttäjältä();
                Console.WriteLine("Onko ruoka gluteiiniton: k. Kyllä e. Ei");
                gluteiiniton = KaikenDatanKäsittelijä.otaKylläTaiEiKäyttäjältä();
                Console.WriteLine("Onko ruoka pähkinätön: k. Kyllä e. Ei");
                pähkinätön = KaikenDatanKäsittelijä.otaKylläTaiEiKäyttäjältä();   
                Ruoka ruoka = new Ruoka(nimi, hinta, kuvaus, laktoositon, gluteiiniton, pähkinätön);
           
            return ruoka;
        }
        public static Annos LuoJuoma()
        {
            Console.WriteLine("Anna juoman Nimi: ");
            string nimi = Console.ReadLine();
            Console.WriteLine("Anna juoman hinta: ");
            float hinta = float.Parse(Console.ReadLine());
            Console.WriteLine("Anna juoman kuvaus: ");
            string kuvaus = Console.ReadLine();
            bool alkoholiton = false;
            Console.WriteLine("Onko juoma alkoholiton: k. Kyllä e. Ei");
            alkoholiton = KaikenDatanKäsittelijä.otaKylläTaiEiKäyttäjältä();
            Juoma juoma = new Juoma(nimi, hinta, kuvaus, alkoholiton);
            return juoma;
        }
        public static void tulostaAllergeenit (Annos annos)
        {
            Boolean olikojotain = false;
            foreach (Allergeenit.AllergeeniTyyppi listattu in annos.Allergeenityypit)
                
            {
                Console.WriteLine($"{listattu}");
                olikojotain = true;
            }
            if (olikojotain == false)
            {
                Console.WriteLine("Annos ei sisällä allergeeneja.");
            }
        }
        public static void tulostaAnnoksenTiedot (Annos annos)
        {
            Console.WriteLine($"\n{annos.Nimi}, {annos.Kuvaus}, {annos.Hinta} £ , \n Allergeenit:");
            tulostaAllergeenit(annos);
            Type type = annos.GetType();
            if (type == typeof(Juoma))
            {
                if ((annos as Juoma).Alkoholiton == true)
                {
                    Console.WriteLine("Ei sisällä alkoholia");
                }
                else if ((annos as Juoma).Alkoholiton == false)
                {
                    Console.WriteLine("Sisältää alkoholia");
                }
            }
        }

    }
 
}
