using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.draw;
using MenuWithSubMenu.Model;
using MenuWithSubMenu.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Image = iTextSharp.text.Image;

namespace MenuWithSubMenu.PagesStock
{
    public partial class CmdsClients : Page
    {
        dbEntities db;
        List<cmdclient> listCmdClients;
        List<cmdclient> selectedListCmdClients;
        DbContextTransaction transaction;

        private List<cmdclient> checkedCmd = new List<cmdclient>();

        DateTime? startDate;
        DateTime? endDate;

        private Page prevPage;

        int count;

        public CmdsClients()
        {
            InitializeComponent();

            db = new dbEntities();
            listCmdClients = new List<cmdclient>();
            selectedListCmdClients = new List<cmdclient>();
            returnBtn.Visibility = Visibility.Collapsed;

            getCmdClients(0);
        }


        public CmdsClients(Page prevP)
        {
            InitializeComponent();

            db = new dbEntities();
            listCmdClients = new List<cmdclient>();
            selectedListCmdClients = new List<cmdclient>();
            prevPage = prevP;

            getCmdClients(0);
        }

        private async Task getCmdClients(int skip)
        {
            loadingBox.Visibility = Visibility.Visible;
            infoBox.Visibility = Visibility.Collapsed;
            nothingBox.Visibility = Visibility.Collapsed;

            try
            {
                if (startDate != null && endDate != null)
                    listCmdClients = await Task.Run(() => db.cmdclients.Where(c => c.DateCmd <= endDate && c.DateCmd >= startDate).ToList());
                else if (startDate == null && endDate != null)
                    listCmdClients = await Task.Run(() => db.cmdclients.Where(c => c.DateCmd <= endDate).ToList());
                else if (startDate != null && endDate == null)
                    listCmdClients = await Task.Run(() => db.cmdclients.Where(c => c.DateCmd >= startDate).ToList());
                else
                    listCmdClients = await Task.Run(() => db.cmdclients.ToList());

                if (listCmdClients.Count() == 0)
                {
                    nothingBox.Visibility = Visibility.Visible;
                    return;
                }

                // get states of orders
                foreach (cmdclient cmd in listCmdClients)
                {
                    List<ligneentree> lignes = await Task.Run(() => db.ligneentrees.Where(l => l.idCmdClient == cmd.idCmdClient).ToList());
                    int lignesNonPayee = 0;
                    foreach(ligneentree ligne in lignes)
                    {
                        if (ligne.EtatCmd == "Non payée")
                            lignesNonPayee++;
                    }

                    if (lignesNonPayee == 0)
                        cmd.etatCmd = "Payée";
                    else
                        cmd.etatCmd = "Non payée (" + lignesNonPayee + " lignes non payée)";
                }

                count = (int)Math.Ceiling((decimal)listCmdClients.Count / 10);
                pagination.MaxPageCount = count;
                cmdClientsDataGrid.ItemsSource = listCmdClients.Skip(skip).Take(10);

                infoBox.Visibility = Visibility.Visible;

                List<fournisseur> fournisseurs = db.fournisseurs.Distinct().ToList();
                fourList.ItemsSource = fournisseurs;
                fourList.DisplayMemberPath = "Nom";
                fourList.SelectedValuePath = "idFournisseur";
                fourList.SelectedIndex = 0;
            }
            catch (Exception exp)
            {
                System.Console.WriteLine(exp.Message);
                nothingBox.Visibility = Visibility.Visible;
            }
            finally
            {
                loadingBox.Visibility = Visibility.Collapsed;
            }
        }


        private void DataGrid_AutoGeneratedColumns(object sender, EventArgs e)
        {
            var grid = (DataGrid)sender;
            foreach (var item in grid.Columns)
            {
                if (item.Header.ToString() == "Edit")
                {
                    item.DisplayIndex = grid.Columns.Count - 1;
                    break;
                }
            }
        }

        private void AddPresetButton_Click(object sender, RoutedEventArgs e)
        {
            var addButton = sender as FrameworkElement;
            if (addButton != null)
            {
                addButton.ContextMenu.IsOpen = true;
            }
        }

