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
    public partial class AddFournisseur : Page
    {
        dbEntities db;
        Page prevPage;

        public AddFournisseur(Page prevPage)
        {
            InitializeComponent();

            this.prevPage = prevPage;
            db = new dbEntities();
        }

        private void addFournisseur(object sender, RoutedEventArgs e)
        {
            if (!validateForm())
            {
                HandyControl.Controls.MessageBox.Show("Veuillez remplir tout les champs");
                return;
            }

            try
            {
                db.fournisseurs.Add(new fournisseur()
                {
                    Nom = nomText.Text,
                    Adresse = adresseText.Text,
                    Email = emailText.Text,
                    Tel = telText.Text,
                    Description = descText.Text
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
