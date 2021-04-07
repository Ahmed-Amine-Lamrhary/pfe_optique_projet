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

namespace MenuWithSubMenu.PagesStock
{
    public partial class AddLigneCmdClient : Page
    {
        dbEntities db;
        private Page prevPage;
        private AddCmdClient addCmdPage;
        private List<traitement> traitements;
        private List<article> references;
        private article selectedReference;
        public ligneentree ligne;
        public bool isUpdate;

        public List<vision> visions;
        public visite lastClientVisite;

        // execute in both consturctors
        private void getVisions()
        {
            visions = db.visions.Where(v => v.visite_id == lastClientVisite.id).ToList();

            // show visions
            foreach(vision v in visions)
            {
                // pres
                if (!v.loin)
                {
                    // droite
                    if (!v.gauche)
                    {
                        od_sph_pres.Value = (double)v.sph;
                        od_axe_pres.Value = (double)v.axe;
                        od_add_pres.Value = (double)v.add;
                        od_cyl_pres.Value = (double)v.cyl;
                    }
                    // gauche
                    else
                    {
                        og_sph_pres.Value = (double)v.sph;
                        og_axe_pres.Value = (double)v.axe;
                        og_add_pres.Value = (double)v.add;
                        og_cyl_pres.Value = (double)v.cyl;
                    }

                    // hauteur et ecart
                    ecartLoinText.Value = (double)v.ecart;
                    hauteurLoinText.Value = (double)v.hauteur;

                } else
                {
                    // droite
                    if (!v.gauche)
                    {
                        od_sph_loin.Value = (double)v.sph;
                        od_axe_loin.Value = (double)v.axe;
                        od_add_loin.Value = (double)v.add;
                        od_cyl_loin.Value = (double)v.cyl;
                    }
                    // gauche
                    else
                    {
                        og_sph_loin.Value = (double)v.sph;
                        og_axe_loin.Value = (double)v.axe;
                        og_add_loin.Value = (double)v.add;
                        og_cyl_loin.Value = (double)v.cyl;
                    }

                    // hauteur et ecart
                    ecartPresText.Value = (double)v.ecart;
                    hauteurPresText.Value = (double)v.hauteur;
                }
            }
        }

        // update constructor
        public AddLigneCmdClient(Page prevP, AddCmdClient addCmdP, ligneentree ligne)
        {
            InitializeComponent();

            db = new dbEntities();
            prevPage = prevP;
            addCmdPage = addCmdP;
            references = new List<article>();
            this.ligne = ligne;
            this.isUpdate = true;

            fillComboBox();
        }

        // create constructor
        public AddLigneCmdClient(Page prevP, AddCmdClient addCmdP, visite lastClientVisite)
        {
            InitializeComponent();

            db = new dbEntities();
            prevPage = prevP;
            addCmdPage = addCmdP;
            references = new List<article>();
            this.isUpdate = false;
            this.lastClientVisite = lastClientVisite;

            fillComboBox();

            getVisions();
        }

        public void selectItem(object sender, SelectionChangedEventArgs e)
        {
            int selectedIemValue = (int)categorie.SelectedValue;
            switch (selectedIemValue)
            {
                case 1:
                    verrePanel.Visibility = Visibility.Visible;
                    cadrePanel.Visibility = Visibility.Collapsed;
                    referenceBox.Visibility = Visibility.Collapsed;
                    articleInfoBox.Visibility = Visibility.Collapsed;
                    additionalInfoBox.IsEnabled = true;
                    break;

                case 2:
                    verrePanel.Visibility = Visibility.Collapsed;
                    cadrePanel.Visibility = Visibility.Visible;
                    referenceBox.Visibility = Visibility.Visible;
                    articleInfoBox.Visibility = Visibility.Visible;
                    additionalInfoBox.IsEnabled = false;
                    break;

                case 3:
                    verrePanel.Visibility = Visibility.Collapsed;
                    cadrePanel.Visibility = Visibility.Visible;
                    referenceBox.Visibility = Visibility.Visible;
                    articleInfoBox.Visibility = Visibility.Visible;
                    additionalInfoBox.IsEnabled = false;
                    break;
            }
            updateReferenceCombo();
        }

