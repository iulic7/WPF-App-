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
using System.Windows.Controls.Primitives;

namespace diploma
{
    /// <summary>
    /// Interaction logic for Adauga.xaml
    /// </summary>
    public partial class Adauga : Window
    {
        string conn = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + Environment.CurrentDirectory + @"\Personal.mdf;Integrated Security=True;Connect Timeout=30";

        private void Arata()
        {
            DataContext db = new DataContext(conn);

            Table<Raionull> raion = db.GetTable<Raionull>();
            Table<Localit> localitatea = db.GetTable<Localit>();
            Table<Nationalit> nationalitatea = db.GetTable<Nationalit>();
            Table<DenInst> institutia = db.GetTable<DenInst>();
            Table<Persoane> persoana = db.GetTable<Persoane>();
            Table<Stud> studii = db.GetTable<Stud>();
            Table<FormCont> formare = db.GetTable<FormCont>();
            Table<Functii> functie = db.GetTable<Functii>();
            Table<Activ> activitati = db.GetTable<Activ>();
            Table<GrStiinte> grad = db.GetTable<GrStiinte>();
            Table<Catedre> catedra = db.GetTable<Catedre>();
            Table<GradulDidactic> grDidactic = db.GetTable<GradulDidactic>();

            var perslist = from p in persoana
                           join r in raion on p.idRaion equals r.IdRaion
                           join l in localitatea on r.idLocal equals l.IdLocal
                           join i in institutia on p.idInst equals i.IdInst
                           join n in nationalitatea on p.idNational equals n.IdNational
                           join s in studii on p.idStudii equals s.IdStudii
                           join g in grad on p.idgradstiint equals g.Idgrstiint
                           join c in catedra on p.IdCatedra equals c.IdCatedra
                           join gr in grDidactic on p.IdGradDidactic equals gr.IdGradDidactic
                           select new
                           {
                               Nume = p.Nume,
                               Prenume = p.Prenume,
                               DataNasterii = p.DataNast.ToShortDateString(),
                               Genul = p.Gen,
                               Idnp = p.IDNP,
                               Email = p.Email,
                               Mobil = p.TelefonMob,
                               EsteProfesor = p.EsteProfesor,
                               Catedra = c.DenumireCatedra,
                               GradDidactic = gr.Gradul,
                               DataPrimiriiGradului = p.DataPrimiriiGrad.ToShortDateString(),
                               ModulAngajarii = p.ModAngaj,
                               Nationalitatea = n.Nationalitatea,
                               Localitatea = l.Localitate + " " + r.RaionDenum,
                               VarstaPensionara = p.VirstaPens,
                               Institutia = i.DenumInst,
                               Studii = s.Studiile,
                               StudiiManageriale = p.StudiiManager,
                               GradulManagerial = p.GradManager,
                               StudiiPedagogice = p.ArStudiiPed,
                               StagiulPedagogic = p.StagiuPedagogic,
                               DataAngajaeii = p.DataAngajarii.ToShortDateString(),
                               DataEliberarii = p.DataEliberarii.ToShortDateString(),
                               VechimeInMunca = p.VechimeInMunc,
                               CazatCamin = p.CazatCam,
                               Camin = p.Camin,
                               Statutul = p.Statutul,
                               GradulStiintific = g.Garad,
                               ConcediuTermenLung = p.ConcediuTermenL,
                           };

            Persoane.ItemsSource = perslist.ToList();
        }

        private void Schimba()
        {
            Nume.Text = "";
            Prenume.Text = "";
            GenMasculin.IsChecked = false;
            GenFeminin.IsChecked = false;
            IDNP.Text = "";
            Telefon.Text = "";
            Email.Text = "";
            Localitatea.SelectedIndex = 0;
            Raion.SelectedIndex = 0;
            DataAngajarii.SelectedDate = null;
            DataEliberarii.SelectedDate = null;
            Catedra.SelectedIndex = -1;
            GradDidactic.SelectedIndex = 0;
            TStiintific.SelectedIndex = 0;
            StudiiPedagogice.IsChecked = false;
            VarstaPensionara.IsChecked = false;
            Studii.SelectedIndex = -1;
            Institutia.SelectedIndex = -1;
            StManagerCheck.IsChecked = false;
            StudiiLabel.Visibility = Visibility.Hidden;
            StManagerialeBox.Visibility = Visibility.Hidden;
            StManagerialeBox.Text = "";
            CazareCheckBox.IsChecked = false;
            CazareLabel.Visibility = Visibility.Hidden;
            CaminComboBox.Visibility = Visibility.Hidden;
            CaminComboBox.SelectedIndex = 0;
            AngajarePermanent.IsChecked = false;
            AngajareCumult.IsChecked = false;
            Statutul.SelectedIndex = 0;
            StagiuPedagogic.Text = "";
            ConcediuTLung.IsChecked = false;
            HiddenBorder.Visibility = Visibility.Hidden;
            Vechimea.Text = "";
            EsteProfesori.IsChecked = false;

            var t = DataNasterii.Template.FindName("PART_TextBox", DataNasterii) as DatePickerTextBox;
            if (t != null)
                t.Text = "Select date";

            var t1 = DataAngajarii.Template.FindName("PART_TextBox", DataAngajarii) as DatePickerTextBox;
            if (t1 != null)
                t1.Text = "Select date";

            var t2 = DataEliberarii.Template.FindName("PART_TextBox", DataEliberarii) as DatePickerTextBox;
            if (t2 != null)
                t2.Text = "Select date";

            var t3 = DataGrad.Template.FindName("PART_TextBox", DataGrad) as DatePickerTextBox;
            if (t3 != null)
                t3.Text = "Select date";

        }

        public Adauga()
        {
            InitializeComponent();

            Arata();

            DataContext db = new DataContext(conn);

            Table<Raionull> raion = db.GetTable<Raionull>();
            Table<Localit> localitatea = db.GetTable<Localit>();
            Table<Nationalit> nationalitatea = db.GetTable<Nationalit>();
            Table<DenInst> institutia = db.GetTable<DenInst>();
            Table<Functii> functia = db.GetTable<Functii>();
            Table<Stud> studii = db.GetTable<Stud>();
            Table<Catedre> catedra = db.GetTable<Catedre>();
            Table<GrStiinte> grad = db.GetTable<GrStiinte>();
            Table<GradulDidactic> gradid = db.GetTable<GradulDidactic>();

            DataAngajarii.DisplayDateEnd = DateTime.Now;
            DataAngajarii.DisplayDateStart = new DateTime(1900, 1, 1);
            DataEliberarii.DisplayDateEnd = DateTime.Now;
            DataEliberarii.DisplayDateStart = new DateTime(1900, 1, 1);

            DataGrad.DisplayDateEnd = DateTime.Now;

            DataNasterii.DisplayDateEnd = DateTime.Now;


            var list = from l in localitatea
                       select l.Localitate;
            Localitatea.ItemsSource = list.ToList();

            var list2 = from r in raion
                        join l in localitatea on r.idLocal equals l.IdLocal
                        where l.Localitate == Localitatea.Text
                        select r.RaionDenum;
            Raion.ItemsSource = list2.ToList();

            var list3 = from n in nationalitatea
                        select n.Nationalitatea;
            Nationalitatea.ItemsSource = list3.ToList();

            var list4 = from i in institutia
                        select i.DenumInst;
            Institutia.ItemsSource = list4.ToList();

            var listStudii = from s in studii
                             select s.Studiile;
            Studii.ItemsSource = listStudii.ToList();

            var listCatedra = from c in catedra
                              select c.DenumireCatedra;
            Catedra.ItemsSource = listCatedra.ToList();

            var listGrad = from g in grad
                           select g.Garad;
            TStiintific.ItemsSource = listGrad.ToList();

            var listGradid = from g in gradid
                             select g.Gradul;
            GradDidactic.ItemsSource = listGradid.ToList();
        }

