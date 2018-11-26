using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TodoCSharp.Models;
using TodoCSharp.TodoPresentationService;
using TodoCSharp.UserAccountPresentationService;

namespace TodoCSharp.Controllers
{
    public class AdminController : Controller
    {   
        private readonly ITodoPresentationService todoPresintationService;
        private readonly IUserAccountPresentationService userAccountPresentationService;
        public AdminController(IUserAccountPresentationService userAccountPresentationService, ITodoPresentationService todoPresintationService)
        {
            this.todoPresintationService = todoPresintationService;
            this.userAccountPresentationService = userAccountPresentationService;
        }

        public IActionResult Index()
        {
            return View(userAccountPresentationService.Users);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(String id)
        {
            ApplicationUser user = await userAccountPresentationService.FindById(id);
            if (user != null)
            {
                // Remove all todos user
                todoPresintationService.RemoveAll(id).Wait();

                // Delete user
                IdentityResult deleteUser = await userAccountPresentationService.Delete(user);     

                if (deleteUser.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    AddErrorsFromResult(deleteUser);
                }
            }
            else
            {
                ModelState.AddModelError("", "User Not Found");
            }

            return View("Index", userAccountPresentationService.Users);
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