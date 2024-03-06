using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebAppWithIdentity.Models;
using WebAppWithIdentity.ViewModels;

namespace WebAppWithIdentity.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<AppUser> signInManager;

        public AccountController(SignInManager<AppUser> signInManager)
        {
            this.signInManager = signInManager;
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
        public IActionResult Register()
        {
            return View();
        }
        public IActionResult Logout()
        {
            return View();
        }
    }
}
