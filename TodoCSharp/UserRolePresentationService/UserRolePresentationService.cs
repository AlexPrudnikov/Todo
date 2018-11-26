using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoCSharp.Models;
using TodoCSharp.UserRoleDao;

namespace TodoCSharp.UserRolePresentationService
{
    public class UserRolePresentationService : IUserRolePresentationService
    {
        private readonly IUserRoleDao userRoleDao;
        public UserRolePresentationService(IUserRoleDao userRoleDao)
        {
            this.userRoleDao = userRoleDao;
        }

        public async Task AddToRole(RoleModificationModel model, Action<IdentityResult> errors)
        {
            IdentityResult result;
            foreach (var userId in model.IdsToAdd ?? new String[] { })
            {
                ApplicationUser user = await userRoleDao.FindByIdAsync(userId);
                if (user != null)
                {
                    result = await userRoleDao.AddToRoleAsync(user, model.RoleName);
                    if (!result.Succeeded)
                    {
                        // Add errors
                        errors(result);
                    }
                }
            }
        }

        public async Task RemoveFromRole(RoleModificationModel model, Action<IdentityResult> errors)
        {
            IdentityResult result;
            foreach (var userId in model.IdsToDelete ?? new String[] { })
            {
                ApplicationUser user = await userRoleDao.FindByIdAsync(userId);
                if (user != null)
                {
                    result = await userRoleDao.RemoveFromRoleAsync(user, model.RoleName);
                    if (!result.Succeeded)
                    {
                        // Add errors
                        errors(result);
                    }
                }
            }
        }
    }
}
