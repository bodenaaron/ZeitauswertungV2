using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeitauswertungV2.DataAccess;
using ZeitauswertungV2.Model;

namespace ZeitauswertungV2.UI.Data
{
    class BookingDataService : IBookingDataService

    {
        private Func<BookingDbContext> contextCreator;

        public BookingDataService(Func<BookingDbContext> contextCreator)
        {
            this.contextCreator = contextCreator;
        }

        public async Task<List<Booking>> GetAllAsync()
        {
            using (var ctx = contextCreator())
            {
                return await ctx.Bookings.AsNoTracking().ToListAsync();
            }
        }

        public async Task<List<Booking>> GetByEmployeeIdAsync(string employeeId)
        {
            using (var ctx = contextCreator())
            {
                return await ctx.Bookings.AsNoTracking().Where(b=>b.Employee ==employeeId).OrderByDescending(b => b.Date).ToListAsync();               
            }
        }

        public async Task<List<Booking>> GetByEmployeeIdAndDateAsync(string employeeId, DateTime from, DateTime till )
        {
            if (till < from)
            {
                till = DateTime.Now;
            }
            using (var ctx = contextCreator())
            {
                return await ctx.Bookings.AsNoTracking().Where(b => b.Employee == employeeId).Where(b=>b.Date>=from&&b.Date<=till).OrderByDescending(b=>b.Date).ToListAsync();
            }
        }
    }
}
