using System.ComponentModel.DataAnnotations;

namespace Senior_Project_Graphic_Design_Portfolio.Models
{
    public class Company
    {
        public Company()
        {
            CompanyName = string.Empty;
            Industry = string.Empty;
            ContactPerson = string.Empty;
            Email = string.Empty;
            Phone = string.Empty;
            Address = string.Empty;
            Website = string.Empty;
        }

        [Key]
        public int CompanyID { get; set; }

        [Required]
        [StringLength(100)]
        public string CompanyName { get; set; }

        [Required]
        [StringLength(100)]
        public string Industry { get; set; }

        [Required]
        [StringLength(100)]
        public string ContactPerson { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(255)]
        public string Email { get; set; }

        [Required]
        [StringLength(20)]
        public string Phone { get; set; }

        [Required]
        [StringLength(255)]
        public string Address { get; set; }

        [Required]
        [StringLength(255)]
        public string Website { get; set; }
    }
}