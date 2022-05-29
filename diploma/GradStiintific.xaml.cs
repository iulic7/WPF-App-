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
    /// Interaction logic for GradStiintific.xaml
    /// </summary>
    public partial class GradStiintific : Window
    {
        string IDgradul = "";
        string conn = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + Environment.CurrentDirectory + @"\Personal.mdf;Integrated Security=True;Connect Timeout=30";
        public GradStiintific()
        {
            InitializeComponent();
            DataContext db = new DataContext(conn);
            Table<GrStiinte> stin = db.GetTable<GrStiinte>();
            var grad = from l in stin
                         select new
                         {
                             Titlul = l.Garad,
                         };
            gradgrid.ItemsSource = grad.ToList();
        }

        private void AdaugGrad_Click(object sender, RoutedEventArgs e)
        {
            string err = "";
            Regex regInstitutii = new Regex("^[A-Za-zĂÎÂȘȚăîâșț ]{1,}$");
            if (!regInstitutii.IsMatch(gtext.Text))
            {
                err += "Sunt introduse date incorecte (Doar litere și spații) \n";
            }
            if (err == "")
            {
                DataContext db = new DataContext(conn);
                Table<GrStiinte> stin = db.GetTable<GrStiinte>();

                var verifica = db.GetTable<GrStiinte>().Where(x => x.Garad == gtext.Text);
                bool run = false;
                foreach (var v in verifica)
                {
                    if (v.Garad == gtext.Text)
                        run = true;
                }
                if (run)
                    MessageBox.Show("Aceast grad științific deja există", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                else
                {

                    GrStiinte newgrad = new GrStiinte
                    {
                        Garad = gtext.Text
                    };
                    db.GetTable<GrStiinte>().InsertOnSubmit(newgrad);
                    db.SubmitChanges();

                    var grad = from l in stin
                               select new
                               {
                                   Titlul = l.Garad,
                               };
                    gradgrid.ItemsSource = grad.ToList();

                    gtext.Text = "";
                }
            }
            else
                MessageBox.Show(err, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void Modifica(object sender, RoutedEventArgs e)
        {
            DataContext db = new DataContext(conn);
            Table<GrStiinte> stin = db.GetTable<GrStiinte>();

            if (gradgrid.SelectedItems.Count > 0)
            {

                for (int i = 0; i < gradgrid.SelectedItems.Count; i++)
                {
                    var list = from c in stin

                               select new { Titlul = c.Garad, };
                    foreach (var l in list)
                    {

                        if (l.ToString() == gradgrid.SelectedItem.ToString())
                        {
                            gtext.Text = l.Titlul;
                            IDgradul = l.Titlul;
                        }
                    }
                }
                AdaugGrad.Visibility = Visibility.Hidden;
                ModificGrad.Visibility = Visibility.Visible;
                Anuleaza.Visibility = Visibility.Visible;
            }
            else
                MessageBox.Show("Alegeți o înregistrare pentru a o modifica", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        private void ModificGrad_Click(object sender, RoutedEventArgs e)
        {
            string err = "";
            Regex regInstitutii = new Regex("^[A-Za-zĂÎÂȘȚăîâșț ]{1,}$");
            DataContext db = new DataContext(conn);
            Table<GrStiinte> stin = db.GetTable<GrStiinte>();
            if (!regInstitutii.IsMatch(gtext.Text))
            {
                err += "Sunt introduse date incorecte (Doar litere și spații) \n";
            }
            if (err == "")
            {

                foreach (var f in stin)
                {
                    if (f.Garad == IDgradul)
                    {
                        var verifica = db.GetTable<GrStiinte>().Where(x => x.Garad == gtext.Text);
                        bool run = false;
                        foreach (var v in verifica)
                        {
                            if (v.Garad == gtext.Text)
                                run = true;
                        }
                        if (run)
                            MessageBox.Show("Aceast grad științific deja există", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                        else
                        {
                            f.Garad = gtext.Text;
                            db.SubmitChanges();
                            MessageBox.Show("Datele au fost modificate", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                            var grad = from l in stin
                                       select new
                                       {
                                           Titlul = l.Garad,
                                       };
                            gradgrid.ItemsSource = grad.ToList();
                            ModificGrad.Visibility = Visibility.Hidden;
                            AdaugGrad.Visibility = Visibility.Visible;
                            Anuleaza.Visibility = Visibility.Hidden;
                            gtext.Text = "";
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
            Table<GrStiinte> stin = db.GetTable<GrStiinte>();
            if (gradgrid.SelectedItems.Count > 0)
            {

                for (int i = 0; i < gradgrid.SelectedItems.Count; i++)
                {

                    var list = from c in stin
                               select new { Titlul = c.Garad, };
                    foreach (var l in list)
                    {

                        if (l.ToString() == gradgrid.SelectedItem.ToString())
                        {
                            IDgradul = l.Titlul;
                        }
                    }
                }
                var Result = MessageBox.Show("Doriți sa ștergeți această înregistrarea?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (Result == MessageBoxResult.Yes)
                {
                    var graddel = db.GetTable<GrStiinte>().Where(u => u.Garad == IDgradul).FirstOrDefault();
                    db.GetTable<GrStiinte>().DeleteOnSubmit(graddel);
                    db.SubmitChanges();
                    var grad = from l in stin
                               select new
                               {
                                   Titlul = l.Garad,
                               };
                    gradgrid.ItemsSource = grad.ToList();
                }
            }
            else
                MessageBox.Show("Alegeți o înregistrare pentru a o șterge", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void Anuleaza_Click(object sender, RoutedEventArgs e)
        {
            AdaugGrad.Visibility = Visibility.Visible;
            ModificGrad.Visibility = Visibility.Hidden;
            gtext.Text = "";
            Anuleaza.Visibility = Visibility.Hidden;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
           
        }
    }
}
