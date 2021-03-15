using MenuWithSubMenu.Model;
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
    /// Interaction logic for UpdateArticle.xaml
    /// </summary>
    public partial class UpdateArticle : Page
    {
        dbEntities db;
        private Page prevPage;
        public UpdateArticle(article article, Page prevP)
        {
            InitializeComponent();
            db = new dbEntities();
            prevPage = prevP;
        }
    }
}
