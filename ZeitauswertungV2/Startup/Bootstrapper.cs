using Autofac;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeitauswertungV2.Data;
using ZeitauswertungV2.DataAccess;
using ZeitauswertungV2.UI.Data;
using ZeitauswertungV2.UI.ViewModel;
using ZeitauswertungV2.ViewModel;

namespace ZeitauswertungV2.UI.Startup
{
    class Bootstrapper
    {
        public IContainer Bootstrap()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<EventAggregator>().As<IEventAggregator>().SingleInstance();

            builder.RegisterType<BookingDbContext>().AsSelf();
            builder.RegisterType<MainWindow>().AsSelf();
            builder.RegisterType<MainViewModel>().AsSelf();

            builder.RegisterType<InputBarViewModel>().As<IInputBarViewModel>();
            builder.RegisterType<DataTableViewModel>().As<IDataTableViewModel>();
            builder.RegisterType<EmployeeDataService>().As<IEmployeeDataService>();
            builder.RegisterType<BookingDataService>().As<IBookingDataService>();

            return builder.Build();

        }
    }
}