        private void voirCmd(object sender, RoutedEventArgs e)
        {
            // cmdfournisseur cmd = cmdFourniDataGrid.SelectedItem as cmdfournisseur;
            // int cmdId = cmd.idCmdFournisseur;

            // CmdsLigne cmdAbout = new CmdsLigne(cmdId);


            // MyContext.navigateTo(cmdAbout);
        }

        private void updateCmd(object sender, RoutedEventArgs e)
        {
            cmdclient cmdRow = cmdClientsDataGrid.SelectedItem as cmdclient;
            AddCmdClient update = new AddCmdClient(cmdRow, this);
            MyContext.navigateTo(update);
        }
        
        private void deleteCmd(object sender, RoutedEventArgs e)
        {

            /*try
            {
                DbContextTransaction transaction = db.Database.BeginTransaction();
                article articleRow = cmdFourniDataGrid.SelectedItem as article;

                db.ordonnances.RemoveRange(db.ordonnances.Where(r => r.article_cin == articleRow.cin));
                db.SaveChanges();

                db.visites.RemoveRange(db.visites.Where(r => r.article_cin == articleRow.cin));
                db.SaveChanges();

                db.articles.Remove(articleRow);
                db.SaveChanges();

                transaction.Commit();

                System.Windows.MessageBox.Show("Success");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }*/


        }
        
        private void addCmd(object sender, RoutedEventArgs e)
        {
            AddCmdClient add_cmd_client = new AddCmdClient(this);
            MyContext.navigateTo(add_cmd_client);
        }

        private void page_PageUpdated(object sender, HandyControl.Data.FunctionEventArgs<int> e)
        {
            getCmdClients((e.Info - 1) * 10);
        }

        private void passerFournisseur(object sender, RoutedEventArgs e)
        {
            try
            {
                int i = 0; // how many orders are submitted

                transaction = db.Database.BeginTransaction();

                int idFour = (int)fourList.SelectedValue;
                cmdfournisseur cmdF = new cmdfournisseur()
                {
                    idFournisseur = idFour,
                    DateEntree = DateTime.Now
                };
                db.cmdfournisseurs.Add(cmdF);
                db.SaveChanges();

                foreach (cmdclient cmd in checkedCmd)
                {
                    if (cmd.idCmdFournisseur == null)
                        i++;
                    else
                    {
                        MessageBox.Show("La commande #" + cmd.idCmdClient + " est déja passée à un fournisseur");
                        continue;
                    }
                    
                    cmd.idCmdFournisseur = cmdF.idCmdFournisseur;
                    db.SaveChanges();

                    List<ligneentree> lignesCmds = db.ligneentrees.Where(l => l.idCmdClient == cmd.idCmdClient).ToList();

                    foreach (ligneentree ligne in lignesCmds)
                    {
                        db.lignecommandes.Add(new lignecommande()
                        {
                            idCmdFournisseur = cmdF.idCmdFournisseur,
                            idArticle = ligne.idArticle,
                            idCmdClient = ligne.idCmdClient,
                            Date_Commande = ligne.Date_Commande,
                            Adresse_Commande = ligne.Adresse_Commande,
                            Qte_Commande = ligne.Qte_Commande,
                            Prix_Total = (float?)ligne.Prix_Total,
                            EtatCmd = "Non Livrée",
                            idVerre = ligne.idVerre,
                            idVisite = ligne.idVisite,
                            idLigneEntree = ligne.idLigne
                        });
                        db.SaveChanges();
                    }
                }
                
                if (i == 0)
                    throw new Exception();

                transaction.Commit();
                HandyControl.Controls.MessageBox.Show("Commandes sont bien passées au fournisseur");

                groupInfo.Visibility = Visibility.Collapsed;
                getCmdClients(0);
            }
            catch (Exception ex)
            {
                transaction.Rollback();
            }
        }

