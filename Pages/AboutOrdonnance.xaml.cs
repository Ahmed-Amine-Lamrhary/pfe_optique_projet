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

            // type verres
            typeverre typeverre = db.typeverres.Where(t => t.idTypeVerre == ordonnance.idTypeVerre).SingleOrDefault();

            // vision loins
            vision visionLoins = db.visions.Where(v => v.ordonnance_id == ordonnance.id && v.vision_type == "loins").SingleOrDefault();

            // vision pres
            vision visionPres = db.visions.Where(v => v.ordonnance_id == ordonnance.id && v.vision_type == "pres").SingleOrDefault();

            dateCreationBox.Text = ordonnance.dateCreation.ToString();
            dateExpirationBox.Text = ordonnance.dateExpiration.ToString();
            typeVerresBox.Text = typeverre.NomType;

            // loins
            od_sph_loin.Text = visionLoins.od_sphere.ToString();
            od_cyl_loin.Text = visionLoins.od_cylindre.ToString();
            od_axe_loin.Text = visionLoins.od_axe.ToString();
            od_add_loin.Text = visionLoins.od_add.ToString();

            og_sph_loin.Text = visionLoins.og_sphère.ToString();
            og_cyl_loin.Text = visionLoins.og_cylindre.ToString();
            og_axe_loin.Text = visionLoins.og_axe.ToString();
            og_add_loin.Text = visionLoins.og_add.ToString();


            // près
            od_sph_pres.Text = visionPres.od_sphere.ToString();
            od_cyl_pres.Text = visionPres.od_cylindre.ToString();
            od_axe_pres.Text = visionPres.od_axe.ToString();
            od_add_pres.Text = visionPres.od_add.ToString();

            og_sph_pres.Text = visionPres.og_sphère.ToString();
            og_cyl_pres.Text = visionPres.og_cylindre.ToString();
            og_axe_pres.Text = visionPres.og_axe.ToString();
            og_add_pres.Text = visionPres.og_add.ToString();
        }
        private void voirOphta(object sender, RoutedEventArgs e)
        {
            int ophtaId = ordonnance.ophtalmologue_ophtalmologueId;

            OphtaProfile ophtaProfile = new OphtaProfile(ophtaId, this);

            MyContext.navigateTo(ophtaProfile);
        }

        private void voirClient(object sender, RoutedEventArgs e)
        {
            string clientCin = ordonnance.client_cin;

            ClientProfile clientProfile = new ClientProfile(clientCin, this);

            MyContext.navigateTo(clientProfile);
        }
    }
}
