using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using AuthAppLib.Model;

namespace Notebook.Controllers
{
    public class AssigningRoleController : Controller
    {
        private readonly UserManager<User> _userManger;
        private readonly RoleManager<IdentityRole> _roleManger;

        public AssigningRoleController(UserManager<User> userManger, RoleManager<IdentityRole> roleManger)
        {
            _userManger = userManger;
            _roleManger = roleManger;
        }

        [HttpGet]
        public async Task<IActionResult> Change(string userName)
        {
            if (!string.IsNullOrEmpty(userName) && User.Identity.Name == userName)
            {
                User? user = await _userManger.FindByNameAsync(userName);
                IList<string> userRoles = await _userManger.GetRolesAsync(user);
                IList<IdentityRole> allRoles = [.. _roleManger.Roles];
                SelectListItem[] items = new SelectListItem[allRoles.Count];
                for (int i = 0; i < allRoles.Count; i++)
                {
                    items[i] = new SelectListItem()
                    {
                        Selected = userRoles.Contains(allRoles.ElementAtOrDefault(i).Name),
                        Value = allRoles.ElementAtOrDefault(i).Name
                    };
                }
                return View(items);
            }
            return Unauthorized();
        }

        [HttpPost]
        public async Task<IActionResult> Change(SelectListItem[]  model)
        {
            if (ModelState.IsValid)
            {
                User? user = await _userManger.FindByNameAsync(User.Identity.Name);
                
                for (int i = 0; i < model.Length; i++)
                {
                    if (model[i].Selected)
                    {
                        if (!await _userManger.IsInRoleAsync(user, model[i].Value))
                        {
                            await _userManger.AddToRoleAsync(user, model[i].Value);
                        }
                    }
                    else
                    {
                        if (await _userManger.IsInRoleAsync(user, model[i].Value))
                        {
                            await _userManger.RemoveFromRoleAsync(user, model[i].Value);
                        }
                    }
                }
                await _userManger.UpdateAsync(user);
            }
            return RedirectToAction("Index", "Contact");

        }


    }
}
 