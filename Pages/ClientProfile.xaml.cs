﻿using System;
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
        private Model.dbEntities db;
        private Model.client pageClient;
        private string clientCin;

        public ClientProfile(string clientCin)
        {
            InitializeComponent();
            this.clientCin = clientCin;
            db = new Model.dbEntities();

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
    }
}
