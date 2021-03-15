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
    public partial class AddOphta : Page
    {
        dbEntities db;

        public AddOphta()
        {
            InitializeComponent();

            db = new dbEntities();
        }

        private void addOphta(object sender, RoutedEventArgs e)
        {
            if (!validateForm())
            {
                HandyControl.Controls.MessageBox.Show("Veuillez remplir tout les champs");
                return;
            }

            try
            {
                db.ophtalmologues.Add(new ophtalmologue()
                {
                    nom = nomText.Text,
                    prenom = prenomText.Text,
                    adresse = adresseText.Text,
                    email = emailText.Text,
                    telephone = telText.Text,
                    inpe = inpeText.Text
                });
                db.SaveChanges();
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
            MyContext.navigateTo(new Ophtalmologues());
        }
    }
}
