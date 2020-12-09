using System;
using System.Collections.Generic;
using System.Text;

namespace MenuDemoLibrary
{
    public class MenuManager
    {
        public void tulostaAnnos(Annos annos)
        {
            Console.WriteLine($"Nimi: {annos.Nimi} hinta: {annos.Hinta}");
            Type type = annos.GetType();
            if (type == typeof(Juoma))
            {
                Juoma juoma = (Juoma)annos;
                Console.WriteLine($"Alkoholillinen:{(annos as Juoma).Alkoholiton}");
            }
        }
        public void tulostaKategoria(Kategoria kategoria)
        {
            Console.WriteLine($"{kategoria.Nimi}");
            foreach (Annos annos in kategoria.Annoslista)
            {
                tulostaAnnos(annos);
            }
        }
        public void tulostaRavintolat(List <Ravintola> ravintolat)
        {
            foreach (Ravintola ravintola in ravintolat)
            {
                Console.WriteLine($"{ravintola.RavintolanNimi}"); 
            }
        }
    }
}
