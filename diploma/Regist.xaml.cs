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
using System.Data.Linq;
using System.Text.RegularExpressions;
using diploma.Clase;


namespace diploma
{
    /// <summary>
    /// Interaction logic for Regist.xaml
    /// </summary>
    public partial class Regist : Window
    {
        string iduser = "";
        string conn = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + Environment.CurrentDirectory + @"\Personal.mdf;Integrated Security=True;Connect Timeout=30";

        public Regist()
        {
            InitializeComponent();
            DataContext db = new DataContext(conn);
            Table<Users> users = db.GetTable<Users>();
            var user = from l in users
                       select new
                       {
                           Login = l.numeUtilizator,
                           Parola = l.Parola,
                           DataAdaug = l.dataInreg.ToShortDateString(),
                           DataModific = l.dataMod.ToShortDateString()
                       };
            usergrid.ItemsSource = user.ToList();

        }

        private void Inapoi_Click(object sender, RoutedEventArgs e)
        {
            MainWindow m = new MainWindow();
            m.Show();
            this.Close();
        }

        private void Registrare_Click(object sender, RoutedEventArgs e)
        {
            DataContext db = new DataContext(conn);

            Table<Users> users = db.GetTable<Users>();

            Regex regLogin = new Regex("^[A-Za-z0-9ĂÎÂȘȚăîâșț]{3,}$");
            Regex regPassword = new Regex("^(?=.*[0-9])(?=.*[A-ZĂÎÂȘȚ])[A-Za-z0-9ĂÎÂȘȚăîâșț]{8,}$");

            bool Login = false;
            foreach (var u in users)
            {
                if (login.Text == u.numeUtilizator)
                    Login = true;
            }
            if (Login)
            {
                MessageBox.Show("Așa login deja există", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                login.Text = "Introduceti loginul";
                parol.Password = "********";
                parol2.Password = "********";
            }
            else
            {
                if (regLogin.IsMatch(login.Text) && regPassword.IsMatch(parol.Password))
                {
                    if (parol.Password == parol2.Password)
                    {
                        Users user = new Users
                        {
                            numeUtilizator = login.Text,
                            Parola = parol.Password,
                            dataInreg = DateTime.Now.Date,
                            dataMod = DateTime.Now.Date,
                        };
                        db.GetTable<Users>().InsertOnSubmit(user);
                        db.SubmitChanges();

                        MessageBox.Show("Utilizatorul a fost adăugat cu succes", "Information", MessageBoxButton.OK, MessageBoxImage.Information);


                    }
                    else
                    {
                        MessageBox.Show("Parolele nu coincid", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        parol.Password = "********";
                        parol2.Password = "********";
                    }
                }
                else
                {
                    if (!regLogin.IsMatch(login.Text))
                    {
                        login.Text = "Introduceti Loginul";
                        MessageBox.Show("Loginul incorect \n (trebuie să conțină minim 3 caractere, cifre sau litere", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    if (!regPassword.IsMatch(parol.Password))
                    {
                        parol.Password = "********";
                        parol2.Password = "********";
                        MessageBox.Show("Parola incorectă \n (trebuie să conțină minim 8 caractere, minim o cifra și o literă mare", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }

        private void login_LostFocus(object sender, RoutedEventArgs e)
        {
            if (login.Text == "")
                login.Text = "Introduceti Loginul";
        }

        private void login_GotFocus(object sender, RoutedEventArgs e)
        {
            if (login.Text == "Introduceti Loginul")
                login.Text = "";
        }

        private void parol_GotFocus(object sender, RoutedEventArgs e)
        {
            if (parol.Password == "********")
                parol.Password = "";
        }

        private void parol_LostFocus(object sender, RoutedEventArgs e)
        {
            if (parol.Password == "")
                parol.Password = "********";
        }

        private void parol2_LostFocus(object sender, RoutedEventArgs e)
        {
            if (parol2.Password == "")
                parol2.Password = "********";
        }

        private void parol2_GotFocus(object sender, RoutedEventArgs e)
        {
            if (parol2.Password == "********")
                parol2.Password = "";
        }

        private void sterge(object sender, RoutedEventArgs e)
        {
            DataContext db = new DataContext(conn);
            Table<Users> util = db.GetTable<Users>();

            if (usergrid.SelectedItems.Count > 0)
            {

                for (int i = 0; i < usergrid.SelectedItems.Count; i++)
                {

                    var list = from l in util
                               select new
                               {
                                   Login = l.numeUtilizator,
                                   Parola = l.Parola,
                                   DataAdaug = l.dataInreg.ToShortDateString(),
                                   DataModific = l.dataMod.ToShortDateString()
                               };
                    foreach (var l in list)
                    {

                        if (l.ToString() == usergrid.SelectedItem.ToString())
                        {
                            iduser = l.Login;
                        }
                    }
                }
                var Result = MessageBox.Show("Doriți să ștergeți această înregistrare?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (Result == MessageBoxResult.Yes)
                {

                    var usdel = db.GetTable<Users>().Where(u => u.numeUtilizator == iduser).FirstOrDefault();
                    db.GetTable<Users>().DeleteOnSubmit(usdel);
                    db.SubmitChanges();
                    var user = from l in util
                               select new
                               {
                                   Login = l.numeUtilizator,
                                   Parola = l.Parola,
                                   DataAdaug = l.dataInreg.ToShortDateString(),
                                   DataModific = l.dataMod.ToShortDateString()
                               };
                    usergrid.ItemsSource = user.ToList();

                }
            }
            else
                MessageBox.Show("Alegeți o înregistrare pentru a o șterge", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MeniuPrin m = new MeniuPrin();
            m.Show();
        }
    }
}




