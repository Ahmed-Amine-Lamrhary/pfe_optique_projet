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
using System.Windows.Shapes;

namespace MenuWithSubMenu.Windows
{
    /// <summary>
    /// Interaction logic for ClientProfile.xaml
    /// </summary>
    public partial class ClientProfile : Window
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

            // get visites
            visiteData.ItemsSource = db.visites.Where(visite => visite.client_cin == clientCin).ToList();

            // get ordonnances
            odonnanceData.ItemsSource = db.ordonnances.Where(ordonnance => ordonnance.client_cin == clientCin).ToList();
        }
    }
}
