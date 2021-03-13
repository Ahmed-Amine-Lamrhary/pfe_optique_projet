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
            examan exam = new examan() {
                od_add_loins = int.Parse(od_add_loin.Text),
                od_cylindre_loins = int.Parse(od_cyl_loin.Text),
                od_axe_loins = int.Parse(od_axe_loin.Text),
                od_sphere_loins = int.Parse(od_sph_loin.Text),
                og_add_loins = int.Parse(og_add_loin.Text),
                og_cylindre_loins = int.Parse(og_cyl_loin.Text),
                og_axe_loins = int.Parse(og_axe_loin.Text),
                og_sphère_loins = int.Parse(og_sph_loin.Text),

                od_add_pres = int.Parse(od_add_pres.Text),
                od_cylindre_pres = int.Parse(od_cyl_pres.Text),
                od_axe_pres = int.Parse(od_axe_pres.Text),
                od_sphere_pres = int.Parse(od_sph_pres.Text),
                og_add_pres = int.Parse(og_add_pres.Text),
                og_cylindre_pres = int.Parse(og_cyl_pres.Text),
                og_axe_pres = int.Parse(og_axe_pres.Text),
                og_sphère_pres = int.Parse(og_sph_pres.Text),

                date_derniere_modif = DateTime.Now,
                remarques = ""
            };
            addVisite.addExamToList(exam);
            this.Close();
        }

    }
}
