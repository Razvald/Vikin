using OnlineShopWpf.Database;
using OnlineShopWpf.Database.Entity;
using OnlineShopWPF.ViewModels;
using System.Windows;

namespace OnlineShopWPF.Command
{
    public class LoginCommand : CommandBase
    {
        private readonly Context _context;
        private readonly ViewModelStore _viewModelStore;
        private readonly StaffStore _employeeStore;
        private readonly LoginViewModel _loginViewModel;

        public LoginCommand(ViewModelStore viewModelStore, StaffStore employeeStore, LoginViewModel loginViewModel, Context context)
        {
            _context = context;
            _viewModelStore = viewModelStore;
            _employeeStore = employeeStore;
            _loginViewModel = loginViewModel;
        }

        public override void Execute(object parameter)
        {
            try
            {
                Staff user = _context.Staffs
                    .FirstOrDefault(e => e.Login == _loginViewModel.Login && e.Password == _loginViewModel.Password)!;

                if (user != null)
                {
                    _employeeStore.CurrentStaff = user;
                    if (_employeeStore.CurrentStaff.Role == _context.Roles.FirstOrDefault(r => r.ID == 1))
                        _viewModelStore.CurrentViewModel = new StatisticsViewModel(_context);
                    else
                        _viewModelStore.CurrentViewModel = new ProductsViewModel(_context);
                }
                else
                {
                    MessageBox.Show("Неверный логин или пароль.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
