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
                    // if article is new
                    int selectedIemValue = (int)ligne.categorie.SelectedValue;
                    switch (selectedIemValue)
                    {
                        //Verre
                        case 1:
                            oeil new_oeil = new oeil()
                            {
                                ADD_D = int.Parse(ligne.od_add_verre.Text),
                                ADD_G = int.Parse(ligne.og_add_verre.Text),
                                AXE_D = int.Parse(ligne.od_axe_verre.Text),
                                AXE_G = int.Parse(ligne.og_axe_verre.Text),
                                CYL_D = int.Parse(ligne.od_cyl_verre.Text),
                                CYL_G = int.Parse(ligne.og_cyl_verre.Text),
                                SPH_D = int.Parse(ligne.od_sph_verre.Text),
                                SPH_G = int.Parse(ligne.og_sph_verre.Text)
                            };
                            db.oeils.Add(new_oeil);
                            db.SaveChanges();
                            verre new_verre = new verre()
                            {
                                idTypeVerre = (int)ligne.typeVerresText.SelectedValue,
                                idArticle = article.idArticle,
                                idOeil = new_oeil.idOeil,
                                Modele = ligne.modelText.Text,
                                Teinte = ligne.teintText.Text
                            };
                            db.verres.Add(new_verre);
                            db.SaveChanges();
                            foreach (traitement t in ligne.get_traitements())
                            {
                                ligne_verre_traitement new_ligne_traitement = new ligne_verre_traitement()
                                {
                                    id_verre = new_verre.idVerre,
                                    id_traitement = t.idTraitement,

                                };
                                db.ligne_verre_traitement.Add(new_ligne_traitement);
                                db.SaveChanges();
                            }
                            
                            break;
                        //Lentille
                        case 2:
                            oeil new_oeil_lentille = new oeil()
                            {
                                ADD_D = int.Parse(ligne.od_add_lentille.Text),
                                ADD_G = int.Parse(ligne.og_add_lentille.Text),
                                AXE_D = int.Parse(ligne.od_axe_lentille.Text),
                                AXE_G = int.Parse(ligne.og_axe_lentille.Text),
                                CYL_D = int.Parse(ligne.od_cyl_lentille.Text),
                                CYL_G = int.Parse(ligne.og_cyl_lentille.Text),
                                SPH_D = int.Parse(ligne.od_sph_lentille.Text),
                                SPH_G = int.Parse(ligne.og_sph_lentille.Text)
                            };
                            db.oeils.Add(new_oeil_lentille);
                            db.SaveChanges();

                            //TypeLentille :
                            ligne_type_lentille newlignetypelentille = new ligne_type_lentille() {};
                            db.ligne_type_lentille.Add(newlignetypelentille);
                            db.SaveChanges();
                            switch ((int)ligne.typeLentilleText.SelectedValue)
                            {
                                //Lentille Torique
                                case 1:
                                    lentilletorique newLentilleTorique = new lentilletorique() {
                                        idTypeLentille = newlignetypelentille.idTypeLentille
                                    };
                                    db.lentilletoriques.Add(newLentilleTorique);
                                    break;
                                //Lentille MultiFocale
                                case 2:
                                    lentillemultifocale newLentilleMulti = new lentillemultifocale()
                                    {
                                        DOM = float.Parse(ligne.domText.Text),
                                        idTypeLentille = newlignetypelentille.idTypeLentille
                                    };
                                    db.lentillemultifocales.Add(newLentilleMulti);
                                    break;
                                //Lentille Spherique
                                case 3:
                                    lentillespherique newLentilleSph = new lentillespherique()
                                    {
                                        DIA = float.Parse(ligne.diaText.Text),
                                        RC = float.Parse(ligne.rcText.Text),
                                        idTypeLentille = newlignetypelentille.idTypeLentille
                                    };
                                    db.lentillespheriques.Add(newLentilleSph);
                                    break;
                            }
                            db.SaveChanges();

                            lentille new_lentille = new lentille()
                            {
                                idArticle = article.idArticle,
                                idOeil = new_oeil_lentille.idOeil,
                                Couleur = ligne.couleurLentille.Text,
                                idTypeLentille = (int)ligne.typeLentilleText.SelectedValue,
                                idLigneType = newlignetypelentille.idTypeLentille
                            };
                            db.lentilles.Add(new_lentille);
                            db.SaveChanges();
                            foreach (traitement t in ligne.get_traitements())
                            {
                                try
                                {
                                    ligne_lentille_traitement new_ligne_traitement = new ligne_lentille_traitement()
                                    {
                                        id_lentille = new_lentille.idLentille,
                                        id_traitement = t.idTraitement
                                    };

                                    db.ligne_lentille_traitement.Add(new_ligne_traitement);
                                    db.SaveChanges();
                                }
                                catch(Exception ex)
                                {
                                    MessageBox.Show(ex.ToString());
                                }
                                
                            }
                            break;
                         //Cadre
                        case 3:
                            cadre new_cadre = new cadre()
                            {
                                DiametreVerre = int.Parse(ligne.diametreText.Text),
                                HauteurVerre = int.Parse(ligne.hautteur_verre_text.Text),
                                LongeurBrache = int.Parse(ligne.langeur_brance_text.Text),
                                Pont = int.Parse(ligne.pontText.Text),
                                Largeur = int.Parse(ligne.largeurText.Text),
                                idArticle = article.idArticle,
                                Couleur = ligne.couleurText.Text
                            };
                            db.cadres.Add(new_cadre);
                            db.SaveChanges();
                            break;
                    }
                    

                    // if article is already in stock


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
    }
}
