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
    /// Interaction logic for CatedraForm.xaml
    /// </summary>
    public partial class CatedraForm : Window
    {
        string IDcated = "";
        string conn = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + Environment.CurrentDirectory + @"\Personal.mdf;Integrated Security=True;Connect Timeout=30";
        public CatedraForm()
        {
            InitializeComponent();
            DataContext db = new DataContext(conn);
            Table<Catedre> cat = db.GetTable<Catedre>();
            var catedr = from l in cat
                          select new
                          {
                              Catedra = l.DenumireCatedra,
                          };
            catgrid.ItemsSource = catedr.ToList();
        }

        private void AdaugCat_Click(object sender, RoutedEventArgs e)
        {
            string err = "";
            Regex regNume = new Regex(@"^[A-ZĂÎÂȘȚ\- ][a-zăîâșț\- ]{2,}$");
            if (!regNume.IsMatch(ctext.Text))
            {
                err += "Sunt introduse date incorecte (Doar litere, se începe cu majusculă) \n";
            }
            if (err == "")
            {
                DataContext db = new DataContext(conn);
                Table<Catedre> cat = db.GetTable<Catedre>();

                var verifica = db.GetTable<Catedre>().Where(x => x.DenumireCatedra == ctext.Text);
                bool run = false;
                foreach(var v in verifica)
                {
                    if (v.DenumireCatedra == ctext.Text)
                        run = true;
                }
                if (run)
                    MessageBox.Show("Această subdiviziune deja există", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                else
                {
                    Catedre newlocal = new Catedre
                    {
                        DenumireCatedra = ctext.Text
                    };
                    db.GetTable<Catedre>().InsertOnSubmit(newlocal);
                    db.SubmitChanges();

                    var catedr = from l in cat
                                 select new
                                 {
                                     Catedra = l.DenumireCatedra,
                                 };
                    catgrid.ItemsSource = catedr.ToList();
                    ctext.Text = "";
                }
            }
            else
                MessageBox.Show(err, "Error", MessageBoxButton.OK, MessageBoxImage.Error);

        }

        private void Modifica(object sender, RoutedEventArgs e)
        {
            DataContext db = new DataContext(conn);
            Table<Catedre> cat = db.GetTable<Catedre>();

            if (catgrid.SelectedItems.Count > 0)
            {

                for (int i = 0; i < catgrid.SelectedItems.Count; i++)
                {

                    var list = from c in cat

                               select new { Catedra = c.DenumireCatedra, };
                    foreach (var l in list)
                    {

                        if (l.ToString() == catgrid.SelectedItem.ToString())
                        {
                            ctext.Text = l.Catedra;
                            IDcated = l.Catedra;
                        }
                    }
                }
                AdaugCat.Visibility = Visibility.Hidden;
                ModificCat.Visibility = Visibility.Visible;
                Anuleaza.Visibility = Visibility.Visible;
            }
            else
                MessageBox.Show("Alegeti o inregistrare pentru a o modifica", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        private void ModificCat_Click(object sender, RoutedEventArgs e)
        {
            string err = "";
            Regex regNume = new Regex(@"^[A-ZĂÎÂȘȚ\- ][a-zăîâșț\- ]{2,}$");
            DataContext db = new DataContext(conn);
            Table<Catedre> cat = db.GetTable<Catedre>();
            if (!regNume.IsMatch(ctext.Text))
            {
                err += "Sunt introduse date incorecte (Doar litere, se începe cu majusculă) \n";
            }
            if (err == "")
            {

                foreach (var f in cat)
                {
                    if (f.DenumireCatedra == IDcated)
                    {
                        var verifica = db.GetTable<Catedre>().Where(x => x.DenumireCatedra == ctext.Text);
                        bool run = false;
                        foreach (var v in verifica)
                        {
                            if (v.DenumireCatedra == ctext.Text)
                                run = true;
                        }
                        if (run)
                            MessageBox.Show("Această subdiviziune deja există", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                        else
                        {
                            f.DenumireCatedra = ctext.Text;
                            db.SubmitChanges();
                            MessageBox.Show("Datele au fost modificate cu succes", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                            var catedr = from l in cat
                                         select new
                                         {
                                             Catedra = l.DenumireCatedra,
                                         };
                            catgrid.ItemsSource = catedr.ToList();
                            ModificCat.Visibility = Visibility.Hidden;
                            AdaugCat.Visibility = Visibility.Visible;
                            Anuleaza.Visibility = Visibility.Hidden;
                            ctext.Text = "";
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
            Table<Catedre> cat = db.GetTable<Catedre>();
            if (catgrid.SelectedItems.Count > 0)
            {

                for (int i = 0; i < catgrid.SelectedItems.Count; i++)
                {

                    var list = from c in cat
                               select new { Catedra = c.DenumireCatedra, };
                    foreach (var l in list)
                    {

                        if (l.ToString() == catgrid.SelectedItem.ToString())
                        {
                            IDcated = l.Catedra;
                        }
                    }
                }
                var Result = MessageBox.Show("Doriți sa ștergeți această înregistrarea?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (Result == MessageBoxResult.Yes)
                {

                    var catdel = db.GetTable<Catedre>().Where(u => u.DenumireCatedra == IDcated).FirstOrDefault();
                    db.GetTable<Catedre>().DeleteOnSubmit(catdel);
                    db.SubmitChanges();
                    var catedr = from l in cat
                                 select new
                                 {
                                     Catedra = l.DenumireCatedra,
                                 };
                    catgrid.ItemsSource = catedr.ToList();

                }
            }
            else
                MessageBox.Show("Alegeți o înregistrare pentru a o șterge", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        private void Anuleaza_Click(object sender, RoutedEventArgs e)
        {
            AdaugCat.Visibility = Visibility.Visible;
            ModificCat.Visibility = Visibility.Hidden;
            ctext.Text = "";
            Anuleaza.Visibility = Visibility.Hidden;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
          
        }
    }
}
