using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopWpf.Database.Entity
{
    public class PickupEmployee
    {
        public string EmployeeName { get; set; }
        public string PickupPointLocation { get; set; }
        public int OrdersCount { get; set; }
    }
}
