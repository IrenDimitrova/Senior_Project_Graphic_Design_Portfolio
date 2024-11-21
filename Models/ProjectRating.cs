using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Senior_Project_Graphic_Design_Portfolio.Models;

public class ProjectRating
{
    public int Id { get; set; }
    public int ProjectId { get; set; }
    [Required]
    public string ProjectType { get; set; } = string.Empty;
    [Required]
    public string UserId { get; set; } = string.Empty;
    [ForeignKey("UserId")]
    public virtual ApplicationUser User { get; set; } = null!;
    public int Rating { get; set; }
}
