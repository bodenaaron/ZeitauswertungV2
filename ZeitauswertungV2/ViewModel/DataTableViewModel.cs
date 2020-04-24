using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeitauswertungV2.Data;

namespace ZeitauswertungV2.ViewModel
{
    class DataTableViewModel : IDataTableViewModel
    {
        private IBookingDataService bookingDataService;

        public DataTableViewModel(IBookingDataService bookingDataService)
        {
            this.bookingDataService = bookingDataService;
        }

        public async Task LoadAsyncBookings(string employeeId)
        {
            var bookings = await bookingDataService.GetByEmployeeIdAsync(employeeId);
        }
    }
}
