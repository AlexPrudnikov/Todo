using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TodoCSharp.Models;
using TodoCSharp.RolePresentationService;

namespace TodoCSharp.Controllers
{
    [Authorize(Roles = "admin")]
    public class RoleAdminController : Controller
    {
        private readonly IRolePresentationService rolePresentationService;
        public RoleAdminController(IRolePresentationService rolePresentationService)
        {
            this.rolePresentationService = rolePresentationService;
        }

        [HttpGet]
        public IActionResult Index() => View(rolePresentationService.Roles);

        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create([Required]String name)
        {
            if (ModelState.IsValid)
            {
                IdentityResult result = await rolePresentationService.Create(name);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    AddErrorsFromResult(result);
                }
            }

            return View(name);
        }

        public async Task<IActionResult> Delete(String id)
        {
            IdentityRole role = await rolePresentationService.FindById(id);
            if (role != null)
            {
                IdentityResult result = await rolePresentationService.Delete(role);
                if (result.Succeeded)
                {
                    RedirectToAction("Index");
                }
                else
                {
                    AddErrorsFromResult(result);
                }
            }
            else
            {
                ModelState.AddModelError("", "No role found");
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(String id)
        {
            IdentityRole role = await rolePresentationService.FindById(id);
            if (role != null)
            {
                return View(role);
            }
            else
            {
                ModelState.AddModelError("", "No role found");
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(String id, String name)
        {
            IdentityRole role = await rolePresentationService.FindById(id);
            if (role != null)
            {
                IdentityResult result = await rolePresentationService.Edit(name, role);
                if (result.Succeeded)
                {
                    RedirectToAction("Index");
                }
                else
                {
                    AddErrorsFromResult(result);
                }
            }
            else
            {
                ModelState.AddModelError("", "No role found");
            }

            return RedirectToAction("Index");
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