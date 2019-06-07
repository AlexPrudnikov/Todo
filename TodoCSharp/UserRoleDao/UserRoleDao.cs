using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using TodoCSharp.Models;

namespace TodoCSharp.UserRoleDao
{
    public class UserRoleDao : IUserRoleDao
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;
        public UserRoleDao(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
        }

        public async Task<IdentityResult> AddToRoleAsync(ApplicationUser user, String name) =>
            await userManager.AddToRoleAsync(user, name);

        public async Task<IdentityResult> RemoveFromRoleAsync(ApplicationUser user, String name) =>
            await userManager.RemoveFromRoleAsync(user, name);

        public async Task<ApplicationUser> FindByIdAsync(String id) =>
            await userManager.FindByIdAsync(id);
    }
}
