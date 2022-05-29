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
    /// Interaction logic for ActivitateaDidiact.xaml
    /// </summary>
    public partial class ActivitateaDidiact : Window
    {
   
        int IDProf = 0;
        string IDactiv = "";
        string conn = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + Environment.CurrentDirectory + @"\Personal.mdf;Integrated Security=True;Connect Timeout=30";
        public ActivitateaDidiact()
        {
            InitializeComponent();
            DataContext db = new DataContext(conn);
            Table<Activ> ac = db.GetTable<Activ>();
            Table<Persoane> pers = db.GetTable<Persoane>();
            var prdis = from a in ac
                        join p in pers on a.IdPerson equals p.IdPerson
                        select new
                        {
                            Activitatea = a.ActivitDidact,
                            Numele = p.Nume,
                            Prenume = p.Prenume
                        };
            actgrid.ItemsSource = prdis.ToList();


         

            var list2 = from p in pers
                        orderby p.Nume
                        select
                          p.Nume + " " + p.Prenume;


            profesori.ItemsSource = list2.ToList();
        }

        private void AdaugaActiv_Click(object sender, RoutedEventArgs e)
        {
            DataContext db = new DataContext(conn);

            Table<Activ> ac = db.GetTable<Activ>();
            Table<Persoane> pers = db.GetTable<Persoane>();
            string err = "";
            Regex regInstitutii = new Regex("^[A-Za-zĂÎÂȘȚăîâșț ]{1,}$");
            if (!regInstitutii.IsMatch(atext.Text))
            {
                err += "Date incorecte la introducere (Se acceptă doar litere și spații) \n";
            }
            if (profesori.Text=="")
            {
                err += "Selectați persoana \n";
            }
            if (err == "")
            {
                int err2 = 0;

                string a = profesori.Text;
                string[] prof = a.Split(' ');

                var prdis = from b in ac
                            join p in pers on b.IdPerson equals p.IdPerson
                            select new
                            {
                                Activitatea = b.ActivitDidact,
                                Numele = p.Nume,
                                Prenume = p.Prenume
                            };
                foreach (var p in prdis)
                {
                    if (p.Activitatea == atext.Text && prof[0] == p.Numele && prof[1] == p.Prenume)
                        err2++;
                }
                if (err2 != 0)
                {
                    MessageBox.Show("Pentru această persoană este deja înscrisă această activitate didactică", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else
                {

                    var list2 = from p in pers
                                select new { ID = p.IdPerson, Nume = p.Nume, Prenume = p.Prenume };
                    foreach (var l in list2)
                    {
                        if (l.Nume.ToString() == prof[0] && l.Prenume.ToString() == prof[1])
                        {
                            IDProf = l.ID;
                        }
                    }

                    Activ newprof = new Activ
                    {
                        ActivitDidact = atext.Text,
                        IdPerson = IDProf,
                    };
                    db.GetTable<Activ>().InsertOnSubmit(newprof);
                    db.SubmitChanges();
                    actgrid.ItemsSource = prdis.ToList();
                    atext.Text = "";
                    profesori.SelectedIndex = -1;
                }
                
            }
            else MessageBox.Show(err, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            
        }
        

        private void Modifica(object sender, RoutedEventArgs e)
        {
            DataContext db = new DataContext(conn);
            Table<Activ> ac = db.GetTable<Activ>();
            Table<Persoane> pers = db.GetTable<Persoane>();
            if (actgrid.SelectedItems.Count > 0)
            {

                for (int i = 0; i < actgrid.SelectedItems.Count; i++)
                {

                    var list = from b in ac
                               join p in pers on b.IdPerson equals p.IdPerson
                               select new
                               {
                                   Activitatea = b.ActivitDidact,
                                   Numele = p.Nume,
                                   Prenume = p.Prenume
                               };

                    foreach (var c in list)
                    {
                        if (c.ToString() == actgrid.SelectedItem.ToString())
                        {
                            atext.Text = c.Activitatea;
                            IDactiv = c.Activitatea;
                            profesori.Text = c.Numele + " " + c.Prenume;
                        }
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

                AdaugaActiv.Visibility = Visibility.Hidden;
                Anuleaza.Visibility = Visibility.Visible;
                ModificActiv.Visibility = Visibility.Visible;
            }
            else
                MessageBox.Show("Alegeți o înregistrare pentru a o modifica", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        private void ModificActiv_Click(object sender, RoutedEventArgs e)
        {
            DataContext db = new DataContext(conn);
            Table<Activ> ac = db.GetTable<Activ>();
            Table<Persoane> pers = db.GetTable<Persoane>();

            string err = "";
            Regex regInstitutii = new Regex("^[A-Za-zĂÎÂȘȚăîâșț ]{1,}$");
            if (!regInstitutii.IsMatch(atext.Text))
            {
                err += "Sunt introduse date incorecte \n";
            }
            if (err == "")
            {

                foreach (var f in ac)
                {
                    if (f.ActivitDidact == IDactiv && f.IdPerson == IDProf)
                    {
                        int err2 = 0;

                        string a = profesori.Text;
                        string[] prof = a.Split(' ');

                        var prdis = from b in ac
                                    join p in pers on b.IdPerson equals p.IdPerson
                                    select new
                                    {
                                        Activitatea = b.ActivitDidact,
                                        Numele = p.Nume,
                                        Prenume = p.Prenume
                                    };

                        foreach (var p in prdis)
                        {
                            if (p.Activitatea == atext.Text && prof[0] == p.Numele && prof[1] == p.Prenume)
                                err2++;
                        }
                        if (err2 != 0)
                        {
                            MessageBox.Show("Pentru această persoană este deja inscrisă această activitate didactică", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                        else
                        {

                            var list2 = from p in pers
                                        select new { ID = p.IdPerson, Nume = p.Nume, Prenume = p.Prenume };
                            foreach (var l in list2)
                            {
                                if (l.Nume.ToString() == prof[0] && l.Prenume.ToString() == prof[1])
                                {
                                    IDProf = l.ID;
                                }
                            }
                            f.ActivitDidact = atext.Text;
                            f.IdPerson = IDProf;
                            db.SubmitChanges();
                            MessageBox.Show("Datele au fost modificate", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                            actgrid.ItemsSource = prdis.ToList();
                            ModificActiv.Visibility = Visibility.Hidden;
                            AdaugaActiv.Visibility = Visibility.Visible;
                            Anuleaza.Visibility = Visibility.Hidden;
                            atext.Text = "";
                            profesori.SelectedIndex = -1;
                        }

                        break;
                    }
                }
            }
            else MessageBox.Show("Date incorecte la introducere (Se acceptă doar litere și spații)", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void Sterge(object sender, RoutedEventArgs e)
        {
            DataContext db = new DataContext(conn);
            Table<Activ> ac = db.GetTable<Activ>();
            Table<Persoane> pers = db.GetTable<Persoane>();

            if (actgrid.SelectedItems.Count > 0)
            {

                for (int i = 0; i < actgrid.SelectedItems.Count; i++)
                {

                    var list = from b in ac
                               join p in pers on b.IdPerson equals p.IdPerson
                               select new
                               {
                                   Activitatea = b.ActivitDidact,
                                   Numele = p.Nume,
                                   Prenume = p.Prenume
                               };
                    foreach (var c in list)
                    {

                        if (c.ToString() == actgrid.SelectedItem.ToString())
                        {
                            IDactiv = c.Activitatea;
                            profesori.Text = c.Numele + " " + c.Prenume;
                        }
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
                var Result = MessageBox.Show("Doriți sa ștergeți această înregistrarea?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (Result == MessageBoxResult.Yes)
                {
                    var actdel = db.GetTable<Activ>().Where(u => u.ActivitDidact == IDactiv).Where(u => u.IdPerson == IDProf).FirstOrDefault();
                    db.GetTable<Activ>().DeleteOnSubmit(actdel);
                    db.SubmitChanges();
                    var prdis = from b in ac
                                join p in pers on b.IdPerson equals p.IdPerson
                                select new
                                {
                                    Activitatea = b.ActivitDidact,
                                    Numele = p.Nume,
                                    Prenume = p.Prenume
                                };
                    actgrid.ItemsSource = prdis.ToList();
                    profesori.SelectedIndex = -1;
                }
            }
            else
                MessageBox.Show("Alegeți o înregistrare pentru a o șterge", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        private void Anuleaza_Click(object sender, RoutedEventArgs e)
        {
            atext.Text = "";
            profesori.SelectedIndex = -1;
            ModificActiv.Visibility = Visibility.Hidden;
            AdaugaActiv.Visibility = Visibility.Visible;
            Anuleaza.Visibility = Visibility.Hidden;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MeniuPrin m = new MeniuPrin();
            m.Show();
        }
    }
}
