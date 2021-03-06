using MenuWithSubMenu.Model;
using MenuWithSubMenu.Utils;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace MenuWithSubMenu.Pages
{
    public partial class Visite : Page
    {
        dbEntities db;
        List<visite> listVisites;

        DateTime? startDate;
        DateTime? endDate;

        private List<visite> checkedVisites = new List<visite>();

        private Page prevPage;

        int count;

        public Visite()
        {
            InitializeComponent();

            returnBtn.Visibility = Visibility.Collapsed;

            db = new dbEntities();

            getVisites(0);
        }

        public Visite(Page prevP)
        {
            InitializeComponent();

            prevPage = prevP;

            db = new dbEntities();

            getVisites(0);
        }

        private async Task getVisites(int skip)
        {
            loadingBox.Visibility = Visibility.Visible;
            infoBox.Visibility = Visibility.Collapsed;
            nothingBox.Visibility = Visibility.Collapsed;

            try
            {
                if (startDate != null && endDate != null)
                {
                    listVisites = await Task.Run(() => db.visites.Where(v => v.date <= endDate && v.date >= startDate).ToList());
                }
                else if (startDate == null && endDate != null)
                    listVisites = await Task.Run(() => db.visites.Where(v => v.date <= endDate).ToList());
                else if (startDate != null && endDate == null)
                    listVisites = await Task.Run(() => db.visites.Where(v => v.date >= startDate).ToList());
                else
                    listVisites = await Task.Run(() => db.visites.ToList());

                if (listVisites.Count() == 0)
                {
                    nothingBox.Visibility = Visibility.Visible;
                    return;
                }

                count = (int)Math.Ceiling((decimal)listVisites.Count / 10);
                pagination.MaxPageCount = count;
                visitesDataGrid.ItemsSource = listVisites.Skip(skip).Take(10);

                infoBox.Visibility = Visibility.Visible;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
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
                if (item.Header.ToString() == "")
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

        private void voirVisite(object sender, RoutedEventArgs e)
        {
            visite visiteRow = visitesDataGrid.SelectedItem as visite;
            int visiteId = visiteRow.id;
            AboutVisite aboutVisite = new AboutVisite(visiteId, this);
            MyContext.navigateTo(aboutVisite);
        }

        private void voirClient(object sender, RoutedEventArgs e)
        {
            visite visiteRow = visitesDataGrid.SelectedItem as visite;
            string clientCin = visiteRow.client_cin;
            ClientProfile clientProfile = new ClientProfile(clientCin, this);
            MyContext.navigateTo(clientProfile);
        }

        private void pagination_PageUpdated(object sender, HandyControl.Data.FunctionEventArgs<int> e)
        {
            getVisites((e.Info - 1) * 10);
        }

        private void filterByDate(object sender, RoutedEventArgs e)
        {
            try
            {
                startDate = (DateTime)filterStartDate.SelectedDate;
                endDate = (DateTime)filterEndDate.SelectedDate;

                filterByDateFunc();
            } catch (Exception)
            {
                startDate = endDate = null;
                getVisites(0);
            }
        }

        private void resetFilter(object sender, RoutedEventArgs e)
        {
            startDate = endDate = null;
            filterStartDate.SelectedDate = filterEndDate.SelectedDate = null;

            getVisites(0);
        }

        private void deleteManualDate(object sender, RoutedEventArgs e)
        {
            lastDate.SelectedIndex = -1;
            manuallyDate.Visibility = Visibility.Collapsed;
            autoDate.IsEnabled = true;
        }


        private void selectLastDate(object sender, SelectionChangedEventArgs e)
        {
            endDate = DateTime.Now;

            switch (lastDate.SelectedIndex)
            {
                // last day
                case 0:
                    startDate = DateTime.Today.AddDays(-1);
                    break;
                // last week
                case 1:
                    startDate = DateTime.Today.AddDays(-7);
                    break;
                // last month
                case 2:
                    startDate = DateTime.Today.AddMonths(-1);
                    break;
                // last year
                case 3:
                    startDate = DateTime.Today.AddYears(-1);
                    break;
                // manually
                case 4:
                    manuallyDate.Visibility = Visibility.Visible;
                    autoDate.IsEnabled = false;
                    return;
                case -1:
                    return;
            }

            filterByDateFunc();
        }

        private void filterByDateFunc()
        {
            try
            {
                if (startDate <= endDate)
                    getVisites(0);
                else
                {
                    HandyControl.Controls.MessageBox.Show("La date de début doit être inférieure à la date de fin");

                    startDate = endDate = null;
                    filterStartDate.SelectedDate = filterEndDate.SelectedDate = null;
                }
            }
            catch (Exception)
            {
                startDate = endDate = null;
                getVisites(0);
            }
        }

        private void checkCmd(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = (CheckBox)e.OriginalSource;
            DataGridRow dataGridRow = VisualTreeHelpers.FindAncestor<DataGridRow>(checkBox);
            visite visite = (visite)dataGridRow.DataContext;

            checkedVisites.Add(visite);

            if (checkedVisites.Count() > 0)
                groupInfo.Visibility = Visibility.Visible;
            else
                groupInfo.Visibility = Visibility.Collapsed;
        }

        private void unCheckCmd(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = (CheckBox)e.OriginalSource;
            DataGridRow dataGridRow = VisualTreeHelpers.FindAncestor<DataGridRow>(checkBox);
            visite visite = (visite)dataGridRow.DataContext;

            checkedVisites.Remove(visite);

            if (checkedVisites.Count() > 0)
                groupInfo.Visibility = Visibility.Visible;
            else
                groupInfo.Visibility = Visibility.Collapsed;
        }

        private void deleteMany(object sender, RoutedEventArgs e)
        {
            DbContextTransaction transaction = db.Database.BeginTransaction();
            try
            {
                foreach (visite visite in checkedVisites)
                {
                    db.visites.Remove(visite);
                    db.SaveChanges();
                }

                transaction.Commit();

                groupInfo.Visibility = Visibility.Collapsed;
                getVisites(0);
            }
            catch (Exception)
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
