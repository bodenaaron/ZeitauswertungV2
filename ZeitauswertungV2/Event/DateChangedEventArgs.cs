using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeitauswertungV2.Event
{
    class DateChangedEventArgs
    {
        public string EmployeeId { get; set; }
        public DateTime From { get; set; }
        public DateTime Till { get; set; }
    }
}
