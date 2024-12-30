using Infrastructure.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class TestDbContext:DbContext
    {
        public TestDbContext()
        {
        }

        public TestDbContext(DbContextOptions<TestDbContext> options)
    : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string conStr = "Server = . ;Database = PLCHelper ;User=sa ;Password=aaaa2624434145 ;Encrypt=True;Trusted_Connection=True;TrustServerCertificate=True; ";
            optionsBuilder.UseSqlServer(conStr);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasKey(u => u.Id);
            var ctx = new TestDbContext();
            var user1 = new User
            {
                UserName = "admin",
                Password = "123456"
            };
            ctx.Users.Add(user1);
            ctx.SaveChanges();
        }

    
    }
}
