using MailKit.Security;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using Senior_Project_Graphic_Design_Portfolio.Data;
using Senior_Project_Graphic_Design_Portfolio.Models;

namespace Senior_Project_Graphic_Design_Portfolio.Controllers;

// Install NuGet package: MailKit

// ContactController.cs
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Net.Mail;

public class ContactController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IConfiguration _configuration;

    public ContactController(
        ApplicationDbContext context,
        UserManager<ApplicationUser> userManager,
        IConfiguration configuration)
    {
        _context = context;
        _userManager = userManager;
        _configuration = configuration;
    }

    [HttpPost]
    public async Task<IActionResult> Send(ContactForm model)
    {
        if (!ModelState.IsValid)
        {
            TempData["Error"] = "Please fill out all required fields correctly.";
            return RedirectToAction("Index", "Profile");
        }

        try
        {
            var user = await _userManager.GetUserAsync(User);

            var contact = new Contact
            {
                Name = model.Name,
                Email = model.Email,
                Subject = model.Subject,
                Message = model.Message,
                UserId = user?.Id,
                DateSent = DateTime.UtcNow
            };

            _context.Contacts.Add(contact);
            await _context.SaveChangesAsync();

            await SendEmail(contact);

            TempData["Success"] = "Your message has been sent successfully!";
        }
        catch (Exception ex)
        {
            TempData["Error"] = "An error occurred while sending your message. Please try again later.";
            // Log the exception
        }

        return RedirectToAction("Index", "Profile");
    }

    private async Task SendEmail(Contact contact)
    {
        var apiKey = _configuration["SendGrid:ApiKey"];
        var client = new SendGridClient(apiKey);

        var htmlContent = $@"
           <h2>New Contact Form Submission</h2>
           <p><strong>From:</strong> {contact.Name}</p>
           <p><strong>Email:</strong> {contact.Email}</p>
           <p><strong>Subject:</strong> {contact.Subject}</p>
           <p><strong>Message:</strong></p>
           <p>{contact.Message}</p>
           <p><strong>Date Sent:</strong> {contact.DateSent:g}</p>";

        var msg = new SendGridMessage
        {
            From = new EmailAddress("neri.avortimid@gmail.com", "HivePortfolios"),
            Subject = $"New Contact Form: {contact.Subject}",
            PlainTextContent = contact.Message,
            HtmlContent = htmlContent
        };

        msg.AddTo(new EmailAddress(contact.Email, contact.Name));

        try
        {
            var response = await client.SendEmailAsync(msg);
            Console.WriteLine($"Status Code: {response.StatusCode}");
            var body = await response.Body.ReadAsStringAsync();
            Console.WriteLine($"Response Body: {body}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Full Exception: {ex.Message}");
        }
    }
}