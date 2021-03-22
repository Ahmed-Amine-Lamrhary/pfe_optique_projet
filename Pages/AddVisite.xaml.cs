using MenuWithSubMenu.Model;
using MenuWithSubMenu.Utils;
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
        private List<vision> listeVision;
        DbContextTransaction transaction;
        private string clientCin;

        public AddVisite(string clientCin)
        {
            InitializeComponent();
            db = new dbEntities();
            listeVision = new List<vision>();
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
        private void saveVisite(object sender, RoutedEventArgs e)
        {
            try
            {
                transaction = db.Database.BeginTransaction();

                ordonnance newOrdonnance = new ordonnance()
                {
                    dateCreation = (DateTime)dateCreationOrdonnance.SelectedDate.Value.Date,
                    dateExpiration = (DateTime)dateExpirationOrdonnance.SelectedDate.Value.Date,
                    notes = notesOphtalmologue.Text,
                    photo = "/C:/Images",
                    ophtalmologueId = (int)ophtalmologueText.SelectedValue,
                };
                db.ordonnances.Add(newOrdonnance);
                db.SaveChanges();

                visite newVisite = new visite()
                {
                    date = DateTime.Now,
                    raison = raisonVisite.Text,
                    client_cin = clientCin,
                    ordonnance_id = newOrdonnance.id,
                };
                db.visites.Add(newVisite);
                db.SaveChanges();

                //Vision Loin & gauche:

                vision vision_Loins_gauche = new vision()
                {
                    add = float.Parse(og_add_loin.Text),
                    cyl = float.Parse(og_cyl_loin.Text),
                    axe = float.Parse(og_axe_loin.Text),
                    sph = float.Parse(og_sph_loin.Text),
                    gauche = true,
                    loin = true,
                    ecart = float.Parse(ecartLoinText.Text),
                    hauteur = float.Parse(hauteurLoinText.Text),
                    visite_id = newVisite.id
                };

                db.visions.Add(vision_Loins_gauche);
                db.SaveChanges();

                //Vision Loin & droit:

                vision vision_Loins_droit = new vision()
                {
                    add = float.Parse(od_add_loin.Text),
                    cyl = float.Parse(od_cyl_loin.Text),
                    axe = float.Parse(od_axe_loin.Text),
                    sph = float.Parse(od_sph_loin.Text),
                    gauche = false,
                    loin = true,
                    ecart = float.Parse(ecartLoinText.Text),
                    hauteur = float.Parse(hauteurLoinText.Text),
                    visite_id = newVisite.id

                };

                db.visions.Add(vision_Loins_droit);
                db.SaveChanges();

                //Vision pres & droit:

                vision vision_pres_droit = new vision()
                {
                    add = float.Parse(od_add_pres.Text),
                    cyl = float.Parse(od_cyl_pres.Text),
                    axe = float.Parse(od_axe_pres.Text),
                    sph = float.Parse(od_sph_pres.Text),
                    gauche = false,
                    loin = false,
                    ecart = float.Parse(ecartPresText.Text),
                    hauteur = float.Parse(hauteurPresText.Text),
                    visite_id = newVisite.id

                };

                db.visions.Add(vision_pres_droit);
                db.SaveChanges();

                //Vision pres & gauche:

                vision vision_pres_gauche = new vision()
                {
                    add = float.Parse(og_add_pres.Text),
                    cyl = float.Parse(og_cyl_pres.Text),
                    axe = float.Parse(og_axe_pres.Text),
                    sph = float.Parse(og_sph_pres.Text),
                    gauche = true,
                    loin = false,
                    ecart = float.Parse(ecartPresText.Text),
                    hauteur = float.Parse(hauteurPresText.Text),
                    visite_id = newVisite.id

                };

                db.visions.Add(vision_pres_gauche);
                db.SaveChanges();

                transaction.Commit();
            }
            catch (Exception exc)
            {
                Console.Write(exc.Message);
                HandyControl.Controls.MessageBox.Show(exc.Message);
                transaction.Rollback();
            }
        }

        private void ReturnBtn_Click(object sender, RoutedEventArgs e)
        {
            MyContext.navigateTo(new EspaceClient());
        }
    }
}
