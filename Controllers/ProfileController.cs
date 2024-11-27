using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Senior_Project_Graphic_Design_Portfolio.Data;
using Senior_Project_Graphic_Design_Portfolio.Models;
using Senior_Project_Graphic_Design_Portfolio.ViewModels;

namespace Senior_Project_Graphic_Design_Portfolio.Controllers
{
    public class ProfileController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        // Constructor using dependency injection to get our required services
        public ProfileController(
            UserManager<ApplicationUser> userManager,
            ApplicationDbContext context,
            IWebHostEnvironment webHostEnvironment)
        {
            _userManager = userManager;
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // Action to display the user's own profile
        [Authorize]
        public async Task<IActionResult> Index()
        {
            // Get the currently logged-in user
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound();
            }

            // Get all projects for this user
            var printProjects = await _context.PrintProjects
                .Where(p => p.UserId == user.Id)
                .ToListAsync();

            var digitalProjects = await _context.DigitalProjects
                .Where(p => p.UserId == user.Id)
                .ToListAsync();

            var brandingProjects = await _context.BrandingProjects
                .Where(p => p.UserId == user.Id)
                .ToListAsync();

            var threeDProjects = await _context.ThreeDProjects
                .Where(p => p.UserId == user.Id)
                .ToListAsync();

            // Create the view model
            var viewModel = new ProfileViewModel
            {
                UserId = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Website = user.Website,
                Biography = user.Biography,
                ProfileImagePath = user.ProfileImagePath,
                DoesPrintDesign = user.DoesPrintDesign,
                DoesDigitalDesign = user.DoesDigitalDesign,
                DoesBrandingDesign = user.DoesBrandingDesign,
                Does3dDesign = user.Does3dDesign,
                PrintProjects = printProjects,
                DigitalProjects = digitalProjects,
                BrandingProjects = brandingProjects,
                ThreeDProjects = threeDProjects,
                TotalProjects = printProjects.Count + digitalProjects.Count +
                               brandingProjects.Count + threeDProjects.Count
            };

            return View(viewModel);
        }

        // Action to handle profile editing
        [Authorize]
        public async Task<IActionResult> Edit()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound();
            }

            var viewModel = new ProfileUpdateViewModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Biography = user.Biography,
                Website = user.Website,
                DoesPrintDesign = user.DoesPrintDesign,
                DoesDigitalDesign = user.DoesDigitalDesign,
                DoesBrandingDesign = user.DoesBrandingDesign,
                Does3dDesign = user.Does3dDesign
            };

            return View(viewModel);
        }

        // Action to handle the profile update form submission
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProfileUpdateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound();
            }

            // Update user properties
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Biography = model.Biography;
            user.Website = model.Website;
            user.DoesPrintDesign = model.DoesPrintDesign;
            user.DoesDigitalDesign = model.DoesDigitalDesign;
            user.DoesBrandingDesign = model.DoesBrandingDesign;
            user.Does3dDesign = model.Does3dDesign;

            // Handle profile image upload if provided
            if (model.ProfileImage != null)
            {
                // Generate a unique filename
                string uniqueFileName = Guid.NewGuid().ToString() + "_" + model.ProfileImage.FileName;
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "profile-images");
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                // Create directory if it doesn't exist
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                // Save the file
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await model.ProfileImage.CopyToAsync(fileStream);
                }

                // Update user profile image path
                user.ProfileImagePath = uniqueFileName;
            }

            // Save changes to database
            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                TempData["SuccessMessage"] = "Profile updated successfully!";
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