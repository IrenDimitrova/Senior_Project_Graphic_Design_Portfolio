using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Senior_Project_Graphic_Design_Portfolio.Models;

public class ProjectComment
{
    [Key]
    public int Id { get; set; }

    public int ProjectId { get; set; }

    [Required]
    public string ProjectType { get; set; } = string.Empty;

    [Required]
    public string UserId { get; set; } = string.Empty;

    [ForeignKey("UserId")]
    public virtual ApplicationUser User { get; set; } = null!;

    [Required]
    public string Comment { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.Now;

    // Add navigation properties for each project type
    public int? PrintProjectId { get; set; }
    [ForeignKey("PrintProjectId")]
    public virtual PrintProject? PrintProject { get; set; }

    public int? DigitalProjectId { get; set; }
    [ForeignKey("DigitalProjectId")]
    public virtual DigitalDesignProject? DigitalProject { get; set; }

    public int? BrandingProjectId { get; set; }
    [ForeignKey("BrandingProjectId")]
    public virtual BrandingProject? BrandingProject { get; set; }

    public int? ThreeDProjectId { get; set; }
    [ForeignKey("ThreeDProjectId")]
    public virtual ThreeDProject? ThreeDProject { get; set; }
}
