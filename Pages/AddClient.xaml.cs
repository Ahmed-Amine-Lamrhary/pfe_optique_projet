using MenuWithSubMenu.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
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
    /// <summary>
    /// Interaction logic for NewUser.xaml
    /// </summary>
    public partial class NewUser : Page
    {
        dbEntities db;
        DbContextTransaction transaction;
        public NewUser()
        {
            InitializeComponent();
            db = new dbEntities();
            //var data = from ophtalmologue in db.ophtalmologues select ophtalmologue;
            //ophtalmologueText.DataContext = data.ToList();
            //ophtalmologueText.DisplayMemberPath = "nom";
            //typeVerres.DataContext = db.typeverres.ToList();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!validateForm())
            {
                MessageBox.Show("Veuillez remplir tout les champs");
                return;
            }
            
            try
            {
                transaction = db.Database.BeginTransaction();
                db.clients.Add(new client()
                {
                    cin = cinText.Text,
                    nom = nomText.Text,
                    prenom = prenomText.Text,
                    adresse = adresseText.Text,
                    email = emailText.Text,
                    telephone = telText.Text,
                    dateNaissance = (DateTime)dateText.SelectedDate.Value.Date
                });
                db.SaveChanges();

                vision_pres vision_Pres = new vision_pres()
                {
                    od_add = int.Parse(od_add_pres.Text),
                    od_cylindre = int.Parse(od_cyl_pres.Text),
                    od_axe = int.Parse(od_axe_pres.Text),
                    od_sphere = int.Parse(od_sph_pres.Text),
                    og_add = int.Parse(og_add_pres.Text),
                    og_cylindre = int.Parse(og_cyl_pres.Text),
                    og_axe = int.Parse(og_axe_pres.Text),
                    og_sphère = int.Parse(og_sph_pres.Text)
                };

                db.vision_pres.Add(vision_Pres);
                db.SaveChanges();

                vision_loins vision_Loins = new vision_loins()
                {
                    od_add = int.Parse(od_add_loin.Text),
                    od_cylindre = int.Parse(od_cyl_loin.Text),
                    od_axe = int.Parse(od_axe_loin.Text),
                    od_sphere = int.Parse(od_sph_loin.Text),
                    og_add = int.Parse(og_add_loin.Text),
                    og_cylindre = int.Parse(og_cyl_loin.Text),
                    og_axe = int.Parse(og_axe_loin.Text),
                    og_sphère = int.Parse(og_sph_loin.Text)
                };

                db.vision_loins.Add(vision_Loins);
                db.SaveChanges();

                db.visites.Add(new visite()
                {
                    client_cin = cinText.Text,
                    date = DateTime.Now,
                    raison = raisonvisiteText.Text
                });
                db.SaveChanges();

                db.ordonnances.Add(new ordonnance()
                {
                    dateCreation = (DateTime)dateCreationOrdonnance.SelectedDate.Value.Date,
                    dateExpiration = (DateTime)dateExpirationOrdonnance.SelectedDate.Value.Date,
                    typeVerres = 1+"",
                    notes = notesOphtalmologue.Text,
                    photo = "/C:/Images",
                    ophtalmologue_ophtalmologueId = 1,
                    client_cin = cinText.Text,
                    vision_loins_id = vision_Loins.id,
                    vision_pres_id = vision_Pres.id
                });
                db.SaveChanges();
                transaction.Commit();
            }
            catch (Exception exc)
            {
                Console.Write(exc.Message);
                MessageBox.Show(exc.Message);
                transaction.Rollback();

            }

        }

        //validation
        private Boolean validateForm()
        {
            if (cinText.Text.Length != 0 &&
                nomText.Text.Length != 0 &&
                emailText.Text.Length != 0 &&
                adresseText.Text.Length != 0 &&
                prenomText.Text.Length != 0 &&
                telText.Text.Length != 0 &&

                ophtalmologueText.Text.Length != 0 &&
                visionLoins.Text.Length != 0 &&
                visionPres.Text.Length != 0 &&
                notesOphtalmologue.Text.Length != 0
                )
                return true;
            return false;
        }

    }
}
