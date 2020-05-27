using Prism.Commands;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using ZeitauswertungV2.Model;
using ZeitauswertungV2.UI.Data;
using ZeitauswertungV2.UI.Event;
using ZeitauswertungV2.UI.Utility;

namespace ZeitauswertungV2.UI.ViewModel
{
    public class DataTableViewModel : ViewModelBase, IDataTableViewModel
    {

        private IBookingDataService bookingDataService;
        private ITimeAccountAdjustmentDataService timeAccountDataService;

        private IEventAggregator eventAggregator;
        public ICommand ToggleView { get; }
        public ICommand AdjustTimeAccount { get; }
        public ObservableCollection<BookingDay> BookingsByDay { get; }
        public ObservableCollection<Booking> Bookings { get; }

        private TimeSpan targetHours;

        #region Überwachte Variablen

        private string groupByDay;
        public string GroupByDay {
            get
            {
                return groupByDay;
            }
            set
            {
                groupByDay = value;
                OnPropertyChanged();
            }
        }
        public bool GroupByBooking { get; set; }

        private TimeSpan bookedDiff;
        public TimeSpan BookedDiff
        {
            get
            {
                return bookedDiff;
            }
            private set
            {
                bookedDiff = value;
                if (value.TotalHours<0)
                {
                    DisplayDiff = string.Format("{0:0.}:{1}", Math.Ceiling(value.TotalHours), value.Minutes);
                }
                else 
                { 
                    DisplayDiff = string.Format("{0:0.}:{1}", Math.Floor(value.TotalHours), value.Minutes);
                }
            }
        }
        public string DisplayDiff
        {
            get
            {
                return displayDiff;
            }
            set
            {
                displayDiff = value;
                OnPropertyChanged();
            }
        }
        private string displayDiff;


        public string DisplayAllHours {
            get
            {
                return displayAllHours;
            }
            set
            {
                displayAllHours = value;
                OnPropertyChanged();
            }
        }
        private string displayAllHours;
        private TimeSpan timeAccount;
        public TimeSpan TimeAccount
        {
            get
            {
                return timeAccount;
            }
            private set
            {
                timeAccount = value;
                DisplayTimeAccount = string.Format("{0:0.}:{1}", Math.Floor(value.TotalHours), value.Minutes);
            }
        }
        public string DisplayTimeAccount
        {
            get
            {
                return displayTimeAccount;
            }
            set
            {
                displayTimeAccount = value;
                OnPropertyChanged();
            }
        }
        private string displayTimeAccount;
        public string DisplayWorkedHours {
            get
            {
                return displayWorkedHours;
            }
            set
            {
                displayWorkedHours = value;
                OnPropertyChanged();
            }
        }
        private string displayWorkedHours;
        private TimeSpan vacationHours;

        public string DisplayVacationHours
        {
            get
            {
                return displayVacationHours;
            }
            set
            {
                displayVacationHours = value;
                OnPropertyChanged();
            }
        }
        private string displayVacationHours;
        private TimeSpan sickHours;

        public string DisplaySickHours
        {
            get
            {
                return displaySickHours;
            }
            set
            {
                displaySickHours = value;
                OnPropertyChanged();
            }
        }
        public string DisplayTargetHours
        {
            get
            {
                return displayTargetHours;
            }
            set
            {
                displayTargetHours = value;
                OnPropertyChanged();
            }
        }

        public TimeSpan TargetHours {
            get
            {
                return targetHours;
            }
            private set
            {
                targetHours = value;
                DisplayTargetHours = string.Format("{0:0.}:{1}", Math.Floor(value.TotalHours), value.Minutes);
            }
        }
        public TimeSpan VacationHours {
            get
            {
                return vacationHours;
            }
            private set
            {
                vacationHours = value;
                DisplayVacationHours = string.Format("{0:0.}:{1}", Math.Floor(value.TotalHours), value.Minutes);
            }
        }
        public TimeSpan SickHours {
            get
            {
                return sickHours;
            }
            private set
            {
                sickHours = value;
                DisplaySickHours = string.Format("{0:0.}:{1}", Math.Floor(value.TotalHours), value.Minutes);
            }
        }
        public TimeSpan WorkedHours {
            get
            {
                return workedHours;
            }
            private set
            {
                workedHours = value;
                DisplayWorkedHours = string.Format("{0:0.}:{1}", Math.Floor(value.TotalHours), value.Minutes);
            }
        }

