using Prism.Commands;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ZeitauswertungV2.Data;
using ZeitauswertungV2.Event;
using ZeitauswertungV2.Model;
using ZeitauswertungV2.UI.Data;

namespace ZeitauswertungV2.UI.ViewModel
{
    public class MainViewModel:ViewModelBase
    {
        private Employee selectedEmployee;

        private IEmployeeDataService employeeDataService;
        private IEventAggregator eventAggregator;
        private IBookingDataService bookingDataService;

        public ObservableCollection<Employee> Employees { get; set; }

        public MainViewModel(IEmployeeDataService employeeDataService, IBookingDataService bookingDataService, IEventAggregator eventAggregator)
        {
            Employees = new ObservableCollection<Employee>();
            this.employeeDataService = employeeDataService;
            this.eventAggregator = eventAggregator;
            this.bookingDataService = bookingDataService;
            SearchCommand = new DelegateCommand(OnSearchExecute, OnSearchCanExecute);
        }

        private void OnSearchExecute()
        {
            throw new NotImplementedException();
        }

        private bool OnSearchCanExecute()
        {
            //todo:Prüfen ob employee valide ist
            return true;
        }

        public async Task LoadAsyncEmployee()
        {
            var employees = await employeeDataService.GetAllEmployeeAsync();
            Employees.Clear();
            foreach(var item in employees)
            {
                Employees.Add(item);
            }
        }

        public async Task LoadAsyncBookings()
        {
            var bookings = await bookingDataService.GetByEmployeeIdAsync(selectedEmployee.Id);
        }

        public Employee SelectedEmployee
        {
            get { return selectedEmployee; }
            set { selectedEmployee = value;
                OnPropertyChanged();
                if (selectedEmployee!=null)
                {
                    eventAggregator.GetEvent<EmployeeChangedEvent>().Publish(selectedEmployee.Id);
                }
            }
        }

        public ICommand SearchCommand { get; }
    }
}
