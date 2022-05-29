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

namespace diploma
{
    /// <summary>
    /// Interaction logic for ProfsoriDiscipline.xaml
    /// </summary>
    public partial class ProfsoriDiscipline : Window
    {

        int IDDiscip = 0;
        int IDProf = 0;

        string discip;
        string profn;
        string profp;

        string conn = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + Environment.CurrentDirectory + @"\Personal.mdf;Integrated Security=True;Connect Timeout=30";
        public ProfsoriDiscipline()
        {
            InitializeComponent();
            DataContext db = new DataContext(conn);
            Table<ProfDisc> pd = db.GetTable<ProfDisc>();
            Table<Persoane> pers = db.GetTable<Persoane>();
            Table<Disciplin> dis = db.GetTable<Disciplin>();
            var prdis = from l in pd
                        join p in pers on l.idPerson equals p.IdPerson
                        join d in dis on l.idDiscip equals d.IdDiscip
                        select new
                        {
                            Disciplina = d.Denumirea,
                            Numele = p.Nume,
                            Prenume = p.Prenume
                        };
            profgrid.ItemsSource = prdis.ToList();


            var list = from d in dis
                       select d.Denumirea;
            Disciplina.ItemsSource = list.ToList();

            var list2 = from p in pers
                        orderby p.Nume
                        select

                          p.Nume + " " + p.Prenume;


            profesori.ItemsSource = list2.ToList();

        }

