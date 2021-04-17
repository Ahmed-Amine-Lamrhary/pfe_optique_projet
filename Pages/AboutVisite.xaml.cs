using HandyControl.Controls;
using MenuWithSubMenu.Model;
using MenuWithSubMenu.Utils;
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

namespace MenuWithSubMenu.Pages
{
    /// <summary>
    /// Interaction logic for AboutVisite.xaml
    /// </summary>
    public partial class AboutVisite : Page
    {
        private dbEntities db;
        private Page prevPage;

        private visite visite;
        private List<vision> visions;
        private client client;
        private ordonnance ordonnance;

        public AboutVisite(int visiteId, Page prevP)
        {
            InitializeComponent();
            db = new dbEntities();
            prevPage = prevP;

            getInformation(visiteId);
        }

        private async Task getInformation(int visiteId)
        {
            // get visite
            visite = await Task.Run(() => db.visites.Where(visite => visite.id == visiteId).SingleOrDefault());
            // get ordonnance
            ordonnance = await Task.Run(() => db.ordonnances.Where(o => o.id == visite.ordonnance_id).SingleOrDefault());
            // get visions
            visions = await Task.Run(() => db.visions.Where(v => v.visite_id == visiteId).ToList());
            // get client
            client = await Task.Run(() => db.clients.Where(c => c.cin == visite.client_cin).SingleOrDefault());

            // visite info
            raisonVisite.Text = visite.raison;
            // client info
            cinText.Text = client.cin;
            nomText.Text = client.nom;
            prenomText.Text = client.prenom;
            adresseText.Text = client.adresse;
            telText.Text = client.telephone;
            emailText.Text = client.email;
            dateText.Text = client.dateNaissance + "";
            // ordonnance info
            photoOrdonnance.Source = new BitmapImage(new Uri(ordonnance.photo));

            visite ordVisite = db.visites.Where(visite => visite.ordonnance_id == ordonnance.id).SingleOrDefault();
            // vision loins & gauche
            vision visionLoinsGauche = db.visions.Where(v => v.visite_id == ordVisite.id && v.gauche == true && v.loin == true).SingleOrDefault();
            // vision pres & gauche
            vision visionPresGauche = db.visions.Where(v => v.visite_id == ordVisite.id && v.gauche == true && v.loin == false).SingleOrDefault();
            // vision pres & droit
            vision visionPresDroit = db.visions.Where(v => v.visite_id == ordVisite.id && v.gauche == false && v.loin == false).SingleOrDefault();
            // vision loin & droit
            vision visionLoinDroit = db.visions.Where(v => v.visite_id == ordVisite.id && v.gauche == false && v.loin == true).SingleOrDefault();

            dateCreationOrdonnance.Text = ordonnance.dateCreation.ToString();
            dateExpirationOrdonnance.Text = ordonnance.dateExpiration.ToString();

            // loins
            od_sph_loin.Value = (double)visionLoinDroit.sph;
            od_cyl_loin.Value = (double)visionLoinDroit.cyl;
            od_axe_loin.Value = (double)visionLoinDroit.axe;
            od_add_loin.Value = (double)visionLoinDroit.add;

            og_sph_loin.Value = (double)visionLoinsGauche.sph;
            og_cyl_loin.Value = (double)visionLoinsGauche.cyl;
            og_axe_loin.Value = (double)visionLoinsGauche.axe;
            og_add_loin.Value = (double)visionLoinsGauche.add;


            // près
            od_sph_pres.Value = (double)visionPresDroit.sph;
            od_cyl_pres.Value = (double)visionPresDroit.cyl;
            od_axe_pres.Value = (double)visionPresDroit.axe;
            od_add_pres.Value = (double)visionPresDroit.add;

            og_sph_pres.Value = (double)visionPresGauche.sph;
            og_cyl_pres.Value = (double)visionPresGauche.cyl;
            og_axe_pres.Value = (double)visionPresGauche.axe;
            og_add_pres.Value = (double)visionPresGauche.add;


            ecartText.Value = (double)visite.ecart;
            hauteurText.Value = (double)visite.hauteur;
        }

        private void showPhotoOrdonnance(object sender, RoutedEventArgs e)
        {
            new ImageBrowser(new Uri(photoOrdonnance.Source.ToString())).Show();
        }

        private void voirOphta(object sender, RoutedEventArgs e)
        {
            OphtaProfile ophtaProfile = new OphtaProfile(ordonnance.ophtalmologueId, this);
            MyContext.navigateTo(ophtaProfile);
        }

        private void returnBtn_Click(object sender, RoutedEventArgs e)
        {
            MyContext.navigateTo(prevPage);
        }
    }
}
