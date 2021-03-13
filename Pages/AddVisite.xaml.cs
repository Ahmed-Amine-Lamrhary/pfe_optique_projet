using MenuWithSubMenu.Model;
using MenuWithSubMenu.Windows;
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

namespace MenuWithSubMenu.Pages
{

    public partial class AddVisite : Page
    {
        dbEntities db;
        private List<examan> listExamen;
        DbContextTransaction transaction;
        private string clientCin;

        public AddVisite(string clientCin)
        {
            InitializeComponent();
            db = new dbEntities();
            listExamen = new List<examan>();
            examList.ItemsSource = listExamen;
            this.clientCin = clientCin;
        }
        private void addExam(object sender, RoutedEventArgs e)
        {
            AddExam newExam = new AddExam(this);
            newExam.Show();
        }
        private void saveVisite(object sender, RoutedEventArgs e)
        {
            try
            {
                transaction = db.Database.BeginTransaction();
                visite newVisite = new visite()
                {
                    date = DateTime.Now,
                    raison = rasonVissite.Text,
                    client_cin = clientCin
                };
                db.visites.Add(newVisite);
                db.SaveChanges();

                foreach (examan ex in listExamen)
                {
                    ex.visite_id = newVisite.id;

                    db.examen.Add(ex);
                    db.SaveChanges();
                }

                transaction.Commit();
            }
            catch (Exception exc)
            {
                Console.Write(exc.Message);
                HandyControl.Controls.MessageBox.Show(exc.Message);
                transaction.Rollback();
            }
        }
        public void addExamToList(examan exam)
        {
            listExamen.Add(exam);
            examList.Items.Refresh();
        }
    }
}
