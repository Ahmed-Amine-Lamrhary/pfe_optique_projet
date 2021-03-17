using Caliburn.Micro;
using MenuWithSubMenu.Model;
using MenuWithSubMenu.Utils;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
    public partial class AddCmd : Page
    {
        dbEntities db;
        DbContextTransaction transaction;

        public BindableCollection<AddLigneCmdFournisseur> lignesCmd;

        public AddCmd()
        {
            InitializeComponent();

            db = new dbEntities();
            lignesCmd = new BindableCollection<AddLigneCmdFournisseur>();

            lignesCmdBox.ItemsSource = lignesCmd;

            List<fournisseur> fournisseurs = db.fournisseurs.Distinct().ToList();
            selectFournisseur.ItemsSource = fournisseurs;
            selectFournisseur.DisplayMemberPath = "Nom";
            selectFournisseur.SelectedValuePath = "idFournisseur";

            selectFournisseur.SelectedIndex = 0;
        }

        public void ajouterLigne(object sender, RoutedEventArgs e)
        {
            AddLigneCmdFournisseur newLigne = new AddLigneCmdFournisseur(this, this);
            MyContext.navigateTo(newLigne);
        }

        // add new ligne to the list
        public void addNewLigneToList(AddLigneCmdFournisseur newLigne)
        {
            lignesCmd.Add(newLigne);
            lignesCmdBox.ItemsSource = lignesCmd;
        }

        public void updateLigneInList(AddLigneCmdFournisseur ligne)
        {
            AddLigneCmdFournisseur updatedLigne = lignesCmd.Where(l => l.Equals(ligne)).First();
            updatedLigne = ligne;
            lignesCmdBox.ItemsSource = lignesCmd;
        }

        private void editLigne(object sender, MouseButtonEventArgs e)
        {
            AddLigneCmdFournisseur ligne = lignesCmdBox.SelectedItem as AddLigneCmdFournisseur;
            MyContext.navigateTo(ligne);
        }

        // save all the order
        public void saveCmd(object sender, RoutedEventArgs e)
        {
            if (lignesCmd.Count == 0)
            {
                MessageBox.Show("Cannot submit");
                return;
            }

            try
            {
                transaction = db.Database.BeginTransaction();

                // create commande
                cmdfournisseur commande = new cmdfournisseur() {
                    idFournisseur = (int)selectFournisseur.SelectedValue,
                    DateEntree = DateTime.Now
                };
                db.cmdfournisseurs.Add(commande);
                db.SaveChanges();

                foreach (AddLigneCmdFournisseur ligne in lignesCmd)
                {
                    // if article is new
                    article article = new article()
                    {
                        idCategorie = (int)ligne.categorie.SelectedValue,
                        QteDisponible = 0,
                        PrixUnitaire = int.Parse(ligne.prixText.Text),
                        Garantie = ligne.garantieText.Text,
                        Description = ligne.descText.Text,
                        Designation = ""
                    };
                    db.articles.Add(article);
                    db.SaveChanges();

                    // if article is already in stock


                    // create ligne de commande
                    db.lignecommandes.Add(new lignecommande()
                    {
                        idArticle = article.idArticle,
                        idCmdFournisseur = commande.idCmdFournisseur,
                        Date_Commande = DateTime.Now,
                        Adresse_Commande = "",
                        Qte_Commande = int.Parse(ligne.qteText.Text),
                        Prix_Total = int.Parse(ligne.qteText.Text) * int.Parse(ligne.prixText.Text),
                        EtatCmd = "en-cours"
                    });
                    db.SaveChanges();
                }

            } catch (Exception ex)
            {
                Console.Write(ex.Message);
                HandyControl.Controls.MessageBox.Show(ex.Message);
                transaction.Rollback();
            }
        }
    }
}
