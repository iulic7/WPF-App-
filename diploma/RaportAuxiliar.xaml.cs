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
using System.IO;
using System.Diagnostics;
using diploma.Clase;

namespace diploma
{
    /// <summary>
    /// Interaction logic for RaportAuxiliar.xaml
    /// </summary>
    public partial class RaportAuxiliar : Window
    {
        string conn = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + Environment.CurrentDirectory + @"\Personal.mdf;Integrated Security=True;Connect Timeout=30";

        public RaportAuxiliar()
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
           
            var persTot = from p in pers
                          join c in catedre on p.IdCatedra equals c.IdCatedra
                          where c.DenumireCatedra != "Personal auxiliar" && c.DenumireCatedra != "Personal didactic auxiliar"
                          select p;

            var persConducere = from p in pers
                                join c in catedre on p.IdCatedra equals c.IdCatedra
                                where c.DenumireCatedra == "Personal de conducere"
                                select p;

            var persConducere2 = from p in persConducere
                                 where p.EsteProfesor == true 
                                 select p;

            var persDidactic = from p in pers
                               join c in catedre on p.IdCatedra equals c.IdCatedra
                               where p.EsteProfesor == true && c.DenumireCatedra != "Personal de conducere" && c.DenumireCatedra != "Personal didactic auxiliar"
                               select p;

            var persDidacticProf = from p in persDidactic
                                   join fp in fpers on p.IdPerson equals fp.IdPerson
                                   join f in functii on fp.IdFunctie equals f.IdFunctie
                                   join c in catedre on p.IdCatedra equals c.IdCatedra
                                   where  p.EsteProfesor == true && c.DenumireCatedra== "Contabilitate și analiză economică" || c.DenumireCatedra == "Finanțe" || c.DenumireCatedra == "Administrarea afacerilor" || c.DenumireCatedra == "Informatică" || c.DenumireCatedra == "Matematică și fizică" || c.DenumireCatedra == "Stiințe socio-umanistice" || c.DenumireCatedra == "Limbă și comunicare"
                                   select p;

            var persDidacticProfGeneral = from p in persDidacticProf
                                          join i in intermediar on p.IdPerson equals i.idPerson
                                          join d in discipline on i.idDiscip equals d.IdDiscip
                                          where d.Denumirea.Contains("Limba Română") || d.Denumirea.Contains("Matematica") || d.Denumirea.Contains("Geografia") || d.Denumirea.Contains("Limba Engleză") || d.Denumirea.Contains("Limba Franceză") || d.Denumirea.Contains("Biologia") || d.Denumirea.Contains("Fizica") || d.Denumirea.Contains("Chimia") || d.Denumirea.Contains("Informatica") || d.Denumirea.Contains("Educația Civică") || d.Denumirea.Contains("Edicația fizică") || d.Denumirea.Contains("Istoria")
                                          select p;

            var persDidacticMaestru = from p in persDidactic
                                      join fp in fpers on p.IdPerson equals fp.IdPerson
                                      join c in catedre on p.IdCatedra equals c.IdCatedra
                                      where c.DenumireCatedra == "Maestru"
                                      select p;

            var alPersDidactic = from p in persDidactic
                                 join fp in fpers on p.IdPerson equals fp.IdPerson
                                 join f in functii on fp.IdFunctie equals f.IdFunctie
                                 join c in catedre on p.IdCatedra equals c.IdCatedra
                                 where p.EsteProfesor==true && c.DenumireCatedra == "Alt personal didactic"
                                 select p;

            var persAux = from p in pers
                          join c in catedre on p.IdCatedra equals c.IdCatedra
                          where c.DenumireCatedra == "Personal didactic auxiliar"
                          select p;

            var list1 = from p in persTot
                        join s in studii on p.idStudii equals s.IdStudii
                        where s.Studiile == "Superioare"
                        select p;
            var list2 = from p in persConducere
                        join s in studii on p.idStudii equals s.IdStudii
                        where s.Studiile == "Superioare"
                        select p;
            var list3 = from p in persConducere2
                        join s in studii on p.idStudii equals s.IdStudii
                        where s.Studiile == "Superioare"
                        select p;
            var list4 = from p in persDidactic
                        join s in studii on p.idStudii equals s.IdStudii
                        where s.Studiile == "Superioare"
                        select p;
            var list5 = from p in persDidacticProf
                        join s in studii on p.idStudii equals s.IdStudii
                        where s.Studiile == "Superioare"
                        select p;
            var list6 = from p in persDidacticProfGeneral
                        join s in studii on p.idStudii equals s.IdStudii
                        where s.Studiile == "Superioare"
                        select p;
            var list7 = from p in persDidacticMaestru
                        join s in studii on p.idStudii equals s.IdStudii
                        where s.Studiile == "Superioare"
                        select p;
            var list8 = from p in alPersDidactic
                        join s in studii on p.idStudii equals s.IdStudii
                        where s.Studiile == "Superioare"
                        select p;
            var list9 = from p in persAux
                        join s in studii on p.idStudii equals s.IdStudii
                        where s.Studiile == "Superioare"
                        select p;

