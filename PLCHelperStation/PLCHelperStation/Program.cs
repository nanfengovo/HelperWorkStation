
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


            //�������ݿ��ַ�������
            builder.Services.AddDbContext<AppIdentityDbContext>(
            options => options.UseSqlServer(builder.Configuration["ConnectionStrings:DefaultConnection"])
            );

            //���ASP.NET Core Identity����
            builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
            {
                // �Զ����������
                options.Password.RequireDigit = false; // Ҫ���������
                options.Password.RequiredLength = 1; // ��С����
                options.Password.RequireNonAlphanumeric = false; // Ҫ���������ĸ�����ַ�
                options.Password.RequireUppercase = false; // Ҫ�������д��ĸ
                options.Password.RequireLowercase = false; // Ҫ�����Сд��ĸ
            }).
            AddEntityFrameworkStores<AppIdentityDbContext>().
            AddDefaultTokenProviders();

            var app = builder.Build();

            // ���� HTTP ����ܵ�
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
