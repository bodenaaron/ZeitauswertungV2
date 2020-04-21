using System;

namespace ZeitauswertungV2.Model
{
    public class Booking
    {
        public int Id { get; set; }

        public Employee Employee { get; set; }
        public Assignment Assignment { get; set; }
        public DateTime Date { get; set; }
        public string Comment { get; set; }
        public DateTime BookingFrom { get; set; }
        public DateTime BookingTill { get; set; }
        public bool Deleted { get; set;}
        public BookingTyp BookingTyp { get; set; }

    }
}
