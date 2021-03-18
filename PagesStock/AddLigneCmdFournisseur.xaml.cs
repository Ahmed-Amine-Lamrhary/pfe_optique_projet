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
        private List<traitement> traitementsSelectiones;
        private int coutTrait,maxTrait;

        public AddLigneCmdFournisseur(Page prevP, AddCmd addCmdP)
        {
            InitializeComponent();
            db = new dbEntities();
            coutTrait = 0;
            maxTrait = db.traitements.Count();
            prevPage = prevP;
            addCmdPage = addCmdP;
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

            List<typeLentille> typeLentille = db.typeLentilles.Distinct().ToList();
            typeLentilleText.ItemsSource = typeLentille;
            typeLentilleText.DisplayMemberPath = "Nom";
            typeLentilleText.SelectedValuePath = "idTypeLentille";

            traitements = db.traitements.Distinct().ToList();
            newTraitementNom.ItemsSource = traitements;
            newTraitementNom.DisplayMemberPath = "Nom";
            newTraitementNom.SelectedValuePath = "idTraitement";

            lentilleNewTrat.ItemsSource = traitements;
            lentilleNewTrat.DisplayMemberPath = "Nom";
            lentilleNewTrat.SelectedValuePath = "idTraitement";
        }
        
        public void addTraitLentilleButton(object sender, RoutedEventArgs e)
        {
            coutTrait++;
            MessageBox.Show(""+coutTrait+"");
            if (coutTrait >= maxTrait +1)
                return;

            StackPanel stackPanel = new StackPanel() { Orientation = System.Windows.Controls.Orientation.Horizontal };
            
            ComboBox newCombobox = new ComboBox() { Width = 100 };
            newCombobox.ItemsSource = traitements;
            newCombobox.DisplayMemberPath = "Nom";
            newCombobox.SelectedValuePath = "idTraitement";
            newCombobox.SelectedIndex = lentilleNewTrat.SelectedIndex;

            TextBox newTextBox = new TextBox() { Width = 100, Text = lentilleNewTratNiveau.Text };
            Button supprimerBtn = new Button() { Content = "Supprimer" };
            supprimerBtn.Click += supprimerTraitementLentille;

            stackPanel.Children.Add(newCombobox);
            stackPanel.Children.Add(newTextBox);
            stackPanel.Children.Add(supprimerBtn);

            traitementsLentilleBox.Children.Add(stackPanel);
        }

        public void addTraitVerreButton(object sender, RoutedEventArgs e)
        {
            coutTrait++;
            if (coutTrait >= maxTrait - 1)
                return;
            StackPanel stackPanel = new StackPanel() { Orientation = System.Windows.Controls.Orientation.Horizontal };

            ComboBox newCombobox = new ComboBox() { Width = 100 };
            newCombobox.ItemsSource = traitements;
            newCombobox.DisplayMemberPath = "Nom";
            newCombobox.SelectedValuePath = "idTraitement";
            newCombobox.SelectedIndex = newTraitementNom.SelectedIndex;
            TextBox newTextBox = new TextBox() { Width = 100, Text = newTraitementNiveau.Text };
            Button supprimerBtn = new Button() { Content = "Supprimer" };
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

        //getId Of selected traitement :


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
        public List<traitement> get_traitements()
        {
            return traitements;
        }
        /*public List<traitement> Get_traitements_selectionee()
        {
            foreach (StackPanel stack in traitementsLentilleBox.Children.OfType<StackPanel>())
            {
                
                foreach(ComboBox c in stack.Children.OfType<ComboBox>)
                {
                    traitementsSelectiones.Add(new traitement()
                    {
                        idTraitement = (int)c.SelectedValue,
                    });
                }

            }
            return traitementsSelectiones;
        }*/

        private void TypeLentilleText_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int selectedIemValue = (int)typeLentilleText.SelectedValue;
            switch (selectedIemValue)
            {
                case 1:
                    panelMultiFocale.Visibility = Visibility.Collapsed;
                    panelSpherique.Visibility = Visibility.Collapsed;
                    break;
                case 2:
                    panelMultiFocale.Visibility = Visibility.Visible;
                    panelSpherique.Visibility = Visibility.Collapsed;
                    break;
                case 3:
                    panelMultiFocale.Visibility = Visibility.Collapsed;
                    panelSpherique.Visibility = Visibility.Visible;
                    break;
            }
        }
    }
}
