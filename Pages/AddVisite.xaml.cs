using HandyControl.Controls;
using MenuWithSubMenu.Model;
using MenuWithSubMenu.Utils;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
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
        private Page prevPage;
        private OpenFileDialog currentPhoto;

        public AddVisite(string clientCin, Page prevP)
        {
            InitializeComponent();
            db = new dbEntities();
            listeVision = new List<vision>();
            this.clientCin = clientCin;
            this.prevPage = prevP;

            // get ophtalmologues
            List<ophtalmologue> ophtalmologues = db.ophtalmologues.Distinct().ToList();
            ophtalmologueText.ItemsSource = ophtalmologues;
            ophtalmologueText.DisplayMemberPath = "nom";
            ophtalmologueText.SelectedValuePath = "id";
        }
        private void saveVisite(object sender, RoutedEventArgs e)
        {
            if (!validateForm())
            {
                HandyControl.Controls.MessageBox.Show("Veuillez remplir tout les champs");
                return;
            }

            try
            {
                transaction = db.Database.BeginTransaction();

                // ordonnance image
                string fileNameToSave = DateTime.Now.ToString("yyyy-dd-M--HH-mm-ss") + System.IO.Path.GetExtension(currentPhoto.FileName);
                string rootPath = System.IO.Path.GetDirectoryName(System.IO.Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory()));
                string filePath = rootPath + @"\Data\ordonnances\" + fileNameToSave;

                File.Copy(currentPhoto.FileName, filePath);

                ordonnance newOrdonnance = new ordonnance()
                {
                    dateCreation = (DateTime)dateCreationOrdonnance.SelectedDate.Value.Date,
                    dateExpiration = (DateTime)dateExpirationOrdonnance.SelectedDate.Value.Date,
                    notes = notesOphtalmologue.Text,
                    photo = filePath,
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
                    ecart = (float?)ecartText.Value,
                    hauteur = (float?)hauteurText.Value,
                };
                db.visites.Add(newVisite);
                db.SaveChanges();

                //Vision Loin & gauche:

                vision vision_Loins_gauche = new vision()
                {
                    add = (float?)og_add_loin.Value,
                    cyl = (float?)og_cyl_loin.Value,
                    axe = (float?)og_axe_loin.Value,
                    sph = (float?)og_sph_loin.Value,
                    gauche = true,
                    loin = true,
                    visite_id = newVisite.id
                };

                db.visions.Add(vision_Loins_gauche);
                db.SaveChanges();

                //Vision Loin & droit:

                vision vision_Loins_droit = new vision()
                {
                    add = (float?)od_add_loin.Value,
                    cyl = (float?)od_cyl_loin.Value,
                    axe = (float?)od_axe_loin.Value,
                    sph = (float?)od_sph_loin.Value,
                    gauche = false,
                    loin = true,
                    visite_id = newVisite.id

                };

                db.visions.Add(vision_Loins_droit);
                db.SaveChanges();

                //Vision pres & droit:

                vision vision_pres_droit = new vision()
                {
                    add = (float?)od_add_pres.Value,
                    cyl = (float?)od_cyl_pres.Value,
                    axe = (float?)od_axe_pres.Value,
                    sph = (float?)od_sph_pres.Value,
                    gauche = false,
                    loin = false,
                    visite_id = newVisite.id

                };

                db.visions.Add(vision_pres_droit);
                db.SaveChanges();

                //Vision pres & gauche:

                vision vision_pres_gauche = new vision()
                {
                    add = (float?)og_add_pres.Value,
                    cyl = (float?)og_cyl_pres.Value,
                    axe = (float?)og_axe_pres.Value,
                    sph = (float?)og_sph_pres.Value,
                    gauche = true,
                    loin = false,
                    visite_id = newVisite.id

                };

                db.visions.Add(vision_pres_gauche);
                db.SaveChanges();

                transaction.Commit();

                HandyControl.Controls.MessageBox.Show("La visite a été ajouté");
                MyContext.navigateTo(new Visite());
            }
            catch (Exception exc)
            {
                Console.Write(exc.Message);
                HandyControl.Controls.MessageBox.Show("Erreur");
                transaction.Rollback();
            }
        }

        private void ReturnBtn_Click(object sender, RoutedEventArgs e)
        {
            MyContext.navigateTo(prevPage);
        }

        private void showPhotoOrdonnance(object sender, RoutedEventArgs e)
        {
            new ImageBrowser(new Uri(photoOrdonnance.Source.ToString())).Show();
        }

        // Upload photo of ordonnance
        private void ajouterPhotoOrdonnance(object sender, RoutedEventArgs e)
        {
            // open file dialog   
            OpenFileDialog open = new OpenFileDialog();
            // image filters  
            open.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp; *.png)|*.jpg; *.jpeg; *.gif; *.bmp; *.png";
            if (open.ShowDialog() == DialogResult.OK)
            {
                photoOrdonnance.Source = new BitmapImage(new Uri(open.FileName));
                currentPhoto = open;
            }
        }

        //validation
        private Boolean validateForm()
        {
            if (raisonVisite.Text.Length != 0 &&
                ophtalmologueText.SelectedIndex != -1 &&
                dateCreationOrdonnance.SelectedDate != null &&
                dateExpirationOrdonnance.SelectedDate != null &&
                visionLoins.Text.Length != 0 &&
                visionPres.Text.Length != 0 &&
                notesOphtalmologue.Text.Length != 0
                )
                return true;
            return false;
        }
    }
}
