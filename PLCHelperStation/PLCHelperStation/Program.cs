
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using NLog;
using NLog.Web;
using PLCHelperStation.DBConfig;
using PLCHelperStation.Modbel;

namespace PLCHelperStation
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // NLog: Setup NLog for Dependency injection
            builder.Logging.ClearProviders();
            builder.Host.UseNLog();


            // ��� CORS ����
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigins",
                    builder =>
                    {
                        builder.WithOrigins("http://localhost:8099") // �����ǰ��Ӧ�ó���� URL
                               .AllowAnyHeader()
                               .AllowAnyMethod();
                    });
            });

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

            // ��������ܵ�
            //app.Use(async (context, next) =>
            //{
            //    context.Response.ContentType = "application/json; charset=utf-8";
            //    await next();
            //});

            // ʹ�� CORS �м��
            app.UseCors("AllowSpecificOrigins");

            // ���� HTTP ����ܵ�
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "PLCHelperStation API v1");
                });
            }


            //����Https�ض���
            //app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();



            app.MapControllers();

            app.Run();
        }
    }
}
