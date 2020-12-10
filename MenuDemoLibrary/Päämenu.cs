using System;
using System.Collections.Generic;
using System.Text;

namespace MenuDemoLibrary
{
    public class Päämenu
    {
        public static void Valinnat()
        {
            
            ConsoleKey selection = ConsoleKey.A;

            while (selection != ConsoleKey.Escape)
            {
                Console.Clear();
                Console.WriteLine("Päävalikko:");
                Console.WriteLine("-----------");
                Console.WriteLine("1. Ravintolat");
                Console.WriteLine("2. Ruokalistat");
                Console.WriteLine("3. Kategoriat");
                Console.WriteLine("4. Annokset");
                Console.WriteLine("ESC lopettaa ohjelman suorituksen");
                
                selection = Console.ReadKey().Key;

                switch (selection)
                {
                    case ConsoleKey.D1:
                        MenuDemoLibrary.Päämenu.ravintolanValinnat();
                        break;
                    case ConsoleKey.D2:
                        MenuDemoLibrary.Päämenu.ruokalistanValinnat();
                        break;
                    case ConsoleKey.D3:
                        MenuDemoLibrary.Päämenu.kategorioidenValinnat();
                        break;
                    case ConsoleKey.D4:
                        MenuDemoLibrary.Päämenu.annostenValinnat();
                        break;
                    case ConsoleKey.Escape:
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("\nValitse numero 1-4.");
                        KaikenDatanKäsittelijä.odotaNäppäimenPainallusta();
                        break;
                }
            }



        }
        public static void ravintolanValinnat()
        {
            ConsoleKey selection = ConsoleKey.A;

            while (selection != ConsoleKey.Escape)
            {
                Console.Clear();
                Console.WriteLine("Ravintola:");
                Console.WriteLine("----------");
                Console.WriteLine("1. Syötä ravintolan tiedot");
                Console.WriteLine("2. Katsele listaa ravintoloista");
                Console.WriteLine("3. Poista ravintolan tiedot");
                Console.WriteLine("4. Näytä ravintolan kaikki tiedot (Nimi, ruokalistat, kategoriat ja annokset)");
                Console.WriteLine("5. Paluu päävalikkoon");

                selection = Console.ReadKey().Key;

                switch (selection)
                {
                    case ConsoleKey.D1:
                        MenuDemoLibrary.KaikenDatanKäsittelijä.syötäRavintolanTiedot();
                        break;
                    case ConsoleKey.D2:
                        MenuDemoLibrary.KaikenDatanKäsittelijä.katseleListaaRavintoloista();
                        break;
                    case ConsoleKey.D3:
                        MenuDemoLibrary.KaikenDatanKäsittelijä.poistaRavintolanTiedot();
                        break;
                    case ConsoleKey.D4:
                        MenuDemoLibrary.KaikenDatanKäsittelijä.näytäRavintolanTiedot();
                        break;
                    case ConsoleKey.D5:
                        MenuDemoLibrary.Päämenu.Valinnat();
                        break;
                    case ConsoleKey.Escape:
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("\nValitse numero 1-5.");
                        KaikenDatanKäsittelijä.odotaNäppäimenPainallusta();
                        break;
                }
            }
        }
        public static void ruokalistanValinnat()
        {
            ConsoleKey selection = ConsoleKey.A;

            while (selection != ConsoleKey.Escape)
            {
                Console.Clear();
                Console.WriteLine("Ruokalistat:");
                Console.WriteLine("-----------");
                Console.WriteLine("1. Näytä ruokalistojen tiedot");
                Console.WriteLine("2. Lisää uusi ruokalista (Ja sen kategoriat automaattisesti)");
                Console.WriteLine("3. Poista ruokalista");
                Console.WriteLine("4. Paluu päävalikkoon");

                selection = Console.ReadKey().Key;

                switch (selection)
                {
                    case ConsoleKey.D1:
                        MenuDemoLibrary.KaikenDatanKäsittelijä.näytäRuokalistojenTiedot();
                        break;
                    case ConsoleKey.D2:
                        MenuDemoLibrary.KaikenDatanKäsittelijä.lisääUusiRuokalista();
                        break;
                    case ConsoleKey.D3:
                        MenuDemoLibrary.KaikenDatanKäsittelijä.poistaRuokalista();
                        break;
                    case ConsoleKey.D4:
                        MenuDemoLibrary.Päämenu.Valinnat();
                        break;
                    case ConsoleKey.Escape:
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("\nValitse numero 1-4.");
                        KaikenDatanKäsittelijä.odotaNäppäimenPainallusta();
                        break;
                }
            }
        }
        public static void kategorioidenValinnat()
        {
            ConsoleKey selection = ConsoleKey.A;

            while (selection != ConsoleKey.Escape)
            {
                Console.Clear();
                Console.WriteLine("Kategoriat:");
                Console.WriteLine("-----------");
                Console.WriteLine("1. Näytä kategorian sisältö");
                Console.WriteLine("2. Muokkaa kategorian nimeä");
                Console.WriteLine("3. Poista kategoria ruokalistasta");
                Console.WriteLine("4. Paluu päävalikkoon");

                selection = Console.ReadKey().Key;

                switch (selection)
                {
                    case ConsoleKey.D1:
                        MenuDemoLibrary.KaikenDatanKäsittelijä.näytäKategorianSisältö();
                        break;
                    case ConsoleKey.D2:
                        MenuDemoLibrary.KaikenDatanKäsittelijä.muokkaaKategorianNimeä();
                        break;
                    case ConsoleKey.D3:
                        MenuDemoLibrary.KaikenDatanKäsittelijä.poistaKategoriaRuokalistasta();
                        break;
                    case ConsoleKey.D4:
                        MenuDemoLibrary.Päämenu.Valinnat();
                        break;
                    case ConsoleKey.Escape:
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("\nValitse numero 1-4.");
                        KaikenDatanKäsittelijä.odotaNäppäimenPainallusta();
                        break;
                }
            }
        }
        public static void annostenValinnat()
        {
            ConsoleKey selection = ConsoleKey.A;

            while (selection != ConsoleKey.Escape)
            {
                Console.Clear();
                Console.WriteLine("Annokset:");
                Console.WriteLine("---------");
                Console.WriteLine("1. Luo annos ja lisää se tietokantaan");
                Console.WriteLine("2. Lisää annos haluamaasi ravintolan ruokalistan kategoriaan tietokannassa");
                Console.WriteLine("3. Tarkastele tietokannan annoksien sisältöä");
                Console.WriteLine("4. Poista annos tietokannasta");
                //Console.WriteLine("5. Muokkaa datamanagerissa olevaa annosta");
                Console.WriteLine("5. Paluu päävalikkoon");


                selection = Console.ReadKey().Key;

                switch (selection)
                {
                    case ConsoleKey.D1:
                        MenuDemoLibrary.KaikenDatanKäsittelijä.luoAnnosJaLisääSeDatamanageriin();
                        break;
                    case ConsoleKey.D2:
                        //MenuDemoLibrary.KaikenDatanKäsittelijä.lisääAnnosDatamanagerista();
                        MenuDemoLibrary.KaikenDatanKäsittelijä.lisääAnnosKategoriaanTietokannassa();
                        break;
                    case ConsoleKey.D3:
                        MenuDemoLibrary.KaikenDatanKäsittelijä.tarkasteleDatamanagerinSisältöä();
                        break;
                    case ConsoleKey.D4:
                        MenuDemoLibrary.KaikenDatanKäsittelijä.poistaAnnosDatamanagerista();
                        break;
                   // case ConsoleKey.D5:
                   //     MenuDemoLibrary.KaikenDatanKäsittelijä.muokkaaDatamanagerinAnnosta();
                   //     break;
                    case ConsoleKey.D5:
                        MenuDemoLibrary.Päämenu.Valinnat();
                        break;
                    case ConsoleKey.Escape:
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("\nValitse numero 1-5.");
                        KaikenDatanKäsittelijä.odotaNäppäimenPainallusta();
                        break;
                }
            }
        }
    }
}