        public void fillVisionsVerre(List<vision> visions)
        { /*
            foreach (vision vision in visions)
            {
                if (vision.loin)
                {
                    if (vision.gauche)
                    {
                        og_sph_loin_verre.Text = vision.sph.ToString();
                        og_cyl_loin_verre.Text = vision.cyl.ToString();
                        og_axe_loin_verre.Text = vision.axe.ToString();
                        og_add_loin_verre.Text = vision.add.ToString();
                    }
                    else
                    {
                        od_sph_loin_verre.Text = vision.sph.ToString();
                        od_cyl_loin_verre.Text = vision.cyl.ToString();
                        od_axe_loin_verre.Text = vision.axe.ToString();
                        od_add_loin_verre.Text = vision.add.ToString();
                    }
                    ecartLoinText_verre.Text = vision.ecart.ToString();
                    hauteurLoinText_verre.Text = vision.hauteur.ToString();
                }
                else
                {
                    if (vision.gauche)
                    {
                        og_sph_pres_verre.Text = vision.sph.ToString();
                        og_cyl_pres_verre.Text = vision.cyl.ToString();
                        og_axe_pres_verre.Text = vision.axe.ToString();
                        og_add_pres_verre.Text = vision.add.ToString();
                    }
                    else
                    {
                        od_sph_pres_verre.Text = vision.sph.ToString();
                        od_cyl_pres_verre.Text = vision.cyl.ToString();
                        od_axe_pres_verre.Text = vision.axe.ToString();
                        od_add_pres_verre.Text = vision.add.ToString();
                    }
                    ecartPresText_verre.Text = vision.ecart.ToString();
                    hauteurPresText_verre.Text = vision.hauteur.ToString();
                }
            }*/
        }


        public void selectReference(object sender, SelectionChangedEventArgs e)
        {
            string idReference = (string)referenceText.SelectedValue;
            article reference = db.articles.Where(article => article.idArticle == idReference).SingleOrDefault();

            if (reference != null)
            {
                selectedReference = reference;

                // reference information
                prixText.Text = reference.PrixUnitaire.ToString();
                garantieText.Text = reference.Garantie;
                descText.Text = reference.Description;
                /*marqueText.Text = ;
                modelText.Text = ;*/

                // int selectedCategorieId = (int)categorie.SelectedValue;
                cadre cadre = db.cadres.Where(c => c.idArticle == reference.idArticle).SingleOrDefault();
                diametreText.Text = cadre.DiametreVerre.ToString();
                pontText.Text = cadre.Pont.ToString();
                langeur_brance_text.Text = cadre.LongeurBrache.ToString();
                largeurText.Text = cadre.Largeur.ToString();
                hautteur_verre_text.Text = cadre.HauteurVerre.ToString();
                couleurText.Text = cadre.Couleur;
            }
        }

        private void fillComboBox()
        {
            List<categorie> categories = db.categories.Distinct().ToList();
            categorie.ItemsSource = categories;
            categorie.DisplayMemberPath = "Nom";
            categorie.SelectedValuePath = "idCategorie";

            categorie.SelectedIndex = 0;

            updateReferenceCombo();

            traitements = db.traitements.Distinct().ToList();
            newTraitementNom.ItemsSource = traitements;
            newTraitementNom.DisplayMemberPath = "Nom";
            newTraitementNom.SelectedValuePath = "idTraitement";

            // matières
            matiereVerresText.Items.Add("Minérale");
            matiereVerresText.Items.Add("Polycarbonate");
            matiereVerresText.Items.Add("Organique");

            // géométries
            geometrieVerresText.Items.Add("Verres progressives");

            // teinte
            teintText.Items.Add("Blanc");
            teintText.Items.Add("Solaire");
            teintText.Items.Add("Sport");
            teintText.Items.Add("Spéciales");
            teintText.Items.Add("Effet mode");
            teintText.Items.Add("Variable");

        }

