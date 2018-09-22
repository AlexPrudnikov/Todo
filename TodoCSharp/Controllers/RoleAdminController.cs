using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TodoCSharp.Models;

namespace TodoCSharp.Controllers
{
    [Authorize(Roles = "admin")]
    public class RoleAdminController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;
        public RoleAdminController(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
        }

        [HttpGet]
        public IActionResult Index() => View(roleManager.Roles);

        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create([Required]String name)
        {
            if (ModelState.IsValid)
            {
                IdentityResult result = await roleManager.CreateAsync(new IdentityRole(name));
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    AddErrorsFromResult(result);
                }
            }

            return View(name);
        }

        public async Task<IActionResult> Delete(String id)
        {
            IdentityRole role = await roleManager.FindByIdAsync(id);
            if (role != null)
            {
                IdentityResult result = await roleManager.DeleteAsync(role);
                if (result.Succeeded)
                {
                    RedirectToAction("Index");
                }
                else
                {
                    AddErrorsFromResult(result);
                }
            }
            else
            {
                ModelState.AddModelError("", "No role found");
            }

            return View("Index"); // Проверить в учебнике было так return View("Index", roleManager.Roles);
        }

   
        // Эти методы вынести в отдельный кнтроллер!

        [HttpGet]
        public async Task<IActionResult> Edit(String id)
        {
            IdentityRole role = await roleManager.FindByIdAsync(id);
            List<ApplicationUser> members = new List<ApplicationUser>();
            List<ApplicationUser> nonMembers = new List<ApplicationUser>();

            foreach (var user in userManager.Users)
            {
                var list = await userManager.IsInRoleAsync(user, role.Name)
                    ? members
                    : nonMembers;

                list.Add(user);
            }

            return View(new RoleEditModel()
            {
                Role = role,
                Members = members,
                NonMembers = nonMembers
            });
        }

        [HttpPost]
        public async Task<IActionResult> Edit(RoleModificationModel model)
        {
            if (ModelState.IsValid)
            {
                await AddToRoleAsync(model);
                await RemoveFromRoleAsync(model);
            }

            if (ModelState.IsValid)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return await Edit(model.RoleId);
            }
        }

        private async Task AddToRoleAsync(RoleModificationModel model)
        {
            IdentityResult result;
            foreach (var userId in model.IdsToAdd ?? new String[] { })
            {
                ApplicationUser user = await userManager.FindByIdAsync(userId);
                if (user != null)
                {
                    result = await userManager.AddToRoleAsync(user, model.RoleName);
                    if (!result.Succeeded)
                    {
                        AddErrorsFromResult(result);
                    }
                }
            }
        }

        private async Task RemoveFromRoleAsync(RoleModificationModel model)
        {
            IdentityResult result;
            foreach (var userId in model.IdsToDelete ?? new String[] { })
            {
                ApplicationUser user = await userManager.FindByIdAsync(userId);
                if(user != null)
                {
                    result = await userManager.RemoveFromRoleAsync(user, model.RoleName);
                    if (!result.Succeeded)
                    {
                        AddErrorsFromResult(result);
                    }
                }
            }
        }

        private void AddErrorsFromResult(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
        }
    }
}