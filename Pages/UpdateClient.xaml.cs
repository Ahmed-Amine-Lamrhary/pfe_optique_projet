using MenuWithSubMenu.Model;
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
    /// Logique d'interaction pour UpdateClient.xaml
    /// </summary>
    public partial class UpdateClient : Page
    {
        string cin;
        dbEntities db;
        public UpdateClient(client c)
        {
            InitializeComponent();
            db = new dbEntities();
            cinText.Text = c.cin;
            this.cin = c.cin;
            nomText.Text = c.nom;
            prenomText.Text = c.prenom;
            adresseText.Text = c.adresse;
            telText.Text = c.telephone;
            emailText.Text = c.email;
        }


        public UpdateClient()
        {
            InitializeComponent();
        }


        private void updateClient(object sender, RoutedEventArgs e)
        {
            if (!validateForm())
            {
                HandyControl.Controls.MessageBox.Show("Veuillez remplir tout les champs");
                return;
            }
            client clientUpdated = db.clients.Where(client => client.cin == cin).SingleOrDefault();
            try
            {
                clientUpdated.nom = nomText.Text;
                clientUpdated.prenom = prenomText.Text;
                clientUpdated.adresse = adresseText.Text;
                clientUpdated.telephone = telText.Text;
                clientUpdated.email = emailText.Text;

                db.SaveChanges();

                foreach (Window window in Application.Current.Windows)
                {
                    if (window.GetType() == typeof(MainWindow))
                    {
                        (window as MainWindow).MainWindowFrame.Navigate(new Uri(string.Format("{0}", "Pages/EspaceClient.xaml"), UriKind.RelativeOrAbsolute));
                    }
                }

            }
            catch (Exception exc)
            {
                Console.Write(exc.Message);
                HandyControl.Controls.MessageBox.Show(exc.Message);

            }

        }

        //validation
        private Boolean validateForm()
        {
            if (cinText.Text.Length != 0 &&
                nomText.Text.Length != 0 &&
                emailText.Text.Length != 0 &&
                adresseText.Text.Length != 0 &&
                prenomText.Text.Length != 0 &&
                telText.Text.Length != 0
                )
                return true;
            return false;
        }
    }
}
