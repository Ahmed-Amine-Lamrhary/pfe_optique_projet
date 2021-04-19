using MenuWithSubMenu.Model;
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
using System.Windows.Shapes;

namespace MenuWithSubMenu
{
    public partial class Authentication : Window
    {
        dbEntities db;

        public Authentication()
        {
            InitializeComponent();
            db = new dbEntities();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        private async void login(object sender, RoutedEventArgs e)
        {
            try
            {
                loginStackPanel.IsEnabled = false;
                setting info = await db.settings.FirstOrDefaultAsync();

                if (passwordField.Password == info.password.Trim())
                {
                    MainWindow mainWindow = new MainWindow();
                    mainWindow.Show();

                    Close();
                }
                else
                {
                    HandyControl.Controls.MessageBox.Show("Mot de passe entré est incorrecte");
                }
            } catch (Exception)
            {
                HandyControl.Controls.MessageBox.Show("Erreur");
            } finally
            {
                loginStackPanel.IsEnabled = true;
            }
        }
    }
}
