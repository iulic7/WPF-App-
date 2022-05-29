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
    /// Interaction logic for RaportGradDid.xaml
    /// </summary>
    public partial class RaportGradDid : Window
    {
        string conn = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + Environment.CurrentDirectory + @"\Personal.mdf;Integrated Security=True;Connect Timeout=30";
        string PrSup = "";
        string PrGr1 = "";
        string PrGr2 = "";
        string NuAr = "";
        int z = 1;
        int x = 1;
        int c = 1;
        int v = 1;
        public RaportGradDid()
        {
            DateTime elib = new DateTime(1800, 1, 1);
            InitializeComponent();
            DataContext db = new DataContext(conn);
            var pers = db.GetTable<Persoane>().Where(x => x.DataEliberarii==elib);
            Table<GradulDidactic> grad = db.GetTable<GradulDidactic>();


            var persSuper = from p in pers
                            join c in grad on p.IdGradDidactic equals c.IdGradDidactic
                            where c.Gradul == "Gradul Superior"
                            select new { Nume = p.Nume + " " + p.Prenume };

            var persGr1 = from p in pers
                          join c in grad on p.IdGradDidactic equals c.IdGradDidactic
                          where c.Gradul == "Gradul 1"
                          select new { Nume = p.Nume + ' ' + p.Prenume };

            var persGr2 = from p in pers
                          join c in grad on p.IdGradDidactic equals c.IdGradDidactic
                          where c.Gradul == "Gradul 2"
                          select new { Nume = p.Nume + ' ' + p.Prenume };
            var persNu = from p in pers
                         join c in grad on p.IdGradDidactic equals c.IdGradDidactic
                         where c.Gradul == "Nu are"
                         select new { Nume = p.Nume + ' ' + p.Prenume };

            List<Raport3Did> raport = new List<Raport3Did>();


            foreach (var ob in persSuper)
            {

                PrSup += z + ". " + ob.Nume + ",";
                z++;

            }
            foreach (var ob in persGr1)
            {

                PrGr1 += x + ". " + ob.Nume + ",";
                x++;

            }
            foreach (var ob in persGr2)
            {

                PrGr2 += c + ". " + ob.Nume + ",";
                c++;

            }
            foreach (var ob in persNu)
            {

                NuAr += v + ". " + ob.Nume + ",";
                v++;

            }



            string[] sp = PrSup.Split(',');
            string[] gr1 = PrGr1.Split(',');
            string[] gr2 = PrGr2.Split(',');
            string[] far = NuAr.Split(',');

            if (sp.Length >= gr1.Length && sp.Length >= gr2.Length && sp.Length >= far.Length)
            {
                Array.Resize(ref gr1, sp.Length);
                Array.Resize(ref gr2, sp.Length);
                Array.Resize(ref far, sp.Length);
            }
            else if (gr1.Length >= sp.Length && gr1.Length >= gr2.Length && gr1.Length >= far.Length)
            {
                Array.Resize(ref sp, gr1.Length);
                Array.Resize(ref gr2, gr1.Length);
                Array.Resize(ref far, gr1.Length);
            }
            else if (gr2.Length >= sp.Length && gr2.Length >= gr1.Length && gr2.Length >= far.Length)
            {
                Array.Resize(ref sp, gr2.Length);
                Array.Resize(ref gr1, gr2.Length);
                Array.Resize(ref far, gr2.Length);
            }
            else if (far.Length >= sp.Length && far.Length >= gr1.Length && far.Length >= gr2.Length)
            {
                Array.Resize(ref sp, far.Length);
                Array.Resize(ref gr1, far.Length);
                Array.Resize(ref gr2, far.Length);
            }


            for (int a = 0; a < gr1.Length; a++)
            {
                raport.Add(new Raport3Did { Superior = sp[a], Gr1 = gr1[a], Gr2 = gr2[a], Fara = far[a] });
            }



            raport.Add(new Raport3Did { Superior = "Total: " + persSuper.Count().ToString(), Gr1 = "Total: " + persGr1.Count().ToString(), Gr2 = "Total: " + persGr2.Count().ToString(), Fara = "Total: " + persNu.Count().ToString() });
            int tot = persSuper.Count() + persGr1.Count() + persGr2.Count() + persNu.Count();
            raport.Add(new Raport3Did { Superior = "Total: " + tot + " cadre didactice" });
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
