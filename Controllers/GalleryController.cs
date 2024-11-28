using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using NuGet.ProjectModel;
using Senior_Project_Graphic_Design_Portfolio.Data;
using Senior_Project_Graphic_Design_Portfolio.Models;
using Senior_Project_Graphic_Design_Portfolio.ViewModels;
using System.Security.Claims;

namespace Senior_Project_Graphic_Design_Portfolio.Controllers
{
    public class GalleryController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public GalleryController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index(string category = "all")
        {
            ViewData["CurrentPage"] = "Gallery";
            var model = new GalleryViewModel { SelectedCategory = category };

            if (category is "all" or "print")
            {
                var printProjects = await _context.PrintProjects
                    .Include(p => p.ApplicationUser)
                    .Select(p => new ProjectCardViewModel
                    {
                        ProjectId = p.ProjectID,
                        ProjectName = p.ProjectName,
                        Description = p.Description,
                        ImagePath = p.ImagePath,
                        DesignerName = p.ApplicationUser != null
                            ? $"{p.ApplicationUser.FirstName} {p.ApplicationUser.LastName}"
                            : "Unknown Designer",
                        AverageRating = _context.ProjectRatings
                            .Where(r => r.ProjectId == p.ProjectID && r.ProjectType == "Print")
                            .Select(r => r.Rating)
                            .DefaultIfEmpty()
                            .Average(),
                        TotalRatings = _context.ProjectRatings
                            .Count(r => r.ProjectId == p.ProjectID && r.ProjectType == "Print")
                    })
                    .ToListAsync();

                model.PrintProjects = printProjects;
            }
            if (category is "all" or "branding")
            {
                var brandingProjects = await _context.BrandingProjects
                    .Include(p => p.ApplicationUser)
                    .Select(p => new ProjectCardViewModel
                    {
                        ProjectId = p.ProjectID,
                        ProjectName = p.ProjectName,
                        Description = p.Description,
                        ImagePath = p.ImagePath,
                        DesignerName = p.ApplicationUser != null
                            ? $"{p.ApplicationUser.FirstName} {p.ApplicationUser.LastName}"
                            : "Unknown Designer",
                        AverageRating = _context.ProjectRatings
                            .Where(r => r.ProjectId == p.ProjectID && r.ProjectType == "Branding")
                            .Select(r => r.Rating)
                            .DefaultIfEmpty()
                            .Average(),
                        TotalRatings = _context.ProjectRatings
                            .Count(r => r.ProjectId == p.ProjectID && r.ProjectType == "Branding")
                    })
                    .ToListAsync();

                model.BrandingProjects = brandingProjects;
            }
            if (category is "all" or "digital")
            {
                var digitalProjects = await _context.DigitalDesignProjects
                    .Include(p => p.ApplicationUser)
                    .Select(p => new ProjectCardViewModel
                    {
                        ProjectId = p.ProjectID,
                        ProjectName = p.ProjectName,
                        Description = p.Description,
                        ImagePath = p.ImagePath,
                        DesignerName = p.ApplicationUser != null
                            ? $"{p.ApplicationUser.FirstName} {p.ApplicationUser.LastName}"
                            : "Unknown Designer",
                        AverageRating = _context.ProjectRatings
                            .Where(r => r.ProjectId == p.ProjectID && r.ProjectType == "Digital")
                            .Select(r => r.Rating)
                            .DefaultIfEmpty()
                            .Average(),
                        TotalRatings = _context.ProjectRatings
                            .Count(r => r.ProjectId == p.ProjectID && r.ProjectType == "Digital")
                    })
                    .ToListAsync();

                model.DigitalProjects = digitalProjects;
            }
            if (category is "all" or "3d")
            {
                var threedProjects = await _context.ThreeDProjects
                    .Include(p => p.ApplicationUser)
                    .Select(p => new ProjectCardViewModel
                    {
                        ProjectId = p.ProjectID,
                        ProjectName = p.ProjectName,
                        Description = p.Description,
                        ImagePath = p.ImagePath,
                        DesignerName = p.ApplicationUser != null
                            ? $"{p.ApplicationUser.FirstName} {p.ApplicationUser.LastName}"
                            : "Unknown Designer",
                        AverageRating = _context.ProjectRatings
                            .Where(r => r.ProjectId == p.ProjectID && r.ProjectType == "3d")
                            .Select(r => r.Rating)
                            .DefaultIfEmpty()
                            .Average(),
                        TotalRatings = _context.ProjectRatings
                            .Count(r => r.ProjectId == p.ProjectID && r.ProjectType == "3d")
                    })
                    .ToListAsync();

                model.ThreeDProjects = threedProjects;
            }
            return View(model);
        }

        public async Task<IActionResult> Details(int id, string projectType)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var model = new ProjectDetailsViewModel();

