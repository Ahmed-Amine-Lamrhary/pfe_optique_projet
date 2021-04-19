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
using System.Windows.Threading;

namespace MenuWithSubMenu
{
    public partial class SplashScreen : Window
    {
        dbEntities db;
        DispatcherTimer dt = new DispatcherTimer();

        public SplashScreen()
        {
            InitializeComponent();
            db = new dbEntities();

            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            dt.Tick += new EventHandler(dt_Tick);
            dt.Interval = new TimeSpan(0, 0, 4);
            dt.Start();
        }

        private async void dt_Tick(object sender, EventArgs e)
        {
            setting settings = null;
            if (db.Database.Exists()) settings = await db.settings.FirstOrDefaultAsync();

            if (!db.Database.Exists())
            {
                db.Database.Create();

                Configuration configuration = new Configuration();
                configuration.Show();
            } else if (settings == null)
            {
                Configuration configuration = new Configuration();
                configuration.Show();
            }
            else
            {
                Authentication authentication = new Authentication();
                authentication.Show();
            }

            dt.Stop();
            Close();
        }
    }
}
