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
using System.IO;
using System.Diagnostics;
using diploma.Clase;
using System.Text.RegularExpressions;
using System.Windows.Controls.Primitives;

namespace diploma
{
    /// <summary>
    /// Interaction logic for MeniuPrin.xaml
    /// </summary>
    public partial class MeniuPrin : Window
    {
        string conn = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + Environment.CurrentDirectory + @"\Personal.mdf;Integrated Security=True;Connect Timeout=30";

        // Фик­тив­ные столб­цы для сло­ев 0 и 1:
        ColumnDefinition column1CloneForLayer0;
        ColumnDefinition column2CloneForLayer0;
        ColumnDefinition column2CloneForLayer1;
        ColumnDefinition column3CloneForLayer1;
        ColumnDefinition column3CloneForLayer0;
        public MeniuPrin()
        {
            InitializeComponent();
            // Ини­циа­ли­зи­ро­вать фик­тив­ные столб­цы, ис­поль­зуе­мые,
            // ко­гда па­не­ли при­сты­ко­ва­ны
            column1CloneForLayer0 = new ColumnDefinition();
            column1CloneForLayer0.SharedSizeGroup = "column1";
            column2CloneForLayer0 = new ColumnDefinition();
            column2CloneForLayer0.SharedSizeGroup = "column2";
            column2CloneForLayer1 = new ColumnDefinition();
            column2CloneForLayer1.SharedSizeGroup = "column2";
            column3CloneForLayer1 = new ColumnDefinition();
            column3CloneForLayer1.SharedSizeGroup = "column3";
            column3CloneForLayer0 = new ColumnDefinition();
            column3CloneForLayer0.SharedSizeGroup = "column3";
            Form.DisplayDateEnd = DateTime.Now;
            Form2.DisplayDateEnd = DateTime.Now;

            Form.DisplayDateStart = new DateTime(1900, 1, 1);
            Form2.DisplayDateStart = new DateTime(1900, 1, 1);
            DataContext db = new DataContext(conn);
            Table<Persoane> persoana = db.GetTable<Persoane>();
            Table<Raionull> raion = db.GetTable<Raionull>();
            Table<DenInst> institutia = db.GetTable<DenInst>();
            Table<Nationalit> nationalitatea = db.GetTable<Nationalit>();
            Table<Stud> studii = db.GetTable<Stud>();
            Table<FormCont> formare = db.GetTable<FormCont>();
            Table<Functii> functie = db.GetTable<Functii>();
            Table<Activ> activitati = db.GetTable<Activ>();
            Table<GrStiinte> grad = db.GetTable<GrStiinte>();

            Table<Localit> localitatea = db.GetTable<Localit>();
            Table<ProfDisc> prdis = db.GetTable<ProfDisc>();
            Table<Disciplin> dis = db.GetTable<Disciplin>();
            Table<Catedre> catedra = db.GetTable<Catedre>();
            Table<GradulDidactic> grDidactic = db.GetTable<GradulDidactic>();





            var listCatedra = from c in catedra
                              select c.DenumireCatedra;
            cat.ItemsSource = listCatedra.ToList();

            var listfun = from c in functie
                          select c.DenFunctie;
            fun.ItemsSource = listfun.ToList();

            var listti = from c in grad
                         select c.Garad;
            titlustcom.ItemsSource = listti.ToList();



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

            PersAfisGrid.ItemsSource = perslist.ToList();
            NrPers.Content = perslist.Count().ToString();

        }
        // Пе­ре­клю­ча­ем со­стоя­ние: при­сты­ко­ва­на/не при­сты­ко­ва­на (па­нель 1)
        public void pane1Pin_Click(object sender, RoutedEventArgs e)
        {
            if (pane1Button.Visibility == Visibility.Collapsed)
                UndockPane(1);
            else
                DockPane(1);
        }
        // Пе­ре­клю­ча­ем со­стоя­ние: при­сты­ко­ва­на/не при­сты­ко­ва­на (па­нель 2)
        public void pane2Pin_Click(object sender, RoutedEventArgs e)
        {
            if (pane2Button.Visibility == Visibility.Collapsed)
                UndockPane(2);
            else
                DockPane(2);
        }

        // По­ка­зы­ва­ем па­нель 1, ко­гда ука­за­тель мы­ши на­хо­дит­ся над ее кноп­кой
        public void pane1Button_MouseEnter(object sender, RoutedEventArgs e)
        {
            layer1.Visibility = Visibility.Visible;
            // Кор­рек­ти­ру­ем Z-по­ря­док, что­бы па­нель все­гда бы­ла свер­ху:
            Grid.SetZIndex(layer1, 1);
            Grid.SetZIndex(layer2, 0);
            // Скры­ва­ем вто­рую па­нель, ес­ли она не при­сты­ко­ва­на
            if (pane2Button.Visibility == Visibility.Visible)
                layer2.Visibility = Visibility.Collapsed;
            if (pane3Button.Visibility == Visibility.Visible)
                layer3.Visibility = Visibility.Collapsed;
        }
        // По­ка­зы­ва­ем па­нель 2, ко­гда ука­за­тель мы­ши на­хо­дит­ся над ее кноп­кой
        public void pane2Button_MouseEnter(object sender, RoutedEventArgs e)
        {
            layer2.Visibility = Visibility.Visible;
            // Кор­рек­ти­ру­ем Z-по­ря­док, что­бы па­нель все­гда бы­ла свер­ху:
            Grid.SetZIndex(layer2, 1);
            Grid.SetZIndex(layer1, 0);
            // Скры­ва­ем вто­рую па­нель, ес­ли она не при­сты­ко­ва­на
            if (pane1Button.Visibility == Visibility.Visible)
                layer1.Visibility = Visibility.Collapsed;
            if (pane3Button.Visibility == Visibility.Visible)
                layer3.Visibility = Visibility.Collapsed;
        }
        public void pane3Button_MouseEnter(object sender, RoutedEventArgs e)
        {
            layer3.Visibility = Visibility.Visible;
            // Кор­рек­ти­ру­ем Z-по­ря­док, что­бы па­нель все­гда бы­ла свер­ху:
            Grid.SetZIndex(layer3, 1);
            Grid.SetZIndex(layer2, 0);
            Grid.SetZIndex(layer1, 0);

            // Скры­ва­ем вто­рую па­нель, ес­ли она не при­сты­ко­ва­на
            if (pane1Button.Visibility == Visibility.Visible)
                layer1.Visibility = Visibility.Collapsed;
            else if (pane2Button.Visibility == Visibility.Visible)
                layer2.Visibility = Visibility.Collapsed;
        }
        // Скры­ва­ем все не­при­сты­ко­ван­ные па­не­ли, ко­гда ука­за­тель мы­ши
        // пе­ре­ме­ща­ет­ся в слой 0
        public void layer0_MouseEnter(object sender, RoutedEventArgs e)
        {
            if (pane1Button.Visibility == Visibility.Visible)
                layer1.Visibility = Visibility.Collapsed;
            if (pane2Button.Visibility == Visibility.Visible)
                layer2.Visibility = Visibility.Collapsed;
        }
        // Скры­ва­ем вто­рую па­нель, ес­ли она не при­сты­ко­ва­на, ко­гда ука­за­тель
        // мы­ши пе­ре­ме­ща­ет­ся на па­нель 1
        public void pane1_MouseEnter(object sender, RoutedEventArgs e)
        {
            // Скры­ва­ем вто­рую па­нель, ес­ли она не при­сты­ко­ва­на
            if (pane2Button.Visibility == Visibility.Visible)
                layer2.Visibility = Visibility.Collapsed;
            if (pane3Button.Visibility == Visibility.Visible)
                layer3.Visibility = Visibility.Collapsed;

        }
        // Скры­ва­ем вто­рую па­нель, ес­ли она не при­сты­ко­ва­на, ко­гда ука­за­тель
        // мы­ши пе­ре­ме­ща­ет­ся на па­нель 2
        public void pane2_MouseEnter(object sender, RoutedEventArgs e)
        {
            // Скры­ва­ем вто­рую па­нель, ес­ли она не при­сты­ко­ва­на
            if (pane1Button.Visibility == Visibility.Visible)
                layer1.Visibility = Visibility.Collapsed;

        }

        public void pane3_MouseEnter(object sender, RoutedEventArgs e)
        {
            // Скры­ва­ем вто­рую па­нель, ес­ли она не при­сты­ко­ва­на
            if (pane1Button.Visibility == Visibility.Visible)
                layer1.Visibility = Visibility.Collapsed;
            else if (pane2Button.Visibility == Visibility.Visible)
                layer2.Visibility = Visibility.Collapsed;
        }
        // При­сты­ко­вы­ва­ем па­нель, при этом скры­ва­ет­ся
        // со­от­вет­ст­вую­щая ей кноп­ка
        public void DockPane(int paneNumber)
        {
            if (paneNumber == 1)
            {
                pane1Button.Visibility = Visibility.Collapsed;

                // До­бав­ля­ем кло­ни­ро­ван­ный стол­бец в слой 0:Все вместе: создание сворачиваемой, стыкуемой, изменяющей размер панели 191
                layer0.ColumnDefinitions.Add(column1CloneForLayer0);
                // До­бав­ля­ем кло­ни­ро­ван­ный стол­бец в слой 1, но толь­ко ес­ли
                // па­нель 2 при­сты­ко­ва­на:
                if (pane2Button.Visibility == Visibility.Collapsed)
                    layer1.ColumnDefinitions.Add(column2CloneForLayer1);
            }
            else if (paneNumber == 2)
            {
                pane2Button.Visibility = Visibility.Collapsed;

                // До­бав­ля­ем кло­ни­ро­ван­ный стол­бец в слой 0:
                layer0.ColumnDefinitions.Add(column2CloneForLayer0);
                // До­бав­ля­ем кло­ни­ро­ван­ный стол­бец в слой 1, но толь­ко ес­ли
                // па­нель 1 при­сты­ко­ва­на:
                if (pane1Button.Visibility == Visibility.Collapsed)
                    layer1.ColumnDefinitions.Add(column2CloneForLayer1);
            }
            else if (paneNumber == 3)
            {
                pane3Button.Visibility = Visibility.Collapsed;

                // До­бав­ля­ем кло­ни­ро­ван­ный стол­бец в слой 0:
                layer0.ColumnDefinitions.Add(column3CloneForLayer0);
                // До­бав­ля­ем кло­ни­ро­ван­ный стол­бец в слой 1, но толь­ко ес­ли
                // па­нель 1 при­сты­ко­ва­на:
                if (pane1Button.Visibility == Visibility.Collapsed)
                    layer1.ColumnDefinitions.Add(column2CloneForLayer1);
                else if (pane2Button.Visibility == Visibility.Collapsed)
                    layer1.ColumnDefinitions.Add(column2CloneForLayer1);
            }
        }
        // От­сты­ко­вы­ва­ем па­нель, при этом ста­но­вит­ся вид­на
        // со­от­вет­ст­вую­щая ей кноп­ка
        public void UndockPane(int paneNumber)
        {
            if (paneNumber == 1)
            {
                layer1.Visibility = Visibility.Visible;
                pane1Button.Visibility = Visibility.Visible;

                // Уда­ля­ем кло­ни­ро­ван­ные столб­цы из сло­ев 0 и 1:
                layer0.ColumnDefinitions.Remove(column1CloneForLayer0);
                // Этот стол­бец при­сут­ст­ву­ет не все­гда, но ме­тод Remove
                // мол­ча иг­но­ри­ру­ет по­пыт­ку уда­лить не­су­ще­ст­вую­щий стол­бец:
                layer1.ColumnDefinitions.Remove(column2CloneForLayer1);
            }
            else if (paneNumber == 2)
            {
                layer2.Visibility = Visibility.Visible;
                pane2Button.Visibility = Visibility.Visible;

                // Уда­ля­ем кло­ни­ро­ван­ные столб­цы из сло­ев 0 и 1:
                layer0.ColumnDefinitions.Remove(column2CloneForLayer0);
                // Этот стол­бец при­сут­ст­ву­ет не все­гда, но ме­тод Remove
                // мол­ча иг­но­ри­ру­ет по­пыт­ку уда­лить не­су­ще­ст­вую­щий стол­бец:
                layer1.ColumnDefinitions.Remove(column2CloneForLayer1);
            }
        }

        private void per(object sender, RoutedEventArgs e)
        {
            Adauga a = new Adauga();
            this.Close();
            a.Show();
        }

        private void discipl(object sender, RoutedEventArgs e)
        {
            ProfsoriDiscipline a = new ProfsoriDiscipline();
            this.Close();
            a.Show();
        }

        private void activdid(object sender, RoutedEventArgs e)
        {
            ActivitateaDidiact a = new ActivitateaDidiact();
            this.Close();
            a.Show();
        }

        private void formcont(object sender, RoutedEventArgs e)
        {
            FormareContinua a = new FormareContinua();
            this.Close();
            a.Show();
        }