        private void StManagerCheck_Click(object sender, RoutedEventArgs e)
        {
            if (StManagerCheck.IsChecked == true)
            {
                StudiiLabel.Visibility = Visibility.Visible;
                StManagerialeBox.Visibility = Visibility.Visible;
                HiddenBorder.Visibility = Visibility.Visible;
            }
            else
            {
                StudiiLabel.Visibility = Visibility.Hidden;
                StManagerialeBox.Visibility = Visibility.Hidden;
                HiddenBorder.Visibility = Visibility.Hidden;
            }
        }

        private void CazareCheckBox_Click(object sender, RoutedEventArgs e)
        {
            if (CazareCheckBox.IsChecked == true)
            {
                CazareLabel.Visibility = Visibility.Visible;
                CaminComboBox.Visibility = Visibility.Visible;
            }
            else
            {
                CazareLabel.Visibility = Visibility.Hidden;
                CaminComboBox.Visibility = Visibility.Hidden;
            }
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            Regex regNume = new Regex("^[A-ZĂÎÂȘȚ][a-zăîâșț]{2,}$");
            Regex regTel = new Regex("^[0][0-9]{8}$");
            Regex regIDNP = new Regex("^[1-9][0-9]{12}$");
            Regex regInstitutii = new Regex("^[A-Za-zĂÎÂȘȚăîâșț :,.\"]{1,}$");
            Regex regMail = new Regex("^[a-zA-Z0-9!#$%^&*\\-_+;:',./~]{3,}[@][a-z]{3,}[.]([a-z]{2}|[a-z]{3})$");
            Regex regStag = new Regex("^(([0-9]|[1-9][0-9]{1,})|([0-9]|[1-9][0-9]{1,})([.]|[,])([1-9]|[0-9]{1,}[1-9]))$");
            string err = "";
            string err2 = "";
            if (!regNume.IsMatch(Nume.Text))
            {
                err += "Date incorecte la nume (Doar litere, prima să fie majusculă) \n";
            }
            if (!regNume.IsMatch(Prenume.Text))
            {
                err += "Date incorecte la prenume (Doar litere, prima să fie majusculă) \n";
            }
            if (DataNasterii.SelectedDate == null)
            {
                err += "Date incorecte la data nașterii \n";
            }
            if (GenFeminin.IsChecked == false && GenMasculin.IsChecked == false)
            {
                err += "Indicați genul \n";
            }
            if (!regIDNP.IsMatch(IDNP.Text))
            {
                err += "Date incorecte la IDNP (13 cifre) \n";
            }
            if (!regTel.IsMatch(Telefon.Text))
            {
                err += "Date incorecte la telefon (9 cifre, prima este 0) \n";
            }
            if (!regMail.IsMatch(Email.Text))
            {
                err += "Date incorecte la email \n";
            }
            if (StManagerCheck.IsChecked == true)
            {
                if (!regInstitutii.IsMatch(StManagerialeBox.Text))
                {
                    err += "Date incorecte la studii manageriale (Doar litere și caractere speciale) \n";
                }
            }
            if (AngajareCumult.IsChecked == false && AngajarePermanent.IsChecked == false)
            {
                err += "Indicați modul de angajare \n";
            }
            if (DataAngajarii.SelectedDate == null)
            {
                err += "Date incorecte la data angajării \n";
            }
            if (!regStag.IsMatch(StagiuPedagogic.Text) && StagiuPedagogic.Text != "")
            {
                err += "Date incorecte la stagiu pedagogic (Doar numere întregi \n";
            }
            if (!regStag.IsMatch(Vechimea.Text) && Vechimea.Text != "")
            {
                err += "Date incorecte la vechimea în muncă (Doar numere întregi \n";
            }
            if (GradDidactic.Text != "Nu are")
            {
                if (DataGrad.SelectedDate == null)
                {
                    err += "Date incorecte la data primirii gradului didactic \n";
                }
            }
            if (Catedra.Text == "")
            {
                err += "Selectați subdiviziunea \n";
            }
            if (Institutia.Text == "")
            {
                err += "Selectați instituția \n";
            }
            if (Studii.Text == "")
            {
                err += "Selectați studiile \n";
            }
            if (err == "")
            {
                DataContext db = new DataContext(conn);
                Table<Raionull> raion = db.GetTable<Raionull>();
                Table<Localit> localitatea = db.GetTable<Localit>();
                Table<Nationalit> nationalitatea = db.GetTable<Nationalit>();
                Table<DenInst> institutia = db.GetTable<DenInst>();
                Table<Functii> functia = db.GetTable<Functii>();
                Table<Persoane> persoana = db.GetTable<Persoane>();
                Table<Stud> studii = db.GetTable<Stud>();
                Table<FormCont> formare = db.GetTable<FormCont>();
                Table<Functii> functie = db.GetTable<Functii>();
                Table<Activ> activitati = db.GetTable<Activ>();
                Table<GrStiinte> grad = db.GetTable<GrStiinte>();
                Table<Catedre> catedra = db.GetTable<Catedre>();
                Table<GradulDidactic> grDidactic = db.GetTable<GradulDidactic>();

                foreach (var f in persoana)
                {
                    if (f.IDNP == IDNP.Text)
                    {
                        err2 += "Persoană cu așa IDNP există \n";
                    }
                }
                if (err2 == "")
                {
                    int idLOcal = -1;

                    var local = db.GetTable<Localit>().Where(x => x.Localitate == Localitatea.Text);
                    idLOcal = local.First().IdLocal;


                    int idRaion = -1;

                    var raionul = db.GetTable<Raionull>().Where(x => x.RaionDenum == Raion.Text);
                    idRaion = raionul.First().IdRaion;

                    int idStudii = -1;

                    var studiile = db.GetTable<Stud>().Where(x => x.Studiile == Studii.Text);
                    idStudii = studiile.First().IdStudii;

                    int idNational = -1;

                    var nat = db.GetTable<Nationalit>().Where(x => x.Nationalitatea == Nationalitatea.Text);
                    idNational = nat.First().IdNational;

                    int idInstitutii = -1;

                    var institutii = db.GetTable<DenInst>().Where(x => x.DenumInst == Institutia.Text);
                    idInstitutii = institutii.First().IdInst;
                    int idcat = -1;

                    var cated = db.GetTable<Catedre>().Where(x => x.DenumireCatedra == Catedra.Text);
                    idcat = cated.First().IdCatedra;

                    int idGradDidactic = -1;

                    var Grad = db.GetTable<GradulDidactic>().Where(x => x.Gradul == GradDidactic.Text);
                    idGradDidactic = Grad.First().IdGradDidactic;

                    int idGradStiinte = -1;

                    var GrStiinte = db.GetTable<GrStiinte>().Where(x => x.Garad == TStiintific.Text);
                    idGradStiinte = GrStiinte.First().Idgrstiint;

                    DateTime dataElib = new DateTime();

                    if (DataEliberarii.SelectedDate == null)
                    {
                        dataElib = new DateTime(1800, 1, 1);
                    }
                    else
                        dataElib = DataEliberarii.SelectedDate.Value;

                    double stagiu;
                    if (StagiuPedagogic.Text == "")
                    {
                        stagiu = 0;
                    }
                    else
                    {
                        string number = StagiuPedagogic.Text;
                        if (number.Contains("."))
                        {
                            double OutVal;
                            double.TryParse(number, out OutVal);
                            if (OutVal.ToString() == StagiuPedagogic.Text)
                                stagiu = Convert.ToDouble(number);
                            else
                                stagiu = Convert.ToDouble(number.Replace(".", ","));
                        }
                        else
                        {
                            double OutVal;
                            double.TryParse(number, out OutVal);
                            if (OutVal.ToString() == StagiuPedagogic.Text)
                                stagiu = Convert.ToDouble(number);
                            else
                                stagiu = Convert.ToDouble(number.Replace(',', '.'));
                        }
                    }

                    string caminul;

                    if (CazareCheckBox.IsChecked == true)
                    {
                        caminul = CaminComboBox.Text;
                    }
                    else
                    {
                        caminul = "";
                    }

                    string gradManager;

                    if (StManagerCheck.IsChecked == true)
                    {
                        gradManager = StManagerialeBox.Text;
                    }
                    else
                    {
                        gradManager = "";
                    }

                    DateTime PrimireaGradului = new DateTime();

                    if (GradDidactic.Text == "Nu are")
                    {
                        PrimireaGradului = new DateTime(1800, 1, 1);
                    }
                    else
                        PrimireaGradului = DataGrad.SelectedDate.Value;

                    double vechimea;
                    if (Vechimea.Text == "")
                    {
                        vechimea = 0;
                    }
                    else
                    {
                        string number = Vechimea.Text;
                        if (number.Contains("."))
                        {
                            double OutVal;
                            double.TryParse(number, out OutVal);
                            if (OutVal.ToString() == Vechimea.Text)
                                vechimea = Convert.ToDouble(number);
                            else
                                vechimea = Convert.ToDouble(number.Replace(".", ","));
                        }
                        else
                        {
                            double OutVal;
                            double.TryParse(number, out OutVal);
                            if (OutVal.ToString() == Vechimea.Text)
                                vechimea = Convert.ToDouble(number);
                            else
                                vechimea = Convert.ToDouble(number.Replace(',', '.'));
                        }
                    }


                    Persoane newPers = new Persoane
                    {
                        Nume = Nume.Text,
                        Prenume = Prenume.Text,
                        DataNast = DataNasterii.SelectedDate.Value.Date,
                        Gen = GenMasculin.IsChecked.Value,
                        IDNP = IDNP.Text,
                        TelefonMob = Telefon.Text,
                        Email = Email.Text,
                        idRaion = idRaion,
                        idNational = idNational,
                        IdCatedra = idcat,
                        IdGradDidactic = idGradDidactic,
                        DataPrimiriiGrad = PrimireaGradului,
                        idgradstiint = idGradStiinte,
                        idInst = idInstitutii,
                        ArStudiiPed = StudiiPedagogice.IsChecked.Value,
                        VirstaPens = VarstaPensionara.IsChecked.Value,
                        idStudii = idStudii,
                        StudiiManager = StManagerCheck.IsChecked.Value,
                        GradManager = gradManager,
                        CazatCam = CazareCheckBox.IsChecked.Value,
                        Camin = caminul,
                        ModAngaj = AngajarePermanent.IsChecked.Value,
                        Statutul = Statutul.Text,
                        ConcediuTermenL = ConcediuTLung.IsChecked.Value,
                        VechimeInMunc = vechimea,
                        DataAngajarii = DataAngajarii.SelectedDate.Value,
                        DataEliberarii = dataElib.Date,
                        StagiuPedagogic = stagiu,
                        EsteProfesor = EsteProfesori.IsChecked.Value,
                    };
                    db.GetTable<Persoane>().InsertOnSubmit(newPers);
                    db.SubmitChanges();

                    Table<Functii> functii = db.GetTable<Functii>();
                    var list = from f in functii
                               select new { ID = f.IdFunctie, Functia = f.DenFunctie };
                    int IDFunctie = 0, IDProf;
                    bool esteFunc = false;
                    foreach (var l in list)
                    {
                        if (l.Functia.ToString() == "Profesor")
                        {
                            esteFunc = true;
                            IDFunctie = l.ID;
                        }
                    }

                    if (!esteFunc)
                    {
                        Functii newfn = new Functii
                        {
                            DenFunctie = "Profesor"
                        };
                        db.GetTable<Functii>().InsertOnSubmit(newfn);
                        db.SubmitChanges();

                        IDFunctie = db.GetTable<Functii>().OrderByDescending(x => x.IdFunctie).Select(x => x.IdFunctie).FirstOrDefault();
                    }

                    IDProf = db.GetTable<Persoane>().OrderByDescending(x => x.IdPerson).Select(x => x.IdPerson).FirstOrDefault();
                    double unitati;
                    string numbers = "1";
                    if (numbers.Contains("."))
                    {
                        double OutVal;
                        double.TryParse(numbers, out OutVal);
                        if (OutVal.ToString() == "1")
                            unitati = Convert.ToDouble(numbers);
                        else
                            unitati = Convert.ToDouble(numbers.Replace(".", ","));
                    }
                    else
                    {
                        double OutVal;
                        double.TryParse(numbers, out OutVal);
                        if (OutVal.ToString() == "1")
                            unitati = Convert.ToDouble(numbers);
                        else
                            unitati = Convert.ToDouble(numbers.Replace(',', '.'));
                    }


                    PersoanaFunc newProf = new PersoanaFunc
                    {
                        IdFunctie = IDFunctie,
                        IdPerson = IDProf,
                        Unitati = unitati,
                        ClasaSalar = 55,
                    };
                    db.GetTable<PersoanaFunc>().InsertOnSubmit(newProf);
                    db.SubmitChanges();


                    Arata();

                    Schimba();

                    MessageBox.Show("Persoana a fost adăugată cu succes", "Information", MessageBoxButton.OK, MessageBoxImage.Information);

                }
                else { MessageBox.Show(err2, "Warning", MessageBoxButton.OK, MessageBoxImage.Warning); }

            }
            else { MessageBox.Show(err, "Error", MessageBoxButton.OK, MessageBoxImage.Error); }

        }

