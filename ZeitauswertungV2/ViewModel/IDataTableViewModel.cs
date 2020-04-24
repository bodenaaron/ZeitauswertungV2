using System.Threading.Tasks;

namespace ZeitauswertungV2.ViewModel
{
    public interface IDataTableViewModel
    {
        Task LoadAsyncBookings(string employeeId);
    }
}