        private void cautot_Click(object sender, RoutedEventArgs e)
        {
            DataContext db = new DataContext(conn);
            Table<Persoane> persoana = db.GetTable<Persoane>();
            Table<Raionull> raion = db.GetTable<Raionull>();
            Table<DenInst> institutia = db.GetTable<DenInst>();
            Table<Nationalit> nationalitatea = db.GetTable<Nationalit>();
            Table<Stud> studii = db.GetTable<Stud>();
            Table<FormCont> formare = db.GetTable<FormCont>();
            Table<Functii> functie = db.GetTable<Functii>();
            Table<Activ> activitati = db.GetTable<Activ>();
            Table<GrStiinte> grad = db.GetTable<GrStiinte>();
            Table<PersoanaFunc> persfun = db.GetTable<PersoanaFunc>();
            Table<Localit> localitatea = db.GetTable<Localit>();
            Table<ProfDisc> prdis = db.GetTable<ProfDisc>();
            Table<Disciplin> dis = db.GetTable<Disciplin>();
            Table<Catedre> catedra = db.GetTable<Catedre>();

            Table<GradulDidactic> grDidactic = db.GetTable<GradulDidactic>();
            Regex regStag = new Regex("^(([0-9]|[1-9][0-9]{1,})|([0-9]|[1-9][0-9]{1,})([.]|[,])([1-9]|[0-9]{1,}[1-9]))$");
            Regex regInstitutii = new Regex("^[A-Za-zĂÎÂȘȚăîâșț ]{1,}$");
            Regex regTel = new Regex("^[0-9][0-9]{7}$");
            Regex regMail = new Regex("^[a-zA-Z0-9!#$%^&*\\-_+;:',./~]{4,}[@][a-z]{3,}[.]([a-z]{2}|[a-z]{3})$");

            double stagiu1;
            if (Stag.Text == "")
            {
                stagiu1 = 0;
            }
            else
            {
                string number =Stag.Text;
                if (number.Contains("."))
                {
                    double OutVal;
                    double.TryParse(number, out OutVal);
                    if (OutVal.ToString() == Stag.Text)
                        stagiu1 = Convert.ToDouble(number);
                    else
                        stagiu1 = Convert.ToDouble(number.Replace(".", ","));
                }
                else
                {
                    double OutVal;
                    double.TryParse(number, out OutVal);
                    if (OutVal.ToString() == Stag.Text)
                        stagiu1 = Convert.ToDouble(number);
                    else
                        stagiu1 = Convert.ToDouble(number.Replace(',', '.'));
                }
            }

            double stagiu2;
            if (Stag2.Text == "")
            {
                stagiu2 = 0;
            }
            else
            {
                string number = Stag2.Text;
                if (number.Contains("."))
                {
                    double OutVal;
                    double.TryParse(number, out OutVal);
                    if (OutVal.ToString() == Stag2.Text)
                        stagiu2 = Convert.ToDouble(number);
                    else
                        stagiu2 = Convert.ToDouble(number.Replace(".", ","));
                }
                else
                {
                    double OutVal;
                    double.TryParse(number, out OutVal);
                    if (OutVal.ToString() == Stag2.Text)
                        stagiu2 = Convert.ToDouble(number);
                    else
                        stagiu2 = Convert.ToDouble(number.Replace(',', '.'));
                }
            }

            double vechimea1;
            if (Vechime.Text == "")
            {
                vechimea1 = 0;
            }
            else
            {
                string number = Vechime.Text;
                if (number.Contains("."))
                {
                    double OutVal;
                    double.TryParse(number, out OutVal);
                    if (OutVal.ToString() == Vechime.Text)
                        vechimea1 = Convert.ToDouble(number);
                    else
                        vechimea1 = Convert.ToDouble(number.Replace(".", ","));
                }
                else
                {

                    double OutVal;
                    double.TryParse(number, out OutVal);
                    if (OutVal.ToString() == Vechime.Text)
                        vechimea1 = Convert.ToDouble(number);
                    else
                        vechimea1 = Convert.ToDouble(number.Replace(',', '.'));
                }
            }

            double vechimea2;
            if (Vechime2.Text == "")
            {
                vechimea2 = 0;
            }
            else
            {
                string number = Vechime2.Text;
                if (number.Contains("."))
                {
                    double OutVal;
                    double.TryParse(number, out OutVal);
                    if (OutVal.ToString() == Vechime2.Text)
                        vechimea2 = Convert.ToDouble(number);
                    else
                        vechimea2 = Convert.ToDouble(number.Replace(".", ","));
                }
                else
                {
                    double OutVal;
                    double.TryParse(number, out OutVal);
                    if (OutVal.ToString() == Vechime2.Text)
                        vechimea2 = Convert.ToDouble(number);
                    else
                        vechimea2 = Convert.ToDouble(number.Replace(',', '.'));
                }
            }

            var t = Form.Template.FindName("PART_TextBox", Form) as DatePickerTextBox;


            var t2 = Form2.Template.FindName("PART_TextBox", Form2) as DatePickerTextBox;

            var perslist = from p in persoana
                           join r in raion on p.idRaion equals r.IdRaion
                           join l in localitatea on r.idLocal equals l.IdLocal
                           join i in institutia on p.idInst equals i.IdInst
                           join n in nationalitatea on p.idNational equals n.IdNational
                           join s in studii on p.idStudii equals s.IdStudii
                           join g in grad on p.idgradstiint equals g.Idgrstiint
                           join c in catedra on p.IdCatedra equals c.IdCatedra
                           join gr in grDidactic on p.IdGradDidactic equals gr.IdGradDidactic
                           join d in prdis on p.IdPerson equals d.idPerson
                           join di in dis on d.idDiscip equals di.IdDiscip
                           select new

                           {
                               Nume = p.Nume,
                               Prenume = p.Prenume,
                               DataNasterii = p.DataNast.ToShortDateString(),
                               Genul = p.Gen,
                               Idnp = p.IDNP,
                               Email = p.Email,
                               Mobil = p.TelefonMob,
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
                               Disciplina = di.Denumirea,
                           };
            var perslistf = from p in persoana
                           join r in raion on p.idRaion equals r.IdRaion
                           join l in localitatea on r.idLocal equals l.IdLocal
                           join i in institutia on p.idInst equals i.IdInst
                           join n in nationalitatea on p.idNational equals n.IdNational
                           join s in studii on p.idStudii equals s.IdStudii
                           join g in grad on p.idgradstiint equals g.Idgrstiint
                           join c in catedra on p.IdCatedra equals c.IdCatedra
                           join gr in grDidactic on p.IdGradDidactic equals gr.IdGradDidactic
                           join f in formare on p.IdPerson equals f.IdPerson
                           select new

                           {
                               Nume = p.Nume,
                               Prenume = p.Prenume,
                               DataNasterii = p.DataNast.ToShortDateString(),
                               Genul = p.Gen,
                               Idnp = p.IDNP,
                               Email = p.Email,
                               Mobil = p.TelefonMob,
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
                               Formarea = f.Denumirea,
                               DataFormarii = f.Anul,
                           };
            var perslist2 = from p in persoana
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
            var perslistfun = from p in persoana
                              join r in raion on p.idRaion equals r.IdRaion
                              join l in localitatea on r.idLocal equals l.IdLocal
                              join i in institutia on p.idInst equals i.IdInst
                              join n in nationalitatea on p.idNational equals n.IdNational
                              join s in studii on p.idStudii equals s.IdStudii
                              join g in grad on p.idgradstiint equals g.Idgrstiint
                              join c in catedra on p.IdCatedra equals c.IdCatedra
                              join gr in grDidactic on p.IdGradDidactic equals gr.IdGradDidactic
                              join pf in persfun on p.IdPerson equals pf.IdPerson
                              join fn in functie on pf.IdFunctie equals fn.IdFunctie
                              select new

                              {
                                  Nume = p.Nume,
                                  Prenume = p.Prenume,
                                  DataNasterii = p.DataNast.ToShortDateString(),
                                  Genul = p.Gen,
                                  Idnp = p.IDNP,
                                  Email = p.Email,
                                  Mobil = p.TelefonMob,
                                  Catedra = c.DenumireCatedra,
                                  Functia = fn.DenFunctie,
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
                                  Unitati = pf.Unitati,
                                  ClasaSalarizare = pf.ClasaSalar

                              };
            var persdisc = from p in persoana
                           join r in raion on p.idRaion equals r.IdRaion
                           join l in localitatea on r.idLocal equals l.IdLocal
                           join i in institutia on p.idInst equals i.IdInst
                           join n in nationalitatea on p.idNational equals n.IdNational
                           join s in studii on p.idStudii equals s.IdStudii
                           join g in grad on p.idgradstiint equals g.Idgrstiint
                           join c in catedra on p.IdCatedra equals c.IdCatedra
                           join gr in grDidactic on p.IdGradDidactic equals gr.IdGradDidactic
                           join d in prdis on p.IdPerson equals d.idPerson
                           join di in dis on d.idDiscip equals di.IdDiscip
                           select new

                           {
                               Nume = p.Nume,
                               Prenume = p.Prenume,
                               DataNasterii = p.DataNast.ToShortDateString(),
                               Genul = p.Gen,
                               Idnp = p.IDNP,
                               Email = p.Email,
                               Mobil = p.TelefonMob,
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
                               Disciplina = di.Denumirea,

                           };
            if (DisciplinaText.Text != ""  && t.Text == "Select date" && t2.Text == "Select date")
            {
                persdisc = from p in persdisc

                           where p.Disciplina.Contains(DisciplinaText.Text)
                           select new
                           {
                               Nume = p.Nume,
                               Prenume = p.Prenume,
                               DataNasterii = p.DataNasterii,
                               Genul = p.Genul,
                               Idnp = p.Idnp,
                               Email = p.Email,
                               Mobil = p.Mobil,
                               Catedra = p.Catedra,
                               GradDidactic = p.GradDidactic,
                               DataPrimiriiGradului = p.DataPrimiriiGradului,
                               ModulAngajarii = p.ModulAngajarii,
                               Nationalitatea = p.Nationalitatea,
                               Localitatea = p.Localitatea,
                               VarstaPensionara = p.VarstaPensionara,
                               Institutia = p.Institutia,
                               Studii = p.Studii,
                               StudiiManageriale = p.StudiiManageriale,
                               GradulManagerial = p.GradulManagerial,
                               StudiiPedagogice = p.StudiiPedagogice,
                               StagiulPedagogic = p.StagiulPedagogic,
                               DataAngajaeii = p.DataAngajaeii,
                               DataEliberarii = p.DataEliberarii,
                               VechimeInMunca = p.VechimeInMunca,
                               CazatCamin = p.CazatCamin,
                               Camin = p.Camin,
                               Statutul = p.Statutul,
                               GradulStiintific = p.GradulStiintific,
                               ConcediuTermenLung = p.ConcediuTermenLung,
                               Disciplina = p.Disciplina,

                           };

                string didact1 = "";
                string didact2 = "";
                string didact3 = "";
                string didact4 = "";


                if (did1.IsChecked == true)
                {
                    didact1 = "Nu are";
                }
                if (did2.IsChecked == true)
                {
                    didact2 = "Gradul 2";
                }
                if (did3.IsChecked == true)
                {
                    didact3 = "Gradul 1";
                }
                if (did4.IsChecked == true)
                {
                    didact4 = "Gradul Superior";
                }
                if (didact1 != "" || didact2 != "" || didact3 != "" || didact4 != "")
                {
                    persdisc = from p in persdisc

                               where p.GradDidactic == didact1 || p.GradDidactic == didact2 || p.GradDidactic == didact3 || p.GradDidactic == didact4

                               select new
                               {
                                   Nume = p.Nume,
                                   Prenume = p.Prenume,
                                   DataNasterii = p.DataNasterii,
                                   Genul = p.Genul,
                                   Idnp = p.Idnp,
                                   Email = p.Email,
                                   Mobil = p.Mobil,
                                   Catedra = p.Catedra,
                                   GradDidactic = p.GradDidactic,
                                   DataPrimiriiGradului = p.DataPrimiriiGradului,
                                   ModulAngajarii = p.ModulAngajarii,
                                   Nationalitatea = p.Nationalitatea,
                                   Localitatea = p.Localitatea,
                                   VarstaPensionara = p.VarstaPensionara,
                                   Institutia = p.Institutia,
                                   Studii = p.Studii,
                                   StudiiManageriale = p.StudiiManageriale,
                                   GradulManagerial = p.GradulManagerial,
                                   StudiiPedagogice = p.StudiiPedagogice,
                                   StagiulPedagogic = p.StagiulPedagogic,
                                   DataAngajaeii = p.DataAngajaeii,
                                   DataEliberarii = p.DataEliberarii,
                                   VechimeInMunca = p.VechimeInMunca,
                                   CazatCamin = p.CazatCamin,
                                   Camin = p.Camin,
                                   Statutul = p.Statutul,
                                   GradulStiintific = p.GradulStiintific,
                                   ConcediuTermenLung = p.ConcediuTermenLung,
                                   Disciplina = p.Disciplina,

                               };


                }
                if (titlustcom.Text != "")
                {
                    persdisc = from p in persdisc

                               where p.GradulStiintific.Contains(titlustcom.Text)
                               select new
                               {
                                   Nume = p.Nume,
                                   Prenume = p.Prenume,
                                   DataNasterii = p.DataNasterii,
                                   Genul = p.Genul,
                                   Idnp = p.Idnp,
                                   Email = p.Email,
                                   Mobil = p.Mobil,
                                   Catedra = p.Catedra,
                                   GradDidactic = p.GradDidactic,
                                   DataPrimiriiGradului = p.DataPrimiriiGradului,
                                   ModulAngajarii = p.ModulAngajarii,
                                   Nationalitatea = p.Nationalitatea,
                                   Localitatea = p.Localitatea,
                                   VarstaPensionara = p.VarstaPensionara,
                                   Institutia = p.Institutia,
                                   Studii = p.Studii,
                                   StudiiManageriale = p.StudiiManageriale,
                                   GradulManagerial = p.GradulManagerial,
                                   StudiiPedagogice = p.StudiiPedagogice,
                                   StagiulPedagogic = p.StagiulPedagogic,
                                   DataAngajaeii = p.DataAngajaeii,
                                   DataEliberarii = p.DataEliberarii,
                                   VechimeInMunca = p.VechimeInMunca,
                                   CazatCamin = p.CazatCamin,
                                   Camin = p.Camin,
                                   Statutul = p.Statutul,
                                   GradulStiintific = p.GradulStiintific,
                                   ConcediuTermenLung = p.ConcediuTermenLung,
                                   Disciplina = p.Disciplina,

                               };

                }
                if (Vechime.Text != "" && Vechime2.Text != "" && regStag.IsMatch(Vechime.Text) && regStag.IsMatch(Vechime2.Text))
                {
                    persdisc = from p in persdisc

                               where p.VechimeInMunca >= vechimea1 && p.VechimeInMunca <= vechimea2
                               select new
                               {
                                   Nume = p.Nume,
                                   Prenume = p.Prenume,
                                   DataNasterii = p.DataNasterii,
                                   Genul = p.Genul,
                                   Idnp = p.Idnp,
                                   Email = p.Email,
                                   Mobil = p.Mobil,
                                   Catedra = p.Catedra,
                                   GradDidactic = p.GradDidactic,
                                   DataPrimiriiGradului = p.DataPrimiriiGradului,
                                   ModulAngajarii = p.ModulAngajarii,
                                   Nationalitatea = p.Nationalitatea,
                                   Localitatea = p.Localitatea,
                                   VarstaPensionara = p.VarstaPensionara,
                                   Institutia = p.Institutia,
                                   Studii = p.Studii,
                                   StudiiManageriale = p.StudiiManageriale,
                                   GradulManagerial = p.GradulManagerial,
                                   StudiiPedagogice = p.StudiiPedagogice,
                                   StagiulPedagogic = p.StagiulPedagogic,
                                   DataAngajaeii = p.DataAngajaeii,
                                   DataEliberarii = p.DataEliberarii,
                                   VechimeInMunca = p.VechimeInMunca,
                                   CazatCamin = p.CazatCamin,
                                   Camin = p.Camin,
                                   Statutul = p.Statutul,
                                   GradulStiintific = p.GradulStiintific,
                                   ConcediuTermenLung = p.ConcediuTermenLung,
                                   Disciplina = p.Disciplina,

                               };

                }
                if (Stag.Text != "" && Stag2.Text != "" && regStag.IsMatch(Stag.Text) && regStag.IsMatch(Stag2.Text))
                {
                    persdisc = from p in persdisc

                               where p.StagiulPedagogic >= stagiu1 && p.StagiulPedagogic <= stagiu2
                               select new
                               {
                                   Nume = p.Nume,
                                   Prenume = p.Prenume,
                                   DataNasterii = p.DataNasterii,
                                   Genul = p.Genul,
                                   Idnp = p.Idnp,
                                   Email = p.Email,
                                   Mobil = p.Mobil,
                                   Catedra = p.Catedra,
                                   GradDidactic = p.GradDidactic,
                                   DataPrimiriiGradului = p.DataPrimiriiGradului,
                                   ModulAngajarii = p.ModulAngajarii,
                                   Nationalitatea = p.Nationalitatea,
                                   Localitatea = p.Localitatea,
                                   VarstaPensionara = p.VarstaPensionara,
                                   Institutia = p.Institutia,
                                   Studii = p.Studii,
                                   StudiiManageriale = p.StudiiManageriale,
                                   GradulManagerial = p.GradulManagerial,
                                   StudiiPedagogice = p.StudiiPedagogice,
                                   StagiulPedagogic = p.StagiulPedagogic,
                                   DataAngajaeii = p.DataAngajaeii,
                                   DataEliberarii = p.DataEliberarii,
                                   VechimeInMunca = p.VechimeInMunca,
                                   CazatCamin = p.CazatCamin,
                                   Camin = p.Camin,
                                   Statutul = p.Statutul,
                                   GradulStiintific = p.GradulStiintific,
                                   ConcediuTermenLung = p.ConcediuTermenLung,
                                   Disciplina = p.Disciplina,

                               };

                }
                bool gn = true;
                if (masc.IsChecked == true)
                {
                    gn = true;
                }
                else if (fem.IsChecked == true)
                {
                    gn = false;
                }

                if (masc.IsChecked == true || fem.IsChecked == true)
                {
                    persdisc = from p in persdisc

                               where p.Genul == gn
                               select new
                               {
                                   Nume = p.Nume,
                                   Prenume = p.Prenume,
                                   DataNasterii = p.DataNasterii,
                                   Genul = p.Genul,
                                   Idnp = p.Idnp,
                                   Email = p.Email,
                                   Mobil = p.Mobil,
                                   Catedra = p.Catedra,
                                   GradDidactic = p.GradDidactic,
                                   DataPrimiriiGradului = p.DataPrimiriiGradului,
                                   ModulAngajarii = p.ModulAngajarii,
                                   Nationalitatea = p.Nationalitatea,
                                   Localitatea = p.Localitatea,
                                   VarstaPensionara = p.VarstaPensionara,
                                   Institutia = p.Institutia,
                                   Studii = p.Studii,
                                   StudiiManageriale = p.StudiiManageriale,
                                   GradulManagerial = p.GradulManagerial,
                                   StudiiPedagogice = p.StudiiPedagogice,
                                   StagiulPedagogic = p.StagiulPedagogic,
                                   DataAngajaeii = p.DataAngajaeii,
                                   DataEliberarii = p.DataEliberarii,
                                   VechimeInMunca = p.VechimeInMunca,
                                   CazatCamin = p.CazatCamin,
                                   Camin = p.Camin,
                                   Statutul = p.Statutul,
                                   GradulStiintific = p.GradulStiintific,
                                   ConcediuTermenLung = p.ConcediuTermenLung,
                                   Disciplina = p.Disciplina,

                               };

                }
                if (cat.Text != "")
                {
                    persdisc = from p in persdisc

                               where p.Catedra == cat.Text
                               select new
                               {
                                   Nume = p.Nume,
                                   Prenume = p.Prenume,
                                   DataNasterii = p.DataNasterii,
                                   Genul = p.Genul,
                                   Idnp = p.Idnp,
                                   Email = p.Email,
                                   Mobil = p.Mobil,
                                   Catedra = p.Catedra,
                                   GradDidactic = p.GradDidactic,
                                   DataPrimiriiGradului = p.DataPrimiriiGradului,
                                   ModulAngajarii = p.ModulAngajarii,
                                   Nationalitatea = p.Nationalitatea,
                                   Localitatea = p.Localitatea,
                                   VarstaPensionara = p.VarstaPensionara,
                                   Institutia = p.Institutia,
                                   Studii = p.Studii,
                                   StudiiManageriale = p.StudiiManageriale,
                                   GradulManagerial = p.GradulManagerial,
                                   StudiiPedagogice = p.StudiiPedagogice,
                                   StagiulPedagogic = p.StagiulPedagogic,
                                   DataAngajaeii = p.DataAngajaeii,
                                   DataEliberarii = p.DataEliberarii,
                                   VechimeInMunca = p.VechimeInMunca,
                                   CazatCamin = p.CazatCamin,
                                   Camin = p.Camin,
                                   Statutul = p.Statutul,
                                   GradulStiintific = p.GradulStiintific,
                                   ConcediuTermenLung = p.ConcediuTermenLung,
                                   Disciplina = p.Disciplina,

                               };

                }
                if (telef.Text != "")
                {
                    persdisc = from p in persdisc

                               where p.Mobil.Contains(telef.Text)
                               select new
                               {
                                   Nume = p.Nume,
                                   Prenume = p.Prenume,
                                   DataNasterii = p.DataNasterii,
                                   Genul = p.Genul,
                                   Idnp = p.Idnp,
                                   Email = p.Email,
                                   Mobil = p.Mobil,
                                   Catedra = p.Catedra,
                                   GradDidactic = p.GradDidactic,
                                   DataPrimiriiGradului = p.DataPrimiriiGradului,
                                   ModulAngajarii = p.ModulAngajarii,
                                   Nationalitatea = p.Nationalitatea,
                                   Localitatea = p.Localitatea,
                                   VarstaPensionara = p.VarstaPensionara,
                                   Institutia = p.Institutia,
                                   Studii = p.Studii,
                                   StudiiManageriale = p.StudiiManageriale,
                                   GradulManagerial = p.GradulManagerial,
                                   StudiiPedagogice = p.StudiiPedagogice,
                                   StagiulPedagogic = p.StagiulPedagogic,
                                   DataAngajaeii = p.DataAngajaeii,
                                   DataEliberarii = p.DataEliberarii,
                                   VechimeInMunca = p.VechimeInMunca,
                                   CazatCamin = p.CazatCamin,
                                   Camin = p.Camin,
                                   Statutul = p.Statutul,
                                   GradulStiintific = p.GradulStiintific,
                                   ConcediuTermenLung = p.ConcediuTermenLung,
                                   Disciplina = p.Disciplina,

                               };

                }
                if (posta.Text != "")
                {
                    persdisc = from p in persdisc

                               where p.Email.Contains(posta.Text)
                               select new
                               {
                                   Nume = p.Nume,
                                   Prenume = p.Prenume,
                                   DataNasterii = p.DataNasterii,
                                   Genul = p.Genul,
                                   Idnp = p.Idnp,
                                   Email = p.Email,
                                   Mobil = p.Mobil,
                                   Catedra = p.Catedra,
                                   GradDidactic = p.GradDidactic,
                                   DataPrimiriiGradului = p.DataPrimiriiGradului,
                                   ModulAngajarii = p.ModulAngajarii,
                                   Nationalitatea = p.Nationalitatea,
                                   Localitatea = p.Localitatea,
                                   VarstaPensionara = p.VarstaPensionara,
                                   Institutia = p.Institutia,
                                   Studii = p.Studii,
                                   StudiiManageriale = p.StudiiManageriale,
                                   GradulManagerial = p.GradulManagerial,
                                   StudiiPedagogice = p.StudiiPedagogice,
                                   StagiulPedagogic = p.StagiulPedagogic,
                                   DataAngajaeii = p.DataAngajaeii,
                                   DataEliberarii = p.DataEliberarii,
                                   VechimeInMunca = p.VechimeInMunca,
                                   CazatCamin = p.CazatCamin,
                                   Camin = p.Camin,
                                   Statutul = p.Statutul,
                                   GradulStiintific = p.GradulStiintific,
                                   ConcediuTermenLung = p.ConcediuTermenLung,
                                   Disciplina = p.Disciplina,

                               };

                }
                var perslistdisc = from p in persdisc
                                   select new
                                   {
                                       Nume = p.Nume,
                                       Prenume = p.Prenume,
                                       DataNasterii = p.DataNasterii,
                                       Genul = p.Genul,
                                       Idnp = p.Idnp,
                                       Email = p.Email,
                                       Mobil = p.Mobil,
                                       Catedra = p.Catedra,
                                       GradDidactic = p.GradDidactic,
                                       DataPrimiriiGradului = p.DataPrimiriiGradului,
                                       ModulAngajarii = p.ModulAngajarii,
                                       Nationalitatea = p.Nationalitatea,
                                       Localitatea = p.Localitatea,
                                       VarstaPensionara = p.VarstaPensionara,
                                       Institutia = p.Institutia,
                                       Studii = p.Studii,
                                       StudiiManageriale = p.StudiiManageriale,
                                       GradulManagerial = p.GradulManagerial,
                                       StudiiPedagogice = p.StudiiPedagogice,
                                       StagiulPedagogic = p.StagiulPedagogic,
                                       DataAngajaeii = p.DataAngajaeii,
                                       DataEliberarii = p.DataEliberarii,
                                       VechimeInMunca = p.VechimeInMunca,
                                       CazatCamin = p.CazatCamin,
                                       Camin = p.Camin,
                                       Statutul = p.Statutul,
                                       GradulStiintific = p.GradulStiintific,
                                       ConcediuTermenLung = p.ConcediuTermenLung,
                                       Disciplina = p.Disciplina,


                                   };
                PersAfisGrid.ItemsSource = perslistdisc.ToList();
                NrPers.Content = perslistdisc.Count();
                DisciplinaText.Text = "";
                did1.IsChecked = false;
                did2.IsChecked = false;
                did3.IsChecked = false;
                did4.IsChecked = false;
                titlustcom.Text = "";
                Vechime.Text = "";
                Vechime2.Text = "";
                Stag.Text = "";
                Stag2.Text = "";
                masc.IsChecked = false;
                fem.IsChecked = false;
                cat.Text = "";
                fun.Text = "";
                telef.Text = "";
                posta.Text = "";

                t = Form.Template.FindName("PART_TextBox", Form) as DatePickerTextBox;
                if (t != null)
                    t.Text = "Select date";

                t2 = Form2.Template.FindName("PART_TextBox", Form2) as DatePickerTextBox;
                if (t2 != null)
                    t2.Text = "Select date";

            }

            else if (fun.Text != "" && DisciplinaText.Text == "" && t.Text == "Select date" && t2.Text == "Select date")
            {
                if (fun.Text != "")
                {
                    perslistfun = from p in perslistfun
                                  where p.Functia == fun.Text
                                  select new
                                  {
                                      Nume = p.Nume,
                                      Prenume = p.Prenume,
                                      DataNasterii = p.DataNasterii,
                                      Genul = p.Genul,
                                      Idnp = p.Idnp,
                                      Email = p.Email,
                                      Mobil = p.Mobil,
                                      Catedra = p.Catedra,
                                      Functia = p.Functia,
                                      GradDidactic = p.GradDidactic,
                                      DataPrimiriiGradului = p.DataPrimiriiGradului,
                                      ModulAngajarii = p.ModulAngajarii,
                                      Nationalitatea = p.Nationalitatea,
                                      Localitatea = p.Localitatea,
                                      VarstaPensionara = p.VarstaPensionara,
                                      Institutia = p.Institutia,
                                      Studii = p.Studii,
                                      StudiiManageriale = p.StudiiManageriale,
                                      GradulManagerial = p.GradulManagerial,
                                      StudiiPedagogice = p.StudiiPedagogice,
                                      StagiulPedagogic = p.StagiulPedagogic,
                                      DataAngajaeii = p.DataAngajaeii,
                                      DataEliberarii = p.DataEliberarii,
                                      VechimeInMunca = p.VechimeInMunca,
                                      CazatCamin = p.CazatCamin,
                                      Camin = p.Camin,
                                      Statutul = p.Statutul,
                                      GradulStiintific = p.GradulStiintific,
                                      ConcediuTermenLung = p.ConcediuTermenLung,
                                      Unitati = p.Unitati,
                                      ClasaSalarizare = p.ClasaSalarizare

                                  };
                }
                string didact1 = "";
                string didact2 = "";
                string didact3 = "";
                string didact4 = "";


                if (did1.IsChecked == true)
                {
                    didact1 = "Nu are";
                }
                if (did2.IsChecked == true)
                {
                    didact2 = "Gradul 2";
                }
                if (did3.IsChecked == true)
                {
                    didact3 = "Gradul 1";
                }
                if (did4.IsChecked == true)
                {
                    didact4 = "Gradul Superior";
                }
                if (didact1 != "" || didact2 != "" || didact3 != "" || didact4 != "")
                {
                    perslistfun = from p in perslistfun

                                  where p.GradDidactic == didact1 || p.GradDidactic == didact2 || p.GradDidactic == didact3 || p.GradDidactic == didact4

                                  select new
                                  {
                                      Nume = p.Nume,
                                      Prenume = p.Prenume,
                                      DataNasterii = p.DataNasterii,
                                      Genul = p.Genul,
                                      Idnp = p.Idnp,
                                      Email = p.Email,
                                      Mobil = p.Mobil,
                                      Catedra = p.Catedra,
                                      Functia = p.Functia,
                                      GradDidactic = p.GradDidactic,
                                      DataPrimiriiGradului = p.DataPrimiriiGradului,
                                      ModulAngajarii = p.ModulAngajarii,
                                      Nationalitatea = p.Nationalitatea,
                                      Localitatea = p.Localitatea,
                                      VarstaPensionara = p.VarstaPensionara,
                                      Institutia = p.Institutia,
                                      Studii = p.Studii,
                                      StudiiManageriale = p.StudiiManageriale,
                                      GradulManagerial = p.GradulManagerial,
                                      StudiiPedagogice = p.StudiiPedagogice,
                                      StagiulPedagogic = p.StagiulPedagogic,
                                      DataAngajaeii = p.DataAngajaeii,
                                      DataEliberarii = p.DataEliberarii,
                                      VechimeInMunca = p.VechimeInMunca,
                                      CazatCamin = p.CazatCamin,
                                      Camin = p.Camin,
                                      Statutul = p.Statutul,
                                      GradulStiintific = p.GradulStiintific,
                                      ConcediuTermenLung = p.ConcediuTermenLung,
                                      Unitati = p.Unitati,
                                      ClasaSalarizare = p.ClasaSalarizare
                                  };


                }
                if (titlustcom.Text != "")
                {
                    perslistfun = from p in perslistfun

                                  where p.GradulStiintific.Contains(titlustcom.Text)
                                  select new
                                  {
                                      Nume = p.Nume,
                                      Prenume = p.Prenume,
                                      DataNasterii = p.DataNasterii,
                                      Genul = p.Genul,
                                      Idnp = p.Idnp,
                                      Email = p.Email,
                                      Mobil = p.Mobil,
                                      Catedra = p.Catedra,
                                      Functia = p.Functia,
                                      GradDidactic = p.GradDidactic,
                                      DataPrimiriiGradului = p.DataPrimiriiGradului,
                                      ModulAngajarii = p.ModulAngajarii,
                                      Nationalitatea = p.Nationalitatea,
                                      Localitatea = p.Localitatea,
                                      VarstaPensionara = p.VarstaPensionara,
                                      Institutia = p.Institutia,
                                      Studii = p.Studii,
                                      StudiiManageriale = p.StudiiManageriale,
                                      GradulManagerial = p.GradulManagerial,
                                      StudiiPedagogice = p.StudiiPedagogice,
                                      StagiulPedagogic = p.StagiulPedagogic,
                                      DataAngajaeii = p.DataAngajaeii,
                                      DataEliberarii = p.DataEliberarii,
                                      VechimeInMunca = p.VechimeInMunca,
                                      CazatCamin = p.CazatCamin,
                                      Camin = p.Camin,
                                      Statutul = p.Statutul,
                                      GradulStiintific = p.GradulStiintific,
                                      ConcediuTermenLung = p.ConcediuTermenLung,
                                      Unitati = p.Unitati,
                                      ClasaSalarizare = p.ClasaSalarizare
                                  };

                }
                if (Vechime.Text != "" && Vechime2.Text != "" && regStag.IsMatch(Vechime.Text) && regStag.IsMatch(Vechime2.Text))
                {
                    perslistfun = from p in perslistfun
                                  where p.VechimeInMunca >= vechimea1 && p.VechimeInMunca <= vechimea2
                                  select new
                                  {
                                      Nume = p.Nume,
                                      Prenume = p.Prenume,
                                      DataNasterii = p.DataNasterii,
                                      Genul = p.Genul,
                                      Idnp = p.Idnp,
                                      Email = p.Email,
                                      Mobil = p.Mobil,
                                      Catedra = p.Catedra,
                                      Functia = p.Functia,
                                      GradDidactic = p.GradDidactic,
                                      DataPrimiriiGradului = p.DataPrimiriiGradului,
                                      ModulAngajarii = p.ModulAngajarii,
                                      Nationalitatea = p.Nationalitatea,
                                      Localitatea = p.Localitatea,
                                      VarstaPensionara = p.VarstaPensionara,
                                      Institutia = p.Institutia,
                                      Studii = p.Studii,
                                      StudiiManageriale = p.StudiiManageriale,
                                      GradulManagerial = p.GradulManagerial,
                                      StudiiPedagogice = p.StudiiPedagogice,
                                      StagiulPedagogic = p.StagiulPedagogic,
                                      DataAngajaeii = p.DataAngajaeii,
                                      DataEliberarii = p.DataEliberarii,
                                      VechimeInMunca = p.VechimeInMunca,
                                      CazatCamin = p.CazatCamin,
                                      Camin = p.Camin,
                                      Statutul = p.Statutul,
                                      GradulStiintific = p.GradulStiintific,
                                      ConcediuTermenLung = p.ConcediuTermenLung,
                                      Unitati = p.Unitati,
                                      ClasaSalarizare = p.ClasaSalarizare
                                  };

                }
                if (Stag.Text != "" && Stag2.Text != "" && regStag.IsMatch(Stag.Text) && regStag.IsMatch(Stag2.Text))
                {
                    perslistfun = from p in perslistfun

                                  where p.StagiulPedagogic >= stagiu1 && p.StagiulPedagogic <= stagiu2
                                  select new
                                  {
                                      Nume = p.Nume,
                                      Prenume = p.Prenume,
                                      DataNasterii = p.DataNasterii,
                                      Genul = p.Genul,
                                      Idnp = p.Idnp,
                                      Email = p.Email,
                                      Mobil = p.Mobil,
                                      Catedra = p.Catedra,
                                      Functia = p.Functia,
                                      GradDidactic = p.GradDidactic,
                                      DataPrimiriiGradului = p.DataPrimiriiGradului,
                                      ModulAngajarii = p.ModulAngajarii,
                                      Nationalitatea = p.Nationalitatea,
                                      Localitatea = p.Localitatea,
                                      VarstaPensionara = p.VarstaPensionara,
                                      Institutia = p.Institutia,
                                      Studii = p.Studii,
                                      StudiiManageriale = p.StudiiManageriale,
                                      GradulManagerial = p.GradulManagerial,
                                      StudiiPedagogice = p.StudiiPedagogice,
                                      StagiulPedagogic = p.StagiulPedagogic,
                                      DataAngajaeii = p.DataAngajaeii,
                                      DataEliberarii = p.DataEliberarii,
                                      VechimeInMunca = p.VechimeInMunca,
                                      CazatCamin = p.CazatCamin,
                                      Camin = p.Camin,
                                      Statutul = p.Statutul,
                                      GradulStiintific = p.GradulStiintific,
                                      ConcediuTermenLung = p.ConcediuTermenLung,
                                      Unitati = p.Unitati,
                                      ClasaSalarizare = p.ClasaSalarizare
                                  };

                }
                bool gn = true;
                if (masc.IsChecked == true)
                {
                    gn = true;
                }
                else if (fem.IsChecked == true)
                {
                    gn = false;
                }

                if (masc.IsChecked == true || fem.IsChecked == true)
                {
                    perslistfun = from p in perslistfun

                                  where p.Genul == gn
                                  select new
                                  {
                                      Nume = p.Nume,
                                      Prenume = p.Prenume,
                                      DataNasterii = p.DataNasterii,
                                      Genul = p.Genul,
                                      Idnp = p.Idnp,
                                      Email = p.Email,
                                      Mobil = p.Mobil,
                                      Catedra = p.Catedra,
                                      Functia = p.Functia,
                                      GradDidactic = p.GradDidactic,
                                      DataPrimiriiGradului = p.DataPrimiriiGradului,
                                      ModulAngajarii = p.ModulAngajarii,
                                      Nationalitatea = p.Nationalitatea,
                                      Localitatea = p.Localitatea,
                                      VarstaPensionara = p.VarstaPensionara,
                                      Institutia = p.Institutia,
                                      Studii = p.Studii,
                                      StudiiManageriale = p.StudiiManageriale,
                                      GradulManagerial = p.GradulManagerial,
                                      StudiiPedagogice = p.StudiiPedagogice,
                                      StagiulPedagogic = p.StagiulPedagogic,
                                      DataAngajaeii = p.DataAngajaeii,
                                      DataEliberarii = p.DataEliberarii,
                                      VechimeInMunca = p.VechimeInMunca,
                                      CazatCamin = p.CazatCamin,
                                      Camin = p.Camin,
                                      Statutul = p.Statutul,
                                      GradulStiintific = p.GradulStiintific,
                                      ConcediuTermenLung = p.ConcediuTermenLung,
                                      Unitati = p.Unitati,
                                      ClasaSalarizare = p.ClasaSalarizare
                                  };

                }
                if (cat.Text != "")
                {
                    perslistfun = from p in perslistfun

                                  where p.Catedra == cat.Text
                                  select new
                                  {
                                      Nume = p.Nume,
                                      Prenume = p.Prenume,
                                      DataNasterii = p.DataNasterii,
                                      Genul = p.Genul,
                                      Idnp = p.Idnp,
                                      Email = p.Email,
                                      Mobil = p.Mobil,
                                      Catedra = p.Catedra,
                                      Functia = p.Functia,
                                      GradDidactic = p.GradDidactic,
                                      DataPrimiriiGradului = p.DataPrimiriiGradului,
                                      ModulAngajarii = p.ModulAngajarii,
                                      Nationalitatea = p.Nationalitatea,
                                      Localitatea = p.Localitatea,
                                      VarstaPensionara = p.VarstaPensionara,
                                      Institutia = p.Institutia,
                                      Studii = p.Studii,
                                      StudiiManageriale = p.StudiiManageriale,
                                      GradulManagerial = p.GradulManagerial,
                                      StudiiPedagogice = p.StudiiPedagogice,
                                      StagiulPedagogic = p.StagiulPedagogic,
                                      DataAngajaeii = p.DataAngajaeii,
                                      DataEliberarii = p.DataEliberarii,
                                      VechimeInMunca = p.VechimeInMunca,
                                      CazatCamin = p.CazatCamin,
                                      Camin = p.Camin,
                                      Statutul = p.Statutul,
                                      GradulStiintific = p.GradulStiintific,
                                      ConcediuTermenLung = p.ConcediuTermenLung,
                                      Unitati = p.Unitati,
                                      ClasaSalarizare = p.ClasaSalarizare
                                  };

                }


                if (telef.Text != "")
                {
                    perslistfun = from p in perslistfun

                                  where p.Mobil.Contains(telef.Text)
                                  select new
                                  {
                                      Nume = p.Nume,
                                      Prenume = p.Prenume,
                                      DataNasterii = p.DataNasterii,
                                      Genul = p.Genul,
                                      Idnp = p.Idnp,
                                      Email = p.Email,
                                      Mobil = p.Mobil,
                                      Catedra = p.Catedra,
                                      Functia = p.Functia,
                                      GradDidactic = p.GradDidactic,
                                      DataPrimiriiGradului = p.DataPrimiriiGradului,
                                      ModulAngajarii = p.ModulAngajarii,
                                      Nationalitatea = p.Nationalitatea,
                                      Localitatea = p.Localitatea,
                                      VarstaPensionara = p.VarstaPensionara,
                                      Institutia = p.Institutia,
                                      Studii = p.Studii,
                                      StudiiManageriale = p.StudiiManageriale,
                                      GradulManagerial = p.GradulManagerial,
                                      StudiiPedagogice = p.StudiiPedagogice,
                                      StagiulPedagogic = p.StagiulPedagogic,
                                      DataAngajaeii = p.DataAngajaeii,
                                      DataEliberarii = p.DataEliberarii,
                                      VechimeInMunca = p.VechimeInMunca,
                                      CazatCamin = p.CazatCamin,
                                      Camin = p.Camin,
                                      Statutul = p.Statutul,
                                      GradulStiintific = p.GradulStiintific,
                                      ConcediuTermenLung = p.ConcediuTermenLung,
                                      Unitati = p.Unitati,
                                      ClasaSalarizare = p.ClasaSalarizare
                                  };

                }
                if (posta.Text != "")
                {
                    perslistfun = from p in perslistfun

                                  where p.Email.Contains(posta.Text)
                                  select new
                                  {
                                      Nume = p.Nume,
                                      Prenume = p.Prenume,
                                      DataNasterii = p.DataNasterii,
                                      Genul = p.Genul,
                                      Idnp = p.Idnp,
                                      Email = p.Email,
                                      Mobil = p.Mobil,
                                      Catedra = p.Catedra,
                                      Functia = p.Functia,
                                      GradDidactic = p.GradDidactic,
                                      DataPrimiriiGradului = p.DataPrimiriiGradului,
                                      ModulAngajarii = p.ModulAngajarii,
                                      Nationalitatea = p.Nationalitatea,
                                      Localitatea = p.Localitatea,
                                      VarstaPensionara = p.VarstaPensionara,
                                      Institutia = p.Institutia,
                                      Studii = p.Studii,
                                      StudiiManageriale = p.StudiiManageriale,
                                      GradulManagerial = p.GradulManagerial,
                                      StudiiPedagogice = p.StudiiPedagogice,
                                      StagiulPedagogic = p.StagiulPedagogic,
                                      DataAngajaeii = p.DataAngajaeii,
                                      DataEliberarii = p.DataEliberarii,
                                      VechimeInMunca = p.VechimeInMunca,
                                      CazatCamin = p.CazatCamin,
                                      Camin = p.Camin,
                                      Statutul = p.Statutul,
                                      GradulStiintific = p.GradulStiintific,
                                      ConcediuTermenLung = p.ConcediuTermenLung,
                                      Unitati = p.Unitati,
                                      ClasaSalarizare = p.ClasaSalarizare
                                  };

                }
                var perslistfuntot = from p in perslistfun
                                     select new
                                     {
                                         Nume = p.Nume,
                                         Prenume = p.Prenume,
                                         DataNasterii = p.DataNasterii,
                                         Genul = p.Genul,
                                         Idnp = p.Idnp,
                                         Email = p.Email,
                                         Mobil = p.Mobil,
                                         Catedra = p.Catedra,
                                         Functia = p.Functia,
                                         GradDidactic = p.GradDidactic,
                                         DataPrimiriiGradului = p.DataPrimiriiGradului,
                                         ModulAngajarii = p.ModulAngajarii,
                                         Nationalitatea = p.Nationalitatea,
                                         Localitatea = p.Localitatea,
                                         VarstaPensionara = p.VarstaPensionara,
                                         Institutia = p.Institutia,
                                         Studii = p.Studii,
                                         StudiiManageriale = p.StudiiManageriale,
                                         GradulManagerial = p.GradulManagerial,
                                         StudiiPedagogice = p.StudiiPedagogice,
                                         StagiulPedagogic = p.StagiulPedagogic,
                                         DataAngajaeii = p.DataAngajaeii,
                                         DataEliberarii = p.DataEliberarii,
                                         VechimeInMunca = p.VechimeInMunca,
                                         CazatCamin = p.CazatCamin,
                                         Camin = p.Camin,
                                         Statutul = p.Statutul,
                                         GradulStiintific = p.GradulStiintific,
                                         ConcediuTermenLung = p.ConcediuTermenLung,
                                         Unitati = p.Unitati,
                                         ClasaSalarizare = p.ClasaSalarizare

                                     };

                PersAfisGrid.ItemsSource = perslistfuntot.ToList();
                NrPers.Content = perslistfuntot.Count();
                DisciplinaText.Text = "";
                did1.IsChecked = false;
                did2.IsChecked = false;
                did3.IsChecked = false;
                did4.IsChecked = false;
                titlustcom.Text = "";
                Vechime.Text = "";
                Vechime2.Text = "";
                Stag.Text = "";
                Stag2.Text = "";
                masc.IsChecked = false;
                fem.IsChecked = false;
                cat.Text = "";
                fun.Text = "";
                telef.Text = "";
                posta.Text = "";
                fun.Text = "";
                t = Form.Template.FindName("PART_TextBox", Form) as DatePickerTextBox;
                if (t != null)
                    t.Text = "Select date";

                t2 = Form2.Template.FindName("PART_TextBox", Form2) as DatePickerTextBox;
                if (t2 != null)
                    t2.Text = "Select date";

            }


            else if (DisciplinaText.Text == "" && t.Text != "Select date" && t2.Text != "Select date")
            {
                perslistf = from p in perslistf
                           select new
                           {
                               Nume = p.Nume,
                               Prenume = p.Prenume,
                               DataNasterii = p.DataNasterii,
                               Genul = p.Genul,
                               Idnp = p.Idnp,
                               Email = p.Email,
                               Mobil = p.Mobil,
                               Catedra = p.Catedra,
                               GradDidactic = p.GradDidactic,
                               DataPrimiriiGradului = p.DataPrimiriiGradului,
                               ModulAngajarii = p.ModulAngajarii,
                               Nationalitatea = p.Nationalitatea,
                               Localitatea = p.Localitatea,
                               VarstaPensionara = p.VarstaPensionara,
                               Institutia = p.Institutia,
                               Studii = p.Studii,
                               StudiiManageriale = p.StudiiManageriale,
                               GradulManagerial = p.GradulManagerial,
                               StudiiPedagogice = p.StudiiPedagogice,
                               StagiulPedagogic = p.StagiulPedagogic,
                               DataAngajaeii = p.DataAngajaeii,
                               DataEliberarii = p.DataEliberarii,
                               VechimeInMunca = p.VechimeInMunca,
                               CazatCamin = p.CazatCamin,
                               Camin = p.Camin,
                               Statutul = p.Statutul,
                               GradulStiintific = p.GradulStiintific,
                               ConcediuTermenLung = p.ConcediuTermenLung,
                               Formarea = p.Formarea,
                               DataFormarii = p.DataFormarii,
                           };

                string didact1 = "";
                string didact2 = "";
                string didact3 = "";
                string didact4 = "";


                if (did1.IsChecked == true)
                {
                    didact1 = "Nu are";
                }
                if (did2.IsChecked == true)
                {
                    didact2 = "Gradul 2";
                }
                if (did3.IsChecked == true)
                {
                    didact3 = "Gradul 1";
                }
                if (did4.IsChecked == true)
                {
                    didact4 = "Gradul Superior";
                }
                if (didact1 != "" || didact2 != "" || didact3 != "" || didact4 != "")
                {
                    perslistf = from p in perslistf

                               where p.GradDidactic == didact1 || p.GradDidactic == didact2 || p.GradDidactic == didact3 || p.GradDidactic == didact4

                               select new
                               {
                                   Nume = p.Nume,
                                   Prenume = p.Prenume,
                                   DataNasterii = p.DataNasterii,
                                   Genul = p.Genul,
                                   Idnp = p.Idnp,
                                   Email = p.Email,
                                   Mobil = p.Mobil,
                                   Catedra = p.Catedra,
                                   GradDidactic = p.GradDidactic,
                                   DataPrimiriiGradului = p.DataPrimiriiGradului,
                                   ModulAngajarii = p.ModulAngajarii,
                                   Nationalitatea = p.Nationalitatea,
                                   Localitatea = p.Localitatea,
                                   VarstaPensionara = p.VarstaPensionara,
                                   Institutia = p.Institutia,
                                   Studii = p.Studii,
                                   StudiiManageriale = p.StudiiManageriale,
                                   GradulManagerial = p.GradulManagerial,
                                   StudiiPedagogice = p.StudiiPedagogice,
                                   StagiulPedagogic = p.StagiulPedagogic,
                                   DataAngajaeii = p.DataAngajaeii,
                                   DataEliberarii = p.DataEliberarii,
                                   VechimeInMunca = p.VechimeInMunca,
                                   CazatCamin = p.CazatCamin,
                                   Camin = p.Camin,
                                   Statutul = p.Statutul,
                                   GradulStiintific = p.GradulStiintific,
                                   ConcediuTermenLung = p.ConcediuTermenLung,
                  
                                   Formarea = p.Formarea,
                                   DataFormarii = p.DataFormarii,
                               };


                }
                if (titlustcom.Text != "")
                {
                    perslistf = from p in perslistf

                               where p.GradulStiintific.Contains(titlustcom.Text)
                               select new
                               {
                                   Nume = p.Nume,
                                   Prenume = p.Prenume,
                                   DataNasterii = p.DataNasterii,
                                   Genul = p.Genul,
                                   Idnp = p.Idnp,
                                   Email = p.Email,
                                   Mobil = p.Mobil,
                                   Catedra = p.Catedra,
                                   GradDidactic = p.GradDidactic,
                                   DataPrimiriiGradului = p.DataPrimiriiGradului,
                                   ModulAngajarii = p.ModulAngajarii,
                                   Nationalitatea = p.Nationalitatea,
                                   Localitatea = p.Localitatea,
                                   VarstaPensionara = p.VarstaPensionara,
                                   Institutia = p.Institutia,
                                   Studii = p.Studii,
                                   StudiiManageriale = p.StudiiManageriale,
                                   GradulManagerial = p.GradulManagerial,
                                   StudiiPedagogice = p.StudiiPedagogice,
                                   StagiulPedagogic = p.StagiulPedagogic,
                                   DataAngajaeii = p.DataAngajaeii,
                                   DataEliberarii = p.DataEliberarii,
                                   VechimeInMunca = p.VechimeInMunca,
                                   CazatCamin = p.CazatCamin,
                                   Camin = p.Camin,
                                   Statutul = p.Statutul,
                                   GradulStiintific = p.GradulStiintific,
                                   ConcediuTermenLung = p.ConcediuTermenLung,
                              
                                   Formarea = p.Formarea,
                                   DataFormarii = p.DataFormarii,
                               };

                }
                if (Vechime.Text != "" && Vechime2.Text != "" && regStag.IsMatch(Vechime.Text) && regStag.IsMatch(Vechime2.Text))
                {
                    perslistf = from p in perslistf
                               where p.VechimeInMunca >= vechimea1 && p.VechimeInMunca <= vechimea2
                               select new
                               {
                                   Nume = p.Nume,
                                   Prenume = p.Prenume,
                                   DataNasterii = p.DataNasterii,
                                   Genul = p.Genul,
                                   Idnp = p.Idnp,
                                   Email = p.Email,
                                   Mobil = p.Mobil,
                                   Catedra = p.Catedra,
                                   GradDidactic = p.GradDidactic,
                                   DataPrimiriiGradului = p.DataPrimiriiGradului,
                                   ModulAngajarii = p.ModulAngajarii,
                                   Nationalitatea = p.Nationalitatea,
                                   Localitatea = p.Localitatea,
                                   VarstaPensionara = p.VarstaPensionara,
                                   Institutia = p.Institutia,
                                   Studii = p.Studii,
                                   StudiiManageriale = p.StudiiManageriale,
                                   GradulManagerial = p.GradulManagerial,
                                   StudiiPedagogice = p.StudiiPedagogice,
                                   StagiulPedagogic = p.StagiulPedagogic,
                                   DataAngajaeii = p.DataAngajaeii,
                                   DataEliberarii = p.DataEliberarii,
                                   VechimeInMunca = p.VechimeInMunca,
                                   CazatCamin = p.CazatCamin,
                                   Camin = p.Camin,
                                   Statutul = p.Statutul,
                                   GradulStiintific = p.GradulStiintific,
                                   ConcediuTermenLung = p.ConcediuTermenLung,
              
                                   Formarea = p.Formarea,
                                   DataFormarii = p.DataFormarii,
                               };

                }
                if (Stag.Text != "" && Stag2.Text != "" && regStag.IsMatch(Stag.Text) && regStag.IsMatch(Stag2.Text))
                {
                    perslistf = from p in perslistf

                               where p.StagiulPedagogic >= stagiu1 && p.StagiulPedagogic <= stagiu2
                               select new
                               {
                                   Nume = p.Nume,
                                   Prenume = p.Prenume,
                                   DataNasterii = p.DataNasterii,
                                   Genul = p.Genul,
                                   Idnp = p.Idnp,
                                   Email = p.Email,
                                   Mobil = p.Mobil,
                                   Catedra = p.Catedra,
                                   GradDidactic = p.GradDidactic,
                                   DataPrimiriiGradului = p.DataPrimiriiGradului,
                                   ModulAngajarii = p.ModulAngajarii,
                                   Nationalitatea = p.Nationalitatea,
                                   Localitatea = p.Localitatea,
                                   VarstaPensionara = p.VarstaPensionara,
                                   Institutia = p.Institutia,
                                   Studii = p.Studii,
                                   StudiiManageriale = p.StudiiManageriale,
                                   GradulManagerial = p.GradulManagerial,
                                   StudiiPedagogice = p.StudiiPedagogice,
                                   StagiulPedagogic = p.StagiulPedagogic,
                                   DataAngajaeii = p.DataAngajaeii,
                                   DataEliberarii = p.DataEliberarii,
                                   VechimeInMunca = p.VechimeInMunca,
                                   CazatCamin = p.CazatCamin,
                                   Camin = p.Camin,
                                   Statutul = p.Statutul,
                                   GradulStiintific = p.GradulStiintific,
                                   ConcediuTermenLung = p.ConcediuTermenLung,
                               
                                   Formarea = p.Formarea,
                                   DataFormarii = p.DataFormarii,
                               };

                }
                bool gn = true;
                if (masc.IsChecked == true)
                {
                    gn = true;
                }
                else if (fem.IsChecked == true)
                {
                    gn = false;
                }

                if (masc.IsChecked == true || fem.IsChecked == true)
                {
                    perslistf = from p in perslistf

                               where p.Genul == gn
                               select new
                               {
                                   Nume = p.Nume,
                                   Prenume = p.Prenume,
                                   DataNasterii = p.DataNasterii,
                                   Genul = p.Genul,
                                   Idnp = p.Idnp,
                                   Email = p.Email,
                                   Mobil = p.Mobil,
                                   Catedra = p.Catedra,
                                   GradDidactic = p.GradDidactic,
                                   DataPrimiriiGradului = p.DataPrimiriiGradului,
                                   ModulAngajarii = p.ModulAngajarii,
                                   Nationalitatea = p.Nationalitatea,
                                   Localitatea = p.Localitatea,
                                   VarstaPensionara = p.VarstaPensionara,
                                   Institutia = p.Institutia,
                                   Studii = p.Studii,
                                   StudiiManageriale = p.StudiiManageriale,
                                   GradulManagerial = p.GradulManagerial,
                                   StudiiPedagogice = p.StudiiPedagogice,
                                   StagiulPedagogic = p.StagiulPedagogic,
                                   DataAngajaeii = p.DataAngajaeii,
                                   DataEliberarii = p.DataEliberarii,
                                   VechimeInMunca = p.VechimeInMunca,
                                   CazatCamin = p.CazatCamin,
                                   Camin = p.Camin,
                                   Statutul = p.Statutul,
                                   GradulStiintific = p.GradulStiintific,
                                   ConcediuTermenLung = p.ConcediuTermenLung,
                           
                                   Formarea = p.Formarea,
                                   DataFormarii = p.DataFormarii,
                               };

                }
                if (cat.Text != "")
                {
                    perslistf = from p in perslistf

                               where p.Catedra == cat.Text
                               select new
                               {
                                   Nume = p.Nume,
                                   Prenume = p.Prenume,
                                   DataNasterii = p.DataNasterii,
                                   Genul = p.Genul,
                                   Idnp = p.Idnp,
                                   Email = p.Email,
                                   Mobil = p.Mobil,
                                   Catedra = p.Catedra,
                                   GradDidactic = p.GradDidactic,
                                   DataPrimiriiGradului = p.DataPrimiriiGradului,
                                   ModulAngajarii = p.ModulAngajarii,
                                   Nationalitatea = p.Nationalitatea,
                                   Localitatea = p.Localitatea,
                                   VarstaPensionara = p.VarstaPensionara,
                                   Institutia = p.Institutia,
                                   Studii = p.Studii,
                                   StudiiManageriale = p.StudiiManageriale,
                                   GradulManagerial = p.GradulManagerial,
                                   StudiiPedagogice = p.StudiiPedagogice,
                                   StagiulPedagogic = p.StagiulPedagogic,
                                   DataAngajaeii = p.DataAngajaeii,
                                   DataEliberarii = p.DataEliberarii,
                                   VechimeInMunca = p.VechimeInMunca,
                                   CazatCamin = p.CazatCamin,
                                   Camin = p.Camin,
                                   Statutul = p.Statutul,
                                   GradulStiintific = p.GradulStiintific,
                                   ConcediuTermenLung = p.ConcediuTermenLung,
                              
                                   Formarea = p.Formarea,
                                   DataFormarii = p.DataFormarii,
                               };

                }
                if (Form.SelectedDate.Value != null && Form2.SelectedDate.Value != null)
                {

                    perslistf = from p in perslistf

                               where Convert.ToDateTime(p.DataFormarii.ToString()) < Form2.SelectedDate.Value && Convert.ToDateTime(p.DataFormarii.ToString()) > Form.SelectedDate.Value
                               select new
                               {
                                   Nume = p.Nume,
                                   Prenume = p.Prenume,
                                   DataNasterii = p.DataNasterii,
                                   Genul = p.Genul,
                                   Idnp = p.Idnp,
                                   Email = p.Email,
                                   Mobil = p.Mobil,
                                   Catedra = p.Catedra,
                                   GradDidactic = p.GradDidactic,
                                   DataPrimiriiGradului = p.DataPrimiriiGradului,
                                   ModulAngajarii = p.ModulAngajarii,
                                   Nationalitatea = p.Nationalitatea,
                                   Localitatea = p.Localitatea,
                                   VarstaPensionara = p.VarstaPensionara,
                                   Institutia = p.Institutia,
                                   Studii = p.Studii,
                                   StudiiManageriale = p.StudiiManageriale,
                                   GradulManagerial = p.GradulManagerial,
                                   StudiiPedagogice = p.StudiiPedagogice,
                                   StagiulPedagogic = p.StagiulPedagogic,
                                   DataAngajaeii = p.DataAngajaeii,
                                   DataEliberarii = p.DataEliberarii,
                                   VechimeInMunca = p.VechimeInMunca,
                                   CazatCamin = p.CazatCamin,
                                   Camin = p.Camin,
                                   Statutul = p.Statutul,
                                   GradulStiintific = p.GradulStiintific,
                                   ConcediuTermenLung = p.ConcediuTermenLung,
                                 
                                   Formarea = p.Formarea,
                                   DataFormarii = p.DataFormarii,
                               };
                }

                if (telef.Text != "")
                {
                    perslistf = from p in perslistf

                               where p.Mobil.Contains(telef.Text)
                               select new
                               {
                                   Nume = p.Nume,
                                   Prenume = p.Prenume,
                                   DataNasterii = p.DataNasterii,
                                   Genul = p.Genul,
                                   Idnp = p.Idnp,
                                   Email = p.Email,
                                   Mobil = p.Mobil,
                                   Catedra = p.Catedra,
                                   GradDidactic = p.GradDidactic,
                                   DataPrimiriiGradului = p.DataPrimiriiGradului,
                                   ModulAngajarii = p.ModulAngajarii,
                                   Nationalitatea = p.Nationalitatea,
                                   Localitatea = p.Localitatea,
                                   VarstaPensionara = p.VarstaPensionara,
                                   Institutia = p.Institutia,
                                   Studii = p.Studii,
                                   StudiiManageriale = p.StudiiManageriale,
                                   GradulManagerial = p.GradulManagerial,
                                   StudiiPedagogice = p.StudiiPedagogice,
                                   StagiulPedagogic = p.StagiulPedagogic,
                                   DataAngajaeii = p.DataAngajaeii,
                                   DataEliberarii = p.DataEliberarii,
                                   VechimeInMunca = p.VechimeInMunca,
                                   CazatCamin = p.CazatCamin,
                                   Camin = p.Camin,
                                   Statutul = p.Statutul,
                                   GradulStiintific = p.GradulStiintific,
                                   ConcediuTermenLung = p.ConcediuTermenLung,
                              
                                   Formarea = p.Formarea,
                                   DataFormarii = p.DataFormarii,
                               };

                }
                if (posta.Text != "")
                {
                    perslistf = from p in perslistf

                               where p.Email.Contains(posta.Text)
                               select new
                               {
                                   Nume = p.Nume,
                                   Prenume = p.Prenume,
                                   DataNasterii = p.DataNasterii,
                                   Genul = p.Genul,
                                   Idnp = p.Idnp,
                                   Email = p.Email,
                                   Mobil = p.Mobil,
                                   Catedra = p.Catedra,
                                   GradDidactic = p.GradDidactic,
                                   DataPrimiriiGradului = p.DataPrimiriiGradului,
                                   ModulAngajarii = p.ModulAngajarii,
                                   Nationalitatea = p.Nationalitatea,
                                   Localitatea = p.Localitatea,
                                   VarstaPensionara = p.VarstaPensionara,
                                   Institutia = p.Institutia,
                                   Studii = p.Studii,
                                   StudiiManageriale = p.StudiiManageriale,
                                   GradulManagerial = p.GradulManagerial,
                                   StudiiPedagogice = p.StudiiPedagogice,
                                   StagiulPedagogic = p.StagiulPedagogic,
                                   DataAngajaeii = p.DataAngajaeii,
                                   DataEliberarii = p.DataEliberarii,
                                   VechimeInMunca = p.VechimeInMunca,
                                   CazatCamin = p.CazatCamin,
                                   Camin = p.Camin,
                                   Statutul = p.Statutul,
                                   GradulStiintific = p.GradulStiintific,
                                   ConcediuTermenLung = p.ConcediuTermenLung,
                            
                                   Formarea = p.Formarea,
                                   DataFormarii = p.DataFormarii,
                               };

                }
                var perslist3 = from p in perslistf
                              
                                select new
                                {
                                    Nume = p.Nume,
                                    Prenume = p.Prenume,
                                    DataNasterii = p.DataNasterii,
                                    Genul = p.Genul,
                                    Idnp = p.Idnp,
                                    Email = p.Email,
                                    Mobil = p.Mobil,
                                    Catedra = p.Catedra,
                                    GradDidactic = p.GradDidactic,
                                    DataPrimiriiGradului = p.DataPrimiriiGradului,
                                    ModulAngajarii = p.ModulAngajarii,
                                    Nationalitatea = p.Nationalitatea,
                                    Localitatea = p.Localitatea,
                                    VarstaPensionara = p.VarstaPensionara,
                                    Institutia = p.Institutia,
                                    Studii = p.Studii,
                                    StudiiManageriale = p.StudiiManageriale,
                                    GradulManagerial = p.GradulManagerial,
                                    StudiiPedagogice = p.StudiiPedagogice,
                                    StagiulPedagogic = p.StagiulPedagogic,
                                    DataAngajaeii = p.DataAngajaeii,
                                    DataEliberarii = p.DataEliberarii,
                                    VechimeInMunca = p.VechimeInMunca,
                                    CazatCamin = p.CazatCamin,
                                    Camin = p.Camin,
                                    Statutul = p.Statutul,
                                    GradulStiintific = p.GradulStiintific,
                                    ConcediuTermenLung = p.ConcediuTermenLung,
                            
                                    Formarea = p.Formarea,
                                    DataFormarii = p.DataFormarii.ToShortDateString(),
                                };

                PersAfisGrid.ItemsSource = perslist3.ToList();
                NrPers.Content = perslist3.Count();
                DisciplinaText.Text = "";
                did1.IsChecked = false;
                did2.IsChecked = false;
                did3.IsChecked = false;
                did4.IsChecked = false;
                titlustcom.Text = "";
                Vechime.Text = "";
                Vechime2.Text = "";
                Stag.Text = "";
                Stag2.Text = "";
                masc.IsChecked = false;
                fem.IsChecked = false;
                cat.Text = "";
                fun.Text = "";
                telef.Text = "";
                posta.Text = "";

                t = Form.Template.FindName("PART_TextBox", Form) as DatePickerTextBox;
                if (t != null)
                    t.Text = "Select date";

                t2 = Form2.Template.FindName("PART_TextBox", Form2) as DatePickerTextBox;
                if (t2 != null)
                    t2.Text = "Select date";

            }

            else if (DisciplinaText.Text != "" )
            {
                perslist = from p in perslist

                           where p.Disciplina.Contains(DisciplinaText.Text)
                           select new
                           {
                               Nume = p.Nume,
                               Prenume = p.Prenume,
                               DataNasterii = p.DataNasterii,
                               Genul = p.Genul,
                               Idnp = p.Idnp,
                               Email = p.Email,
                               Mobil = p.Mobil,
                               Catedra = p.Catedra,
                               GradDidactic = p.GradDidactic,
                               DataPrimiriiGradului = p.DataPrimiriiGradului,
                               ModulAngajarii = p.ModulAngajarii,
                               Nationalitatea = p.Nationalitatea,
                               Localitatea = p.Localitatea,
                               VarstaPensionara = p.VarstaPensionara,
                               Institutia = p.Institutia,
                               Studii = p.Studii,
                               StudiiManageriale = p.StudiiManageriale,
                               GradulManagerial = p.GradulManagerial,
                               StudiiPedagogice = p.StudiiPedagogice,
                               StagiulPedagogic = p.StagiulPedagogic,
                               DataAngajaeii = p.DataAngajaeii,
                               DataEliberarii = p.DataEliberarii,
                               VechimeInMunca = p.VechimeInMunca,
                               CazatCamin = p.CazatCamin,
                               Camin = p.Camin,
                               Statutul = p.Statutul,
                               GradulStiintific = p.GradulStiintific,
                               ConcediuTermenLung = p.ConcediuTermenLung,
                               Disciplina = p.Disciplina,
                              
                      

                           };

                string didact1 = "";
                string didact2 = "";
                string didact3 = "";
                string didact4 = "";


                if (did1.IsChecked == true)
                {
                    didact1 = "Nu are";
                }
                if (did2.IsChecked == true)
                {
                    didact2 = "Gradul 2";
                }
                if (did3.IsChecked == true)
                {
                    didact3 = "Gradul 1";
                }
                if (did4.IsChecked == true)
                {
                    didact4 = "Gradul Superior";
                }
                if (didact1 != "" || didact2 != "" || didact3 != "" || didact4 != "")
                {
                    perslist = from p in perslist

                               where p.GradDidactic == didact1 || p.GradDidactic == didact2 || p.GradDidactic == didact3 || p.GradDidactic == didact4

                               select new
                               {
                                   Nume = p.Nume,
                                   Prenume = p.Prenume,
                                   DataNasterii = p.DataNasterii,
                                   Genul = p.Genul,
                                   Idnp = p.Idnp,
                                   Email = p.Email,
                                   Mobil = p.Mobil,
                                   Catedra = p.Catedra,
                                   GradDidactic = p.GradDidactic,
                                   DataPrimiriiGradului = p.DataPrimiriiGradului,
                                   ModulAngajarii = p.ModulAngajarii,
                                   Nationalitatea = p.Nationalitatea,
                                   Localitatea = p.Localitatea,
                                   VarstaPensionara = p.VarstaPensionara,
                                   Institutia = p.Institutia,
                                   Studii = p.Studii,
                                   StudiiManageriale = p.StudiiManageriale,
                                   GradulManagerial = p.GradulManagerial,
                                   StudiiPedagogice = p.StudiiPedagogice,
                                   StagiulPedagogic = p.StagiulPedagogic,
                                   DataAngajaeii = p.DataAngajaeii,
                                   DataEliberarii = p.DataEliberarii,
                                   VechimeInMunca = p.VechimeInMunca,
                                   CazatCamin = p.CazatCamin,
                                   Camin = p.Camin,
                                   Statutul = p.Statutul,
                                   GradulStiintific = p.GradulStiintific,
                                   ConcediuTermenLung = p.ConcediuTermenLung,
                                   Disciplina = p.Disciplina,
                                  
                               };


                }
                if (titlustcom.Text != "")
                {
                    perslist = from p in perslist

                               where p.GradulStiintific.Contains(titlustcom.Text)
                               select new
                               {
                                   Nume = p.Nume,
                                   Prenume = p.Prenume,
                                   DataNasterii = p.DataNasterii,
                                   Genul = p.Genul,
                                   Idnp = p.Idnp,
                                   Email = p.Email,
                                   Mobil = p.Mobil,
                                   Catedra = p.Catedra,
                                   GradDidactic = p.GradDidactic,
                                   DataPrimiriiGradului = p.DataPrimiriiGradului,
                                   ModulAngajarii = p.ModulAngajarii,
                                   Nationalitatea = p.Nationalitatea,
                                   Localitatea = p.Localitatea,
                                   VarstaPensionara = p.VarstaPensionara,
                                   Institutia = p.Institutia,
                                   Studii = p.Studii,
                                   StudiiManageriale = p.StudiiManageriale,
                                   GradulManagerial = p.GradulManagerial,
                                   StudiiPedagogice = p.StudiiPedagogice,
                                   StagiulPedagogic = p.StagiulPedagogic,
                                   DataAngajaeii = p.DataAngajaeii,
                                   DataEliberarii = p.DataEliberarii,
                                   VechimeInMunca = p.VechimeInMunca,
                                   CazatCamin = p.CazatCamin,
                                   Camin = p.Camin,
                                   Statutul = p.Statutul,
                                   GradulStiintific = p.GradulStiintific,
                                   ConcediuTermenLung = p.ConcediuTermenLung,
                                   Disciplina = p.Disciplina,
                                  
                               };

                }
                if (Vechime.Text != "" && Vechime2.Text != "" && regStag.IsMatch(Vechime.Text) && regStag.IsMatch(Vechime2.Text))
                {
                    perslist = from p in perslist

                               where p.VechimeInMunca >= vechimea1 && p.VechimeInMunca <= vechimea2
                               select new
                               {
                                   Nume = p.Nume,
                                   Prenume = p.Prenume,
                                   DataNasterii = p.DataNasterii,
                                   Genul = p.Genul,
                                   Idnp = p.Idnp,
                                   Email = p.Email,
                                   Mobil = p.Mobil,
                                   Catedra = p.Catedra,
                                   GradDidactic = p.GradDidactic,
                                   DataPrimiriiGradului = p.DataPrimiriiGradului,
                                   ModulAngajarii = p.ModulAngajarii,
                                   Nationalitatea = p.Nationalitatea,
                                   Localitatea = p.Localitatea,
                                   VarstaPensionara = p.VarstaPensionara,
                                   Institutia = p.Institutia,
                                   Studii = p.Studii,
                                   StudiiManageriale = p.StudiiManageriale,
                                   GradulManagerial = p.GradulManagerial,
                                   StudiiPedagogice = p.StudiiPedagogice,
                                   StagiulPedagogic = p.StagiulPedagogic,
                                   DataAngajaeii = p.DataAngajaeii,
                                   DataEliberarii = p.DataEliberarii,
                                   VechimeInMunca = p.VechimeInMunca,
                                   CazatCamin = p.CazatCamin,
                                   Camin = p.Camin,
                                   Statutul = p.Statutul,
                                   GradulStiintific = p.GradulStiintific,
                                   ConcediuTermenLung = p.ConcediuTermenLung,
                                   Disciplina = p.Disciplina,
                                   
                               };

                }
                if (Stag.Text != "" && Stag2.Text != "" && regStag.IsMatch(Stag.Text) && regStag.IsMatch(Stag2.Text))
                {
                    perslist = from p in perslist

                               where p.StagiulPedagogic >= stagiu1 && p.StagiulPedagogic <= stagiu2
                               select new
                               {
                                   Nume = p.Nume,
                                   Prenume = p.Prenume,
                                   DataNasterii = p.DataNasterii,
                                   Genul = p.Genul,
                                   Idnp = p.Idnp,
                                   Email = p.Email,
                                   Mobil = p.Mobil,
                                   Catedra = p.Catedra,
                                   GradDidactic = p.GradDidactic,
                                   DataPrimiriiGradului = p.DataPrimiriiGradului,
                                   ModulAngajarii = p.ModulAngajarii,
                                   Nationalitatea = p.Nationalitatea,
                                   Localitatea = p.Localitatea,
                                   VarstaPensionara = p.VarstaPensionara,
                                   Institutia = p.Institutia,
                                   Studii = p.Studii,
                                   StudiiManageriale = p.StudiiManageriale,
                                   GradulManagerial = p.GradulManagerial,
                                   StudiiPedagogice = p.StudiiPedagogice,
                                   StagiulPedagogic = p.StagiulPedagogic,
                                   DataAngajaeii = p.DataAngajaeii,
                                   DataEliberarii = p.DataEliberarii,
                                   VechimeInMunca = p.VechimeInMunca,
                                   CazatCamin = p.CazatCamin,
                                   Camin = p.Camin,
                                   Statutul = p.Statutul,
                                   GradulStiintific = p.GradulStiintific,
                                   ConcediuTermenLung = p.ConcediuTermenLung,
                                   Disciplina = p.Disciplina,
                                  
                               };

                }
                bool gn = true;
                if (masc.IsChecked == true)
                {
                    gn = true;
                }
                else if (fem.IsChecked == true)
                {
                    gn = false;
                }

                if (masc.IsChecked == true || fem.IsChecked == true)
                {
                    perslist = from p in perslist

                               where p.Genul == gn
                               select new
                               {
                                   Nume = p.Nume,
                                   Prenume = p.Prenume,
                                   DataNasterii = p.DataNasterii,
                                   Genul = p.Genul,
                                   Idnp = p.Idnp,
                                   Email = p.Email,
                                   Mobil = p.Mobil,
                                   Catedra = p.Catedra,
                                   GradDidactic = p.GradDidactic,
                                   DataPrimiriiGradului = p.DataPrimiriiGradului,
                                   ModulAngajarii = p.ModulAngajarii,
                                   Nationalitatea = p.Nationalitatea,
                                   Localitatea = p.Localitatea,
                                   VarstaPensionara = p.VarstaPensionara,
                                   Institutia = p.Institutia,
                                   Studii = p.Studii,
                                   StudiiManageriale = p.StudiiManageriale,
                                   GradulManagerial = p.GradulManagerial,
                                   StudiiPedagogice = p.StudiiPedagogice,
                                   StagiulPedagogic = p.StagiulPedagogic,
                                   DataAngajaeii = p.DataAngajaeii,
                                   DataEliberarii = p.DataEliberarii,
                                   VechimeInMunca = p.VechimeInMunca,
                                   CazatCamin = p.CazatCamin,
                                   Camin = p.Camin,
                                   Statutul = p.Statutul,
                                   GradulStiintific = p.GradulStiintific,
                                   ConcediuTermenLung = p.ConcediuTermenLung,
                                   Disciplina = p.Disciplina,
                                   
                               };

                }
                if (cat.Text != "")
                {
                    perslist = from p in perslist

                               where p.Catedra == cat.Text
                               select new
                               {
                                   Nume = p.Nume,
                                   Prenume = p.Prenume,
                                   DataNasterii = p.DataNasterii,
                                   Genul = p.Genul,
                                   Idnp = p.Idnp,
                                   Email = p.Email,
                                   Mobil = p.Mobil,
                                   Catedra = p.Catedra,
                                   GradDidactic = p.GradDidactic,
                                   DataPrimiriiGradului = p.DataPrimiriiGradului,
                                   ModulAngajarii = p.ModulAngajarii,
                                   Nationalitatea = p.Nationalitatea,
                                   Localitatea = p.Localitatea,
                                   VarstaPensionara = p.VarstaPensionara,
                                   Institutia = p.Institutia,
                                   Studii = p.Studii,
                                   StudiiManageriale = p.StudiiManageriale,
                                   GradulManagerial = p.GradulManagerial,
                                   StudiiPedagogice = p.StudiiPedagogice,
                                   StagiulPedagogic = p.StagiulPedagogic,
                                   DataAngajaeii = p.DataAngajaeii,
                                   DataEliberarii = p.DataEliberarii,
                                   VechimeInMunca = p.VechimeInMunca,
                                   CazatCamin = p.CazatCamin,
                                   Camin = p.Camin,
                                   Statutul = p.Statutul,
                                   GradulStiintific = p.GradulStiintific,
                                   ConcediuTermenLung = p.ConcediuTermenLung,
                                   Disciplina = p.Disciplina,
                                   
                               };

                }

               

                if (telef.Text != "")
                {
                    perslist = from p in perslist

                               where p.Mobil.Contains(telef.Text)
                               select new
                               {
                                   Nume = p.Nume,
                                   Prenume = p.Prenume,
                                   DataNasterii = p.DataNasterii,
                                   Genul = p.Genul,
                                   Idnp = p.Idnp,
                                   Email = p.Email,
                                   Mobil = p.Mobil,
                                   Catedra = p.Catedra,
                                   GradDidactic = p.GradDidactic,
                                   DataPrimiriiGradului = p.DataPrimiriiGradului,
                                   ModulAngajarii = p.ModulAngajarii,
                                   Nationalitatea = p.Nationalitatea,
                                   Localitatea = p.Localitatea,
                                   VarstaPensionara = p.VarstaPensionara,
                                   Institutia = p.Institutia,
                                   Studii = p.Studii,
                                   StudiiManageriale = p.StudiiManageriale,
                                   GradulManagerial = p.GradulManagerial,
                                   StudiiPedagogice = p.StudiiPedagogice,
                                   StagiulPedagogic = p.StagiulPedagogic,
                                   DataAngajaeii = p.DataAngajaeii,
                                   DataEliberarii = p.DataEliberarii,
                                   VechimeInMunca = p.VechimeInMunca,
                                   CazatCamin = p.CazatCamin,
                                   Camin = p.Camin,
                                   Statutul = p.Statutul,
                                   GradulStiintific = p.GradulStiintific,
                                   ConcediuTermenLung = p.ConcediuTermenLung,
                                   Disciplina = p.Disciplina,
                                
                               };

                }
                if (posta.Text != "")
                {
                    perslist = from p in perslist

                               where p.Email.Contains(posta.Text)
                               select new
                               {
                                   Nume = p.Nume,
                                   Prenume = p.Prenume,
                                   DataNasterii = p.DataNasterii,
                                   Genul = p.Genul,
                                   Idnp = p.Idnp,
                                   Email = p.Email,
                                   Mobil = p.Mobil,
                                   Catedra = p.Catedra,
                                   GradDidactic = p.GradDidactic,
                                   DataPrimiriiGradului = p.DataPrimiriiGradului,
                                   ModulAngajarii = p.ModulAngajarii,
                                   Nationalitatea = p.Nationalitatea,
                                   Localitatea = p.Localitatea,
                                   VarstaPensionara = p.VarstaPensionara,
                                   Institutia = p.Institutia,
                                   Studii = p.Studii,
                                   StudiiManageriale = p.StudiiManageriale,
                                   GradulManagerial = p.GradulManagerial,
                                   StudiiPedagogice = p.StudiiPedagogice,
                                   StagiulPedagogic = p.StagiulPedagogic,
                                   DataAngajaeii = p.DataAngajaeii,
                                   DataEliberarii = p.DataEliberarii,
                                   VechimeInMunca = p.VechimeInMunca,
                                   CazatCamin = p.CazatCamin,
                                   Camin = p.Camin,
                                   Statutul = p.Statutul,
                                   GradulStiintific = p.GradulStiintific,
                                   ConcediuTermenLung = p.ConcediuTermenLung,
                                   Disciplina = p.Disciplina,
                                
                               };

                }
                var perslist4 = from p in perslist
                                select new
                                {
                                    Nume = p.Nume,
                                    Prenume = p.Prenume,
                                    DataNasterii = p.DataNasterii,
                                    Genul = p.Genul,
                                    Idnp = p.Idnp,
                                    Email = p.Email,
                                    Mobil = p.Mobil,
                                    Catedra = p.Catedra,
                                    GradDidactic = p.GradDidactic,
                                    DataPrimiriiGradului = p.DataPrimiriiGradului,
                                    ModulAngajarii = p.ModulAngajarii,
                                    Nationalitatea = p.Nationalitatea,
                                    Localitatea = p.Localitatea,
                                    VarstaPensionara = p.VarstaPensionara,
                                    Institutia = p.Institutia,
                                    Studii = p.Studii,
                                    StudiiManageriale = p.StudiiManageriale,
                                    GradulManagerial = p.GradulManagerial,
                                    StudiiPedagogice = p.StudiiPedagogice,
                                    StagiulPedagogic = p.StagiulPedagogic,
                                    DataAngajaeii = p.DataAngajaeii,
                                    DataEliberarii = p.DataEliberarii,
                                    VechimeInMunca = p.VechimeInMunca,
                                    CazatCamin = p.CazatCamin,
                                    Camin = p.Camin,
                                    Statutul = p.Statutul,
                                    GradulStiintific = p.GradulStiintific,
                                    ConcediuTermenLung = p.ConcediuTermenLung,
                                    Disciplina = p.Disciplina,
                                 

                                };
                PersAfisGrid.ItemsSource = perslist4.ToList();
                NrPers.Content = perslist4.Count();
                DisciplinaText.Text = "";
                did1.IsChecked = false;
                did2.IsChecked = false;
                did3.IsChecked = false;
                did4.IsChecked = false;
                titlustcom.Text = "";
                Vechime.Text = "";
                Vechime2.Text = "";
                Stag.Text = "";
                Stag2.Text = "";
                masc.IsChecked = false;
                fem.IsChecked = false;
                cat.Text = "";
                fun.Text = "";
                telef.Text = "";
                posta.Text = "";

                t = Form.Template.FindName("PART_TextBox", Form) as DatePickerTextBox;
                if (t != null)
                    t.Text = "Select date";

                t2 = Form2.Template.FindName("PART_TextBox", Form2) as DatePickerTextBox;
                if (t2 != null)
                    t2.Text = "Select date";
            }
            else
            {


                string didact1 = "";
                string didact2 = "";
                string didact3 = "";
                string didact4 = "";


                if (did1.IsChecked == true)
                {
                    didact1 = "Nu are";
                }
                if (did2.IsChecked == true)
                {
                    didact2 = "Gradul 2";
                }
                if (did3.IsChecked == true)
                {
                    didact3 = "Gradul 1";
                }
                if (did4.IsChecked == true)
                {
                    didact4 = "Gradul Superior";
                }
                if (didact1 != "" || didact2 != "" || didact3 != "" || didact4 != "")
                {
                    perslist2 = from p in perslist2

                                where p.GradDidactic == didact1 || p.GradDidactic == didact2 || p.GradDidactic == didact3 || p.GradDidactic == didact4

                                select new
                                {
                                    Nume = p.Nume,
                                    Prenume = p.Prenume,
                                    DataNasterii = p.DataNasterii,
                                    Genul = p.Genul,
                                    Idnp = p.Idnp,
                                    Email = p.Email,
                                    Mobil = p.Mobil,
                                    Catedra = p.Catedra,
                                    GradDidactic = p.GradDidactic,
                                    DataPrimiriiGradului = p.DataPrimiriiGradului,
                                    ModulAngajarii = p.ModulAngajarii,
                                    Nationalitatea = p.Nationalitatea,
                                    Localitatea = p.Localitatea,
                                    VarstaPensionara = p.VarstaPensionara,
                                    Institutia = p.Institutia,
                                    Studii = p.Studii,
                                    StudiiManageriale = p.StudiiManageriale,
                                    GradulManagerial = p.GradulManagerial,
                                    StudiiPedagogice = p.StudiiPedagogice,
                                    StagiulPedagogic = p.StagiulPedagogic,
                                    DataAngajaeii = p.DataAngajaeii,
                                    DataEliberarii = p.DataEliberarii,
                                    VechimeInMunca = p.VechimeInMunca,
                                    CazatCamin = p.CazatCamin,
                                    Camin = p.Camin,
                                    Statutul = p.Statutul,
                                    GradulStiintific = p.GradulStiintific,
                                    ConcediuTermenLung = p.ConcediuTermenLung,
                                };


                }
                if (titlustcom.Text != "")
                {
                    perslist2 = from p in perslist2

                                where p.GradulStiintific.Contains(titlustcom.Text)
                                select new
                                {
                                    Nume = p.Nume,
                                    Prenume = p.Prenume,
                                    DataNasterii = p.DataNasterii,
                                    Genul = p.Genul,
                                    Idnp = p.Idnp,
                                    Email = p.Email,
                                    Mobil = p.Mobil,
                                    Catedra = p.Catedra,
                                    GradDidactic = p.GradDidactic,
                                    DataPrimiriiGradului = p.DataPrimiriiGradului,
                                    ModulAngajarii = p.ModulAngajarii,
                                    Nationalitatea = p.Nationalitatea,
                                    Localitatea = p.Localitatea,
                                    VarstaPensionara = p.VarstaPensionara,
                                    Institutia = p.Institutia,
                                    Studii = p.Studii,
                                    StudiiManageriale = p.StudiiManageriale,
                                    GradulManagerial = p.GradulManagerial,
                                    StudiiPedagogice = p.StudiiPedagogice,
                                    StagiulPedagogic = p.StagiulPedagogic,
                                    DataAngajaeii = p.DataAngajaeii,
                                    DataEliberarii = p.DataEliberarii,
                                    VechimeInMunca = p.VechimeInMunca,
                                    CazatCamin = p.CazatCamin,
                                    Camin = p.Camin,
                                    Statutul = p.Statutul,
                                    GradulStiintific = p.GradulStiintific,
                                    ConcediuTermenLung = p.ConcediuTermenLung,
                                };

                }
                if (Vechime.Text != "" && Vechime2.Text != "" && regStag.IsMatch(Vechime.Text) && regStag.IsMatch(Vechime2.Text))
                {
                    perslist2 = from p in perslist2

                                where p.VechimeInMunca >= vechimea1 && p.VechimeInMunca <= vechimea2
                                select new
                                {
                                    Nume = p.Nume,
                                    Prenume = p.Prenume,
                                    DataNasterii = p.DataNasterii,
                                    Genul = p.Genul,
                                    Idnp = p.Idnp,
                                    Email = p.Email,
                                    Mobil = p.Mobil,
                                    Catedra = p.Catedra,
                                    GradDidactic = p.GradDidactic,
                                    DataPrimiriiGradului = p.DataPrimiriiGradului,
                                    ModulAngajarii = p.ModulAngajarii,
                                    Nationalitatea = p.Nationalitatea,
                                    Localitatea = p.Localitatea,
                                    VarstaPensionara = p.VarstaPensionara,
                                    Institutia = p.Institutia,
                                    Studii = p.Studii,
                                    StudiiManageriale = p.StudiiManageriale,
                                    GradulManagerial = p.GradulManagerial,
                                    StudiiPedagogice = p.StudiiPedagogice,
                                    StagiulPedagogic = p.StagiulPedagogic,
                                    DataAngajaeii = p.DataAngajaeii,
                                    DataEliberarii = p.DataEliberarii,
                                    VechimeInMunca = p.VechimeInMunca,
                                    CazatCamin = p.CazatCamin,
                                    Camin = p.Camin,
                                    Statutul = p.Statutul,
                                    GradulStiintific = p.GradulStiintific,
                                    ConcediuTermenLung = p.ConcediuTermenLung,
                                };

                }
                if (Stag.Text != "" && Stag2.Text != "" && regStag.IsMatch(Stag.Text) && regStag.IsMatch(Stag2.Text))
                {
                    perslist2 = from p in perslist2

                                where p.StagiulPedagogic >= stagiu1 && p.StagiulPedagogic <= stagiu2
                                select new
                                {
                                    Nume = p.Nume,
                                    Prenume = p.Prenume,
                                    DataNasterii = p.DataNasterii,
                                    Genul = p.Genul,
                                    Idnp = p.Idnp,
                                    Email = p.Email,
                                    Mobil = p.Mobil,
                                    Catedra = p.Catedra,
                                    GradDidactic = p.GradDidactic,
                                    DataPrimiriiGradului = p.DataPrimiriiGradului,
                                    ModulAngajarii = p.ModulAngajarii,
                                    Nationalitatea = p.Nationalitatea,
                                    Localitatea = p.Localitatea,
                                    VarstaPensionara = p.VarstaPensionara,
                                    Institutia = p.Institutia,
                                    Studii = p.Studii,
                                    StudiiManageriale = p.StudiiManageriale,
                                    GradulManagerial = p.GradulManagerial,
                                    StudiiPedagogice = p.StudiiPedagogice,
                                    StagiulPedagogic = p.StagiulPedagogic,
                                    DataAngajaeii = p.DataAngajaeii,
                                    DataEliberarii = p.DataEliberarii,
                                    VechimeInMunca = p.VechimeInMunca,
                                    CazatCamin = p.CazatCamin,
                                    Camin = p.Camin,
                                    Statutul = p.Statutul,
                                    GradulStiintific = p.GradulStiintific,
                                    ConcediuTermenLung = p.ConcediuTermenLung,
                                };

                }
                bool gn = true;
                if (masc.IsChecked == true)
                {
                    gn = true;
                }
                else if (fem.IsChecked == true)
                {
                    gn = false;
                }

                if (masc.IsChecked == true || fem.IsChecked == true)
                {
                    perslist2 = from p in perslist2

                                where p.Genul == gn
                                select new
                                {
                                    Nume = p.Nume,
                                    Prenume = p.Prenume,
                                    DataNasterii = p.DataNasterii,
                                    Genul = p.Genul,
                                    Idnp = p.Idnp,
                                    Email = p.Email,
                                    Mobil = p.Mobil,
                                    Catedra = p.Catedra,
                                    GradDidactic = p.GradDidactic,
                                    DataPrimiriiGradului = p.DataPrimiriiGradului,
                                    ModulAngajarii = p.ModulAngajarii,
                                    Nationalitatea = p.Nationalitatea,
                                    Localitatea = p.Localitatea,
                                    VarstaPensionara = p.VarstaPensionara,
                                    Institutia = p.Institutia,
                                    Studii = p.Studii,
                                    StudiiManageriale = p.StudiiManageriale,
                                    GradulManagerial = p.GradulManagerial,
                                    StudiiPedagogice = p.StudiiPedagogice,
                                    StagiulPedagogic = p.StagiulPedagogic,
                                    DataAngajaeii = p.DataAngajaeii,
                                    DataEliberarii = p.DataEliberarii,
                                    VechimeInMunca = p.VechimeInMunca,
                                    CazatCamin = p.CazatCamin,
                                    Camin = p.Camin,
                                    Statutul = p.Statutul,
                                    GradulStiintific = p.GradulStiintific,
                                    ConcediuTermenLung = p.ConcediuTermenLung,
                                };

                }
                if (cat.Text != "")
                {
                    perslist2 = from p in perslist2

                                where p.Catedra == cat.Text
                                select new
                                {
                                    Nume = p.Nume,
                                    Prenume = p.Prenume,
                                    DataNasterii = p.DataNasterii,
                                    Genul = p.Genul,
                                    Idnp = p.Idnp,
                                    Email = p.Email,
                                    Mobil = p.Mobil,
                                    Catedra = p.Catedra,
                                    GradDidactic = p.GradDidactic,
                                    DataPrimiriiGradului = p.DataPrimiriiGradului,
                                    ModulAngajarii = p.ModulAngajarii,
                                    Nationalitatea = p.Nationalitatea,
                                    Localitatea = p.Localitatea,
                                    VarstaPensionara = p.VarstaPensionara,
                                    Institutia = p.Institutia,
                                    Studii = p.Studii,
                                    StudiiManageriale = p.StudiiManageriale,
                                    GradulManagerial = p.GradulManagerial,
                                    StudiiPedagogice = p.StudiiPedagogice,
                                    StagiulPedagogic = p.StagiulPedagogic,
                                    DataAngajaeii = p.DataAngajaeii,
                                    DataEliberarii = p.DataEliberarii,
                                    VechimeInMunca = p.VechimeInMunca,
                                    CazatCamin = p.CazatCamin,
                                    Camin = p.Camin,
                                    Statutul = p.Statutul,
                                    GradulStiintific = p.GradulStiintific,
                                    ConcediuTermenLung = p.ConcediuTermenLung,
                                };

                }

                if (telef.Text != "")
                {
                    perslist2 = from p in perslist2

                                where p.Mobil.Contains(telef.Text)
                                select new
                                {
                                    Nume = p.Nume,
                                    Prenume = p.Prenume,
                                    DataNasterii = p.DataNasterii,
                                    Genul = p.Genul,
                                    Idnp = p.Idnp,
                                    Email = p.Email,
                                    Mobil = p.Mobil,
                                    Catedra = p.Catedra,
                                    GradDidactic = p.GradDidactic,
                                    DataPrimiriiGradului = p.DataPrimiriiGradului,
                                    ModulAngajarii = p.ModulAngajarii,
                                    Nationalitatea = p.Nationalitatea,
                                    Localitatea = p.Localitatea,
                                    VarstaPensionara = p.VarstaPensionara,
                                    Institutia = p.Institutia,
                                    Studii = p.Studii,
                                    StudiiManageriale = p.StudiiManageriale,
                                    GradulManagerial = p.GradulManagerial,
                                    StudiiPedagogice = p.StudiiPedagogice,
                                    StagiulPedagogic = p.StagiulPedagogic,
                                    DataAngajaeii = p.DataAngajaeii,
                                    DataEliberarii = p.DataEliberarii,
                                    VechimeInMunca = p.VechimeInMunca,
                                    CazatCamin = p.CazatCamin,
                                    Camin = p.Camin,
                                    Statutul = p.Statutul,
                                    GradulStiintific = p.GradulStiintific,
                                    ConcediuTermenLung = p.ConcediuTermenLung,
                                };

                }
                if (posta.Text != "")
                {
                    perslist2 = from p in perslist2

                                where p.Email.Contains(posta.Text)
                                select new
                                {
                                    Nume = p.Nume,
                                    Prenume = p.Prenume,
                                    DataNasterii = p.DataNasterii,
                                    Genul = p.Genul,
                                    Idnp = p.Idnp,
                                    Email = p.Email,
                                    Mobil = p.Mobil,
                                    Catedra = p.Catedra,
                                    GradDidactic = p.GradDidactic,
                                    DataPrimiriiGradului = p.DataPrimiriiGradului,
                                    ModulAngajarii = p.ModulAngajarii,
                                    Nationalitatea = p.Nationalitatea,
                                    Localitatea = p.Localitatea,
                                    VarstaPensionara = p.VarstaPensionara,
                                    Institutia = p.Institutia,
                                    Studii = p.Studii,
                                    StudiiManageriale = p.StudiiManageriale,
                                    GradulManagerial = p.GradulManagerial,
                                    StudiiPedagogice = p.StudiiPedagogice,
                                    StagiulPedagogic = p.StagiulPedagogic,
                                    DataAngajaeii = p.DataAngajaeii,
                                    DataEliberarii = p.DataEliberarii,
                                    VechimeInMunca = p.VechimeInMunca,
                                    CazatCamin = p.CazatCamin,
                                    Camin = p.Camin,
                                    Statutul = p.Statutul,
                                    GradulStiintific = p.GradulStiintific,
                                    ConcediuTermenLung = p.ConcediuTermenLung,
                                };

                }
                PersAfisGrid.ItemsSource = perslist2.ToList();
                NrPers.Content = perslist2.Count();

                did1.IsChecked = false;
                did2.IsChecked = false;
                did3.IsChecked = false;
                did4.IsChecked = false;
                titlustcom.Text = "";
                Vechime.Text = "";
                Vechime2.Text = "";
                Stag.Text = "";
                Stag2.Text = "";
                masc.IsChecked = false;
                fem.IsChecked = false;
                cat.Text = "";
                fun.Text = "";
                telef.Text = "";
                posta.Text = "";
            }

        }

