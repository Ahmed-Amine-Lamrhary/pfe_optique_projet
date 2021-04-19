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
using System.Windows.Shapes;

namespace MenuWithSubMenu
{
    public partial class Configuration : Window
    {
        dbEntities db;

        public Configuration()
        {
            InitializeComponent();
            db = new dbEntities();

            WindowStartupLocation = WindowStartupLocation.CenterScreen;
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
                db.settings.Add(new setting()
                {
                    id = 1,
                    nom = nomText.Text,
                    email = emailText.Text,
                    telephone = telText.Text,
                    adresse = adresseText.Text,
                    password = passwordText.Password
                });

                db.SaveChanges();

                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                Close();
            }
            catch (Exception ex)
            {
                HandyControl.Controls.MessageBox.Show("Erreur: " + ex.Message);
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

    }
}
