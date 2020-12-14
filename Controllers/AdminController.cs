﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SL_StockTrade.Models;
using SL_StockTrade.ViewModel;

namespace SL_StockTrade.Controllers
{
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

                if(result.Succeeded)
                {
                    return RedirectToAction("ListRoles", "admin");
                }

                foreach(IdentityError err in result.Errors )
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

            if(role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {Id} can not be found";
                return View("NotFound");
            }

            var model = new EditRoleViewModel
            {
                Id = role.Id,
                RoleName = role.Name
            };

            foreach(var user in usermanager.Users)
            {
                if(await usermanager.IsInRoleAsync(user, role.Name))
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

                if(res.Succeeded)
                {
                    return RedirectToAction("ListRoles");
                }
            }

            return View(model);

        }
    }
}
