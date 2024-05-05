using OnlineShopWpf.Database.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopWPF.Models
{
    public class PickupStaffModel
    {
        public string Search { get; set; }
        public IEnumerable<PickupEmployee> EmployeesOrders { get; set; }
    }
}
