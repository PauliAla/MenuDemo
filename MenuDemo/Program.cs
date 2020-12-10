using System;
using MenuDemoLibrary;
using System.Collections.Generic;
//Console.OutputEncoding = System.Text.Encoding.UTF8; //dollarihommat

namespace MenuDemo
{
    class Program
    {
        public List<Ravintola> ravintolat;
        public static List<Annos> annoksia;
        public AnnosDataManager dm = new AnnosDataManager();        
        


        static void Main(string[] args)
        {
            var test = AnnosDataManager.GetTest();           
            Päämenu.Valinnat();
        }        
    }
}

        