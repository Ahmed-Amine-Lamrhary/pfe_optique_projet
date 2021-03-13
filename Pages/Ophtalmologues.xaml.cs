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

namespace MenuWithSubMenu.Pages
{
    /// <summary>
    /// Interaction logic for Inbox.xaml
    /// </summary>
    public partial class Inbox : Page
    {
        dbEntities db;
        public Inbox()
        {
            InitializeComponent();

            db = new dbEntities();

            getOphtalmologues();
        }

        private async void getOphtalmologues()
        {
            loadingBox.Visibility = Visibility.Visible;
            ophtalmologuesDataGrid.Visibility = Visibility.Hidden;

            try
            {
                ophtalmologuesDataGrid.ItemsSource = await db.ophtalmologues.ToListAsync();
            }
            catch (Exception exp)
            {
                Console.WriteLine(exp.Message);
            }
            finally
            {
                loadingBox.Visibility = Visibility.Hidden;
                ophtalmologuesDataGrid.Visibility = Visibility.Visible;
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

        private void voirOphtalmologue(object sender, RoutedEventArgs e)
        {
            ophtalmologue ophtaRow = ophtalmologuesDataGrid.SelectedItem as ophtalmologue;
            int ophtaId = ophtaRow.id;

            OphtaProfile ophtaProfile = new OphtaProfile(ophtaId);

            MyContext.navigateTo(ophtaProfile);
        }

        private void updateOphtalmologue(object sender, RoutedEventArgs e)
        {

            ophtalmologue ophtaRow = ophtalmologuesDataGrid.SelectedItem as ophtalmologue;
            int ophtaId = ophtaRow.id;
            UpdateOphta update = new UpdateOphta(db.ophtalmologues.Where(ophtalmologue => ophtalmologue.id == ophtaId).SingleOrDefault());
            MyContext.navigateTo(update);
        }
        private void deleteOphtalmologue(object sender, RoutedEventArgs e)
        {

            ophtalmologue ophtaRow = ophtalmologuesDataGrid.SelectedItem as ophtalmologue;
            db.ophtalmologues.Remove(ophtaRow);
            db.SaveChanges();
        }
        private void addOphtalmologue(object sender, RoutedEventArgs e)
        {
            AddOphta add_ophta = new AddOphta();
            MyContext.navigateTo(add_ophta);
            ophtalmologuesDataGrid.Items.Refresh();
        }
    }
}
