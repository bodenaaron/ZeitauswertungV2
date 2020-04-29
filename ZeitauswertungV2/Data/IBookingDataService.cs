using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ZeitauswertungV2.Model;

namespace ZeitauswertungV2.UI.Data
{
    public interface IBookingDataService
    {
        Task<List<Booking>> GetAllAsync();
        Task<List<Booking>> GetByEmployeeIdAsync(string employeeId);
        Task<List<Booking>> GetByEmployeeIdAndDateAsync(string employeeId, DateTime from, DateTime till);
    }
}