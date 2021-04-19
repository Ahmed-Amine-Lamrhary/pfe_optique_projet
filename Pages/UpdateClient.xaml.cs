using MenuWithSubMenu.Model;
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
    /// Logique d'interaction pour UpdateClient.xaml
    /// </summary>
    public partial class UpdateClient : Page
    {
        string cin;
        dbEntities db;
        private Page prevPage;

        public UpdateClient(client c, Page prevP)
        {
            InitializeComponent();
            db = new dbEntities();
            prevPage = prevP;
            cinText.Text = c.cin;
            cin = c.cin;
            nomText.Text = c.nom;
            prenomText.Text = c.prenom;
            adresseText.Text = c.adresse;
            telText.Text = c.telephone;
            emailText.Text = c.email;
            dateText.Text = c.dateNaissance + "";
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

                HandyControl.Controls.MessageBox.Show("Les informations ont été enregistré");
                MyContext.navigateTo(new EspaceClient());

            }
            catch (Exception exc)
            {
                Console.Write(exc.Message);
                HandyControl.Controls.MessageBox.Show("Erreur");

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
        private void ReturnBtn_Click(object sender, RoutedEventArgs e)
        {
            MyContext.navigateTo(prevPage);
        }
    }
}
