using OnlineShopWpf.Database;
using OnlineShopWPF.Command;
using OnlineShopWPF.Models;
using System.Windows.Input;

namespace OnlineShopWPF.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        public LoginModel _loginM;
        public LoginViewModel(ViewModelStore viewModelStore, StaffStore staffStore, Context dbContext)
        {
            _loginM = new LoginModel();
            LoginCommand = new LoginCommand(viewModelStore, staffStore, this, dbContext);
        }

        public string Login
        {
            get { return _loginM.LoginEmail; }
            set
            {
                _loginM.LoginEmail = value;
                OnPropertyChanged(nameof(Login));
            }
        }

        public string Password
        {
            get { return _loginM.Password; }
            set
            {
                _loginM.Password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        public ICommand LoginCommand { get; set; }
    }
}
