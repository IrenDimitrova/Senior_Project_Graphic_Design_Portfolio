using System.ComponentModel.DataAnnotations;

namespace Senior_Project_Graphic_Design_Portfolio.Models;

public class ContactForm
{
    [Required]
    [StringLength(100)]
    public string Name { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [StringLength(200)]
    public string Subject { get; set; }

    [Required]
    [StringLength(1000)]
    public string Message { get; set; }
}
