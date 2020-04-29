using Autofac;
using System.Windows;
using ZeitauswertungV2.UI;
using ZeitauswertungV2.UI.Startup;

namespace ZeitauswertungV2.UI
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
