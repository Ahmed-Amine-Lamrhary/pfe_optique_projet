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
    /// Logique d'interaction pour ClientProfile.xaml
    /// </summary>
    public partial class ClientProfile : Page
    {
        private dbEntities db;
        private client pageClient;
        private string clientCin;
        private Page prevPage;

        public ClientProfile(string clientCin, Page prevP)
        {
            InitializeComponent();
            this.clientCin = clientCin;
            db = new dbEntities();
            prevPage = prevP;

            getClientInformation();
        }

        public void getClientInformation()
        {
            // get client
            pageClient = db.clients.Where(client => client.cin == clientCin).SingleOrDefault();

            // information
            fullnameBox.Text = pageClient.nom;
            cinBox.Text = pageClient.cin;
            adresseBox.Text = pageClient.adresse;
            emailBox.Text = pageClient.email;
            telephoneBox.Text = pageClient.telephone;
            dateNaissanceBox.Text = pageClient.dateNaissance.ToString();
            if(pageClient.medicaments != null)
            {
                List<string> medsList = pageClient.medicaments.Split('@').ToList();
                meds.ItemsSource = medsList;
            }
            else
            {
                panelMeds.Visibility = Visibility.Collapsed;
            }
            if(pageClient.maladies != null)
            {
                List<string> maladiesList = pageClient.maladies.Split('@').ToList();
                maladies.ItemsSource = maladiesList;
            }
            else
            {
                panelMaladies.Visibility = Visibility.Collapsed; 
            }
            // get visites
            visiteData.ItemsSource = db.visites.Where(visite => visite.client_cin == clientCin).ToList();

            // get ordonnances
            odonnanceData.ItemsSource = db.ordonnances.Where(ordonnance => ordonnance.client_cin == clientCin).ToList();
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
