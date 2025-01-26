
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using PLCHelperStation.DBConfig;
using PLCHelperStation.Modbel;

namespace PLCHelperStation
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "PLCHelperStation API", Version = "v1" });
            });


            //创建数据库字符串链接
            builder.Services.AddDbContext<AppIdentityDbContext>(
            options => options.UseSqlServer(builder.Configuration["ConnectionStrings:DefaultConnection"])
            );

            //添加ASP.NET Core Identity服务
            builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
            {
                // 自定义密码策略
                options.Password.RequireDigit = false; // 要求包含数字
                options.Password.RequiredLength = 1; // 最小长度
                options.Password.RequireNonAlphanumeric = false; // 要求包含非字母数字字符
                options.Password.RequireUppercase = false; // 要求包含大写字母
                options.Password.RequireLowercase = false; // 要求包含小写字母
            }).
            AddEntityFrameworkStores<AppIdentityDbContext>().
            AddDefaultTokenProviders();

            var app = builder.Build();

            // 配置 HTTP 请求管道
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "PLCHelperStation API v1");
                });
            }

         

            app.UseHttpsRedirection();

            app.UseAuthorization();
            app.UseAuthentication();
           

            app.MapControllers();

            app.Run();
        }
    }
}
