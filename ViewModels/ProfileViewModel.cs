using Senior_Project_Graphic_Design_Portfolio.Models;

namespace Senior_Project_Graphic_Design_Portfolio.ViewModels;

public class ProfileViewModel
{
    // Basic user information properties remain the same
    public string UserId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string? Biography { get; set; }
    public string? Website { get; set; }
    public string? ProfileImagePath { get; set; }

    // Service flags remain the same
    public bool DoesPrintDesign { get; set; }
    public bool DoesDigitalDesign { get; set; }
    public bool DoesBrandingDesign { get; set; }
    public bool Does3dDesign { get; set; }

    // Update the Digital project collection to match your DbContext
    public ICollection<PrintProject> PrintProjects { get; set; } = new List<PrintProject>();
    public ICollection<DigitalDesignProject> DigitalProjects { get; set; } = new List<DigitalDesignProject>();
    public ICollection<BrandingProject> BrandingProjects { get; set; } = new List<BrandingProject>();
    public ICollection<ThreeDProject> ThreeDProjects { get; set; } = new List<ThreeDProject>();

    // Statistics properties remain the same
    public int TotalProjects { get; set; }
    public double? AverageRating { get; set; }
    public int TotalViews { get; set; }
}