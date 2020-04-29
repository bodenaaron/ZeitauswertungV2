using Nager.Date;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeitauswertungV2.Model;

namespace ZeitauswertungV2.UI.Utility
{
    class DateChecker
    {
        public bool IsWorkday(DateTime date) 
        {
            if (DateSystem.IsWeekend(date, CountryCode.DE))
            {
                return false;
            }
            foreach (Holiday f in Holidays())
            {
                if (f.Date == date)
                {
                    return false;
                }
            }
            return true;
        }




        public IEnumerable<Holiday> Holidays()
        {
            yield return new Holiday { Name = "Neujahr", Date = new DateTime(2020, 01, 01), Hours = 0 };
            yield return new Holiday { Name = "Karfreitag", Date = new DateTime(2020, 04, 10), Hours = 0 };
            yield return new Holiday { Name = "Ostermontag", Date = new DateTime(2020, 04, 13), Hours = 0 };
            yield return new Holiday { Name = "Tag der Arbeit", Date = new DateTime(2020, 05, 1), Hours = 0 };
            yield return new Holiday { Name = "Christie Himmelfahrt", Date = new DateTime(2020, 05, 21), Hours = 0 };
            yield return new Holiday { Name = "Pfingstmontag", Date = new DateTime(2020, 06, 01), Hours = 0 };
            yield return new Holiday { Name = "Weltkindertag", Date = new DateTime(2020, 09, 20), Hours = 0 };
            yield return new Holiday { Name = "Tag der Deutschen Einheit", Date = new DateTime(2020, 10, 03), Hours = 0 };
            yield return new Holiday { Name = "Reformationstag", Date = new DateTime(2020, 10, 31), Hours = 0 };
            //yield return neHolidayag { Name = "Weihnachten",Datum=new DateTime(2020,10,31),Stunden=4 };
            yield return new Holiday { Name = "1 Weihnachtstag", Date = new DateTime(2020, 12, 25), Hours = 0 };
            yield return new Holiday { Name = "2 Weihnachtstag", Date = new DateTime(2020, 12, 26), Hours = 0 };
            //yield return neHolidayag { Name = "Sylvester",Datum=new DateTime(2020,12,26),Stunden=4 };
        }
    }
}
