using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Abstractions;
using WebAppWithIdentity.Models;
using WebAppWithIdentity.ViewModels;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WebApp8WithIdentity.Controllers
{
    [EnableCors(PolicyName = "enablecorsfromreact")]
    public class AccountController : Controller
    {
        private readonly SignInManager<AppUser> signInManager;
        private readonly UserManager<AppUser> userManager;

        public AccountController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginVM model)
        {
            

            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(model.Username, model.Password, model.RememberMe, false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
                return View(model);
            }
            ModelState.AddModelError(string.Empty, "Invalid Login Attempt - ModelState is not valid");
            return View(model);
        }

        [HttpPost]
        public async Task<bool> LoginFromReact([FromForm] LoginVM model)
        {
            //Login from React
            var result = await signInManager.PasswordSignInAsync(model.Username, model.Password, model.RememberMe, false);
            return result.Succeeded;
        }


        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM model)
        {
            if (ModelState.IsValid)
            {
                AppUser user = new()
                {
                    Name = model.Name,
                    UserName = model.Email,
                    Email = model.Email,
                    Address = model.Address,
                };
                IdentityResult result = await userManager.CreateAsync(user,model.Password!);

                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(user, isPersistent: false);

                    return RedirectToAction("Index", "Home");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            ModelState.AddModelError(string.Empty, "ModelState is not valid");
            return View(model);
        }
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<bool> LogoutFromReact()
        {
            //Logout from react
            try
            {
                await signInManager.SignOutAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
