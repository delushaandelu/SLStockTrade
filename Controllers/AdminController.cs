using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SL_StockTrade.Models;
using SL_StockTrade.ViewModel;

namespace SL_StockTrade.Controllers
{
    [Authorize(Roles ="Admin")]
    public class AdminController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<SellerDeviceData> usermanager;

        public AdminController(RoleManager<IdentityRole> roleManager,
                                UserManager<SellerDeviceData> usermanager)
        {
            this.roleManager = roleManager;
            this.usermanager = usermanager;
        }

        [HttpGet]
        public IActionResult CreateRole()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole(CreateRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                IdentityRole identityRole = new IdentityRole
                {
                    Name = model.RoleName
                };
                IdentityResult result = await roleManager.CreateAsync(identityRole);

                if (result.Succeeded)
                {
                    return RedirectToAction("ListRoles", "admin");
                }

                foreach (IdentityError err in result.Errors)
                {
                    ModelState.AddModelError("", err.Description);
                }

            }

            return View();
        }

        [HttpGet]
        public IActionResult ListRoles()
        {
            var roles = roleManager.Roles;

            return View(roles);
        }

        [HttpGet]
        public async Task<IActionResult> EditRole(string Id)
        {
            var role = await roleManager.FindByIdAsync(Id);

            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {Id} can not be found";
                return View("NotFound");
            }

            var model = new EditRoleViewModel
            {
                Id = role.Id,
                RoleName = role.Name
            };

            foreach (var user in usermanager.Users)
            {
                if (await usermanager.IsInRoleAsync(user, role.Name))
                {
                    model.Users.Add(user.UserName);
                }
            }

            return View(model);

        }

        [HttpPost]
        public async Task<IActionResult> EditRole(EditRoleViewModel model)
        {
            var role = await roleManager.FindByIdAsync(model.Id);

            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {model.Id} can not be found";
                return View("NotFound");
            }
            else
            {
                role.Name = model.RoleName;
                var res = await roleManager.UpdateAsync(role);

                if (res.Succeeded)
                {
                    return RedirectToAction("ListRoles");
                }
            }

            return View(model);

        }

        [HttpGet]
        public async Task<IActionResult> EditUsersInRole(string roleId)
        {
            ViewBag.roleId = roleId;

            var role = await roleManager.FindByIdAsync(roleId);

            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {roleId} can not be found";
                return View("NotFound");
            }

            var model = new List<UserRoleViewModel>();

            foreach (var user in usermanager.Users)
            {
                var userRoleViewMode = new UserRoleViewModel
                {
                    UserID = user.Id,
                    Username = user.UserName
                };

                if (await usermanager.IsInRoleAsync(user, role.Name))
                {
                    userRoleViewMode.IsSelected = false;
                }

                model.Add(userRoleViewMode);
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditUsersInRole(List<UserRoleViewModel> model, string roleId)
        {
            var role = await roleManager.FindByIdAsync(roleId);

            if(role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id : {roleId} can not be found";
                return View("NotFound");
            }

            for (int i = 0; i< model.Count; i++)
            {
                var user = await usermanager.FindByIdAsync(model[i].UserID);
                IdentityResult res = null;

                if(model[i].IsSelected && !(await usermanager.IsInRoleAsync(user, role.Name)))
                {
                   res = await usermanager.AddToRoleAsync(user, role.Name);
                }
                else if(!model[i].IsSelected && (await usermanager.IsInRoleAsync(user, role.Name)))
                {
                    res = await usermanager.RemoveFromRoleAsync(user, role.Name);
                }
                else
                {
                    continue;
                }

                if(res.Succeeded)
                {
                    if (i < (model.Count - 1))
                        continue;
                    else
                        return RedirectToAction("EditRole", new { id = roleId });
                }
            }


            return RedirectToAction("EditRole", new { Id = roleId });
        }
    }
}
