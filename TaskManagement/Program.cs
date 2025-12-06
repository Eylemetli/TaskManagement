using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TaskManagement.Services;
using TaskManagement.Data;
using Microsoft.EntityFrameworkCore;
using FluentValidation.AspNetCore;

namespace TaskManagement
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Controllers + FluentValidation
            builder.Services
                .AddControllers()
                .AddFluentValidation(config =>
                {
                    // Bu assembly içindeki tüm validatorlar? kaydeder
                    config.RegisterValidatorsFromAssemblyContaining<Program>();
                });

            // Services & DB
            builder.Services.AddScoped<ITaskService, TaskService>();
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Swagger
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Swagger UI
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "TaskManagement v1");
                c.RoutePrefix = "swagger";
            });

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();

        }
    }
}

