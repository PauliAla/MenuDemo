using System;
using System.Collections.Generic;
using System.Text;
using Dapper;
using System.Data;
using System.Data.SqlClient;
using System.Linq;


namespace MenuDemoLibrary
{
    public class KaikenDatanKäsittelijä
    {
        static public List<Ravintola> _ravintolat = new List<Ravintola>();
        public static AnnosDataManager dm = new AnnosDataManager();

        public List<Ravintola> Ravintolat { get => _ravintolat; set => _ravintolat = value; }

        public static void syötäRavintolanTiedot()
        // tietojen syottäminen ja uuden henkilön luominen niiden perusteella
        {
            Console.Clear();
            string ravintolannimi = "";
            bool onkoOk = false;

            while (onkoOk == false)
            {
                Console.WriteLine("Anna ravintolan nimi:");
                ravintolannimi = Console.ReadLine();
                ravintolannimi = ravintolannimi.Trim();
                onkoOk = Tarkasta(ravintolannimi);
            }
            Ravintola ravintola = new Ravintola(ravintolannimi);
            lisääRavintolaTietokantaan(ravintola);
        }
        public static void lisääRavintolaTietokantaan(Ravintola ravintola) 
        {
            List<Ravintola> haetut = haekaikkiRavintolatTietokannasta();
            string str = DataAccess.CnnVal(DataAccess.currentDBName);
            Console.WriteLine($"{haetut.Count}");
            int ravintolaId = (haetut.Count + 1);
            using (IDbConnection connection = new SqlConnection(str))
            {
                ravintolaId = connection.QuerySingle<int>("INSERT Ravintolalista (Id, Nimi) OUTPUT inserted.id " +
                    "VALUES (@Id,@Nimi)", new { Id = ravintolaId , Nimi= ravintola.RavintolanNimi });
            }

        }
        //ei toimi
        public static void lisääRuokalistaTietokantaan(Ravintola ravintola, Ruokalista ruokalista)
        {
            string str = DataAccess.CnnVal(DataAccess.currentDBName);
            
            using (IDbConnection connection = new SqlConnection(str))
            {
                var lista1 = connection.Query<int>("SELECT Id FROM Ruokalistat").ToList();
                int suurinId = 0;
                foreach(int Id in lista1)
                {
                    if (Id>suurinId)
                    {
                        suurinId = Id;
                    }
                }
                connection.Execute("INSERT Ruokalistat (Id, Nimi, Kuvaus, RavintolaId) " +
                    "VALUES (@Id,@Nimi,@Kuvaus,@RavintolaId)", new { Id = (suurinId+1), Nimi = ruokalista.Nimi, Kuvaus=ruokalista.Kuvaus, RavintolaId = ravintola.Id});
                var lista = connection.Query<int>("SELECT Id FROM Ravintolaruokalistat").ToList();
                
                int suurinId2 = 0;
                foreach (int Id in lista)
                {
                    if (Id > suurinId2)
                    {
                        suurinId2 = Id;
                    }
                }
                connection.Execute("INSERT RavintolaRuokalistat (Id, RavintolaId, RuokalistaId)" +
                   "VALUES (@Id,@RavintolaId,@RuokalistaId)", new { Id=(suurinId2+1),RavintolaId = ravintola.Id, RuokalistaId = ruokalista.Id });
        
            }

        }

        public static void lisääLuotuAnnosTietokantaan (Annos annos)
        {
            string str = DataAccess.CnnVal(DataAccess.currentDBName);
            using (IDbConnection connection = new SqlConnection(str))
            {
                var lista1 = connection.Query<int>("SELECT Id FROM Annokset").ToList();
                int suurinId = 0;
                foreach (int Id in lista1)
                {
                    if (Id > suurinId)
                    {
                        suurinId = Id;
                    }
                }
                int annostyyppi = 0;
                Type type = annos.GetType();
                if (type == typeof(Juoma))
                {
                    annostyyppi = 2;
                    var lista3 = connection.Query<int>("SELECT Id FROM Annokset").ToList();
                    int suurinId3 = 0;
                    foreach (int Id in lista3)
                    {
                        if (Id > suurinId3)
                        {
                            suurinId3 = Id;
                        }
                    }
                    connection.Execute("INSERT JuomaTyyppiAnnokset (Id, Alkoholiton, AnnosId ) " +
                   "VALUES (@Id, @Alkoholiton, @AnnosId )", new { Id = (suurinId3 + 1),  Alkoholiton= annos.Alkoholiton, AnnosId = (suurinId+1) });
                }   else
                {
                    annostyyppi = 1;
                }
                    connection.Execute("set IDENTITY_INSERT Annokset ON INSERT Annokset (Id, Nimi, Kuvaus, Hinta, KategoriaId, AllergeeniId, AnnostyyppiId ) " +
                    "VALUES (@Id,@Nimi,@Kuvaus,@Hinta, @KategoriaId, @AllergeeniId, @AnnostyyppiId )", new { Id = (suurinId + 1), Nimi = annos.Nimi, Kuvaus = annos.Kuvaus, Hinta = annos.Hinta, KategoriaId=0, AllergeeniId=0, AnnostyyppiId=annostyyppi });
                var lista2 = connection.Query<int>("SELECT Id FROM AllergeeniAnnokset").ToList();
                int suurinId2 = 0;
                foreach (int Id in lista2)
                {
                    if (Id > suurinId2)
                    {
                        suurinId2 = Id;
                    }
                }

                if (annos.Allergeenityypit.Contains(Allergeenit.AllergeeniTyyppi.Laktoosi))
                {
                    connection.Execute("INSERT AllergeeniAnnokset (Id,  AllergeeniId, AnnosId ) " +
                    "VALUES (@Id, @AllergeeniId, @AnnosId )", new { Id = (suurinId2 + 1), AllergeeniId = 1, AnnosId = (suurinId + 1) });
                    suurinId2++;
                }
                if (annos.Allergeenityypit.Contains(Allergeenit.AllergeeniTyyppi.Pähkinä))
                {
                    connection.Execute("INSERT AllergeeniAnnokset (Id,  AllergeeniId, AnnosId ) " +
                    "VALUES (@Id, @AllergeeniId, @AnnosId )", new { Id = (suurinId2 + 1), AllergeeniId = 2, AnnosId = (suurinId + 1) });
                    suurinId2++;
                }
                if (annos.Allergeenityypit.Contains(Allergeenit.AllergeeniTyyppi.Gluteeni))
                {
                    connection.Execute("INSERT AllergeeniAnnokset (Id,  AllergeeniId, AnnosId ) " +
                    "VALUES (@Id, @AllergeeniId, @AnnosId )", new { Id = (suurinId2 + 1), AllergeeniId = 3, AnnosId = (suurinId + 1) });
                }
            }

        }

