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
    /// Interaction logic for InstitutiaForm.xaml
    /// </summary>
    public partial class InstitutiaForm : Window
    {
        string IDinst = "";
        string conn = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + Environment.CurrentDirectory + @"\Personal.mdf;Integrated Security=True;Connect Timeout=30";
    

        public InstitutiaForm()
        {
            InitializeComponent();
            DataContext db = new DataContext(conn);
            Table<DenInst> instit = db.GetTable<DenInst>();
            var inst = from l in instit
                       select new
                       {
                           Institutia = l.DenumInst
                       };
            instgrid.ItemsSource = inst.ToList();
        }

        private void AdaugInst_Click(object sender, RoutedEventArgs e)
        {
            string err = "";
            Regex regInstitutii = new Regex("^[A-Za-zĂÎÂȘȚăîâșț .,:\"]{1,}$");
            if (!regInstitutii.IsMatch(itext.Text))
            {
                err += "Sunt introduse date incorecte (Doar litere și caractere speciale)\n";
            }
            if (err == "")
            {
                DataContext db = new DataContext(conn);
                Table<DenInst> instit = db.GetTable<DenInst>();

                var verifica = db.GetTable<DenInst>().Where(x => x.DenumInst == itext.Text);
                bool run = false;
                foreach (var v in verifica)
                {
                    if (v.DenumInst == itext.Text)
                        run = true;
                }
                if (run)
                    MessageBox.Show("Această instituție deja există", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                else
                {

                    DenInst newinst = new DenInst
                    {
                        DenumInst = itext.Text
                    };
                    db.GetTable<DenInst>().InsertOnSubmit(newinst);
                    db.SubmitChanges();

                    var inst = from l in instit
                               select new
                               {
                                   Institutia = l.DenumInst,
                               };
                    instgrid.ItemsSource = inst.ToList();

                    itext.Text = "";
                }
            }
            else
                MessageBox.Show(err, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void Modifica(object sender, RoutedEventArgs e)
        {
            DataContext db = new DataContext(conn);
            Table<DenInst> instit = db.GetTable<DenInst>();

            if (instgrid.SelectedItems.Count > 0)
            {

                for (int i = 0; i < instgrid.SelectedItems.Count; i++)
                {

                    var list = from c in instit

                               select new { Institutia = c.DenumInst, };
                    foreach (var l in list)
                    {

                        if (l.ToString() == instgrid.SelectedItem.ToString())
                        {
                            itext.Text = l.Institutia;
                            IDinst = l.Institutia;
                        }
                    }
                }
                AdaugInst.Visibility = Visibility.Hidden;
                ModificInst.Visibility = Visibility.Visible;
                Anuleaza.Visibility = Visibility.Visible;
            }
            else
                MessageBox.Show("Alegeți o înregistrare pentru a o modifica", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        private void ModificInst_Click(object sender, RoutedEventArgs e)
        {
            string err = "";
            Regex regInstitutii = new Regex("^[A-Za-zĂÎÂȘȚăîâșț .,:\"]{1,}$");
            DataContext db = new DataContext(conn);
            Table<DenInst> instit = db.GetTable<DenInst>();
            if (!regInstitutii.IsMatch(itext.Text))
            {
                err += "Sunt introduse date incorecte (Doar litere și caractere speciale)\n";
            }
            if (err == "")
            {

                foreach (var f in instit)
                {
                    if (f.DenumInst == IDinst)
                    {
                        var verifica = db.GetTable<DenInst>().Where(x => x.DenumInst == itext.Text);
                        bool run = false;
                        foreach (var v in verifica)
                        {
                            if (v.DenumInst == itext.Text)
                                run = true;
                        }
                        if (run)
                            MessageBox.Show("Această instituție deja există", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                        else
                        {

                            f.DenumInst = itext.Text;
                            db.SubmitChanges();
                            MessageBox.Show("Datele au fost modificate", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                            var inst = from l in instit
                                       select new
                                       {
                                           Institutia = l.DenumInst,
                                       };
                            instgrid.ItemsSource = inst.ToList();
                            ModificInst.Visibility = Visibility.Hidden;
                            AdaugInst.Visibility = Visibility.Visible;
                            Anuleaza.Visibility = Visibility.Hidden;
                            itext.Text = "";
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
            Table<DenInst> instit = db.GetTable<DenInst>();
            if (instgrid.SelectedItems.Count > 0)
            {

                for (int i = 0; i < instgrid.SelectedItems.Count; i++)
                {

                    var list = from c in instit
                               select new { Institutia = c.DenumInst, };
                    foreach (var l in list)
                    {

                        if (l.ToString() == instgrid.SelectedItem.ToString())
                        {
                            IDinst = l.Institutia;
                        }
                    }
                }
                var Result = MessageBox.Show("Doriți sa ștergeți această înregistrarea?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Question);

                Table<FormCont> form = db.GetTable<FormCont>();

                var list2 = from f in form
                           join i in instit on f.IdInst equals i.IdInst
                           where i.DenumInst == IDinst
                           select f;

                foreach(var i in list2)
                {
                    db.GetTable<FormCont>().DeleteOnSubmit(i);
                    db.SubmitChanges();
                }

                if (Result == MessageBoxResult.Yes)
                {
                    var instdel = db.GetTable<DenInst>().Where(u => u.DenumInst == IDinst).FirstOrDefault();
                    db.GetTable<DenInst>().DeleteOnSubmit(instdel);
                    db.SubmitChanges();
                    var inst = from l in instit
                               select new
                               {
                                   Institutia = l.DenumInst,
                               };
                    instgrid.ItemsSource = inst.ToList();
                }
            }
            else
                MessageBox.Show("Alegeți o înregistrare pentru a o șterge", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        private void Anuleaza_Click(object sender, RoutedEventArgs e)
        {
            AdaugInst.Visibility = Visibility.Visible;
            ModificInst.Visibility = Visibility.Hidden;
            itext.Text = "";
            Anuleaza.Visibility = Visibility.Hidden;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
          
        }
    }
}
