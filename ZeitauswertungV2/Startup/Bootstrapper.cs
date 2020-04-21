using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeitauswertungV2.DataAccess;
using ZeitauswertungV2.UI.Data;
using ZeitauswertungV2.UI.ViewModel;

namespace ZeitauswertungV2.UI.Startup
{
    class Bootstrapper
    {
        public IContainer Bootstrap()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<BookingDbContext>().AsSelf();
            builder.RegisterType<MainWindow>().AsSelf();
            builder.RegisterType<MainViewModel>().AsSelf();
            builder.RegisterType<EmployeeDataService>().As<IEmployeeDataService>();

            return builder.Build();

        }
    }
}
