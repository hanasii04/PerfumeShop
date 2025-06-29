using Microsoft.EntityFrameworkCore;
using PerfumeShop.Models;

namespace PerfumeShop
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Thêm dịch vụ Razor View + DbContext
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<PerfumeShopContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
                       .UseLazyLoadingProxies()
            );

            // ➤ Cấu hình session
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30); // Thời gian sống session
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            var app = builder.Build();

            // Middleware
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseRouting();

            // ➤ Kích hoạt session
            app.UseSession();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=NguoiDung}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
