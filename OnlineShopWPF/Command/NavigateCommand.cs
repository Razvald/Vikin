using OnlineShopWPF.ViewModels;

namespace OnlineShopWPF.Command
{
    public class NavigateCommand : CommandBase
    {
        private readonly ViewModelStore _viewModelStore;
        private readonly StaffStore _employeeStore;
        private readonly Func<ViewModelStore, StaffStore, ViewModelBase> _createViewModel;

        public NavigateCommand(ViewModelStore viewModelStore, StaffStore employeeStore, Func<ViewModelStore, StaffStore, ViewModelBase> createViewModel)
        {
            _viewModelStore = viewModelStore;
            _employeeStore = employeeStore;
            _createViewModel = createViewModel;
        }

        public override void Execute(object parameter)
        {
            _viewModelStore.CurrentViewModel = _createViewModel(_viewModelStore, _employeeStore);
        }
    }
}
