using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoCSharp.Models;

namespace TodoCSharp.UserRoleDao
{
    public interface IUserRoleDao
    {
        Task<IdentityResult> AddToRoleAsync(ApplicationUser user, String name);
        Task<IdentityResult> RemoveFromRoleAsync(ApplicationUser user, String name);
        Task<ApplicationUser> FindByIdAsync(String id);
    }
}
