using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TodoCSharp.Models;
using TodoCSharp.TodoPresentationService;

namespace TodoCSharp.Controllers
{
    public class AdminController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ITodoPresentationService todoPresintationService;
        public AdminController(UserManager<ApplicationUser> userManager, ITodoPresentationService todoPresintationService)
        {
            this.userManager = userManager;
            this.todoPresintationService = todoPresintationService;
        }

        public IActionResult Index()
        {
            return View(userManager.Users);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(String id)
        {
            ApplicationUser user = await userManager.FindByIdAsync(id);
            if (user != null)
            {
                // Remove all todos user
                todoPresintationService.RemoveAll(id).Wait();

                IdentityResult deleteUser = await userManager.DeleteAsync(user);
                
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

            return View("Index", userManager.Users);
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