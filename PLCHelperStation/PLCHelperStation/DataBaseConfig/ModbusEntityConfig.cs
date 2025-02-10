using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PLCHelperStation.Modbel;

namespace PLCHelperStation.DBConfig
{
    public class ModbusEntityConfig :DbContext ,IEntityTypeConfiguration<ModbusConfig>
    {
        public void Configure(EntityTypeBuilder<ModbusConfig> builder)
        {
            builder.ToTable("T_ModbusConfig");
        }

       
    }
}
