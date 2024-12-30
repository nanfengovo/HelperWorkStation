
using Microsoft.EntityFrameworkCore;


using Infrastructure;
using Infrastructure.Model;
using System.Buffers.Text;



namespace PLCHeler
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //跨域配置①
            var MyCORS = "_mycors";
            var builder = WebApplication.CreateBuilder(args);

            // 添加 BaseDb 到依赖注入容器
            builder.Services.AddDbContext<TestDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Add services to the container.

            //跨域配置②
            builder.Services.AddCors(x =>
            {
                x.AddPolicy(name:MyCORS,
                    policy=>
                    {
                        policy.WithOrigins("http://localhost:8099/#/");
                    }
                    );
            });

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

          

            var app = builder.Build();


            // 确保在应用启动时应用迁移并初始化种子数据
            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<TestDbContext>();
                dbContext.Database.Migrate();
                // 初始化数据库或种子数据
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();


            //跨域配置③
            app.UseCors(MyCORS);

            //授权中间件
            app.UseAuthorization();

            app.MapControllers();

            app.Run();

        
        }

      
    }
}
