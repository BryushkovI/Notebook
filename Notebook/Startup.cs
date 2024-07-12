using Microsoft.AspNetCore.Builder;

namespace Notebook
{
    public class Startup
    {
        public void Configure(IServiceCollection services)
        {
            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.HttpOnly = true;
                options.Cookie.Expiration = TimeSpan.FromMinutes(30);
                options.LoginPath = "/Account/Login";
                options.LogoutPath = "/Account/Logout";
                options.SlidingExpiration = true;
            });
        }
    }
}
