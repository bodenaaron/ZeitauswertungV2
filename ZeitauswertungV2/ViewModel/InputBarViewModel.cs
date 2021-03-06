﻿using Prism.Commands;
using Prism.Events;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using ZeitauswertungV2.Model;
using ZeitauswertungV2.UI.Data;
using ZeitauswertungV2.UI.Event;

namespace ZeitauswertungV2.UI.ViewModel
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
            FromDate = DateTime.Now;
            TillDate = DateTime.Now;
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

        private DateTime fromDate;
        public DateTime FromDate {
            get { return fromDate; }
            set
            {
                fromDate = value;
                TillDate = fromDate;
                OnPropertyChanged();
                if (fromDate!=null)
                {
                    eventAggregator.GetEvent<InputChangedEvent>().Publish(new InputChangedEventArgs { EmployeeId = selectedEmployee?.Id, From = fromDate, Till=tillDate});
                }

                
            }
        }

        private DateTime tillDate;
        public DateTime TillDate
        {
            get { return tillDate; }
            set
            {
                tillDate = value;
                OnPropertyChanged();
                if (tillDate != null)
                {
                    eventAggregator.GetEvent<InputChangedEvent>().Publish(new InputChangedEventArgs {EmployeeId=selectedEmployee?.Id, From = fromDate, Till = tillDate });
                }
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
                    eventAggregator.GetEvent<InputChangedEvent>().Publish(new InputChangedEventArgs { EmployeeId = selectedEmployee?.Id, From = fromDate, Till = tillDate });
                }                                
            }
        }

        private void OnSearchExecute()
        {
            
        }

        private bool OnSearchCanExecute()
        {
            //todo:Prüfen ob employee valide ist
            return true;
        }





        public ICommand SearchCommand { get; }
    }
}