        private void checkCmd(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = (CheckBox)e.OriginalSource;
            DataGridRow dataGridRow = VisualTreeHelpers.FindAncestor<DataGridRow>(checkBox);
            cmdclient cmd = (cmdclient)dataGridRow.DataContext;

            checkedCmd.Add(cmd);

            if (checkedCmd.Count() > 0)
                groupInfo.Visibility = Visibility.Visible;
            else
                groupInfo.Visibility = Visibility.Collapsed;
        }

        private void unCheckCmd(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = (CheckBox)e.OriginalSource;
            DataGridRow dataGridRow = VisualTreeHelpers.FindAncestor<DataGridRow>(checkBox);
            cmdclient cmd = (cmdclient)dataGridRow.DataContext;

            checkedCmd.Remove(cmd);

            if (checkedCmd.Count() > 0)
                groupInfo.Visibility = Visibility.Visible;
            else
                groupInfo.Visibility = Visibility.Collapsed;
        }

        private void verifyOrders(object sender, RoutedEventArgs e)
        {
            try
            {
                transaction = db.Database.BeginTransaction();

                foreach (cmdclient cmd in checkedCmd)
                {
                    List<ligneentree> lignes = db.ligneentrees.Where(l => l.idCmdClient == cmd.idCmdClient).ToList();

                    foreach (ligneentree ligne in lignes)
                    {
                        if (ligne.EtatCmd == "Payée")
                            continue;

                        ligne.EtatCmd = "Payée";
                        db.SaveChanges();

                        if (ligne.idArticle != null)
                        {
                            article article = db.articles.Where(a => a.idArticle == ligne.idArticle).SingleOrDefault();

                            if (ligne.Qte_Commande <= article.QteDisponible)
                            {
                                article.QteDisponible -= ligne.Qte_Commande;
                                db.SaveChanges();
                            } else
                            {
                                HandyControl.Controls.MessageBox.Show("La quantité d'article " + article.idArticle + " n'est pas suffisante");
                            }
                        }
                    }
                }

                transaction.Commit();

                groupInfo.Visibility = Visibility.Collapsed;

                MyContext.navigateTo(new CmdsClients());
            }
            catch (Exception)
            {
                transaction.Rollback();
                HandyControl.Controls.MessageBox.Show("Erreur");
            }
        }

        private void filterByDate(object sender, RoutedEventArgs e)
        {
            try
            {
                startDate = (DateTime)filterStartDate.SelectedDate;
                endDate = (DateTime)filterEndDate.SelectedDate;

                filterByDateFunc();
            }
            catch (Exception)
            {
                startDate = endDate = null;
                getCmdClients(0);
            }
        }

        private void resetFilter(object sender, RoutedEventArgs e)
        {
            startDate = endDate = null;
            filterStartDate.SelectedDate = filterEndDate.SelectedDate = null;

            getCmdClients(0);
        }

        private void deleteManualDate(object sender, RoutedEventArgs e)
        {
            lastDate.SelectedIndex = -1;
            manuallyDate.Visibility = Visibility.Collapsed;
            autoDate.IsEnabled = true;
        }

        private void selectLastDate(object sender, SelectionChangedEventArgs e)
        {
            endDate = DateTime.Now;

            switch (lastDate.SelectedIndex)
            {
                // last day
                case 0:
                    startDate = DateTime.Today.AddDays(-1);
                    break;
                // last week
                case 1:
                    startDate = DateTime.Today.AddDays(-7);
                    break;
                // last month
                case 2:
                    startDate = DateTime.Today.AddMonths(-1);
                    break;
                // last year
                case 3:
                    startDate = DateTime.Today.AddYears(-1);
                    break;
                // manually
                case 4:
                    manuallyDate.Visibility = Visibility.Visible;
                    autoDate.IsEnabled = false;
                    return;
                case -1:
                    return;
            }

            filterByDateFunc();
        }

        private void filterByDateFunc()
        {
            try
            {
                if (startDate <= endDate)
                    getCmdClients(0);
                else
                {
                    HandyControl.Controls.MessageBox.Show("La date de début doit être inférieure à la date de fin");

                    startDate = endDate = null;
                    filterStartDate.SelectedDate = filterEndDate.SelectedDate = null;
                }
            }
            catch (Exception)
            {
                startDate = endDate = null;
                getCmdClients(0);
            }
        }