        public static void lisääAnnosTietokannassa (int kategoriaId, int annosId)
        {
            string str = DataAccess.CnnVal(DataAccess.currentDBName);
            using (IDbConnection connection = new SqlConnection(str))
            {
                connection.Execute("INSERT KategoriaAnnokset (KategoriaId, AnnosId)" +
                    "VALUES (@KategoriaId,@AnnosId)", new { KategoriaId = kategoriaId, AnnosId = annosId });
            }

        }
        public static void poistaAnnosTietokannasta (int annosId)
        {
            string str = DataAccess.CnnVal(DataAccess.currentDBName);
            using (IDbConnection connection = new SqlConnection(str))
            {
                connection.Execute("DELETE FROM Annokset WHERE Id=@Id", new { id = annosId });
                connection.Execute("DELETE FROM KategoriaAnnokset WHERE AnnosId=@Id", new { Id = annosId });
            }
        }

        public static void poistaRavintolaTietokannasta(int ravintolaId)
        {
            string str = DataAccess.CnnVal(DataAccess.currentDBName);
            using (IDbConnection connection = new SqlConnection(str))
            {
                connection.Execute("DELETE FROM Ravintolalista WHERE Id=@Id", new { id = ravintolaId});
                connection.Execute("DELETE FROM RavintolaRuokalistat WHERE RavintolaId=@Id", new { Id = ravintolaId });
            }

        }

        public static void poistaRuokalistaTietokannasta(Ravintola ravintola, Ruokalista ruokalista)
        {
            string str = DataAccess.CnnVal(DataAccess.currentDBName);
            using (IDbConnection connection = new SqlConnection(str))
            {
                connection.Execute("DELETE FROM Ruokalistat WHERE Id=@Id", new { id = ruokalista.Id });
                connection.Execute("DELETE FROM RavintolaRuokalistat WHERE RuokalistaId=@Id", new { Id = ruokalista.Id });
            }

        }

        public static Annos haeAnnosIdllä (int Annosid)
        {
            string str = DataAccess.CnnVal(DataAccess.currentDBName);
            using (IDbConnection connection = new SqlConnection(str))
            {
                var toReturn = connection.QueryFirst<Annos>("SELECT * FROM Annokset WHERE id=@id", new { id = Annosid });
                var allergeenit = connection.Query<int>("SELECT AllergeeniId FROM AllergeeniAnnokset WHERE AnnosId=@id", new { id = Annosid }).ToArray();
                var annostyyppiIdt = connection.Query<int>("SELECT AnnostyyppiId FROM Annokset WHERE id=@id", new { id = Annosid }).ToArray();
                bool gluteeni=false;
                bool laktoosi=false;
                bool pähkinä=false;
                foreach (var allergeeni in allergeenit)
                {
                    if (allergeeni == 1) {
                        laktoosi = true;
                     }
                    if(allergeeni == 2) {
                        pähkinä= true;
                    }
                    if (allergeeni == 3)
                    {
                        gluteeni = true;
                    }
                }
                foreach (var Id in annostyyppiIdt)
                {
                    if (Id == 1)
                    {
                        Ruoka ruoka = new Ruoka(toReturn.Id, toReturn.Nimi, toReturn.Hinta, toReturn.Kuvaus, gluteeni, laktoosi, pähkinä);
                        return ruoka;
                    }
                    if (Id == 2)
                    {                       
                        var alkoholiton = connection.QueryFirst<bool>("SELECT Alkoholiton FROM JuomatyyppiAnnokset WHERE AnnosId=@id", new { id = Annosid });                        
                        Juoma juoma = new Juoma (toReturn.Id, toReturn.Nimi, toReturn.Hinta, toReturn.Kuvaus, gluteeni, laktoosi, pähkinä, alkoholiton);
                        return juoma;
                    }

                }
                return null;

            }
        }


