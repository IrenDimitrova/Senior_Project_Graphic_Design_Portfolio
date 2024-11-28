namespace Senior_Project_Graphic_Design_Portfolio.Models;

public class Contact
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Subject { get; set; }
    public string Message { get; set; }
    public string UserId { get; set; }
    public DateTime DateSent { get; set; }
}
