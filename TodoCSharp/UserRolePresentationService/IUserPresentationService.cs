using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoCSharp.Models;

namespace TodoCSharp.UserRolePresentationService
{
    public interface IUserPresentationService
    {
        Task AddToRole(RoleModificationModel model);
        Task RemoveFromRole(RoleModificationModel model);
    }
}
