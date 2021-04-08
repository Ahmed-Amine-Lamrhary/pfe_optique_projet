﻿using iTextSharp.text;
using iTextSharp.text.pdf;
using MenuWithSubMenu.Model;
using MenuWithSubMenu.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

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

        int count;

        public CmdsClients()
        {
            InitializeComponent();

            db = new dbEntities();
            listCmdClients = new List<cmdclient>();
            selectedListCmdClients = new List<cmdclient>();

            getCmdClients(0);
        }

        private async void getCmdClients(int skip)
        {
            loadingBox.Visibility = Visibility.Visible;
            infoBox.Visibility = Visibility.Collapsed;
            nothingBox.Visibility = Visibility.Collapsed;

            try
            {
                if (startDate != null && endDate != null)
                {
                    listCmdClients = await db.cmdclients.Where(c => c.DateCmd <= endDate && c.DateCmd >= startDate).ToListAsync();
                }
                else if (startDate == null && endDate != null)
                    listCmdClients = await db.cmdclients.Where(c => c.DateCmd <= endDate).ToListAsync();
                else if (startDate != null && endDate == null)
                    listCmdClients = await db.cmdclients.Where(c => c.DateCmd >= startDate).ToListAsync();
                else
                    listCmdClients = await db.cmdclients.ToListAsync();

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
            int cmdId = cmdRow.idCmdClient;
            AddCmdClient update = new AddCmdClient(db.cmdclients.Where(cmd => cmd.idCmdClient == cmdId).SingleOrDefault());
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
            AddCmdClient add_cmd_client = new AddCmdClient();
            MyContext.navigateTo(add_cmd_client);
        }

        private void deleteCmds(object sender, RoutedEventArgs e)
        {

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
                        continue;

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
                            EtatCmd = ligne.EtatCmd,
                            idVerre = ligne.idVerre,
                            idVisite = null,
                            idLigneEntree = ligne.idLigne
                        });
                        db.SaveChanges();
                    }
                }
                
                if (i == 0)
                    throw new Exception();

                transaction.Commit();
                MessageBox.Show("Commandes sont passées au fournisseur");
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                MessageBox.Show(ex.ToString());
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
                        if (ligne.EtatCmd == "verified")
                            continue;

                        ligne.EtatCmd = "verified";
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
                                MessageBox.Show("La quantité d'article " + article.idArticle + " n'est pas suffisante");
                            }
                        }
                    }
                }

                transaction.Commit();
            }
            catch (Exception)
            {
                transaction.Rollback();
                MessageBox.Show("Erreur");
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
                    MessageBox.Show("start date must be less than end date");

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
                    db.cmdclients.Remove(cmdclient);
                    db.SaveChanges();
                }

                transaction.Commit();
            }
            catch (Exception)
            {
                transaction.Rollback();
                System.Windows.MessageBox.Show("Erreur");
            }
        }

        private void printCmd(object sender, RoutedEventArgs e)
        {
            try
            {
                // get information
                cmdclient commande = cmdClientsDataGrid.SelectedItem as cmdclient;
                List<ligneentree> lignes = db.ligneentrees.Where(l => l.idCmdClient == commande.idCmdClient).ToList();
                client client = db.clients.Where(c => c.cin == commande.client_cin).SingleOrDefault();
                float? totalPrice = 0;

                // pdf file
                var pdfPath = @"G:\Learning\facture.pdf";
                Document document = new Document(PageSize.A4, 10, 10, 10, 10);
                PdfWriter pdfWriter = PdfWriter.GetInstance(document, new FileStream(pdfPath, FileMode.Create));
                document.Open();

                // print Header
                PdfPTable headerTable = new PdfPTable(2);

                // print optic information
                PdfPCell opticInfo = new PdfPCell();
                opticInfo.AddElement(new Paragraph("OPTICA"));
                opticInfo.AddElement(new Paragraph("xxx Fes Ain Chkef"));
                opticInfo.AddElement(new Paragraph("+2126487457"));
                opticInfo.AddElement(new Paragraph("optica@gmail.com"));
                headerTable.AddCell(opticInfo);

                // print client information
                PdfPCell clientInfo = new PdfPCell();
                clientInfo.AddElement(new Paragraph("Facture à: " + client.nom + " " + client.prenom));
                clientInfo.AddElement(new Paragraph("CIN: " + client.cin));
                clientInfo.AddElement(new Paragraph("Adresse: " + client.adresse));
                clientInfo.AddElement(new Paragraph("Email: " + client.email));
                clientInfo.AddElement(new Paragraph("Telephone: " + client.telephone));
                headerTable.AddCell(clientInfo);

                // print lignes table
                PdfPTable articlesTable = new PdfPTable(4);
                articlesTable.AddCell(new PdfPCell(new Phrase("Article")));
                articlesTable.AddCell(new PdfPCell(new Phrase("Quantité")));
                articlesTable.AddCell(new PdfPCell(new Phrase("Prix unitaire")));
                articlesTable.AddCell(new PdfPCell(new Phrase("Prix total")));

                PdfPTable verresTable = new PdfPTable(1);

                foreach (ligneentree ligne in lignes)
                {
                    totalPrice += ligne.Prix_Total;

                    if (ligne.idArticle == null)
                    {
                        verre verre = db.verres.Where(v => v.idVerre == ligne.idVerre).SingleOrDefault();

                        List<ligne_traitement_verre> lignesTrait = db.ligne_traitement_verre.Where(l => l.verre_idVerre == verre.idVerre).ToList();

                        PdfPCell verreCell = new PdfPCell();
                        verreCell.AddElement(new Paragraph("Matière: " + verre.matiere));
                        verreCell.AddElement(new Paragraph("Indice: " + verre.indice));
                        verreCell.AddElement(new Paragraph("Géométrie: " + verre.geometrie));
                        verreCell.AddElement(new Paragraph("Teinte: " + verre.Teinte));
                        verreCell.AddElement(new Paragraph("Quantité: " + ligne.Qte_Commande));
                        verreCell.AddElement(new Paragraph("Prix total: " + ligne.Prix_Total));

                        if (lignesTrait != null && lignesTrait.Count() > 0)
                        {
                            verreCell.AddElement(new Paragraph("Traitements:"));
                            foreach (ligne_traitement_verre l in lignesTrait)
                            {
                                traitement traitement = db.traitements.Where(t => t.idTraitement == l.traitement_idTraitement).SingleOrDefault();
                                verreCell.AddElement(new Paragraph(traitement.Nom + " - Niveau: " + l.niveau));
                            }
                        }

                        verresTable.AddCell(verreCell);
                    } else
                    {
                        article article = db.articles.Where(a => a.idArticle == ligne.idArticle).SingleOrDefault();

                        articlesTable.AddCell(new PdfPCell(new Phrase(article.idArticle)));
                        articlesTable.AddCell(new PdfPCell(new Phrase(ligne.Qte_Commande + "")));
                        articlesTable.AddCell(new PdfPCell(new Phrase(article.PrixUnitaire + "")));
                        articlesTable.AddCell(new PdfPCell(new Phrase(ligne.Prix_Total + "")));
                    }
                }

                document.Add(headerTable);
                document.Add(new Paragraph("Montures: "));
                document.Add(articlesTable);
                document.Add(new Paragraph("Verres: "));
                document.Add(verresTable);
                document.Add(new Paragraph("Prix à payer: " + totalPrice + " DHS"));

                document.Close();
            } catch(Exception ex)
            {
                MessageBox.Show("Erreur: " + ex.Message);
            }
        }
    }
}