     //   public static Annos haeAnnosLuokkaAnnosTyypillä (int tyyppi, Annos template = null)
     //   {
      //     if (tyyppi == 1)
       //     {
      //          annos = new Ruoka();
      //      }
      //  }
        public static Ravintola haeRavintolaIdllä (int RavintolaId)
        {
            string str = DataAccess.CnnVal(DataAccess.currentDBName);
            using (IDbConnection connection = new SqlConnection(str))
            {
                var toReturn = connection.QueryFirst<Ravintola>("SELECT * FROM Ravintolalista WHERE id=@id", new { id = RavintolaId });
                return toReturn;
            }
        }


        public static List <Ravintola>  haekaikkiRavintolatTietokannasta ()
        {
            string str = DataAccess.CnnVal(DataAccess.currentDBName);
            using (IDbConnection connection = new SqlConnection(str))
            {
                var toReturn = connection.Query<Ravintola>("SELECT * FROM Ravintolalista").ToList();
                return toReturn;
            }
        }

        public static List <Ruokalista> haeRuokalistatRavintolanIdllä (int RavintolaId)
        {
            string str = DataAccess.CnnVal(DataAccess.currentDBName);
            using (IDbConnection connection = new SqlConnection(str))
            {
                var toReturn = connection.Query<Ruokalista>("SELECT * FROM Ruokalistat  WHERE RavintolaId=@id", new { id = RavintolaId }).ToList();
                return toReturn;
            }
        }
        public static List <Kategoria> haeKategoriatRuokalistanIdllä (int RuokalistaId)
        {
            string str = DataAccess.CnnVal(DataAccess.currentDBName);
            using (IDbConnection connection = new SqlConnection(str))
            {
                var toReturn = connection.Query<Kategoria>("SELECT * FROM Kategoriat WHERE RuokalistaId=@id", new { id = RuokalistaId }).ToList();
                return toReturn;
            }
        }


        public static List <int> haeAnnostenTunnuksetKategoriasta (int kategoriaId)
        {
            string str = DataAccess.CnnVal(DataAccess.currentDBName);
            using (IDbConnection connection = new SqlConnection(str))
            {
                var toReturn = connection.Query<int>("SELECT AnnosId FROM KategoriaAnnokset WHERE KategoriaId=@id", new { id = kategoriaId } ).ToList();
                return toReturn;
            }
        }

        public static List <Annos> HaeKaikkiAnnoksetKategoriasta (int kategoriaId)
        {
            List <int> idt = haeAnnostenTunnuksetKategoriasta(kategoriaId);
            List<Annos> annokset = new List<Annos>();
            foreach (int id in idt)
            {
                annokset.Add(haeAnnosIdllä(id));
            }
            return annokset;

        }
        public static Ravintola haeKaikkiRavintolanDataIdllä(int RavintolaId)
        {
            Ravintola ravintola = haeRavintolaIdllä(RavintolaId);
            ravintola.Ruokalistat = haeRuokalistatRavintolanIdllä(ravintola.Id);

            foreach (Ruokalista ruokalista in ravintola.Ruokalistat)
            {
                ruokalista.Kategoriat = haeKategoriatRuokalistanIdllä(ruokalista.Id);
                foreach (Kategoria kategoria in ruokalista.Kategoriat)
                {
                    kategoria.Annoslista = HaeKaikkiAnnoksetKategoriasta(kategoria.Id);
                }
            }

            return ravintola;
        }
        public static List <Annos> haeKaikkiAnnokset ()
        {
            string str = DataAccess.CnnVal(DataAccess.currentDBName);
            using (IDbConnection connection = new SqlConnection(str))
            {
                var toReturn = connection.Query<Annos>("SELECT * FROM Annokset").ToList();
                return toReturn;
            }
        }
        public static void näytäRavintolanTiedot ()
        {

            List <Ravintola> ravintolat = haekaikkiRavintolatTietokannasta();
            katseleListaaRavintoloista2();
            Console.WriteLine("Valitse ravintolan Id jonka kaikki tiedot haluat nähdä:");
            int valinta = int.Parse(Console.ReadLine());
            foreach (Ravintola ravintola in ravintolat)
            {
                if (ravintola.Id==valinta)
                {
                    Ravintola haettu = haeKaikkiRavintolanDataIdllä(valinta);
                    foreach (Ruokalista ruokalista in haettu.Ruokalistat)
                    {
                        Console.WriteLine("\nRuokalista: ");
                        Console.WriteLine("-----------");
                        Console.WriteLine($"{ruokalista.Nimi}, {ruokalista.Kuvaus}");
                        foreach (Kategoria kategoria in ruokalista.Kategoriat)
                        {
                            Console.WriteLine("\nKategoria:");
                            Console.WriteLine("----------");
                            Console.WriteLine($"{kategoria.Nimi}, {kategoria.Kuvaus}, {kategoria.Id}");
                            foreach (Annos etsitty in kategoria.Annoslista)
                            {
                                Console.WriteLine("\nAnnos:");
                                Console.WriteLine("------");
                                Annos.tulostaAnnoksenTiedot(etsitty);
                            }
                        }
                    }
                }
            }
            odotaNäppäimenPainallusta();


        }
        public static void katseleListaaRavintoloista()
        {
            Console.WriteLine("\n");
            List <Ravintola> ravintolat = haekaikkiRavintolatTietokannasta();
            foreach (Ravintola ravintola in ravintolat)
            {
                Console.WriteLine($": Id: {ravintola.Id} Nimi: {ravintola.RavintolanNimi} ");
            }
            odotaNäppäimenPainallusta();            
        }
        //ei toimi vielä
        public static void poistaRavintolanTiedot()
        {
            Console.WriteLine("\n");
            katseleListaaRavintoloista2();
            bool onkoOk = false;

                Console.WriteLine("\nAnna sen ravintolan Id joka poistetaan: ");
                int numero = int.Parse(Console.ReadLine());
                List<Ravintola> ravintolat = haekaikkiRavintolatTietokannasta();
            foreach (Ravintola ravintola in ravintolat)
            {
                if (ravintola.Id == numero)
                {
                    poistaRavintolaTietokannasta(ravintola.Id);
                    onkoOk = true;

                }
            }
                if (onkoOk == false)
                {
                    Console.WriteLine("Tunnusta ei ole listassa");
                }
            

            odotaNäppäimenPainallusta();
        }
        public static void näytäRuokalistojenTiedot()
        {
            Console.Clear();
            katseleListaaRavintoloista2();
            Console.WriteLine("Anna ravintolan tunnus:");
            int numero = int.Parse(Console.ReadLine());
            
            bool onkoOk = false;
            List<Ravintola> ravintolat = haekaikkiRavintolatTietokannasta();
            foreach (Ravintola ravintola in ravintolat)
            {
                if (numero == ravintola.Id)
                {
                    onkoOk = true;

                    ravintola.Ruokalistat = haeRuokalistatRavintolanIdllä(numero);
                    foreach (Ruokalista ruokalista in ravintola.Ruokalistat)
                    {
                        Ruokalista.tulostaRuokalistanTiedot(ruokalista);
                   }
                }
            }
                if (onkoOk == false)
                {
                    Console.WriteLine("Tunnusta ei ole listassa.");
                }
                odotaNäppäimenPainallusta();

            
        }
        public static void lisääUusiRuokalista()