        private void Localitatea_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataContext db = new DataContext(conn);

            Table<Raionull> raion = db.GetTable<Raionull>();
            Table<Localit> localitatea = db.GetTable<Localit>();

            var list = from r in raion
                       join l in localitatea on r.idLocal equals l.IdLocal
                       where l.Localitate == Localitatea.SelectedItem.ToString()
                       select r.RaionDenum;
            Raion.ItemsSource = list.ToList();

            Raion.SelectedIndex = 0;
        }

        string Idnp;

        private void Modifica(object sender, RoutedEventArgs e)
        {
            DataContext db = new DataContext(conn);

            Table<Raionull> raion = db.GetTable<Raionull>();
            Table<Localit> localitatea = db.GetTable<Localit>();
            Table<Nationalit> nationalitatea = db.GetTable<Nationalit>();
            Table<DenInst> institutia = db.GetTable<DenInst>();
            Table<Functii> functia = db.GetTable<Functii>();
            Table<Persoane> persoana = db.GetTable<Persoane>();
            Table<Stud> studii = db.GetTable<Stud>();
            Table<FormCont> formare = db.GetTable<FormCont>();
            Table<Functii> functie = db.GetTable<Functii>();
            Table<Activ> activitati = db.GetTable<Activ>();
            Table<GrStiinte> grad = db.GetTable<GrStiinte>();
            Table<Catedre> catedra = db.GetTable<Catedre>();
            Table<GradulDidactic> grDidactic = db.GetTable<GradulDidactic>();


            if (Persoane.SelectedItems.Count > 0)
            {

                for (int i = 0; i < Persoane.SelectedItems.Count; i++)
                {
                    var perslist = from p in persoana
                                   join r in raion on p.idRaion equals r.IdRaion
                                   join l in localitatea on r.idLocal equals l.IdLocal
                                   join ins in institutia on p.idInst equals ins.IdInst
                                   join n in nationalitatea on p.idNational equals n.IdNational
                                   join s in studii on p.idStudii equals s.IdStudii
                                   join g in grad on p.idgradstiint equals g.Idgrstiint
                                   join c in catedra on p.IdCatedra equals c.IdCatedra
                                   join gr in grDidactic on p.IdGradDidactic equals gr.IdGradDidactic
                                   select new
                                   {
                                       Nume = p.Nume,
                                       Prenume = p.Prenume,
                                       DataNasterii = p.DataNast.ToShortDateString(),
                                       Genul = p.Gen,
                                       Idnp = p.IDNP,
                                       Email = p.Email,
                                       Mobil = p.TelefonMob,
                                       EsteProfesor = p.EsteProfesor,
                                       Catedra = c.DenumireCatedra,
                                       GradDidactic = gr.Gradul,
                                       DataPrimiriiGradului = p.DataPrimiriiGrad.ToShortDateString(),
                                       ModulAngajarii = p.ModAngaj,
                                       Nationalitatea = n.Nationalitatea,
                                       Localitatea = l.Localitate + " " + r.RaionDenum,
                                       VarstaPensionara = p.VirstaPens,
                                       Institutia = ins.DenumInst,
                                       Studii = s.Studiile,
                                       StudiiManageriale = p.StudiiManager,
                                       GradulManagerial = p.GradManager,
                                       StudiiPedagogice = p.ArStudiiPed,
                                       StagiulPedagogic = p.StagiuPedagogic,
                                       DataAngajaeii = p.DataAngajarii.ToShortDateString(),
                                       DataEliberarii = p.DataEliberarii.ToShortDateString(),
                                       VechimeInMunca = p.VechimeInMunc,
                                       CazatCamin = p.CazatCam,
                                       Camin = p.Camin,
                                       Statutul = p.Statutul,
                                       GradulStiintific = g.Garad,
                                       ConcediuTermenLung = p.ConcediuTermenL,
                                   };
                    foreach (var l in perslist)
                    {
                        if (l.ToString() == Persoane.SelectedItem.ToString())
                        {
                            Nume.Text = l.Nume;
                            Prenume.Text = l.Prenume;
                            DataNasterii.SelectedDate = Convert.ToDateTime(l.DataNasterii);
                            if (l.Genul)
                                GenMasculin.IsChecked = true;
                            else
                                GenFeminin.IsChecked = true;
                            IDNP.Text = l.Idnp;
                            Telefon.Text = l.Mobil;
                            Email.Text = l.Email;
                            Localitatea.Text = l.Localitatea.Split(' ')[0];
                            Raion.Text = l.Localitatea.Split(' ')[1];
                            DataAngajarii.SelectedDate = Convert.ToDateTime(l.DataAngajaeii);
                            if (l.DataEliberarii != "1/1/1800" && l.DataEliberarii != "1.1.1800")
                                DataEliberarii.SelectedDate = Convert.ToDateTime(l.DataEliberarii);
                            else
                            {
                                var t2 = DataEliberarii.Template.FindName("PART_TextBox", DataEliberarii) as DatePickerTextBox;
                                if (t2 != null)
                                    t2.Text = "Select date";
                            }
                            Catedra.Text = l.Catedra;
                            GradDidactic.Text = l.GradDidactic;
                            TStiintific.Text = l.GradulStiintific;
                            StudiiPedagogice.IsChecked = l.StudiiPedagogice;
                            VarstaPensionara.IsChecked = l.VarstaPensionara;
                            Studii.Text = l.Studii;
                            StManagerCheck.IsChecked = l.StudiiManageriale;
                            if (l.StudiiManageriale)
                            {
                                StudiiLabel.Visibility = Visibility.Visible;
                                StManagerialeBox.Visibility = Visibility.Visible;
                                HiddenBorder.Visibility = Visibility.Visible;
                            }
                            else
                            {
                                StudiiLabel.Visibility = Visibility.Hidden;
                                StManagerialeBox.Visibility = Visibility.Hidden;
                                HiddenBorder.Visibility = Visibility.Hidden;
                            }
                            StManagerialeBox.Text = l.GradulManagerial;
                            CazareCheckBox.IsChecked = l.CazatCamin;

                            if (l.CazatCamin)
                            {
                                CazareLabel.Visibility = Visibility.Visible;
                                CaminComboBox.Visibility = Visibility.Visible;
                                CaminComboBox.Text = l.Camin;
                            }
                            else
                            {
                                CazareLabel.Visibility = Visibility.Hidden;
                                CaminComboBox.Visibility = Visibility.Hidden;
                                CaminComboBox.Text = "1";
                            }
                            if (l.ModulAngajarii)
                                AngajarePermanent.IsChecked = true;
                            else
                                AngajareCumult.IsChecked = true;
                            Statutul.Text = l.Statutul;
                            EsteProfesori.IsChecked = l.EsteProfesor;
                            StagiuPedagogic.Text = l.StagiulPedagogic.ToString();
                            ConcediuTLung.IsChecked = l.ConcediuTermenLung;
                            Institutia.Text = l.Institutia;

                            if (l.DataPrimiriiGradului != "1/1/1800" && l.DataPrimiriiGradului != "1.1.1800")
                                DataGrad.SelectedDate = Convert.ToDateTime(l.DataPrimiriiGradului);
                            else
                            {
                                var t3 = DataGrad.Template.FindName("PART_TextBox", DataGrad) as DatePickerTextBox;
                                if (t3 != null)
                                    t3.Text = "Select date";
                            }
                            Vechimea.Text = l.VechimeInMunca.ToString();

                            Idnp = l.Idnp;

                            Add.Visibility = Visibility.Hidden;
                            Modificare.Visibility = Visibility.Visible;
                            Anuleaza.Visibility = Visibility.Visible;
                        }
                    }
                }
            }
            else
                MessageBox.Show("Alegeți o persoană pentru a o modifica", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        private void Sterge(object sender, RoutedEventArgs e)
        {
            DataContext db = new DataContext(conn);

            Table<Raionull> raion = db.GetTable<Raionull>();
            Table<Localit> localitatea = db.GetTable<Localit>();
            Table<Nationalit> nationalitatea = db.GetTable<Nationalit>();
            Table<DenInst> institutia = db.GetTable<DenInst>();
            Table<Functii> functia = db.GetTable<Functii>();
            Table<Persoane> persoana = db.GetTable<Persoane>();
            Table<Stud> studii = db.GetTable<Stud>();
            Table<FormCont> formare = db.GetTable<FormCont>();
            Table<Functii> functie = db.GetTable<Functii>();
            Table<Activ> activitati = db.GetTable<Activ>();
            Table<GrStiinte> grad = db.GetTable<GrStiinte>();
            Table<Catedre> catedra = db.GetTable<Catedre>();
            Table<GradulDidactic> grDidactic = db.GetTable<GradulDidactic>();
            Table<ProfDisc> intermediar = db.GetTable<ProfDisc>();

            if (Persoane.SelectedItems.Count > 0)
            {
                bool deleted = true;
                for (int i = 0; i < Persoane.SelectedItems.Count; i++)
                {
                    var perslist = from p in persoana
                                   join r in raion on p.idRaion equals r.IdRaion
                                   join l in localitatea on r.idLocal equals l.IdLocal
                                   join ins in institutia on p.idInst equals ins.IdInst
                                   join n in nationalitatea on p.idNational equals n.IdNational
                                   join s in studii on p.idStudii equals s.IdStudii
                                   join g in grad on p.idgradstiint equals g.Idgrstiint
                                   join c in catedra on p.IdCatedra equals c.IdCatedra
                                   join gr in grDidactic on p.IdGradDidactic equals gr.IdGradDidactic
                                   select new
                                   {
                                       Nume = p.Nume,
                                       Prenume = p.Prenume,
                                       DataNasterii = p.DataNast.ToShortDateString(),
                                       Genul = p.Gen,
                                       Idnp = p.IDNP,
                                       Email = p.Email,
                                       Mobil = p.TelefonMob,
                                       EsteProfesor = p.EsteProfesor,
                                       Catedra = c.DenumireCatedra,
                                       GradDidactic = gr.Gradul,
                                       DataPrimiriiGradului = p.DataPrimiriiGrad.ToShortDateString(),
                                       ModulAngajarii = p.ModAngaj,
                                       Nationalitatea = n.Nationalitatea,
                                       Localitatea = l.Localitate + " " + r.RaionDenum,
                                       VarstaPensionara = p.VirstaPens,
                                       Institutia = ins.DenumInst,
                                       Studii = s.Studiile,
                                       StudiiManageriale = p.StudiiManager,
                                       GradulManagerial = p.GradManager,
                                       StudiiPedagogice = p.ArStudiiPed,
                                       StagiulPedagogic = p.StagiuPedagogic,
                                       DataAngajaeii = p.DataAngajarii.ToShortDateString(),
                                       DataEliberarii = p.DataEliberarii.ToShortDateString(),
                                       VechimeInMunca = p.VechimeInMunc,
                                       CazatCamin = p.CazatCam,
                                       Camin = p.Camin,
                                       Statutul = p.Statutul,
                                       GradulStiintific = g.Garad,
                                       ConcediuTermenLung = p.ConcediuTermenL,
                                   };
                    foreach (var l in perslist)
                    {
                        if (l.ToString() == Persoane.SelectedItem.ToString())
                        {
                            Idnp = l.Idnp;
                            deleted = false;
                        }
                    }
                }
                if (!deleted)
                {
                    var list = from p in persoana
                               join d in intermediar on p.IdPerson equals d.idPerson
                               where p.IDNP == Idnp
                               select p;
                    var list2 = from p in persoana
                                join f in formare on p.IdPerson equals f.IdPerson
                                where p.IDNP == Idnp
                                select p;
                    var list3 = from p in persoana
                                join f in activitati on p.IdPerson equals f.IdPerson
                                where p.IDNP == Idnp
                                select p;
                    string intrebare;

                    if (list.Count() > 0 && list2.Count() > 0 && list3.Count() > 0)
                        intrebare = "Această persoană are date suplimentare, doriți să o ștergeți ?";
                    else
                        intrebare = "Doriți să ștergeți această persoană ?";

                    var Result = MessageBox.Show(intrebare, "Question", MessageBoxButton.YesNo, MessageBoxImage.Question);

                    if (Result == MessageBoxResult.Yes)
                    {
                        var pers = db.GetTable<Persoane>().Where(p => p.IDNP == Idnp).FirstOrDefault();
                        db.GetTable<Persoane>().DeleteOnSubmit(pers);
                        db.SubmitChanges();
                        Arata();
                    }
                    Idnp = "";
                }
                else
                {
                    MessageBox.Show("Această înregistrare a fost ștearsă anterior", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    Arata();
                }
            }
            else
                MessageBox.Show("Alegeți o persoană pentru a o șterge", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);

        }

        private void LocalForm_Click(object sender, RoutedEventArgs e)
        {
            LocalitateForm a = new LocalitateForm();
            a.Show();

        }

        private void RaionForm_Click(object sender, RoutedEventArgs e)
        {
            RaionulForm a = new RaionulForm();
            a.Show();

        }

        private void NationalForm_Click(object sender, RoutedEventArgs e)
        {
            NationalitateaForm a = new NationalitateaForm();
            a.Show();

        }

        private void CatedrForm_Click(object sender, RoutedEventArgs e)
        {
            CatedraForm a = new CatedraForm();
            a.Show();

        }

        private void GrStiinForm_Click(object sender, RoutedEventArgs e)
        {
            GradStiintific a = new GradStiintific();
            a.Show();

        }

        private void InstitutForm_Click(object sender, RoutedEventArgs e)
        {
            InstitutiaForm a = new InstitutiaForm();
            a.Show();

        }

        private void StudiiForm_Click(object sender, RoutedEventArgs e)
        {
            StudiiForm a = new StudiiForm();
            a.Show();

        }

        private void Modifica_Click(object sender, RoutedEventArgs e)
        {
            Regex regNume = new Regex("^[A-ZĂÎÂȘȚ][a-zăîâșț]{2,}$");
            Regex regTel = new Regex("^[0][0-9]{8}$");
            Regex regIDNP = new Regex("^[1-9][0-9]{12}$");
            Regex regInstitutii = new Regex("^[A-Za-zĂÎÂȘȚăîâșț ]{1,}$");
            Regex regMail = new Regex("^[a-zA-Z0-9!#$%^&*\\-_+;:',./~]{3,}[@][a-z]{3,}[.]([a-z]{2}|[a-z]{3})$");
            Regex regStag = new Regex("^(([0-9]|[1-9][0-9]{1,})|([0-9]|[1-9][0-9]{1,})([.]|[,])([1-9]|[0-9]{1,}[1-9]))$");
            string err = "";
            string err2 = "";
            if (!regNume.IsMatch(Nume.Text))
            {
                err += "Date incorecte la nume (Doar litere, prima să fie majusculă) \n";
            }
            if (!regNume.IsMatch(Prenume.Text))
            {
                err += "Date incorecte la prenume (Doar litere, prima să fie majusculă) \n";
            }
            if (DataNasterii.SelectedDate == null)
            {
                err += "Date incorecte la data nașterii \n";
            }
            if (GenFeminin.IsChecked == false && GenMasculin.IsChecked == false)
            {
                err += "Indicați genul \n";
            }
            if (!regIDNP.IsMatch(IDNP.Text))
            {
                err += "Date incorecte la IDNP (13 cifre) \n";
            }
            if (!regTel.IsMatch(Telefon.Text))
            {
                err += "Date incorecte la telefon (9 cifre, prima este 0) \n";
            }
            if (!regMail.IsMatch(Email.Text))
            {
                err += "Date incorecte la email \n";
            }
            if (StManagerCheck.IsChecked == true)
            {
                if (!regInstitutii.IsMatch(StManagerialeBox.Text))
                {
                    err += "Date incorecte la studii manageriale (Doar litere și caractere speciale) \n";
                }
            }
            if (AngajareCumult.IsChecked == false && AngajarePermanent.IsChecked == false)
            {
                err += "Indicați modul de angajare \n";
            }
            if (!regInstitutii.IsMatch(Statutul.Text))
            {
                err += "Date incorecte la statut (Doar litere și caractere speciale) \n";
            }
            if (DataAngajarii.SelectedDate == null)
            {
                err += "Date incorecte la data angajării \n";
            }
            if (!regStag.IsMatch(StagiuPedagogic.Text) && StagiuPedagogic.Text != "")
            {
                err += "Date incorecte la stagiu pedagogic (Doar numere întregi \n";
            }
            if (!regStag.IsMatch(Vechimea.Text) && Vechimea.Text != "")
            {
                err += "Date incorecte la vechimea în muncă (Doar numere întregi \n";
            }
            if (GradDidactic.Text != "Nu are")
            {
                if (DataGrad.SelectedDate == null)
                {
                    err += "Date incorecte la data primirii gradului didactic \n";
                }
            }

            if (err == "")
            {
                DataContext db = new DataContext(conn);
                Table<Raionull> raion = db.GetTable<Raionull>();
                Table<Localit> localitatea = db.GetTable<Localit>();
                Table<Nationalit> nationalitatea = db.GetTable<Nationalit>();
                Table<DenInst> institutia = db.GetTable<DenInst>();
                Table<Functii> functia = db.GetTable<Functii>();
                Table<Persoane> persoana = db.GetTable<Persoane>();
                Table<Stud> studii = db.GetTable<Stud>();
                Table<FormCont> formare = db.GetTable<FormCont>();
                Table<Functii> functie = db.GetTable<Functii>();
                Table<Activ> activitati = db.GetTable<Activ>();
                Table<GrStiinte> grad = db.GetTable<GrStiinte>();
                Table<Catedre> catedra = db.GetTable<Catedre>();
                Table<GradulDidactic> grDidactic = db.GetTable<GradulDidactic>();

                foreach (var f in persoana)
                {
                    if (f.IDNP == IDNP.Text && f.IDNP != Idnp)
                    {
                        err2 += "Persoana cu asa IDNP există \n";
                    }
                }
                if (err2 == "")
                {
                    int idLOcal = -1;

                    var local = db.GetTable<Localit>().Where(x => x.Localitate == Localitatea.Text);
                    idLOcal = local.First().IdLocal;


                    int idRaion = -1;

                    var raionul = db.GetTable<Raionull>().Where(x => x.RaionDenum == Raion.Text);
                    idRaion = raionul.First().IdRaion;

                    int idStudii = -1;

                    var studiile = db.GetTable<Stud>().Where(x => x.Studiile == Studii.Text);
                    idStudii = studiile.First().IdStudii;

                    int idNational = -1;

                    var nat = db.GetTable<Nationalit>().Where(x => x.Nationalitatea == Nationalitatea.Text);
                    idNational = nat.First().IdNational;

                    int idInstitutii = -1;

                    var institutii = db.GetTable<DenInst>().Where(x => x.DenumInst == Institutia.Text);
                    idInstitutii = institutii.First().IdInst;
                    int idcat = -1;

                    var cated = db.GetTable<Catedre>().Where(x => x.DenumireCatedra == Catedra.Text);
                    idcat = cated.First().IdCatedra;

                    int idGradDidactic = -1;

                    var Grad = db.GetTable<GradulDidactic>().Where(x => x.Gradul == GradDidactic.Text);
                    idGradDidactic = Grad.First().IdGradDidactic;

                    int idGradStiinte = -1;

                    var GrStiinte = db.GetTable<GrStiinte>().Where(x => x.Garad == TStiintific.Text);
                    idGradStiinte = GrStiinte.First().Idgrstiint;

                    DateTime dataElib = new DateTime();

                    if (DataEliberarii.SelectedDate == null)
                    {
                        dataElib = new DateTime(1800, 1, 1);
                    }
                    else
                        dataElib = DataEliberarii.SelectedDate.Value;

                    double stagiu;
                    if (StagiuPedagogic.Text == "")
                    {
                        stagiu = 0;
                    }
                    else
                    {
                        string number = StagiuPedagogic.Text;
                        if (number.Contains("."))
                        {
                            double OutVal;
                            double.TryParse(number, out OutVal);
                            if (OutVal.ToString() == StagiuPedagogic.Text)
                                stagiu = Convert.ToDouble(number);
                            else
                                stagiu = Convert.ToDouble(number.Replace(".", ","));
                        }
                        else
                        {
                            double OutVal;
                            double.TryParse(number, out OutVal);
                            if (OutVal.ToString() == StagiuPedagogic.Text)
                                stagiu = Convert.ToDouble(number);
                            else
                                stagiu = Convert.ToDouble(number.Replace(',', '.'));
                        }
                    }

                    double vechimea;
                    if (Vechimea.Text == "")
                    {
                        vechimea = 0;
                    }
                    else
                    {
                        string number = Vechimea.Text;
                        if (number.Contains("."))
                        {
                            double OutVal;
                            double.TryParse(number, out OutVal);
                            if (OutVal.ToString() == Vechimea.Text)
                                vechimea = Convert.ToDouble(number);
                            else
                                vechimea = Convert.ToDouble(number.Replace(".", ","));
                        }
                        else
                        {
                            double OutVal;
                            double.TryParse(number, out OutVal);
                            if (OutVal.ToString() == Vechimea.Text)
                                vechimea = Convert.ToDouble(number);
                            else
                                vechimea = Convert.ToDouble(number.Replace(',', '.'));
                        }
                    }

                    string caminul;

                    if (CazareCheckBox.IsChecked == true)
                    {
                        caminul = CaminComboBox.Text;
                    }
                    else
                    {
                        caminul = "";
                    }

                    string gradManager;

                    if (StManagerCheck.IsChecked == true)
                    {
                        gradManager = StManagerialeBox.Text;
                    }
                    else
                    {
                        gradManager = "";
                    }

                    DateTime PrimireaGradului = new DateTime();

                    if (GradDidactic.Text == "Nu are")
                    {
                        PrimireaGradului = new DateTime(1800, 1, 1);
                    }
                    else
                        PrimireaGradului = DataGrad.SelectedDate.Value;

                    foreach (var p in persoana)
                    {
                        if (p.IDNP == Idnp)
                        {
                            p.Nume = Nume.Text;
                            p.Prenume = Prenume.Text;
                            p.DataNast = DataNasterii.SelectedDate.Value.Date;
                            p.Gen = GenMasculin.IsChecked.Value;
                            p.IDNP = IDNP.Text;
                            p.TelefonMob = Telefon.Text;
                            p.Email = Email.Text;
                            p.idRaion = idRaion;
                            p.idNational = idNational;
                            p.IdCatedra = idcat;
                            p.IdGradDidactic = idGradDidactic;
                            p.idgradstiint = idGradStiinte;
                            p.idInst = idInstitutii;
                            p.ArStudiiPed = StudiiPedagogice.IsChecked.Value;
                            p.VirstaPens = VarstaPensionara.IsChecked.Value;
                            p.idStudii = idStudii;
                            p.StudiiManager = StManagerCheck.IsChecked.Value;
                            p.GradManager = gradManager;
                            p.CazatCam = CazareCheckBox.IsChecked.Value;
                            p.Camin = caminul;
                            p.ModAngaj = AngajarePermanent.IsChecked.Value;
                            p.Statutul = Statutul.Text;
                            p.ConcediuTermenL = ConcediuTLung.IsChecked.Value;
                            p.DataAngajarii = DataAngajarii.SelectedDate.Value;
                            p.DataEliberarii = dataElib.Date;
                            p.StagiuPedagogic = stagiu;
                            p.EsteProfesor = EsteProfesori.IsChecked.Value;
                            p.VechimeInMunc = vechimea;
                            p.DataPrimiriiGrad = PrimireaGradului;
                            db.SubmitChanges();

                            MessageBox.Show("Datele despre persoană au fost modificate cu succes", "Information", MessageBoxButton.OK, MessageBoxImage.Information);

                            if (EsteProfesori.IsChecked == true)
                            {
                                Table<PersoanaFunc> fpers = db.GetTable<PersoanaFunc>();
                                Table<Functii> functii = db.GetTable<Functii>();
                                var list = from f in functii
                                           select new { ID = f.IdFunctie, Functia = f.DenFunctie };
                                int IDFunctie = 0, IDProf;
                                bool esteFunc = false;

                                bool areFunct = false;
                                var prdiss = from fp in fpers
                                             join per in persoana on fp.IdPerson equals p.IdPerson
                                             join f in functii on fp.IdFunctie equals f.IdFunctie
                                             select new
                                             {
                                                 Numele = per.Nume,
                                                 Prenume = per.Prenume,
                                                 Funcția = f.DenFunctie,
                                                 Unitați = fp.Unitati,
                                                 ClSalarizare = fp.ClasaSalar,
                                             };

                                foreach (var c in prdiss)
                                {
                                    if (c.Numele == Nume.Text && c.Prenume == Prenume.Text && c.Funcția == "Profesor")
                                    {
                                        areFunct = true;
                                    }
                                }

                                if (!areFunct)
                                {
                                    foreach (var l in list)
                                    {
                                        if (l.Functia.ToString() == "Profesor")
                                        {
                                            esteFunc = true;
                                            IDFunctie = l.ID;
                                        }
                                    }

                                    if (!esteFunc)
                                    {
                                        Functii newfn = new Functii
                                        {
                                            DenFunctie = "Profesor"
                                        };
                                        db.GetTable<Functii>().InsertOnSubmit(newfn);
                                        db.SubmitChanges();

                                        IDFunctie = db.GetTable<Functii>().OrderByDescending(x => x.IdFunctie).Select(x => x.IdFunctie).FirstOrDefault();
                                    }

                                    IDProf = db.GetTable<Persoane>().Where(x => x.IDNP == IDNP.Text).Select(x => x.IdPerson).FirstOrDefault();
                                    double unitati;
                                    string numbers = "1";
                                    if (numbers.Contains("."))
                                    {
                                        double OutVal;
                                        double.TryParse(numbers, out OutVal);
                                        if (OutVal.ToString() == "1")
                                            unitati = Convert.ToDouble(numbers);
                                        else
                                            unitati = Convert.ToDouble(numbers.Replace(".", ","));
                                    }
                                    else
                                    {
                                        double OutVal;
                                        double.TryParse(numbers, out OutVal);
                                        if (OutVal.ToString() == "1")
                                            unitati = Convert.ToDouble(numbers);
                                        else
                                            unitati = Convert.ToDouble(numbers.Replace(',', '.'));
                                    }


                                    PersoanaFunc newProf = new PersoanaFunc
                                    {
                                        IdFunctie = IDFunctie,
                                        IdPerson = IDProf,
                                        Unitati = unitati,
                                        ClasaSalar = 55,
                                    };
                                    db.GetTable<PersoanaFunc>().InsertOnSubmit(newProf);
                                    db.SubmitChanges();
                                }
                            }
                            else
                            {
                                Table<PersoanaFunc> fpers = db.GetTable<PersoanaFunc>();
                                Table<Functii> functii = db.GetTable<Functii>();

                                var prdiss = from fp in fpers
                                             join per in persoana on fp.IdPerson equals p.IdPerson
                                             join f in functii on fp.IdFunctie equals f.IdFunctie
                                             select new
                                             {
                                                 Numele = p.Nume,
                                                 Prenume = p.Prenume,
                                                 Funcția = f.DenFunctie,
                                                 Unitați = fp.Unitati,
                                                 ClSalarizare = fp.ClasaSalar,
                                             };
                                string funct = "", profes = "";
                                int IDFunctie = 0;
                                int IDProf = 0;
                                bool deleted = true;

                                foreach (var c in prdiss)
                                {
                                    if (c.Numele == Nume.Text && c.Prenume == Prenume.Text && c.Funcția == "Profesor")
                                    {
                                        funct = c.Funcția;
                                        profes = c.Numele + " " + c.Prenume;
                                        deleted = false;
                                    }
                                }
                                if (!deleted)
                                {
                                    var list = from fn in functii
                                               select new { ID = fn.IdFunctie, Functia = fn.DenFunctie };
                                    foreach (var l in list)
                                    {
                                        if (l.Functia.ToString() == funct)
                                        {
                                            IDFunctie = l.ID;
                                        }
                                    }
                                    string a = profes;
                                    string[] prof = a.Split(' ');

                                    var list3 = from per in persoana
                                                select new { ID = per.IdPerson, Nume = per.Nume, Prenume = per.Prenume };
                                    foreach (var l in list3)
                                    {
                                        if (l.Nume.ToString() == prof[0] && l.Prenume.ToString() == prof[1])
                                        {
                                            IDProf = l.ID;
                                        }
                                    }

                                    var profdel = db.GetTable<PersoanaFunc>().Where(u => u.IdFunctie == IDFunctie).Where(u => u.IdPerson == IDProf).FirstOrDefault();
                                    db.GetTable<PersoanaFunc>().DeleteOnSubmit(profdel);
                                    db.SubmitChanges();
                                }
                            }


                            Arata();

                            Schimba();

                            Add.Visibility = Visibility.Visible;
                            Modificare.Visibility = Visibility.Hidden;
                            Anuleaza.Visibility = Visibility.Hidden;
                            Idnp = "";

                            break;
                        }
                    }
                }
                else { MessageBox.Show(err2, "Warning", MessageBoxButton.OK, MessageBoxImage.Warning); }
            }
            else { MessageBox.Show(err, "Error", MessageBoxButton.OK, MessageBoxImage.Error); }
        }

        private void DataAngajarii_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataEliberarii.SelectedDate != null && DataEliberarii.SelectedDate < DataAngajarii.SelectedDate)
                DataEliberarii.SelectedDate = DataAngajarii.SelectedDate.Value;
            if (DataAngajarii.SelectedDate != null)
                DataEliberarii.DisplayDateStart = DataAngajarii.SelectedDate.Value;
            else
                DataEliberarii.DisplayDateStart = new DateTime(1900, 1, 1);
        }

        private void Anuleaza_Click(object sender, RoutedEventArgs e)
        {
            Schimba();
            Idnp = "";

            Modificare.Visibility = Visibility.Hidden;
            Add.Visibility = Visibility.Visible;
            Anuleaza.Visibility = Visibility.Hidden;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

            MeniuPrin m = new MeniuPrin();
            m.Show();

        }

        private void GradDidactic_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (GradDidactic.SelectedItem.ToString() == "Nu are")
            {
                DataGrad.Visibility = Visibility.Hidden;
                DataGradLabel.Visibility = Visibility.Hidden;
            }
            else
            {
                DataGrad.Visibility = Visibility.Visible;
                DataGradLabel.Visibility = Visibility.Visible;
            }
        }

