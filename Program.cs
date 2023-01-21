using Microsoft.EntityFrameworkCore;
using NToastNotify;
using WebApplication1.Interfaces;
using WebApplication1.Models;
using WebApplication1.Repositories;

namespace WebApplication1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            //register The DBContext
            var connectionString= builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<ApplicationDBContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });
            //rigister the toaster
            builder.Services.AddMvc().AddNToastNotifyToastr(new ToastrOptions
            {
                ProgressBar = true,
                PositionClass= ToastPositions.TopRight,
                PreventDuplicates= true,    
                CloseButton= true
            });
            //register the generic repository
            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Products}/{action=Index}/{id?}");

            app.Run();
        }
    }
}