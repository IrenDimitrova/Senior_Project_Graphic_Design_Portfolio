using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Senior_Project_Graphic_Design_Portfolio.Models
{
    public class ProjectView
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public string ProjectType { get; set; } = string.Empty;
        public DateTime ViewDate { get; set; }
        public string? ApplicationUserId { get; set; }
        [ForeignKey("ApplicationUserId")]
        public ApplicationUser? ApplicationUser { get; set; }
    }
}