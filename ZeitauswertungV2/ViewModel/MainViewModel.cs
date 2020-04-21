using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeitauswertungV2.Model;
using ZeitauswertungV2.UI.Data;

namespace ZeitauswertungV2.UI.ViewModel
{
    public class MainViewModel:ViewModelBase
    {
        private Employee selectedEmployee;

        private IEmployeeDataService employeeDataService;

        public ObservableCollection<Employee> Employees { get; set; }

        public MainViewModel(IEmployeeDataService employeeDataService)
        {
            Employees = new ObservableCollection<Employee>();
            this.employeeDataService = employeeDataService;
        }

        public async Task LoadAsync()
        {
            var employees = await employeeDataService.GetAllEmployeeAsync();
            Employees.Clear();
            foreach(var item in employees)
            {
                Employees.Add(item);
            }
        }

        public Employee SelectedEmployee
        {
            get { return selectedEmployee; }
            set { selectedEmployee = value;
                OnPropertyChanged();
            }
        }
    }
}
