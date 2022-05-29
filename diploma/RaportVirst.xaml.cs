using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Text.RegularExpressions;
using System.Data.Linq;
using diploma.Clase;
using System.IO;
using System.Diagnostics;

namespace diploma
{
    /// <summary>
    /// Interaction logic for RaportVirst.xaml
    /// </summary>
    public partial class RaportVirst : Window
    {
        string conn = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + Environment.CurrentDirectory + @"\Personal.mdf;Integrated Security=True;Connect Timeout=30";

        public RaportVirst()
        {
            DateTime elib = new DateTime(1800, 1, 1);
            InitializeComponent();
            DataContext db = new DataContext(conn);
           var persoana = db.GetTable<Persoane>().Where(x => x.DataEliberarii==elib);
            Table<Raionull> raion = db.GetTable<Raionull>();
            Table<DenInst> institutia = db.GetTable<DenInst>();
            Table<Nationalit> nationalitatea = db.GetTable<Nationalit>();
            Table<Stud> studii = db.GetTable<Stud>();
            Table<FormCont> formare = db.GetTable<FormCont>();
            Table<Functii> functii = db.GetTable<Functii>();
            Table<Activ> activitati = db.GetTable<Activ>();
            Table<GrStiinte> grad = db.GetTable<GrStiinte>();
            Table<PersoanaFunc> fpers = db.GetTable<PersoanaFunc>();
            Table<Catedre> catedre = db.GetTable<Catedre>();
            Table<Localit> localitatea = db.GetTable<Localit>();
            Table<ProfDisc> prdis = db.GetTable<ProfDisc>();
            Table<Disciplin> dis = db.GetTable<Disciplin>();
            Table<Catedre> catedra = db.GetTable<Catedre>();

            DateTime a = new DateTime(1997, 12, 31);
            DateTime a25 = new DateTime(1996, 12, 31);
            DateTime a29 = new DateTime(1992, 12, 31);
            DateTime a30 = new DateTime(1991, 12, 31);
            DateTime a34 = new DateTime(1987, 12, 31);
            DateTime a35 = new DateTime(1986, 12, 31);
            DateTime a39 = new DateTime(1982, 12, 31);
            DateTime a40 = new DateTime(1981, 12, 31);
            DateTime a44 = new DateTime(1977, 12, 31);
            DateTime a45 = new DateTime(1976, 12, 31);
            DateTime a49 = new DateTime(1972, 12, 31);
            DateTime a50 = new DateTime(1971, 12, 31);
            DateTime a54 = new DateTime(1967, 12, 31);
            DateTime a55 = new DateTime(1966, 12, 31);
            DateTime a59 = new DateTime(1962, 12, 31);
            DateTime a60 = new DateTime(1961, 12, 31);
            DateTime a64 = new DateTime(1957, 12, 31);
            DateTime a65 = new DateTime(1956, 12, 31);

            var tot = from p in persoana
                      join c in catedra on p.IdCatedra equals c.IdCatedra
                      where c.DenumireCatedra != "Personal auxiliar" && c.DenumireCatedra != "Personal didactic auxiliar"
                      select new
                      {
                          ID = p.IdPerson,
                          Genul = p.Gen,
                          DataN = p.DataNast
                      };

            var fem = from p in tot
                      where p.Genul == false
                      select new
                      {
                          ID = p.ID,
                          Datanast = p.DataN
                      };
            var tot1 = from p in tot
                       where p.DataN >= a
                       select new
                       {
                           id = p.ID,
                           Genul = p.Genul,
                       };
            var fem1 = from p in tot1
                       where p.Genul == false
                       select new
                       {
                           id = p.id,
                           Genul = p.Genul,
                       };

            var tot2 = from p in tot
                       where p.DataN >= a29 && p.DataN <= a25
                       select new
                       {
                           id = p.ID,
                           Genul = p.Genul,
                       };
            var fem2 = from p in tot2
                       where p.Genul == false
                       select new
                       {
                           id = p.id,
                           Genul = p.Genul,
                       };
            var tot3 = from p in tot
                       where p.DataN >= a34 && p.DataN <= a30
                       select new
                       {
                           id = p.ID,
                           Genul = p.Genul,
                       };
            var fem3 = from p in tot3
                       where p.Genul == false
                       select new
                       {
                           id = p.id,
                           Genul = p.Genul,
                       };
            var tot4 = from p in tot
                       where p.DataN >= a39 && p.DataN <= a35
                       select new
                       {
                           id = p.ID,
                           Genul = p.Genul,
                       };
            var fem4 = from p in tot4
                       where p.Genul == false
                       select new
                       {
                           id = p.id,
                           Genul = p.Genul,
                       };
            var tot5 = from p in tot
                       where p.DataN >= a44 && p.DataN <= a40
                       select new
                       {
                           id = p.ID,
                           Genul = p.Genul,
                       };
            var fem5 = from p in tot5
                       where p.Genul == false
                       select new
                       {
                           id = p.id,
                           Genul = p.Genul,
                       };
            var tot6 = from p in tot
                       where p.DataN >= a49 && p.DataN <= a45
                       select new
                       {
                           id = p.ID,
                           Genul = p.Genul,
                       };
            var fem6 = from p in tot6
                       where p.Genul == false
                       select new
                       {
                           id = p.id,
                           Genul = p.Genul,
                       };
            var tot7 = from p in tot
                       where p.DataN >= a54 && p.DataN <= a50
                       select new
                       {
                           id = p.ID,
                           Genul = p.Genul,
                       };
            var fem7 = from p in tot7
                       where p.Genul == false
                       select new
                       {
                           id = p.id,
                           Genul = p.Genul,
                       };
            var tot8 = from p in tot
                       where p.DataN >= a59 && p.DataN <= a55
                       select new
                       {
                           id = p.ID,
                           Genul = p.Genul,
                       };
            var fem8 = from p in tot8
                       where p.Genul == false
                       select new
                       {
                           id = p.id,
                           Genul = p.Genul,
                       };
            var tot9 = from p in tot
                       where p.DataN >= a64 && p.DataN <= a60
                       select new
                       {
                           id = p.ID,
                           Genul = p.Genul,
                       };
            var fem9 = from p in tot9
                       where p.Genul == false
                       select new
                       {
                           id = p.id,
                           Genul = p.Genul,
                       };
            var tot10 = from p in tot
                        where p.DataN <= a65
                        select new
                        {
                            id = p.ID,
                            Genul = p.Genul,
                        };
            var fem10 = from p in tot10
                        where p.Genul == false
                        select new
                        {
                            id = p.id,
                            Genul = p.Genul,
                        };


            var totr2 = from p in persoana
                        join c in catedra on p.IdCatedra equals c.IdCatedra
                        where c.DenumireCatedra == "Personal de conducere"
                        select new
                        {
                            ID = p.IdPerson,
                            Genul = p.Gen,
                            DataN = p.DataNast
                        };

            var fema = from p in totr2
                       where p.Genul == false
                       select new
                       {
                           ID = p.ID,
                           Datanast = p.DataN
                       };
            var tota1 = from p in totr2
                        where p.DataN >= a
                        select new
                        {
                            id = p.ID,
                            Genul = p.Genul,
                        };
            var fema1 = from p in tota1
                        where p.Genul == false
                        select new
                        {
                            id = p.id,
                            Genul = p.Genul,
                        };

            var tota2 = from p in totr2
                        where p.DataN >= a29 && p.DataN <= a25
                        select new
                        {
                            id = p.ID,
                            Genul = p.Genul,
                        };
            var fema2 = from p in tota2
                        where p.Genul == false
                        select new
                        {
                            id = p.id,
                            Genul = p.Genul,
                        };
            var tota3 = from p in totr2
                        where p.DataN >= a34 && p.DataN <= a30
                        select new
                        {
                            id = p.ID,
                            Genul = p.Genul,
                        };
            var fema3 = from p in tota3
                        where p.Genul == false
                        select new
                        {
                            id = p.id,
                            Genul = p.Genul,
                        };
            var tota4 = from p in totr2
                        where p.DataN >= a39 && p.DataN <= a35
                        select new
                        {
                            id = p.ID,
                            Genul = p.Genul,
                        };
            var fema4 = from p in tota4
                        where p.Genul == false
                        select new
                        {
                            id = p.id,
                            Genul = p.Genul,
                        };
            var tota5 = from p in totr2
                        where p.DataN >= a44 && p.DataN <= a40
                        select new
                        {
                            id = p.ID,
                            Genul = p.Genul,
                        };
            var fema5 = from p in tota5
                        where p.Genul == false
                        select new
                        {
                            id = p.id,
                            Genul = p.Genul,
                        };
            var tota6 = from p in totr2
                        where p.DataN >= a49 && p.DataN <= a45
                        select new
                        {
                            id = p.ID,
                            Genul = p.Genul,
                        };
            var fema6 = from p in tota6
                        where p.Genul == false
                        select new
                        {
                            id = p.id,
                            Genul = p.Genul,
                        };
            var tota7 = from p in totr2
                        where p.DataN >= a54 && p.DataN <= a50
                        select new
                        {
                            id = p.ID,
                            Genul = p.Genul,
                        };
            var fema7 = from p in tota7
                        where p.Genul == false
                        select new
                        {
                            id = p.id,
                            Genul = p.Genul,
                        };
            var tota8 = from p in totr2
                        where p.DataN >= a59 && p.DataN <= a55
                        select new
                        {
                            id = p.ID,
                            Genul = p.Genul,
                        };
            var fema8 = from p in tota8
                        where p.Genul == false
                        select new
                        {
                            id = p.id,
                            Genul = p.Genul,
                        };
            var tota9 = from p in totr2
                        where p.DataN >= a64 && p.DataN <= a60
                        select new
                        {
                            id = p.ID,
                            Genul = p.Genul,
                        };
            var fema9 = from p in tota9
                        where p.Genul == false
                        select new
                        {
                            id = p.id,
                            Genul = p.Genul,
                        };
            var tota10 = from p in totr2
                         where p.DataN <= a65
                         select new
                         {
                             id = p.ID,
                             Genul = p.Genul,
                         };
            var fema10 = from p in tota10
                         where p.Genul == false
                         select new
                         {
                             id = p.id,
                             Genul = p.Genul,
                         };

            var totr3 = from p in persoana
                        join c in catedra on p.IdCatedra equals c.IdCatedra
                        where c.DenumireCatedra == "Personal de conducere" && p.EsteProfesor == true
                        select new
                        {
                            ID = p.IdPerson,
                            Genul = p.Gen,
                            DataN = p.DataNast
                        };

            var femb = from p in totr3
                       where p.Genul == false
                       select new
                       {
                           ID = p.ID,
                           Datanast = p.DataN
                       };
            var totb1 = from p in totr3
                        where p.DataN >= a
                        select new
                        {
                            id = p.ID,
                            Genul = p.Genul,
                        };
            var femb1 = from p in totb1
                        where p.Genul == false
                        select new
                        {
                            id = p.id,
                            Genul = p.Genul,
                        };

            var totb2 = from p in totr3
                        where p.DataN >= a29 && p.DataN <= a25
                        select new
                        {
                            id = p.ID,
                            Genul = p.Genul,
                        };
            var femb2 = from p in totb2
                        where p.Genul == false
                        select new
                        {
                            id = p.id,
                            Genul = p.Genul,
                        };
            var totb3 = from p in totr3
                        where p.DataN >= a34 && p.DataN <= a30
                        select new
                        {
                            id = p.ID,
                            Genul = p.Genul,
                        };
            var femb3 = from p in totb3
                        where p.Genul == false
                        select new
                        {
                            id = p.id,
                            Genul = p.Genul,
                        };
            var totb4 = from p in totr3
                        where p.DataN >= a39 && p.DataN <= a35
                        select new
                        {
                            id = p.ID,
                            Genul = p.Genul,
                        };
            var femb4 = from p in totb4
                        where p.Genul == false
                        select new
                        {
                            id = p.id,
                            Genul = p.Genul,
                        };
            var totb5 = from p in totr3
                        where p.DataN >= a44 && p.DataN <= a40
                        select new
                        {
                            id = p.ID,
                            Genul = p.Genul,
                        };
            var femb5 = from p in totb5
                        where p.Genul == false
                        select new
                        {
                            id = p.id,
                            Genul = p.Genul,
                        };
            var totb6 = from p in totr3
                        where p.DataN >= a49 && p.DataN <= a45
                        select new
                        {
                            id = p.ID,
                            Genul = p.Genul,
                        };
            var femb6 = from p in totb6
                        where p.Genul == false
                        select new
                        {
                            id = p.id,
                            Genul = p.Genul,
                        };
            var totb7 = from p in totr3
                        where p.DataN >= a54 && p.DataN <= a50
                        select new
                        {
                            id = p.ID,
                            Genul = p.Genul,
                        };
            var femb7 = from p in totb7
                        where p.Genul == false
                        select new
                        {
                            id = p.id,
                            Genul = p.Genul,
                        };
            var totb8 = from p in totr3
                        where p.DataN >= a59 && p.DataN <= a55
                        select new
                        {
                            id = p.ID,
                            Genul = p.Genul,
                        };
            var femb8 = from p in totb8
                        where p.Genul == false
                        select new
                        {
                            id = p.id,
                            Genul = p.Genul,
                        };
            var totb9 = from p in totr3
                        where p.DataN >= a64 && p.DataN <= a60
                        select new
                        {
                            id = p.ID,
                            Genul = p.Genul,
                        };
            var femb9 = from p in totb9
                        where p.Genul == false
                        select new
                        {
                            id = p.id,
                            Genul = p.Genul,
                        };
            var totb10 = from p in totr3
                         where p.DataN <= a65
                         select new
                         {
                             id = p.ID,
                             Genul = p.Genul,
                         };
            var femb10 = from p in totb10
                         where p.Genul == false
                         select new
                         {
                             id = p.id,
                             Genul = p.Genul,
                         };


            var totr4 = from p in persoana
                        join c in catedra on p.IdCatedra equals c.IdCatedra
                        where p.EsteProfesor == true && c.DenumireCatedra != "Personal de conducere" && c.DenumireCatedra != "Personal didactic auxiliar"
                        select new
                        {
                            ID = p.IdPerson,
                            Genul = p.Gen,
                            DataN = p.DataNast
                        };

            var femc = from p in totr4
                       where p.Genul == false
                       select new
                       {
                           ID = p.ID,
                           Datanast = p.DataN
                       };
            var totc1 = from p in totr4
                        where p.DataN >= a
                        select new
                        {
                            id = p.ID,
                            Genul = p.Genul,
                        };
            var femc1 = from p in totc1
                        where p.Genul == false
                        select new
                        {
                            id = p.id,
                            Genul = p.Genul,
                        };

            var totc2 = from p in totr4
                        where p.DataN >= a29 && p.DataN <= a25
                        select new
                        {
                            id = p.ID,
                            Genul = p.Genul,
                        };
            var femc2 = from p in totc2
                        where p.Genul == false
                        select new
                        {
                            id = p.id,
                            Genul = p.Genul,
                        };
            var totc3 = from p in totr4
                        where p.DataN >= a34 && p.DataN <= a30
                        select new
                        {
                            id = p.ID,
                            Genul = p.Genul,
                        };
            var femc3 = from p in totc3
                        where p.Genul == false
                        select new
                        {
                            id = p.id,
                            Genul = p.Genul,
                        };
            var totc4 = from p in totr4
                        where p.DataN >= a39 && p.DataN <= a35
                        select new
                        {
                            id = p.ID,
                            Genul = p.Genul,
                        };
            var femc4 = from p in totc4
                        where p.Genul == false
                        select new
                        {
                            id = p.id,
                            Genul = p.Genul,
                        };
            var totc5 = from p in totr4
                        where p.DataN >= a44 && p.DataN <= a40
                        select new
                        {
                            id = p.ID,
                            Genul = p.Genul,
                        };
            var femc5 = from p in totc5
                        where p.Genul == false
                        select new
                        {
                            id = p.id,
                            Genul = p.Genul,
                        };
            var totc6 = from p in totr4
                        where p.DataN >= a49 && p.DataN <= a45
                        select new
                        {
                            id = p.ID,
                            Genul = p.Genul,
                        };
            var femc6 = from p in totc6
                        where p.Genul == false
                        select new
                        {
                            id = p.id,
                            Genul = p.Genul,
                        };
            var totc7 = from p in totr4
                        where p.DataN >= a54 && p.DataN <= a50
                        select new
                        {
                            id = p.ID,
                            Genul = p.Genul,
                        };
            var femc7 = from p in totc7
                        where p.Genul == false
                        select new
                        {
                            id = p.id,
                            Genul = p.Genul,
                        };
            var totc8 = from p in totr4
                        where p.DataN >= a59 && p.DataN <= a55
                        select new
                        {
                            id = p.ID,
                            Genul = p.Genul,
                        };
            var femc8 = from p in totc8
                        where p.Genul == false
                        select new
                        {
                            id = p.id,
                            Genul = p.Genul,
                        };
            var totc9 = from p in totr4
                        where p.DataN >= a64 && p.DataN <= a60
                        select new
                        {
                            id = p.ID,
                            Genul = p.Genul,
                        };
            var femc9 = from p in totc9
                        where p.Genul == false
                        select new
                        {
                            id = p.id,
                            Genul = p.Genul,
                        };
            var totc10 = from p in totr4
                         where p.DataN <= a65
                         select new
                         {
                             id = p.ID,
                             Genul = p.Genul,
                         };
            var femc10 = from p in totc10
                         where p.Genul == false
                         select new
                         {
                             id = p.id,
                             Genul = p.Genul,
                         };


            var totr5 = from p in persoana
                        join c in catedre on p.IdCatedra equals c.IdCatedra
                        where p.EsteProfesor == true && c.DenumireCatedra == "Contabilitate și analiză economică" || c.DenumireCatedra == "Finanțe" || c.DenumireCatedra == "Administrarea afacerilor" || c.DenumireCatedra == "Informatică" || c.DenumireCatedra == "Matematică și fizică" || c.DenumireCatedra == "Stiințe socio-umanistice" || c.DenumireCatedra == "Limbă și comunicare"
                        select new
                        {
                            p,
                            ID = p.IdPerson,
                            Genul = p.Gen,
                            DataN = p.DataNast
                        };

            var femd = from p in totr5
                       where p.Genul == false
                       select new
                       {
                           ID = p.ID,
                           Datanast = p.DataN
                       };
            var totd1 = from p in totr5
                        where p.DataN >= a
                        select new
                        {
                            id = p.ID,
                            Genul = p.Genul,
                        };
            var femd1 = from p in totd1
                        where p.Genul == false
                        select new
                        {
                            id = p.id,
                            Genul = p.Genul,
                        };

            var totd2 = from p in totr5
                        where p.DataN >= a29 && p.DataN <= a25
                        select new
                        {
                            id = p.ID,
                            Genul = p.Genul,
                        };
            var femd2 = from p in totd2
                        where p.Genul == false
                        select new
                        {
                            id = p.id,
                            Genul = p.Genul,
                        };
            var totd3 = from p in totr5
                        where p.DataN >= a34 && p.DataN <= a30
                        select new
                        {
                            id = p.ID,
                            Genul = p.Genul,
                        };
            var femd3 = from p in totd3
                        where p.Genul == false
                        select new
                        {
                            id = p.id,
                            Genul = p.Genul,
                        };
            var totd4 = from p in totr5
                        where p.DataN >= a39 && p.DataN <= a35
                        select new
                        {
                            id = p.ID,
                            Genul = p.Genul,
                        };
            var femd4 = from p in totd4
                        where p.Genul == false
                        select new
                        {
                            id = p.id,
                            Genul = p.Genul,
                        };
            var totd5 = from p in totr5
                        where p.DataN >= a44 && p.DataN <= a40
                        select new
                        {
                            id = p.ID,
                            Genul = p.Genul,
                        };
            var femd5 = from p in totd5
                        where p.Genul == false
                        select new
                        {
                            id = p.id,
                            Genul = p.Genul,
                        };
            var totd6 = from p in totr5
                        where p.DataN >= a49 && p.DataN <= a45
                        select new
                        {
                            id = p.ID,
                            Genul = p.Genul,
                        };
            var femd6 = from p in totd6
                        where p.Genul == false
                        select new
                        {
                            id = p.id,
                            Genul = p.Genul,
                        };
            var totd7 = from p in totr5
                        where p.DataN >= a54 && p.DataN <= a50
                        select new
                        {
                            id = p.ID,
                            Genul = p.Genul,
                        };
            var femd7 = from p in totd7
                        where p.Genul == false
                        select new
                        {
                            id = p.id,
                            Genul = p.Genul,
                        };
            var totd8 = from p in totr5
                        where p.DataN >= a59 && p.DataN <= a55
                        select new
                        {
                            id = p.ID,
                            Genul = p.Genul,
                        };
            var femd8 = from p in totd8
                        where p.Genul == false
                        select new
                        {
                            id = p.id,
                            Genul = p.Genul,
                        };
            var totd9 = from p in totr5
                        where p.DataN >= a64 && p.DataN <= a60
                        select new
                        {
                            id = p.ID,
                            Genul = p.Genul,
                        };
            var femd9 = from p in totd9
                        where p.Genul == false
                        select new
                        {
                            id = p.id,
                            Genul = p.Genul,
                        };
            var totd10 = from p in totr5
                         where p.DataN <= a65
                         select new
                         {
                             id = p.ID,
                             Genul = p.Genul,
                         };
            var femd10 = from p in totd10
                         where p.Genul == false
                         select new
                         {
                             id = p.id,
                             Genul = p.Genul,
                         };

            var totr6 = from p in totr5
                        join i in prdis on p.p.IdPerson equals i.idPerson
                        join d in dis on i.idDiscip equals d.IdDiscip
                        where d.Denumirea.Contains("Limba Română") || d.Denumirea.Contains("Matematica") || d.Denumirea.Contains("Geografia") || d.Denumirea.Contains("Limba Engleză") || d.Denumirea.Contains("Limba Franceză") || d.Denumirea.Contains("Biologia") || d.Denumirea.Contains("Fizica") || d.Denumirea.Contains("Chimia") || d.Denumirea.Contains("Informatica") || d.Denumirea.Contains("Educația Civică") || d.Denumirea.Contains("Edicația fizică") || d.Denumirea.Contains("Istoria")
                        select new
                        {
                            ID = p.p.IdPerson,
                            Genul = p.p.Gen,
                            DataN = p.p.DataNast
                        };

            var feme = from p in totr6
                       where p.Genul == false
                       select new
                       {
                           ID = p.ID,
                           Datanast = p.DataN
                       };
            var tote1 = from p in totr6
                        where p.DataN >= a
                        select new
                        {
                            id = p.ID,
                            Genul = p.Genul,
                        };
            var feme1 = from p in totd1
                        where p.Genul == false
                        select new
                        {
                            id = p.id,
                            Genul = p.Genul,
                        };

            var tote2 = from p in totr6
                        where p.DataN >= a29 && p.DataN <= a25
                        select new
                        {
                            id = p.ID,
                            Genul = p.Genul,
                        };
            var feme2 = from p in tote2
                        where p.Genul == false
                        select new
                        {
                            id = p.id,
                            Genul = p.Genul,
                        };
            var tote3 = from p in totr6
                        where p.DataN >= a34 && p.DataN <= a30
                        select new
                        {
                            id = p.ID,
                            Genul = p.Genul,
                        };
            var feme3 = from p in tote3
                        where p.Genul == false
                        select new
                        {
                            id = p.id,
                            Genul = p.Genul,
                        };
            var tote4 = from p in totr6
                        where p.DataN >= a39 && p.DataN <= a35
                        select new
                        {
                            id = p.ID,
                            Genul = p.Genul,
                        };
            var feme4 = from p in tote4
                        where p.Genul == false
                        select new
                        {
                            id = p.id,
                            Genul = p.Genul,
                        };
            var tote5 = from p in totr6
                        where p.DataN >= a44 && p.DataN <= a40
                        select new
                        {
                            id = p.ID,
                            Genul = p.Genul,
                        };
            var feme5 = from p in tote5
                        where p.Genul == false
                        select new
                        {
                            id = p.id,
                            Genul = p.Genul,
                        };
            var tote6 = from p in totr6
                        where p.DataN >= a49 && p.DataN <= a45
                        select new
                        {
                            id = p.ID,
                            Genul = p.Genul,
                        };
            var feme6 = from p in tote6
                        where p.Genul == false
                        select new
                        {
                            id = p.id,
                            Genul = p.Genul,
                        };
            var tote7 = from p in totr6
                        where p.DataN >= a54 && p.DataN <= a50
                        select new
                        {
                            id = p.ID,
                            Genul = p.Genul,
                        };
            var feme7 = from p in tote7
                        where p.Genul == false
                        select new
                        {
                            id = p.id,
                            Genul = p.Genul,
                        };
            var tote8 = from p in totr6
                        where p.DataN >= a59 && p.DataN <= a55
                        select new
                        {
                            id = p.ID,
                            Genul = p.Genul,
                        };
            var feme8 = from p in tote8
                        where p.Genul == false
                        select new
                        {
                            id = p.id,
                            Genul = p.Genul,
                        };
            var tote9 = from p in totr6
                        where p.DataN >= a64 && p.DataN <= a60
                        select new
                        {
                            id = p.ID,
                            Genul = p.Genul,
                        };
            var feme9 = from p in tote9
                        where p.Genul == false
                        select new
                        {
                            id = p.id,
                            Genul = p.Genul,
                        };
            var tote10 = from p in totr6
                         where p.DataN <= a65
                         select new
                         {
                             id = p.ID,
                             Genul = p.Genul,
                         };
            var feme10 = from p in tote10
                         where p.Genul == false
                         select new
                         {
                             id = p.id,
                             Genul = p.Genul,
                         };



            var totr7 = from p in persoana
                        join fp in fpers on p.IdPerson equals fp.IdPerson
                        join c in catedre on p.IdCatedra equals c.IdCatedra
                        where c.DenumireCatedra == "Maestru"
                        select new
                        {
                            ID = p.IdPerson,
                            Genul = p.Gen,
                            DataN = p.DataNast
                        };

            var femf = from p in totr7
                       where p.Genul == false
                       select new
                       {
                           ID = p.ID,
                           Datanast = p.DataN
                       };
            var totf1 = from p in totr7
                        where p.DataN >= a
                        select new
                        {
                            id = p.ID,
                            Genul = p.Genul,
                        };
            var femf1 = from p in totf1
                        where p.Genul == false
                        select new
                        {
                            id = p.id,
                            Genul = p.Genul,
                        };

            var totf2 = from p in totr7
                        where p.DataN >= a29 && p.DataN <= a25
                        select new
                        {
                            id = p.ID,
                            Genul = p.Genul,
                        };
            var femf2 = from p in totf2
                        where p.Genul == false
                        select new
                        {
                            id = p.id,
                            Genul = p.Genul,
                        };
            var totf3 = from p in totr7
                        where p.DataN >= a34 && p.DataN <= a30
                        select new
                        {
                            id = p.ID,
                            Genul = p.Genul,
                        };
            var femf3 = from p in totf3
                        where p.Genul == false
                        select new
                        {
                            id = p.id,
                            Genul = p.Genul,
                        };
            var totf4 = from p in totr7
                        where p.DataN >= a39 && p.DataN <= a35
                        select new
                        {
                            id = p.ID,
                            Genul = p.Genul,
                        };
            var femf4 = from p in totf4
                        where p.Genul == false
                        select new
                        {
                            id = p.id,
                            Genul = p.Genul,
                        };
            var totf5 = from p in totr7
                        where p.DataN >= a44 && p.DataN <= a40
                        select new
                        {
                            id = p.ID,
                            Genul = p.Genul,
                        };
            var femf5 = from p in totf5
                        where p.Genul == false
                        select new
                        {
                            id = p.id,
                            Genul = p.Genul,
                        };
            var totf6 = from p in totr7
                        where p.DataN >= a49 && p.DataN <= a45
                        select new
                        {
                            id = p.ID,
                            Genul = p.Genul,
                        };
            var femf6 = from p in totf6
                        where p.Genul == false
                        select new
                        {
                            id = p.id,
                            Genul = p.Genul,
                        };
            var totf7 = from p in totr7
                        where p.DataN >= a54 && p.DataN <= a50
                        select new
                        {
                            id = p.ID,
                            Genul = p.Genul,
                        };
            var femf7 = from p in totf7
                        where p.Genul == false
                        select new
                        {
                            id = p.id,
                            Genul = p.Genul,
                        };
            var totf8 = from p in totr7
                        where p.DataN >= a59 && p.DataN <= a55
                        select new
                        {
                            id = p.ID,
                            Genul = p.Genul,
                        };
            var femf8 = from p in totf8
                        where p.Genul == false
                        select new
                        {
                            id = p.id,
                            Genul = p.Genul,
                        };
            var totf9 = from p in totr7
                        where p.DataN >= a64 && p.DataN <= a60
                        select new
                        {
                            id = p.ID,
                            Genul = p.Genul,
                        };
            var femf9 = from p in totf9
                        where p.Genul == false
                        select new
                        {
                            id = p.id,
                            Genul = p.Genul,
                        };
            var totf10 = from p in totr7
                         where p.DataN <= a65
                         select new
                         {
                             id = p.ID,
                             Genul = p.Genul,
                         };
            var femf10 = from p in totf10
                         where p.Genul == false
                         select new
                         {
                             id = p.id,
                             Genul = p.Genul,
                         };



            var totr8 = from p in persoana
                        join fp in fpers on p.IdPerson equals fp.IdPerson
                        join c in catedre on p.IdCatedra equals c.IdCatedra
                        where p.EsteProfesor == true && c.DenumireCatedra == "Alt personal didactic"
                        select new
                        {
                            ID = p.IdPerson,
                            Genul = p.Gen,
                            DataN = p.DataNast
                        };

            var femg = from p in totr8
                       where p.Genul == false
                       select new
                       {
                           ID = p.ID,
                           Datanast = p.DataN
                       };
            var totg1 = from p in totr8
                        where p.DataN >= a
                        select new
                        {
                            id = p.ID,
                            Genul = p.Genul,
                        };
            var femg1 = from p in totg1
                        where p.Genul == false
                        select new
                        {
                            id = p.id,
                            Genul = p.Genul,
                        };

            var totg2 = from p in totr8
                        where p.DataN >= a29 && p.DataN <= a25
                        select new
                        {
                            id = p.ID,
                            Genul = p.Genul,
                        };
            var femg2 = from p in totg2
                        where p.Genul == false
                        select new
                        {
                            id = p.id,
                            Genul = p.Genul,
                        };
            var totg3 = from p in totr8
                        where p.DataN >= a34 && p.DataN <= a30
                        select new
                        {
                            id = p.ID,
                            Genul = p.Genul,
                        };
            var femg3 = from p in totg3
                        where p.Genul == false
                        select new
                        {
                            id = p.id,
                            Genul = p.Genul,
                        };
            var totg4 = from p in totr8
                        where p.DataN >= a39 && p.DataN <= a35
                        select new
                        {
                            id = p.ID,
                            Genul = p.Genul,
                        };
            var femg4 = from p in totg4
                        where p.Genul == false
                        select new
                        {
                            id = p.id,
                            Genul = p.Genul,
                        };
            var totg5 = from p in totr8
                        where p.DataN >= a44 && p.DataN <= a40
                        select new
                        {
                            id = p.ID,
                            Genul = p.Genul,
                        };
            var femg5 = from p in totg5
                        where p.Genul == false
                        select new
                        {
                            id = p.id,
                            Genul = p.Genul,
                        };
            var totg6 = from p in totr8
                        where p.DataN >= a49 && p.DataN <= a45
                        select new
                        {
                            id = p.ID,
                            Genul = p.Genul,
                        };
            var femg6 = from p in totg6
                        where p.Genul == false
                        select new
                        {
                            id = p.id,
                            Genul = p.Genul,
                        };
            var totg7 = from p in totr8
                        where p.DataN >= a54 && p.DataN <= a50
                        select new
                        {
                            id = p.ID,
                            Genul = p.Genul,
                        };
            var femg7 = from p in totg7
                        where p.Genul == false
                        select new
                        {
                            id = p.id,
                            Genul = p.Genul,
                        };
            var totg8 = from p in totr8
                        where p.DataN >= a59 && p.DataN <= a55
                        select new
                        {
                            id = p.ID,
                            Genul = p.Genul,
                        };
            var femg8 = from p in totg8
                        where p.Genul == false
                        select new
                        {
                            id = p.id,
                            Genul = p.Genul,
                        };
            var totg9 = from p in totr8
                        where p.DataN >= a64 && p.DataN <= a60
                        select new
                        {
                            id = p.ID,
                            Genul = p.Genul,
                        };
            var femg9 = from p in totg9
                        where p.Genul == false
                        select new
                        {
                            id = p.id,
                            Genul = p.Genul,
                        };
            var totg10 = from p in totr8
                         where p.DataN <= a65
                         select new
                         {
                             id = p.ID,
                             Genul = p.Genul,
                         };
            var femg10 = from p in totg10
                         where p.Genul == false
                         select new
                         {
                             id = p.id,
                             Genul = p.Genul,
                         };

            List<Raport2> date = new List<Raport2>();
            date.Add(new Raport2 { descriere = "Total (rând. 020+040)", rind = "010", total = tot.Count(), femei = fem.Count(), total25 = tot1.Count(), femei25 = fem1.Count(), total2529 = tot2.Count(), femei2529 = fem2.Count(), total3034 = tot3.Count(), femei3034 = fem3.Count(), total3539 = tot4.Count(), femei3539 = fem4.Count(), total4044 = tot5.Count(), femei4044 = fem5.Count(), total4549 = tot6.Count(), femei4549 = fem6.Count(), total5054 = tot7.Count(), femei5054 = fem7.Count(), total5559 = tot8.Count(), femei5559 = fem8.Count(), total6064 = tot9.Count(), femei6064 = fem9.Count(), total65 = tot10.Count(), femei65 = fem10.Count() });
            date.Add(new Raport2 { descriere = "Personal de conducere (director, director adjunct, șef secție)", rind = "020", total = totr2.Count(), femei = fema.Count(), total25 = tota1.Count(), femei25 = fema1.Count(), total2529 = tota2.Count(), femei2529 = fema2.Count(), total3034 = tota3.Count(), femei3034 = fema3.Count(), total3539 = tota4.Count(), femei3539 = fema4.Count(), total4044 = tota5.Count(), femei4044 = fema5.Count(), total4549 = tota6.Count(), femei4549 = fema6.Count(), total5054 = tota7.Count(), femei5054 = fema7.Count(), total5559 = tota8.Count(), femei5559 = fema8.Count(), total6064 = tota9.Count(), femei6064 = fema9.Count(), total65 = tota10.Count(), femei65 = fema10.Count() });
            date.Add(new Raport2 { descriere = "din care cu norme didactice", rind = "030", total = totr3.Count(), femei = femb.Count(), total25 = totb1.Count(), femei25 = femb1.Count(), total2529 = totb2.Count(), femei2529 = femb2.Count(), total3034 = totb3.Count(), femei3034 = femb3.Count(), total3539 = totb4.Count(), femei3539 = femb4.Count(), total4044 = totb5.Count(), femei4044 = femb5.Count(), total4549 = totb6.Count(), femei4549 = femb6.Count(), total5054 = totb7.Count(), femei5054 = femb7.Count(), total5559 = totb8.Count(), femei5559 = femb8.Count(), total6064 = totb9.Count(), femei6064 = femb9.Count(), total65 = totb10.Count(), femei65 = femb10.Count() });
            date.Add(new Raport2 { descriere = "Personal didactic (rând. 050+070+080)", rind = "040", total = totr4.Count(), femei = femc.Count(), total25 = totc1.Count(), femei25 = femc1.Count(), total2529 = totc2.Count(), femei2529 = femc2.Count(), total3034 = totc3.Count(), femei3034 = femc3.Count(), total3539 = totc4.Count(), femei3539 = femc4.Count(), total4044 = totc5.Count(), femei4044 = femc5.Count(), total4549 = totc6.Count(), femei4549 = femc6.Count(), total5054 = totc7.Count(), femei5054 = femc7.Count(), total5559 = totc8.Count(), femei5559 = femc8.Count(), total6064 = totc9.Count(), femei6064 = femc9.Count(), total65 = totc10.Count(), femei65 = femc10.Count() });
            date.Add(new Raport2 { descriere = "profesori", rind = "050", total = totr5.Count(), femei = femd.Count(), total25 = totd1.Count(), femei25 = femd1.Count(), total2529 = totd2.Count(), femei2529 = femd2.Count(), total3034 = totd3.Count(), femei3034 = femd3.Count(), total3539 = totd4.Count(), femei3539 = femd4.Count(), total4044 = totd5.Count(), femei4044 = femd5.Count(), total4549 = totd6.Count(), femei4549 = femd6.Count(), total5054 = totd7.Count(), femei5054 = femd7.Count(), total5559 = totd8.Count(), femei5559 = femd8.Count(), total6064 = totd9.Count(), femei6064 = femd9.Count(), total65 = totd10.Count(), femei65 = femd10.Count() });
            date.Add(new Raport2 { descriere = "din care: profesori disciplini generale", rind = "060", total = totr6.Count(), femei = feme.Count(), total25 = tote1.Count(), femei25 = feme1.Count(), total2529 = tote2.Count(), femei2529 = feme2.Count(), total3034 = tote3.Count(), femei3034 = feme3.Count(), total3539 = tote4.Count(), femei3539 = feme4.Count(), total4044 = tote5.Count(), femei4044 = feme5.Count(), total4549 = tote6.Count(), femei4549 = feme6.Count(), total5054 = tote7.Count(), femei5054 = feme7.Count(), total5559 = tote8.Count(), femei5559 = feme8.Count(), total6064 = tote9.Count(), femei6064 = feme9.Count(), total65 = tote10.Count(), femei65 = feme10.Count() });
            date.Add(new Raport2 { descriere = "maeștri-instructori", rind = "070", total = totr7.Count(), femei = femf.Count(), total25 = totf1.Count(), femei25 = femf1.Count(), total2529 = totf2.Count(), femei2529 = femf2.Count(), total3034 = totf3.Count(), femei3034 = femf3.Count(), total3539 = totf4.Count(), femei3539 = femf4.Count(), total4044 = totf5.Count(), femei4044 = femf5.Count(), total4549 = totf6.Count(), femei4549 = femf6.Count(), total5054 = totf7.Count(), femei5054 = femf7.Count(), total5559 = totf8.Count(), femei5559 = femf8.Count(), total6064 = totf9.Count(), femei6064 = femf9.Count(), total65 = totf10.Count(), femei65 = femf10.Count() });
            date.Add(new Raport2 { descriere = "alt personal didactic", rind = "080", total = totr8.Count(), femei = femg.Count(), total25 = totg1.Count(), femei25 = femg1.Count(), total2529 = totg2.Count(), femei2529 = femg2.Count(), total3034 = totg3.Count(), femei3034 = femg3.Count(), total3539 = totg4.Count(), femei3539 = femg4.Count(), total4044 = totg5.Count(), femei4044 = femg5.Count(), total4549 = totg6.Count(), femei4549 = femg6.Count(), total5054 = totg7.Count(), femei5054 = femg7.Count(), total5559 = totg8.Count(), femei5559 = femg8.Count(), total6064 = totg9.Count(), femei6064 = femg9.Count(), total65 = totg10.Count(), femei65 = femg10.Count() });
            aloha.ItemsSource = date;
        }

        private void Export_Click(object sender, RoutedEventArgs e)
        {
            if (aloha.Items.Count != 0)
            {

                this.aloha.SelectAllCells();
                this.aloha.ClipboardCopyMode = DataGridClipboardCopyMode.IncludeHeader;
                ApplicationCommands.Copy.Execute(null, this.aloha);

                String result = (string)Clipboard.GetData(DataFormats.Text);
                this.aloha.UnselectAllCells();

                StreamWriter sw = new StreamWriter("raport.xls");
                sw.WriteLine(result);


                sw.Close();
                Process.Start("raport.xls");
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MeniuPrin m = new MeniuPrin();
            m.Show();
        }
    }
}
