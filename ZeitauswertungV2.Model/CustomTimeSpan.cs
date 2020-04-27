using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeitauswertungV2.Model
{
    public class CustomTimeSpan
    {
        private TimeSpan timeSpan;
        public TimeSpan TimeSpan
        {
            get
            {
                return timeSpan;
            }
            set
            {
                timeSpan = value;
                FormattedTimeSpan = string.Format("{0:0.}:{1}", timeSpan.TotalHours, timeSpan.Minutes);
            }
        }
        public string FormattedTimeSpan { get; set; }
        
        public CustomTimeSpan(TimeSpan timeSpan)
        {
            TimeSpan = timeSpan;
        }

        public CustomTimeSpan()
        {
            TimeSpan = new TimeSpan();
        }
    }
}
