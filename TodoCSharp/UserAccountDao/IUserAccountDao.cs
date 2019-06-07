using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodoCSharp.Models;

namespace TodoCSharp.UserAccountDao
{
    public interface IUserAccountDao
    {
        IEnumerable<ApplicationUser> Users { get; }
        Task<Boolean> IsInRoleAsync(ApplicationUser user, String name);
        Task<ApplicationUser> FindByIdAsync(String id);
        Task<IdentityResult> DeleteAsync(ApplicationUser user);
        Task<IdentityResult> UpdateAsync(ApplicationUser user);
        Task<IdentityResult> ValidatePasswordAsync(ApplicationUser user, String password);
        Task<IdentityResult> ValidateUserAsync(ApplicationUser user);
        String HashPasswordAsync(ApplicationUser user, String password);
        Task<IdentityResult> CreateAsync(ApplicationUser user, String password);
        Task SignOutAsync();
        Task SignInAsync(ApplicationUser user, Boolean isPersistent);
        Task<SignInResult> PasswordSignInAsync(String userName, String password, Boolean isPersistent, Boolean lockoutOnFailure);
    }
}
