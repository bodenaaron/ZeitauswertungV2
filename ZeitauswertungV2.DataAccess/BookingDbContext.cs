using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeitauswertungV2.Model;

namespace ZeitauswertungV2.DataAccess
{
    public class BookingDbContext:DbContext
    {
        public BookingDbContext() : base("BookingDb") { }

        public DbSet<Employee> Employees{ get; set; }

        public DbSet<Booking> Bookings { get; set; }
        public DbSet<TimeAccountAdjustment> TimeAccounts { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
