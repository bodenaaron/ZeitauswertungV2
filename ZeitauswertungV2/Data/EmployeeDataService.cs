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
    class EmployeeDataService:IEmployeeDataService
    {
        private Func<BookingDbContext> contextCreator;

        public EmployeeDataService(Func<BookingDbContext> contextCreator)
        {
            this.contextCreator = contextCreator;
        }

        public async Task<List<Employee>> GetAllEmployeeAsync()
        {
            using (var ctx = contextCreator())
            {
                return await ctx.Employees.AsNoTracking().ToListAsync();
            }            
        }
    }
}
