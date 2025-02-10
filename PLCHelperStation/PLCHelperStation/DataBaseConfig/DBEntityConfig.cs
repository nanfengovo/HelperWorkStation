using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PLCHelperStation.Modbel;

namespace PLCHelperStation.DBConfig
{
        public class DBEntityConfig : DbContext, IEntityTypeConfiguration<DBPointConfig> 
        {
            public void Configure(EntityTypeBuilder<DBPointConfig> builder)
            {
                builder.ToTable("T_DBPointConfig");
            }
        }
}
