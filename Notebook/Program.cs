using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Notebook.Data;
using Microsoft.AspNetCore.Mvc.TagHelpers;
namespace Notebook
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<NotebookContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("NotebookContext") ?? throw new InvalidOperationException("Connection string 'NotebookContext' not found.")));

            
            builder.Services.AddMvc();
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
