using MenuWithSubMenu.Model;
using MenuWithSubMenu.Pages;
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
using System.Windows.Shapes;

namespace MenuWithSubMenu.Windows
{
    /// <summary>
    /// Logique d'interaction pour AddExam.xaml
    /// </summary>
    public partial class AddExam : Window
    {
        dbEntities db;
        AddVisite addVisite;
        public AddExam()
        {
            
            InitializeComponent();
        }
        public AddExam(AddVisite addVisite)
        {
            InitializeComponent();
            db = new dbEntities();
            this.addVisite = addVisite;
        }
        private void addExam(object sender, EventArgs e)
        {

            vision_pres vision_Pres = new vision_pres()
            {
                od_add = int.Parse(od_add_pres.Text),
                od_cylindre = int.Parse(od_cyl_pres.Text),
                od_axe = int.Parse(od_axe_pres.Text),
                od_sphere = int.Parse(od_sph_pres.Text),
                og_add = int.Parse(og_add_pres.Text),
                og_cylindre = int.Parse(og_cyl_pres.Text),
                og_axe = int.Parse(og_axe_pres.Text),
                og_sphère = int.Parse(og_sph_pres.Text)
            };
            db.vision_pres.Add(vision_Pres);

            vision_loins vision_Loins = new vision_loins()
            {
                od_add = int.Parse(od_add_loin.Text),
                od_cylindre = int.Parse(od_cyl_loin.Text),
                od_axe = int.Parse(od_axe_loin.Text),
                od_sphere = int.Parse(od_sph_loin.Text),
                og_add = int.Parse(og_add_loin.Text),
                og_cylindre = int.Parse(og_cyl_loin.Text),
                og_axe = int.Parse(og_axe_loin.Text),
                og_sphère = int.Parse(og_sph_loin.Text)
            };
            db.vision_loins.Add(vision_Loins);

            examan exa = new examan() {
                visionLoins = vision_Loins.id,
                visionPres = vision_Pres.id,
                modifOrdonnance = editOrdonnance.IsChecked.ToString(),
                remarques = remarquesText.Text
            };
            addVisite.addExamToList(exa);
            this.Close();
        }

    }
}
