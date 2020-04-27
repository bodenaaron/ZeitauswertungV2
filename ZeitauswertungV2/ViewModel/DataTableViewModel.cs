using Prism.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using ZeitauswertungV2.Data;
using ZeitauswertungV2.Event;
using ZeitauswertungV2.Model;
using ZeitauswertungV2.UI.ViewModel;
using ZeitauswertungV2.Utility;

namespace ZeitauswertungV2.ViewModel
{
    class DataTableViewModel : ViewModelBase, IDataTableViewModel
    {

        private IBookingDataService bookingDataService;
        private IEventAggregator eventAggregator;

        public ObservableCollection<Booking> Bookings { get; }

        private TimeSpan targetHours;

        #region Überwachte Variablen
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

        private string displaySickHours;
        private string displayTargetHours;
        private TimeSpan workedHours;

        #endregion


        public DataTableViewModel(IBookingDataService bookingDataService, IEventAggregator eventAggregator)
        {
            this.bookingDataService = bookingDataService;
            this.eventAggregator = eventAggregator;
            this.eventAggregator.GetEvent<EmployeeChangedEvent>().Subscribe(OnEmployeeChanged);
            this.eventAggregator.GetEvent<DateChangedEvent>().Subscribe(OnDateChanged);
            Bookings = new ObservableCollection<Booking>();
            
        }

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
                Bookings.Add(item);
            }
        }

        public async void OnDateChanged(DateChangedEventArgs dateChangedEventArgs)
        {
            await LoadAsyncBookingsBeetweenDate(dateChangedEventArgs.EmployeeId,dateChangedEventArgs.From,dateChangedEventArgs.Till);
            calculateHours();
            calculateTargetHours(dateChangedEventArgs);

        }

        private void calculateTargetHours(DateChangedEventArgs dateChangedEventArgs)
        {
            DateChecker dc = new DateChecker();
            TargetHours = TimeSpan.Zero;
            TimeSpan duration = dateChangedEventArgs.Till - dateChangedEventArgs.From;

            foreach (var day in EachDay(dateChangedEventArgs.From, dateChangedEventArgs.Till))
            {
                if (dc.IsWorkday(day))
                {
                    TargetHours = TargetHours.Add(TimeSpan.FromHours(8));
                }
                
            }
            
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


                switch (booking.BookingTyp)
                {
                    case "Urlaub"://todo: Den Eigentlichen Zeittypen benutzen
                        VacationHours = VacationHours + TimeSpan.Parse("8:00"); //die Im Bearbeiter hinterlegten Pflichtstunden benutzen
                        AllHours= AllHours + TimeSpan.Parse("8:00");
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

        public async Task LoadAsyncBookingsBeetweenDate(string employeeId, DateTime from, DateTime till)
        {
            var bookings = await bookingDataService.GetByEmployeeIdAndDateAsync(employeeId,from, till);
            Bookings.Clear();
            foreach (var item in bookings)
            {
                Bookings.Add(item);
            }
        }


    }
}
