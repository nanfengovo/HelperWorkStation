using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PLCHelperStation.Modbel;

namespace PLCHelperStation.DBConfig
{
    public class PLCEntityConfig :DbContext ,IEntityTypeConfiguration<PLCConfig>
    {
        public void Configure(EntityTypeBuilder<PLCConfig> builder)
        {
            builder.ToTable("T_PLCConfig");
        }

       
    }
}
