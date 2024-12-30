using Infrastructure.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DBConfig
{
    internal class UserEntityConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            //表名 —— T_User
            builder.ToTable("T_User");

            //用户名是必填的最大长度是10位
            builder.Property(x=>x.UserName).HasMaxLength(10).IsRequired();

            //密码是必填的最大长度是20位
            builder.Property(x => x.Password).HasMaxLength(20).IsRequired();


        }
    }
}
