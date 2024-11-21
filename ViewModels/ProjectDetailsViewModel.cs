using Senior_Project_Graphic_Design_Portfolio.Models;

namespace Senior_Project_Graphic_Design_Portfolio.ViewModels;

public class ProjectDetailsViewModel
{
    public object Project { get; set; } = null!;
    public string ProjectType { get; set; } = string.Empty;
    public List<ProjectComment> Comments { get; set; } = new();
    public string NewComment { get; set; } = string.Empty;
    public double AverageRating { get; set; }
    public int TotalRatings { get; set; }
    public int? UserRating { get; set; }
}
