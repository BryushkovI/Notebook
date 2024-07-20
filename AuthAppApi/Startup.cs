using Microsoft.AspNetCore.Builder;
using NotebookContext;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.EntityFrameworkCore;
using AuthAppLib.Model;
using Microsoft.AspNetCore.Identity;

namespace AuthAppApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public void Configure(IApplicationBuilder app)
        {
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();

        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddMvc();
        }
    }
}
