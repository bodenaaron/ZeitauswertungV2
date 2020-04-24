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
using ZeitauswertungV2.UI.ViewModel;

namespace ZeitauswertungV2.ViewModel
{
    class InputBarViewModel : ViewModelBase, IInputBarViewModel
    {
        private Employee selectedEmployee;
        private IEmployeeDataService employeeDataService;
        private IEventAggregator eventAggregator;
        
        public ObservableCollection<Employee> Employees { get; set; }

        public InputBarViewModel(IEmployeeDataService employeeDataService, IEventAggregator eventAggregator)
        {
            Employees = new ObservableCollection<Employee>();
            this.employeeDataService = employeeDataService;
            this.eventAggregator = eventAggregator;
            
            SearchCommand = new DelegateCommand(OnSearchExecute, OnSearchCanExecute);
        }
        public async Task LoadAsyncEmployee()
        {
            var employees = await employeeDataService.GetAllEmployeeAsync();
            Employees.Clear();
            foreach (var item in employees)
            {
                Employees.Add(item);
            }
        }

        

        public Employee SelectedEmployee
        {
            get { return selectedEmployee; }
            set
            {
                selectedEmployee = value;
                OnPropertyChanged();
                if (selectedEmployee != null)
                {
                    eventAggregator.GetEvent<EmployeeChangedEvent>().Publish(selectedEmployee.Id);
                }
            }
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





        public ICommand SearchCommand { get; }
    }
}
