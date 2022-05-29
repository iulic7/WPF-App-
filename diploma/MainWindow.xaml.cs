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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Data.Linq;
using diploma.Clase;

namespace diploma
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string conn = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + Environment.CurrentDirectory + @"\Personal.mdf;Integrated Security=True;Connect Timeout=30";

        public MainWindow()
        {
            InitializeComponent();

            using (StreamReader sr = File.OpenText(@"..\user.dll"))
            {
                while (sr.ReadLine() != null)
                {
                    MeniuPrin m = new MeniuPrin();
                    m.Show();
                    this.Close();
                }
            }
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DataContext db = new DataContext(conn);

            Table<Users> user = db.GetTable<Users>();

            int err = 0;

            foreach (var u in user)
            {
                if (u.numeUtilizator == Login.Text)
                {
                    if (u.Parola == Password.Password)
                    {
                        if (date.IsChecked == true)
                        {

                            using (StreamWriter sw = File.AppendText(@"..\user.dll"))
                            {
                                sw.WriteLine("true");
                            }
                            this.Close();
                            MeniuPrin c = new MeniuPrin();
                            c.Show();

                        }
                        else
                        {
                            MeniuPrin b = new MeniuPrin();
                            b.Show();
                            this.Close();
                        }

                    }
                    else
                    {
                        MessageBox.Show("Parola nu este corectă", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        Password.Clear();
                    }
                }
                else
                    err++;
            }

            if (err == user.Count())
            {
                MessageBox.Show("Datele despre utilizator incorecte", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Login.Text = "Introduceti Loginul";
                Password.Password = "********";
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Regist r = new Regist();
            r.Show();
            this.Close();
        }

        private void Login_GotFocus(object sender, RoutedEventArgs e)
        {
            if (Login.Text == "Introduceti Loginul")
                Login.Text = "";
        }

        private void Login_LostFocus(object sender, RoutedEventArgs e)
        {
            if (Login.Text == "")
                Login.Text = "Introduceti Loginul";
        }

        private void Password_LostFocus(object sender, RoutedEventArgs e)
        {
            if (Password.Password == "")
                Password.Password = "********";
        }

        private void Password_GotFocus(object sender, RoutedEventArgs e)
        {
            if (Password.Password == "********")
                Password.Password = "";
        }

        private void Login_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
