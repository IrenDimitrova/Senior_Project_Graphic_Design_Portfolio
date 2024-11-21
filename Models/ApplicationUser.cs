using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Senior_Project_Graphic_Design_Portfolio.Models
{
    public class ApplicationUser : IdentityUser
    {
        [PersonalData]
        [Required]
        [StringLength(50)]
        public string FirstName { get; set; } = string.Empty;

        [PersonalData]
        [Required]
        [StringLength(50)]
        public string LastName { get; set; } = string.Empty;

        [PersonalData]
        [StringLength(255)]
        public string? Address { get; set; }

        [PersonalData]
        [StringLength(255)]
        public override string? PasswordHash { get; set; }

        [PersonalData]
        [StringLength(255)]
        public string? Website { get; set; }

        [PersonalData]
        public string? Biography { get; set; }

        [PersonalData]
        public bool DoesPrintDesign { get; set; }

        [PersonalData]
        public bool DoesBrandingDesign { get; set; }

        [PersonalData]
        public bool DoesDigitalDesign { get; set; }

        [PersonalData]
        public bool Does3dDesign { get; set; }

        [Required]
        [StringLength(50)]
        public string UserRole { get; set; } = "Viewer";
    }
}