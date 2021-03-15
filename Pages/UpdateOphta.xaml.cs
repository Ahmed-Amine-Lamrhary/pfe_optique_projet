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
    /// Interaction logic for UpdateOphta.xaml
    /// </summary>
    public partial class UpdateOphta : Page
    {
        private dbEntities db;
        private ophtalmologue ophtalmologue;
        private Page pagePrev;

        public UpdateOphta(ophtalmologue ophtalmologue, Page pageP)
        {
            InitializeComponent();

            pagePrev = pageP;

            db = new dbEntities();
            this.ophtalmologue = ophtalmologue;

            nomText.Text = ophtalmologue.nom;
            prenomText.Text = ophtalmologue.prenom;
            adresseText.Text = ophtalmologue.adresse;
            telText.Text = ophtalmologue.telephone;
            emailText.Text = ophtalmologue.email;
            inpeText.Text = ophtalmologue.inpe;
        }

        private void updateOphta(object sender, RoutedEventArgs e)
        {
            if (!validateForm())
            {
                HandyControl.Controls.MessageBox.Show("Veuillez remplir tout les champs");
                return;
            }
            ophtalmologue ophtalmologueUpdated = db.ophtalmologues.Where(ophtalmologue => ophtalmologue.id == this.ophtalmologue.id).SingleOrDefault();
            try
            {
                ophtalmologueUpdated.nom = nomText.Text;
                ophtalmologueUpdated.prenom = prenomText.Text;
                ophtalmologueUpdated.adresse = adresseText.Text;
                ophtalmologueUpdated.telephone = telText.Text;
                ophtalmologueUpdated.email = emailText.Text;
                ophtalmologueUpdated.inpe = inpeText.Text;

                db.SaveChanges();

                foreach (Window window in Application.Current.Windows)
                {
                    if (window.GetType() == typeof(MainWindow))
                    {
                        (window as MainWindow).MainWindowFrame.Navigate(new Uri(string.Format("{0}", "Pages/Ophtalmologues.xaml"), UriKind.RelativeOrAbsolute));
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
            if (inpeText.Text.Length != 0 &&
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
            MyContext.navigateTo(pagePrev);
        }


    }
}