        private void anulare_Click(object sender, RoutedEventArgs e)
        {
            DataContext db = new DataContext(conn);
            Table<Persoane> persoana = db.GetTable<Persoane>();
            Table<Raionull> raion = db.GetTable<Raionull>();
            Table<DenInst> institutia = db.GetTable<DenInst>();
            Table<Nationalit> nationalitatea = db.GetTable<Nationalit>();
            Table<Stud> studii = db.GetTable<Stud>();
            Table<FormCont> formare = db.GetTable<FormCont>();
            Table<Functii> functie = db.GetTable<Functii>();
            Table<Activ> activitati = db.GetTable<Activ>();
            Table<GrStiinte> grad = db.GetTable<GrStiinte>();

            Table<Localit> localitatea = db.GetTable<Localit>();
            Table<ProfDisc> prdis = db.GetTable<ProfDisc>();
            Table<Disciplin> dis = db.GetTable<Disciplin>();
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

            PersAfisGrid.ItemsSource = perslist.ToList();
            NrPers.Content = perslist.Count();

            DisciplinaText.Text = "";
            did1.IsChecked = false;
            did2.IsChecked = false;
            did3.IsChecked = false;
            did4.IsChecked = false;
            titlustcom.Text = "";
            Vechime.Text = "";
            Vechime2.Text = "";
            Stag.Text = "";
            Stag2.Text = "";
            masc.IsChecked = false;
            fem.IsChecked = false;
            cat.Text = "";

            telef.Text = "";
            posta.Text = "";
            fun.Text = "";
            var t = Form.Template.FindName("PART_TextBox", Form) as DatePickerTextBox;
            if (t != null)
                t.Text = "Select date";

            var t2 = Form2.Template.FindName("PART_TextBox", Form2) as DatePickerTextBox;
            if (t2 != null)
                t2.Text = "Select date";
        }