        private TimeSpan allHours;
        public TimeSpan AllHours {
            get
            {
                return allHours;
            }
            private set
            {
                allHours = value;
                DisplayAllHours = string.Format("{0:0.}:{1}", Math.Floor(value.TotalHours), value.Minutes);                
            }
        }

        private int timeAdjustmentHours;
        private int timeAdjustmentMinutes;

        public int TimeAdjustmentMinutes
        {
            get { return timeAdjustmentMinutes; }
            set
            {
                timeAdjustmentMinutes = value;
                OnPropertyChanged();
            }
        }
        public int TimeAdjustmentHours
        {
            get { return timeAdjustmentHours; }
            set
            {
                timeAdjustmentHours = value;
                OnPropertyChanged();
            }
        }
        private string displaySickHours;
        private string displayTargetHours;
        private TimeSpan workedHours;

        #endregion

        public DataTableViewModel(IBookingDataService bookingDataService, IEventAggregator eventAggregator, ITimeAccountAdjustmentDataService timeAccountAdjustmentDataService)
        {
            this.bookingDataService = bookingDataService;
            this.timeAccountDataService = timeAccountAdjustmentDataService;
            this.eventAggregator = eventAggregator;
            //this.eventAggregator.GetEvent<EmployeeChangedEvent>().Subscribe(OnEmployeeChanged);
            this.eventAggregator.GetEvent<InputChangedEvent>().Subscribe(OnInputChanged);
            Bookings = new ObservableCollection<Booking>();
            BookingsByDay = new ObservableCollection<BookingDay>();
            ToggleView= new DelegateCommand(OnSearchExecute, OnSearchCanExecute);
            AdjustTimeAccount = new DelegateCommand(OnAdjustExecute, OnAdjustCanExecute);
            GroupByDay = "Hidden";
        }

        private bool OnAdjustCanExecute()
        {
            return true;
        }

        private void OnAdjustExecute()
        {
            //eventAggregator.GetEvent<TimeAccountAdjustEvent>().Publish(new TimeAccountAdjustment { Employee = selectedEmployee, DateOfAdjustment=DateTime.Now, TimeAccountAdjustedMinutes = TimeAdjustmentMinutes, TimeAccountAdjustedHours = TimeAdjustmentHours});
            timeAccountDataService.SetTimeAccountAdjustment(new TimeAccountAdjustment { Employee = selectedEmployee, DateOfAdjustment = DateTime.Now, TimeAccountAdjustedMinutes = TimeAdjustmentMinutes, TimeAccountAdjustedHours = TimeAdjustmentHours });
            
        }

        private bool OnSearchCanExecute()
        {
            return true;
        }

        private void OnSearchExecute()
        {
            if (GroupByDay=="Visible")
            {
                GroupByDay = "Hidden";
            }
            else { GroupByDay = "Visible"; }
        }

        private string selectedEmployee;

        private async void OnEmployeeChanged(string employeeId)
        {               
            await LoadAsyncBookings(employeeId);
        }

        public async Task LoadAsyncBookings(string employeeId)
        {
            var bookings = await bookingDataService.GetByEmployeeIdAsync(employeeId);
            Bookings.Clear();
            foreach (var item in bookings)
            {
                if (item.Deleted != true)
                {
                    Bookings.Add(item);
                }
                
            }
        }

        public void OnInputChanged(InputChangedEventArgs dateChangedEventArgs)
        {
            LoadAsyncBookingsBeetweenDate(dateChangedEventArgs.EmployeeId,dateChangedEventArgs.From,dateChangedEventArgs.Till);
            LoadAsyncBookingsForYear(dateChangedEventArgs.EmployeeId, new DateTime(dateChangedEventArgs.From.Year,01,01), dateChangedEventArgs.Till);
            LoadBookingsByDay(dateChangedEventArgs.EmployeeId, dateChangedEventArgs.From, dateChangedEventArgs.Till);
            calculateHours();
            calculateTargetHours(dateChangedEventArgs);
            selectedEmployee = dateChangedEventArgs.EmployeeId;
        }

