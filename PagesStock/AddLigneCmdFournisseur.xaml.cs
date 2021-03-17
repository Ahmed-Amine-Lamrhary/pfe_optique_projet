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
    public partial class AddLigneCmdFournisseur : Page
    {
        dbEntities db;
        private Page prevPage;
        private AddCmd addCmdPage;
        private List<traitement> traitements;

        public AddLigneCmdFournisseur(Page prevP, AddCmd addCmdP)
        {
            InitializeComponent();

            prevPage = prevP;
            addCmdPage = addCmdP;
            db = new dbEntities();
            fillComboBox();
        }

        public void selectItem(object sender, SelectionChangedEventArgs e)
        {
            int selectedIemValue = (int)categorie.SelectedValue;
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

            categorie.SelectedIndex = 0;

            List<typeverre> typeVerres = db.typeverres.Distinct().ToList();
            typeVerresText.ItemsSource = typeVerres;
            typeVerresText.DisplayMemberPath = "NomType";
            typeVerresText.SelectedValuePath = "idTypeVerre";

            List<typelentille> typeLentille = db.typelentilles.Distinct().ToList();
            typeLentilleText.ItemsSource = typeLentille;
            typeLentilleText.DisplayMemberPath = "NomType";
            typeLentilleText.SelectedValuePath = "idTypeLentille";

            traitements = db.traitements.Distinct().ToList();
            newTraitementNom.ItemsSource = traitements;
            newTraitementNom.DisplayMemberPath = "Nom";
            newTraitementNom.SelectedValuePath = "idTraitement";

            lentilleNewTrat.ItemsSource = traitements;
            newTraitementNom.DisplayMemberPath = "Nom";
            newTraitementNom.SelectedValuePath = "idTraitement";
        }
        
        public void addTraitLentilleButton(object sender, RoutedEventArgs e)
        {
            StackPanel stackPanel = new StackPanel() { Orientation = System.Windows.Controls.Orientation.Horizontal };

            ComboBox newCombobox = new ComboBox() { Width = 100 };
            newCombobox.ItemsSource = traitements;
            newCombobox.DisplayMemberPath = "Nom";
            newCombobox.SelectedValuePath = "idTraitement";
            newCombobox.SelectedIndex = newTraitementNom.SelectedIndex;
            TextBox newTextBox = new TextBox() { Width = 100, Text = lentilleNewTratNiveau.Text };
            Button supprimerBtn = new Button() { Content = "supprimer" };
            supprimerBtn.Click += supprimerTraitementLentille;

            stackPanel.Children.Add(newCombobox);
            stackPanel.Children.Add(newTextBox);
            stackPanel.Children.Add(supprimerBtn);

            traitementsLentilleBox.Children.Add(stackPanel);
        }

        public void addTraitVerreButton(object sender, RoutedEventArgs e)
        {
            StackPanel stackPanel = new StackPanel() { Orientation = System.Windows.Controls.Orientation.Horizontal };

            ComboBox newCombobox = new ComboBox() { Width = 100 };
            newCombobox.ItemsSource = traitements;
            newCombobox.DisplayMemberPath = "Nom";
            newCombobox.SelectedValuePath = "idTraitement";
            newCombobox.SelectedIndex = newTraitementNom.SelectedIndex;
            TextBox newTextBox = new TextBox() { Width = 100, Text = newTraitementNiveau.Text };
            Button supprimerBtn = new Button() { Content = "supprimer" };
            supprimerBtn.Click += supprimerTraitementVerre;

            stackPanel.Children.Add(newCombobox);
            stackPanel.Children.Add(newTextBox);
            stackPanel.Children.Add(supprimerBtn);

            traitementsBox.Children.Add(stackPanel);
        }

        private void supprimerTraitementVerre(object sender, RoutedEventArgs e)
        {
            traitementsBox.Children.Remove((UIElement)(sender as FrameworkElement).Parent);
        }
        
        private void supprimerTraitementLentille(object sender, RoutedEventArgs e)
        {
            traitementsLentilleBox.Children.Remove((UIElement)(sender as FrameworkElement).Parent);
        }

        private void ReturnBtn_Click(object sender, RoutedEventArgs e)
        {
            MyContext.navigateTo(prevPage);
        }

        // abort ligne
        public void deleteLigne(object sender, RoutedEventArgs e)
        {
            addCmdPage.lignesCmd.Remove(this);
            MyContext.navigateTo(prevPage);
        }

        // save ligne
        public void saveLigne(object sender, RoutedEventArgs e)
        {
            if (!addCmdPage.lignesCmd.Contains(this))
                addCmdPage.addNewLigneToList(this);
            else
            {
                addCmdPage.updateLigneInList(this);
            }
            MyContext.navigateTo(prevPage);
        }

    }
}