        private void deleteMany(object sender, RoutedEventArgs e)
        {
            DbContextTransaction transaction = db.Database.BeginTransaction();
            try
            {
                foreach (cmdclient cmdclient in checkedCmd)
                {
                    List<ligneentree> lignesE =  db.ligneentrees.Where(l => l.idCmdClient == cmdclient.idCmdClient).ToList();

                    foreach (ligneentree li in lignesE)
                    {
                        lignecommande ligneC = db.lignecommandes.Where(l => l.idLigneEntree == li.idLigne).SingleOrDefault();
                        if (ligneC != null)
                            db.lignecommandes.Remove(ligneC);
                    }

                    db.cmdclients.Remove(cmdclient);
                    db.SaveChanges();

                    // remove commande fournisseur
                    List<lignecommande> lignesCF = db.lignecommandes.Where(l => l.idCmdFournisseur == cmdclient.idCmdFournisseur).ToList();
                    if (lignesCF == null || lignesCF.Count == 0)
                    {
                        List<cmdclient> cmdCList = db.cmdclients.Where(c => c.idCmdFournisseur == cmdclient.idCmdFournisseur).ToList();
                        if (cmdCList != null && cmdCList.Count > 0)
                        {
                            foreach (cmdclient c in cmdCList)
                            {
                                c.idCmdFournisseur = null;
                                db.SaveChanges();
                            }
                        }

                        db.cmdfournisseurs.Remove(db.cmdfournisseurs.Where(c => c.idCmdFournisseur == cmdclient.idCmdFournisseur).SingleOrDefault());
                        db.SaveChanges();
                    }

                }

                transaction.Commit();

                groupInfo.Visibility = Visibility.Collapsed;
                getCmdClients(0);
            }
            catch (Exception)
            {
                transaction.Rollback();
                HandyControl.Controls.MessageBox.Show("Erreur");
            }
        }

