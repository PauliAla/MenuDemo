using System;
using System.Collections.Generic;
using System.Text;
using Dapper;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace MenuDemoLibrary
{
    public class AnnosDataManager:DataAccess
    {
        private List<Annos> _annokset = new List<Annos>();

        public static Object GetTest()
        {
            string str = DataAccess.CnnVal(DataAccess.currentDBName);
            using (IDbConnection connection = new SqlConnection(str))
            {
                var toReturn = connection.QueryFirst<Object>("SELECT * FROM dbo.Annokset");
                return toReturn;
            }
            
              
        }

        public static List <Annos> GetAllDishes()
        {
            string str = DataAccess.CnnVal(DataAccess.currentDBName);
            using (IDbConnection connection = new SqlConnection(str))
            {
                var toReturn = connection.Query<Annos>("SELECT * FROM dbo.Annokset").ToList();
                return toReturn;
            }


        }


        public AnnosDataManager ()
        {
            this.AddTestData();
        }

        public List<Annos> Annokset { get => _annokset; set => _annokset = value; }

        private void AddTestData ()
        {
            _annokset.Add(new Juoma("Kalja", 5, "Hyvä", false));
            _annokset.Add(new Ruoka("Vihannes special", 10, "Hyvä", true,true,true));
            _annokset.Add(new Ruoka("Lohikeitto", 10, "Hyvä", false,true,true));
            _annokset.Add(new Ruoka("Pannari", 7, "Maukas", false, false, true));
            _annokset.Add(new Juoma("Vesi", 1, "Neutraali", true));
            _annokset.Add(new Ruoka("Hernekeitto", 6, "Vegaaninen hernekeitto", true, true, true));
            _annokset.Add(new Ruoka("Kasvislautanen", 8, "Kasvis", true, true, true));
            _annokset.Add(new Juoma("Maito", 3, "Neutraali", true));
            _annokset.Add(new Ruoka("Tuplarenu", 12, "Tuplarenu isompaan nälkään.", false, false, true));
            _annokset.Add(new Ruoka("Sipulikeitto", 7, "Ei kovin hyvä",true, true, true));
            KaikenDatanKäsittelijä._ravintolat.Add(new Ravintola("Kruunuhaka"));
            KaikenDatanKäsittelijä._ravintolat[0].Ruokalistat.Add(new Ruokalista("Lounasmenu"));
        }
    }
}
