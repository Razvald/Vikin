using OnlineShopWpf.Database;
using OnlineShopWpf.Database.Entity;
using OnlineShopWPF.Command;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace OnlineShopWPF.ViewModels
{
    public class PickupStaffViewModel : ViewModelBase
    {
        private readonly Context _context;
        public ObservableCollection<PickupEmployee> CombinedDataList { get; set; } = new ObservableCollection<PickupEmployee>();

        public PickupStaffViewModel(Context context)
        {
            _context = context;

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
            }
        }
        public ICommand SaveCommand { get; set; }
    }
}
