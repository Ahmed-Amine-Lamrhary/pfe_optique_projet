using MenuWithSubMenu.Model;
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

namespace MenuWithSubMenu.PagesSettings
{
    public partial class Parametres : Page
    {
        Page prevPage;
        dbEntities db;
        setting settings;

        private async void getSettings()
        {
            settings = await db.settings.FirstOrDefaultAsync();

            if (settings != null)
            {
                nomText.Text = settings.nom.Trim();
                emailText.Text = settings.email.Trim();
                telText.Text = settings.telephone.Trim();
                adresseText.Text = settings.adresse.Trim();
                passwordText.Password = settings.password.Trim();
            }
        }

        public Parametres()
        {
            InitializeComponent();
            db = new dbEntities();
            returnBtn.Visibility = Visibility.Collapsed;

            getSettings();
        }

        public Parametres(Page prevP)
        {
            InitializeComponent();
            prevPage = prevP;
            db = new dbEntities();

            getSettings();
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
                        nom = nomText.Text.Trim(),
                        email = emailText.Text.Trim(),
                        telephone = telText.Text.Trim(),
                        adresse = adresseText.Text.Trim(),
                        password = passwordText.Password.Trim()
                    });
                } else
                {
                    settings.nom = nomText.Text.Trim();
                    settings.email = emailText.Text.Trim();
                    settings.telephone = telText.Text.Trim();
                    settings.adresse = adresseText.Text.Trim();
                    settings.password = passwordText.Password.Trim();
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
                telText.Text.Length != 0 &&
                passwordText.Password.Length != 0
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
