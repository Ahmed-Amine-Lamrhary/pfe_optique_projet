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

namespace MenuWithSubMenu.PagesStock
{
    public partial class Fournisseurs : Page
    {
        dbEntities db;
        List<fournisseur> listFournisseurs;

        private List<fournisseur> checkedFour = new List<fournisseur>();

        private Page prevPage;

        int count;

        public Fournisseurs()
        {
            InitializeComponent();

            returnBtn.Visibility = Visibility.Collapsed;

            db = new dbEntities();

            getFournisseurs(0);
        }

        public Fournisseurs(Page prevP)
        {
            InitializeComponent();

            prevPage = prevP;

            db = new dbEntities();

            getFournisseurs(0);
        }

        private async void getFournisseurs(int skip)
        {
            loadingBox.Visibility = Visibility.Visible;
            infoBox.Visibility = Visibility.Collapsed;
            nothingBox.Visibility = Visibility.Collapsed;

            try
            {
                if (searchBar.Text != "")
                    listFournisseurs = await db.fournisseurs.Where(c => c.Nom.Contains(searchBar.Text) || c.Email.Contains(searchBar.Text)).ToListAsync();
                else
                    listFournisseurs = await db.fournisseurs.ToListAsync();

                if (listFournisseurs.Count() == 0)
                {
                    nothingBox.Visibility = Visibility.Visible;
                    return;
                }


                count = (int)Math.Ceiling((decimal)listFournisseurs.Count / 10);
                pagination.MaxPageCount = count;
                fournisseursDataGrid.ItemsSource = listFournisseurs.Skip(skip).Take(10);

                infoBox.Visibility = Visibility.Visible;
            }
            catch (Exception exp)
            {
                Console.WriteLine(exp.Message);
                nothingBox.Visibility = Visibility.Visible;
            }
            finally
            {
                loadingBox.Visibility = Visibility.Collapsed;
            }
        }


        private void DataGrid_AutoGeneratedColumns(object sender, EventArgs e)
        {
            var grid = (DataGrid)sender;
            foreach (var item in grid.Columns)
            {
                if (item.Header.ToString() == "Edit")
                {
                    item.DisplayIndex = grid.Columns.Count - 1;
                    break;
                }
            }
        }

        private void AddPresetButton_Click(object sender, RoutedEventArgs e)
        {
            var addButton = sender as FrameworkElement;
            if (addButton != null)
            {
                addButton.ContextMenu.IsOpen = true;
            }
        }

        private void voirFournisseur(object sender, RoutedEventArgs e)
        {
            fournisseur fournisseurRow = fournisseursDataGrid.SelectedItem as fournisseur;
            int fournisseurId = fournisseurRow.idFournisseur;

            FournisseurProfile fournisseurProfile = new FournisseurProfile(fournisseurId, this);
            MyContext.navigateTo(fournisseurProfile);
        }

        private void updateFournisseur(object sender, RoutedEventArgs e)
        {

            fournisseur FournisseurRow = fournisseursDataGrid.SelectedItem as fournisseur;
            int FournisseurId = FournisseurRow.idFournisseur;
            UpdateFournisseur update = new UpdateFournisseur(db.fournisseurs.Where(Fournisseur => Fournisseur.idFournisseur == FournisseurId).SingleOrDefault(), this);
            MyContext.navigateTo(update);
        }
        private void deleteFournisseur(object sender, RoutedEventArgs e)
        {

            fournisseur fournisseurRow = fournisseursDataGrid.SelectedItem as fournisseur;
            db.fournisseurs.Remove(fournisseurRow);
            db.SaveChanges();
        }
        private void addFournisseur(object sender, RoutedEventArgs e)
        {
            AddFournisseur add_Fournisseur = new AddFournisseur(this);
            MyContext.navigateTo(add_Fournisseur);
            fournisseursDataGrid.Items.Refresh();
        }

        private void pagination_PageUpdated(object sender, HandyControl.Data.FunctionEventArgs<int> e)
        {
            getFournisseurs((e.Info - 1) * 10);
        }

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (searchBar.Text != "") cancelFocus.Visibility = Visibility.Visible;
            else cancelFocus.Visibility = Visibility.Collapsed;

            getFournisseurs(0);
        }

        private void CancelFocus_Click(object sender, RoutedEventArgs e)
        {
            searchBar.Text = "";
        }


        private void checkCmd(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = (CheckBox)e.OriginalSource;
            DataGridRow dataGridRow = VisualTreeHelpers.FindAncestor<DataGridRow>(checkBox);
            fournisseur fournisseur = (fournisseur)dataGridRow.DataContext;

            checkedFour.Add(fournisseur);

            if (checkedFour.Count() > 0)
                groupInfo.Visibility = Visibility.Visible;
            else
                groupInfo.Visibility = Visibility.Collapsed;
        }

        private void unCheckCmd(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = (CheckBox)e.OriginalSource;
            DataGridRow dataGridRow = VisualTreeHelpers.FindAncestor<DataGridRow>(checkBox);
            fournisseur fournisseur = (fournisseur)dataGridRow.DataContext;

            checkedFour.Remove(fournisseur);

            if (checkedFour.Count() > 0)
                groupInfo.Visibility = Visibility.Visible;
            else
                groupInfo.Visibility = Visibility.Collapsed;
        }

        private void deleteMany(object sender, RoutedEventArgs e)
        {
            DbContextTransaction transaction = db.Database.BeginTransaction();
            try
            {
                foreach(fournisseur fournisseur in checkedFour)
                {
                    db.fournisseurs.Remove(fournisseur);
                    db.SaveChanges();
                }

                transaction.Commit();

                groupInfo.Visibility = Visibility.Collapsed;
                getFournisseurs(0);
            } catch (Exception)
            {
                transaction.Rollback();
                HandyControl.Controls.MessageBox.Show("Erreur");
            }
        }

        private void ReturnBtn_Click(object sender, RoutedEventArgs e)
        {
            MyContext.navigateTo(prevPage);
        }

    }
}
