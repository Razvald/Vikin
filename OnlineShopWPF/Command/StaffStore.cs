using OnlineShopWpf.Database.Entity;

namespace OnlineShopWPF.Command
{
    public class StaffStore
    {
        Staff _currentStaff = new();

        public Staff CurrentStaff
        {
            get { return _currentStaff; }
            set
            {
                _currentStaff = value;
                OnCurrentStaffChanged();
            }
        }

        public event Action CurrentStaffChanged;
        private void OnCurrentStaffChanged()
        {
            CurrentStaffChanged?.Invoke();
        }
    }
}
