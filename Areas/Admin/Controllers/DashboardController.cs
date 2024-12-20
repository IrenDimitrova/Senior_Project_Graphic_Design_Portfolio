﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Senior_Project_Graphic_Design_Portfolio.Areas.Admin.ViewModels;
using Senior_Project_Graphic_Design_Portfolio.Data;
using Senior_Project_Graphic_Design_Portfolio.Models;

namespace Senior_Project_Graphic_Design_Portfolio.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class DashboardController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;

        public DashboardController(
            UserManager<ApplicationUser> userManager,
            ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public async Task<IActionResult> Index(string searchString, string role)
        {
            var users = await _userManager.Users
                .Select(u => new UserViewModel
                {
                    Id = u.Id,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Email = u.Email,
                    Role = u.UserRole,
                    DoesPrintDesign = u.DoesPrintDesign,
                    DoesBrandingDesign = u.DoesBrandingDesign,
                    DoesDigitalDesign = u.DoesDigitalDesign,
                    Does3dDesign = u.Does3dDesign,
                    Status = "Active" // You can add a status field to ApplicationUser if needed
                })
                .ToListAsync();

            // Apply filters
            if (!string.IsNullOrEmpty(searchString))
            {
                users = users.Where(u =>
                    u.FirstName.Contains(searchString, StringComparison.OrdinalIgnoreCase) ||
                    u.LastName.Contains(searchString, StringComparison.OrdinalIgnoreCase) ||
                    u.Email.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            if (!string.IsNullOrEmpty(role) && role.ToLower() != "all")
            {
                users = users.Where(u => u.Role.ToLower() == role.ToLower()).ToList();
            }

            var viewModel = new DashboardViewModel
            {
                TotalUsers = await _userManager.Users.CountAsync(),
                TotalDesigners = await _userManager.Users.CountAsync(u => u.UserRole == "Designer"),
                TotalViewers = await _userManager.Users.CountAsync(u => u.UserRole == "Viewer"),
                Users = users
            };

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> EditUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var viewModel = new EditUserViewModel
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Role = user.UserRole,
                DoesPrintDesign = user.DoesPrintDesign,
                DoesBrandingDesign = user.DoesBrandingDesign,
                DoesDigitalDesign = user.DoesDigitalDesign,
                Does3dDesign = user.Does3dDesign
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditUser(EditUserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.FindByIdAsync(model.Id);
            if (user == null)
            {
                return NotFound();
            }

            // Update user properties
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Email = model.Email;
            user.UserRole = model.Role;
            user.DoesPrintDesign = model.DoesPrintDesign;
            user.DoesBrandingDesign = model.DoesBrandingDesign;
            user.DoesDigitalDesign = model.DoesDigitalDesign;
            user.Does3dDesign = model.Does3dDesign;

            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                TempData["Success"] = "User updated successfully!";
                return RedirectToAction(nameof(Index));
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                var result = await _userManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    return Json(new { success = true });
                }
            }
            return Json(new { success = false });
        }
    }
}