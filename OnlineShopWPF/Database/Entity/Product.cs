using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopWpf.Database.Entity
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string Title { get; set; }
        public int Price { get; set; }
        public int Quantity { get; set; }
        public decimal Rating { get; set; }


        [ForeignKey("Seller")]
        public int SellerID { get; set; }

        [ForeignKey("Category")]
        public int CategoryID { get; set; } = 1;

        public Seller Seller { get; set; }
        public Category Category { get; set; }
    }
}
