using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Owin.Security.OAuth;
using SL_StockTrade.Models;
using SL_StockTrade.ViewModel;


namespace SL_StockTrade.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<SellerDeviceData> userManager;
        private readonly SignInManager<SellerDeviceData> signInManager;

        public AccountController(UserManager<SellerDeviceData> userManager,
                                    SignInManager<SellerDeviceData> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult SellerRegister()
        {
            return View();
        }


        [HttpPost]
        [AllowAnonymous]
        [Obsolete]
        public async Task<IActionResult> SellerRegister(SellerRegisterViewModel model)
        {
            if(ModelState.IsValid)
            {
                //geting current ip
                string hostName = Dns.GetHostName();
                string myIP = Dns.GetHostByName(hostName).AddressList[0].ToString();

                var user = new SellerDeviceData
                {
                    UserName = model.Email,
                    Email = model.Email,
                    deviceUsername = Environment.UserName,
                    ipaddress = myIP,
                    deviceLocation = hostName
                };
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

        [AcceptVerbs("Get", "Post")]
        [AllowAnonymous]
        public async Task<IActionResult> IsEmailInUse(string email)
        {
            var user = await userManager.FindByEmailAsync(email);

            if(user == null)
            {
                return Json(true);
            }
            else
            {
                return Json($"Email : {email} is already in use");
            }    
        }


        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult SellerSignIn()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> SellerSignIn(SellerLoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {                
                var restult = await signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);

                if (restult.Succeeded)
                {
                    if(!string.IsNullOrEmpty(returnUrl))
                    {
                        return LocalRedirect(returnUrl);
                    }
                    else 
                    { 
                        return RedirectToAction("index", "Home");
                    }
                }

                ModelState.AddModelError(string.Empty, "invalid login Attempt");
            }

            return View(model);
        }
    }
}
