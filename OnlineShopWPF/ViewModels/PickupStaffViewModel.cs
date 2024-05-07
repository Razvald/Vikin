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
        public ObservableCollection<PickupEmployee> CombinedDataList { get; set; } = new ObservableCollection<PickupEmployee>();
        public ObservableCollection<PickupEmployee> _allStaff { get; set; } = new ObservableCollection<PickupEmployee>();

        public PickupStaffViewModel(Context context)
        {
            _context = context;
            _pickupStaffM = new PickupStaffModel();
            PropertyChanged += ProductsVM_PropertyChanged;

            InitializeCombinedDataList();

            SaveCommand = new SaveCommand();
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