        private void printCmd(object sender, RoutedEventArgs e)
        {
            try
            {
                // get information
                cmdclient commande = cmdClientsDataGrid.SelectedItem as cmdclient;
                List<ligneentree> lignesVerres = db.ligneentrees.Where(l => l.idCmdClient == commande.idCmdClient && l.idArticle == null).ToList();
                List<ligneentree> lignesArticles = db.ligneentrees.Where(l => l.idCmdClient == commande.idCmdClient && l.idArticle != null).ToList();
                List<ligneentree> lignesLunettes = new List<ligneentree>();
                List<ligneentree> lignesMontures = new List<ligneentree>();
                foreach (ligneentree ligne in lignesArticles)
                {
                    if (db.articles.Where(a => a.idArticle == ligne.idArticle).SingleOrDefault().idCategorie == 2)
                        lignesLunettes.Add(ligne);
                    else if (db.articles.Where(a => a.idArticle == ligne.idArticle).SingleOrDefault().idCategorie == 3)
                        lignesMontures.Add(ligne);
                }

                client client = db.clients.Where(c => c.cin == commande.client_cin).SingleOrDefault();
                float? totalPrice = 0;

                // pdf file
                // var pdfPath = @"G:\Learning\facture.pdf";
                string pdfPath = System.IO.Path.GetTempPath() + Guid.NewGuid().ToString() + ".pdf";

                Document document = new Document(PageSize.A4, 10, 10, 10, 10);
                PdfWriter pdfWriter = PdfWriter.GetInstance(document, new FileStream(pdfPath, FileMode.Create));
                // PdfWriter pdfWriter = PdfWriter.GetInstance(document, myMemoryStream);
                document.Open();

                // print Header
                PdfPTable headerTable;

                

                setting opticSettings = db.settings.FirstOrDefault();
                if (opticSettings != null)
                {
                    headerTable = new PdfPTable(2) { WidthPercentage = 100f };

                    PdfPCell opticInfo = new PdfPCell()
                    {
                        Border = Rectangle.NO_BORDER
                    };

                    opticInfo.AddElement(new Paragraph(opticSettings.nom, FontFactory.GetFont("Poppins", 14)));
                    opticInfo.AddElement(new Paragraph(opticSettings.adresse, FontFactory.GetFont("Poppins", 9)));
                    opticInfo.AddElement(new Paragraph(opticSettings.telephone, FontFactory.GetFont("Poppins", 9)));
                    opticInfo.AddElement(new Paragraph(opticSettings.email, FontFactory.GetFont("Poppins", 9)));
                    headerTable.AddCell(opticInfo);
                } else
                {
                    headerTable = new PdfPTable(1) { WidthPercentage = 100f };
                }

                
                PdfPCell invoiceInfo = new PdfPCell() { Border = Rectangle.NO_BORDER };
                invoiceInfo.AddElement(new Paragraph("FACTURE", FontFactory.GetFont("Poppins", 17, Font.BOLD)));
                invoiceInfo.AddElement(new Paragraph("Date " + commande.DateCmd, FontFactory.GetFont("Poppins", 9)));
                invoiceInfo.AddElement(new Paragraph("Facture # " + commande.idCmdClient, FontFactory.GetFont("Poppins", 9)));
                headerTable.AddCell(invoiceInfo);

                // client info
                PdfPTable clientTable = new PdfPTable(1) { WidthPercentage = 100f };
                PdfPCell clientInfo = new PdfPCell()
                {
                    Border = Rectangle.NO_BORDER,
                    HorizontalAlignment = Element.ALIGN_RIGHT
                };

                Font headingFont = new Font(Font.FontFamily.HELVETICA, 12.0f, Font.BOLD, BaseColor.WHITE);
                Font thFont = new Font(Font.FontFamily.HELVETICA, 10.0f, Font.BOLD);
                Font tdFont = new Font(Font.FontFamily.HELVETICA, 10.0f);

                PdfPTable clientHeading = new PdfPTable(1) { WidthPercentage = 100f, SpacingAfter = 10, SpacingBefore = 15 };
                clientHeading.AddCell(new PdfPCell(new Paragraph("Facture à", headingFont)) { BackgroundColor = new BaseColor(85, 168, 253), Border = Rectangle.NO_BORDER });

                clientInfo.AddElement(new Paragraph(client.nom + " " + client.prenom, FontFactory.GetFont("Poppins", 9)));
                clientInfo.AddElement(new Paragraph("CIN: " + client.cin, FontFactory.GetFont("Poppins", 9)));
                clientInfo.AddElement(new Paragraph("Adresse: " + client.adresse, FontFactory.GetFont("Poppins", 9)));
                clientInfo.AddElement(new Paragraph("Email: " + client.email, FontFactory.GetFont("Poppins", 9)));
                clientInfo.AddElement(new Paragraph("Telephone: " + client.telephone, FontFactory.GetFont("Poppins", 9)));
                clientTable.AddCell(clientInfo);

                headerTable.SpacingAfter = 20;
                document.Add(headerTable);

                document.Add(clientHeading);
                document.Add(clientTable);

                // montures
                if (lignesMontures != null && lignesMontures.Count() > 0)
                {
                    PdfPTable monturesTable = new PdfPTable(4) { WidthPercentage = 100f };

                    PdfPTable monturesHeading = new PdfPTable(1) { WidthPercentage = 100f, SpacingAfter = 10, SpacingBefore = 15 };
                    monturesHeading.AddCell(new PdfPCell(new Paragraph("Montures", headingFont)) { BackgroundColor = new BaseColor(85, 168, 253), Border = Rectangle.NO_BORDER });


                    monturesTable.AddCell(new PdfPCell(new Paragraph("Article", thFont)) { BorderColor = new BaseColor(232, 235, 238) });
                    monturesTable.AddCell(new PdfPCell(new Paragraph("Quantité", thFont)) { BorderColor = new BaseColor(232, 235, 238) });
                    monturesTable.AddCell(new PdfPCell(new Paragraph("Prix unitaire", thFont)) { BorderColor = new BaseColor(232, 235, 238) });
                    monturesTable.AddCell(new PdfPCell(new Paragraph("Prix total", thFont)) { BorderColor = new BaseColor(232, 235, 238) });

                    foreach (ligneentree ligne in lignesMontures)
                    {
                        totalPrice += ligne.Prix_Total;

                        article article = db.articles.Where(a => a.idArticle == ligne.idArticle).SingleOrDefault();

                        monturesTable.AddCell(new PdfPCell(new Paragraph(article.idArticle, tdFont)) { BorderColor = new BaseColor(232, 235, 238) });
                        monturesTable.AddCell(new PdfPCell(new Paragraph(ligne.Qte_Commande + "", tdFont)) { BorderColor = new BaseColor(232, 235, 238) });
                        monturesTable.AddCell(new PdfPCell(new Paragraph(article.PrixUnitaire + " DHS", tdFont)) { BorderColor = new BaseColor(232, 235, 238) });
                        monturesTable.AddCell(new PdfPCell(new Paragraph(ligne.Prix_Total + " DHS", tdFont)) { BorderColor = new BaseColor(232, 235, 238) });
                    }

                    document.Add(monturesHeading);
                    document.Add(monturesTable);
                }

                // lunettes solaires
                if (lignesLunettes != null && lignesLunettes.Count() > 0)
                {
                    PdfPTable lunettesTable = new PdfPTable(4) { WidthPercentage = 100f };

                    PdfPTable lunettesHeading = new PdfPTable(1) { WidthPercentage = 100f, SpacingAfter = 10, SpacingBefore = 15 };
                    lunettesHeading.AddCell(new PdfPCell(new Paragraph("Lunettes Solaires", headingFont)) { BackgroundColor = new BaseColor(85, 168, 253), Border = Rectangle.NO_BORDER });


                    lunettesTable.AddCell(new PdfPCell(new Paragraph("Article", thFont)) { BorderColor = new BaseColor(232, 235, 238) });
                    lunettesTable.AddCell(new PdfPCell(new Paragraph("Quantité", thFont)) { BorderColor = new BaseColor(232, 235, 238) });
                    lunettesTable.AddCell(new PdfPCell(new Paragraph("Prix unitaire", thFont)) { BorderColor = new BaseColor(232, 235, 238) });
                    lunettesTable.AddCell(new PdfPCell(new Paragraph("Prix total", thFont)) { BorderColor = new BaseColor(232, 235, 238) });

                    foreach (ligneentree ligne in lignesLunettes)
                    {
                        totalPrice += ligne.Prix_Total;

                        article article = db.articles.Where(a => a.idArticle == ligne.idArticle).SingleOrDefault();

                        lunettesTable.AddCell(new PdfPCell(new Paragraph(article.idArticle, tdFont)) { BorderColor = new BaseColor(232, 235, 238) });
                        lunettesTable.AddCell(new PdfPCell(new Paragraph(ligne.Qte_Commande + "", tdFont)) { BorderColor = new BaseColor(232, 235, 238) });
                        lunettesTable.AddCell(new PdfPCell(new Paragraph(article.PrixUnitaire + " DHS", tdFont)) { BorderColor = new BaseColor(232, 235, 238) });
                        lunettesTable.AddCell(new PdfPCell(new Paragraph(ligne.Prix_Total + " DHS", tdFont)) { BorderColor = new BaseColor(232, 235, 238) });
                    }

                    document.Add(lunettesHeading);
                    document.Add(lunettesTable);
                }

                // verres
                if (lignesVerres != null && lignesVerres.Count() > 0)
                {
                    PdfPTable verresTable = new PdfPTable(6) { WidthPercentage = 100f };

                    PdfPTable verresHeading = new PdfPTable(1) { WidthPercentage = 100f, SpacingAfter = 10, SpacingBefore = 15 };
                    verresHeading.AddCell(new PdfPCell(new Paragraph("Verres", headingFont)) { BackgroundColor = new BaseColor(85, 168, 253), Border = Rectangle.NO_BORDER });

                    foreach (ligneentree ligne in lignesVerres)
                    {
                        totalPrice += ligne.Prix_Total;

                        verre verre = db.verres.Where(v => v.idVerre == ligne.idVerre).SingleOrDefault();
                        List<ligne_traitement_verre> lignesTrait = db.ligne_traitement_verre.Where(l => l.verre_idVerre == verre.idVerre).ToList();

                        verresTable.AddCell(new PdfPCell(new Paragraph("Matière", thFont)) { BorderColor = new BaseColor(232, 235, 238) });
                        verresTable.AddCell(new PdfPCell(new Paragraph("Indice", thFont)) { BorderColor = new BaseColor(232, 235, 238) });
                        verresTable.AddCell(new PdfPCell(new Paragraph("Géométrie", thFont)) { BorderColor = new BaseColor(232, 235, 238) });
                        verresTable.AddCell(new PdfPCell(new Paragraph("Teinte", thFont)) { BorderColor = new BaseColor(232, 235, 238) });
                        verresTable.AddCell(new PdfPCell(new Paragraph("Quantité", thFont)) { BorderColor = new BaseColor(232, 235, 238) });
                        verresTable.AddCell(new PdfPCell(new Paragraph("Prix total", thFont)) { BorderColor = new BaseColor(232, 235, 238) });

                        verresTable.AddCell(new PdfPCell(new Paragraph(verre.matiere, tdFont)) { BorderColor = new BaseColor(232, 235, 238) });
                        verresTable.AddCell(new PdfPCell(new Paragraph(verre.indice + "", tdFont)) { BorderColor = new BaseColor(232, 235, 238) });
                        verresTable.AddCell(new PdfPCell(new Paragraph(verre.geometrie, tdFont)) { BorderColor = new BaseColor(232, 235, 238) });
                        verresTable.AddCell(new PdfPCell(new Paragraph(verre.Teinte, tdFont)) { BorderColor = new BaseColor(232, 235, 238) });
                        verresTable.AddCell(new PdfPCell(new Paragraph(ligne.Qte_Commande + "", tdFont)) { BorderColor = new BaseColor(232, 235, 238) });
                        verresTable.AddCell(new PdfPCell(new Paragraph(ligne.Prix_Total + " DHS", tdFont)) { BorderColor = new BaseColor(232, 235, 238) });

                        // traitements
                        if (lignesTrait != null && lignesTrait.Count() > 0)
                        {
                            verresTable.AddCell(new PdfPCell(new Paragraph("Traitements", thFont)) { BorderColor = new BaseColor(232, 235, 238), Colspan = 6 });

                            foreach (ligne_traitement_verre l in lignesTrait)
                            {
                                traitement traitement = db.traitements.Where(t => t.idTraitement == l.traitement_idTraitement).SingleOrDefault();
                                verresTable.AddCell(new PdfPCell(new Paragraph(traitement.Nom, tdFont)) { BorderColor = new BaseColor(232, 235, 238), Colspan = 3 });
                                verresTable.AddCell(new PdfPCell(new Paragraph(l.niveau + "", tdFont)) { BorderColor = new BaseColor(232, 235, 238), Colspan = 3 });
                            }
                        }

                        verresTable.AddCell(new PdfPCell(new Paragraph("")) { Colspan = 6, MinimumHeight = 15, Border = Rectangle.NO_BORDER });
                    }

                    verresTable.SpacingAfter = 50;
                    document.Add(verresHeading);
                    document.Add(verresTable);
                }

                double frais = commande.frais == null ? 0 : (double)commande.frais;

                document.Add(new Chunk(new LineSeparator(1f, 100f, new BaseColor(232, 235, 238), Element.ALIGN_CENTER, 0)));
                document.Add(new Paragraph("Frais: " + frais + " DHS", FontFactory.GetFont("Poppins", 12)));
                document.Add(new Paragraph("Prix à payer: " + (totalPrice + frais) + " DHS", FontFactory.GetFont("Poppins", 12, Font.BOLD)));

                document.Close();

                System.Diagnostics.Process.Start(pdfPath);
            }
            catch (Exception ex)
            {
                HandyControl.Controls.MessageBox.Show("Erreur");
            }
        }

        private void ReturnBtn_Click(object sender, RoutedEventArgs e)
        {
            MyContext.navigateTo(prevPage);
        }
    }
}
