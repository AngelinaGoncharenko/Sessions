using Microsoft.EntityFrameworkCore;
using Security.Models;
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
        public DbSet<Gender> Genders { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserType> UserTypes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            var connectionString = "Server=192.168.121.80;User ID=user106;Password=N7OOtA;Database=user116;Encrypt=False";
            optionsBuilder.UseSqlServer(connectionString);
        }

        public static MyDbContext GetContext()
        {
            _context ??= new MyDbContext();
            return _context;
        }
    }
}
