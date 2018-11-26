using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TodoCSharp.Models;
using TodoCSharp.UserAccountPresentationService;
using TodoCSharp.ViewModels;

namespace TodoCSharp.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserAccountPresentationService userAccountPresentationService;
        public AccountController(IUserAccountPresentationService userAccountPresentationService)
        {
            this.userAccountPresentationService = userAccountPresentationService;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser() { UserName = model.UserName, Age = model.Age, Email = model.Email };

                // Добавление пользователя
                IdentityResult result = await userAccountPresentationService.Create(user, model.Password);
                if (result.Succeeded)
                {
                    // Устанавливаем куки
                    await userAccountPresentationService.SignIn(user, false);
                    return RedirectToAction("Create", "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(String.Empty, error.Description);
                    }
                }
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(String id)
        {
            ApplicationUser user = await userAccountPresentationService.FindById(id);
            if (user != null)
            {
                return View(user);
            }
            else
            {
                return RedirectToAction("Home");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(String id, String email, String password)
        {
            ApplicationUser user = await userAccountPresentationService.FindById(id);
            if (user != null)
            {
                user.Email = email;
                IdentityResult validEmail = await userAccountPresentationService.ValidateUser(user);
                if (!validEmail.Succeeded)
                {
                    AddErrorsFromResult(validEmail);
                }

                IdentityResult validPass = null;
                if (!String.IsNullOrEmpty(password))
                {
                    validPass = await userAccountPresentationService.ValidatePassword(user, password);
                    if (validPass.Succeeded)
                    {
                        // Получаем hash пароля
                        user.PasswordHash = userAccountPresentationService.HashPassword(user, password);
                    }
                    else
                    {
                        AddErrorsFromResult(validPass);
                    }
                }

                if ((validEmail.Succeeded && validPass == null) || 
                    (validEmail.Succeeded && password != String.Empty && validPass.Succeeded))
                {
                    IdentityResult result = await userAccountPresentationService.Update(user);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        AddErrorsFromResult(result);
                    }
                }
                else
                {
                    ModelState.AddModelError("", "User Not Found");
                }
            }

            return View(user);
        }

        [HttpGet]
        public IActionResult Login(String returnUrl = null)
        {
            return View(new LoginViewModel() { ReturnUrl = returnUrl });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await userAccountPresentationService.PasswordSignIn(model.UserName, model.Password, model.RememberMe, false);
                if (result.Succeeded)
                {
                    if (!String.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Create", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError(String.Empty, "Неправильный логин и (или) пароль");
                }
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogOff()
        {
            // Удаляем аутентификационные куки
            await userAccountPresentationService.SignOut();
            return RedirectToAction("Create", "Home");
        }

        private void AddErrorsFromResult(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
        }
    }
}

// https://metanit.com/sharp/aspnet5/16.4.php