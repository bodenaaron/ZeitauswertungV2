using Prism.Commands;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ZeitauswertungV2.Data;
using ZeitauswertungV2.Event;
using ZeitauswertungV2.Model;
using ZeitauswertungV2.UI.Data;
using ZeitauswertungV2.ViewModel;

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
