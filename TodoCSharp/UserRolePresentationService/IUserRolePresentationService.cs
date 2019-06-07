using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoCSharp.Models;

namespace TodoCSharp.UserRolePresentationService
{
    public interface IUserRolePresentationService
    {
        Task AddToRole(RoleModificationModel model, Action<IdentityResult> result);
        Task RemoveFromRole(RoleModificationModel model, Action<IdentityResult> result);
    }
}
