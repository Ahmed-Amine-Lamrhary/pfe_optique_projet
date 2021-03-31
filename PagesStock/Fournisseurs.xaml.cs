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
    public partial class Fournisseurs : Page
    {
        dbEntities db;
        public Fournisseurs()
        {
            InitializeComponent();

            db = new dbEntities();

            getFournisseurs();
        }

        private async void getFournisseurs()
        {
            loadingBox.Visibility = Visibility.Visible;
            fournisseursDataGrid.Visibility = Visibility.Hidden;

            try
            {
                fournisseursDataGrid.ItemsSource = await db.fournisseurs.ToListAsync();
            }
            catch (Exception exp)
            {
                Console.WriteLine(exp.Message);
            }
            finally
            {
                loadingBox.Visibility = Visibility.Hidden;
                fournisseursDataGrid.Visibility = Visibility.Visible;
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

        private void voirFournisseur(object sender, RoutedEventArgs e)
        {
            fournisseur fournisseurRow = fournisseursDataGrid.SelectedItem as fournisseur;
            int fournisseurId = fournisseurRow.idFournisseur;

            FournisseurProfile fournisseurProfile = new FournisseurProfile(fournisseurId, this);
            MyContext.navigateTo(fournisseurProfile);
        }

        private void updateFournisseur(object sender, RoutedEventArgs e)
        {

            fournisseur FournisseurRow = fournisseursDataGrid.SelectedItem as fournisseur;
            int FournisseurId = FournisseurRow.idFournisseur;
            UpdateFournisseur update = new UpdateFournisseur(db.fournisseurs.Where(Fournisseur => Fournisseur.idFournisseur == FournisseurId).SingleOrDefault(), this);
            MyContext.navigateTo(update);
        }
        private void deleteFournisseur(object sender, RoutedEventArgs e)
        {

            fournisseur fournisseurRow = fournisseursDataGrid.SelectedItem as fournisseur;
            db.fournisseurs.Remove(fournisseurRow);
            db.SaveChanges();
        }
        private void addFournisseur(object sender, RoutedEventArgs e)
        {
            AddFournisseur add_Fournisseur = new AddFournisseur(this);
            MyContext.navigateTo(add_Fournisseur);
            fournisseursDataGrid.Items.Refresh();
        }
    }
}
