using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopWpf.Database.Entity
{
    public class Staff
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public int? Salary { get; set; }


        [ForeignKey("Role")]
        public int RoleID { get; set; } = 1;

        [ForeignKey("PickupPoint")]
        public int PickupPointID { get; set; }

        public PickupPoint PickupPoint { get; set; }
        public Role Role { get; set; }
    }
}
