using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodoCSharp.Models;
using TodoCSharp.UserAccountDao;

namespace TodoCSharp.UserAccountPresentationService
{
    public class UserAccountPresentationService : IUserAccountPresentationService
    {
        private readonly IUserAccountDao userAccountDao;
        public UserAccountPresentationService(IUserAccountDao userAccountDao)
        {
            this.userAccountDao = userAccountDao;
        }

        public IEnumerable<ApplicationUser> Users =>
            userAccountDao.Users;

        public async Task<Boolean> IsInRole(ApplicationUser user, String name) =>
            await userAccountDao.IsInRoleAsync(user, name);

        public async Task<ApplicationUser> FindById(String id) =>
            await userAccountDao.FindByIdAsync(id);

        public async Task<IdentityResult> Delete(ApplicationUser user) =>
            await userAccountDao.DeleteAsync(user);

        public async Task<IdentityResult> Update(ApplicationUser user) =>
            await userAccountDao.UpdateAsync(user);

        public async Task<IdentityResult> ValidatePassword(ApplicationUser user, String password) =>
            await userAccountDao.ValidatePasswordAsync(user, password);

        public async Task<IdentityResult> ValidateUser(ApplicationUser user) =>
            await userAccountDao.ValidateUserAsync(user);

        public String HashPassword(ApplicationUser user, String password) =>
            userAccountDao.HashPasswordAsync(user, password);

        public async Task<IdentityResult> Create(ApplicationUser user, String password) =>
            await userAccountDao.CreateAsync(user, password);

        public async Task SignOut() =>
            await userAccountDao.SignOutAsync();

        public async Task SignIn(ApplicationUser user, Boolean isPersistent) =>
            await userAccountDao.SignInAsync(user, isPersistent);

        public async Task<SignInResult> PasswordSignIn(String userName, String password, Boolean isPresistent, Boolean lockoutOnFailure) =>
            await userAccountDao.PasswordSignInAsync(userName, password, isPresistent, lockoutOnFailure);
    }
}
