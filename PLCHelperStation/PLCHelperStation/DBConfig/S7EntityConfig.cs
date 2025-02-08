using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PLCHelperStation.Modbel;

namespace PLCHelperStation.DBConfig
{
    public class S7EntityConfig :DbContext ,IEntityTypeConfiguration<S7Config>
    {
        public void Configure(EntityTypeBuilder<S7Config> builder)
        {
            builder.ToTable("T_S7Config");
        }

       
    }
}
