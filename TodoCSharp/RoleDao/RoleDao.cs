using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoCSharp.Models;

namespace TodoCSharp.RoleDao
{
    public class RoleDao : IRoleDao
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;
        public RoleDao(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
        }

        public IQueryable<IdentityRole> Roles =>
            roleManager.Roles;

        public async Task<IdentityResult> CreateAsync(IdentityRole role) =>
            await roleManager.CreateAsync(role);

        public async Task<IdentityResult> DeleteAsync(IdentityRole role) =>
            await roleManager.DeleteAsync(role);

        public async Task<IdentityResult> UpdateAsync(IdentityRole role) =>
            await roleManager.UpdateAsync(role);

        public async Task<IdentityRole> FindByIdAsync(String id) =>
            await roleManager.FindByIdAsync(id);
    }
}
