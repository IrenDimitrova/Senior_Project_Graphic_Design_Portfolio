using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Senior_Project_Graphic_Design_Portfolio.Models;

namespace Senior_Project_Graphic_Design_Portfolio.ViewModels
{
    public class ProfileUpdateViewModel
    {
        [Required]
        [Display(Name = "First Name")]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        [StringLength(50)]
        public string LastName { get; set; }

        [Display(Name = "Biography")]
        public string? Biography { get; set; }

        [Display(Name = "Website")]
        [Url(ErrorMessage = "Please enter a valid website URL")]
        public string? Website { get; set; }

        [Display(Name = "Profile Image")]
        [AllowedExtensions(new string[] { ".jpg", ".jpeg", ".png" })]
        [MaxFileSize(5 * 1024 * 1024)] // 5MB limit
        public IFormFile? ProfileImage { get; set; }

        public bool DoesPrintDesign { get; set; }
        public bool DoesBrandingDesign { get; set; }
        public bool DoesDigitalDesign { get; set; }
        public bool Does3dDesign { get; set; }
    }
}