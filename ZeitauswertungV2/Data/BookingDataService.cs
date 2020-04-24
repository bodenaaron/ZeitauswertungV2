using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeitauswertungV2.DataAccess;
using ZeitauswertungV2.Model;

namespace ZeitauswertungV2.Data
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
                return await ctx.Bookings.AsNoTracking().Where(b=>b.Employee.Id ==employeeId).ToListAsync();               
            }
        }
    }
}
