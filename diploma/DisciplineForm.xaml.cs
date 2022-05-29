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
    /// Interaction logic for DisciplineForm.xaml
    /// </summary>
    public partial class DisciplineForm : Window
    {
        string IDdis = "";
        string conn = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + Environment.CurrentDirectory + @"\Personal.mdf;Integrated Security=True;Connect Timeout=30";
        public DisciplineForm()
        {
        
            InitializeComponent();
            DataContext db = new DataContext(conn);
            Table<Disciplin> dis = db.GetTable<Disciplin>();
            var discip = from l in dis
                        select new
                        {
                            Disciplina = l.Denumirea,
                        };
            discgrid.ItemsSource = discip.ToList();
        }

        private void AdaugDisc_Click(object sender, RoutedEventArgs e)
        {
            string err = "";
            Regex regInstitutii = new Regex(@"^[A-Za-zĂÎÂȘȚăîâșț \W]{1,}$");
            if (!regInstitutii.IsMatch(dtext.Text))
            {
                err += "Sunt introduse date incorecte (Doar litere și spațiu) \n";
            }
            if (err == "")
            {
                DataContext db = new DataContext(conn);
                Table<Disciplin> dis = db.GetTable<Disciplin>();

                var verifica = db.GetTable<Disciplin>().Where(x => x.Denumirea == dtext.Text);
                bool run = false;
                foreach (var v in verifica)
                {
                    if (v.Denumirea == dtext.Text)
                        run = true;
                }
                if (run)
                    MessageBox.Show("Această disciplină deja există", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                else
                {
                    Disciplin newdis = new Disciplin
                    {
                        Denumirea = dtext.Text
                    };
                    db.GetTable<Disciplin>().InsertOnSubmit(newdis);
                    db.SubmitChanges();
                    var discip = from l in dis
                                 select new
                                 {
                                     Disciplina = l.Denumirea,
                                 };
                    discgrid.ItemsSource = discip.ToList();
                    dtext.Text = "";
                }
            }
            else
                MessageBox.Show(err, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void Modifica(object sender, RoutedEventArgs e)
        {
            DataContext db = new DataContext(conn);
            Table<Disciplin> dis = db.GetTable<Disciplin>();

            if (discgrid.SelectedItems.Count > 0)
            {

                for (int i = 0; i < discgrid.SelectedItems.Count; i++)
                {

                    var list = from c in dis

                               select new { Disciplina = c.Denumirea, };
                    foreach (var l in list)
                    {

                        if (l.ToString() == discgrid.SelectedItem.ToString())
                        {
                            dtext.Text = l.Disciplina;
                            IDdis = l.Disciplina;
                        }
                    }
                }
                AdaugDisc.Visibility = Visibility.Hidden;
                ModificDisc.Visibility = Visibility.Visible;
                Anuleaza.Visibility = Visibility.Visible;
            }
            else
                MessageBox.Show("Alegeți o înregistrare pentru a o modifica", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        private void ModificDisc_Click(object sender, RoutedEventArgs e)
        {
            string err = "";
            Regex regInstitutii = new Regex("^[A-Za-zĂÎÂȘȚăîâșț ]{1,}$");
            DataContext db = new DataContext(conn);
            Table<Disciplin> dis = db.GetTable<Disciplin>();
            if (!regInstitutii.IsMatch(dtext.Text))
            {
                err += "Sunt introduse date incorecte (Doar litere și spațiu)\n";
            }
            if (err == "")
            {

                foreach (var f in dis)
                {
                    if (f.Denumirea == IDdis)
                    {
                        var verifica = db.GetTable<Disciplin>().Where(x => x.Denumirea == dtext.Text);
                        bool run = false;
                        foreach (var v in verifica)
                        {
                            if (v.Denumirea == dtext.Text)
                                run = true;
                        }
                        if (run)
                            MessageBox.Show("Această disciplină deja există", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                        else
                        {

                            f.Denumirea = dtext.Text;
                            db.SubmitChanges();
                            MessageBox.Show("Datele au fost modificate", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                            var discip = from l in dis
                                         select new
                                         {
                                             Disciplina = l.Denumirea,
                                         };
                            discgrid.ItemsSource = discip.ToList();
                            ModificDisc.Visibility = Visibility.Hidden;
                            AdaugDisc.Visibility = Visibility.Visible;
                            Anuleaza.Visibility = Visibility.Hidden;
                            dtext.Text = "";
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
            Table<Disciplin> dis = db.GetTable<Disciplin>();
            if (discgrid.SelectedItems.Count > 0)
            {

                for (int i = 0; i < discgrid.SelectedItems.Count; i++)
                {

                    var list = from c in dis
                               select new { Disciplina = c.Denumirea, };
                    foreach (var l in list)
                    {

                        if (l.ToString() == discgrid.SelectedItem.ToString())
                        {
                            IDdis = l.Disciplina;
                        }
                    }
                }
                var Result = MessageBox.Show("Doriți sa ștergeți această înregistrarea?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (Result == MessageBoxResult.Yes)
                {
                    var discdel = db.GetTable<Disciplin>().Where(u => u.Denumirea == IDdis).FirstOrDefault();
                    db.GetTable<Disciplin>().DeleteOnSubmit(discdel);
                    db.SubmitChanges();
                    var discip = from l in dis
                                 select new
                                 {
                                     Disciplina = l.Denumirea,
                                 };
                    discgrid.ItemsSource = discip.ToList();
                }
            }
            else
                MessageBox.Show("Alegeți o înregistrare pentru a o șterge", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        private void Anuleaza_Click(object sender, RoutedEventArgs e)
        {
            AdaugDisc.Visibility = Visibility.Visible;
            ModificDisc.Visibility = Visibility.Hidden;
            dtext.Text = "";
            Anuleaza.Visibility = Visibility.Hidden;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
           
        }
    }
}
