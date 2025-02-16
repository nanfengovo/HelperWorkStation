using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PLCHelperStation.DataBaseConfig;
using PLCHelperStation.Modbel;

namespace PLCHelperStation.DB
{
    public class TestDbContext : DbContext
    {

        public DbSet<PLCConfig> PLCs { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<ModbusConfig> ModbusConfigs { get; set; }

        public DbSet<Log> logs { get; set; }

        public DbSet<S7Config> S7Configs { get; set; }

        public DbSet<DBPointConfig> DBConfigs { get; set; }

        public DbSet<S7DBRWRecord> S7DBRWRecords { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string conStr = "Server = . ;Database = PLCHelperDB ;User=sa ;Password=aaaa2624434145 ;Encrypt=True;Trusted_Connection=True;TrustServerCertificate=True";
            optionsBuilder.UseSqlServer(conStr);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
        }
    }
}
