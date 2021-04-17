using HandyControl.Controls;
using MenuWithSubMenu.Model;
using MenuWithSubMenu.Utils;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
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
    /// <summary>
    /// Interaction logic for AddClient.xaml
    /// </summary>
    public partial class AddClient : Page
    {
        dbEntities db;
        DbContextTransaction transaction;
        private Page prevPage;
        private OpenFileDialog currentPhoto;

        public AddClient(Page prevP)
        {
            InitializeComponent();
            db = new dbEntities();

            currentPhoto = new OpenFileDialog();

            prevPage = prevP;

            // get ophtalmologues
            List<ophtalmologue> ophtalmologues = db.ophtalmologues.ToList();
            ophtalmologueText.ItemsSource = ophtalmologues;
            ophtalmologueText.DisplayMemberPath = "nom";
            ophtalmologueText.SelectedValuePath = "id";
        }

        private void addClient(object sender, RoutedEventArgs e)
        {
            if (!validateForm())
            {
                HandyControl.Controls.MessageBox.Show("Veuillez remplir tout les champs");
                return;
            }

            try
            {
                string medicaments = getMedicamentsString();
                string maladies = getMaladiesString();

                transaction = db.Database.BeginTransaction();
                db.clients.Add(new client()
                {
                    cin = cinText.Text,
                    nom = nomText.Text,
                    prenom = prenomText.Text,
                    adresse = adresseText.Text,
                    email = emailText.Text,
                    telephone = telText.Text,
                    dateNaissance = (DateTime)dateText.SelectedDate.Value.Date,
                    medicaments = medicaments,
                    maladies = maladies
                });
                db.SaveChanges();

                /* Ordonnance */

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

                //visite :
                visite new_visite = new visite()
                {
                    client_cin = cinText.Text,
                    date = DateTime.Now,
                    raison = raisonvisiteText.Text,
                    ordonnance_id = newOrdonnance.id,
                    ecart = (float?)ecartText.Value,
                    hauteur = (float?)hauteurText.Value
                };

                db.visites.Add(new_visite);
                db.SaveChanges();

                //vision :
                //Vision Loin & gauche:

                vision vision_Loins_gauche = new vision()
                {
                    add = (float?)og_add_loin.Value,
                    cyl = (float?)og_cyl_loin.Value,
                    axe = (float?)og_axe_loin.Value,
                    sph = (float?)og_sph_loin.Value,
                    gauche = true,
                    loin = true,
                    visite_id = new_visite.id
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
                    visite_id = new_visite.id
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
                    visite_id = new_visite.id
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
                    visite_id = new_visite.id
                };

                db.visions.Add(vision_pres_gauche);
                db.SaveChanges();
                

                transaction.Commit();

                MyContext.navigateTo(prevPage);
            }
            catch (Exception exc)
            {
                Console.Write(exc.Message);
                HandyControl.Controls.MessageBox.Show("Erreur");
                transaction.Rollback();

            }

        }

        // add medicaments
        private void enterAddMed(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                ajouterMedicament();
        }

        private void addMedButton(object sender, RoutedEventArgs e)
        {
            ajouterMedicament();
        }

        private void ajouterMedicament()
        {
            if (!String.IsNullOrEmpty(newMedicament.Text) && !String.IsNullOrWhiteSpace(newMedicament.Text))
            {
                StackPanel stackPanel = new StackPanel() { Orientation = System.Windows.Controls.Orientation.Horizontal, Margin = new Thickness(0, 0, 0, 5) };
                HandyControl.Controls.TextBox textBox = new HandyControl.Controls.TextBox() { Text = newMedicament.Text, Width = 200, Margin = new Thickness(0, 0, 10, 0) };
                System.Windows.Controls.Button deleteButton = new System.Windows.Controls.Button() { Content = "Supprimer" };
                deleteButton.Click += supprimerMedic;

                stackPanel.Children.Add(textBox);
                stackPanel.Children.Add(deleteButton);

                medicamentsBox.Children.Add(stackPanel);
                newMedicament.Text = "";
            }
        }

        private void supprimerMedic(object sender, RoutedEventArgs e)
        {
            medicamentsBox.Children.Remove((UIElement)(sender as FrameworkElement).Parent);
        }

        private string getMedicamentsString()
        {
            // check if medicaments enabled
            if (!(bool)medicamentsCheckbox.IsChecked)
                return null;

            // check if medicaments filled
            List<string> medicamentsList = new List<string>();

            foreach (StackPanel s in medicamentsBox.Children)
            {
                System.Windows.Controls.TextBox t = s.Children.OfType<System.Windows.Controls.TextBox>().FirstOrDefault();
                if (!String.IsNullOrEmpty(t.Text) && !String.IsNullOrWhiteSpace(t.Text))
                    medicamentsList.Add(t.Text);
            }

            if (medicamentsList.Count == 0)
                return null;

            // return medicaments string
            return String.Join("@", medicamentsList);
        }

        // add maladies
        private void enterAddMaladie(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                ajouterMaladie();
        }

        private void addMaladieButton(object sender, RoutedEventArgs e)
        {
            ajouterMaladie();
        }

        private void ajouterMaladie()
        {
            if (!String.IsNullOrEmpty(newMaladie.Text) && !String.IsNullOrWhiteSpace(newMaladie.Text))
            {
                StackPanel stackPanel = new StackPanel() { Orientation = System.Windows.Controls.Orientation.Horizontal, Margin = new Thickness(0, 0, 0, 5) };
                HandyControl.Controls.TextBox textBox = new HandyControl.Controls.TextBox() { Text = newMaladie.Text, Width = 200, Margin = new Thickness(0, 0, 10, 0) };
                System.Windows.Controls.Button deleteButton = new System.Windows.Controls.Button() { Content = "Supprimer" };
                deleteButton.Click += supprimerMaladie;

                stackPanel.Children.Add(textBox);
                stackPanel.Children.Add(deleteButton);

                maladiesBox.Children.Add(stackPanel);
                newMaladie.Text = "";
            }
        }

        private void supprimerMaladie(object sender, RoutedEventArgs e)
        {
            maladiesBox.Children.Remove((UIElement)(sender as FrameworkElement).Parent);
        }

        private string getMaladiesString()
        {
            // check if maladies enabled
            if (!(bool)maladiesCheckbox.IsChecked)
                return null;

            // check if maladies filled
            List<string> maladiesList = new List<string>();

            foreach (StackPanel s in maladiesBox.Children)
            {
                System.Windows.Controls.TextBox t = s.Children.OfType<System.Windows.Controls.TextBox>().FirstOrDefault();
                if (!String.IsNullOrEmpty(t.Text) && !String.IsNullOrWhiteSpace(t.Text))
                    maladiesList.Add(t.Text);
            }

            if (maladiesList.Count == 0)
                return null;

            // return maladies string
            return String.Join("@", maladiesList);
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
                dateText.SelectedDate != null &&
                raisonvisiteText.Text.Length != 0 &&

                ophtalmologueText.SelectedIndex != -1 &&
                photoOrdonnance.Source != null &&
                dateCreationOrdonnance.SelectedDate != null &&
                dateExpirationOrdonnance.SelectedDate != null &&
                visionLoins.Text.Length != 0 &&
                visionPres.Text.Length != 0 &&
                notesOphtalmologue.Text.Length != 0
                )
                return true;
            return false;
        }

        private void ReturnBtn_Click(object sender, RoutedEventArgs e)
        {
            MyContext.navigateTo(prevPage);
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

        private void Button_Prev(object sender, RoutedEventArgs e)
        {
            step.Prev();

            if (step.StepIndex == 1)
            {
                addClientBtn.Visibility = Visibility.Visible;
                infoOrdoGrid.Visibility = Visibility.Visible;
                infoPersoGrid.Visibility = Visibility.Collapsed;
            }
            else
            {
                addClientBtn.Visibility = Visibility.Hidden;
                infoOrdoGrid.Visibility = Visibility.Collapsed;
                infoPersoGrid.Visibility = Visibility.Visible;
            }
        }

        private void Button_Next(object sender, RoutedEventArgs e)
        {
            step.Next();

            if (step.StepIndex == 1)
            {
                addClientBtn.Visibility = Visibility.Visible;
                infoOrdoGrid.Visibility = Visibility.Visible;
                infoPersoGrid.Visibility = Visibility.Collapsed;
            } else
            {
                addClientBtn.Visibility = Visibility.Hidden;
                infoOrdoGrid.Visibility = Visibility.Collapsed;
                infoPersoGrid.Visibility = Visibility.Visible;
            }
        }

        private void showPhotoOrdonnance(object sender, RoutedEventArgs e)
        {
            new ImageBrowser(new Uri(photoOrdonnance.Source.ToString())).Show();
        }
    }
}
