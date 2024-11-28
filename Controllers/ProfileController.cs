using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging;
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

        public ProfileController(
            UserManager<ApplicationUser> userManager,
            ApplicationDbContext context,
            IWebHostEnvironment webHostEnvironment)
        {
            _userManager = userManager;
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound();
            }

            // Fetch projects with tracking disabled for better performance
            var projects = await _context.ProjectRatings
                .AsNoTracking()
                .Where(p => p.UserId == user.Id)
                .ToListAsync();

            var printProjects = await _context.PrintProjects
                .AsNoTracking()
                .Where(p => p.ApplicationUserId == user.Id)
                .ToListAsync();

            foreach (var item in printProjects)
            {
                var pr = projects.FirstOrDefault(q => q.ProjectId == item.ProjectID && q.ProjectType == "Print");
                if(pr != null)
                item.Rating = pr.Rating;
                else item.Rating = 0;
            }

            var digitalProjects = await _context.DigitalDesignProjects
                .AsNoTracking()
                .Where(p => p.ApplicationUserId == user.Id)
                .ToListAsync();

            foreach (var item1 in digitalProjects)
            {
                var pr = projects.FirstOrDefault(q => q.ProjectId == item1.ProjectID && q.ProjectType == "Digital");
                if (pr != null)
                    item1.Rating = pr.Rating;
                else item1.Rating = 0;
            }

            var brandingProjects = await _context.BrandingProjects
                .AsNoTracking()
                .Where(p => p.ApplicationUserId == user.Id)
                .ToListAsync();

            foreach (var item2 in brandingProjects)
            {
                var pr = projects.FirstOrDefault(q => q.ProjectId == item2.ProjectID && q.ProjectType == "Branding");
                if (pr != null)
                    item2.Rating = pr.Rating;
                else item2.Rating = 0;
            }

            var threeDProjects = await _context.ThreeDProjects
                .AsNoTracking()
                .Where(p => p.ApplicationUserId == user.Id)
                .ToListAsync();

            foreach (var item3 in threeDProjects)
            {
                var pr = projects.FirstOrDefault(q => q.ProjectId == item3.ProjectID && q.ProjectType == "3d");
                if (pr != null)
                    item3.Rating = pr.Rating;
                else item3.Rating = 0;
            }

            // Calculate statistics
            var totalProjects = printProjects.Count() +
                    digitalProjects.Count() +
                    brandingProjects.Count() +
                    threeDProjects.Count();

            var averageRating = 0.0;
            var totalViews = 0;

            if (totalProjects > 0)
            {
                // Calculate total ratings, handling null cases
                var ratings = new List<int>();

                ratings.AddRange((IEnumerable<int>)projects.Select(p => p.Rating));

                averageRating = (double)(ratings!.Any() ? Math.Round(ratings!.Average(), 2)!  : 0);

                // Calculate total views
                totalViews =
                    printProjects.Sum(p => p.Views) +
                    digitalProjects.Sum(p => p.Views) +
                    brandingProjects.Sum(p => p.Views) +
                    threeDProjects.Sum(p => p.Views);
            }

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
                TotalProjects = totalProjects,
                AverageRating = averageRating,
                TotalViews = totalViews
            };

            return View(viewModel);
        }

        public async Task<IActionResult> PublicProfile(string userId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);

            if (user == null)
            {
                return NotFound();
            }
            
            if(user.Id == _userManager.GetUserId(User))
            {
                return RedirectToAction("Index");
            }
            

            // Fetch projects with tracking disabled for better performance
            var projects = await _context.ProjectRatings
                .AsNoTracking()
                .Where(p => p.UserId == user.Id)
                .ToListAsync();

            var printProjects = await _context.PrintProjects
                .AsNoTracking()
                .Where(p => p.ApplicationUserId == user.Id)
                .ToListAsync();

            foreach (var item in printProjects)
            {
                var pr = projects.FirstOrDefault(q => q.ProjectId == item.ProjectID && q.ProjectType == "Print");
                if (pr != null)
                    item.Rating = pr.Rating;
                else item.Rating = 0;
            }

            var digitalProjects = await _context.DigitalDesignProjects
                .AsNoTracking()
                .Where(p => p.ApplicationUserId == user.Id)
                .ToListAsync();

            foreach (var item1 in digitalProjects)
            {
                var pr = projects.FirstOrDefault(q => q.ProjectId == item1.ProjectID && q.ProjectType == "Digital");
                if (pr != null)
                    item1.Rating = pr.Rating;
                else item1.Rating = 0;
            }

            var brandingProjects = await _context.BrandingProjects
                .AsNoTracking()
                .Where(p => p.ApplicationUserId == user.Id)
                .ToListAsync();

            foreach (var item2 in brandingProjects)
            {
                var pr = projects.FirstOrDefault(q => q.ProjectId == item2.ProjectID && q.ProjectType == "Branding");
                if (pr != null)
                    item2.Rating = pr.Rating;
                else item2.Rating = 0;
            }

            var threeDProjects = await _context.ThreeDProjects
                .AsNoTracking()
                .Where(p => p.ApplicationUserId == user.Id)
                .ToListAsync();

            foreach (var item3 in threeDProjects)
            {
                var pr = projects.FirstOrDefault(q => q.ProjectId == item3.ProjectID && q.ProjectType == "3d");
                if (pr != null)
                    item3.Rating = pr.Rating;
                else item3.Rating = 0;
            }

            // Calculate statistics
            var totalProjects = printProjects.Count() +
                    digitalProjects.Count() +
                    brandingProjects.Count() +
                    threeDProjects.Count();

            var averageRating = 0.0;
            var totalViews = 0;

            if (totalProjects > 0)
            {
                // Calculate total ratings, handling null cases
                var ratings = new List<int>();

                ratings.AddRange((IEnumerable<int>)projects.Select(p => p.Rating));

                averageRating = (double)(ratings!.Any() ? Math.Round(ratings!.Average(), 2)! : 0);

                // Calculate total views
                totalViews =
                    printProjects.Sum(p => p.Views) +
                    digitalProjects.Sum(p => p.Views) +
                    brandingProjects.Sum(p => p.Views) +
                    threeDProjects.Sum(p => p.Views);
            }

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
                TotalProjects = totalProjects,
                AverageRating = averageRating,
                TotalViews = totalViews
            };

            return View(viewModel);
        }

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
                try
                {
                    if (model.ProfileImage.Length <= 5 * 1024 * 1024) // 5MB limit
                    {
                        var extension = Path.GetExtension(model.ProfileImage.FileName).ToLower();
                        var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };

                        if (allowedExtensions.Contains(extension))
                        {
                            string uniqueFileName = Guid.NewGuid().ToString() + "_" + model.ProfileImage.FileName;
                            string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "profile-images");
                            string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                            Directory.CreateDirectory(uploadsFolder);

                            // Clean up old profile image
                            if (!string.IsNullOrEmpty(user.ProfileImagePath))
                            {
                                string oldFilePath = Path.Combine(uploadsFolder, user.ProfileImagePath);
                                if (System.IO.File.Exists(oldFilePath))
                                {
                                    System.IO.File.Delete(oldFilePath);
                                }
                            }

                            // Save new image
                            using (var fileStream = new FileStream(filePath, FileMode.Create))
                            {
                                await model.ProfileImage.CopyToAsync(fileStream);
                            }

                            user.ProfileImagePath = uniqueFileName;
                        }
                        else
                        {
                            ModelState.AddModelError("ProfileImage", "Please upload only JPG or PNG files.");
                            return View(model);
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("ProfileImage", "File size must be less than 5MB.");
                        return View(model);
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("ProfileImage", "Error uploading image. Please try again.");
                    return View(model);
                }
            }

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