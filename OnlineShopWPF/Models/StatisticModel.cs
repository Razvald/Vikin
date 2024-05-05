using OnlineShopWpf.Database.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopWPF.Models
{
    public class StatisticModel
    {
        public string SearchPoint { get; set; }
        public string SearchEmp { get; set; }

        public IEnumerable<Staff> Staffs { get; set; }
        public IEnumerable<PickupPoint> PickupPoints { get; set; }
    }
}
