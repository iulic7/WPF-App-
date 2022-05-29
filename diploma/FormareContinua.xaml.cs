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
using System.Windows.Controls.Primitives;

namespace diploma
{
    /// <summary>
    /// Interaction logic for FormareContinua.xaml
    /// </summary>
    public partial class FormareContinua : Window
    {
        int IDDiscip = 0;
        int IDProf = 0;
        string IDForm = "";
        bool formOpen = false;

        string discip;
        string profn;
        string profp;

        string conn = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + Environment.CurrentDirectory + @"\Personal.mdf;Integrated Security=True;Connect Timeout=30";
        public FormareContinua()
        {
            InitializeComponent();
            DataFinisarii.DisplayDateEnd = DateTime.Now;
            DataContext db = new DataContext(conn);
            Table<FormCont> fc = db.GetTable<FormCont>();
            Table<Persoane> pers = db.GetTable<Persoane>();
            Table<DenInst> ins = db.GetTable<DenInst>();
            var prdis = from l in fc
                        join p in pers on l.IdPerson equals p.IdPerson
                        join d in ins on l.IdInst equals d.IdInst
                        select new
                        {
                            Denumirea = l.Denumirea,
                            Data = l.Anul.ToShortDateString(),
                            Institutia = d.DenumInst,
                            Numele = p.Nume,
                            Prenume = p.Prenume
                        };
            formgrid.ItemsSource = prdis.ToList();


            var list = from d in ins
                       select d.DenumInst;
            Institutia.ItemsSource = list.ToList();

            var list2 = from p in pers
                        orderby p.Nume
                        select

                          p.Nume + " " + p.Prenume;


            profesori.ItemsSource = list2.ToList();

        }

