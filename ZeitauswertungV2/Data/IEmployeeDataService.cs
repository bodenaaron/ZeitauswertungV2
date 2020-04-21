using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeitauswertungV2.Model;

namespace ZeitauswertungV2.UI.Data
{
    public interface IEmployeeDataService
    {
        Task<List<Employee>> GetAllEmployeeAsync();
    }
}
