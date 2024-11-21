// ViewModels/RegisterViewModel.cs
using System.ComponentModel.DataAnnotations;

namespace Senior_Project_Graphic_Design_Portfolio.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; } = string.Empty;

        [Required]
        [StringLength(100, MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; } = string.Empty;

        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "Passwords don't match.")]
        public string ConfirmPassword { get; set; } = string.Empty;

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; } = string.Empty;

        [Display(Name = "Address")]
        public string? Address { get; set; }

        [Display(Name = "Website")]
        public string? Website { get; set; }

        [Display(Name = "Account Type")]
        public string AccountType { get; set; } = "Viewer";

        [Display(Name = "Biography")]
        public string? Biography { get; set; }

        [Display(Name = "Print Design")]
        public bool DoesPrintDesign { get; set; }

        [Display(Name = "Branding Design")]
        public bool DoesBrandingDesign { get; set; }

        [Display(Name = "Digital Design")]
        public bool DoesDigitalDesign { get; set; }

        [Display(Name = "3D Design")]
        public bool Does3dDesign { get; set; }
    }
}