        private void Export_Click(object sender, RoutedEventArgs e)
        {
            if (PersAfisGrid.Items.Count != 0)
            {

                this.PersAfisGrid.SelectAllCells();
                this.PersAfisGrid.ClipboardCopyMode = DataGridClipboardCopyMode.IncludeHeader;
                ApplicationCommands.Copy.Execute(null, this.PersAfisGrid);

                String result = (string)Clipboard.GetData(DataFormats.Text);
                this.PersAfisGrid.UnselectAllCells();

                StreamWriter sw = new StreamWriter("export.xls");
                sw.WriteLine(result.Replace("01.01.1800", "").Replace("1/1/1800", ""));

                sw.Close();
                Process.Start("export.xls");
            }
            else
            {
                MessageBox.Show("Nu sunt date pentru export", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }


        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            var Result = MessageBox.Show("Doriți să ieșiți de pe account ?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (Result == MessageBoxResult.Yes)
            {

                File.WriteAllText(@"..\user.dll", "");
                MainWindow m = new MainWindow();
                m.Show();
                this.Hide();
            }
        }

        private void utiliz_Click(object sender, RoutedEventArgs e)
        {
            Regist a = new Regist();
            this.Close();
            a.Show();
        }

        private void Form_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Form2.SelectedDate != null && Form2.SelectedDate < Form.SelectedDate)
                Form2.SelectedDate = Form.SelectedDate.Value;
            if (Form.SelectedDate != null)
                Form2.DisplayDateStart = Form.SelectedDate.Value;


        }

