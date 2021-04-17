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

namespace MenuWithSubMenu.PagesSettings
{
    /// <summary>
    /// Interaction logic for Parametres.xaml
    /// </summary>
    public partial class Parametres : Page
    {
        Page prevPage;
        dbEntities db;
        setting settings;

        public Parametres()
        {
            InitializeComponent();
            db = new dbEntities();
            returnBtn.Visibility = Visibility.Collapsed;

            settings = db.settings.FirstOrDefault();
            if (settings != null)
            {
                nomText.Text = settings.nom;
                emailText.Text = settings.email;
                telText.Text = settings.telephone;
                adresseText.Text = settings.adresse;
            }
        }

        public Parametres(Page prevP)
        {
            InitializeComponent();
            prevPage = prevP;
            db = new dbEntities();

            settings = db.settings.FirstOrDefault();
            if (settings != null)
            {
                nomText.Text = settings.nom;
                emailText.Text = settings.email;
                telText.Text = settings.telephone;
                adresseText.Text = settings.adresse;
            }
        }

        private void saveSettings(object sender, RoutedEventArgs e)
        {
            if (!validateForm())
            {
                HandyControl.Controls.MessageBox.Show("Veuillez remplir tout les champs");
                return;
            }

            try
            {
                if (settings == null)
                {
                    db.settings.Add(new setting()
                    {
                        id = 1,
                        nom = nomText.Text,
                        email = emailText.Text,
                        telephone = telText.Text,
                        adresse = adresseText.Text
                    });
                } else
                {
                    settings.nom = nomText.Text;
                    settings.email = emailText.Text;
                    settings.telephone = telText.Text;
                    settings.adresse = adresseText.Text;
                }

                db.SaveChanges();
            } catch (Exception)
            {
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
            MyContext.navigateTo(prevPage);
        }
    }
}