        {
            Console.Clear();
            katseleListaaRavintoloista2();
            bool onkoOk = false;
            Console.WriteLine("Anna ravintolan tunnus johon ruokalista lisätään:");            
            int numero = int.Parse(Console.ReadLine());
            List<Ravintola> ravintolat = haekaikkiRavintolatTietokannasta();
            foreach (Ravintola ravintola in ravintolat)
            {
                if (numero == ravintola.Id)
                {
                    Console.WriteLine("Anna ruokalistan nimi");
                    string ruokalistannimi = Console.ReadLine();
                    Console.WriteLine("Anna ruokalistan kuvaus");
                    string ruokalistankuvaus = Console.ReadLine();
                    ravintola.Ruokalistat = haeRuokalistatRavintolanIdllä(numero);
                    int suurinId = 0;
                    foreach (Ruokalista ruokalista in ravintola.Ruokalistat)
                    {
                        if (ruokalista.Id > suurinId)
                        {
                            suurinId = ruokalista.Id;
                        }
                    }
                    
                    
                    Ruokalista uusiRuokalista = new Ruokalista((suurinId+1), ruokalistannimi, ruokalistankuvaus, numero);
                    lisääRuokalistaTietokantaan(ravintola, uusiRuokalista);
                    Console.WriteLine("Ruokalista lisätty");
                    onkoOk = true;
                }
            }
            if (onkoOk == false)
            {
                Console.WriteLine("Tunnusta ei ole listassa.");
            }
            odotaNäppäimenPainallusta();


        }
        public static void poistaRuokalista()

        {
            Console.Clear();
            katseleListaaRavintoloista2();
            bool onkoOk = false;

            Console.WriteLine("Anna ravintolan tunnus josta ruokalista poistetaan:");
            int numero  = int.Parse(Console.ReadLine());
            List<Ravintola> ravintolat = haekaikkiRavintolatTietokannasta();
            if (!(numero < 1 || numero > ravintolat.Count))
            {
                foreach(Ravintola ravintola in ravintolat)
                {
                    if (ravintola.Id == numero)
                    {
                        onkoOk = true;
                        ravintola.Ruokalistat = haeRuokalistatRavintolanIdllä(numero);
                        foreach (Ruokalista ruokalista in ravintola.Ruokalistat)
                        {
                            Ruokalista.tulostaRuokalistanTiedot(ruokalista);
                        }
                        Console.WriteLine("Anna ruokalistan tunnus joka poistetaan");
                        int ruokalistannumero = int.Parse(Console.ReadLine());
                        foreach (Ruokalista ruokalista in ravintola.Ruokalistat)
                        {
                            if (ruokalista.Id == ruokalistannumero)
                            {
                                poistaRuokalistaTietokannasta(ravintola, ruokalista);
                            }
                        }
                    }
                }
            }   
            if (onkoOk == false)
            {
                Console.WriteLine("Tunnusta ei löytynyt.");
            }
            
            odotaNäppäimenPainallusta();
        }


