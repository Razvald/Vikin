using OnlineShopWpf.Database;
using OnlineShopWpf.Database.Entity;
using OnlineShopWPF.Command;
using OnlineShopWPF.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace OnlineShopWPF.ViewModels
{
    public class StatisticsViewModel : ViewModelBase
    {
        private readonly Context _context;
        private StatisticModel _statisticM;

        public ObservableCollection<Staff> EmployeesDataList { get; set; } = new ObservableCollection<Staff>();
        public ObservableCollection<Staff> _allStaff { get; set; } = new ObservableCollection<Staff>();
        public ObservableCollection<PickupPoint> PickupPointsDataList { get; set; } = new ObservableCollection<PickupPoint>();
        public ObservableCollection<PickupPoint> _allPoint { get; set; } = new ObservableCollection<PickupPoint>();

        public StatisticsViewModel(Context context)
        {
            _context = context;
            _statisticM = new StatisticModel();
            EmployeesDataList = new ObservableCollection<Staff>(_context.Staffs);
            _allStaff = new ObservableCollection<Staff>(_context.Staffs);
            PickupPointsDataList = new ObservableCollection<PickupPoint>(_context.PickupPoints);
            _allPoint = new ObservableCollection<PickupPoint>(_context.PickupPoints);

            PropertyChanged += StatisticsVM_PropertyChanged;

            SaveCommand = new SaveCommand();
        }

        private void StatisticsVM_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(SearchPoint))
            {
                UpdatePoint();
            }
            else if (e.PropertyName == nameof(SearchEmp))
            {
                UpdateStaff();
            }
        }

        private void UpdateStaff()
        {
            EmployeesDataList.Clear();

            foreach (var staff in _allStaff)
            {
                if (MatchesSearchStaff(staff))
                {
                    EmployeesDataList.Add(staff);
                }
            }
        }

        private void UpdatePoint()
        {
            PickupPointsDataList.Clear();

            foreach (var staff in _allPoint)
            {
                if (MatchesSearchPoint(staff))
                {
                    PickupPointsDataList.Add(staff);
                }
            }
        }

        private bool MatchesSearchStaff(Staff staff)
        {
            return string.IsNullOrEmpty(SearchEmp) || staff.Name.Contains(SearchEmp, StringComparison.CurrentCultureIgnoreCase);
        }

        private bool MatchesSearchPoint(PickupPoint point)
        {
            return string.IsNullOrEmpty(SearchPoint) || point.Location.Contains(SearchPoint, StringComparison.CurrentCultureIgnoreCase);
        }

        public string SearchPoint
        {
            get { return _statisticM.SearchPoint; }
            set { _statisticM.SearchPoint = value; OnPropertyChanged(nameof(SearchPoint)); }
        }

        public string SearchEmp
        {
            get { return _statisticM.SearchEmp; }
            set { _statisticM.SearchEmp = value; OnPropertyChanged(nameof(SearchEmp)); }
        }

        public ICommand SaveCommand { get; set; }
    }
}
