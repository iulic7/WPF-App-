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
using System.Text.RegularExpressions;
using diploma.Clase;

namespace diploma
{
    /// <summary>
    /// Interaction logic for ProfesorFunctie.xaml
    /// </summary>
    public partial class ProfesorFunctie : Window
    {
        int IDFunctie = 0;
        int IDProf = 0;


        string conn = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + Environment.CurrentDirectory + @"\Personal.mdf;Integrated Security=True;Connect Timeout=30";
        public ProfesorFunctie()
        {
            InitializeComponent();
            DateTime a = new DateTime(1800, 1, 1);
            DataContext db = new DataContext(conn);
            var pers = db.GetTable<Persoane>().Where(x => x.DataEliberarii == a);
            Table<PersoanaFunc> fpers = db.GetTable<PersoanaFunc>();

            Table<Functii> functii = db.GetTable<Functii>();
            var prdis = from fp in fpers
                        join p in pers on fp.IdPerson equals p.IdPerson
                        join f in functii on fp.IdFunctie equals f.IdFunctie
                        select new
                        {
                            Numele = p.Nume,
                            Prenume = p.Prenume,
                            Funcția = f.DenFunctie,
                            Unitați = fp.Unitati,
                            ClSalarizare = fp.ClasaSalar,
                        };
            profgrid.ItemsSource = prdis.ToList();


            var list = from f in functii
                       select f.DenFunctie;
            Functia.ItemsSource = list.ToList();

            var list2 = from p in pers
                        orderby p.Nume
                        select

                          p.Nume + " " + p.Prenume;


            profesori.ItemsSource = list2.ToList();

        }