        private void Raion_DropDownOpened(object sender, EventArgs e)
        {
            DataContext db = new DataContext(conn);
            Table<Raionull> raion = db.GetTable<Raionull>();
            Table<Localit> localitatea = db.GetTable<Localit>();
            var list2 = from r in raion
                        join l in localitatea on r.idLocal equals l.IdLocal
                        where l.Localitate == Localitatea.Text
                        select r.RaionDenum;
            Raion.ItemsSource = list2.ToList();
        }

        private void Localitatea_DropDownOpened(object sender, EventArgs e)
        {
            DataContext db = new DataContext(conn);

            Table<Localit> localitatea = db.GetTable<Localit>();
            var list = from l in localitatea
                       select l.Localitate;
            Localitatea.ItemsSource = list.ToList();
        }

        private void Nationalitatea_DropDownOpened(object sender, EventArgs e)
        {
            DataContext db = new DataContext(conn);

            Table<Nationalit> nationalitatea = db.GetTable<Nationalit>();
            var list3 = from n in nationalitatea
                        select n.Nationalitatea;
            Nationalitatea.ItemsSource = list3.ToList();
        }

        private void Catedra_DropDownOpened(object sender, EventArgs e)
        {
            DataContext db = new DataContext(conn);

            Table<Catedre> catedra = db.GetTable<Catedre>();
            var listCatedra = from c in catedra
                              select c.DenumireCatedra;
            Catedra.ItemsSource = listCatedra.ToList();
        }

        private void TStiintific_DropDownOpened(object sender, EventArgs e)
        {
            DataContext db = new DataContext(conn);
            Table<GrStiinte> grad = db.GetTable<GrStiinte>();
            var listGrad = from g in grad
                           select g.Garad;
            TStiintific.ItemsSource = listGrad.ToList();
        }

        private void Institutia_DropDownOpened(object sender, EventArgs e)
        {
            DataContext db = new DataContext(conn);


            Table<DenInst> institutia = db.GetTable<DenInst>();
            var list4 = from i in institutia
                        select i.DenumInst;
            Institutia.ItemsSource = list4.ToList();
        }

        private void Studii_DropDownOpened(object sender, EventArgs e)
        {
            DataContext db = new DataContext(conn);


            Table<Stud> studii = db.GetTable<Stud>();
            var listStudii = from s in studii
                             select s.Studiile;
            Studii.ItemsSource = listStudii.ToList();
        }
    }
}
