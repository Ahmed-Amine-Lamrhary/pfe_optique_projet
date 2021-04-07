using Caliburn.Micro;
using MenuWithSubMenu.Model;
using MenuWithSubMenu.Utils;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
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
    public partial class AddCmdClient : Page
    {
        dbEntities db;
        DbContextTransaction transaction;
        cmdclient cmdClient;

        public BindableCollection<AddLigneCmdClient> lignesCmd;

        // update constructor
        public AddCmdClient(cmdclient cmdClient)
        {
            InitializeComponent();

            db = new dbEntities();
            lignesCmd = new BindableCollection<AddLigneCmdClient>();
            this.cmdClient = cmdClient;

            lignesCmdBox.Visibility = Visibility.Collapsed;
            nothingBox.Visibility = Visibility.Collapsed;
            loadingBox.Visibility = Visibility.Visible;

            try
            {
                // get lignes
                List<ligneentree> ligneentree = db.ligneentrees.Where(l => l.idCmdClient == cmdClient.idCmdClient).ToList();
                foreach (ligneentree ligne in ligneentree)
                {
                    lignesCmd.Add(new AddLigneCmdClient(this, this, ligne));
                }

                lignesCmdBox.ItemsSource = ligneentree;

                lignesCmdBox.Visibility = Visibility.Visible;
            } catch (Exception ex)
            {
                Console.WriteLine(ex);
                nothingBox.Visibility = Visibility.Visible;
            } finally
            {
                loadingBox.Visibility = Visibility.Collapsed;
            }

            List<client> clients = db.clients.Distinct().ToList();
            selectClient.ItemsSource = clients;
            selectClient.DisplayMemberPath = "nom";
            selectClient.SelectedValuePath = "cin";

            for (var i = 0; i < clients.Count(); i++)
            {
                if (clients[i].cin == cmdClient.client_cin)
                {
                    selectClient.SelectedIndex = i;
                    break;
                }
            }
        }
        
        // create constructor
        public AddCmdClient()
        {
            InitializeComponent();

            db = new dbEntities();
            lignesCmd = new BindableCollection<AddLigneCmdClient>();

            lignesCmdBox.ItemsSource = lignesCmd;

            List<client> clients = db.clients.Distinct().ToList();
            selectClient.ItemsSource = clients;
            selectClient.DisplayMemberPath = "nom";
            selectClient.SelectedValuePath = "cin";

            nothingBox.Visibility = Visibility.Visible;

            selectClient.SelectedIndex = 0;
        }

        public void ajouterLigne(object sender, RoutedEventArgs e)
        {
            // get last visite
            visite lastClientVisite = db.visites.OrderByDescending(v => v.date).Where(v => v.client_cin == (string)selectClient.SelectedValue).FirstOrDefault();

            if (lastClientVisite == null)
            {
                MessageBox.Show("Client ne possède aucune visite");
                return;
            }

            AddLigneCmdClient newLigne = new AddLigneCmdClient(this, this, lastClientVisite);
            MyContext.navigateTo(newLigne);
        }

        // add new ligne to the list
        public void addNewLigneToList(AddLigneCmdClient newLigne)
        {
            lignesCmd.Add(newLigne);
            lignesCmdBox.ItemsSource = lignesCmd;
            nothingBox.Visibility = Visibility.Collapsed;
            lignesCmdBox.Visibility = Visibility.Visible;
        }

        public void removeLigneFromList(AddLigneCmdClient ligne)
        {
            lignesCmd.Remove(ligne);
            lignesCmdBox.ItemsSource = lignesCmd;

            if (lignesCmd.Count() == 0)
            {
                nothingBox.Visibility = Visibility.Visible;
                lignesCmdBox.Visibility = Visibility.Collapsed;
            }
        }


        public void updateLigneInList(AddLigneCmdClient ligne)
        {
            AddLigneCmdClient updatedLigne = lignesCmd.Where(l => l.Equals(ligne)).First();
            updatedLigne = ligne;
            lignesCmdBox.ItemsSource = lignesCmd;
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

                cmdclient commande;
                string message;

                if (cmdClient == null)
                {
                    // create commande
                    commande = new cmdclient()
                    {
                        client_cin = (string)selectClient.SelectedValue,
                        DateCmd = DateTime.Now
                    };
                    db.cmdclients.Add(commande);
                    db.SaveChanges();

                    message = "Votre Commande a été bien ajouté";
                }
                else
                {
                    // get commande
                    commande = cmdClient;
                    db.cmdclients.Where(c => c.idCmdClient == commande.idCmdClient).SingleOrDefault().client_cin = (string)selectClient.SelectedValue;
                    db.SaveChanges();

                    message = "Votre Commande a été bien modifié";
                }
               
                foreach (AddLigneCmdClient ligne in lignesCmd)
                {
                    if (ligne.isUpdate) continue;

                    int categorie = (int)ligne.categorie.SelectedValue;

                    // verre
                    if (categorie == 1)
                    {
                        // add verre
                        verre verre = new verre()
                        {
                            Teinte = (string)ligne.teintText.SelectedValue,
                            indice = (double?)ligne.indiceVerresText.SelectedValue,
                            matiere = (string)ligne.matiereVerresText.SelectedValue,
                            geometrie = (string)ligne.geometrieVerresText.SelectedValue,
                        };
                        db.verres.Add(verre);
                        db.SaveChanges();

                        // add ligne
                        db.ligneentrees.Add(new ligneentree()
                        {
                            idVerre = verre.idVerre,
                            idArticle = null,
                            idCmdClient = commande.idCmdClient,
                            Date_Commande = DateTime.Now,
                            Adresse_Commande = "",
                            Qte_Commande = int.Parse(ligne.qteText.Text),
                            EtatCmd = "En-Cours"
                        });
                        db.SaveChanges();
                    } else
                    {
                        db.ligneentrees.Add(new ligneentree()
                        {
                            idArticle = (string)ligne.referenceText.SelectedValue,
                            idCmdClient = commande.idCmdClient,
                            Date_Commande = DateTime.Now,
                            Adresse_Commande = "",
                            Qte_Commande = int.Parse(ligne.qteText.Text),
                            Prix_Total = int.Parse(ligne.qteText.Text) * float.Parse(ligne.prixText.Text),
                            EtatCmd = "En-Cours"
                        });
                        db.SaveChanges();
                    }
                }
                transaction.Commit();
                MessageBox.Show(message);
                MyContext.navigateTo(new CmdsClients());
            }
            catch (DbEntityValidationException ex)
            {
                HandyControl.Controls.MessageBox.Show(ex.Message);
                transaction.Rollback();
            }
        }

        public void changeEtat(object sender, RoutedEventArgs e)
        {
            ligneentree ligne = lignesCmdBox.SelectedItem as ligneentree;
            if (ligne.EtatCmd == "En-Cours")
            {
                // convert to completed
                try
                {
                    transaction = db.Database.BeginTransaction();
                    // change etat to completed
                    ligne.EtatCmd = "completed";
                    db.SaveChanges();

                    // change article qte
                    article article = db.articles.Where(a => a.idArticle == ligne.idArticle).SingleOrDefault();
                    if (article.QteDisponible >= ligne.Qte_Commande)
                    {
                        article.QteDisponible -= ligne.Qte_Commande;
                        db.SaveChanges();

                        transaction.Commit();
                        MessageBox.Show("La ligne est complétée");
                    } else
                        throw new Exception("Qantité non disponible");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    transaction.Rollback();
                }
            }
        }
    
        public void deleteLigne(object sender, RoutedEventArgs e)
        {
            ligneentree ligne = lignesCmdBox.SelectedItem as ligneentree;
            if (ligne.EtatCmd == "En-Cours")
            {
                db.ligneentrees.Remove(ligne);
                db.SaveChanges();

                MyContext.navigateTo(new CmdsClients());
            } else
            {
                MessageBox.Show("Ligne is completed");
            }
        }

        private void editLigne(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
