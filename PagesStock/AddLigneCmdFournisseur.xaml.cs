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
        private List<traitement> traitements;
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
            addCmdPage.removeLigneFromList(this);
            MyContext.navigateTo(prevPage);
        }

        // save ligne
        public void saveLigne(object sender, RoutedEventArgs e)
        {
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
