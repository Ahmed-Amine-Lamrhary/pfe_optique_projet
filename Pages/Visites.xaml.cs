﻿using MenuWithSubMenu.Model;
using MenuWithSubMenu.Utils;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
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

        int count;

        public Visite()
        {
            InitializeComponent();

            db = new dbEntities();

            getVisites(0);
        }

        private async void getVisites(int skip)
        {
            loadingBox.Visibility = Visibility.Visible;
            visitesDataGrid.Visibility = Visibility.Collapsed;
            nothingBox.Visibility = Visibility.Collapsed;

            try
            {
                if (startDate != null && endDate != null)
                {
                    listVisites = await db.visites.Where(v => v.date <= endDate && v.date >= startDate).ToListAsync();
                }
                else if (startDate == null && endDate != null)
                    listVisites = await db.visites.Where(v => v.date <= endDate).ToListAsync();
                else if (startDate != null && endDate == null)
                    listVisites = await db.visites.Where(v => v.date >= startDate).ToListAsync();
                else
                    listVisites = await db.visites.ToListAsync();

                count = (int)Math.Ceiling((decimal)listVisites.Count / 10);
                pagination.MaxPageCount = count;
                visitesDataGrid.ItemsSource = listVisites.Skip(skip).Take(10);

                visitesDataGrid.Visibility = Visibility.Visible;
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

                if (startDate <= endDate)
                    getVisites(0);
                else
                {
                    MessageBox.Show("start date must be less than end date");

                    startDate = endDate = null;
                    filterStartDate.SelectedDate = filterEndDate.SelectedDate = null;
                }
            } catch(Exception)
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
    }
}