        private void toRaport1_Click(object sender, RoutedEventArgs e)
        {
            RaportAuxiliar r = new RaportAuxiliar();
            r.Show();
            this.Close();
        }

        private void toRaport2_Click(object sender, RoutedEventArgs e)
        {
            RaportVirst a = new RaportVirst();
            a.Show();
            this.Close();
        }

        private void pane3Button_Click(object sender, RoutedEventArgs e)
        {
            if (pane3Button.Visibility == Visibility.Collapsed)
                UndockPane(3);
            else
                DockPane(3);
        }

        private void toRaport3_Click(object sender, RoutedEventArgs e)
        {
            RaportCatedre a = new RaportCatedre();
            a.Show();
            this.Close();
        }

        private void toRaport4_Click(object sender, RoutedEventArgs e)
        {
            RaportGradDid a = new RaportGradDid();
            a.Show();
            this.Close();
        }

        private void toRaport5_Click(object sender, RoutedEventArgs e)
        {
            RaportPersonal a = new RaportPersonal();
            a.Show();
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Raport4 a = new Raport4();
            a.Show();
            this.Close();
        }

        private void functii(object sender, RoutedEventArgs e)
        {
            ProfesorFunctie pf = new ProfesorFunctie();
            pf.Show();
            this.Close();
        }

