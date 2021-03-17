﻿using MenuWithSubMenu.Model;
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
    /// <summary>
    /// Logique d'interaction pour CmdsFournisseur.xaml
    /// </summary>
    public partial class CmdsFournisseur : Page
    {
        dbEntities db;
        List<cmdfournisseur> listCmdFourni;
        int count;

        public CmdsFournisseur()
        {

            InitializeComponent();

            db = new dbEntities();
            getCmdFourni(0);
        }

        private async void getCmdFourni(int skip)
        {
            loadingBox.Visibility = Visibility.Visible;
            cmdFourniDataGrid.Visibility = Visibility.Hidden;

            try
            {
                listCmdFourni = await db.cmdfournisseurs.ToListAsync();

                count = (int)Math.Ceiling((decimal)listCmdFourni.Count / 10);
                pagination.MaxPageCount = count;
                cmdFourniDataGrid.ItemsSource = listCmdFourni.Skip(skip).Take(10);

            }
            catch (Exception exp)
            {
                Console.WriteLine(exp.Message);
            }
            finally
            {
                loadingBox.Visibility = Visibility.Hidden;
                cmdFourniDataGrid.Visibility = Visibility.Visible;
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
            article articleRow = cmdFourniDataGrid.SelectedItem as article;
            int articleId = articleRow.idArticle;

            //ArticleAbout articleAbout = new ArticleAbout(articleId, this);


            //MyContext.navigateTo(articleAbout);
        }

        private void updateCmd(object sender, RoutedEventArgs e)
        {

            article articleRow = cmdFourniDataGrid.SelectedItem as article;
            int articleId = articleRow.idArticle;
            //UpdateArticle update = new UpdateArticle(db.articles.Where(article => article.idArticle == articleId).SingleOrDefault(), this);
            //MyContext.navigateTo(update);
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
            AddCmd add_cmd = new AddCmd();
            MyContext.navigateTo(add_cmd);
        }

        private void page_PageUpdated(object sender, HandyControl.Data.FunctionEventArgs<int> e)
        {

            getCmdFourni((e.Info - 1) * 10);
        }
    }
}
