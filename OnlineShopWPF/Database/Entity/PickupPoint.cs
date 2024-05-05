using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopWpf.Database.Entity
{
    public class PickupPoint
    {
        public int ID { get; set; }
        public string Location { get; set; }
        public int Turnover { get; set; }
        public decimal Rating { get; set; }
    }
}
