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

namespace MenuWithSubMenu.PagesStock
{
    public partial class UpdateFournisseur : Page
    {
        private dbEntities db;
        private fournisseur fournisseur;
        private Page pagePrev;

        public UpdateFournisseur(fournisseur fournisseur, Page pageP)
        {
            InitializeComponent();

            pagePrev = pageP;

            db = new dbEntities();
            this.fournisseur = fournisseur;

            nomText.Text = fournisseur.Nom;
            adresseText.Text = fournisseur.Adresse;
            telText.Text = fournisseur.Tel;
            emailText.Text = fournisseur.Email;
            descText.Text = fournisseur.Description;
        }

        private void updateFournisseur(object sender, RoutedEventArgs e)
        {
            if (!validateForm())
            {
                HandyControl.Controls.MessageBox.Show("Veuillez remplir tout les champs");
                return;
            }
            fournisseur fournisseurUpdated = db.fournisseurs.Where(fournisseur => fournisseur.idFournisseur == this.fournisseur.idFournisseur).SingleOrDefault();
            try
            {
                fournisseurUpdated.Nom = nomText.Text;
                fournisseurUpdated.Adresse = adresseText.Text;
                fournisseurUpdated.Tel = telText.Text;
                fournisseurUpdated.Email = emailText.Text;
                fournisseurUpdated.Description = descText.Text;

                db.SaveChanges();

                HandyControl.Controls.MessageBox.Show("Les informations ont été enregistré");
                MyContext.navigateTo(new Fournisseurs());
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
            if (nomText.Text.Length != 0 &&
                emailText.Text.Length != 0 &&
                adresseText.Text.Length != 0 &&
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
