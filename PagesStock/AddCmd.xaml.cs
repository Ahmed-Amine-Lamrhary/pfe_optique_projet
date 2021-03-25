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

                
                string refer = null;
                foreach (AddLigneCmdFournisseur ligne in lignesCmd)
                {



                    article article = null;
                    if (ligne.newRefText.Visibility == Visibility.Visible)
                    {
                        refer = ligne.newRefText.Text;
                        
                           
                        article = new article()
                        {
                            idCategorie = (int)ligne.categorie.SelectedValue,
                            QteDisponible = 0,
                            PrixUnitaire = int.Parse(ligne.prixText.Text),
                            Garantie = ligne.garantieText.Text,
                            Description = ligne.descText.Text,
                            idArticle = refer,
                        };
                        db.articles.Add(article);
                        db.SaveChanges();
                    }
                    else
                    {
                        refer = (string)ligne.referenceText.SelectedValue;
                        article = db.articles.Where(a => a.idArticle == refer).Single();
                    }


                    // if article is new
                    int selectedIemValue = (int)ligne.categorie.SelectedValue;
                    switch (selectedIemValue)
                    {
                        //Verre
                        case 1:

                            verre new_verre = new verre()
                            {
                                idTypeVerre = (int)ligne.typeVerresText.SelectedValue,
                                idArticle = article.idArticle,
                                Teinte = ligne.teintText.Text
                            };
                            db.verres.Add(new_verre);
                            db.SaveChanges();
                            //vision :
                            //Vision Loin & gauche:

                            vision vision_Loins_gauche = new vision()
                            {
                                add = float.Parse(ligne.og_add_loin_verre.Text),
                                cyl = float.Parse(ligne.og_cyl_loin_verre.Text),
                                axe = float.Parse(ligne.og_axe_loin_verre.Text),
                                sph = float.Parse(ligne.og_sph_loin_verre.Text),
                                gauche = true,
                                loin = true,
                                ecart = float.Parse(ligne.ecartLoinText_verre.Text),
                                hauteur = float.Parse(ligne.hauteurLoinText_verre.Text),
                                verre_idVerre = new_verre.idVerre,
                            };

                            db.visions.Add(vision_Loins_gauche);
                            db.SaveChanges();

                            //Vision Loin & droit:

                            vision vision_Loins_droit = new vision()
                            {
                                add = float.Parse(ligne.od_add_loin_verre.Text),
                                cyl = float.Parse(ligne.od_cyl_loin_verre.Text),
                                axe = float.Parse(ligne.od_axe_loin_verre.Text),
                                sph = float.Parse(ligne.od_sph_loin_verre.Text),
                                gauche = false,
                                loin = true,
                                ecart = float.Parse(ligne.ecartLoinText_verre.Text),
                                hauteur = float.Parse(ligne.hauteurLoinText_verre.Text),
                                verre_idVerre = new_verre.idVerre,
                            };

                            db.visions.Add(vision_Loins_droit);
                            db.SaveChanges();

                            //Vision pres & droit:

                            vision vision_pres_droit = new vision()
                            {
                                add = float.Parse(ligne.od_add_pres_verre.Text),
                                cyl = float.Parse(ligne.od_cyl_pres_verre.Text),
                                axe = float.Parse(ligne.od_axe_pres_verre.Text),
                                sph = float.Parse(ligne.od_sph_pres_verre.Text),
                                gauche = false,
                                loin = false,
                                ecart = float.Parse(ligne.ecartPresText_verre.Text),
                                hauteur = float.Parse(ligne.hauteurPresText_verre.Text),
                                verre_idVerre = new_verre.idVerre,
                            };

                            db.visions.Add(vision_pres_droit);
                            db.SaveChanges();

                            //Vision pres & gauche:

                            vision vision_pres_gauche = new vision()
                            {
                                add = float.Parse(ligne.og_add_pres_verre.Text),
                                cyl = float.Parse(ligne.og_cyl_pres_verre.Text),
                                axe = float.Parse(ligne.og_axe_pres_verre.Text),
                                sph = float.Parse(ligne.og_sph_pres_verre.Text),
                                gauche = true,
                                loin = false,
                                ecart = float.Parse(ligne.ecartPresText_verre.Text),
                                hauteur = float.Parse(ligne.hauteurPresText_verre.Text),
                                verre_idVerre = new_verre.idVerre,
                            };

                            db.visions.Add(vision_pres_gauche);
                            db.SaveChanges();
                           
                            foreach (traitement t in ligne.get_traitements())
                            {
                                ligne_traitement_verre new_ligne_traitement = new ligne_traitement_verre()
                                {
                                    verre_idVerre = new_verre.idVerre,
                                    traitement_idTraitement = t.idTraitement,
                                };
                                db.ligne_traitement_verre.Add(new_ligne_traitement);
                                db.SaveChanges();
                            }
                            
                            break;
                        //Lentille
                        case 2:

                            ligne_type_lentille newlignetypelentille = new ligne_type_lentille() { };
                            db.ligne_type_lentille.Add(newlignetypelentille);
                            db.SaveChanges();

                            lentille new_lentille = new lentille()
                            {
                                idArticle = article.idArticle,
                                Couleur = ligne.couleurLentille.Text,
                                idtype_lentille = (int)ligne.typeLentilleText.SelectedValue,
                                idLigneType = newlignetypelentille.idTypeLentille,
                            };
                            db.lentilles.Add(new_lentille);
                            db.SaveChanges();

                            //vision :
                            //Vision Loin & gauche:

                            vision vision_Loins_gauche_lentille = new vision()
                            {
                                add = float.Parse(ligne.og_add_loin_lentille.Text),
                                cyl = float.Parse(ligne.og_cyl_loin_lentille.Text),
                                axe = float.Parse(ligne.og_axe_loin_lentille.Text),
                                sph = float.Parse(ligne.og_sph_loin_lentille.Text),
                                gauche = true,
                                loin = true,
                                ecart = float.Parse(ligne.ecartLoinText_verre.Text),
                                hauteur = float.Parse(ligne.hauteurLoinText_verre.Text),
                                lentille_idLentille = new_lentille.idLentille,
                            };

                            db.visions.Add(vision_Loins_gauche_lentille);
                            db.SaveChanges();

                            //Vision Loin & droit:

                            vision vision_Loins_droit_lentille = new vision()
                            {
                                add = float.Parse(ligne.od_add_loin_lentille.Text),
                                cyl = float.Parse(ligne.od_cyl_loin_lentille.Text),
                                axe = float.Parse(ligne.od_axe_loin_lentille.Text),
                                sph = float.Parse(ligne.od_sph_loin_lentille.Text),
                                gauche = false,
                                loin = true,
                                ecart = float.Parse(ligne.ecartLoinText_lentille.Text),
                                hauteur = float.Parse(ligne.hauteurLoinText_lentille.Text),
                                lentille_idLentille = new_lentille.idLentille,
                            };

                            db.visions.Add(vision_Loins_droit_lentille);
                            db.SaveChanges();

                            //Vision pres & droit:

                            vision vision_pres_droit_lentille = new vision()
                            {
                                add = float.Parse(ligne.od_add_pres_lentille.Text),
                                cyl = float.Parse(ligne.od_cyl_pres_lentille.Text),
                                axe = float.Parse(ligne.od_axe_pres_lentille.Text),
                                sph = float.Parse(ligne.od_sph_pres_lentille.Text),
                                gauche = false,
                                loin = false,
                                ecart = float.Parse(ligne.ecartPresText_lentille.Text),
                                hauteur = float.Parse(ligne.hauteurPresText_lentille.Text),
                                lentille_idLentille = new_lentille.idLentille,
                            };

                            db.visions.Add(vision_pres_droit_lentille);
                            db.SaveChanges();

                            //Vision pres & gauche:

                            vision vision_pres_gauche_lentille = new vision()
                            {
                                add = float.Parse(ligne.og_add_pres_lentille.Text),
                                cyl = float.Parse(ligne.og_cyl_pres_lentille.Text),
                                axe = float.Parse(ligne.og_axe_pres_lentille.Text),
                                sph = float.Parse(ligne.og_sph_pres_lentille.Text),
                                gauche = true,
                                loin = false,
                                ecart = float.Parse(ligne.ecartPresText_lentille.Text),
                                hauteur = float.Parse(ligne.hauteurPresText_lentille.Text),
                                lentille_idLentille = new_lentille.idLentille,
                            };

                            db.visions.Add(vision_pres_gauche_lentille);
                            db.SaveChanges();
                            //TypeLentille :
                            
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

                            foreach (traitement t in ligne.get_traitements())
                            {
                                try
                                {
                                    ligne_traitement_lentille new_ligne_traitement = new ligne_traitement_lentille()
                                    {
                                        lentille_idLentille = new_lentille.idLentille,
                                        traitement_idTraitement = t.idTraitement
                                    };

                                    db.ligne_traitement_lentille.Add(new_ligne_traitement);
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
                MessageBox.Show("Votre Commande a été bien ajouté");
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
    }
}
