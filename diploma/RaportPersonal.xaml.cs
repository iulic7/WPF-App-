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
    /// Interaction logic for RaportPersonal.xaml
    /// </summary>
    public partial class RaportPersonal : Window
    {
        string conn = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + Environment.CurrentDirectory + @"\Personal.mdf;Integrated Security=True;Connect Timeout=30";
        public RaportPersonal()
        {
            InitializeComponent();
            DateTime a = new DateTime(1800, 1, 1);
            DataContext db = new DataContext(conn);

            var pers = db.GetTable<Persoane>().Where(x => x.DataEliberarii==a);
            Table<GrStiinte> titlu = db.GetTable<GrStiinte>();
            Table<GradulDidactic> grad = db.GetTable<GradulDidactic>();
            Table<Stud> studii = db.GetTable<Stud>();

            var cadre = from p in pers
                        join t in titlu on p.idgradstiint equals t.Idgrstiint
                        join g in grad on p.IdGradDidactic equals g.IdGradDidactic
                        join s in studii on p.idStudii equals s.IdStudii
                        where p.EsteProfesor == true
                        select new { p, t.Garad, g.Gradul, s.Studiile };

            List<PersonalDidactic> raport = new List<PersonalDidactic>();
            raport.Add(new PersonalDidactic { Total = cadre.Count(), Barbati = cadre.Where(x => x.p.Gen == true).Count(),
                Femei = cadre.Where(x => x.p.Gen == false).Count(), StSuper = cadre.Where(x => x.Studiile == "Superioare").Count(),
                StMediiSpec = cadre.Where(x => x.Studiile == "Medii speciale").Count(), Vechime_8 = cadre.Where(x => (x.p.VechimeInMunc + Math.Round(Math.Abs((DateTime.Now.Date - x.p.DataAngajarii).Days) / 365.25, 2, MidpointRounding.AwayFromZero)) < 8).Count(),
                Vechime8_13 = cadre.Where(x => (x.p.VechimeInMunc + Math.Round(Math.Abs((DateTime.Now.Date - x.p.DataAngajarii).Days) / 365.25, 2, MidpointRounding.AwayFromZero)) >= 8 && (x.p.VechimeInMunc + Math.Round(Math.Abs((DateTime.Now.Date - x.p.DataAngajarii).Days) / 365.25, 2, MidpointRounding.AwayFromZero)) < 13).Count(),
                Vechime13_18 = cadre.Where(x => (x.p.VechimeInMunc + Math.Round(Math.Abs((DateTime.Now.Date - x.p.DataAngajarii).Days) / 365.25, 2, MidpointRounding.AwayFromZero)) >= 13 && (x.p.VechimeInMunc + Math.Round(Math.Abs((DateTime.Now.Date - x.p.DataAngajarii).Days) / 365.25, 2, MidpointRounding.AwayFromZero)) < 18).Count(),
                Vechime18_ = cadre.Where(x => (x.p.VechimeInMunc + Math.Round(Math.Abs((DateTime.Now.Date - x.p.DataAngajarii).Days) / 365.25, 2, MidpointRounding.AwayFromZero)) >= 18).Count(), CadreDidactice = cadre.Where(x => x.p.DataPrimiriiGrad.Year == DateTime.Now.Year).Count(),
                TitulNormDepl = cadre.Where(x => x.p.ModAngaj == true && x.p.Statutul == "Cu normă întreagă").Count(),
                TitulGradMan = cadre.Where(x => x.p.ModAngaj == true && x.p.StudiiManager == true).Count(), TitulGradDidSup = cadre.Where(x => x.p.ModAngaj == true && x.Gradul == "Gradul Superior").Count(),
                TitulGradDid1 = cadre.Where(x => x.p.ModAngaj == true && x.Gradul == "Gradul 1").Count(), TitulGradDid2 = cadre.Where(x => x.p.ModAngaj == true && x.Gradul == "Gradul 2").Count(), 
                TitulGradStiin = cadre.Where(x => x.p.ModAngaj == true && x.Garad != "Nu are").Count(), TitulFaraGrad = cadre.Where(x => x.p.ModAngaj == true && x.Gradul == "Nu are").Count(),
                CumultCuGrad = cadre.Where(x => x.p.ModAngaj == false && x.Gradul != "Nu are").Count(), CumultFaraGrad = cadre.Where(x => x.p.ModAngaj == false && x.Gradul == "Nu are").Count()});
            RaportGrid.ItemsSource = raport;
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