        private void clear_Click(object sender, RoutedEventArgs e)
        {
            DataContext db = new DataContext(conn);
            Table<Persoane> persoana = db.GetTable<Persoane>();
            Table<Raionull> raion = db.GetTable<Raionull>();
            Table<DenInst> institutia = db.GetTable<DenInst>();
            Table<Nationalit> nationalitatea = db.GetTable<Nationalit>();
            Table<Stud> studii = db.GetTable<Stud>();
            Table<FormCont> formare = db.GetTable<FormCont>();
            Table<Functii> functie = db.GetTable<Functii>();
            Table<Activ> activitati = db.GetTable<Activ>();
            Table<GrStiinte> grad = db.GetTable<GrStiinte>();

            Table<Localit> localitatea = db.GetTable<Localit>();
            Table<ProfDisc> prdis = db.GetTable<ProfDisc>();
            Table<Disciplin> dis = db.GetTable<Disciplin>();
            Table<Catedre> catedra = db.GetTable<Catedre>();
            Table<GradulDidactic> grDidactic = db.GetTable<GradulDidactic>();
            var Result = MessageBox.Show("Doriți sa ștergeți toate datele ?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (Result == MessageBoxResult.Yes)
            {
                var a = db.GetTable<PersoanaFunc>();
                var profdis = db.GetTable<ProfDisc>();
                var frm1 = db.GetTable<FormCont>();
                var activ = db.GetTable<Activ>();
                var pers = db.GetTable<Persoane>();
                var cat = db.GetTable<Catedre>();
                var dein = db.GetTable<DenInst>();
                var dein1 = db.GetTable<Disciplin>();
                var catdel1 = db.GetTable<Functii>();
                var catdel2 = db.GetTable<GradulDidactic>();
                var catdel3 = db.GetTable<GrStiinte>();
                var catdel4 = db.GetTable<Localit>();
                var catdel5 = db.GetTable<Nationalit>();
                var catdel6 = db.GetTable<Raionull>();
                var catdel8 = db.GetTable<Stud>();

                db.GetTable<PersoanaFunc>().DeleteAllOnSubmit(a);
                db.GetTable<ProfDisc>().DeleteAllOnSubmit(profdis);
                db.GetTable<Activ>().DeleteAllOnSubmit(activ);
                db.GetTable<Persoane>().DeleteAllOnSubmit(pers);
                db.GetTable<Catedre>().DeleteAllOnSubmit(cat);
                db.GetTable<DenInst>().DeleteAllOnSubmit(dein);
                db.GetTable<Disciplin>().DeleteAllOnSubmit(dein1);
                db.GetTable<Functii>().DeleteAllOnSubmit(catdel1);
                db.GetTable<GradulDidactic>().DeleteAllOnSubmit(catdel2);
                db.GetTable<GrStiinte>().DeleteAllOnSubmit(catdel3);
                db.GetTable<Localit>().DeleteAllOnSubmit(catdel4);
                db.GetTable<Nationalit>().DeleteAllOnSubmit(catdel5);
                db.GetTable<Raionull>().DeleteAllOnSubmit(catdel6);
                db.GetTable<Stud>().DeleteAllOnSubmit(catdel8);

                db.SubmitChanges();


            }
        }
    }
}

