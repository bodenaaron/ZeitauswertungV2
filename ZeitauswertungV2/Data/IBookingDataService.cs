﻿using System.Collections.Generic;
using System.Threading.Tasks;
using ZeitauswertungV2.Model;

namespace ZeitauswertungV2.Data
{
    public interface IBookingDataService
    {
        Task<List<Booking>> GetAllAsync();
        Task<List<Booking>> GetByEmployeeIdAsync(string employeeId);
    }
}