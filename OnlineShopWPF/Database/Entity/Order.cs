using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopWpf.Database.Entity
{
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public DateTime OrderDate { get; set; }
        public int TotalAmount { get; set; }


        [ForeignKey("Client")]
        public int ClientID { get; set; }

        [ForeignKey("PickupPoint")]
        public int PickupPointID { get; set; }

        public Client Client { get; set; }
        public PickupPoint PickupPoint { get; set; }
    }
}
