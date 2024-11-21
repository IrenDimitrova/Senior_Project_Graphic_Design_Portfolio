namespace Senior_Project_Graphic_Design_Portfolio.ViewModels;

public class ProjectCardViewModel
{
    public int ProjectId { get; set; }
    public string ProjectName { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string? ImagePath { get; set; }
    public string DesignerName { get; set; } = string.Empty;
    public string ProjectType { get; set; } = string.Empty;
    public double AverageRating { get; set; }
    public int TotalComments { get; set; }
    public int TotalRatings { get; set; }
}