        private void AdaugaDenprof_Click(object sender, RoutedEventArgs e)
        {
            DataContext db = new DataContext(conn);
            Table<FormCont> fc = db.GetTable<FormCont>();
            Table<Persoane> pers = db.GetTable<Persoane>();
            Table<DenInst> ins = db.GetTable<DenInst>();

            string err = "";
            Regex regInstitutii = new Regex("^[A-Za-zĂÎÂȘȚăîâșț ,.:#c\"]{1,}$");
            if (!regInstitutii.IsMatch(ftext.Text))
            {
                err += "Sunt introduse date incorecte (Doar litere și caractere speciale) \n";
            }
            if (DataFinisarii.SelectedDate == null)
            {
                err += "Date incorecte la data finisării \n";
            }
            if (Institutia.Text=="")
            {
                err += "Selectați instituția \n";
            }
            if (profesori.Text == "")
            {
                err += "Selectați persoana \n";
            }
            if (err == "")
            {

                var list = from d in ins
                           select new { ID = d.IdInst, Institutia = d.DenumInst };
                foreach (var l in list)
                {
                    if (l.Institutia.ToString() == Institutia.Text)
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

                var verifica = db.GetTable<FormCont>().Where(x => x.Denumirea == ftext.Text && x.IdPerson == IDProf);
                bool run = false;
                foreach (var v in verifica)
                {
                    if (v.Denumirea == ftext.Text && v.IdPerson == IDProf)
                        run = true;
                }
                if (run)
                    MessageBox.Show("Acest profesor deja a trecut formarea continuă dată", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                else
                {
                    FormCont newprof = new FormCont
                    {
                        Denumirea = ftext.Text,
                        Anul = DataFinisarii.SelectedDate.Value,
                        IdInst = IDDiscip,
                        IdPerson = IDProf,
                    };
                    db.GetTable<FormCont>().InsertOnSubmit(newprof);
                    db.SubmitChanges();
                    var prdis = from l in fc
                                join p in pers on l.IdPerson equals p.IdPerson
                                join d in ins on l.IdInst equals d.IdInst
                                select new
                                {
                                    Denumirea = l.Denumirea,
                                    Data = l.Anul.ToShortDateString(),
                                    Institutia = d.DenumInst,
                                    Numele = p.Nume,
                                    Prenume = p.Prenume
                                };
                    formgrid.ItemsSource = prdis.ToList();

                    ftext.Text = "";
                    var t = DataFinisarii.Template.FindName("PART_TextBox", DataFinisarii) as DatePickerTextBox;
                    if (t != null)
                        t.Text = "Select date";
                    Institutia.SelectedIndex = -1;
                    profesori.SelectedIndex = -1;

                }
            }
            else
                MessageBox.Show(err, "Error", MessageBoxButton.OK, MessageBoxImage.Error);

        }

        private void Modifica(object sender, RoutedEventArgs e)
        {
            DataContext db = new DataContext(conn);
            Table<FormCont> fc = db.GetTable<FormCont>();
            Table<Persoane> pers = db.GetTable<Persoane>();
            Table<DenInst> ins = db.GetTable<DenInst>();
            if (formgrid.SelectedItems.Count > 0)
            {

                for (int i = 0; i < formgrid.SelectedItems.Count; i++)
                {

                    var list = from l in fc
                                join p in pers on l.IdPerson equals p.IdPerson
                                join d in ins on l.IdInst equals d.IdInst
                                select new
                                {
                                    Denumirea = l.Denumirea,
                                    Data = l.Anul.ToShortDateString(),
                                    Institutia = d.DenumInst,
                                    Numele = p.Nume,
                                    Prenume = p.Prenume
                                };

                    foreach (var c in list)
                    {

                        if (c.ToString() == formgrid.SelectedItem.ToString())
                        {
                            Institutia.Text = c.Institutia;
                            profesori.Text = c.Numele + " " + c.Prenume;
                            IDForm = c.Denumirea;
                            ftext.Text = c.Denumirea;
                            DataFinisarii.SelectedDate = Convert.ToDateTime(c.Data);

                        }
                    }
                }
                var list2 = from d in ins
                            select new { ID = d.IdInst, Denumire = d.DenumInst };
                foreach (var l in list2)
                {
                    if (l.Denumire.ToString() == Institutia.Text)
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
                Anuleaza.Visibility = Visibility.Visible;
                AdaugaDenprof.Visibility = Visibility.Hidden;
                ModificDenprof.Visibility = Visibility.Visible;
            }
            else
                MessageBox.Show("Alegeți o înregistrare pentru a o modifica", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        private void ModificDenprof_Click(object sender, RoutedEventArgs e)
        {
            DataContext db = new DataContext(conn);
            Table<FormCont> fc = db.GetTable<FormCont>();
            Table<Persoane> pers = db.GetTable<Persoane>();
            Table<DenInst> ins = db.GetTable<DenInst>();

            string err = "";
            Regex regInstitutii = new Regex("^[A-Za-zĂÎÂȘȚăîâșț ,.:#\"]{1,}$");
            if (!regInstitutii.IsMatch(ftext.Text))
            {
                err += "Sunt introduse date incorecte (Doar litere și caractere speciale) \n";
            }
            if (DataFinisarii.SelectedDate == null)
            {
                err += "Date incorecte la data finisării \n";
            }
            if (err == "")
            {

                foreach (var f in fc)
                {
                    if (f.IdInst == IDDiscip && f.IdPerson == IDProf && f.Denumirea==IDForm)
                    {

                        var verifica = db.GetTable<FormCont>().Where(x => x.Denumirea == ftext.Text && x.IdPerson == IDProf);
                        bool run = false;
                        foreach (var v in verifica)
                        {
                            if (v.Denumirea == ftext.Text && v.IdPerson == IDProf)
                                run = true;
                        }
                        if (run)
                            MessageBox.Show("Acest profesor deja a trecut formarea continuă dată", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                        else
                        {
                            var list = from d in ins
                                       select new { ID = d.IdInst, Disciplina = d.DenumInst };
                            foreach (var l in list)
                            {
                                if (l.Disciplina.ToString() == Institutia.Text)
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


                            f.Denumirea = ftext.Text;
                            f.Anul = DataFinisarii.SelectedDate.Value;
                            f.IdInst = IDDiscip;
                            f.IdPerson = IDProf;

                            db.SubmitChanges();
                            MessageBox.Show("Datele au fost modificate", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                            var prdis = from l in fc
                                        join p in pers on l.IdPerson equals p.IdPerson
                                        join d in ins on l.IdInst equals d.IdInst
                                        select new
                                        {
                                            Denumirea = l.Denumirea,
                                            Data = l.Anul.ToShortDateString(),
                                            Institutia = d.DenumInst,
                                            Numele = p.Nume,
                                            Prenume = p.Prenume
                                        };
                            formgrid.ItemsSource = prdis.ToList();
                            ModificDenprof.Visibility = Visibility.Hidden;
                            AdaugaDenprof.Visibility = Visibility.Visible;
                            Anuleaza.Visibility = Visibility.Hidden;
                            ftext.Text = "";
                            var t = DataFinisarii.Template.FindName("PART_TextBox", DataFinisarii) as DatePickerTextBox;
                            if (t != null)
                                t.Text = "Select date";
                            Institutia.SelectedIndex = -1;
                            profesori.SelectedIndex = -1;
                        }
                        break;
                    }
                }
            }
            else
                MessageBox.Show(err, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void Sterge(object sender, RoutedEventArgs e)
        {
            DataContext db = new DataContext(conn);
            Table<FormCont> fc = db.GetTable<FormCont>();
            Table<Persoane> pers = db.GetTable<Persoane>();
            Table<DenInst> ins = db.GetTable<DenInst>();

            if (formgrid.SelectedItems.Count > 0)
            {
                bool deleted = true;
                for (int i = 0; i < formgrid.SelectedItems.Count; i++)
                {
                    var list = from l in fc
                               join p in pers on l.IdPerson equals p.IdPerson
                               join d in ins on l.IdInst equals d.IdInst
                               select new
                               {
                                   Denumirea = l.Denumirea,
                                   Data = l.Anul.ToShortDateString(),
                                   Institutia = d.DenumInst,
                                   Numele = p.Nume,
                                   Prenume = p.Prenume
                               };

                    foreach (var c in list)
                    {

                        if (c.ToString() == formgrid.SelectedItem.ToString())
                        {
                            deleted = false;
                            IDForm = c.Denumirea;
                            discip = c.Institutia;
                            profn = c.Numele;
                            profp = c.Prenume;

                        }
                    }
                }
                if (!deleted)
                {
                    var list2 = from d in ins
                                select new { ID = d.IdInst, Disciplina = d.DenumInst };
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

                    var Result = MessageBox.Show("Doriți sa ștergeți această înregistrarea?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Question);

                    if (Result == MessageBoxResult.Yes)
                    {

                        var formdel = db.GetTable<FormCont>().Where(u => u.Denumirea == IDForm).Where(u => u.IdPerson == IDProf).Where(u => u.IdInst == IDDiscip).FirstOrDefault();
                        db.GetTable<FormCont>().DeleteOnSubmit(formdel);
                        db.SubmitChanges();
                        var prdis = from l in fc
                                    join p in pers on l.IdPerson equals p.IdPerson
                                    join d in ins on l.IdInst equals d.IdInst
                                    select new
                                    {
                                        Denumirea = l.Denumirea,
                                        Data = l.Anul.ToShortDateString(),
                                        Institutia = d.DenumInst,
                                        Numele = p.Nume,
                                        Prenume = p.Prenume
                                    };
                        formgrid.ItemsSource = prdis.ToList();

                    }
                }
                else
                {
                    MessageBox.Show("Această înregistrare a fost ștearsă anterior", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    var prdis = from l in fc
                                join p in pers on l.IdPerson equals p.IdPerson
                                join d in ins on l.IdInst equals d.IdInst
                                select new
                                {
                                    Denumirea = l.Denumirea,
                                    Data = l.Anul.ToShortDateString(),
                                    Institutia = d.DenumInst,
                                    Numele = p.Nume,
                                    Prenume = p.Prenume
                                };
                    formgrid.ItemsSource = prdis.ToList();
                }
                }
            else
                MessageBox.Show("Alegeți o înregistrare pentru a o șterge", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        private void Anuleaza_Click(object sender, RoutedEventArgs e)
        {
            AdaugaDenprof.Visibility = Visibility.Visible;
            ModificDenprof.Visibility = Visibility.Hidden;
            ftext.Text = "";
            Institutia.SelectedIndex = -1;
            profesori.SelectedIndex = -1;
            Anuleaza.Visibility = Visibility.Hidden;

            var t = DataFinisarii.Template.FindName("PART_TextBox", DataFinisarii) as DatePickerTextBox;
            if (t != null)
                t.Text = "Select date";
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!formOpen)
            {
                MeniuPrin m = new MeniuPrin();
                m.Show();
            }
        }

        private void InstitForm_Click(object sender, RoutedEventArgs e)
        {
            InstitutiaForm i = new InstitutiaForm();
            i.Show();   
       
        }

        private void Institutia_DropDownOpened(object sender, EventArgs e)
        {
            DataFinisarii.DisplayDateEnd = DateTime.Now;
            DataContext db = new DataContext(conn);
        
            Table<DenInst> ins = db.GetTable<DenInst>();
            var list = from d in ins
                       select d.DenumInst;
            Institutia.ItemsSource = list.ToList();
        }
    }
}
