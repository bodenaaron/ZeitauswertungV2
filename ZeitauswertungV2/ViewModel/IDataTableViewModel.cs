using System.Threading.Tasks;

namespace ZeitauswertungV2.UI.ViewModel
{
    public interface IDataTableViewModel
    {
        Task LoadAsyncBookings(string employeeId);
    }
}