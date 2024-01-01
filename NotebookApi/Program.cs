
using Microsoft.EntityFrameworkCore;
using NotebookApi.Data;

namespace NotebookApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var dbHost=Environment.GetEnvironmentVariable("DB_HOST");
            var dbName=Environment.GetEnvironmentVariable("DB_NAME");
            var dbPassword=Environment.GetEnvironmentVariable("DB_SA_PASSWORD");

            var connectionString = $"Data Source={dbHost};Initial Catalog={dbName};User ID=sa;Password={dbPassword};MultipleActiveResultSets=true;TrustServerCertificate=True;";
                //builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<NotebookDbContext>(options => options.UseSqlServer(connectionString));

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


            app.MapControllers();

            app.Run();
        }
    }
}
