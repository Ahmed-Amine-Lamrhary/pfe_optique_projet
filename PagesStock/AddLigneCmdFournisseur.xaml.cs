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
    public partial class AddLigneCmdFournisseur : Page
    {
        dbEntities db;
        private Page prevPage;
        private AddCmd addCmdPage;
        private List<article> references;
        public lignecommande lignecommande;
        public bool isUpdate;

        // update constructor
        public AddLigneCmdFournisseur(Page prevP, AddCmd addCmdP, lignecommande lignecommande)
        {
            InitializeComponent();
            db = new dbEntities();
            prevPage = prevP;
            addCmdPage = addCmdP;
            references = new List<article>();
            this.isUpdate = true;
            this.lignecommande = lignecommande;
            fillComboBox();

            updateBoxBtns.Visibility = Visibility.Collapsed;
            mainStackPanel.IsEnabled = false;

            /////// fill the information

            // set quantité de commande
            qteText.Text = lignecommande.Qte_Commande + "";

            article article = db.articles.Where(a => a.idArticle == lignecommande.idArticle).SingleOrDefault();

            // set categorie
            categorie.SelectedValue = article.idCategorie;

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


        // create constructor
        public AddLigneCmdFournisseur(Page prevP, AddCmd addCmdP)
        {
            InitializeComponent();
            db = new dbEntities();
            prevPage = prevP;
            addCmdPage = addCmdP;
            references = new List<article>();
            this.isUpdate = false;
            fillComboBox();
        }

        public void selectReference(object sender, SelectionChangedEventArgs e)
        {
            // initialize
            prixText.Text = "";
            garantieText.Text = "";
            descText.Text = "";

            // begin
            string idReference = (string)referenceText.SelectedValue;
            article reference = db.articles.Where(article => article.idArticle == idReference).SingleOrDefault();

            if (reference != null)
            {
                // reference information
                prixText.Text = reference.PrixUnitaire.ToString();
                garantieText.Text = reference.Garantie;
                descText.Text = reference.Description;
                /*marqueText.Text = ;
                modelText.Text = ;*/

                int selectedCategorieId = (int)categorie.SelectedValue;
                switch (selectedCategorieId)
                {
                    // verre
                    case 1:
                        // verre verre = db.verres.Where(v => v.idArticle == reference.idArticle).SingleOrDefault();
                        // typeVerresText.SelectedIndex = verre.idTypeVerre - 1;

                        // teinte
                        // teintText.Text = verre.Teinte;

                        // traitements
                        // List<ligne_traitement_verre> lignesVerre = db.ligne_traitement_verre.Where(l => l.verre_idVerre == verre.idVerre).ToList();
                        // foreach (ligne_traitement_verre ligne in lignesVerre)
                        // {

                        //     traitement traitement = db.traitements.Where(t => t.idTraitement == ligne.traitement_idTraitement).SingleOrDefault();
                        //    addTraitVerre(ligne.niveau.ToString(), traitement.Nom);
                        // }

                        // visions
                        // fillVisionsVerre(db.visions.Where(v => v.verre_idVerre == verre.idVerre).ToList());
                        break;
                    // cadre
                    case 3:
                        cadre cadre = db.cadres.Where(c => c.idArticle == reference.idArticle).SingleOrDefault();
                        diametreText.Text = cadre.DiametreVerre.ToString();
                        pontText.Text = cadre.Pont.ToString();
                        langeur_brance_text.Text = cadre.LongeurBrache.ToString();
                        largeurText.Text = cadre.Largeur.ToString();
                        hautteur_verre_text.Text = cadre.HauteurVerre.ToString();
                        couleurText.Text = cadre.Couleur;
                        break;
                }
            }
        }

        public void selectItem(object sender, SelectionChangedEventArgs e)
        {
            updateReferenceCombo();
        }

        private void fillComboBox()
        {
            List<categorie> categories = db.categories.Where(c => c.Nom != "Verre Optique").Distinct().ToList();
            categorie.ItemsSource = categories;
            categorie.DisplayMemberPath = "Nom";
            categorie.SelectedValuePath = "idCategorie";

            categorie.SelectedIndex = 0;

            updateReferenceCombo();
        }

        private void ReturnBtn_Click(object sender, RoutedEventArgs e)
        {
            MyContext.navigateTo(prevPage);
        }

        // abort ligne
        public void deleteLigne(object sender, RoutedEventArgs e)
        {
            this.lignecommande = new lignecommande()
            {
                Date_Commande = DateTime.Now,
                Qte_Commande = int.Parse(qteText.Text),
                Prix_Total = float.Parse(prixText.Text),
                EtatCmd = "En-Cours",
                addLigneCmdFournisseur = this
            };

            addCmdPage.removeLigneFromList(this.lignecommande);
            MyContext.navigateTo(prevPage);
        }

        // save ligne
        public void saveLigne(object sender, RoutedEventArgs e)
        {
            this.lignecommande = new lignecommande()
            {
                Date_Commande = DateTime.Now,
                Qte_Commande = int.Parse(qteText.Text),
                Prix_Total = float.Parse(prixText.Text),
                EtatCmd = "En-Cours",
                addLigneCmdFournisseur = this
            };

            bool found = false;
            if (addCmdPage.lignecommandes != null && addCmdPage.lignecommandes.Count > 0)
            {
                foreach (lignecommande l in addCmdPage.lignecommandes)
                {
                    if (l.addLigneCmdFournisseur == this.lignecommande.addLigneCmdFournisseur)
                    {
                        addCmdPage.updateLigneInList(this.lignecommande);
                        found = true;
                        break;
                    }
                }
            }
            
            if (!found)
                addCmdPage.addNewLigneToList(this.lignecommande);

            MyContext.navigateTo(prevPage);
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
    }
}
