using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Notebook.AuthApp;
using Notebook.Model;

namespace Notebook.Controllers
{
    public class AccountController : Controller
    {

        private readonly UserManager<User> _userManger;
        private readonly SignInManager<User> _signInManager;


        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManger = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            return View(new UserLogin()
            {
                ReturnUrl = returnUrl
            });

        }


        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(UserLogin model)
        {
            if (ModelState.IsValid)
            {
                var loginResult = await _signInManager.PasswordSignInAsync(model.UserName,
                                                                           model.Password,
                                                                           false,
                                                                           false);
                if (loginResult.Succeeded)
                {
                    if (Url.IsLocalUrl(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    }
                    return RedirectToAction("Index", "Contact");
                }
            }

            ModelState.AddModelError("", "Пользователь не найден");
            return View(model);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View(new UserRegistration());
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(UserRegistration model)
        {
            if (ModelState.IsValid)
            {
                var user = new User { UserName = model.UserName };
                var createResult = await _userManger.CreateAsync(user, model.Password);

                if (createResult.Succeeded)
                {
                    await _signInManager.SignInAsync(user, false);
                    return RedirectToAction("Index", "Contact");
                }
                else
                {
                    foreach (var identityError in createResult.Errors)
                    {
                        ModelState.AddModelError("", identityError.Description);
                    }
                }
            }
            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Contact");
        }

        [HttpGet]
        public async Task<IActionResult> CurrentUser(string userName)
        {
            if (User.Identity.Name == userName)
            {
                var user = await _userManger.FindByNameAsync(userName);
                if (user == null)
                {
                    return NotFound();
                }
                user.Roles = await _userManger.GetRolesAsync(user);
                return View(user);
            }
            return Forbid();
        }
    }
}
