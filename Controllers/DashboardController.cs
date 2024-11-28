using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Senior_Project_Graphic_Design_Portfolio.ViewModels;
using Senior_Project_Graphic_Design_Portfolio.Data;
using Senior_Project_Graphic_Design_Portfolio.Models;
using System.Globalization;
using Microsoft.AspNetCore.Identity;
using Senior_Project_Graphic_Design_Portfolio.Areas.Admin.ViewModels;

namespace Senior_Project_Graphic_Design_Portfolio.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public DashboardController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null || user.UserRole != "Designer")
            {
                return RedirectToAction("AccessDenied", "Account");
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var projectStats = new List<ProjectStatsViewModel>
            {
                new() {
                    Category = "Print",
                    Count = await _context.PrintProjects.CountAsync(p => p.ApplicationUserId == userId),
                    Views = await _context.PrintProjects.Where(p => p.ApplicationUserId == userId).SumAsync(p => p.Views),
                    Rating = await _context.PrintProjects.Where(p => p.ApplicationUserId == userId).AverageAsync(p => p.Rating) ?? 0
                },
                new() {
                    Category = "Digital",
                    Count = await _context.DigitalDesignProjects.CountAsync(p => p.ApplicationUserId == userId),
                    Views = await _context.DigitalDesignProjects.Where(p => p.ApplicationUserId == userId).SumAsync(p => p.Views),
                    Rating = await _context.DigitalDesignProjects.Where(p => p.ApplicationUserId == userId).AverageAsync(p => p.Rating) ?? 0
                },
                new() {
                    Category = "Branding",
                    Count = await _context.BrandingProjects.CountAsync(p => p.ApplicationUserId == userId),
                    Views = await _context.BrandingProjects.Where(p => p.ApplicationUserId == userId).SumAsync(p => p.Views),
                    Rating = await _context.BrandingProjects.Where(p => p.ApplicationUserId == userId).AverageAsync(p => p.Rating) ?? 0
                },
                new() {
                    Category = "3D",
                    Count = await _context.ThreeDProjects.CountAsync(p => p.ApplicationUserId == userId),
                    Views = await _context.ThreeDProjects.Where(p => p.ApplicationUserId == userId).SumAsync(p => p.Views),
                    Rating = await _context.ThreeDProjects.Where(p => p.ApplicationUserId == userId).AverageAsync(p => p.Rating) ?? 0
                }
            };

            var viewModel = new ViewModels.DashboardViewModel
            {
                ProjectStats = projectStats,
                UserId = userId,
                TotalUsers = await _context.Users.CountAsync(),
                TotalDesigners = await _context.Users.CountAsync(u => u.UserRole == "Designer"),
                TotalViewers = await _context.Users.CountAsync(u => u.UserRole == "Viewer"),
                Users = await _context.Users.CountAsync()
            };

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> GetMonthlyViews()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var monthlyViews = await _context.ProjectViews
                .Where(v => v.ApplicationUserId == userId)
                .GroupBy(v => v.ViewDate.Month)
                .Select(g => new {
                    Month = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(g.Key),
                    Views = g.Count()
                })
                .OrderByDescending(x => x.Month)
                .Take(6)
                .ToListAsync();

            return Json(monthlyViews);
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
                return RedirectToAction(nameof(Index));
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View(model);
        }
    }
}