using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TodoCSharp.Models;
using TodoCSharp.RolePresentationService;
using TodoCSharp.UserAccountPresentationService;
using TodoCSharp.UserRolePresentationService;

namespace TodoCSharp.Controllers
{
    [Authorize(Roles = "admin")]
    public class UserRoleAdminController : Controller
    {
        private readonly IRolePresentationService rolePresentationService;
        private readonly IUserRolePresentationService userRolePresentationService;
        private readonly IUserAccountPresentationService userAccountPresentationService;
        public UserRoleAdminController(IRolePresentationService rolePresentationService, IUserRolePresentationService userRolePresentationService, IUserAccountPresentationService userAccountPresentationService)
        {
            this.rolePresentationService = rolePresentationService;
            this.userRolePresentationService = userRolePresentationService;
            this.userAccountPresentationService = userAccountPresentationService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            UserRolesModel model = null;
            List<UserRolesModel> userRoles = new List<UserRolesModel>();

            foreach (ApplicationUser user in userAccountPresentationService.Users)
            {
                model = new UserRolesModel
                {
                    User = new List<ApplicationUser>(),
                    Roles = new List<IdentityRole>()
                };

                model.User.Add(user);

                foreach (IdentityRole role in rolePresentationService.Roles)
                {
                    if (await userAccountPresentationService.IsInRole(user, role.Name))
                    {
                        model.Roles.Add(role);
                    }
                }

                userRoles.Add(model);
            }

            return View(userRoles);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(String id)
        {
            IdentityRole role = await rolePresentationService.FindById(id);
            List<ApplicationUser> members = new List<ApplicationUser>();
            List<ApplicationUser> nonMembers = new List<ApplicationUser>();

            foreach (var user in userAccountPresentationService.Users)
            {
                var list = await userAccountPresentationService.IsInRole(user, role.Name)
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
                //await userRolePresentationService.AddToRole(model, AddErrorsFromResult);
                //await userRolePresentationService.RemoveFromRole(model, AddErrorsFromResult);

                //------------------------------------------------------------------------------------------
                // TODO Попробывать так!
                Task addRole = userRolePresentationService.AddToRole(model, AddErrorsFromResult);
                Task removeRole = userRolePresentationService.RemoveFromRole(model, AddErrorsFromResult);
                await Task.WhenAll(addRole, removeRole);
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

        private void AddErrorsFromResult(IdentityResult result)
        {
            foreach (IdentityError error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
        }
    }
}