using Microsoft.EntityFrameworkCore;
using OnlineShopWpf.Database;
using OnlineShopWpf.Database.Entity;
using System.Collections.ObjectModel;

namespace OnlineShopWPF.ViewModels
{
    public class StatisticsViewModel : ViewModelBase
    {
        private readonly Context _context;

        public ObservableCollection<Staff> EmployeesDataList { get; set; } = new ObservableCollection<Staff>();
        public ObservableCollection<PickupPoint> PickupPointsDataList { get; set; } = new ObservableCollection<PickupPoint>();

        public StatisticsViewModel(Context context)
        {
            _context = context;
            EmployeesDataList = new ObservableCollection<Staff>(_context.Staffs);
            PickupPointsDataList = new ObservableCollection<PickupPoint>(_context.PickupPoints);
        }
    }
}
