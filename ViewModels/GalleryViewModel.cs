using Senior_Project_Graphic_Design_Portfolio.Models;

namespace Senior_Project_Graphic_Design_Portfolio.ViewModels;

public class GalleryViewModel
{
    public List<ProjectCardViewModel> PrintProjects { get; set; } = new();
    public List<ProjectCardViewModel> DigitalProjects { get; set; } = new();
    public List<ProjectCardViewModel> BrandingProjects { get; set; } = new();
    public List<ProjectCardViewModel> ThreeDProjects { get; set; } = new();
    public string SelectedCategory { get; set; } = "all";
}

