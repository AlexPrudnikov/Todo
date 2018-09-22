using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoCSharp.Models;

namespace TodoCSharp.RoleDao
{
    public interface IRoleDao
    {
        IQueryable<IdentityRole> Roles { get; }
        Task<IdentityResult> CreateAsync(IdentityRole role);
        Task<IdentityResult> DeleteAsync(IdentityRole role);
        Task<IdentityResult> UpdateAsync(IdentityRole role);
        Task<IdentityRole> FindByIdAsync(String id);
    }
}
