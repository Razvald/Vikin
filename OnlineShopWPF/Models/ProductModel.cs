using OnlineShopWpf.Database.Entity;

namespace OnlineShopWPF.Models
{
    public class ProductModel
    {
        public string Search { get; set; }
        public IEnumerable<Product> Products { get; set; }
    }
}