        public void updateReferenceCombo()
        {
            //Références Combobox :
            references.Clear();
            foreach (article a in db.articles.ToList())
            {
                if (a.idCategorie == (int)categorie.SelectedValue)
                    references.Add(a);

            }

            referenceText.ItemsSource = null;
            referenceText.ItemsSource = references;
            referenceText.DisplayMemberPath = "idArticle";
            referenceText.SelectedValuePath = "idArticle";
            referenceText.SelectedIndex = 0;
        }

        public void addTraitVerreButton(object sender, RoutedEventArgs e)
        {
            addTraitVerre(newTraitementNiveau.Text, newTraitementNom.DisplayMemberPath);
        }

        public void addTraitVerre(string niveau, string nom)
        {
            // coutTrait++;
            // if (coutTrait >= maxTrait - 1)
            //     return;
            StackPanel stackPanel = new StackPanel() { Orientation = System.Windows.Controls.Orientation.Horizontal };

            // ComboBox newCombobox = new ComboBox() { Width = 100 };
            TextBlock newTextBlock = new TextBlock() { Text = nom, IsEnabled = false };
            // newCombobox.ItemsSource = traitements;
            // newCombobox.DisplayMemberPath = "Nom";
            // newCombobox.SelectedValuePath = "idTraitement";
            // newCombobox.SelectedIndex = selectedIndex;
            TextBox newTextBox = new TextBox() { Width = 100, Text = niveau };
            Button supprimerBtn = new Button() { Content = "Supprimer" };
            supprimerBtn.Click += supprimerTraitementVerre;

            stackPanel.Children.Add(newTextBlock);
            stackPanel.Children.Add(newTextBox);
            stackPanel.Children.Add(supprimerBtn);

            traitementsBox.Children.Add(stackPanel);
        }

        private void supprimerTraitementVerre(object sender, RoutedEventArgs e)
        {
            traitementsBox.Children.Remove((UIElement)(sender as FrameworkElement).Parent);
        }

        private void ReturnBtn_Click(object sender, RoutedEventArgs e)
        {
            MyContext.navigateTo(prevPage);
        }

        // abort ligne
        public void deleteLigne(object sender, RoutedEventArgs e)
        {
            addCmdPage.removeLigneFromList(this);
            MyContext.navigateTo(prevPage);
        }

        //getId Of selected traitement :


        // save ligne
        public void saveLigne(object sender, RoutedEventArgs e)
        {
            if (selectedReference != null && selectedReference.QteDisponible < int.Parse(qteText.Text))
            {
                MessageBox.Show("Quantité non disponible");
                return;
            }


            if (!addCmdPage.lignesCmd.Contains(this))
                addCmdPage.addNewLigneToList(this);
            else
            {
                addCmdPage.updateLigneInList(this);
            }
            MyContext.navigateTo(prevPage);
        }


        public List<traitement> get_traitements()
        {
            return traitements;
        }

        private bool referenceIsExiste(string reference)
        {
            foreach (article a in db.articles.ToList())
            {
                if (a.idArticle.Equals(reference))
                    return true;
            }
            return false;
        }

        private void matiereVerresText_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string matiere = (string)matiereVerresText.SelectedValue;
            indiceVerresText.Items.Clear();

            switch(matiere)
            {
                case "Minérale":
                    indiceVerresText.Items.Add(1.80);
                    indiceVerresText.Items.Add(1.90);
                    break;
                case "Polycarbonate":
                    indiceVerresText.Items.Add(1.59);
                    break;
                case "Organique":
                    indiceVerresText.Items.Add(1.50);
                    indiceVerresText.Items.Add(1.60);
                    indiceVerresText.Items.Add(1.67);
                    indiceVerresText.Items.Add(1.71);
                    indiceVerresText.Items.Add(1.74);
                    break;
            }
        }

        /*public List<traitement> Get_traitements_selectionee()
        {
           foreach (StackPanel stack in traitementsLentilleBox.Children.OfType<StackPanel>())
           {

               foreach(ComboBox c in stack.Children.OfType<ComboBox>)
               {
                   traitementsSelectiones.Add(new traitement()
                   {
                       idTraitement = (int)c.SelectedValue,
                   });
               }

           }
           return traitementsSelectiones;
        }*/

    }
}
