using Senior_Project_Graphic_Design_Portfolio.Models;
using System.ComponentModel.DataAnnotations;

public class ProjectManagementViewModel
{
    public List<PrintProject> PrintProjects { get; set; } = new();
    public List<DigitalDesignProject> DigitalProjects { get; set; } = new();
    public List<BrandingProject> BrandingProjects { get; set; } = new();
    public List<ThreeDProject> ThreeDProjects { get; set; } = new();
}

public class CreateProjectViewModel
{
    [Required]
    public string ProjectName { get; set; } = string.Empty;

    [Required]
    public string Description { get; set; } = string.Empty;

    [Required]
    public string ProjectType { get; set; } = string.Empty;

    public IFormFile? ProjectImage { get; set; }
}