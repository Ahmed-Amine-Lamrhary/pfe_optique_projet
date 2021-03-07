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
    /// Interaction logic for NewUser.xaml
    /// </summary>
    public partial class NewUser : Page
    {
        dbEntities db;

        public NewUser()
        {
            InitializeComponent();

            db = new dbEntities();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            db.clients.Add(new client()
            {
                cin = cinText.Text,
                nom = nomText.Text,
                prenom = prenomText.Text,
                adresse = adresseText.Text,
                email = emailText.Text,
                telephone = telText.Text,
                dateNaissance = (DateTime)dateText.SelectedDate
            });
            db.SaveChanges();
        }
    }
}
