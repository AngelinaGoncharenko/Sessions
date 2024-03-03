using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeeperProCommonDivision.Database
{
    public class MyDbContext : DbContext
    {
        private static MyDbContext? _context;

        public DbSet<BannedUser> BannedUsers { get; set; }
        public DbSet<Bid> Bids { get; set; }
        public DbSet<BidStatus> BidStatuses { get; set; }
        public DbSet<BidType> BidTypes { get; set; }
        public DbSet<BidVisitTarget> BidVisitTargets { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Visitor> Visitors { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            var connectionString = "Server=192.168.121.80;User ID=user106;Password=N7OOtA;Database=user106;Encrypt=False";
            optionsBuilder.UseSqlServer(connectionString);
        }

        public static MyDbContext GetContext()
        {
            _context ??= new MyDbContext();
            return _context;
        }
    }
}
