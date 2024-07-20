using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Notebook.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {

        private readonly RoleManager<IdentityRole> _roleManager;


        public RoleController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        /// <summary>
        /// Все роли в системе
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IEnumerable<IdentityRole>> Get()
        {
            return await _roleManager.Roles.ToListAsync();
        }
        //[HttpGet]
        //public IActionResult Create() => View();
        /// <summary>
        /// Создание новой роли в системе
        /// </summary>
        /// <param name="roleName"></param>
        /// <returns></returns>
        [HttpPost]
        public async void Create(string roleName)
        {
            if (!string.IsNullOrEmpty(roleName))
            {
                IdentityResult result = await _roleManager.CreateAsync(new IdentityRole(roleName));

                if (result.Succeeded)
                {
                    
                }
                else
                {
                    foreach (var identityError in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, identityError.Description);
                    }
                }
            }
            
        }

        

    }
}
