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
    /// Interaction logic for RaportCatedre.xaml
    /// </summary>
    public partial class RaportCatedre : Window
    {
        string conn = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + Environment.CurrentDirectory + @"\Personal.mdf;Integrated Security=True;Connect Timeout=30";

        public RaportCatedre()
        {
            InitializeComponent();
            DateTime a = new DateTime(1800, 1, 1);
            DataContext db = new DataContext(conn);
           var pers = db.GetTable<Persoane>().Where(x => x.DataEliberarii == a);
            Table<Catedre> catedre = db.GetTable<Catedre>();
            Table<GrStiinte> titlu = db.GetTable<GrStiinte>();
            Table<GradulDidactic> grad = db.GetTable<GradulDidactic>();


            var person = from p in pers
                         join c in catedre on p.IdCatedra equals c.IdCatedra
                         join g in grad on p.IdGradDidactic equals g.IdGradDidactic
                         join t in titlu on p.idgradstiint equals t.Idgrstiint
                         select new { Titlu = t.Garad, Catedra = c.DenumireCatedra, Grad = g.Gradul };

            List<CatedreRaport> raport = new List<CatedreRaport>();
            raport.Add(new CatedreRaport { Descriere = "Superior", Finante = person.Where(x => x.Grad == "Gradul Superior" && x.Catedra == "Finanțe").Count(), AdministrareaAfacerilor = person.Where(x => x.Grad == "Gradul Superior" && x.Catedra == "Administrarea afacerilor").Count(), Contabilitate = person.Where(x => x.Grad == "Gradul Superior" && x.Catedra == "Contabilitate și analiză economică").Count(), Informatica = person.Where(x => x.Grad == "Gradul Superior" && x.Catedra == "Informatică").Count(), Matematica = person.Where(x => x.Grad == "Gradul Superior" && x.Catedra == "Matematică și fizică").Count(), LimbaComunicare = person.Where(x => x.Grad == "Gradul Superior" && x.Catedra == "Limbă și comunicare").Count(), StiinteSocio = person.Where(x => x.Grad == "Gradul Superior" && x.Catedra == "Stiințe socio-umanistice").Count(), Total = person.Where(x => x.Grad == "Gradul Superior" && x.Catedra == "Finanțe").Count() + person.Where(x => x.Grad == "Gradul Superior" && x.Catedra == "Administrarea afacerilor").Count() + person.Where(x => x.Grad == "Gradul Superior" && x.Catedra == "Contabilitate și analiză economică").Count() + person.Where(x => x.Grad == "Gradul Superior" && x.Catedra == "Informatică").Count() + person.Where(x => x.Grad == "Gradul Superior" && x.Catedra == "Matematică și fizică").Count() + person.Where(x => x.Grad == "Gradul Superior" && x.Catedra == "Limbă și comunicare").Count() + person.Where(x => x.Grad == "Gradul Superior" && x.Catedra == "Stiințe socio-umanistice").Count()});
            raport.Add(new CatedreRaport { Descriere = "Întâi", Finante = person.Where(x => x.Grad == "Gradul 1" && x.Catedra == "Finanțe").Count(), AdministrareaAfacerilor = person.Where(x => x.Grad == "Gradul 1" && x.Catedra == "Administrarea afacerilor").Count(), Contabilitate = person.Where(x => x.Grad == "Gradul 1" && x.Catedra == "Contabilitate și analiză economică").Count(), Informatica = person.Where(x => x.Grad == "Gradul 1" && x.Catedra == "Informatică").Count(), Matematica = person.Where(x => x.Grad == "Gradul 1" && x.Catedra == "Matematică și fizică").Count(), LimbaComunicare = person.Where(x => x.Grad == "Gradul 1" && x.Catedra == "Limbă și comunicare").Count(), StiinteSocio = person.Where(x => x.Grad == "Gradul 1" && x.Catedra == "Stiințe socio-umanistice").Count(), Total = person.Where(x => x.Grad == "Gradul 1" && x.Catedra == "Finanțe").Count() + person.Where(x => x.Grad == "Gradul 1" && x.Catedra == "Administrarea afacerilor").Count() + person.Where(x => x.Grad == "Gradul 1" && x.Catedra == "Contabilitate și analiză economică").Count() + person.Where(x => x.Grad == "Gradul 1" && x.Catedra == "Informatică").Count() + person.Where(x => x.Grad == "Gradul 1" && x.Catedra == "Matematică și fizică").Count() + person.Where(x => x.Grad == "Gradul 1" && x.Catedra == "Limbă și comunicare").Count() + person.Where(x => x.Grad == "Gradul 1" && x.Catedra == "Stiințe socio-umanistice").Count() });
            raport.Add(new CatedreRaport { Descriere = "Doi", Finante = person.Where(x => x.Grad == "Gradul 2" && x.Catedra == "Finanțe").Count(), AdministrareaAfacerilor = person.Where(x => x.Grad == "Gradul 2" && x.Catedra == "Administrarea afacerilor").Count(), Contabilitate = person.Where(x => x.Grad == "Gradul 2" && x.Catedra == "Contabilitate și analiză economică").Count(), Informatica = person.Where(x => x.Grad == "Gradul 2" && x.Catedra == "Informatică").Count(), Matematica = person.Where(x => x.Grad == "Gradul 2" && x.Catedra == "Matematică și fizică").Count(), LimbaComunicare = person.Where(x => x.Grad == "Gradul 2" && x.Catedra == "Limbă și comunicare").Count(), StiinteSocio = person.Where(x => x.Grad == "Gradul 2" && x.Catedra == "Stiințe socio-umanistice").Count(), Total = person.Where(x => x.Grad == "Gradul 2" && x.Catedra == "Finanțe").Count() + person.Where(x => x.Grad == "Gradul 2" && x.Catedra == "Administrarea afacerilor").Count() + person.Where(x => x.Grad == "Gradul 2" && x.Catedra == "Contabilitate și analiză economică").Count() + person.Where(x => x.Grad == "Gradul 2" && x.Catedra == "Informatică").Count() + person.Where(x => x.Grad == "Gradul 2" && x.Catedra == "Matematică și fizică").Count() + person.Where(x => x.Grad == "Gradul 2" && x.Catedra == "Limbă și comunicare").Count() + person.Where(x => x.Grad == "Gradul 2" && x.Catedra == "Stiințe socio-umanistice").Count() });
            raport.Add(new CatedreRaport { Descriere = "Fără grad", Finante = person.Where(x => x.Grad == "Nu are" && x.Catedra == "Finanțe").Count(), AdministrareaAfacerilor = person.Where(x => x.Grad == "Nu are" && x.Catedra == "Administrarea afacerilor").Count(), Contabilitate = person.Where(x => x.Grad == "Nu are" && x.Catedra == "Contabilitate și analiză economică").Count(), Informatica = person.Where(x => x.Grad == "Nu are" && x.Catedra == "Informatică").Count(), Matematica = person.Where(x => x.Grad == "Nu are" && x.Catedra == "Matematică și fizică").Count(), LimbaComunicare = person.Where(x => x.Grad == "Nu are" && x.Catedra == "Limbă și comunicare").Count(), StiinteSocio = person.Where(x => x.Grad == "Nu are" && x.Catedra == "Stiințe socio-umanistice").Count(), Total = person.Where(x => x.Grad == "Nu are" && x.Catedra == "Finanțe").Count() + person.Where(x => x.Grad == "Nu are" && x.Catedra == "Administrarea afacerilor").Count() + person.Where(x => x.Grad == "Nu are" && x.Catedra == "Contabilitate și analiză economică").Count() + person.Where(x => x.Grad == "Nu are" && x.Catedra == "Informatică").Count() + person.Where(x => x.Grad == "Nu are" && x.Catedra == "Matematică și fizică").Count() + person.Where(x => x.Grad == "Nu are" && x.Catedra == "Limbă și comunicare").Count() + person.Where(x => x.Grad == "Nu are" && x.Catedra == "Stiințe socio-umanistice").Count() });
            raport.Add(new CatedreRaport { Descriere = "Doctori", Finante = person.Where(x => x.Titlu == "Doctor" && x.Catedra == "Finanțe").Count(), AdministrareaAfacerilor = person.Where(x => x.Titlu == "Doctor" && x.Catedra == "Administrarea afacerilor").Count(), Contabilitate = person.Where(x => x.Titlu == "Doctor" && x.Catedra == "Contabilitate și analiză economică").Count(), Informatica = person.Where(x => x.Titlu == "Doctor" && x.Catedra == "Informatică").Count(), Matematica = person.Where(x => x.Titlu == "Doctor" && x.Catedra == "Matematică și fizică").Count(), LimbaComunicare = person.Where(x => x.Titlu == "Doctor" && x.Catedra == "Limbă și comunicare").Count(), StiinteSocio = person.Where(x => x.Titlu == "Doctor" && x.Catedra == "Stiințe socio-umanistice").Count(), Total = person.Where(x => x.Titlu == "Doctor" && x.Catedra == "Finanțe").Count() + person.Where(x => x.Titlu == "Doctor" && x.Catedra == "Administrarea afacerilor").Count() + person.Where(x => x.Titlu == "Doctor" && x.Catedra == "Contabilitate și analiză economică").Count() + person.Where(x => x.Titlu == "Doctor" && x.Catedra == "Informatică").Count() + person.Where(x => x.Titlu == "Doctor" && x.Catedra == "Matematică și fizică").Count() + person.Where(x => x.Titlu == "Doctor" && x.Catedra == "Limbă și comunicare").Count() + person.Where(x => x.Titlu == "Doctor" && x.Catedra == "Stiințe socio-umanistice").Count() });
            raport.Add(new CatedreRaport { Descriere = "Master", Finante = person.Where(x => x.Titlu == "Master" && x.Catedra == "Finanțe").Count(), AdministrareaAfacerilor = person.Where(x => x.Titlu == "Master" && x.Catedra == "Administrarea afacerilor").Count(), Contabilitate = person.Where(x => x.Titlu == "Master" && x.Catedra == "Contabilitate și analiză economică").Count(), Informatica = person.Where(x => x.Titlu == "Master" && x.Catedra == "Informatică").Count(), Matematica = person.Where(x => x.Titlu == "Master" && x.Catedra == "Matematică și fizică").Count(), LimbaComunicare = person.Where(x => x.Titlu == "Master" && x.Catedra == "Limbă și comunicare").Count(), StiinteSocio = person.Where(x => x.Titlu == "Master" && x.Catedra == "Stiințe socio-umanistice").Count(), Total = person.Where(x => x.Titlu == "Master" && x.Catedra == "Finanțe").Count() + person.Where(x => x.Titlu == "Master" && x.Catedra == "Administrarea afacerilor").Count() + person.Where(x => x.Titlu == "Master" && x.Catedra == "Contabilitate și analiză economică").Count() + person.Where(x => x.Titlu == "Master" && x.Catedra == "Informatică").Count() + person.Where(x => x.Titlu == "Master" && x.Catedra == "Matematică și fizică").Count() + person.Where(x => x.Titlu == "Master" && x.Catedra == "Limbă și comunicare").Count() + person.Where(x => x.Titlu == "Master" && x.Catedra == "Stiințe socio-umanistice").Count() });

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
