using BPCenter2.Components;
using BPCenter2.Data;
using BPCenter2.Services;
using BPCenter2.Services.Middleware;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using MudBlazor.Services;

namespace BPCenter2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();
            builder.Services.AddMudServices();
            //<>
            builder.Services.AddTransient<SeedDB>();
            builder.Services.AddMudServices();
            builder.Services.AddScoped<ServicesLogin>();
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.Cookie.Name = "auth_token";
                    options.LoginPath = "/AccountLogin";
                    options.Cookie.MaxAge = TimeSpan.FromMinutes(30);
                    options.AccessDeniedPath = "/access-denied";
                });
            builder.Services.AddAuthorization();
            builder.Services.AddCascadingAuthenticationState();

            //<Irepository>
            builder.Services.AddScoped(typeof(IServicesDB<>), typeof(ServicesDB<>));
            //<Irepository>

            // builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration["DB_SQL"]));
            builder.Services.AddDbContext<AppDbContext>(options =>options.UseSqlServer(builder.Configuration.GetConnectionString("DBConnection")));
            //</>


            var app = builder.Build();

            //<SeedData>
            SeedData(app);
            static void SeedData(WebApplication app)
            {
                IServiceScopeFactory? scopedFactory = app.Services.GetService<IServiceScopeFactory>();
                using IServiceScope scope = scopedFactory!.CreateScope();
                SeedDB? service = scope.ServiceProvider.GetService<SeedDB>();
                service!.SeedAsync().Wait();
            }
            //<SeedData>

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
            app.UseHttpsRedirection();

            app.UseAntiforgery();

            //<>
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseMiddleware<AuthMiddleware>();
            //<>

            app.MapStaticAssets();
            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();

            app.Run();
        }
    }
}
