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
    /// Interaction logic for LocalitateForm.xaml
    /// </summary>
    public partial class LocalitateForm : Window
    {
        string IDLocal = "";
        string conn = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + Environment.CurrentDirectory + @"\Personal.mdf;Integrated Security=True;Connect Timeout=30";
        public LocalitateForm()
        {
            InitializeComponent();
            DataContext db = new DataContext(conn);
            Table<Localit> localitatea = db.GetTable<Localit>();
            var localit = from l in localitatea
                          select new
                          {
                              Localitatea = l.Localitate,
                          };
            local.ItemsSource = localit.ToList();
        }


        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void AdaugLocal_Click(object sender, RoutedEventArgs e)
        {
            string err = "";
            Regex regNume = new Regex("^([A-ZĂÎÂȘȚ][a-zăîâșț]{2,}|[A-ZĂÎÂȘȚ][a-zăîâșț]{2,}[ ][[A-ZĂÎÂȘȚ][a-zăîâșț]{2,})$");
            if (!regNume.IsMatch(ltext.Text))
            {
                err += "Sunt introduse date incorecte \n (Sunt permise doar caractere și spațiu)";
            }
            if (err == "")
            {
                DataContext db = new DataContext(conn);
                Table<Localit> loc = db.GetTable<Localit>();

                var verifica = db.GetTable<Localit>().Where(x => x.Localitate == ltext.Text);
                bool run = false;
                foreach (var v in verifica)
                {
                    if (v.Localitate == ltext.Text)
                        run = true;
                }
                if (run)
                    MessageBox.Show("Această localitate deja există", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                else
                {

                    Localit newlocal = new Localit
                    {
                        Localitate = ltext.Text
                    };
                    db.GetTable<Localit>().InsertOnSubmit(newlocal);
                    db.SubmitChanges();

                    var localit = from l in loc
                                  select new
                                  {
                                      Localitatea = l.Localitate,
                                  };
                    local.ItemsSource = localit.ToList();

                    ltext.Text = "";
                }
            }
            else
                MessageBox.Show(err, "Error", MessageBoxButton.OK, MessageBoxImage.Error);

        }

        private void Modifica(object sender, RoutedEventArgs e)
        {
            DataContext db = new DataContext(conn);
            Table<Localit> loc = db.GetTable<Localit>();

            if (local.SelectedItems.Count > 0)
            {

                for (int i = 0; i < local.SelectedItems.Count; i++)
                {

                    var list = from c in loc

                               select new { Localitatea = c.Localitate };
                    foreach (var l in list)
                    {

                        if (l.ToString() == local.SelectedItem.ToString())
                        {
                            ltext.Text = l.Localitatea;
                            IDLocal = l.Localitatea;
                        }
                    }
                }
                AdaugLocal.Visibility = Visibility.Hidden;
                ModificLocal.Visibility = Visibility.Visible;
                Anuleaza.Visibility = Visibility.Visible;
            }
            else
                MessageBox.Show("Alegeți o înregistrare pentru a o modifica", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        private void ModificLocal_Click(object sender, RoutedEventArgs e)
        {
            string err = "";
            Regex regNume = new Regex("^([A-ZĂÎÂȘȚ][a-zăîâșț]{2,}|[A-ZĂÎÂȘȚ][a-zăîâșț]{2,}[ ][[A-ZĂÎÂȘȚ][a-zăîâșț]{2,})$");
            DataContext db = new DataContext(conn);
            Table<Localit> loc = db.GetTable<Localit>();
            if (!regNume.IsMatch(ltext.Text))
            {
                err += "Sunt introduse date incorecte \n (Sunt permise doar caractere și spațiu)";
            }
            if (err == "")
            {

                foreach (var f in loc)
                {
                    if (f.Localitate == IDLocal)
                    {
                        var verifica = db.GetTable<Localit>().Where(x => x.Localitate == ltext.Text);
                        bool run = false;
                        foreach (var v in verifica)
                        {
                            if (v.Localitate == ltext.Text)
                                run = true;
                        }
                        if (run)
                            MessageBox.Show("Această localitate deja există", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                        else
                        {
                            f.Localitate = ltext.Text;
                            db.SubmitChanges();
                            MessageBox.Show("Datele au fost modificate", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                            var localit = from l in loc
                                          select new
                                          {
                                              Localitatea = l.Localitate,
                                          };
                            local.ItemsSource = localit.ToList();
                            ModificLocal.Visibility = Visibility.Hidden;
                            AdaugLocal.Visibility = Visibility.Visible;
                            Anuleaza.Visibility = Visibility.Hidden;
                            ltext.Text = "";
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
            Table<Localit> loc = db.GetTable<Localit>();

            if (local.SelectedItems.Count > 0)
            {

                for (int i = 0; i < local.SelectedItems.Count; i++)
                {

                    var list = from c in loc
                               select new { Localitatea = c.Localitate };
                    foreach (var l in list)
                    {

                        if (l.ToString() == local.SelectedItem.ToString())
                        {
                            IDLocal = l.Localitatea;
                        }
                    }
                }
                var Result = MessageBox.Show("Doriți să ștergeți această înregistrare?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (Result == MessageBoxResult.Yes)
                {

                    var locdel = db.GetTable<Localit>().Where(u => u.Localitate == IDLocal).FirstOrDefault();
                    db.GetTable<Localit>().DeleteOnSubmit(locdel);
                    db.SubmitChanges();
                    var localit = from l in loc
                                  select new
                                  {
                                      Localitatea = l.Localitate,
                                  };
                    local.ItemsSource = localit.ToList();

                }
            }
            else
                MessageBox.Show("Alegeți o înregistrare pentru a o șterge", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        private void Anuleaza_Click(object sender, RoutedEventArgs e)
        {
            AdaugLocal.Visibility = Visibility.Visible;
            ModificLocal.Visibility = Visibility.Hidden;
            ltext.Text = "";
            Anuleaza.Visibility = Visibility.Hidden;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
           
        }
    }
}
