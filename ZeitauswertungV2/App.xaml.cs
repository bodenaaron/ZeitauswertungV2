using Autofac;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using ZeitauswertungV2.UI.Startup;

namespace ZeitauswertungV2
{
    
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            var bootstrapper = new Bootstrapper(); 
            var container = bootstrapper.Bootstrap();
            var mainWindow = container.Resolve<MainWindow>();
            mainWindow.Show();
        }
    }
}
