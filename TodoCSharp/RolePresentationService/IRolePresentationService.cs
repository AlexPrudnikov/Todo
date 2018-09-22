using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoCSharp.Models;

namespace TodoCSharp.RolePresentationService
{
    public interface IRolePresentationService
    {
        IQueryable<IdentityRole> Roles { get; }
        Task<IdentityResult> Create(String name);
        Task<IdentityResult> Delete(IdentityRole role);
        Task<IdentityResult> Edit(String name, IdentityRole role);
        Task<IdentityRole> FindById(String id);
    }
}
