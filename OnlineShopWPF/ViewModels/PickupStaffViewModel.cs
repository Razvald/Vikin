using OnlineShopWpf.Database;
using OnlineShopWpf.Database.Entity;
using OnlineShopWPF.Command;
using OnlineShopWPF.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace OnlineShopWPF.ViewModels
{
    public class PickupStaffViewModel : ViewModelBase
    {
        private readonly Context _context;
        private PickupStaffModel _pickupStaffM;
        private readonly StaffStore _employeeStore;

        public ObservableCollection<PickupEmployee> CombinedDataList { get; set; } = new ObservableCollection<PickupEmployee>();
        public ObservableCollection<PickupEmployee> _allStaff { get; set; } = new ObservableCollection<PickupEmployee>();

        private bool _isGuest;

        public bool IsGuest
        {
            get { return _isGuest; }
            set
            {
                _isGuest = value;
                OnPropertyChanged(nameof(IsGuest));
            }
        }

        private bool _isRead;

        public bool IsRead
        {
            get { return _isRead; }
            set
            {
                _isRead = value;
                OnPropertyChanged(nameof(IsRead));
            }
        }

        public PickupStaffViewModel(Context context, StaffStore employeeStore)
        {
            _context = context;
            _employeeStore = employeeStore;
            _pickupStaffM = new PickupStaffModel();
            PropertyChanged += ProductsVM_PropertyChanged;

            InitializeCombinedDataList();

            SaveCommand = new SaveCommand();

            if (_employeeStore.CurrentStaff.RoleID == 1 || _employeeStore.CurrentStaff.RoleID == 3)
            {
                IsGuest = false;
            }
            else
            {
                IsGuest = true;
            }

            if (_employeeStore.CurrentStaff.RoleID != 2)
            {
                IsRead = true;
            }
            else
            {
                IsRead = false;
            }
        }

        private void InitializeCombinedDataList()
        {
            var employees = _context.Staffs;
            var pickupPoints = _context.PickupPoints;
            var orders = _context.Orders;

            var ordersSummary = from employee in employees
                                join pickupPoint in pickupPoints on employee.PickupPointID equals pickupPoint.ID
                                join order in orders on pickupPoint.ID equals order.PickupPointID
                                where order.PickupPointID == employee.PickupPointID // Фильтруем заказы только для текущего сотрудника
                                group order by new { employee.Name, pickupPoint.Location } into groupedOrders
                                select new PickupEmployee
                                {
                                    EmployeeName = groupedOrders.Key.Name,
                                    PickupPointLocation = groupedOrders.Key.Location,
                                    OrdersCount = groupedOrders.Count() // Подсчитываем количество заказов для каждой группы
                                };

            foreach (var item in ordersSummary)
            {
                CombinedDataList.Add(item);
                _allStaff.Add(item);
            }
        }

        private void ProductsVM_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            CombinedDataList.Clear();

            foreach (var staff in _allStaff)
            {
                if (MatchesSearch(staff))
                {
                    CombinedDataList.Add(staff);
                }
            }
        }

        private bool MatchesSearch(PickupEmployee staff)
        {
            return string.IsNullOrEmpty(Search) || staff.EmployeeName.Contains(Search, StringComparison.CurrentCultureIgnoreCase);
        }

        public string Search
        {
            get { return _pickupStaffM.Search; }
            set { _pickupStaffM.Search = value; OnPropertyChanged(nameof(Search)); }
        }

        public ICommand SaveCommand { get; set; }
    }
}
