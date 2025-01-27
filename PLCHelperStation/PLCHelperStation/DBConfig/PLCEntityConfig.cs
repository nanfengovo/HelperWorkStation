using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PLCHelperStation.Modbel;

namespace PLCHelperStation.DBConfig
{
    public class PLCEntityConfig :DbContext ,IEntityTypeConfiguration<PLC>
    {
        public void Configure(EntityTypeBuilder<PLC> builder)
        {
            builder.ToTable("T_PLCConfig");
        }

       
    }
}
