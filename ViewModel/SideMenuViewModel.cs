using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace MenuWithSubMenu
{
    class SideMenuViewModel
    {
        //to call resource dictionary in our code behind
        ResourceDictionary dict = Application.LoadComponent(new Uri("/MenuWithSubMenu;component/Assets/IconDictionary.xaml", UriKind.RelativeOrAbsolute)) as ResourceDictionary;

        //Our Source List for Menu Items
        public List<MenuItemsData> MenuList
        {
            get
            {
                return new List<MenuItemsData>
                {
                    // Acceuil
                    new MenuItemsData(){ PathData= (PathGeometry)dict["icon_dashboard"], MenuText="Acceuil",file_name="Acceuil",dir="Pages", SubMenuList=null},
                    
                    // Client
                    new MenuItemsData(){ PathData= (PathGeometry)dict["icon_dashboard"], MenuText="EspaceClient",file_name="EspaceClient",dir="Pages", SubMenuList=null},
                    
                    // Visite
                    new MenuItemsData(){ PathData= (PathGeometry)dict["icon_settings"], MenuText="Visites",file_name="Visites",dir="Pages", SubMenuList=null},

                    // Ophtalmologue
                    new MenuItemsData(){ PathData= (PathGeometry)dict["icon_settings"], MenuText="Ophtalmologues",file_name="Ophtalmologues",dir="Pages", SubMenuList=null},

                    // Stock
                    new MenuItemsData(){ PathData= (PathGeometry)dict["icon_settings"], MenuText="Commandes Fournisseur",file_name="CmdsFournisseur",dir="PagesStock", SubMenuList=null},

                    // Fournisseurs
                    new MenuItemsData(){ PathData= (PathGeometry)dict["icon_settings"], MenuText="Fournisseurs",file_name="Fournisseurs",dir="Pages", SubMenuList=null}

                                     
                    /*
                    new MenuItemsData(){ PathData= (PathGeometry)dict["icon_users"], MenuText="Espace Client"
                    
                    , SubMenuList=new List<SubMenuItemsData>{
                    new SubMenuItemsData(){ PathData=(PathGeometry)dict["icon_adduser"], SubMenuText="Ajouter Client" },
                    new SubMenuItemsData(){ PathData=(PathGeometry)dict["icon_alluser"], SubMenuText="ClientArea" }}
                    },*/
                };
            }   
        }
    }

    public class MenuItemsData
    {
        //Icon Data
        public PathGeometry PathData { get; set; }
        public string MenuText { get; set; }
        public string dir { get; set; }
        public string file_name { get; set; }
        public List<SubMenuItemsData> SubMenuList { get; set; }

        public MenuItemsData()
        {
            Command = new CommandViewModel(Execute);

            // active page by default
            navigateToPage("Acceuil","Pages");
        }

        public ICommand Command { get; }

        private void Execute()
        {
            if (!string.IsNullOrEmpty(file_name))
                navigateToPage(file_name, dir);
        }
                    
 
        private void navigateToPage(string Menu,string dir)
        {
            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(MainWindow))
                {
                    (window as MainWindow).MainWindowFrame.Navigate(new Uri(string.Format("{0}{1}{2}", dir+"/", Menu, ".xaml"), UriKind.RelativeOrAbsolute));
                }
            }
        }
    }
    public class SubMenuItemsData
    {
        public PathGeometry PathData { get; set; }
        public string SubMenuText { get; set; }

        //To Add click event to our Buttons we will use ICommand here to switch pages when specific button is clicked
        public SubMenuItemsData()
        {
            SubMenuCommand = new CommandViewModel(Execute);
        }

        public ICommand SubMenuCommand { get; }

        private void Execute()
        {
            //our logic comes here
            string SMT = SubMenuText.Replace(" ", string.Empty);
            if (!string.IsNullOrEmpty(SMT))
                navigateToPage(SMT);
        }

        private void navigateToPage(string Menu)
        {
            //We will search for our Main Window in open windows and then will access the frame inside it to set the navigation to desired page..
            //lets see how... ;)
            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(MainWindow))
                {
                    (window as MainWindow).MainWindowFrame.Navigate(new Uri(string.Format("{0}{1}{2}", "Pages/", Menu, ".xaml"), UriKind.RelativeOrAbsolute));
                }
            }
        }
    }
}