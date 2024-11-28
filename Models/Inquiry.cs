using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Senior_Project_Graphic_Design_Portfolio.Models
{
    public class Inquiry
    {
        [Key]
        public int InquiryId { get; set; }

        [ForeignKey("Sender")]
        public string? SenderId { get; set; }

        [MaxLength(255)]
        public string? SenderEmail { get; set; }

        [MaxLength(100)]
        public string? SenderName { get; set; }

        [Required]
        [ForeignKey("Receiver")]
        public string ReceiverId { get; set; }

        [Required]
        public string Message { get; set; }

        [MaxLength(255)]
        public string Subject { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [MaxLength(50)]
        public string Status { get; set; } = "Unread";

        public virtual ApplicationUser? Sender { get; set; }
        public virtual ApplicationUser Receiver { get; set; }
    }
}