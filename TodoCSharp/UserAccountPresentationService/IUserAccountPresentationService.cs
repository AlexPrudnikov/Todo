using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodoCSharp.Models;

namespace TodoCSharp.UserAccountPresentationService
{
    public interface IUserAccountPresentationService
    {
        IEnumerable<ApplicationUser> Users { get; }
        Task<Boolean> IsInRole(ApplicationUser user, String name);
        Task<ApplicationUser> FindById(String id);
        Task<IdentityResult> Delete(ApplicationUser user);
        Task<IdentityResult> Update(ApplicationUser user);
        Task<IdentityResult> ValidatePassword(ApplicationUser user, String password);
        Task<IdentityResult> ValidateUser(ApplicationUser user);
        String HashPassword(ApplicationUser user, String password);
        Task<IdentityResult> Create(ApplicationUser user, String password);
        Task SignOut();
        Task SignIn(ApplicationUser user, Boolean isPersistent);
        Task<SignInResult> PasswordSignIn(String userName, String password, Boolean isPresistent, Boolean lockoutOnFailure);
    }
}
