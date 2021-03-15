﻿using MenuWithSubMenu.Model;
using MenuWithSubMenu.Utils;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for OphtaProfile.xaml
    /// </summary>
    public partial class OphtaProfile : Page
    {
        private dbEntities db;
        private ophtalmologue pageOphta;
        private int ophtaId;
        private Page prevPage;

        public OphtaProfile(int ophtaId, Page prevP)
        {
            InitializeComponent();

            this.ophtaId = ophtaId;
            prevPage = prevP;

            db = new dbEntities();

            getOphtaInformation();
        }

        public void getOphtaInformation()
        {
            // get client
            pageOphta = db.ophtalmologues.Where(ophtalmologue => ophtalmologue.id == this.ophtaId).SingleOrDefault();

            // information
            nomBox.Text = pageOphta.nom;
            prenomBox.Text = pageOphta.prenom;
            adresseBox.Text = pageOphta.adresse;
            emailBox.Text = pageOphta.email;
            telephoneBox.Text = pageOphta.telephone;
            inpeBox.Text = pageOphta.inpe;

            // get ordonnances
            odonnanceData.ItemsSource = db.ordonnances.Where(ordonnance => ordonnance.ophtalmologue_ophtalmologueId == this.ophtaId).ToList();
        }

        private void voirOrdonnance(object sender, RoutedEventArgs e)
        {
            ordonnance ordonnanceRow = odonnanceData.SelectedItem as ordonnance;
            int ordonnanceId = ordonnanceRow.id;

            AboutOrdonnance aboutOrdonnance = new AboutOrdonnance(ordonnanceId, this);

            MyContext.navigateTo(aboutOrdonnance);
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

        private void ReturnBtn_Click(object sender, RoutedEventArgs e)
        {
            MyContext.navigateTo(prevPage);
        }
    }
}
