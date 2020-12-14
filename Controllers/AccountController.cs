using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SL_StockTrade.ViewModel;

namespace SL_StockTrade.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;

        public AccountController(UserManager<IdentityUser> userManager,
                                    SignInManager<IdentityUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult SellerRegister()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SellerRegister(SellerRegisterViewModel model)
        {
            if(ModelState.IsValid)
            {
                var user = new IdentityUser{ UserName = model.Email, Email = model.Email };
                var restult = await userManager.CreateAsync(user, model.Password);

                if(restult.Succeeded)
                {
                    await signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("index", "Home");
                }

                foreach(var err in restult.Errors)
                {
                    ModelState.AddModelError("", err.Description);
                }
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult SellerSignIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SellerSignIn(SellerLoginViewModel model)
        {
            if (ModelState.IsValid)
            {                
                var restult = await signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);

                if (restult.Succeeded)
                {
                    return RedirectToAction("index", "Home");
                }

                ModelState.AddModelError(string.Empty, "invalid login Attempt");
            }

            return View(model);
        }
    }
}
