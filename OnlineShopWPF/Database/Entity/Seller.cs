﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopWpf.Database.Entity
{
    public class Seller
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public decimal Rating { get; set; }
    }
}
