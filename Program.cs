using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebAppWithIdentity.Data;
using WebAppWithIdentity.Models;

namespace WebAppWithIdentity
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var ReactSpecificOrigins = "enablecorsfromreact";

            var builder = WebApplication.CreateBuilder(args);

            // Add CORS
            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: ReactSpecificOrigins,
                                       builder =>
                                       {
                                           builder.WithOrigins("http://localhost:3000")
                                   .AllowAnyHeader()
                                   .AllowAnyMethod();
                                       });
            });



            var connectionString = builder.Configuration.GetConnectionString("WebAppDevConnection");
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));

            builder.Services.AddIdentity<AppUser, IdentityRole>(
                options =>
                {
                    //options.SignIn.RequireConfirmedAccount = true;
                    options.Password.RequiredUniqueChars = 0;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequiredLength = 0;

                })
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

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

            // InvalidOperationException: Endpoint WebApp8WithIdentity.Controllers.AccountController.Login(WebApp8WithIdentity)
            // contains CORS metadata, but a middleware was not found that supports CORS.
            // Configure your application startup by adding app.UseCors() in the application startup code.
            // If there are calls to app.UseRouting() and app.UseEndpoints(...), the call to app.UseCors() must go between them.

            app.UseRouting();

            // CORS
            app.UseCors(ReactSpecificOrigins);



            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
