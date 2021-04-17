using MenuWithSubMenu.Model;
using MenuWithSubMenu.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        public visite selectedVisite;

        public List<vision> visions;

        public string client_cin;

        // execute in both consturctors
        private void getVisions(visite visite)
        {
            visions = db.visions.Where(v => v.visite_id == visite.id).ToList();

            // hauteur et ecart
            ecartText.Value = (double)visite.ecart;
            hauteurText.Value = (double)visite.hauteur;

            // show visions
            foreach (vision v in visions)
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
                }
            }
        }

        // update constructor
        public AddLigneCmdClient(Page prevP, ligneentree ligne)
        {
            InitializeComponent();

            db = new dbEntities();
            prevPage = prevP;
            references = new List<article>();
            this.ligne = ligne;
            this.isUpdate = true;
            fillComboBox();

            visiteStack.Visibility = Visibility.Collapsed;
            
            visite visite = db.visites.Where(v => v.id == ligne.idVisite).SingleOrDefault();

            selectedVisite = visite;

            updateBoxBtns.Visibility = Visibility.Collapsed;
            mainStackPanel.IsEnabled = false;

            /////// fill the information

            // set quantité de commande
            qteText.Text = ligne.Qte_Commande + "";

            // verre
            if (ligne.idArticle == null)
            {
                verre verre = db.verres.Where(v => v.idVerre == ligne.idVerre).SingleOrDefault();

                // set categorie
                categorie.SelectedValue = 1;
                verrePanel.Visibility = Visibility.Visible;
                cadrePanel.Visibility = Visibility.Collapsed;
                referenceBox.Visibility = Visibility.Collapsed;
                articleInfoBox.Visibility = Visibility.Collapsed;
                additionalInfoBox.IsEnabled = true;

                // set matiere
                matiereVerresText.Items.Add(verre.matiere);
                matiereVerresText.SelectedIndex = matiereVerresText.Items.Count - 1;

                // set indice
                indiceVerresText.Items.Add(verre.indice + "");
                indiceVerresText.SelectedIndex = indiceVerresText.Items.Count - 1;

                // set geometrie
                geometrieVerresText.Items.Add(verre.geometrie);
                geometrieVerresText.SelectedIndex = geometrieVerresText.Items.Count - 1;

                // set teinte
                teintText.Items.Add(verre.Teinte);
                teintText.SelectedIndex = teintText.Items.Count - 1;

                // set prix
                verrePrix.Text = (ligne.Prix_Total / ligne.Qte_Commande) + "";

                // set traitements
                List<ligne_traitement_verre> traiVerres = db.ligne_traitement_verre.Where(l => l.verre_idVerre == verre.idVerre).ToList();
                if (traiVerres.Count == 0)
                    allTraitBox.Visibility = Visibility.Collapsed;
                else
                {
                    foreach (ligne_traitement_verre l in traiVerres)
                    {
                        traitement traitement = db.traitements.Where(t => t.idTraitement == l.traitement_idTraitement).SingleOrDefault();

                        StackPanel stackPanel = new StackPanel() { Orientation = Orientation.Horizontal, Margin = new Thickness(0, 0, 0, 5) };
                        HandyControl.Controls.TextBox nomBox = new HandyControl.Controls.TextBox() { Width = 200, Text = traitement.Nom, Margin = new Thickness(0, 0, 10, 0) };
                        HandyControl.Controls.TextBox niveauBox = new HandyControl.Controls.TextBox() { Width = 100, Text = l.niveau + "", Margin = new Thickness(0, 0, 10, 0) };
                        stackPanel.Children.Add(nomBox);
                        stackPanel.Children.Add(niveauBox);
                        traitementsBox.Children.Add(stackPanel);
                    }
                }
                traitementsCheckbox.Visibility = Visibility.Collapsed;
                addTraitBox.Visibility = Visibility.Collapsed;

                // set visions
                getVisions(visite);
            } else
            {
                article article = db.articles.Where(a => a.idArticle == ligne.idArticle).SingleOrDefault();

                // set categorie
                categorie.SelectedValue = article.idCategorie;

                verrePanel.Visibility = Visibility.Collapsed;
                cadrePanel.Visibility = Visibility.Visible;
                referenceBox.Visibility = Visibility.Visible;
                articleInfoBox.Visibility = Visibility.Visible;
                additionalInfoBox.IsEnabled = false;

                // set reference
                // referenceText.ItemsSource = new ObservableCollection<string>() { article.idArticle };
                // referenceText.Items.Add(article.idArticle);
                // referenceText.SelectedIndex = referenceText.Items.Count - 1;

                // set prix unitaire
                prixText.Text = article.PrixUnitaire + "";

                // set garantie
                garantieText.Text = article.Garantie;

                // set marque
                marqueText.Text = "";

                // set description
                descText.Text = article.Description;

                // set modele
                modelText.Text = "";

                // set infos cadre
                cadre cadre = db.cadres.Where(c => c.idArticle == article.idArticle).SingleOrDefault();

                diametreText.Text = cadre.DiametreVerre + "";
                pontText.Text = cadre.Pont + "";
                langeur_brance_text.Text = cadre.LongeurBrache + "";
                largeurText.Text = cadre.Largeur + "";
                hautteur_verre_text.Text = cadre.HauteurVerre + "";
                couleurText.Text = cadre.Couleur;
            }
        }

        // create constructor
        public AddLigneCmdClient(Page prevP, AddCmdClient addCmdP, visite lastClientVisite, string client_cin)
        {
            InitializeComponent();

            db = new dbEntities();
            prevPage = prevP;
            addCmdPage = addCmdP;
            references = new List<article>();
            isUpdate = false;

            selectedVisite = lastClientVisite;
            this.client_cin = client_cin;

            fillComboBox();

            getVisions(lastClientVisite);
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

            if (!isUpdate)
            {
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

                // visites
                List<visite> visites = db.visites.Where(v => v.client_cin == client_cin).ToList();
                visiteList.ItemsSource = visites;
                visiteList.DisplayMemberPath = "date";
                visiteList.SelectedValuePath = "id";
            }
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
            addTraitVerre(newTraitementNiveau.Text, newTraitementNom.SelectedIndex);
        }

        public void addTraitVerre(string niveau, int selectedIndex)
        {
            // START VALIDATION
            if (selectedIndex == -1 || niveau.Length == 0)
            {
                HandyControl.Controls.MessageBox.Show("Veuillez remplir tous les champs");
                return;
            }
            int nv = 0;
            if (!int.TryParse(niveau, out nv))
            {
                HandyControl.Controls.MessageBox.Show("Veuillez remplir un nombre dans les champs du niveau");
                return;
            }
            if (nv <= 0)
            {
                HandyControl.Controls.MessageBox.Show("Le niveau du traitement ne peut pas être nulle ou négative!");
                return;
            }
            // END VALIDATION

            StackPanel stackPanel = new StackPanel() { Orientation = Orientation.Horizontal, Margin = new Thickness(0, 0, 0, 5) };

            ComboBox newCombobox = new ComboBox() { Width = 200, IsEnabled = false, Margin = new Thickness(0, 0, 10, 0) };
            newCombobox.ItemsSource = traitements;
            newCombobox.DisplayMemberPath = "Nom";
            newCombobox.SelectedValuePath = "idTraitement";
            newCombobox.SelectedIndex = selectedIndex;

            HandyControl.Controls.TextBox newTextBox = new HandyControl.Controls.TextBox() { Width = 100, Text = niveau, Margin = new Thickness(0, 0, 10, 0), IsEnabled = false };
            
            Button supprimerBtn = new Button() { Content = "Supprimer" };
            supprimerBtn.Click += supprimerTraitementVerre;

            stackPanel.Children.Add(newCombobox);
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
            this.ligne = new ligneentree()
            {
                Date_Commande = DateTime.Now,
                Qte_Commande = int.Parse(qteText.Text),
                Prix_Total = (int)categorie.SelectedValue == 1 ? float.Parse(verrePrix.Text) : float.Parse(prixText.Text),
                EtatCmd = "Non payée",
                addLigneCmdClient = this
            };

            addCmdPage.removeLigneFromList(this.ligne);
            MyContext.navigateTo(prevPage);
        }

        //getId Of selected traitement :


        // save ligne
        public void saveLigne(object sender, RoutedEventArgs e)
        {
            if (validateForm() != "")
            {
                HandyControl.Controls.MessageBox.Show(validateForm());
                return;
            }

            if (selectedReference != null && selectedReference.QteDisponible < int.Parse(qteText.Text))
            {
                HandyControl.Controls.MessageBox.Show("Quantité non disponible");
                return;
            }

            this.ligne = new ligneentree()
            {
                Date_Commande = DateTime.Now,
                Qte_Commande = int.Parse(qteText.Text),
                Prix_Total = (int)categorie.SelectedValue == 1 ? float.Parse(verrePrix.Text) : float.Parse(prixText.Text),
                EtatCmd = "Non payée",
                addLigneCmdClient = this
            };

            bool found = false;
            if (addCmdPage.ligneentreeList != null && addCmdPage.ligneentreeList.Count > 0)
            {
                foreach (ligneentree l in addCmdPage.ligneentreeList)
                {
                    if (l.addLigneCmdClient == this.ligne.addLigneCmdClient)
                    {
                        addCmdPage.updateLigneInList(this.ligne);
                        found = true;
                        break;
                    }
                }
            }
            
            if (!found)
                addCmdPage.addNewLigneToList(this.ligne);

            MyContext.navigateTo(prevPage);
        }


        public List<traitement> get_traitements()
        {
            return traitements;
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


        //validation
        private string validateForm()
        {
            int selectedIemValue = (int)categorie.SelectedValue;

            if (selectedIemValue == 1)
            {
                // VERRES
                if (matiereVerresText.SelectedIndex == -1 || indiceVerresText.SelectedIndex == -1 || geometrieVerresText.SelectedIndex == -1 ||
                    teintText.SelectedIndex == -1 || verrePrix.Text.Length == 0)
                    return "Veuillez remplir tous les champs";

                float pr = 0;
                if (!float.TryParse(verrePrix.Text, out pr))
                    return "Veuillez remplir un nombre dans le champs du prix";

                if (pr <= 0)
                    return "Le prix ne peut pas être négative!";

            } else
            {
                // LUNETTES
                if (referenceText.SelectedIndex == -1 || qteText.Text.Length == 0)
                    return "Veuillez remplir tous les champs";
            }

            // quantité
            int num = 0;
            if (!int.TryParse(qteText.Text, out num))
                return "Veuillez remplir un nombre dans le champs de la quantité";

            if (num <= 0)
                return "La quantité ne peut pas être nulle ou négative!";

            return "";
        }

        // select visite
        private void selectVisite(object sender, SelectionChangedEventArgs e)
        {
            visite visite = db.visites.Where(v => v.id == (int)visiteList.SelectedValue).SingleOrDefault();
            selectedVisite = visite;
            getVisions(visite);
        }
    }
}
