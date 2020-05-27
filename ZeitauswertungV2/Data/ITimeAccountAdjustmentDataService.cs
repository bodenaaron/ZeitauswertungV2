using System.Collections.Generic;
using System.Threading.Tasks;
using ZeitauswertungV2.Model;

namespace ZeitauswertungV2.UI.Data
{
    public interface ITimeAccountAdjustmentDataService
    {
        Task<List<TimeAccountAdjustment>> GetAllAsync();


        Task<List<TimeAccountAdjustment>> GetByEmployeeIdAsync(string employeeId);


        void SetTimeAccountAdjustment(TimeAccountAdjustment accountAdjustment);
        
    }
}