        private void calculateTargetHours(InputChangedEventArgs dateChangedEventArgs)
        {
            DateChecker dc = new DateChecker();
            TargetHours = TimeSpan.Zero;
            TimeSpan duration = dateChangedEventArgs.Till - dateChangedEventArgs.From;

            foreach (var day in EachDay(dateChangedEventArgs.From, dateChangedEventArgs.Till))
            {                
               TargetHours = TargetHours.Add(TimeSpan.FromHours(dc.getHoursofDay(day)));                                
            }

            BookedDiff = AllHours - TargetHours;
        }
        

        private IEnumerable<DateTime> EachDay(DateTime from, DateTime thru)
        {
            for (var day = from.Date; day.Date <= thru.Date; day = day.AddDays(1))
                yield return day;
        }
        private void calculateHours()
        {
            VacationHours = TimeSpan.Zero;
            SickHours = TimeSpan.Zero;
            WorkedHours = TimeSpan.Zero;
            AllHours= TimeSpan.Zero;
            foreach (var booking in Bookings)
            {
                if (booking.Deleted != true)
                {

                    switch (booking.BookingTyp)
                    {
                        case "Urlaub"://todo: Den Eigentlichen Zeittypen benutzen
                            VacationHours = VacationHours + TimeSpan.Parse("8:00"); //die Im Bearbeiter hinterlegten Pflichtstunden benutzen
                            AllHours = AllHours + TimeSpan.Parse("8:00");
                            break;
                        case "Krank":
                            SickHours = SickHours + TimeSpan.Parse("8:00");
                            AllHours = AllHours + TimeSpan.Parse("8:00");
                            break;
                        default:
                            WorkedHours = WorkedHours + booking.Duration;
                            AllHours = AllHours + booking.Duration;
                            break;
                    }
                }
            }
        }

        public void LoadAsyncBookingsBeetweenDate(string employeeId, DateTime from, DateTime till)
        {
            var bookings =  bookingDataService.GetByEmployeeIdAndDate(employeeId,from, till);
            Bookings.Clear();
            foreach (var item in bookings)
            {
                if (item.Deleted != true)
                {
                    Bookings.Add(item);
                }
            }
        }

        public void LoadAsyncBookingsForYear(string employeeId, DateTime from, DateTime till)
        {
            var bookings = bookingDataService.GetByEmployeeIdAndDate(employeeId, from, till);
            
            DateChecker dc = new DateChecker();
            TargetHours = TimeSpan.Zero;
            TimeSpan duration = till - from;

            TimeSpan mandatory = new TimeSpan();
            foreach (var day in EachDay(from, till))
            {               
                mandatory = mandatory.Add(TimeSpan.FromHours(dc.getHoursofDay(day)));               
            }
            TimeSpan bookedHours = CalculateHoursFromBeginningOfYear(bookings);

            TimeAccount = bookedHours - mandatory;

        }

        public TimeSpan CalculateHoursFromBeginningOfYear(List<Booking> bookings)
        {
            TimeSpan mandatoryHours = new TimeSpan();
            foreach (var booking in bookings) 
                {
                    if (booking.Deleted != true)
                    {

                        switch (booking.BookingTyp)
                        {
                            case "Urlaub"://todo: Den Eigentlichen Zeittypen benutzen                            
                                mandatoryHours = mandatoryHours + TimeSpan.Parse("8:00");
                                break;
                            case "Krank":
                                mandatoryHours = mandatoryHours + TimeSpan.Parse("8:00");
                                break;
                            default:
                                mandatoryHours = mandatoryHours + booking.Duration;
                                break;
                        }
                    }
                }
            return mandatoryHours;
        }

        public void LoadBookingsByDay(string employeeId, DateTime from, DateTime till)
        {
            DateTime date = from;
            BookingsByDay.Clear();

            while (date<=till)
            {            
                var bookings = bookingDataService.GetByEmployeeIdAndDate(employeeId, date, date);
                
                BookingDay bd = new BookingDay();
                foreach (var item in bookings)
                {
                    if (item.Deleted!=true)
                    {
                        bd.Bookings.Add(item);
                    }                                 
                }
                bd.sumHours();
                if (bd.BookedHours!=TimeSpan.Zero)
                {
                    BookingsByDay.Add(bd);
                }
                
                date = date.AddDays(1);
            }
        }


    }
}
