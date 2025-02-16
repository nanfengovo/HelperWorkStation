using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PLCHelperStation.Modbel;

namespace PLCHelperStation.DataBaseConfig
{
        public class S7DBRWRecordEntity : DbContext, IEntityTypeConfiguration<S7DBRWRecord>
        {
            public void Configure(EntityTypeBuilder<S7DBRWRecord> builder)
            {
                builder.ToTable("T_S7DBRWRecords");
            }


        }
}
