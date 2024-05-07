using OnlineShopWpf.Database;
using OnlineShopWpf.Database.Entity;
using OnlineShopWPF.Command;
using System.Windows.Input;

namespace OnlineShopWPF.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly Context _context;
        private readonly ViewModelStore _viewModelStore;
        private readonly StaffStore _employeeStore;

        public ViewModelBase CurrentViewModel => _viewModelStore.CurrentViewModel;
        public Staff CurrentEmployee
        {
            get
            {
                Staff employee = _employeeStore.CurrentStaff;
                if (employee != null)
                {
                    employee.Role = _context.Roles.FirstOrDefault(r => r.ID == employee.RoleID);
                }
                return employee;
            }
        }

        public MainViewModel(ViewModelStore viewModelStore, StaffStore employeeStore, Context context)
        {
            _context = context;
            _viewModelStore = viewModelStore;
            _employeeStore = employeeStore;

            _viewModelStore.CurrentViewModel = new ProductsViewModel(_context, _employeeStore);
            _viewModelStore.CurrentViewModelChanged += OnCurrentViewModelChanged;

            _employeeStore.CurrentStaff = new Staff();
            _employeeStore.CurrentStaffChanged += OnCurrentEmployeeChanged;

            ChangeViewModelCommand = new MainCommand(_viewModelStore, _employeeStore, _context);
        }

        private void OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }

        private void OnCurrentEmployeeChanged()
        {
            OnPropertyChanged(nameof(CurrentEmployee));
        }

        public ICommand ChangeViewModelCommand { get; }
    }
}
