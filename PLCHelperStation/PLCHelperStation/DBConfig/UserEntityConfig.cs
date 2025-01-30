using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PLCHelperStation.Modbel;

namespace PLCHelperStation.DBConfig
{
    public class UserEntityConfig : DbContext,IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("T_User");
        }
    }
}
