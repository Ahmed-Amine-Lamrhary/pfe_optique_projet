using MenuWithSubMenu.PagesSettings;
using MenuWithSubMenu.PagesStock;
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
    /// Interaction logic for Acceuil.xaml
    /// </summary>
    public partial class Acceuil : Page
    {
        public Acceuil()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var page = null as Page;

            switch(button.Name)
            {
                case "addClient":
                    page = new AddClient(this);
                    break;
                case "addVisite":
                    page = new Visite(this);
                    break;
                case "addCmdClient":
                    page = new AddCmdClient(this);
                    break;
                case "addCmdFour":
                    page = new AddCmd(this);
                    break;
                case "parametres":
                    page = new Parametres(this);
                    break;
            }

            if (page != null)
                MyContext.navigateTo(page);
        }
    }
}
