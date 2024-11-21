using System.ComponentModel.DataAnnotations;

public class EditProjectViewModel
{
    public int Id { get; set; }

    [Required]
    public string ProjectName { get; set; } = string.Empty;

    [Required]
    public string Description { get; set; } = string.Empty;

    [Required]
    public string ProjectType { get; set; } = string.Empty;

    public IFormFile? ProjectImage { get; set; }

    public string? CurrentImagePath { get; set; }
}