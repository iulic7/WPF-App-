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
    /// Interaction logic for RaionulForm.xaml
    /// </summary>
    public partial class RaionulForm : Window
    {
        string IDRai = "";
        int IDlocal = 0;

        string conn = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + Environment.CurrentDirectory + @"\Personal.mdf;Integrated Security=True;Connect Timeout=30";
        public RaionulForm()
        {
            InitializeComponent();
            DataContext db = new DataContext(conn);
            Table<Raionull> raion = db.GetTable<Raionull>();
            Table<Localit> localitatea = db.GetTable<Localit>();
            var Raion = from r in raion
                        join l in localitatea on r.idLocal equals l.IdLocal
                        select new
                        {
                            Raionul = r.RaionDenum,
                            Localitatea = l.Localitate
                        };
            raionul.ItemsSource = Raion.ToList();

            var list = from l in localitatea
                       select l.Localitate;
            locbox.ItemsSource = list.ToList();
        }

        private void AdaugRaion_Click(object sender, RoutedEventArgs e)
        {
            string err = "";
            DataContext db = new DataContext(conn);
            Regex regNume = new Regex("^([A-ZĂÎÂȘȚ][a-zăîâșț]{2,}|[A-ZĂÎÂȘȚ][a-zăîâșț]{2,}[ ][[A-ZĂÎÂȘȚ][a-zăîâșț]{2,})$");
            Table<Localit> loc = db.GetTable<Localit>();
            if (!regNume.IsMatch(rtext.Text))
            {
                err += "Sunt introduse date incorecte \n (Sunt permise doar caractere și spațiu)";
            }
            if (err == "")
            {
                var list = from c in loc
                           select new { ID = c.IdLocal, Localitatea = c.Localitate };
                foreach (var l in list)
                {
                    if (l.Localitatea.ToString() == locbox.Text)
                    {
                        IDlocal = l.ID;
                    }
                }
                Table<Raionull> raion = db.GetTable<Raionull>();

                var verifica = db.GetTable<Raionull>().Where(x => x.RaionDenum == rtext.Text && x.idLocal == IDlocal);
                bool run = false;
                foreach (var v in verifica)
                {
                    if (v.RaionDenum == rtext.Text && v.idLocal == IDlocal)
                        run = true;
                }
                if (run)
                    MessageBox.Show("În această localitate deja este acest raion", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                else
                {
                    Raionull newraion = new Raionull
                    {
                        idLocal = IDlocal,
                        RaionDenum = rtext.Text
                    };
                    db.GetTable<Raionull>().InsertOnSubmit(newraion);
                    db.SubmitChanges();
                    var Raion = from r in raion
                                join l in loc on r.idLocal equals l.IdLocal
                                select new
                                {
                                    Raionul = r.RaionDenum,
                                    Localitatea = l.Localitate
                                };
                    raionul.ItemsSource = Raion.ToList();

                    rtext.Text = "";
                    locbox.SelectedIndex = 0;
                }
            }
            else
                MessageBox.Show(err, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void Modifica(object sender, RoutedEventArgs e)
        {
            DataContext db = new DataContext(conn);
            Table<Localit> loc = db.GetTable<Localit>();
            Table<Raionull> raion = db.GetTable<Raionull>();
            if (raionul.SelectedItems.Count > 0)
            {

                for (int i = 0; i < raionul.SelectedItems.Count; i++)
                {

                    var list = from c in raion
                               join l in loc on c.idLocal equals l.IdLocal
                               select new { Raionul = c.RaionDenum, Localitatea = l.Localitate };

                    foreach (var c in list)
                    {

                        if (c.ToString() == raionul.SelectedItem.ToString())
                        {
                            IDRai = c.Raionul;
                            rtext.Text = c.Raionul;
                            locbox.Text = c.Localitatea;
                        }
                    }
                }
                var list2 = from c in loc
                            select new { ID = c.IdLocal, Localitatea = c.Localitate };
                foreach (var l in list2)
                {
                    if (l.Localitatea.ToString() == locbox.Text)
                    {
                        IDlocal = l.ID;
                    }
                }
                AdaugRaion.Visibility = Visibility.Hidden;
                ModificRaion.Visibility = Visibility.Visible;
                Anuleaza.Visibility = Visibility.Visible;
            }
            else
                MessageBox.Show("Alegeți o înregistrare pentru a o modifica", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        private void ModificRaion_Click(object sender, RoutedEventArgs e)
        {

            string err = "";
            Regex regNume = new Regex("^([A-ZĂÎÂȘȚ][a-zăîâșț]{2,}|[A-ZĂÎÂȘȚ][a-zăîâșț]{2,}[ ][[A-ZĂÎÂȘȚ][a-zăîâșț]{2,})$");
            DataContext db = new DataContext(conn);
            Table<Localit> loc = db.GetTable<Localit>();
            Table<Raionull> raion = db.GetTable<Raionull>();
            if (!regNume.IsMatch(rtext.Text))
            {
                err += "Sunt introduse date incorecte \n (Sunt permise doar caractere și spațiu)";
            }
            if (err == "")
            {


                foreach (var f in raion)
                {
                    if (f.RaionDenum == IDRai && f.idLocal == IDlocal)
                    {
                        var verifica = db.GetTable<Raionull>().Where(x => x.RaionDenum == rtext.Text && x.idLocal == IDlocal);
                        bool run = false;
                        foreach (var v in verifica)
                        {
                            if (v.RaionDenum == rtext.Text && v.idLocal == IDlocal)
                                run = true;
                        }
                        if (run)
                            MessageBox.Show("În această localitate deja este acest raion", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                        else
                        {
                            var loca = db.GetTable<Localit>().Where(x => x.Localitate == locbox.Text);

                            f.idLocal = loca.First().IdLocal;
                            f.RaionDenum = rtext.Text;
                            db.SubmitChanges();
                            MessageBox.Show("Datele au fost modificate", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                            var Raion = from r in raion
                                        join l in loc on r.idLocal equals l.IdLocal
                                        select new
                                        {
                                            Raionul = r.RaionDenum,
                                            Localitatea = l.Localitate
                                        };
                            raionul.ItemsSource = Raion.ToList();
                            ModificRaion.Visibility = Visibility.Hidden;
                            Anuleaza.Visibility = Visibility.Hidden;
                            AdaugRaion.Visibility = Visibility.Visible;
                            rtext.Text = "";
                            locbox.SelectedIndex = 0;
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
            Table<Raionull> raion = db.GetTable<Raionull>();
            if (raionul.SelectedItems.Count > 0)
            {

                for (int i = 0; i < raionul.SelectedItems.Count; i++)
                {

                    var list = from c in raion
                               join l in loc on c.idLocal equals l.IdLocal
                               select new { Raionul = c.RaionDenum, Localitatea = l.Localitate };

                    foreach (var c in list)
                    {

                        if (c.ToString() == raionul.SelectedItem.ToString())
                        {
                            IDRai = c.Raionul;
                        }
                    }
                }
                var list2 = from c in loc
                            select new { ID = c.IdLocal, Localitatea = c.Localitate };
                foreach (var l in list2)
                {
                    if (l.Localitatea.ToString() == locbox.Text)
                    {
                        IDlocal = l.ID;
                    }
                }
                var Result = MessageBox.Show("Doriți să ștergeți această înregistrare?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (Result == MessageBoxResult.Yes)
                {
                    var raidel = db.GetTable<Raionull>().Where(u => u.RaionDenum == IDRai).Where(u => u.idLocal == IDlocal).FirstOrDefault();
                    db.GetTable<Raionull>().DeleteOnSubmit(raidel);
                    db.SubmitChanges();
                    var Raion = from r in raion
                                join l in loc on r.idLocal equals l.IdLocal
                                select new
                                {
                                    Raionul = r.RaionDenum,
                                    Localitatea = l.Localitate
                                };
                    raionul.ItemsSource = Raion.ToList();
                }
            }
            else
                MessageBox.Show("Alegeți o înregistrare pentru a o șterge", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        private void Anuleaza_Click(object sender, RoutedEventArgs e)
        {
            AdaugRaion.Visibility = Visibility.Visible;
            ModificRaion.Visibility = Visibility.Hidden;
            rtext.Text = "";
            locbox.SelectedIndex = 0;
            Anuleaza.Visibility = Visibility.Hidden;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
           
        }
    }
}
