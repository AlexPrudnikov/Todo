using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodoCSharp.Models;

namespace TodoCSharp.UserAccountDao
{
    public class UserAccountDao : IUserAccountDao
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IUserValidator<ApplicationUser> userValidator;
        private readonly IPasswordHasher<ApplicationUser> passwordHasher;
        private readonly IPasswordValidator<ApplicationUser> passwordValidator;
        public UserAccountDao(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IUserValidator<ApplicationUser> userValidator, IPasswordHasher<ApplicationUser> passwordHasher, IPasswordValidator<ApplicationUser> passwordValidator)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.userValidator = userValidator;
            this.passwordHasher = passwordHasher;
            this.passwordValidator = passwordValidator;
        }

        public IEnumerable<ApplicationUser> Users =>
            userManager.Users;

        public async Task<Boolean> IsInRoleAsync(ApplicationUser user, String name) =>
            await userManager.IsInRoleAsync(user, name);

        public async Task<ApplicationUser> FindByIdAsync(String id) =>
            await userManager.FindByIdAsync(id);

        public async Task<IdentityResult> DeleteAsync(ApplicationUser user) =>
            await userManager.DeleteAsync(user);

        public async Task<IdentityResult> UpdateAsync(ApplicationUser user) =>
            await userManager.UpdateAsync(user);

        public async Task<IdentityResult> ValidatePasswordAsync(ApplicationUser user, String password) =>
            await passwordValidator.ValidateAsync(userManager, user, password);

        public async Task<IdentityResult> ValidateUserAsync(ApplicationUser user) =>
            await userValidator.ValidateAsync(userManager, user);

        public String HashPasswordAsync(ApplicationUser user, String password) =>
            passwordHasher.HashPassword(user, password);

        public async Task<IdentityResult> CreateAsync(ApplicationUser user, String password) =>
            await userManager.CreateAsync(user, password);

        public async Task SignOutAsync() =>
            await signInManager.SignOutAsync();

        public async Task SignInAsync(ApplicationUser user, Boolean isPersistent) =>
            await signInManager.SignInAsync(user, isPersistent);

        public async Task<SignInResult> PasswordSignInAsync(String userName, String password, Boolean isPersistent, Boolean lockoutOnFailure) =>
            await signInManager.PasswordSignInAsync(userName, password, isPersistent, lockoutOnFailure);
    }
}
