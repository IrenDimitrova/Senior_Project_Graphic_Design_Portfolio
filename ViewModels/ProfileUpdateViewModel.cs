using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Senior_Project_Graphic_Design_Portfolio.ViewModels
{
    public class ProfileUpdateViewModel
    {
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Bio")]
        [MaxLength(500)]
        public string Bio { get; set; }

        [Display(Name = "Website")]
        [Url]
        public string Website { get; set; }

        [Display(Name = "Profile Image")]
        public IFormFile ProfileImage { get; set; }

        // Service offerings
        public bool DoesPrintDesign { get; set; }
        public bool DoesDigitalDesign { get; set; }
        public bool DoesBrandingDesign { get; set; }
        public bool Does3dDesign { get; set; }
    }
}
