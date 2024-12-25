
namespace PLCHeler
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //øÁ”Ú≈‰÷√¢Ÿ
            var MyCORS = "_mycors";
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            //øÁ”Ú≈‰÷√¢⁄
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

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();


            //øÁ”Ú≈‰÷√¢€
            app.UseCors(MyCORS);

            // ⁄»®÷–º‰º˛
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
