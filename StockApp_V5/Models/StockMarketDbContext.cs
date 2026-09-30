using Microsoft.EntityFrameworkCore;



namespace StockApp_V5.Models
{
    public class StockMarketDbContext : DbContext
    {
        public StockMarketDbContext(DbContextOptions<StockMarketDbContext> options) : base(options)
        {
        }
        public DbSet<BuyOrder> BuyOrders { get; set; }
        public DbSet<SellOrder> SellOrders { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
                base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<BuyOrder>().ToTable("BuyOrders");
            modelBuilder.Entity<SellOrder>().ToTable("SellOrders");
            modelBuilder.Entity<BuyOrder>().HasData(
    new BuyOrder
    {
        BuyOrderID = new Guid("11111111-1111-1111-1111-111111111111"),
        StockSymbol = "AAPL",
        StockName = "Apple Inc.",
        DateAndTimeOfOrder = new DateTime(2024, 1, 1, 10, 0, 0),
        Quantity = 100,
        Price = 150.50
    },
    new BuyOrder
    {
        BuyOrderID = new Guid("11111111-1111-1111-1111-111111111112"),
        StockSymbol = "MSFT",
        StockName = "Microsoft Corporation",
        DateAndTimeOfOrder = new DateTime(2024, 1, 1, 10, 0, 0),
        Quantity = 500,
        Price = 300.25
    },
    new BuyOrder
    {
        BuyOrderID = new Guid("11111111-1111-1111-1111-111111111113"),
        StockSymbol = "GOOGL",
        StockName = "Alphabet Inc.",
        DateAndTimeOfOrder = new DateTime(2024, 1, 1, 10, 0, 0),
        Quantity = 1000,
        Price = 175.75
    },
    new BuyOrder
    {
        BuyOrderID = new Guid("11111111-1111-1111-1111-111111111114"),
        StockSymbol = "AMZN",
        StockName = "Amazon.com Inc.",
        DateAndTimeOfOrder = new DateTime(2024, 1, 1, 10, 0, 0),
        Quantity = 2500,
        Price = 180.00
    },
    new BuyOrder
    {
        BuyOrderID = new Guid("11111111-1111-1111-1111-111111111115"),
        StockSymbol = "TSLA",
        StockName = "Tesla Inc.",
        DateAndTimeOfOrder = new DateTime(2024, 1, 1, 10, 0, 0),
        Quantity = 10000,
        Price = 250.50
    });

            modelBuilder.Entity<SellOrder>().HasData(
                new SellOrder
                {
                    SellOrderID = new Guid("22222222-2222-2222-2222-222222222221"),
                    StockSymbol = "AAPL",
                    StockName = "Apple Inc.",
                    DateAndTimeOfOrder = new DateTime(2024, 1, 1, 10, 0, 0),
                    Quantity = 100,
                    Price = 150.50
                },
                new SellOrder
                {
                    SellOrderID = new Guid("22222222-2222-2222-2222-222222222222"),
                    StockSymbol = "MSFT",
                    StockName = "Microsoft Corporation",
                    DateAndTimeOfOrder = new DateTime(2024, 1, 1, 10, 0, 0),
                    Quantity = 500,
                    Price = 300.25
                },
                new SellOrder
                {
                    SellOrderID = new Guid("22222222-2222-2222-2222-222222222223"),
                    StockSymbol = "GOOGL",
                    StockName = "Alphabet Inc.",
                    DateAndTimeOfOrder = new DateTime(2024, 1, 1, 10, 0, 0),
                    Quantity = 1000,
                    Price = 175.75
                },
                new SellOrder
                {
                    SellOrderID = new Guid("22222222-2222-2222-2222-222222222224"),
                    StockSymbol = "AMZN",
                    StockName = "Amazon.com Inc.",
                    DateAndTimeOfOrder = new DateTime(2024, 1, 1, 10, 0, 0),
                    Quantity = 2500,
                    Price = 180.00
                },
                new SellOrder
                {
                    SellOrderID = new Guid("22222222-2222-2222-2222-222222222225"),
                    StockSymbol = "TSLA",
                    StockName = "Tesla Inc.",
                    DateAndTimeOfOrder = new DateTime(2024, 1, 1, 10, 0, 0),
                    Quantity = 10000,
                    Price = 250.50
                });

        }
    }
}
