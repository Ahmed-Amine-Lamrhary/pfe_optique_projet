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
    /// <summary>
    /// Interaction logic for ArticleAbout.xaml
    /// </summary>
    public partial class ArticleAbout : Page
    {
        private Page prevPage;
        public ArticleAbout(int articleId, Page prevP)
        {
            InitializeComponent();

            prevPage = prevP;
        }

        // add medicaments
        private void enterAddTrait(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                ajouterTrait();
        }

        private void addTraitButton(object sender, RoutedEventArgs e)
        {
            ajouterTrait();
        }

        private void ajouterTrait()
        {
            if (!String.IsNullOrEmpty(newTraitementNiveau.Text) && !String.IsNullOrWhiteSpace(newTraitementNiveau.Text))
            {
                StackPanel stackPanel = new StackPanel() { Orientation = System.Windows.Controls.Orientation.Horizontal };

                System.Windows.Controls.ComboBox comboBox = new System.Windows.Controls.ComboBox() { Text = newTraitementNom.Text, Width = 200 };
                System.Windows.Controls.TextBox textBox = new System.Windows.Controls.TextBox() { Text = newTraitementNiveau.Text, Width = 200 };
                System.Windows.Controls.Button deleteButton = new System.Windows.Controls.Button() { Content = "Supprimer" };
                deleteButton.Click += supprimerTrait;

                stackPanel.Children.Add(comboBox);
                stackPanel.Children.Add(textBox);
                stackPanel.Children.Add(deleteButton);

                traitementsBox.Children.Add(stackPanel);
                newTraitementNiveau.Text = "";
            }
        }

        private void supprimerTrait(object sender, RoutedEventArgs e)
        {
            traitementsBox.Children.Remove((UIElement)(sender as FrameworkElement).Parent);
        }

        private string getTraString()
        {
            // check if medicaments enabled
            if (!(bool)medicamentsCheckbox.IsChecked)
                return null;

            // check if medicaments filled
            List<string> medicamentsList = new List<string>();

            foreach (StackPanel s in traitementsBox.Children)
            {
                System.Windows.Controls.TextBox t = s.Children.OfType<System.Windows.Controls.TextBox>().FirstOrDefault();
                if (!String.IsNullOrEmpty(t.Text) && !String.IsNullOrWhiteSpace(t.Text))
                    medicamentsList.Add(t.Text);
            }

            if (medicamentsList.Count == 0)
                return null;

            // return medicaments string
            return String.Join("@", medicamentsList);
        }

        // add maladies
        private void enterAddMaladie(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                ajouterMaladie();
        }

        private void addMaladieButton(object sender, RoutedEventArgs e)
        {
            ajouterMaladie();
        }

        private void ajouterMaladie()
        {
            if (!String.IsNullOrEmpty(newMaladie.Text) && !String.IsNullOrWhiteSpace(newMaladie.Text))
            {
                StackPanel stackPanel = new StackPanel() { Orientation = System.Windows.Controls.Orientation.Horizontal };
                System.Windows.Controls.TextBox textBox = new System.Windows.Controls.TextBox() { Text = newMaladie.Text, Width = 200 };
                System.Windows.Controls.Button deleteButton = new System.Windows.Controls.Button() { Content = "Supprimer" };
                deleteButton.Click += supprimerMaladie;

                stackPanel.Children.Add(textBox);
                stackPanel.Children.Add(deleteButton);

                maladiesBox.Children.Add(stackPanel);
                newMaladie.Text = "";
            }
        }

        private void supprimerMaladie(object sender, RoutedEventArgs e)
        {
            maladiesBox.Children.Remove((UIElement)(sender as FrameworkElement).Parent);
        }

        private string getMaladiesString()
        {
            // check if maladies enabled
            if (!(bool)maladiesCheckbox.IsChecked)
                return null;

            // check if maladies filled
            List<string> maladiesList = new List<string>();

            foreach (StackPanel s in maladiesBox.Children)
            {
                System.Windows.Controls.TextBox t = s.Children.OfType<System.Windows.Controls.TextBox>().FirstOrDefault();
                if (!String.IsNullOrEmpty(t.Text) && !String.IsNullOrWhiteSpace(t.Text))
                    maladiesList.Add(t.Text);
            }

            if (maladiesList.Count == 0)
                return null;

            // return maladies string
            return String.Join("@", maladiesList);
        }


        //validation
        private Boolean validateForm()
        {
            if (qteText.Text.Length != 0 &&
                prixText.Text.Length != 0 &&
                garantieText.Text.Length != 0 &&
                modeleText.Text.Length != 0 &&
                marqueText.Text.Length != 0 &&

                od_add.Text.Length != 0 &&
                od_axe.Text.Length != 0 &&
                od_cyl.Text.Length != 0 &&
                od_sph.Text.Length != 0 &&

                og_add.Text.Length != 0 &&
                og_axe.Text.Length != 0 &&
                og_cyl.Text.Length != 0 &&
                og_sph.Text.Length != 0
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
