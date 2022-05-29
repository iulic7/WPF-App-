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
    /// Interaction logic for StudiiForm.xaml
    /// </summary>
    public partial class StudiiForm : Window
    {
        string IDstud = "";
        string conn = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + Environment.CurrentDirectory + @"\Personal.mdf;Integrated Security=True;Connect Timeout=30";
        public StudiiForm()
        {
            InitializeComponent();
            DataContext db = new DataContext(conn);
            Table<Stud> studii = db.GetTable<Stud>();
            var st = from l in studii
                     select new
                     {
                         Studiii = l.Studiile,
                     };
            studgrid.ItemsSource = st.ToList();
        }

        private void AdaugStud_Click(object sender, RoutedEventArgs e)
        {
            string err = "";
            Regex regInstitutii = new Regex("^[A-Za-zĂÎÂȘȚăîâșț .,:\"]{1,}$");
            if (!regInstitutii.IsMatch(stext.Text))
            {
                err += "Sunt introduse date incorecte \n (Se perminte litere și caractere speciale)";
            }
            if (err == "")
            {
                DataContext db = new DataContext(conn);
                Table<Stud> studii = db.GetTable<Stud>();

                var verifica = db.GetTable<Stud>().Where(x => x.Studiile == stext.Text);
                bool run = false;
                foreach (var v in verifica)
                {
                    if (v.Studiile == stext.Text)
                        run = true;
                }
                if (run)
                    MessageBox.Show("Aceste studii au fost deja adăugate", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                else
                {
                    Stud newst = new Stud
                    {
                        Studiile = stext.Text
                    };
                    db.GetTable<Stud>().InsertOnSubmit(newst);
                    db.SubmitChanges();
                    var st = from l in studii
                             select new
                             {
                                 Studiii = l.Studiile,
                             };
                    studgrid.ItemsSource = st.ToList();

                    stext.Text = "";
                }
            }
            else
                MessageBox.Show(err, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void Modifica(object sender, RoutedEventArgs e)
        {
            DataContext db = new DataContext(conn);
            Table<Stud> studii = db.GetTable<Stud>();

            if (studgrid.SelectedItems.Count > 0)
            {

                for (int i = 0; i < studgrid.SelectedItems.Count; i++)
                {

                    var list = from c in studii

                               select new { Studiii = c.Studiile, };
                    foreach (var l in list)
                    {

                        if (l.ToString() == studgrid.SelectedItem.ToString())
                        {
                            stext.Text = l.Studiii;
                            IDstud = l.Studiii;
                        }
                    }
                }
                AdaugStud.Visibility = Visibility.Hidden;
                ModificStud.Visibility = Visibility.Visible;
                Anuleaza.Visibility = Visibility.Visible;
            }
            else
                MessageBox.Show("Alegeți o înregistrare pentru a o modifica", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        private void ModificStud_Click(object sender, RoutedEventArgs e)
        {
            string err = "";
            Regex regInstitutii = new Regex("^[A-Za-zĂÎÂȘȚăîâșț .,:\"]{1,}$");
            DataContext db = new DataContext(conn);
            Table<Stud> studii = db.GetTable<Stud>();
            if (!regInstitutii.IsMatch(stext.Text))
            {
                err += "Sunt introduse date incorecte \n (Se perminte litere și caractere speciale)";
            }
            if (err == "")
            {

                foreach (var f in studii)
                {
                    if (f.Studiile == IDstud)
                    {
                        var verifica = db.GetTable<Stud>().Where(x => x.Studiile == stext.Text);
                        bool run = false;
                        foreach (var v in verifica)
                        {
                            if (v.Studiile == stext.Text)
                                run = true;
                        }
                        if (run)
                            MessageBox.Show("Aceste studii au fost deja adăugate", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                        else
                        {
                            f.Studiile = stext.Text;
                            db.SubmitChanges();
                            MessageBox.Show("Datele au fost modificate", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                            var st = from l in studii
                                     select new
                                     {
                                         Studiii = l.Studiile,
                                     };
                            studgrid.ItemsSource = st.ToList();
                            ModificStud.Visibility = Visibility.Hidden;
                            Anuleaza.Visibility = Visibility.Hidden;
                            AdaugStud.Visibility = Visibility.Visible;
                            stext.Text = "";
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
            Table<Stud> studii = db.GetTable<Stud>();
            if (studgrid.SelectedItems.Count > 0)
            {
                for (int i = 0; i < studgrid.SelectedItems.Count; i++)
                {

                    var list = from c in studii
                               select new { Studiii = c.Studiile, };
                    foreach (var l in list)
                    {

                        if (l.ToString() == studgrid.SelectedItem.ToString())
                        {
                            IDstud = l.Studiii;
                        }
                    }
                }
                var Result = MessageBox.Show("Doriți să ștergeți această înregistrare?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (Result == MessageBoxResult.Yes)
                {
                    var studdel = db.GetTable<Stud>().Where(u => u.Studiile == IDstud).FirstOrDefault();
                    db.GetTable<Stud>().DeleteOnSubmit(studdel);
                    db.SubmitChanges();
                    var st = from l in studii
                             select new
                             {
                                 Studiii = l.Studiile,
                             };
                    studgrid.ItemsSource = st.ToList();
                }
            }
            else
                MessageBox.Show("Alegeți o înregistrare pentru a o șterge", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        private void Anuleaza_Click(object sender, RoutedEventArgs e)
        {
            AdaugStud.Visibility = Visibility.Visible;
            ModificStud.Visibility = Visibility.Hidden;
            stext.Text = "";
            Anuleaza.Visibility = Visibility.Hidden;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
           
        }
    }
}
