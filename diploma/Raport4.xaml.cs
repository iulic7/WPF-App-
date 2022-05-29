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
using System.Data.Linq;
using diploma.Clase;
using System.IO;
using System.Diagnostics;
namespace diploma
{
    /// <summary>
    /// Interaction logic for Raport4.xaml
    /// </summary>
    public partial class Raport4 : Window
    {
        string conn = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + Environment.CurrentDirectory + @"\Personal.mdf;Integrated Security=True;Connect Timeout=30";
        public Raport4()
        {
            InitializeComponent();
            DateTime a = new DateTime(1800, 1, 1);
            DataContext db = new DataContext(conn);
            var pers = db.GetTable<Persoane>().Where(x => x.DataEliberarii == a);
            Table<Stud> studii = db.GetTable<Stud>();
            Table<Catedre> catedre = db.GetTable<Catedre>();
            Table<Functii> functii = db.GetTable<Functii>();
            Table<Disciplin> discipline = db.GetTable<Disciplin>();
            Table<ProfDisc> intermediar = db.GetTable<ProfDisc>();
            Table<PersoanaFunc> fpers = db.GetTable<PersoanaFunc>();

            var perstot = from p in pers
                          join fp in fpers on p.IdPerson equals fp.IdPerson
                          join f in functii on fp.IdFunctie equals f.IdFunctie
                          select new { p, fp.Unitati };
            var persm = from p in perstot
                        where p.p.Gen == true
                        select p;
            var persf = from p in perstot
                        where p.p.Gen == false
                        select p;
            var persCon = from p in pers
                          join fp in fpers on p.IdPerson equals fp.IdPerson
                          join f in functii on fp.IdFunctie equals f.IdFunctie
                          join c in catedre on p.IdCatedra equals c.IdCatedra
                          where c.DenumireCatedra == "Personal de conducere" && f.DenFunctie != "Profesor"
                          select new { p, fp.Unitati };
            var persCon1 = from p in persCon
                           where p.p.StudiiManager == true
                           select p;
            var persdid = from p in pers
                          join fp in fpers on p.IdPerson equals fp.IdPerson
                          join c in catedre on p.IdCatedra equals c.IdCatedra
                          join f in functii on fp.IdFunctie equals f.IdFunctie
                          where (p.EsteProfesor == true || c.DenumireCatedra == "Maestru") && f.DenFunctie != "Director" && f.DenFunctie != "Șef secție" && f.DenFunctie != "Director adjunct"
                          select new { p, fp.Unitati };
            var perstit = from p in persdid
                          where p.p.ModAngaj == true
                          select p;
            var perscum = from p in persdid
                          where p.p.ModAngaj == false
                          select p;
            var perspens = from p in persdid
                           where p.p.VirstaPens == true
                           select p;
            var persdidaux = from p in pers
                             join fp in fpers on p.IdPerson equals fp.IdPerson
                             join f in functii on fp.IdFunctie equals f.IdFunctie
                             join c in catedre on p.IdCatedra equals c.IdCatedra
                             where c.DenumireCatedra == "Personal didactic auxiliar" && f.DenFunctie != "Profesor"
                             select new { p, fp.Unitati };
            var persaux = from p in pers
                          join fp in fpers on p.IdPerson equals fp.IdPerson
                          join f in functii on fp.IdFunctie equals f.IdFunctie
                          join c in catedre on p.IdCatedra equals c.IdCatedra
                          where c.DenumireCatedra == "Personal auxiliar"
                          select new { p, fp.Unitati };
            double aux;
            if (persdidaux.Count() == 0)
            {
                aux = 0;
            }
            else
            {
                aux = persdidaux.Sum(x => x.Unitati);
            }
            double aux2;
            if (persaux.Count() == 0)
            {
                aux2 = 0;
            }
            else
            {
                aux2 = persaux.Sum(x => x.Unitati);
            }
            double tot;
            if (perstot.Count() == 0)
            {
                tot = 0;
            }
            else
            {
                tot = perstot.Sum(x => x.Unitati);
            }
            double cond;
            if (persCon.Count() == 0)
            {
                cond = 0;
            }
            else
            {
                cond = persCon.Sum(x => x.Unitati);
            }
            double didac;
            if (persdid.Count() == 0)
            {
                didac = 0;
            }
            else
            {
                didac = persdid.Sum(x => x.Unitati);
            }
            List<Raport4per> date = new List<Raport4per>();
            date.Add(new Raport4per { NrUnitTotal = tot, NrPersTot = perstot.GroupBy(x => x.p.IDNP).Count(), BarbTot = persm.GroupBy(x => x.p.IDNP).Count(), FemTot = persf.GroupBy(x => x.p.IDNP).Count(), Cond = cond, CondPers = persCon.GroupBy(X => X.p.IDNP).Count(), CondMan = persCon1.GroupBy(X => X.p.IDNP).Count(), Did = didac, DidTit = perstit.GroupBy(X => X.p.IDNP).Count(), DidCum = perscum.GroupBy(X => X.p.IDNP).Count(), DidPens = perspens.GroupBy(X => X.p.IDNP).Count(), UnitTot2 = 00, Nrdeunitpers = 00, AuxNrTot = aux, TotPersNedidact = persdidaux.GroupBy(X => X.p.IDNP).Count(), NeDidTOt = aux2, AuxPers = persaux.GroupBy(X => X.p.IDNP).Count() });

            RaportGrid.ItemsSource = date;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MeniuPrin m = new MeniuPrin();
            m.Show();
        }

        private void Export_Click(object sender, RoutedEventArgs e)
        {
            if (RaportGrid.Items.Count != 0)
            {

                this.RaportGrid.SelectAllCells();
                this.RaportGrid.ClipboardCopyMode = DataGridClipboardCopyMode.IncludeHeader;
                ApplicationCommands.Copy.Execute(null, this.RaportGrid);

                String result = (string)Clipboard.GetData(DataFormats.Text);
                this.RaportGrid.UnselectAllCells();

                StreamWriter sw = new StreamWriter("raport.xls");
                sw.WriteLine(result);


                sw.Close();
                Process.Start("raport.xls");
            }
        }
    }
}

