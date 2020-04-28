using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeitauswertungV2.Model
{
    public class BookingDay
    {
        public DateTime Date { get; set; }       
        public List<Booking> Bookings { get; set; }
        public TimeSpan BookedHours { get; set; }
        public string Employee { get; set; }

        public BookingDay()
        {
            this.Bookings = new List<Booking>();
        }

        public void sumHours()
        {
            foreach(var booking in Bookings)
            {
                BookedHours = BookedHours + booking.Duration;
                Date = booking.Date;//todo:unsauber
                Employee = booking.Employee;
            }

        }
    }
}
