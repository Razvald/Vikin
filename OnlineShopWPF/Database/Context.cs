using Microsoft.EntityFrameworkCore;
using OnlineShopWpf.Database.Entity;

namespace OnlineShopWpf.Database
{
    public class Context : DbContext
    {
        public DbSet<Client> Clients { get; set; }
        public DbSet<Staff> Staffs { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<PickupPoint> PickupPoints { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Seller> Sellers { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Role> Roles { get; set; }

        public Context() : base()
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        private readonly string connectString = "Server=(local); Database=OnlineStore; Integrated Security=true; TrustServerCertificate=True";

        //private readonly string connectString = "Data Source=(localdb)\\MSSQLLocalDB; Initial Catalog=db_online_store; Integrated Security=true;";

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            SeedData(modelBuilder);
        }

        private static void SeedData(ModelBuilder modelBuilder)
        {
            List<Client> clients = new()
            {
                new() { ID = 1, Name = "Александр Смирнов", Email = "smirnov@example.com", TotalOrders = 1 },
                new() { ID = 2, Name = "Екатерина Иванова", Email = "ivanova@example.com", TotalOrders = 1 },
                new() { ID = 3, Name = "Максим Петров", Email = "petrov@example.com", TotalOrders = 1 },
                new() { ID = 4, Name = "Ольга Сидорова", Email = "sidorova@example.com", TotalOrders = 1 },
                new() { ID = 5, Name = "Иван Козлов", Email = "kozlov@example.com", TotalOrders = 1 }
            };
            List<Order> orders = new()
            {
                new() { ID = 1, ClientID = 1, OrderDate = new DateTime(2024, 2, 5), TotalAmount = 12000, PickupPointID = 1 },
                new() { ID = 2, ClientID = 2, OrderDate = new DateTime(2024, 2, 4), TotalAmount = 21000, PickupPointID = 2 },
                new() { ID = 3, ClientID = 3, OrderDate = new DateTime(2024, 2, 3), TotalAmount = 28000, PickupPointID = 3 },
                new() { ID = 4, ClientID = 4, OrderDate = new DateTime(2024, 2, 2), TotalAmount = 42000, PickupPointID = 4 },
                new() { ID = 5, ClientID = 5, OrderDate = new DateTime(2024, 2, 1), TotalAmount = 15000, PickupPointID = 5 },
                new() { ID = 6, ClientID = 2, OrderDate = new DateTime(2024, 2, 1), TotalAmount = 11000, PickupPointID = 2 }
            };
            List<Staff> staffs = new()
            {
                new() { ID = 1, RoleID = 2, Name = "Андрей Кузнецов", Login = "1", Password = "1", PickupPointID = 1 },
                new() { ID = 2, RoleID = 3, Name = "Мария Васильева", Login = "2", Password = "2", Salary = 15000, PickupPointID = 2 },
                new() { ID = 3, RoleID = 3, Name = "Сергей Попов", Login = "sergeyp", Password = "654321", Salary = 10000, PickupPointID = 3 },
                new() { ID = 4, RoleID = 3, Name = "Елена Новикова", Login = "elenan", Password = "123", Salary = 11000, PickupPointID = 4 },
                new() { ID = 5, RoleID = 3, Name = "Алексей Иванов", Login = "alekseyi", Password = "321", Salary = 13000, PickupPointID = 5 }
            };
            List<OrderDetail> ordersDetail = new()
            {
                new() { ID = 1, OrderID = 1, ProductID = 4, Quantity = 4, Subtotal = 12000 },
                new() { ID = 2, OrderID = 2, ProductID = 16, Quantity = 2, Subtotal = 20000 },
                new() { ID = 3, OrderID = 3, ProductID = 8, Quantity = 14, Subtotal = 28000 },
                new() { ID = 4, OrderID = 2, ProductID = 11, Quantity = 1, Subtotal = 1000 },
                new() { ID = 5, OrderID = 4, ProductID = 20, Quantity = 6, Subtotal = 42000 },
                new() { ID = 6, OrderID = 5, ProductID = 1, Quantity = 2, Subtotal = 15000 }
            };
            List<PickupPoint> pickupPoints = new()
            {
                new() { ID = 1, Location = "ул. Ленина, 10", Turnover = 1, Rating = 3.9M },
                new() { ID = 2, Location = "пр. Победы, 25", Turnover = 2, Rating = 4.2M },
                new() { ID = 3, Location = "пр. Советская, 5", Turnover = 1, Rating = 3.2M },
                new() { ID = 4, Location = "пр. Мира, 15", Turnover = 1, Rating = 5.0M },
                new() { ID = 5, Location = "пр. Гагарина, 30", Turnover = 1, Rating = 4.0M }
            };
            List<Product> products = new()
            {
                new() { ID = 1, Title = "Ноутбук Dell XPS 13", Price = 15000, Quantity = 10, SellerID = 1, Rating = 4.2m, CategoryID = 3 },
                new() { ID = 2, Title = "Смартфон iPhone 12", Price = 10000, Quantity = 15, SellerID = 2, Rating = 3.2m, CategoryID = 4 },
                new() { ID = 3, Title = "Телевизор Samsung QLED 55\"", Price = 1200, Quantity = 8, SellerID = 3, Rating = 5, CategoryID = 4 },
                new() { ID = 4, Title = "Наушники Sony WH-1000XM4", Price = 3000, Quantity = 20, SellerID = 4, Rating = 4.5m, CategoryID = 4 },
                new() { ID = 5, Title = "Кофемашина DeLonghi Magnifica", Price = 6000, Quantity = 12, SellerID = 5, Rating = 4.8m, CategoryID = 2 },
                new() { ID = 6, Title = "Планшет Samsung Galaxy Tab S7", Price = 8000, Quantity = 18, SellerID = 1, Rating = 4.3m, CategoryID = 4 },
                new() { ID = 7, Title = "Фотоаппарат Canon EOS R5", Price = 25000, Quantity = 5, SellerID = 2, Rating = 4.2m, CategoryID = 4 },
                new() { ID = 8, Title = "Беспроводные наушники Apple AirPods Pro", Price = 2500, Quantity = 25, SellerID = 3, Rating = 4.2m, CategoryID = 4 },
                new() { ID = 9, Title = "Умные часы Garmin Forerunner 945", Price = 6000, Quantity = 10, SellerID = 4, Rating = 4, CategoryID = 4 },
                new() { ID = 10, Title = "Холодильник LG InstaView", Price = 15000, Quantity = 8, SellerID = 5, Rating = 4, CategoryID = 2 },
                new() { ID = 11, Title = "Умные колонки Amazon Echo", Price = 1000, Quantity = 30, SellerID = 1, Rating = 3.3m, CategoryID = 4 },
                new() { ID = 12, Title = "Видеокарта NVIDIA GeForce RTX 3080", Price = 12000, Quantity = 15, SellerID = 2, Rating = 4.5m, CategoryID = 3 },
                new() { ID = 13, Title = "Фитнес трекер Fitbit Charge 4", Price = 1500, Quantity = 20, SellerID = 3, Rating = 4.8m, CategoryID = 4 },
                new() { ID = 14, Title = "Соковыжималка Bosch MESM731M", Price = 2000, Quantity = 10, SellerID = 4, Rating = 4.3m, CategoryID = 2 },
                new() { ID = 15, Title = "Ноутбук Apple MacBook Pro 13\"", Price = 18000, Quantity = 7, SellerID = 5, Rating = 3.8m, CategoryID = 3 },
                new() { ID = 16, Title = "Квадрокоптер DJI Mavic Air 2", Price = 10000, Quantity = 12, SellerID = 1, Rating = 4, CategoryID = 4 },
                new() { ID = 17, Title = "Монитор LG UltraGear 27GN950-B", Price = 7000, Quantity = 10, SellerID = 2, Rating = 3.2m, CategoryID = 3 },
                new() { ID = 18, Title = "Кофеварка DeLonghi Dedica", Price = 1500, Quantity = 15, SellerID = 3, Rating = 5, CategoryID = 2 },
                new() { ID = 19, Title = "Умный термостат Nest Learning Thermostat", Price = 2500, Quantity = 8, SellerID = 4, Rating = 4.2m, CategoryID = 2 },
                new() { ID = 20, Title = "Смарт-часы Apple Watch Series 6", Price = 7000, Quantity = 6, SellerID = 5, Rating = 4.2m, CategoryID = 4 }
            };
            List<Seller> sellers = new()
            {
                new() { ID = 1, Name = "Иванов Иван", Email = "ivanov@example.com", Rating = 4.2m },
                new() { ID = 2, Name = "Петров Петр", Email = "petrov@example.com", Rating = 4.0m },
                new() { ID = 3, Name = "Сидорова Анна", Email = "sidorova@example.com", Rating = 3.1m },
                new() { ID = 4, Name = "Козлов Дмитрий", Email = "kozlov@example.com", Rating = 4.8m },
                new() { ID = 5, Name = "Михайлова Елена", Email = "mikhaylova@example.com", Rating = 3.9m }
            };
            List<Category> categories = new()
            {
                new() { ID = 1, Name = "All" },
                new() { ID = 2, Name = "Appliances" },
                new() { ID = 3, Name = "Computers" },
                new() { ID = 4, Name = "Electronics" }
            };
            List<Role> roles = new()
            {
                new() { ID = 1, Name = "Guest" },
                new() { ID = 2, Name = "Administrator" },
                new() { ID = 3, Name = "Worker" }
            };

            modelBuilder.Entity<Client>().HasData(clients);
            modelBuilder.Entity<Staff>().HasData(staffs);
            modelBuilder.Entity<Order>().HasData(orders);
            modelBuilder.Entity<OrderDetail>().HasData(ordersDetail);
            modelBuilder.Entity<PickupPoint>().HasData(pickupPoints);
            modelBuilder.Entity<Product>().HasData(products);
            modelBuilder.Entity<Seller>().HasData(sellers);
            modelBuilder.Entity<Category>().HasData(categories);
            modelBuilder.Entity<Role>().HasData(roles);
        }
    }
}