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

        public AddLigneCmdFournisseur(Page prevP, AddCmd addCmdP)
        {
            InitializeComponent();
            db = new dbEntities();
            prevPage = prevP;
            addCmdPage = addCmdP;
            references = new List<article>();
            fillComboBox();
            
        }

        public void selectItem(object sender, SelectionChangedEventArgs e)
        {
            int selectedIemValue = (int)categorie.SelectedValue;
            switch (selectedIemValue)
            {
                case 1:
                    verrePanel.Visibility = Visibility.Visible;
                    lentillePanel.Visibility = Visibility.Collapsed;
                    cadrePanel.Visibility = Visibility.Collapsed;
                    break;
                case 2:
                    verrePanel.Visibility = Visibility.Collapsed;
                    lentillePanel.Visibility = Visibility.Visible;
                    cadrePanel.Visibility = Visibility.Collapsed;
                    break;
                case 3:
                    verrePanel.Visibility = Visibility.Collapsed;
                    lentillePanel.Visibility = Visibility.Collapsed;
                    cadrePanel.Visibility = Visibility.Visible;
                    break;
            }
            updateReferenceCombo();

        }

        public void fillVisionsVerre(List<vision> visions)
        {
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
            }
        }

        public void fillVisionsLentille(List<vision> visions)
        {
            foreach (vision vision in visions)
            {
                if (vision.loin)
                {
                    if (vision.gauche)
                    {
                        og_sph_loin_lentille.Text = vision.sph.ToString();
                        og_cyl_loin_lentille.Text = vision.cyl.ToString();
                        og_axe_loin_lentille.Text = vision.axe.ToString();
                        og_add_loin_lentille.Text = vision.add.ToString();
                    }
                    else
                    {
                        od_sph_loin_lentille.Text = vision.sph.ToString();
                        od_cyl_loin_lentille.Text = vision.cyl.ToString();
                        od_axe_loin_lentille.Text = vision.axe.ToString();
                        od_add_loin_lentille.Text = vision.add.ToString();
                    }
                    ecartLoinText_lentille.Text = vision.ecart.ToString();
                    hauteurLoinText_lentille.Text = vision.hauteur.ToString();
                }
                else
                {
                    if (vision.gauche)
                    {
                        og_sph_pres_lentille.Text = vision.sph.ToString();
                        og_cyl_pres_lentille.Text = vision.cyl.ToString();
                        og_axe_pres_lentille.Text = vision.axe.ToString();
                        og_add_pres_lentille.Text = vision.add.ToString();
                    }
                    else
                    {
                        od_sph_pres_lentille.Text = vision.sph.ToString();
                        od_cyl_pres_lentille.Text = vision.cyl.ToString();
                        od_axe_pres_lentille.Text = vision.axe.ToString();
                        od_add_pres_lentille.Text = vision.add.ToString();
                    }
                    ecartPresText_lentille.Text = vision.ecart.ToString();
                    hauteurPresText_lentille.Text = vision.hauteur.ToString();
                }
            }
        }

        public void selectReference(object sender, SelectionChangedEventArgs e)
        {
            // initialize
            prixText.Text = "";
            garantieText.Text = "";
            descText.Text = "";
            traitementsBox.Children.Clear();
            traitementsLentilleBox.Children.Clear();
            teintText.Text = "";
            couleurLentille.Text = "";

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
                        verre verre = db.verres.Where(v => v.idArticle == reference.idArticle).SingleOrDefault();
                        typeVerresText.SelectedIndex = verre.idTypeVerre - 1;

                        // teinte
                        teintText.Text = verre.Teinte;

                        // traitements
                        List<ligne_traitement_verre> lignesVerre = db.ligne_traitement_verre.Where(l => l.verre_idVerre == verre.idVerre).ToList();
                        foreach (ligne_traitement_verre ligne in lignesVerre)
                        {

                            traitement traitement = db.traitements.Where(t => t.idTraitement == ligne.traitement_idTraitement).SingleOrDefault();
                            addTraitVerre(ligne.niveau.ToString(), traitement.Nom);
                        }

                        // visions
                        fillVisionsVerre(db.visions.Where(v => v.verre_idVerre == verre.idVerre).ToList());
                        break;
                    // lentille
                    case 2:
                        lentille lentille = db.lentilles.Where(l => l.idArticle == reference.idArticle).SingleOrDefault();

                        couleurLentille.Text = lentille.Couleur;

                        // type de lentille
                        string type = db.type_lentille.Where(t => t.idtype_lentille == lentille.idtype_lentille).SingleOrDefault().nom;
                        switch (type)
                        {
                            case "Les Lentilles Toriques":
                                lentilletorique lentilleTorique = db.lentilletoriques.Where(l => l.idTypeLentille == lentille.idLigneType).SingleOrDefault();
                                typeLentilleText.SelectedIndex = 0;
                                cylText.Text = lentilleTorique.CYL.ToString();
                                axeText.Text = lentilleTorique.AXE.ToString();
                                break;
                            case "Les Lentilles MultiFocales":
                                lentillemultifocale lentilleMultiFocale = db.lentillemultifocales.Where(l => l.idTypeLentille == lentille.idLigneType).SingleOrDefault();
                                typeLentilleText.SelectedIndex = 1;
                                domText.Text = lentilleMultiFocale.DOM.ToString();
                                addText.Text = lentilleMultiFocale.ADD.ToString();
                                break;
                            case "Les Lentilles Spheriques":
                                lentillespherique lentilleSpherique = db.lentillespheriques.Where(l => l.idTypeLentille == lentille.idLigneType).SingleOrDefault();
                                typeLentilleText.SelectedIndex = 2;
                                rcText.Text = lentilleSpherique.RC.ToString();
                                diaText.Text = lentilleSpherique.DIA.ToString();
                                break;
                        }

                        // traitements
                        List<ligne_traitement_lentille> lignesLentilles = db.ligne_traitement_lentille.Where(l => l.lentille_idLentille == lentille.idLentille).ToList();
                        foreach (ligne_traitement_lentille ligne in lignesLentilles)
                        {
                            traitement traitement = db.traitements.Where(t => t.idTraitement == ligne.traitement_idTraitement).SingleOrDefault();
                            addTraitLentille("", traitement.Nom);
                        }

                        // visions
                        fillVisionsLentille(db.visions.Where(v => v.lentille_idLentille == lentille.idLentille).ToList());

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

        private void fillComboBox()
        {
            List<categorie> categories = db.categories.Distinct().ToList();
            categorie.ItemsSource = categories;
            categorie.DisplayMemberPath = "Nom";
            categorie.SelectedValuePath = "idCategorie";

            categorie.SelectedIndex = 0;

            List<typeverre> typeVerres = db.typeverres.Distinct().ToList();
            typeVerresText.ItemsSource = typeVerres;
            typeVerresText.DisplayMemberPath = "NomType";
            typeVerresText.SelectedValuePath = "idTypeVerre";

            List<type_lentille> typeLentille = db.type_lentille.Distinct().ToList();
            typeLentilleText.ItemsSource = typeLentille;
            typeLentilleText.DisplayMemberPath = "nom";
            typeLentilleText.SelectedValuePath = "idtype_lentille";

            updateReferenceCombo();

            traitements = db.traitements.Distinct().ToList();
            newTraitementNom.ItemsSource = traitements;
            newTraitementNom.DisplayMemberPath = "Nom";
            newTraitementNom.SelectedValuePath = "idTraitement";

            lentilleNewTrat.ItemsSource = traitements;
            lentilleNewTrat.DisplayMemberPath = "Nom";
            lentilleNewTrat.SelectedValuePath = "idTraitement";
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
        }

        public void addTraitLentilleButton(object sender, RoutedEventArgs e)
        {
            addTraitLentille(lentilleNewTratNiveau.Text, lentilleNewTrat.DisplayMemberPath);
        }

        public void addTraitVerreButton(object sender, RoutedEventArgs e)
        {
            addTraitVerre(newTraitementNiveau.Text, newTraitementNom.DisplayMemberPath);
        }

        public void addTraitLentille(string niveau, string nom)
        {
            StackPanel stackPanel = new StackPanel() { Orientation = System.Windows.Controls.Orientation.Horizontal };

            TextBlock newTextBlock = new TextBlock() { Text = nom, IsEnabled = false };
            TextBox newTextBox = new TextBox() { Width = 100, Text = niveau };
            Button supprimerBtn = new Button() { Content = "Supprimer" };
            supprimerBtn.Click += supprimerTraitementLentille;

            stackPanel.Children.Add(newTextBlock);
            stackPanel.Children.Add(newTextBox);
            stackPanel.Children.Add(supprimerBtn);

            traitementsLentilleBox.Children.Add(stackPanel);
        }


        public void addTraitVerre(string niveau, string nom)
        {
            StackPanel stackPanel = new StackPanel() { Orientation = System.Windows.Controls.Orientation.Horizontal };

            TextBlock newTextBlock = new TextBlock() { Text = nom, IsEnabled = false };
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
        
        private void supprimerTraitementLentille(object sender, RoutedEventArgs e)
        {
            traitementsLentilleBox.Children.Remove((UIElement)(sender as FrameworkElement).Parent);
        }

        private void ReturnBtn_Click(object sender, RoutedEventArgs e)
        {
            MyContext.navigateTo(prevPage);
        }

        // abort ligne
        public void deleteLigne(object sender, RoutedEventArgs e)
        {
            addCmdPage.lignesCmd.Remove(this);
            MyContext.navigateTo(prevPage);
        }

        //getId Of selected traitement :


        // save ligne
        public void saveLigne(object sender, RoutedEventArgs e)
        {
            if (newRefText.Visibility == Visibility.Visible && referenceIsExiste(newRefText.Text))
            {
                MessageBox.Show("Référence déja existe");
                btnNewRef.Content = "Ajouter réference";
                newRefText.Visibility = Visibility.Collapsed;
                referenceText.Visibility = Visibility.Visible;
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(newRefText.Visibility == Visibility.Collapsed)
            {
                newRefText.Visibility = Visibility.Visible;
                btnNewRef.Content = "X";
                referenceText.IsEnabled = false;

                articleInfoBox.IsEnabled = true;
                additionalInfoBox.IsEnabled = true;
            }
            else
            {
                newRefText.Visibility = Visibility.Collapsed;
                btnNewRef.Content = "Ajouter Référence";
                referenceText.IsEnabled = true;

                articleInfoBox.IsEnabled = false;
                additionalInfoBox.IsEnabled = false;
            }
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

        private void TypeLentilleText_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int selectedIemValue = (int)typeLentilleText.SelectedValue;
            switch (selectedIemValue)
            {
                case 1:
                    panelMultiFocale.Visibility = Visibility.Collapsed;
                    panelSpherique.Visibility = Visibility.Collapsed;
                    break;
                case 2:
                    panelMultiFocale.Visibility = Visibility.Visible;
                    panelSpherique.Visibility = Visibility.Collapsed;
                    break;
                case 3:
                    panelMultiFocale.Visibility = Visibility.Collapsed;
                    panelSpherique.Visibility = Visibility.Visible;
                    break;
            }
        }
    }
}
