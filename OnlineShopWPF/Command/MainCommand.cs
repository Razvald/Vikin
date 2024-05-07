using OnlineShopWpf.Database;
using OnlineShopWPF.ViewModels;

namespace OnlineShopWPF.Command
{
    public class MainCommand : CommandBase
    {
        private readonly Context _context;
        private readonly ViewModelStore _viewModelStore;
        private readonly StaffStore _employeeStore;

        public MainCommand(ViewModelStore viewModelStore, StaffStore employeeStore, Context context)
        {
            _context = context;
            _viewModelStore = viewModelStore;
            _employeeStore = employeeStore;
        }
        public override void Execute(object parameter)
        {

            if (parameter is string viewModelName)
            {
                Func<ViewModelStore, StaffStore, ViewModelBase> createViewModel = viewModelName switch
                {
                    "LoginViewModel" => (vmStore, empStore) => new LoginViewModel(vmStore, empStore, _context),
                    "PickupEmployeeViewModel" => (vmStore, empStore) => new PickupStaffViewModel(_context, _employeeStore),
                    "ProductsViewModel" => (vmStore, empStore) => new ProductsViewModel(_context, _employeeStore),
                    "StatisticsViewModel" => (vmStore, empStore) => new StatisticsViewModel(_context, _employeeStore),
                    _ => throw new ArgumentException("Invalid ViewModel name: " + parameter),
                };

                NavigateCommand navigateCommand = new(_viewModelStore, _employeeStore, createViewModel);
                navigateCommand.Execute(null);
            }
            else
            {
                throw new ArgumentException("Invalid parameter type");
            }
        }
    }
}
