using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Notebook.Data;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Identity;
using Notebook.Data.Interfaces;
using AuthAppLib.Model;
namespace Notebook
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<NotebookContext.NotebookContext>(options =>
                options.UseSqlServer(
                    builder.Configuration.GetConnectionString("NotebookContext") ?? throw new InvalidOperationException("Connection string 'NotebookContext' not found.")));

            builder.Services.AddTransient<IContactData, ContactDB>();
            builder.Services.AddMvc();

            builder.Services.AddIdentity<User, IdentityRole>()
                    .AddEntityFrameworkStores<NotebookContext.NotebookContext>()
                    .AddDefaultTokenProviders();


            builder.Services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequiredLength = 8;
                options.Lockout.MaxFailedAccessAttempts = 3;
                options.Lockout.AllowedForNewUsers = true;
            });

            builder.Services.AddRazorPages();
            builder.Services.AddControllersWithViews();


            var app = builder.Build();
            
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(name: "default", pattern: "{controller=Contact}/{action=Index}");

            app.Run();
        }
    }
}
