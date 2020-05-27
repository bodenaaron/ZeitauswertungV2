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
    public class TimeAccountAdjustmentDataService : ITimeAccountAdjustmentDataService

    {
        private Func<BookingDbContext> contextCreator;

        public TimeAccountAdjustmentDataService(Func<BookingDbContext> contextCreator)
        {
            this.contextCreator = contextCreator;
        }

        public async Task<List<TimeAccountAdjustment>> GetAllAsync()
        {
            using (var ctx = contextCreator())
            {
                return await ctx.TimeAccounts.AsNoTracking().ToListAsync();
            }
        }

        public async Task<List<TimeAccountAdjustment>> GetByEmployeeIdAsync(string employeeId)
        {
            using (var ctx = contextCreator())
            {
                return await ctx.TimeAccounts.AsNoTracking().Where(b=>b.Employee == employeeId).Where(b=>b.DateOfAdjustment.Year==DateTime.Now.Year).ToListAsync();               
            }
        }
        
        public void SetTimeAccountAdjustment(TimeAccountAdjustment accountAdjustment)
        {
            using (var ctx = contextCreator())
            {
                ctx.TimeAccounts.Add(accountAdjustment);
                ctx.SaveChanges();
            }
        }
    }
}
