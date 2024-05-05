using OnlineShopWpf.Database;
using OnlineShopWPF.Command;
using OnlineShopWPF.ViewModels;
using OnlineShopWPF.Views;
using System.Windows;

namespace OnlineShopWPF
{
    public partial class App : Application
    {
        private readonly Context _context;
        private readonly ViewModelStore _viewModelStore;
        private readonly StaffStore _employeeStore;
        public App()
        {
            _context = new Context();
            _viewModelStore = new ViewModelStore();
            _employeeStore = new StaffStore();

            _viewModelStore.CurrentViewModel = new MainViewModel(_viewModelStore, _employeeStore, _context);
        }
        protected override void OnStartup(StartupEventArgs e)
        {
            MainWindow = new Main()
            {
                DataContext = new MainViewModel(_viewModelStore, _employeeStore, _context)
            };
            MainWindow.Show();

            base.OnStartup(e);
        }
    }
}
