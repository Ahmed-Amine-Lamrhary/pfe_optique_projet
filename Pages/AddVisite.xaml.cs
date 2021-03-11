using MenuWithSubMenu.Model;
using MenuWithSubMenu.Windows;
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

    public partial class AddVisite : Page
    {
        dbEntities db;
        private List<examan> listExamen;
        public AddVisite()
        {
            InitializeComponent();
            db = new dbEntities();
            listExamen = new List<examan>();
            examList.ItemsSource = listExamen;
        }
        private void addExam(object sender, RoutedEventArgs e)
        {
            AddExam newExam = new AddExam(this);
            newExam.Show();
        }
        private void saveVisite(object sender, RoutedEventArgs e)
        {
            
            foreach(examan ex in listExamen)
            {
                db.examen.Add(ex);
            }
        }
        public void addExamToList(examan exam)
        {
            listExamen.Add(exam);
            examList.Items.Refresh();
        }
    }
}
