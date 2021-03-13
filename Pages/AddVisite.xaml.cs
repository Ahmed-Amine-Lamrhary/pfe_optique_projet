using MenuWithSubMenu.Model;
using MenuWithSubMenu.Utils;
using MenuWithSubMenu.Windows;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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

namespace MenuWithSubMenu.Pages
{

    public partial class AddVisite : Page
    {
        dbEntities db;
        private List<examan> listExamen;
        DbContextTransaction transaction;
        private string clientCin;

        public AddVisite(string clientCin)
        {
            InitializeComponent();
            db = new dbEntities();
            listExamen = new List<examan>();
            examList.ItemsSource = listExamen;
            this.clientCin = clientCin;

            // get ophtalmologues
            List<ophtalmologue> ophtalmologues = db.ophtalmologues.Distinct().ToList();
            ophtalmologueText.ItemsSource = ophtalmologues;
            ophtalmologueText.DisplayMemberPath = "nom";
            ophtalmologueText.SelectedValuePath = "id";

            // get types de verre
            List<typeverre> typesverre = db.typeverres.Distinct().ToList();
            typeVerres.ItemsSource = typesverre;
            typeVerres.DisplayMemberPath = "NomType";
            typeVerres.SelectedValuePath = "idTypeVerre";


        }
        private void addExam(object sender, RoutedEventArgs e)
        {
            AddExam newExam = new AddExam(this);
            newExam.Show();
        }
        private void saveVisite(object sender, RoutedEventArgs e)
        {
            try
            {
                transaction = db.Database.BeginTransaction();
                visite newVisite = new visite()
                {
                    date = DateTime.Now,
                    raison = rasonVissite.Text,
                    client_cin = clientCin
                };
                db.visites.Add(newVisite);
                db.SaveChanges();

                

                ordonnance newOrdonnance = new ordonnance()
                {
                    dateCreation = (DateTime)dateCreationOrdonnance.SelectedDate.Value.Date,
                    dateExpiration = (DateTime)dateExpirationOrdonnance.SelectedDate.Value.Date,
                    idTypeVerre = (int)typeVerres.SelectedValue,
                    notes = notesOphtalmologue.Text,
                    photo = "/C:/Images",
                    ophtalmologue_ophtalmologueId = (int)ophtalmologueText.SelectedValue,
                    client_cin = clientCin
                };
                db.ordonnances.Add(newOrdonnance);
                db.SaveChanges();

                vision vis_loins = new vision()
                {
                    ordonnance_id = newOrdonnance.id,
                    od_add = int.Parse(od_add_loin.Text),
                    od_cylindre = int.Parse(od_cyl_loin.Text),
                    od_axe = int.Parse(od_axe_loin.Text),
                    od_sphere = int.Parse(od_sph_loin.Text),
                    og_add = int.Parse(og_add_loin.Text),
                    og_cylindre = int.Parse(og_cyl_loin.Text),
                    og_axe = int.Parse(og_axe_loin.Text),
                    og_sphère = int.Parse(og_sph_loin.Text),
                    vision_type = "loins"
                };
                db.visions.Add(vis_loins);
                db.SaveChanges();

                vision vis_pres = new vision()
                {
                    ordonnance_id = newOrdonnance.id,
                    od_add = int.Parse(od_add_pres.Text),
                    od_cylindre = int.Parse(od_cyl_pres.Text),
                    od_axe = int.Parse(od_axe_pres.Text),
                    od_sphere = int.Parse(od_sph_pres.Text),
                    og_add = int.Parse(og_add_pres.Text),
                    og_cylindre = int.Parse(og_cyl_pres.Text),
                    og_axe = int.Parse(og_axe_pres.Text),
                    og_sphère = int.Parse(og_sph_pres.Text),
                    vision_type = "pres"
                };
                db.visions.Add(vis_pres);
                db.SaveChanges();


                foreach (examan ex in listExamen)
                {
                    ex.visite_id = newVisite.id;

                    db.examen.Add(ex);
                    db.SaveChanges();
                }

                transaction.Commit();
            }
            catch (Exception exc)
            {
                Console.Write(exc.Message);
                HandyControl.Controls.MessageBox.Show(exc.Message);
                transaction.Rollback();
            }
        }
        public void addExamToList(examan exam)
        {
            listExamen.Add(exam);
            examList.Items.Refresh();
        }
        private void ReturnBtn_Click(object sender, RoutedEventArgs e)
        {
            MyContext.navigateTo(new EspaceClient());
        }
    }
}
