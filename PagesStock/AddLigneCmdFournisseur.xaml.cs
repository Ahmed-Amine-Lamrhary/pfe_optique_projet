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

namespace MenuWithSubMenu.PagesStock
{
    /// <summary>
    /// Logique d'interaction pour AddLigneCmd.xaml
    /// </summary>
    public partial class AddLigneCmdFournisseur : Page
    {
        dbEntities db;
        public AddLigneCmdFournisseur()
        {
            InitializeComponent();
            db = new dbEntities();
            fillComboBox();
           
        }
        public void selectItem(object sender, SelectionChangedEventArgs e)
        {
            int selectedIemValue = int.Parse(categorie.SelectedValue.ToString());
            switch (selectedIemValue)
            {
                case 1:
                    verrePanel.Visibility = Visibility.Visible;
                    lentillePanel.Visibility = Visibility.Collapsed;
                    cadrePanel.Visibility = Visibility.Collapsed;
                    break;
                case 2:
                    verrePanel.Visibility = Visibility.Collapsed;
                    lentillePanel.Visibility = Visibility.Visible;
                    cadrePanel.Visibility = Visibility.Collapsed;
                    break;
                case 3:
                    verrePanel.Visibility = Visibility.Collapsed;
                    lentillePanel.Visibility = Visibility.Collapsed;
                    cadrePanel.Visibility = Visibility.Visible;
                    break;
            }

        }
        private void fillComboBox()
        {
            List<categorie> categories = db.categories.Distinct().ToList();
            categorie.ItemsSource = categories;
            categorie.DisplayMemberPath = "Nom";
            categorie.SelectedValuePath = "idCategorie";

            List<typeverre> typeVerres = db.typeverres.Distinct().ToList();
            typeVerresText.ItemsSource = typeVerres;
            typeVerresText.DisplayMemberPath = "NomType";
            typeVerresText.SelectedValuePath = "idTypeVerre";

            List<typelentille> typeLentille = db.typelentilles.Distinct().ToList();
            typeLentilleText.ItemsSource = typeLentille;
            typeLentilleText.DisplayMemberPath = "NomType";
            typeLentilleText.SelectedValuePath = "idTypeLentille";
        }
        public void addTraitLentilleButton()
        {

        }
        public void addTraitVerreButton()
        {

        }
    }
}
