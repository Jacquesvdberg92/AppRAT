using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AppRat.Data;
using AppRat.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using AppRat.ViewModels;

namespace AppRat.Controllers
{
    [Authorize]
    public class ARR_AuthController : Controller
    {
        private readonly AppRatDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public ARR_AuthController(AppRatDbContext context, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        // GET: ARR_Auth
        public async Task<IActionResult> Index()
        {
            var users = _userManager.Users.ToList();
            var userRolesViewModel = new List<ARL_UserRolesViewModel>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                userRolesViewModel.Add(new ARL_UserRolesViewModel
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                    Roles = roles,
                    RoleId = roles.FirstOrDefault() // Assuming single role for simplicity
                });
            }

            return View(userRolesViewModel);
        }

        // GET: ARR_Auth/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var roles = await _userManager.GetRolesAsync(user);

            var model = new ARL_UserRolesViewModel
            {
                UserId = user.Id,
                UserName = user.UserName,
                Roles = roles
            };

            return View(model);
        }

        // GET: ARR_Auth/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var userRoles = await _userManager.GetRolesAsync(user);
            var allRoles = _roleManager.Roles.ToList();
            var model = new EditUserRolesViewModel
            {
                UserId = user.Id,
                UserName = user.UserName,
                UserRoles = userRoles,
                AllRoles = allRoles.Select(role => new SelectListItem
                {
                    Value = role.Id,
                    Text = role.Name
                }).ToList(),
                SelectedRoleId = userRoles.Any() ? allRoles.First(r => r.Name == userRoles.First()).Id : null
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditUserRolesViewModel model)
        {
            if (model == null)
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(model.UserId);
            if (user == null)
            {
                return NotFound();
            }

            var userRoles = await _userManager.GetRolesAsync(user);
            var selectedRole = _roleManager.Roles.First(r => r.Id == model.SelectedRoleId).Name;

            var rolesToAdd = new List<string>();
            var rolesToRemove = new List<string>();

            if (!userRoles.Contains(selectedRole))
            {
                rolesToAdd.Add(selectedRole);
                rolesToRemove.AddRange(userRoles);
            }

            if (rolesToAdd.Any())
            {
                var addResult = await _userManager.AddToRolesAsync(user, rolesToAdd);
                if (!addResult.Succeeded)
                {
                    ModelState.AddModelError("", "Failed to add roles");
                    return View(model);
                }
            }

            if (rolesToRemove.Any())
            {
                var removeResult = await _userManager.RemoveFromRolesAsync(user, rolesToRemove);
                if (!removeResult.Succeeded)
                {
                    ModelState.AddModelError("", "Failed to remove roles");
                    return View(model);
                }
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: ARR_Auth/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var roles = await _userManager.GetRolesAsync(user);

            var model = new ARL_UserRolesViewModel
            {
                UserId = user.Id,
                UserName = user.UserName,
                Roles = roles
            };

            return View(model);
        }

        // POST: ARR_Auth/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var roles = await _userManager.GetRolesAsync(user);
            var result = await _userManager.RemoveFromRolesAsync(user, roles);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Failed to remove roles");
                return View();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
