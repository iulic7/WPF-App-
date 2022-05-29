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
    /// Interaction logic for FunctiaForm.xaml
    /// </summary>
    public partial class FunctiaForm : Window
    {
        string IDfun = "";
        string conn = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + Environment.CurrentDirectory + @"\Personal.mdf;Integrated Security=True;Connect Timeout=30";
        public FunctiaForm()
        {
            InitializeComponent();
            DataContext db = new DataContext(conn);
            Table<Functii> fun = db.GetTable<Functii>();
            var funct = from l in fun
                     select new
                     {
                         Functiile = l.DenFunctie,
                     };
            functgrid.ItemsSource = funct.ToList();

        }

        private void AdaugFunc_Click(object sender, RoutedEventArgs e)
        {
            string err = "";
            Regex regInstitutii = new Regex("^[A-Za-zĂÎÂȘȚăîâșț ]{1,}$");
            if (!regInstitutii.IsMatch(ftext.Text))
            {
                err += "Sunt introduse date incorecte (Doar litere și spații) \n";
            }
            if (err == "")
            {
                DataContext db = new DataContext(conn);
                Table<Functii> fun = db.GetTable<Functii>();

                var verifica = db.GetTable<Functii>().Where(x => x.DenFunctie == ftext.Text);
                bool run = false;
                foreach (var v in verifica)
                {
                    if (v.DenFunctie == ftext.Text)
                        run = true;
                }
                if (run)
                    MessageBox.Show("Această funcție deja există", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                else
                {

                    Functii newfn = new Functii
                    {
                        DenFunctie = ftext.Text
                    };
                    db.GetTable<Functii>().InsertOnSubmit(newfn);
                    db.SubmitChanges();
                    var funct = from l in fun
                                select new
                                {
                                    Functiile = l.DenFunctie,
                                };
                    functgrid.ItemsSource = funct.ToList();

                    ftext.Text = "";
                }
            }
            else
                MessageBox.Show(err, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void Modifica(object sender, RoutedEventArgs e)
        {
            DataContext db = new DataContext(conn);
            Table<Functii> fun = db.GetTable<Functii>();

            if (functgrid.SelectedItems.Count > 0)
            {

                for (int i = 0; i < functgrid.SelectedItems.Count; i++)
                {
                    var list = from c in fun

                               select new { Functiile = c.DenFunctie, };
                    foreach (var l in list)
                    {

                        if (l.ToString() == functgrid.SelectedItem.ToString())
                        {
                            ftext.Text = l.Functiile;
                            IDfun = l.Functiile;
                        }
                    }
                }
                AdaugFunc.Visibility = Visibility.Hidden;
                ModificFunc.Visibility = Visibility.Visible;
                Anuleaza.Visibility = Visibility.Visible;
            }
            else
                MessageBox.Show("Alegeți o înregistrare pentru a o modifica", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        private void ModificFunc_Click(object sender, RoutedEventArgs e)
        {
            string err = "";
            Regex regInstitutii = new Regex("^[A-Za-zĂÎÂȘȚăîâșț ]{1,}$");
            DataContext db = new DataContext(conn);
            Table<Functii> fun = db.GetTable<Functii>();
            if (!regInstitutii.IsMatch(ftext.Text))
            {
                err += "Sunt introduse date incorecte (Doar litere și spații) \n";
            }
            if (err == "")
            {

                foreach (var f in fun)
                {
                    if (f.DenFunctie == IDfun)
                    {
                        var verifica = db.GetTable<Functii>().Where(x => x.DenFunctie == ftext.Text);
                        bool run = false;
                        foreach (var v in verifica)
                        {
                            if (v.DenFunctie == ftext.Text)
                                run = true;
                        }
                        if (run)
                            MessageBox.Show("Această funcție deja există", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                        else
                        {
                            f.DenFunctie = ftext.Text;
                            db.SubmitChanges();
                            MessageBox.Show("Datele au fost modificate" , "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                            var funct = from l in fun
                                        select new
                                        {
                                            Functiile = l.DenFunctie,
                                        };
                            functgrid.ItemsSource = funct.ToList();
                            ModificFunc.Visibility = Visibility.Hidden;
                            AdaugFunc.Visibility = Visibility.Visible;
                            Anuleaza.Visibility = Visibility.Hidden;
                            ftext.Text = "";
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
            Table<Functii> fun = db.GetTable<Functii>();
            if (functgrid.SelectedItems.Count > 0)
            {

                for (int i = 0; i < functgrid.SelectedItems.Count; i++)
                {

                    var list = from c in fun
                               select new { Functiile = c.DenFunctie, };
                    foreach (var l in list)
                    {

                        if (l.ToString() == functgrid.SelectedItem.ToString())
                        {
                            IDfun = l.Functiile;
                        }
                    }
                }

                var Result = MessageBox.Show("Doriți sa ștergeți această înregistrarea?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (Result == MessageBoxResult.Yes)
                {
                    var funddel = db.GetTable<Functii>().Where(u => u.DenFunctie == IDfun).FirstOrDefault();
                    db.GetTable<Functii>().DeleteOnSubmit(funddel);
                    db.SubmitChanges();
                    var funct = from l in fun
                                select new
                                {
                                    Functiile = l.DenFunctie,
                                };
                    functgrid.ItemsSource = funct.ToList();
                }
            }
            else
                MessageBox.Show("Alegeți o înregistrare pentru a o șterge", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        private void Anuleaza_Click(object sender, RoutedEventArgs e)
        {
            AdaugFunc.Visibility = Visibility.Visible;
            ModificFunc.Visibility = Visibility.Hidden;
            ftext.Text = "";
            Anuleaza.Visibility = Visibility.Hidden;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
          
        }
    }
}
