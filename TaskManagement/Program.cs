using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TaskManagement.Services;
using TaskManagement.Data;
using Microsoft.EntityFrameworkCore;


namespace TaskManagement
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //1 Controller servislerini ekle
            builder.Services.AddControllers();


            builder.Services.AddScoped<ITaskService, TaskService>();

            //Veritaban? ba?lant?s?n? aktif et


           



            builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));



            var app = builder.Build();

            //HTTP pipeline
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();


            //Controller endpointlerini map et

            app.MapControllers();

            app.Run();
        }
    }
}
