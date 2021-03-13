﻿using MenuWithSubMenu.Model;
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
    /// Interaction logic for AboutVisite.xaml
    /// </summary>
    public partial class AboutVisite : Page
    {
        private int visiteId;
        private dbEntities db;
        private visite visite;

        public AboutVisite(int visiteId)
        {
            InitializeComponent();
            db = new dbEntities();
            
            this.visiteId = visiteId;
            visite = db.visites.Where(visite => visite.id == visiteId).SingleOrDefault();
            raisonVisite.Text = visite.raison;

            List<examan> listExamens  = db.examen.Where(examan => examan.visite_id == visiteId).ToList();
            examList.ItemsSource = listExamens;
        }
    }
}