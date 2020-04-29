using System.Threading.Tasks;

namespace ZeitauswertungV2.UI.ViewModel
{
    public class MainViewModel:ViewModelBase
    {
        public MainViewModel(IInputBarViewModel inputBarViewModel, IDataTableViewModel dataTableViewModel)
        {
            InputBarViewModel = inputBarViewModel;
            DataTableViewModel = dataTableViewModel;
            
        }

        public IInputBarViewModel InputBarViewModel { get;}
        public IDataTableViewModel DataTableViewModel { get;}

        public async Task LoadAsyncEmployee()
        {
            await InputBarViewModel.LoadAsyncEmployee();
        }
    }
}
