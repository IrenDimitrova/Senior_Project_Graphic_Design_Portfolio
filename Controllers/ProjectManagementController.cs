using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Senior_Project_Graphic_Design_Portfolio.Data;
using Senior_Project_Graphic_Design_Portfolio.Models;
using Senior_Project_Graphic_Design_Portfolio.ViewModels;
using System.Security.Claims;

namespace Senior_Project_Graphic_Design_Portfolio.Controllers
{
    [Authorize(Roles = "Designer")]
    public class ProjectManagementController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public ProjectManagementController(ApplicationDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var viewModel = new ProjectManagementViewModel
            {
                PrintProjects = await _context.PrintProjects
                    .Where(p => p.ApplicationUserId == userId)
                    .ToListAsync(),
                DigitalProjects = await _context.DigitalDesignProjects
                    .Where(p => p.ApplicationUserId == userId)
                    .ToListAsync(),
                BrandingProjects = await _context.BrandingProjects
                    .Where(p => p.ApplicationUserId == userId)
                    .ToListAsync(),
                ThreeDProjects = await _context.ThreeDProjects
                    .Where(p => p.ApplicationUserId == userId)
                    .ToListAsync()
            };

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new CreateProjectViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateProjectViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            string uniqueFileName = null;

            if (model.ProjectImage != null)
            {
                string uploadsFolder = Path.Combine(_environment.WebRootPath, "project-images");
                // Create directory if it doesn't exist
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.ProjectImage.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await model.ProjectImage.CopyToAsync(fileStream);
                }
            }

            switch (model.ProjectType)
            {
                case "Print":
                    var printProject = new PrintProject
                    {
                        ProjectName = model.ProjectName,
                        Description = model.Description,
                        CreationDate = DateTime.Now,
                        ApplicationUserId = userId,
                        ImagePath = uniqueFileName
                    };
                    _context.PrintProjects.Add(printProject);
                    break;
                case "Digital":
                    var digitalProject = new DigitalDesignProject
                    {
                        ProjectName = model.ProjectName,
                        Description = model.Description,
                        CreationDate = DateTime.Now,
                        ApplicationUserId = userId,
                        ImagePath = uniqueFileName
                    };
                    _context.DigitalDesignProjects.Add(digitalProject);
                    break;
                case "Branding":
                    var brandingProject = new BrandingProject
                    {
                        ProjectName = model.ProjectName,
                        Description = model.Description,
                        CreationDate = DateTime.Now,
                        ApplicationUserId = userId,
                        ImagePath = uniqueFileName
                    };
                    _context.BrandingProjects.Add(brandingProject);
                    break;
                case "3D":
                    var threeDProject = new ThreeDProject
                    {
                        ProjectName = model.ProjectName,
                        Description = model.Description,
                        CreationDate = DateTime.Now,
                        ApplicationUserId = userId,
                        ImagePath = uniqueFileName
                    };
                    _context.ThreeDProjects.Add(threeDProject);
                    break;
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id, string projectType)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var model = new EditProjectViewModel { Id = id, ProjectType = projectType };

            switch (projectType.ToLower())
            {
                case "print":
                    var printProject = await _context.PrintProjects
                        .FirstOrDefaultAsync(p => p.ProjectID == id && p.ApplicationUserId == userId);
                    if (printProject == null) return NotFound();
                    model.ProjectName = printProject.ProjectName;
                    model.Description = printProject.Description;
                    model.CurrentImagePath = printProject.ImagePath;
                    break;
                case "digital":
                    var digitalProject = await _context.DigitalDesignProjects
                        .FirstOrDefaultAsync(p => p.ProjectID == id && p.ApplicationUserId == userId);
                    if (digitalProject == null) return NotFound();
                    model.ProjectName = digitalProject.ProjectName;
                    model.Description = digitalProject.Description;
                    model.CurrentImagePath = digitalProject.ImagePath;
                    break;
                case "branding":
                    var brandingProject = await _context.BrandingProjects
                        .FirstOrDefaultAsync(p => p.ProjectID == id && p.ApplicationUserId == userId);
                    if (brandingProject == null) return NotFound();
                    model.ProjectName = brandingProject.ProjectName;
                    model.Description = brandingProject.Description;
                    model.CurrentImagePath = brandingProject.ImagePath;
                    break;
                case "3d":
                    var threeDProject = await _context.ThreeDProjects
                        .FirstOrDefaultAsync(p => p.ProjectID == id && p.ApplicationUserId == userId);
                    if (threeDProject == null) return NotFound();
                    model.ProjectName = threeDProject.ProjectName;
                    model.Description = threeDProject.Description;
                    model.CurrentImagePath = threeDProject.ImagePath;
                    break;
                default:
                    return NotFound();
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditProjectViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            string uniqueFileName = model.CurrentImagePath;

            if (model.ProjectImage != null)
            {
                string uploadsFolder = Path.Combine(_environment.WebRootPath, "project-images");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                // Delete old image if exists
                if (!string.IsNullOrEmpty(model.CurrentImagePath))
                {
                    var oldImagePath = Path.Combine(uploadsFolder, model.CurrentImagePath);
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }

                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.ProjectImage.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await model.ProjectImage.CopyToAsync(fileStream);
                }
            }

            switch (model.ProjectType.ToLower())
            {
                case "print":
                    var printProject = await _context.PrintProjects
                        .FirstOrDefaultAsync(p => p.ProjectID == model.Id && p.ApplicationUserId == userId);
                    if (printProject == null) return NotFound();
                    printProject.ProjectName = model.ProjectName;
                    printProject.Description = model.Description;
                    printProject.ImagePath = uniqueFileName;
                    break;
                case "digital":
                    var digitalProject = await _context.DigitalDesignProjects
                        .FirstOrDefaultAsync(p => p.ProjectID == model.Id && p.ApplicationUserId == userId);
                    if (digitalProject == null) return NotFound();
                    digitalProject.ProjectName = model.ProjectName;
                    digitalProject.Description = model.Description;
                    digitalProject.ImagePath = uniqueFileName;
                    break;
                case "branding":
                    var brandingProject = await _context.BrandingProjects
                        .FirstOrDefaultAsync(p => p.ProjectID == model.Id && p.ApplicationUserId == userId);
                    if (brandingProject == null) return NotFound();
                    brandingProject.ProjectName = model.ProjectName;
                    brandingProject.Description = model.Description;
                    brandingProject.ImagePath = uniqueFileName;
                    break;
                case "3d":
                    var threeDProject = await _context.ThreeDProjects
                        .FirstOrDefaultAsync(p => p.ProjectID == model.Id && p.ApplicationUserId == userId);
                    if (threeDProject == null) return NotFound();
                    threeDProject.ProjectName = model.ProjectName;
                    threeDProject.Description = model.Description;
                    threeDProject.ImagePath = uniqueFileName;
                    break;
                default:
                    return NotFound();
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, string projectType)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            switch (projectType.ToLower())
            {
                case "print":
                    var printProject = await _context.PrintProjects
                        .FirstOrDefaultAsync(p => p.ProjectID == id && p.ApplicationUserId == userId);
                    if (printProject != null)
                        _context.PrintProjects.Remove(printProject);
                    break;
                    // Similar cases for other project types
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}