        public static void näytäKategorianSisältö()
        {
            Console.WriteLine("");
            katseleListaaRavintoloista2();
            bool onkoOk = false;
            Console.WriteLine("");
            Console.WriteLine("Anna ravintolan tunnus jonka ruokalistan kategoriaa haluat tarkastella: ");
            int numero = int.Parse(Console.ReadLine());
            List<Ravintola> ravintolat = haekaikkiRavintolatTietokannasta();            
            if (!(numero < 1 || numero > _ravintolat.Count))
            {
                foreach (Ravintola ravintola in ravintolat)
                {
                    if (ravintola.Id == numero)
                    {
                        ravintola.Ruokalistat = haeRuokalistatRavintolanIdllä(numero);
                        foreach (Ruokalista ruokalista in ravintola.Ruokalistat)
                        {
                            Ruokalista.tulostaRuokalistanTiedot(ruokalista);
                        }
                        Console.WriteLine("Anna sen ruokalistan tunnus jonka kategoriaa haluat tarkastella: ");
                        int ruokalistantunnus = int.Parse(Console.ReadLine());
                        if (!(ruokalistantunnus < 1 || ruokalistantunnus > ravintola.Ruokalistat.Count)) {
                            foreach (Ruokalista ruokalista in ravintola.Ruokalistat)
                            {
                                if (ruokalista.Id == ruokalistantunnus)
                                {
                                    ruokalista.Kategoriat = haeKategoriatRuokalistanIdllä(ruokalistantunnus);
                                    foreach (Kategoria kategoria in ruokalista.Kategoriat)
                                    {
                                        Console.WriteLine($"{kategoria.Id}, {kategoria.Nimi}, {kategoria.Kuvaus}, {kategoria.RuokalistaId}");
                                    }                                                                        
                                    Console.WriteLine("Anna kategorian tunnus jonka annoksia haluat katsella");
                                    int kategoriantunnus = int.Parse(Console.ReadLine());
                                    foreach (Kategoria kategoria in ruokalista.Kategoriat)
                                    {
                                        if (kategoria.Id == kategoriantunnus)
                                        {
                                            List<Annos> annoslista = HaeKaikkiAnnoksetKategoriasta(kategoriantunnus);
                                            foreach (Annos annos in annoslista)
                                            {
                                                Annos.tulostaAnnoksenTiedot(annos);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    }
            }
            odotaNäppäimenPainallusta();

        }
        public static void luoAnnosJaLisääSeDatamanageriin ()
        {
            var annos = Annos.luoAnnos();
            lisääLuotuAnnosTietokantaan(annos);
        }
        public static void lisääAnnosDatamanagerista ()
        {
            Console.WriteLine("\nAnnokset:");
            for (int i=0; i<dm.Annokset.Count; i++)
            {
                Console.WriteLine($"\n{i+1}: {dm.Annokset[i].Nimi}, {dm.Annokset[i].Hinta}, {dm.Annokset[i].Kuvaus}");
            }
            Console.WriteLine("Valitse haluamasi annos:");
            int valinta = int.Parse(Console.ReadLine());
            Console.WriteLine("Seuraavaksi valitse ravintola, ruokalista, sekä kategoria johon haluat lisätä:");
            katseleListaaRavintoloista2();
            bool onkoOk = false;
            Console.WriteLine("\nAnna ravintolan tunnus jonka ruokalistan kategoriaan haluat lisätä: ");
            int numero = int.Parse(Console.ReadLine());
            foreach (Ravintola ravintola in _ravintolat)
            {
                if (ravintola.Id == numero)
                {
                    List<Ruokalista> ruokalistat = haeRuokalistatRavintolanIdllä(numero);
                    foreach (Ruokalista ruokalista in ruokalistat)
                    {
                        Ruokalista.tulostaRuokalistanTiedot(ruokalista);
                    }
                    Console.WriteLine("Anna sen ruokalistan tunnus jonka kategoriaan haluat lisätä: ");
                    int ruokalistantunnus = int.Parse(Console.ReadLine());
                    foreach (Ruokalista ruokalista in ravintola.Ruokalistat)
                    {
                        if (ruokalista.Ruokalistaid == ruokalistantunnus)
                        {
                            Ruokalista.tulostaKategorianNimet(ravintola, ruokalistantunnus);
                            Console.WriteLine("Anna sen kategorian tunnus johon haluat lisätä annoksen");
                            int kategoriantunnus = int.Parse(Console.ReadLine());
                            foreach (Kategoria kategoria in ruokalista.Kategoriat)
                            {
                                if (kategoria.Id == kategoriantunnus)
                                {
                                    Kategoria.lisääAnnosKategoriaan(ravintola, ruokalista, kategoria, dm.Annokset[valinta-1]);
                                    break;
                                    
                                }
                            }
                        }
                    }

                }
            }
            odotaNäppäimenPainallusta();

        }
        public static void lisääAnnosKategoriaanTietokannassa()
        {
            Console.WriteLine("\nAnnokset:");
            List <Annos> annokset = haeKaikkiAnnokset();
            for (int i = 0; i < annokset.Count; i++)
            {
                Console.WriteLine($"Id: {annokset[i].Id}, {annokset[i].Nimi}, {annokset[i].Kuvaus}, {annokset[i].Hinta} £"); 

            }
            Console.WriteLine("Valitse haluamasi annos:");
            int valinta = int.Parse(Console.ReadLine());
            Console.WriteLine("Seuraavaksi valitse ravintola, ruokalista, sekä kategoria johon haluat lisätä:");
            List<Ravintola> ravintolat = haekaikkiRavintolatTietokannasta();
            for (int i = 0; i < ravintolat.Count; i++)
            {
                Console.WriteLine($"{ravintolat[i].Id}, {ravintolat[i].RavintolanNimi}"); 
            }
            bool onkoOk = false;
            Console.WriteLine("\nAnna ravintolan tunnus jonka ruokalistan kategoriaan haluat lisätä: ");
            int numero = int.Parse(Console.ReadLine());
            foreach (Ravintola ravintola in ravintolat)
            {
                if (ravintola.Id == numero)
                {
                    ravintola.Ruokalistat = haeRuokalistatRavintolanIdllä(numero);
                    foreach (Ruokalista ruokalista in ravintola.Ruokalistat)
                    {
                        Ruokalista.tulostaRuokalistanTiedot(ruokalista);
                    }
                    Console.WriteLine("Anna sen ruokalistan tunnus jonka kategoriaan haluat lisätä: ");
                    int ruokalistantunnus = int.Parse(Console.ReadLine());
                    foreach (Ruokalista ruokalista in ravintola.Ruokalistat)
                    {
                        if (ruokalista.Id == ruokalistantunnus)
                        {
                            ruokalista.Kategoriat = haeKategoriatRuokalistanIdllä(ruokalistantunnus);
                            foreach (Kategoria kategoria in ruokalista.Kategoriat)
                            {
                                Console.WriteLine($"{kategoria.Id}, {kategoria.Nimi}, {kategoria.Kuvaus}, {kategoria.RuokalistaId}");
                            }
                            Console.WriteLine("Anna sen kategorian tunnus johon haluat lisätä annoksen");
                            int kategoriantunnus = int.Parse(Console.ReadLine());
                            foreach (Kategoria kategoria in ruokalista.Kategoriat)
                            {
                                if (kategoria.Id == kategoriantunnus)
                                {
                                    
                                    lisääAnnosTietokannassa(kategoriantunnus,valinta);
                                    Console.WriteLine("Annos lisätty.");
                                    break;

                                }
                            }
                        }
                    }

                }
            }
                    odotaNäppäimenPainallusta();

        }
        public static void tarkasteleDatamanagerinSisältöä()
        {

            // foreach(Annos annos in dm.Annokset)
            // { 
            //     Console.WriteLine($"\n{annos.Nimi}, {annos.Hinta}, {annos.Kuvaus}");
            //     for (int i = 0; i < annos.Allergeenityypit.Count; i++)
            //     {
            //         Console.WriteLine($"{annos.Allergeenityypit[i]}");
            //     }
            //     Type type = annos.GetType();
            //     if (type == typeof(Juoma))
            //     {
            //         if ((annos as Juoma).Alkoholiton==true ) {
            //             Console.WriteLine("Ei sisällä alkoholia");
            //         }   else if ((annos as Juoma).Alkoholiton==false)
            //         {
            //             Console.WriteLine("Sisältää alkoholia");
            //         }
            //     }
            // }
            List<Annos> lista = AnnosDataManager.GetAllDishes();
            foreach (Annos annos in lista)
            {
                Console.WriteLine($"\n{annos.Nimi}, {annos.Hinta}, {annos.Kuvaus}");
                for (int i = 0; i < annos.Allergeenityypit.Count; i++)
                {
                    Console.WriteLine($"{annos.Allergeenityypit[i]}");
                }
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

            odotaNäppäimenPainallusta();
        
        }
        public static void muokkaaDatamanagerinAnnosta()
        {
            bool jatketaanko = true;
            for (int i = 0; i < dm.Annokset.Count; i++)
            {
                Console.WriteLine($"\n{i + 1}: {dm.Annokset[i].Nimi}, {dm.Annokset[i].Hinta}, {dm.Annokset[i].Kuvaus},");
                for (int c= 0; c < dm.Annokset[i].Allergeenityypit.Count; c++)
                {
                    Console.WriteLine($"{dm.Annokset[i].Allergeenityypit[c]}");
                }
            }
            Console.WriteLine("Valitse annos jota haluat muokata");
            int valinta = int.Parse(Console.ReadLine());
            if (!(dm.Annokset[valinta-1]== null)) 
            {


                while (jatketaanko)
                {
                    Console.WriteLine("Valitse mitä haluat muokata:");
                    Console.WriteLine("1. Nimi, 2. Hinta, 3. Kuvaus");
                    Console.WriteLine("Ja jos on allergeeneja niin:");
                    Console.WriteLine("4. Gluteeni 5. Laktoosi 6.Pähkinä");
                    Console.WriteLine("Ja jos on juoma niin: 7. Alkoholiton");
                    Console.WriteLine("8. Lopeta muokkaus");
                    int muokattava = int.Parse(Console.ReadLine());
                    if (muokattava < 1 || muokattava > 8)
                    {
                        Console.WriteLine("Anna arvo väliltä 1-8");
                    }
                    else
                    {
                        if (muokattava == 1)
                        {
                            Console.WriteLine("Anna uusi nimi:");
                            string nimi = Console.ReadLine();
                            dm.Annokset[valinta - 1].Nimi = nimi;
                        }
                        else if (muokattava == 2)
                        {
                            Console.WriteLine("Anna uusi hinta:");
                            float hinta = float.Parse(Console.ReadLine());
                            dm.Annokset[valinta - 1].Hinta = hinta;
                        }
                        else if (muokattava == 3)
                        {
                            Console.WriteLine("Anna uusi kuvaus:");
                            string kuvaus = Console.ReadLine();
                            dm.Annokset[valinta - 1].Kuvaus = kuvaus;
                        }
                        else if (muokattava == 4)
                        {
                            if ((dm.Annokset[valinta - 1].Allergeenityypit.Contains(Allergeenit.AllergeeniTyyppi.Gluteeni) == true))
                            {
                                dm.Annokset[valinta - 1].Allergeenityypit.Remove(Allergeenit.AllergeeniTyyppi.Gluteeni);
                                Console.WriteLine("Gluteeni poistettu");

                            }
                            else
                            {
                                dm.Annokset[valinta - 1].Allergeenityypit.Add(Allergeenit.AllergeeniTyyppi.Gluteeni);
                                Console.WriteLine("Gluteeni lisätty");
                            }
                        }
                        else if (muokattava == 5)
                        {
                            if ((dm.Annokset[valinta - 1].Allergeenityypit.Contains(Allergeenit.AllergeeniTyyppi.Laktoosi) == true))
                            {
                                dm.Annokset[valinta - 1].Allergeenityypit.Remove(Allergeenit.AllergeeniTyyppi.Laktoosi);
                                Console.WriteLine("Laktoosi poistettu");

                            }
                            else
                            {
                                dm.Annokset[valinta - 1].Allergeenityypit.Add(Allergeenit.AllergeeniTyyppi.Laktoosi);
                                Console.WriteLine("Laktoosi lisätty");
                            }
                        }
                        else if (muokattava == 6)
                        {
                            if ((dm.Annokset[valinta - 1].Allergeenityypit.Contains(Allergeenit.AllergeeniTyyppi.Pähkinä) == true))
                            {
                                dm.Annokset[valinta - 1].Allergeenityypit.Remove(Allergeenit.AllergeeniTyyppi.Pähkinä);
                                Console.WriteLine("Pähkinä poistettu");

                            }
                            else
                            {
                                dm.Annokset[valinta - 1].Allergeenityypit.Add(Allergeenit.AllergeeniTyyppi.Pähkinä);
                                Console.WriteLine("Pähkinä lisätty");
                            }
                        }
                        else if (muokattava == 7)
                        {
                            if (dm.Annokset[valinta - 1].Alkoholiton == (true || false))   // en tiedä onko oikein
                                if ((dm.Annokset[valinta - 1].Alkoholiton == true))
                                {
                                    dm.Annokset[valinta - 1].Alkoholiton = false;
                                    Console.WriteLine("Alkoholiton : False");

                                }
                                else
                                {
                                    dm.Annokset[valinta - 1].Alkoholiton = true;
                                    Console.WriteLine("Alkoholiton : True");
                                }
                        }
                        else if (muokattava == 8)
                        {
                            jatketaanko = false;
                        }

                    }
       
                }

            }
            odotaNäppäimenPainallusta();

        }
        public static void poistaAnnosDatamanagerista ()
        {
            Console.WriteLine("\nAnnokset:");
            List<Annos> annokset = haeKaikkiAnnokset();

            foreach (Annos annos in annokset)
            {
                Annos.tulostaAnnoksenTiedot(annos);
            }
            int valinta;
            while (true)
            {
                Console.WriteLine("Valitse annos jonka haluat poistaa");
                valinta = int.Parse(Console.ReadLine());
                if (valinta<1 || valinta>annokset.Count)
                {
                    Console.WriteLine("Numeroa ei ole listassa");
                }   else
                {
                    break;
                }
            }

            dm.Annokset.RemoveAt(valinta - 1);
            odotaNäppäimenPainallusta();

        }


        public static void muokkaaKategorianNimeä ()
        {
            Console.WriteLine("");
            katseleListaaRavintoloista2();
            Console.WriteLine("Valitse ravintola jonka ruokalistan kategorian nimeä haluat muokata:");
            int valinta = int.Parse(Console.ReadLine());
            Ravintola.tulostaRuokalistat(_ravintolat[valinta - 1]);
            Console.WriteLine("Valitse ruokalista jonka kategorian nimeä haluat muokata.");
            int valinta2 = int.Parse(Console.ReadLine());
            Ruokalista.tulostaKategorianNimet(_ravintolat[valinta - 1], _ravintolat[valinta - 1].Ruokalistat[valinta2 - 1].Ruokalistaid);
            Console.WriteLine("Valitse kategorian tunnus jonka nimeä haluat muokata.");
            int valinta3 = int.Parse(Console.ReadLine());
            Console.WriteLine("Anna uusi nimi:");
            string uusinimi = Console.ReadLine();
            _ravintolat[valinta - 1].Ruokalistat[valinta2 - 1].Kategoriat[valinta3 - 1].Nimi = uusinimi;
            odotaNäppäimenPainallusta();
        }
        public static void poistaKategoriaRuokalistasta ()
        {
            Console.WriteLine("");
            katseleListaaRavintoloista2();
            Console.WriteLine("Valitse ravintola jonka ruokalistan kategorian haluat poistaa:");
            int valinta = int.Parse(Console.ReadLine());
            Ravintola.tulostaRuokalistat(_ravintolat[valinta - 1]);
            Console.WriteLine("Valitse ruokalista jonka kategorian haluat poistaa.");
            int valinta2 = int.Parse(Console.ReadLine());
            Ruokalista.tulostaKategorianNimet(_ravintolat[valinta - 1], _ravintolat[valinta - 1].Ruokalistat[valinta2 - 1].Ruokalistaid);
            Console.WriteLine("Valitse kategorian tunnus jonka haluat poistaa.");
            int valinta3 = int.Parse(Console.ReadLine());
            _ravintolat[valinta - 1].Ruokalistat[valinta2 - 1].Kategoriat.RemoveAt(valinta3 - 1);
            odotaNäppäimenPainallusta();
        }
        public static void luoUusiKategoria()
        {
            Console.WriteLine("");
            katseleListaaRavintoloista2();
            Console.WriteLine("Valitse ravintola jonka ruokalistaan haluat luoda uuden kategorian::");
            int valinta = int.Parse(Console.ReadLine());
            Ravintola ravintola = haeRavintolaIdllä(valinta);
            if (ravintola.Id == valinta) {
                ravintola.Ruokalistat = haeRuokalistatRavintolanIdllä(ravintola.Id);
                Console.WriteLine("Valitse ruokalista johon haluat luoda uuden kategorian:");
                int valinta2 = int.Parse(Console.ReadLine());
                foreach (Ruokalista ruokalista in ravintola.Ruokalistat)
                {
                    if (ruokalista.Id == valinta2)
                    {
                        ruokalista.Kategoriat = haeKategoriatRuokalistanIdllä(valinta2);
                        bool onkoOk = false;
                        string uusinimi = "";
                        int tunnus = 0;
                        while (onkoOk == false) {
                            Console.WriteLine("Anna nimi uudelle kategorialle:");
                            uusinimi = Console.ReadLine();
                            foreach (Kategoria kategoria in ruokalista.Kategoriat)
                            {
                                if (kategoria.Nimi == uusinimi)
                                {
                                    Console.WriteLine("Nimi on jo listassa. Kirjoita eri nimi.");
                                    onkoOk = false;
                                    break;
                                }
                                onkoOk = true;
                            }


                        }
                        onkoOk = false;
                        while (onkoOk == false)
                        {
                            Console.WriteLine("Anna tunnus uudelle kategorialle:");
                            tunnus = int.Parse(Console.ReadLine());
                            foreach (Kategoria kategoria in _ravintolat[valinta - 1].Ruokalistat[valinta2 - 1].Kategoriat)
                            {
                                if (kategoria.Id == tunnus)
                                {
                                    Console.WriteLine("Tunnus on jo listassa. Kirjoita eri tunnus.");
                                    onkoOk = false;
                                    break;
                                }
                                onkoOk = true;
                            }


                        }
                        Kategoria uusiKategoria = new Kategoria(uusinimi, tunnus);
                        _ravintolat[valinta - 1].Ruokalistat[valinta2 - 1].Kategoriat.Add(uusiKategoria);
                        odotaNäppäimenPainallusta();

                    }
                }
            } }

        public static  bool otaKylläTaiEiKäyttäjältä()
        {
            while (true)
            {
                
                    ConsoleKey consolekey = Console.ReadKey().Key;

                    if (consolekey == ConsoleKey.K)
                    {
                        return true;

                    }
                    else if (consolekey == ConsoleKey.E)
                    {
                        return false;
                    }   else
                {
                    Console.WriteLine("\nEi ollut K tai E");
                }                
            }   
        }
        public static void odotaNäppäimenPainallusta()
        {
            Console.WriteLine("Paina jotain näppäintä jatkaaksesi");
            ConsoleKey selection = ConsoleKey.A;
            selection = Console.ReadKey().Key;

            switch (selection)
            {
                default:
                    break;
            }
        }
        public static void katseleListaaRavintoloista2()
        {
            Console.WriteLine("\n");
            List<Ravintola> ravintolat = haekaikkiRavintolatTietokannasta();
            foreach (Ravintola ravintola in ravintolat)
            {
                Console.WriteLine($": Id: {ravintola.Id} Nimi: {ravintola.RavintolanNimi} ");
            }
        }
        




        private static bool Tarkasta(string tarkastettava)
        // metodi joka tarkastaa onko nimet liian lyhyitä
        {
            
            if (String.IsNullOrWhiteSpace(tarkastettava))
            {
                return false;
            }
            if (tarkastettava.Length > 1)
            {
                return true;
            }
            else
            {
                Console.WriteLine("liian lyhyt");
                return false;
            }
        }


    }
}
