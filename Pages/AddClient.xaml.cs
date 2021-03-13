using HandyControl.Controls;
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
    /// Interaction logic for AddClient.xaml
    /// </summary>
    public partial class AddClient : Page
    {
        dbEntities db;
        DbContextTransaction transaction;
        
        public AddClient()
        {
            InitializeComponent();
            db = new dbEntities();

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

                ordonnance newOrdonnance = new ordonnance()
                {
                    dateCreation = (DateTime)dateCreationOrdonnance.SelectedDate.Value.Date,
                    dateExpiration = (DateTime)dateExpirationOrdonnance.SelectedDate.Value.Date,
                    idTypeVerre = (int)typeVerres.SelectedValue,
                    notes = notesOphtalmologue.Text,
                    photo = "/C:/Images",
                    ophtalmologue_ophtalmologueId = (int)ophtalmologueText.SelectedValue,
                    client_cin = cinText.Text
                };

                db.ordonnances.Add(newOrdonnance);
                db.SaveChanges();

                vision vision_Pres = new vision()
                {
                    od_add = int.Parse(od_add_pres.Text),
                    od_cylindre = int.Parse(od_cyl_pres.Text),
                    od_axe = int.Parse(od_axe_pres.Text),
                    od_sphere = int.Parse(od_sph_pres.Text),
                    og_add = int.Parse(og_add_pres.Text),
                    og_cylindre = int.Parse(og_cyl_pres.Text),
                    og_axe = int.Parse(og_axe_pres.Text),
                    og_sphère = int.Parse(og_sph_pres.Text),
                    vision_type = "pres",
                    ordonnance_id = newOrdonnance.id
                };

                db.visions.Add(vision_Pres);
                db.SaveChanges();

                vision vision_Loins = new vision()
                {
                    od_add = int.Parse(od_add_loin.Text),
                    od_cylindre = int.Parse(od_cyl_loin.Text),
                    od_axe = int.Parse(od_axe_loin.Text),
                    od_sphere = int.Parse(od_sph_loin.Text),
                    og_add = int.Parse(og_add_loin.Text),
                    og_cylindre = int.Parse(og_cyl_loin.Text),
                    og_axe = int.Parse(og_axe_loin.Text),
                    og_sphère = int.Parse(og_sph_loin.Text),
                    vision_type = "loins",
                    ordonnance_id = newOrdonnance.id
                };

                db.visions.Add(vision_Loins);
                db.SaveChanges();

                db.visites.Add(new visite()
                {
                    client_cin = cinText.Text,
                    date = DateTime.Now,
                    raison = raisonvisiteText.Text,
                });
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
                StackPanel stackPanel = new StackPanel() { Orientation = System.Windows.Controls.Orientation.Horizontal };
                System.Windows.Controls.TextBox textBox = new System.Windows.Controls.TextBox() { Text = newMedicament.Text, Width = 200 };
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
                StackPanel stackPanel = new StackPanel() { Orientation = System.Windows.Controls.Orientation.Horizontal };
                System.Windows.Controls.TextBox textBox = new System.Windows.Controls.TextBox() { Text = newMaladie.Text, Width = 200 };
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
