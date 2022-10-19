using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WotPersonalDataCollectorWebApp.CosmosDb.Context;
using WotPersonalDataCollectorWebApp.Utilities;

namespace WotPersonalDataCollectorWebApp
{
    internal static class WotPersonalDataCollectorWebApp
    {
        public static async Task Main(string[] args)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
            var config = new AspConfiguration(); 
            // builder.Services.AddDbContext<ApplicationDbContext>(options =>
            //     options.UseSqlite(connectionString));
            // builder.Services.AddDbContext<CosmosDatabaseContext>(s => new CosmosDatabaseContext()
            //    );
            builder.Services.AddDbContext<ICosmosDatabaseContext ,CosmosDatabaseContext>(b =>
                b.UseCosmos(config.CosmosConnectionString, config.DatabaseName));
            //// builder.Services.AddScoped<ICosmosDatabaseContext>();
            
            builder.Services.BuildServiceProvider().GetService<CosmosDatabaseContext>();
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<CosmosDatabaseContext>();
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapRazorPages();

            await app.RunAsync();
        }
    }
}
