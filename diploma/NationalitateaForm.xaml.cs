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
    /// Interaction logic for NationalitateaForm.xaml
    /// </summary>
    public partial class NationalitateaForm : Window
    {
        string IDNaltional = "";
        string conn = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + Environment.CurrentDirectory + @"\Personal.mdf;Integrated Security=True;Connect Timeout=30";
        public NationalitateaForm()
        {
            InitializeComponent();
            DataContext db = new DataContext(conn);
            Table<Nationalit> nat = db.GetTable<Nationalit>();
            var national = from l in nat
                           select new
                           {
                               Nationalitatea = l.Nationalitatea,
                           };
            nationgrid.ItemsSource = national.ToList();

        }

        private void AdaugNation_Click(object sender, RoutedEventArgs e)
        {
            string err = "";
            Regex regNume = new Regex("^[A-ZĂÎÂȘȚ][a-zăîâșț]{2,}$");
            if (!regNume.IsMatch(ntext.Text))
            {
                err += "Sunt introduse date incorecte \n (se permit doar caractere)";
            }
            if (err == "")
            {
                DataContext db = new DataContext(conn);
                Table<Nationalit> nat = db.GetTable<Nationalit>();

                var verifica = db.GetTable<Nationalit>().Where(x => x.Nationalitatea == ntext.Text);
                bool run = false;
                foreach (var v in verifica)
                {
                    if (v.Nationalitatea == ntext.Text)
                        run = true;
                }
                if (run)
                    MessageBox.Show("Această naționalitate deja există", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                else
                {
                    Nationalit newlocal = new Nationalit
                    {
                        Nationalitatea = ntext.Text
                    };
                    db.GetTable<Nationalit>().InsertOnSubmit(newlocal);
                    db.SubmitChanges();

                    var national = from l in nat
                                   select new
                                   {
                                       Nationalitatea = l.Nationalitatea,
                                   };
                    nationgrid.ItemsSource = national.ToList();

                    ntext.Text = "";
                }
            }
            else
                MessageBox.Show(err, "Error", MessageBoxButton.OK, MessageBoxImage.Error);

        }

        private void Modifica(object sender, RoutedEventArgs e)
        {
            DataContext db = new DataContext(conn);
            Table<Nationalit> nat = db.GetTable<Nationalit>();

            if (nationgrid.SelectedItems.Count > 0)
            {

                for (int i = 0; i < nationgrid.SelectedItems.Count; i++)
                {

                    var list = from c in nat

                               select new { Nationalitatea = c.Nationalitatea, };
                    foreach (var l in list)
                    {

                        if (l.ToString() == nationgrid.SelectedItem.ToString())
                        {
                            ntext.Text = l.Nationalitatea;
                            IDNaltional = l.Nationalitatea;
                        }
                    }
                }
                AdaugNation.Visibility = Visibility.Hidden;
                ModificNation.Visibility = Visibility.Visible;
                Anuleaza.Visibility = Visibility.Visible;
            }
            else
                MessageBox.Show("Alegeți o înregistrare pentru a o modifica", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        private void ModificNation_Click(object sender, RoutedEventArgs e)
        {
            string err = "";
            Regex regNume = new Regex("^[A-ZĂÎÂȘȚ][a-zăîâșț]{2,}$");
            DataContext db = new DataContext(conn);
            Table<Nationalit> nat = db.GetTable<Nationalit>();
            if (!regNume.IsMatch(ntext.Text))
            {
                err += "Sunt introduse date incorecte \n (se permit doar caractere)";
            }
            if (err == "")
            {

                foreach (var f in nat)
                {
                    if (f.Nationalitatea == IDNaltional)
                    {
                        var verifica = db.GetTable<Nationalit>().Where(x => x.Nationalitatea == ntext.Text);
                        bool run = false;
                        foreach (var v in verifica)
                        {
                            if (v.Nationalitatea == ntext.Text)
                                run = true;
                        }
                        if (run)
                            MessageBox.Show("Această naționalitate deja există", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                        else
                        {
                            f.Nationalitatea = ntext.Text;
                            db.SubmitChanges();
                            MessageBox.Show("Datele au fost modificate", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                            var national = from l in nat
                                           select new
                                           {
                                               Nationalitatea = l.Nationalitatea,
                                           };
                            nationgrid.ItemsSource = national.ToList();
                            ModificNation.Visibility = Visibility.Hidden;
                            AdaugNation.Visibility = Visibility.Visible;
                            Anuleaza.Visibility = Visibility.Hidden;
                            ntext.Text = "";
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
            Table<Nationalit> nat = db.GetTable<Nationalit>();

            if (nationgrid.SelectedItems.Count > 0)
            {

                for (int i = 0; i < nationgrid.SelectedItems.Count; i++)
                {

                    var list = from c in nat
                               select new { Nationalitatea = c.Nationalitatea, };
                    foreach (var l in list)
                    {

                        if (l.ToString() == nationgrid.SelectedItem.ToString())
                        {
                            IDNaltional = l.Nationalitatea;
                        }
                    }
                }
                var Result = MessageBox.Show("Doriți să ștergeți această înregistrare?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (Result == MessageBoxResult.Yes)
                {

                    var natdel = db.GetTable<Nationalit>().Where(u => u.Nationalitatea == IDNaltional).FirstOrDefault();
                    db.GetTable<Nationalit>().DeleteOnSubmit(natdel);
                    db.SubmitChanges();
                    var national = from l in nat
                                   select new
                                   {
                                       Nationalitatea = l.Nationalitatea,
                                   };
                    nationgrid.ItemsSource = national.ToList();
                }
            }
            else
                MessageBox.Show("Alegeți o înregistrare pentru a o șterge", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        private void Anuleaza_Click(object sender, RoutedEventArgs e)
        {
            AdaugNation.Visibility = Visibility.Visible;
            ModificNation.Visibility = Visibility.Hidden;
            ntext.Text = "";
            Anuleaza.Visibility = Visibility.Hidden;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
           
        }
    }
}