        private void AdaugaDenprof_Click(object sender, RoutedEventArgs e)
        {
            DataContext db = new DataContext(conn);
            Table<PersoanaFunc> fpers = db.GetTable<PersoanaFunc>();
            Table<Persoane> pers = db.GetTable<Persoane>();
            Table<Functii> functii = db.GetTable<Functii>();

            string err = "";
            Regex regUnitati = new Regex("^(([0-9]|[1-9][0-9]{1,})|([0-9]|[1-9][0-9]{1,})([.]|[,])([1-9]|[0-9]{1,}[1-9]))$");
            Regex regClasa = new Regex("^([0-9]|[1-9][0-9]{1,})$");
            if (!regUnitati.IsMatch(Unitati.Text))
            {
                err += "Date incorecte la unități (Doar numere reale) \n";
            }
            if (!regClasa.IsMatch(Salarizare.Text))
            {
                err += "Date incorecte la clasa de salarizare (Doar numere) \n";
            }
            if (Functia.Text == "")
            {
                err += "Alegeți funcția\n";
            }
            if (profesori.Text == "")
            {
                err += "Alegeți persoana\n";
            }
            if (err == "")
            {

                var list = from f in functii
                           select new { ID = f.IdFunctie, Functia = f.DenFunctie };
                foreach (var l in list)
                {
                    if (l.Functia.ToString() == Functia.Text)
                    {
                        IDFunctie = l.ID;
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
                var verifica = db.GetTable<PersoanaFunc>().Where(x => x.IdPerson == IDProf && x.IdFunctie == IDFunctie);
                bool run = false;
                foreach (var v in verifica)
                {
                    if (v.IdPerson == IDProf && v.IdFunctie == IDFunctie)
                        run = true;
                }
                if (run)
                    MessageBox.Show("Acest profesor deja are această funcție", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                else
                {
                    double unitati;
                    if (Unitati.Text == "")
                    {
                        unitati = 0;
                    }
                    else
                    {
                        string number = Unitati.Text;
                        if (number.Contains("."))
                        {
                            double OutVal;
                            double.TryParse(number, out OutVal);
                            if (OutVal.ToString() == Unitati.Text)
                                unitati = Convert.ToDouble(number);
                            else
                                unitati = Convert.ToDouble(number.Replace(".", ","));
                        }
                        else
                        {
                            double OutVal;
                            double.TryParse(number, out OutVal);
                            if (OutVal.ToString() == Unitati.Text)
                                unitati = Convert.ToDouble(number);
                            else
                                unitati = Convert.ToDouble(number.Replace(',', '.'));
                        }
                    }

                    PersoanaFunc newProf = new PersoanaFunc
                    {
                        IdFunctie = IDFunctie,
                        IdPerson = IDProf,
                        Unitati = unitati,
                        ClasaSalar = Convert.ToInt32(Salarizare.Text),
                    };
                    db.GetTable<PersoanaFunc>().InsertOnSubmit(newProf);
                    db.SubmitChanges();
                    var prdis = from fp in fpers
                                join p in pers on fp.IdPerson equals p.IdPerson
                                join f in functii on fp.IdFunctie equals f.IdFunctie
                                select new
                                {
                                    Numele = p.Nume,
                                    Prenume = p.Prenume,
                                    Funcția = f.DenFunctie,
                                    Unitați = fp.Unitati,
                                    ClSalarizare = fp.ClasaSalar,
                                };
                    profgrid.ItemsSource = prdis.ToList();

                    profesori.SelectedIndex = -1;
                    Functia.SelectedIndex = -1;
                    Unitati.Text = "";
                    Salarizare.Text = "";
                }
            }
            else
                MessageBox.Show(err, "Error", MessageBoxButton.OK, MessageBoxImage.Error);

        }
        string profes, functie;

        private void Modifica(object sender, RoutedEventArgs e)
        {
            DataContext db = new DataContext(conn);
            Table<PersoanaFunc> fpers = db.GetTable<PersoanaFunc>();
            Table<Persoane> pers = db.GetTable<Persoane>();
            Table<Functii> functii = db.GetTable<Functii>();
            if (profgrid.SelectedItems.Count > 0)
            {

                for (int i = 0; i < profgrid.SelectedItems.Count; i++)
                {

                    var prdis = from fp in fpers
                                join p in pers on fp.IdPerson equals p.IdPerson
                                join f in functii on fp.IdFunctie equals f.IdFunctie
                                select new
                                {
                                    Numele = p.Nume,
                                    Prenume = p.Prenume,
                                    Funcția = f.DenFunctie,
                                    Unitați = fp.Unitati,
                                    ClSalarizare = fp.ClasaSalar,
                                };

                    foreach (var c in prdis)
                    {
                        if (c.ToString() == profgrid.SelectedItem.ToString())
                        {
                            Functia.Text = c.Funcția;
                            profesori.Text = c.Numele + " " + c.Prenume;
                            Unitati.Text = c.Unitați.ToString();
                            Salarizare.Text = c.ClSalarizare.ToString();

                            functie = c.Funcția;
                            profes = c.Numele + " " + c.Prenume;
                        }
                    }
                }
                var list2 = from f in functii
                            select new { ID = f.IdFunctie, Functia = f.DenFunctie };
                foreach (var l in list2)
                {
                    if (l.Functia.ToString() == Functia.Text)
                    {
                        IDFunctie = l.ID;
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
            Table<PersoanaFunc> fpers = db.GetTable<PersoanaFunc>();
            Table<Persoane> pers = db.GetTable<Persoane>();
            Table<Functii> functii = db.GetTable<Functii>();

            string err = "";
            Regex regUnitati = new Regex("^(([0-9]|[1-9][0-9]{1,})|([0-9]|[1-9][0-9]{1,})([.]|[,])([1-9]|[0-9]{1,}[1-9]))$");
            Regex regClasa = new Regex("^([0-9]|[1-9][0-9]{1,})$");
            if (!regUnitati.IsMatch(Unitati.Text))
            {
                err += "Date incorecte la unități (Doar numere reale) \n";
            }
            if (!regClasa.IsMatch(Salarizare.Text))
            {
                err += "Date incorecte la clasa de salarizare (Doar numere) \n";
            }
            if (err == "")
            {

                foreach (var f in fpers)
                {
                    if (f.IdFunctie == IDFunctie && f.IdPerson == IDProf)
                    {
                        var list = from fn in functii
                                   select new { ID = fn.IdFunctie, Functia = fn.DenFunctie };
                        foreach (var l in list)
                        {
                            if (l.Functia.ToString() == Functia.Text)
                            {
                                IDFunctie = l.ID;
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


                        var verifica = from p in pers
                                       join fp in fpers on p.IdPerson equals fp.IdPerson
                                       join fun in functii on fp.IdFunctie equals fun.IdFunctie
                                       where fp.IdPerson == IDProf && fp.IdFunctie == IDFunctie
                                       select new { fp.IdPerson, p.Nume, p.Prenume, fp.IdFunctie, fun.DenFunctie };
                        bool run = false;
                        foreach (var v in verifica)
                        {
                            if (v.IdPerson == IDProf && v.IdFunctie == IDFunctie && !(v.DenFunctie == functie && profes == v.Nume + " " + v.Prenume))
                                run = true;
                        }
                        if (run)
                        {
                            MessageBox.Show("Acest profesor deja are această funcție", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                            break;
                        }
                        else
                        {
                            double unitati;
                            if (Unitati.Text == "")
                            {
                                unitati = 0;
                            }
                            else
                            {
                                string number = Unitati.Text;
                                if (number.Contains("."))
                                {
                                    double OutVal;
                                    double.TryParse(number, out OutVal);
                                    if (OutVal.ToString() == Unitati.Text)
                                        unitati = Convert.ToDouble(number);
                                    else
                                        unitati = Convert.ToDouble(number.Replace(".", ","));
                                }
                                else
                                {
                                    double OutVal;
                                    double.TryParse(number, out OutVal);
                                    if (OutVal.ToString() == Unitati.Text)
                                        unitati = Convert.ToDouble(number);
                                    else
                                        unitati = Convert.ToDouble(number.Replace(',', '.'));
                                }
                            }

                            f.IdFunctie = IDFunctie;
                            f.IdPerson = IDProf;
                            f.Unitati = unitati;
                            f.ClasaSalar = Convert.ToInt32(Salarizare.Text);
                            db.SubmitChanges();
                            MessageBox.Show("Datele au fost modificate", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                            var prdis = from fp in fpers
                                        join p in pers on fp.IdPerson equals p.IdPerson
                                        join fn in functii on fp.IdFunctie equals fn.IdFunctie
                                        select new
                                        {
                                            Numele = p.Nume,
                                            Prenume = p.Prenume,
                                            Funcția = fn.DenFunctie,
                                            Unitați = fp.Unitati,
                                            ClSalarizare = fp.ClasaSalar,
                                        };
                            profgrid.ItemsSource = prdis.ToList();
                            ModificDenprof.Visibility = Visibility.Hidden;
                            AdaugaDenprof.Visibility = Visibility.Visible;
                            Anuleaza.Visibility = Visibility.Hidden;

                            profesori.SelectedIndex = -1;
                            Functia.SelectedIndex = -1;
                            Unitati.Text = "";
                            Salarizare.Text = "";

                            break;
                        }
                    }
                }
            }
            else
                MessageBox.Show(err, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void Sterge(object sender, RoutedEventArgs e)
        {
            DataContext db = new DataContext(conn);
            Table<PersoanaFunc> fpers = db.GetTable<PersoanaFunc>();
            Table<Persoane> pers = db.GetTable<Persoane>();
            Table<Functii> functii = db.GetTable<Functii>();

            if (profgrid.SelectedItems.Count > 0)
            {
                bool deleted = true;

                var prdiss = from fp in fpers
                             join p in pers on fp.IdPerson equals p.IdPerson
                             join f in functii on fp.IdFunctie equals f.IdFunctie
                             select new
                             {
                                 Numele = p.Nume,
                                 Prenume = p.Prenume,
                                 Funcția = f.DenFunctie,
                                 Unitați = fp.Unitati,
                                 ClSalarizare = fp.ClasaSalar,
                             };

                foreach (var c in prdiss)
                {

                    if (c.ToString() == profgrid.SelectedItem.ToString())
                    {
                        deleted = false;
                        functie = c.Funcția;
                        profes = c.Numele + " " + c.Prenume;
                        deleted = false;
                    }
                }

                if (!deleted)
                {

                    var list = from fn in functii
                               select new { ID = fn.IdFunctie, Functia = fn.DenFunctie };
                    foreach (var l in list)
                    {
                        if (l.Functia.ToString() == functie)
                        {
                            IDFunctie = l.ID;
                        }
                    }
                    string a = profes;
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
                    var Result = MessageBox.Show("Doriți să ștergeți această înregistrare?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Question);

                    
                    if (Result == MessageBoxResult.Yes)
                    {
                        var profdel = db.GetTable<PersoanaFunc>().Where(u => u.IdFunctie == IDFunctie).Where(u => u.IdPerson == IDProf).FirstOrDefault();
                        db.GetTable<PersoanaFunc>().DeleteOnSubmit(profdel);
                        db.SubmitChanges();
                        var prdis = from fp in fpers
                                    join p in pers on fp.IdPerson equals p.IdPerson
                                    join f in functii on fp.IdFunctie equals f.IdFunctie
                                    select new
                                    {
                                        Numele = p.Nume,
                                        Prenume = p.Prenume,
                                        Funcția = f.DenFunctie,
                                        Unitați = fp.Unitati,
                                        ClSalarizare = fp.ClasaSalar,
                                    };
                        profgrid.ItemsSource = prdis.ToList();
                    }
                }
                else
                {
                    MessageBox.Show("Această înregistrare a fost ștearsă anterior", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    var prdis = from fp in fpers
                                join p in pers on fp.IdPerson equals p.IdPerson
                                join f in functii on fp.IdFunctie equals f.IdFunctie
                                select new
                                {
                                    Numele = p.Nume,
                                    Prenume = p.Prenume,
                                    Funcția = f.DenFunctie,
                                    Unitați = fp.Unitati,
                                    ClSalarizare = fp.ClasaSalar,
                                };
                    profgrid.ItemsSource = prdis.ToList();
                }
            }
            else
                MessageBox.Show("Alegeți o înregistrare pentru a o șterge", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        private void Anuleaza_Click(object sender, RoutedEventArgs e)
        {
            AdaugaDenprof.Visibility = Visibility.Visible;
            ModificDenprof.Visibility = Visibility.Hidden;
            profesori.SelectedIndex = -1;
            Functia.SelectedIndex = -1;
            Anuleaza.Visibility = Visibility.Hidden;
            Salarizare.Text = "";
            Unitati.Text = "";
        }

        private void FunForm_Click(object sender, RoutedEventArgs e)
        {
            FunctiaForm f = new FunctiaForm();
            f.Show();

        }

        private void Functia_DropDownOpened(object sender, EventArgs e)
        {
            DataContext db = new DataContext(conn);

            Table<Functii> functii = db.GetTable<Functii>();
            var list = from f in functii
                       select f.DenFunctie;
            Functia.ItemsSource = list.ToList();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

            MeniuPrin m = new MeniuPrin();
            m.Show();

        }
    }
}
