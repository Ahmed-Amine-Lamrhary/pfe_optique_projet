﻿using Caliburn.Micro;
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
    public partial class AddCmd : Page
    {
        dbEntities db;
        DbContextTransaction transaction;
        cmdfournisseur cmdfournisseur;

        public BindableCollection<AddLigneCmdFournisseur> lignesCmd;

        // update constructor
        public AddCmd(cmdfournisseur cmdfournisseur)
        {
            InitializeComponent();

            db = new dbEntities();
            lignesCmd = new BindableCollection<AddLigneCmdFournisseur>();
            this.cmdfournisseur = cmdfournisseur;

            lignesCmdBox.Visibility = Visibility.Collapsed;
            nothingBox.Visibility = Visibility.Collapsed;
            loadingBox.Visibility = Visibility.Visible;

            try
            {
                // get lignes
                List<lignecommande> lignecommandes = db.lignecommandes.Where(l => l.idCmdFournisseur == cmdfournisseur.idCmdFournisseur).ToList();
                foreach (lignecommande ligne in lignecommandes)
                {
                    lignesCmd.Add(new AddLigneCmdFournisseur(this, this, ligne));
                }

                lignesCmdBox.ItemsSource = lignecommandes;
                lignesCmdBox.Visibility = Visibility.Visible;

            } catch (Exception ex)
            {
                Console.WriteLine(ex);
                nothingBox.Visibility = Visibility.Visible;
            } finally
            {
                loadingBox.Visibility = Visibility.Collapsed;
            }

            List<fournisseur> fournisseurs = db.fournisseurs.Distinct().ToList();
            selectFournisseur.ItemsSource = fournisseurs;
            selectFournisseur.DisplayMemberPath = "Nom";
            selectFournisseur.SelectedValuePath = "idFournisseur";

            for (var i = 0; i < fournisseurs.Count(); i++)
            {
                if (fournisseurs[i].idFournisseur == cmdfournisseur.idFournisseur)
                {
                    selectFournisseur.SelectedIndex = i;
                    break;
                }
            }
        }

        // create constructor
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

            nothingBox.Visibility = Visibility.Visible;

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
            nothingBox.Visibility = Visibility.Collapsed;
            lignesCmdBox.Visibility = Visibility.Visible;
        }

        public void updateLigneInList(AddLigneCmdFournisseur ligne)
        {
            AddLigneCmdFournisseur updatedLigne = lignesCmd.Where(l => l.Equals(ligne)).First();
            updatedLigne = ligne;
            lignesCmdBox.ItemsSource = lignesCmd;
        }

        public void removeLigneFromList(AddLigneCmdFournisseur ligne)
        {
            lignesCmd.Remove(ligne);
            lignesCmdBox.ItemsSource = lignesCmd;

            if (lignesCmd.Count() == 0)
            {
                nothingBox.Visibility = Visibility.Visible;
                lignesCmdBox.Visibility = Visibility.Collapsed;
            }
        }


        private void editLigne(object sender, RoutedEventArgs e)
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

            cmdfournisseur commande;
            string message;

            try
            {
                transaction = db.Database.BeginTransaction();

                if (cmdfournisseur == null)
                {
                    // create commande
                    commande = new cmdfournisseur()
                    {
                        idFournisseur = (int)selectFournisseur.SelectedValue,
                        DateEntree = DateTime.Now
                    };
                    db.cmdfournisseurs.Add(commande);
                    db.SaveChanges();

                    message = "Votre Commande a été bien ajouté";
                } else
                {
                    // get commande
                    commande = cmdfournisseur;
                    db.cmdfournisseurs.Where(c => c.idCmdFournisseur == commande.idCmdFournisseur).SingleOrDefault().idFournisseur = (int)selectFournisseur.SelectedValue;
                    db.SaveChanges();

                    message = "Votre Commande a été bien modifié";
                }

                string reference = null;
                foreach (AddLigneCmdFournisseur ligne in lignesCmd)
                {
                    if (ligne.isUpdate) continue;

                    article article = null;

                    reference = (string)ligne.referenceText.SelectedValue;
                    article = db.articles.Where(a => a.idArticle == reference).SingleOrDefault();

                    // create ligne de commande
                    db.lignecommandes.Add(new lignecommande()
                    {
                        idArticle = article.idArticle,
                        idCmdFournisseur = commande.idCmdFournisseur,
                        Date_Commande = DateTime.Now,
                        Adresse_Commande = "",
                        Qte_Commande = int.Parse(ligne.qteText.Text),
                        Prix_Total = int.Parse(ligne.qteText.Text) * float.Parse(ligne.prixText.Text),
                        EtatCmd = "En-Cours"
                    });
                    db.SaveChanges();
                }

                transaction.Commit();
                MessageBox.Show(message);
                MyContext.navigateTo(new CmdsFournisseur());

            } catch (DbEntityValidationException ex)
            {
                /*Console.Write(ex.Message);
                HandyControl.Controls.MessageBox.Show(ex.Message);*/
                foreach (var eve in ex.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                transaction.Rollback();
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

        public void changeEtat(object sender, RoutedEventArgs e)
        {
            lignecommande ligne = lignesCmdBox.SelectedItem as lignecommande;
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

                    article.QteDisponible += ligne.Qte_Commande;
                    db.SaveChanges();

                    transaction.Commit();
                    MessageBox.Show("La ligne est complétée");
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
            lignecommande ligne = lignesCmdBox.SelectedItem as lignecommande;
            if (ligne.EtatCmd == "En-Cours")
            {
                db.lignecommandes.Remove(ligne);
                db.SaveChanges();

                MyContext.navigateTo(new CmdsFournisseur());
            }
            else
            {
                MessageBox.Show("Ligne is completed");
            }
        }
    }
}