            List<Raport1> raport = new List<Raport1>();
            raport.Add(new Raport1 { Descriere = "Total", PersonalDeBaza = persTot.Count(), StudiiSuperioare = list1.Count(), Femei = persTot.Where(x => x.Gen == false).Count(), NormaIntreaga = persTot.Where(x => x.Statutul == "Cu normă întreagă").Count(), NormaPartiala = persTot.Where(x => x.Statutul == "Cu normă parțială").Count(), Cumulare = persTot.Where(x => x.ModAngaj == false).Count() });
            raport.Add(new Raport1 { Descriere = "Personal de conducere (director, director adjunct, șef secție)", PersonalDeBaza = persConducere.Count(), StudiiSuperioare = list2.Count(), Femei = persConducere.Where(x => x.Gen == false).Count(), NormaIntreaga = persConducere.Where(x => x.Statutul == "Cu normă întreagă").Count(), NormaPartiala = persConducere.Where(x => x.Statutul == "Cu normă parțială").Count(), Cumulare = persConducere.Where(x => x.ModAngaj == false).Count() });
            raport.Add(new Raport1 { Descriere = "  din care cu norme didactice", PersonalDeBaza = persConducere2.Count(), StudiiSuperioare = list3.Count(), Femei = persConducere2.Where(x => x.Gen == false).Count(), NormaIntreaga = persConducere2.Where(x => x.Statutul == "Cu normă întreagă").Count(), NormaPartiala = persConducere2.Where(x => x.Statutul == "Cu normă parțială").Count(), Cumulare = persConducere2.Where(x => x.ModAngaj == false).Count() });
            raport.Add(new Raport1 { Descriere = "Personal didactic", PersonalDeBaza = persDidactic.Count(), StudiiSuperioare = list4.Count(), Femei = persDidactic.Where(x => x.Gen == false).Count(), NormaIntreaga = persDidactic.Where(x => x.Statutul == "Cu normă întreagă").Count(), NormaPartiala = persDidactic.Where(x => x.Statutul == "Cu normă parțială").Count(), Cumulare = persDidactic.Where(x => x.ModAngaj == false).Count() });
            raport.Add(new Raport1 { Descriere = "  profesori", PersonalDeBaza = persDidacticProf.Count(), StudiiSuperioare = list5.Count(), Femei = persDidacticProf.Where(x => x.Gen == false).Count(), NormaIntreaga = persDidacticProf.Where(x => x.Statutul == "Cu normă întreagă").Count(), NormaPartiala = persDidacticProf.Where(x => x.Statutul == "Cu normă parțială").Count(), Cumulare = persDidacticProf.Where(x => x.ModAngaj == false).Count() });
            raport.Add(new Raport1 { Descriere = "     din care profesori disciplini generale", PersonalDeBaza = persDidacticProfGeneral.Count(), StudiiSuperioare = list6.Count(), Femei = persDidacticProfGeneral.Where(x => x.Gen == false).Count(), NormaIntreaga = persDidacticProfGeneral.Where(x => x.Statutul == "Cu normă întreagă").Count(), NormaPartiala = persDidacticProfGeneral.Where(x => x.Statutul == "Cu normă parțială").Count(), Cumulare = persDidacticProfGeneral.Where(x => x.ModAngaj == false).Count() });
            raport.Add(new Raport1 { Descriere = "  maeștri-intructori", PersonalDeBaza = persDidacticMaestru.Count(), StudiiSuperioare = list7.Count(), Femei = persDidacticMaestru.Where(x => x.Gen == false).Count(), NormaIntreaga = persDidacticMaestru.Where(x => x.Statutul == "Cu normă întreagă").Count(), NormaPartiala = persDidacticMaestru.Where(x => x.Statutul == "Cu normă parțială").Count(), Cumulare = persDidacticMaestru.Where(x => x.ModAngaj == false).Count() });
            raport.Add(new Raport1 { Descriere = "  alte persoane didactice (metodist, psiholog, psihopedagog, maestru de concert, conducător de cerc, conducător de cor, dirijor de orchestră)", PersonalDeBaza = alPersDidactic.Count(), StudiiSuperioare = list8.Count(), Femei = alPersDidactic.Where(x => x.Gen == false).Count(), NormaIntreaga = alPersDidactic.Where(x => x.Statutul == "Cu normă întreagă").Count(), NormaPartiala = alPersDidactic.Where(x => x.Statutul == "Cu normă parțială").Count(), Cumulare = alPersDidactic.Where(x => x.ModAngaj == false).Count() });
            raport.Add(new Raport1 { Descriere = "Personal didactic auxiliar (bibliotecar, laborant și pedagogul social din căminele pentru elevi)", PersonalDeBaza = persAux.Count(), StudiiSuperioare = list9.Count(), Femei = persAux.Where(x => x.Gen == false).Count(), NormaIntreaga = persAux.Where(x => x.Statutul == "Cu normă întreagă").Count(), NormaPartiala = persAux.Where(x => x.Statutul == "Cu normă parțială").Count(), Cumulare = persAux.Where(x => x.ModAngaj == false).Count() });

            RaportGrid.ItemsSource = raport.ToList();
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

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MeniuPrin m = new MeniuPrin();
            m.Show();
        }
    }
}