        private void AdaugaDenprof_Click(object sender, RoutedEventArgs e)
        {
            DataContext db = new DataContext(conn);
            Table<ProfDisc> pd = db.GetTable<ProfDisc>();
            Table<Persoane> pers = db.GetTable<Persoane>();
            Table<Disciplin> dis = db.GetTable<Disciplin>();
            string err = "";
            if (Disciplina.Text == "")
            {
                err += "Alegeți funcția\n";
            }
            if (profesori.Text == "")
            {
                err += "Alegeți persoana\n";
            }
            if (err == "")
            {


                var list = from d in dis
                           select new { ID = d.IdDiscip, Disciplina = d.Denumirea };
                foreach (var l in list)
                {
                    if (l.Disciplina.ToString() == Disciplina.Text)
                    {
                        IDDiscip = l.ID;
                    }
                }

                string a = profesori.Text;
                string[] prof = a.Split(' ');

                var list2 = from p in pers
                            select new { ID = p.IdPerson, Nume = p.Nume, Prenume = p.Prenume };
                foreach (var l in list2)
                {
                    if (l.Nume.ToString() == prof[0] && l.Prenume.ToString() == prof[1])
                    {
                        IDProf = l.ID;
                    }
                }
                var verific = db.GetTable<ProfDisc>().Where(x => x.idDiscip == IDDiscip).Where(x => x.idPerson == IDProf);

                if (verific.Count() == 0)
                {
                    ProfDisc newprof = new ProfDisc
                    {
                        idDiscip = IDDiscip,
                        idPerson = IDProf,
                    };
                    db.GetTable<ProfDisc>().InsertOnSubmit(newprof);
                    db.SubmitChanges();
                    var prdis = from l in pd
                                join p in pers on l.idPerson equals p.IdPerson
                                join d in dis on l.idDiscip equals d.IdDiscip
                                select new
                                {
                                    Disciplina = d.Denumirea,
                                    Numele = p.Nume,
                                    Prenume = p.Prenume
                                };
                    profgrid.ItemsSource = prdis.ToList();

                    profesori.SelectedIndex = -1;
                    Disciplina.SelectedIndex = -1;
                }
                else MessageBox.Show("Acest profesor deja este introdus la această disciplină", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
          else  MessageBox.Show(err, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void Modifica(object sender, RoutedEventArgs e)
        {
            DataContext db = new DataContext(conn);
            Table<ProfDisc> pd = db.GetTable<ProfDisc>();
            Table<Persoane> pers = db.GetTable<Persoane>();
            Table<Disciplin> dis = db.GetTable<Disciplin>();
            if (profgrid.SelectedItems.Count > 0)
            {

                for (int i = 0; i < profgrid.SelectedItems.Count; i++)
                {

                    var list = from l in pd
                               join p in pers on l.idPerson equals p.IdPerson
                               join d in dis on l.idDiscip equals d.IdDiscip
                               select new
                               {
                                   Disciplina = d.Denumirea,
                                   Numele = p.Nume,
                                   Prenume = p.Prenume
                               };

                    foreach (var c in list)
                    {

                        if (c.ToString() == profgrid.SelectedItem.ToString())
                        {
                            Disciplina.Text = c.Disciplina;
                            profesori.Text = c.Numele + " " + c.Prenume;
                        }
                    }
                }
                var list2 = from d in dis
                            select new { ID = d.IdDiscip, Disciplina = d.Denumirea };
                foreach (var l in list2)
                {
                    if (l.Disciplina.ToString() == Disciplina.Text)
                    {
                        IDDiscip = l.ID;
                    }
                }

                string a = profesori.Text;
                string[] prof = a.Split(' ');

                var list3 = from p in pers
                            select new { ID = p.IdPerson, Nume = p.Nume, Prenume = p.Prenume };
                foreach (var l in list3)
                {
                    if (l.Nume.ToString() == prof[0] && l.Prenume.ToString() == prof[1])
                    {
                        IDProf = l.ID;
                    }
                }

                AdaugaDenprof.Visibility = Visibility.Hidden;
                ModificDenprof.Visibility = Visibility.Visible;
                Anuleaza.Visibility = Visibility.Visible;
            }
            else
                MessageBox.Show("Alegeți o înregistrare pentru a o modifica", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        private void ModificDenprof_Click(object sender, RoutedEventArgs e)
        {

            DataContext db = new DataContext(conn);
            Table<ProfDisc> pd = db.GetTable<ProfDisc>();
            Table<Persoane> pers = db.GetTable<Persoane>();
            Table<Disciplin> dis = db.GetTable<Disciplin>();



            foreach (var f in pd)
            {
                if (f.idDiscip == IDDiscip && f.idPerson == IDProf)
                {
                    var list = from d in dis
                               select new { ID = d.IdDiscip, Disciplina = d.Denumirea };
                    foreach (var l in list)
                    {
                        if (l.Disciplina.ToString() == Disciplina.Text)
                        {
                            IDDiscip = l.ID;
                        }
                    }

                    string a = profesori.Text;
                    string[] prof = a.Split(' ');

                    var list2 = from p in pers
                                select new { ID = p.IdPerson, Nume = p.Nume, Prenume = p.Prenume };
                    foreach (var l in list2)
                    {
                        if (l.Nume.ToString() == prof[0] && l.Prenume.ToString() == prof[1])
                        {
                            IDProf = l.ID;
                        }
                    }

                    var verific = db.GetTable<ProfDisc>().Where(x => x.idDiscip == IDDiscip).Where(x => x.idPerson == IDProf);

                    if (verific.Count() == 0)
                    {

                        f.idDiscip = IDDiscip;
                        f.idPerson = IDProf;
                        db.SubmitChanges();
                        MessageBox.Show("Datele au fost modificate", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                        var prdis = from l in pd
                                    join p in pers on l.idPerson equals p.IdPerson
                                    join d in dis on l.idDiscip equals d.IdDiscip
                                    select new
                                    {
                                        Disciplina = d.Denumirea,
                                        Numele = p.Nume,
                                        Prenume = p.Prenume
                                    };
                        profgrid.ItemsSource = prdis.ToList();
                        ModificDenprof.Visibility = Visibility.Hidden;
                        AdaugaDenprof.Visibility = Visibility.Visible;
                        Anuleaza.Visibility = Visibility.Hidden;

                        profesori.SelectedIndex = -1;
                        Disciplina.SelectedIndex = -1;

                        break;
                    }
                    else MessageBox.Show("Acest profesor deja este introdus la această disciplină", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                    break;



                }
            }



        }

        private void Sterge(object sender, RoutedEventArgs e)
        {

            DataContext db = new DataContext(conn);
            Table<ProfDisc> pd = db.GetTable<ProfDisc>();
            Table<Persoane> pers = db.GetTable<Persoane>();
            Table<Disciplin> dis = db.GetTable<Disciplin>();

            if (profgrid.SelectedItems.Count > 0)
            {
                bool deleted = true;
                for (int i = 0; i < profgrid.SelectedItems.Count; i++)
                {
                    var list = from l in pd
                               join p in pers on l.idPerson equals p.IdPerson
                               join d in dis on l.idDiscip equals d.IdDiscip
                               select new
                               {
                                   Disciplina = d.Denumirea,
                                   Numele = p.Nume,
                                   Prenume = p.Prenume
                               };

                    foreach (var c in list)
                    {

                        if (c.ToString() == profgrid.SelectedItem.ToString())
                        {
                            deleted = false;
                            discip = c.Disciplina;
                            profn = c.Numele;
                            profp = c.Prenume;

                        }
                    }
                }

                if (!deleted)
                {
                    var list2 = from d in dis
                                select new { ID = d.IdDiscip, Disciplina = d.Denumirea };
                    foreach (var l in list2)
                    {
                        if (l.Disciplina.ToString() == discip)
                        {
                            IDDiscip = l.ID;
                        }
                    }



                    var list3 = from p in pers
                                select new { ID = p.IdPerson, Nume = p.Nume, Prenume = p.Prenume };
                    foreach (var l in list3)
                    {
                        if (l.Nume.ToString() == profn && l.Prenume.ToString() == profp)
                        {
                            IDProf = l.ID;
                        }
                    }

                    var Result = MessageBox.Show("Doriți să ștergeți această înregistrare?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Question);

                    if (Result == MessageBoxResult.Yes)
                    {
                        var profdel = db.GetTable<ProfDisc>().Where(u => u.idDiscip == IDDiscip).Where(u => u.idPerson == IDProf).FirstOrDefault();
                        db.GetTable<ProfDisc>().DeleteOnSubmit(profdel);
                        db.SubmitChanges();
                        var prdis = from l in pd
                                    join p in pers on l.idPerson equals p.IdPerson
                                    join d in dis on l.idDiscip equals d.IdDiscip
                                    select new
                                    {
                                        Disciplina = d.Denumirea,
                                        Numele = p.Nume,
                                        Prenume = p.Prenume
                                    };
                        profgrid.ItemsSource = prdis.ToList();
                    }
                }
                else
                {
                    MessageBox.Show("Această înregistrare a fost ștearsă anterior", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    var prdis = from l in pd
                                join p in pers on l.idPerson equals p.IdPerson
                                join d in dis on l.idDiscip equals d.IdDiscip
                                select new
                                {
                                    Disciplina = d.Denumirea,
                                    Numele = p.Nume,
                                    Prenume = p.Prenume
                                };
                    profgrid.ItemsSource = prdis.ToList();
                }
            }
            else
                MessageBox.Show("Alegeți o înregistrare pentru a o șterge", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        private void DicipForm_Click(object sender, RoutedEventArgs e) 
        {
            DisciplineForm a = new DisciplineForm();
            a.Show();
           
        }

        private void Anuleaza_Click(object sender, RoutedEventArgs e)
        {
            AdaugaDenprof.Visibility = Visibility.Visible;
            ModificDenprof.Visibility = Visibility.Hidden;
            profesori.SelectedIndex = -1;
            Disciplina.SelectedIndex = -1;
            Anuleaza.Visibility = Visibility.Hidden;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
           
                MeniuPrin m = new MeniuPrin();
                m.Show();
       
        }

        private void Disciplina_DropDownOpened(object sender, EventArgs e)
        {
            DataContext db = new DataContext(conn);
       
            Table<Disciplin> dis = db.GetTable<Disciplin>();
            var list = from d in dis
                       select d.Denumirea;
            Disciplina.ItemsSource = list.ToList();
        }
    }
}
