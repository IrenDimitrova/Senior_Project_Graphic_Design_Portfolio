using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Senior_Project_Graphic_Design_Portfolio.Models
{
    public class DigitalDesignProject
    {
        [Key]
        public int ProjectID { get; set; }
        [Required]
        [StringLength(100)]
        public string ProjectName { get; set; } = string.Empty;
        [Required]
        public string Description { get; set; } = string.Empty;
        [Required]
        public DateTime CreationDate { get; set; } = DateTime.Now;
        public string? ApplicationUserId { get; set; }
        [ForeignKey("ApplicationUserId")]
        public ApplicationUser? ApplicationUser { get; set; }
        public int Views { get; set; }
        public double? Rating { get; set; }

        public string ImagePath { get; set; }
        public virtual ICollection<ProjectComment> Comments { get; set; } = new List<ProjectComment>();

    }
}