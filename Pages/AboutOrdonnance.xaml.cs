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

            visite newVisiste = db.visites.Where(visite => visite.ordonnance_id == ordonnance.id).SingleOrDefault();

            // vision loins & gauche
            vision visionLoinsGauche = db.visions.Where(v => v.visite_id == newVisiste.id && v.gauche == true && v.loin == true).SingleOrDefault();

            // vision pres & gauche
            vision visionPresGauche = db.visions.Where(v => v.visite_id == newVisiste.id && v.gauche == true && v.loin == false).SingleOrDefault();

            // vision pres & droit
            vision visionPresDroit = db.visions.Where(v => v.visite_id == newVisiste.id && v.gauche == false && v.loin == false).SingleOrDefault();

            // vision loin & droit
            vision visionLoinDroit = db.visions.Where(v => v.visite_id == newVisiste.id && v.gauche == false && v.loin == true).SingleOrDefault();


            dateCreationBox.Text = ordonnance.dateCreation.ToString();
            dateExpirationBox.Text = ordonnance.dateExpiration.ToString();

            // loins
            od_sph_loin.Text = visionLoinDroit.sph.ToString();
            od_cyl_loin.Text = visionLoinDroit.cyl.ToString();
            od_axe_loin.Text = visionLoinDroit.axe.ToString();
            od_add_loin.Text = visionLoinDroit.add.ToString();

            og_sph_loin.Text = visionLoinsGauche.sph.ToString();
            og_cyl_loin.Text = visionLoinsGauche.cyl.ToString();
            og_axe_loin.Text = visionLoinsGauche.axe.ToString();
            og_add_loin.Text = visionLoinsGauche.add.ToString();


            // près
            od_sph_pres.Text = visionPresDroit.sph.ToString();
            od_cyl_pres.Text = visionPresDroit.cyl.ToString();
            od_axe_pres.Text = visionPresDroit.axe.ToString();
            od_add_pres.Text = visionPresDroit.add.ToString();

            og_sph_pres.Text = visionPresGauche.sph.ToString();
            og_cyl_pres.Text = visionPresGauche.cyl.ToString();
            og_axe_pres.Text = visionPresGauche.axe.ToString();
            og_add_pres.Text = visionPresGauche.add.ToString();
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
    }
}