            switch (projectType.ToLower())
            {
                case "print":
                    var printProject = await _context.PrintProjects
                        .Include(p => p.ApplicationUser)
                        .Include(p => p.Comments)
                            .ThenInclude(c => c.User)
                        .FirstOrDefaultAsync(p => p.ProjectID == id);
                    if (printProject == null) return NotFound();
                    var ratings = await _context.ProjectRatings
                .Where(r => r.ProjectId == id && r.ProjectType == projectType)
                .ToListAsync();
                    model.Project = printProject;
                    model.ProjectType = projectType;
                    model.Comments = printProject.Comments.ToList();
                    model.AverageRating = ratings.Any() ? ratings.Average(r => r.Rating) : 0;
                    model.TotalRatings = ratings.Count;
                    if (userId != null)
                    {
                        model.UserRating = ratings
                            .FirstOrDefault(r => r.UserId == userId)?.Rating;
                    }
                    break;
                case "digital":
                    var digitalProject = await _context.DigitalDesignProjects
                        .Include(p => p.ApplicationUser)
                        .Include(p => p.Comments)
                            .ThenInclude(c => c.User)
                        .FirstOrDefaultAsync(p => p.ProjectID == id);
                    if (digitalProject == null) return NotFound();
                    ratings = await _context.ProjectRatings
    .Where(r => r.ProjectId == id && r.ProjectType == projectType)
    .ToListAsync();
                    model.Project = digitalProject;
                    model.ProjectType = projectType;
                    model.Comments = digitalProject.Comments.ToList();
                    model.AverageRating = ratings.Any() ? ratings.Average(r => r.Rating) : 0;
                    model.TotalRatings = ratings.Count;
                    if (userId != null)
                    {
                        model.UserRating = ratings
                            .FirstOrDefault(r => r.UserId == userId)?.Rating;
                    }
                    break;
                case "branding":
                    var brandingProject = await _context.BrandingProjects
                        .Include(p => p.ApplicationUser)
                        .Include(p => p.Comments)
                            .ThenInclude(c => c.User)
                        .FirstOrDefaultAsync(p => p.ProjectID == id);
                    if (brandingProject == null) return NotFound();
                    ratings = await _context.ProjectRatings
    .Where(r => r.ProjectId == id && r.ProjectType == projectType)
    .ToListAsync();
                    model.Project = brandingProject;
                    model.ProjectType = projectType;
                    model.Comments = brandingProject.Comments.ToList();
                    model.AverageRating = ratings.Any() ? ratings.Average(r => r.Rating) : 0;
                    model.TotalRatings = ratings.Count;
                    if (userId != null)
                    {
                        model.UserRating = ratings
                            .FirstOrDefault(r => r.UserId == userId)?.Rating;
                    }
                    break;
                case "3d":
                    var threeDProject = await _context.ThreeDProjects
                        .Include(p => p.ApplicationUser)
                        .Include(p => p.Comments)
                            .ThenInclude(c => c.User)
                        .FirstOrDefaultAsync(p => p.ProjectID == id);
                    if (threeDProject == null) return NotFound();
                    ratings = await _context.ProjectRatings
                .Where(r => r.ProjectId == id && r.ProjectType == projectType)
                .ToListAsync();
                    model.Project = threeDProject;
                    model.ProjectType = projectType;
                    model.Comments = threeDProject.Comments.ToList();
                    model.AverageRating = ratings.Any() ? ratings.Average(r => r.Rating) : 0;
                    model.TotalRatings = ratings.Count;
                    if (userId != null)
                    {
                        model.UserRating = ratings
                            .FirstOrDefault(r => r.UserId == userId)?.Rating;
                    }
                    break;
                default:
                    return NotFound();
            }

            ViewData["ProjectType"] = projectType;  

            if (model.Project != null)
            {
                var comments = model.Project.GetType()
                    .GetProperty("Comments")
                    ?.GetValue(model.Project) as IEnumerable<ProjectComment>;

                ViewBag.CommentsCount = comments?.Count() ?? 0;
            }

            return View(model);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddComment(int projectId, string projectType, string comment)
        {
            if (string.IsNullOrEmpty(comment))
            {
                TempData["Error"] = "Comment cannot be empty";
                return RedirectToAction(nameof(Details), new { id = projectId, projectType });
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return Challenge();
            }

            var projectComment = new ProjectComment
            {
                ProjectType = projectType,
                UserId = userId,
                Comment = comment,
                CreatedAt = DateTime.Now
            };

            // Set the appropriate project ID based on type
            switch (projectType.ToLower())
            {
                case "print":
                    projectComment.PrintProjectId = projectId;
                    break;
                case "digital":
                    projectComment.DigitalProjectId = projectId;
                    break;
                case "branding":
                    projectComment.BrandingProjectId = projectId;
                    break;
                case "3d":
                    projectComment.ThreeDProjectId = projectId;
                    break;
            }

            try
            {
                _context.ProjectComments.Add(projectComment);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Comment added successfully!";
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Failed to add comment. Please try again.";
            }

            return RedirectToAction(nameof(Details), new { id = projectId, projectType });
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddRating(int projectId, string projectType, int rating)
        {
            if (rating < 1 || rating > 5)
            {
                TempData["Error"] = "Invalid rating value";
                return RedirectToAction(nameof(Details), new { id = projectId, projectType });
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return Challenge();
            }

            var existingRating = await _context.ProjectRatings
                .FirstOrDefaultAsync(r => r.ProjectId == projectId &&
                                        r.ProjectType == projectType &&
                                        r.UserId == userId);

            if (existingRating != null)
            {
                existingRating.Rating = rating;
                _context.ProjectRatings.Update(existingRating);
            }
            else
            {
                var projectRating = new ProjectRating
                {
                    ProjectId = projectId,
                    ProjectType = projectType,
                    UserId = userId,
                    Rating = rating
                };
                _context.ProjectRatings.Add(projectRating);
            }

            await _context.SaveChangesAsync();
            TempData["Success"] = "Rating submitted successfully!";

            return RedirectToAction(nameof(Details), new { id = projectId, projectType });
        }
    }
}