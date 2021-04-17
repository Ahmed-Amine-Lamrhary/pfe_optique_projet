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
    public partial class AboutOrdonnance : Page
    {
        private dbEntities db;
        private ordonnance ordonnance;
        private Page prevPage;

        public AboutOrdonnance(int ordonnanceId, Page prevP)
        {
            InitializeComponent();

            db = new dbEntities();

            prevPage = prevP;

            ordonnance = db.ordonnances.Where(ordonnance => ordonnance.id == ordonnanceId).SingleOrDefault();

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

            dateCreationBox.Text = ordonnance.dateCreation.ToString();
            dateExpirationBox.Text = ordonnance.dateExpiration.ToString();

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

            ecartText.Value = (double)ordVisite.ecart;
            hauteurText.Value = (double)ordVisite.hauteur;
        }
        private void voirOphta(object sender, RoutedEventArgs e)
        {
            int ophtaId = ordonnance.ophtalmologueId;
            OphtaProfile ophtaProfile = new OphtaProfile(ophtaId, this);
            MyContext.navigateTo(ophtaProfile);
        }

        private void voirClient(object sender, RoutedEventArgs e)
        {
            string clientCin = db.visites.Where(v => v.ordonnance_id == ordonnance.id).Single().client_cin;
            ClientProfile clientProfile = new ClientProfile(clientCin, this);
            MyContext.navigateTo(clientProfile);
        }

        private void showPhotoOrdonnance(object sender, RoutedEventArgs e)
        {
            new ImageBrowser(new Uri(photoOrdonnance.Source.ToString())).Show();
        }

        private void returnBtn_Click(object sender, RoutedEventArgs e)
        {
            MyContext.navigateTo(prevPage);
        }
    }
}
