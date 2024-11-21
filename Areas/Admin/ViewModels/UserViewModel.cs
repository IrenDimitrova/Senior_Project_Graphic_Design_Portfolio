namespace Senior_Project_Graphic_Design_Portfolio.Areas.Admin.ViewModels
{
    public class UserViewModel
    {
        public string Id { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public bool DoesPrintDesign { get; set; }
        public bool DoesBrandingDesign { get; set; }
        public bool DoesDigitalDesign { get; set; }
        public bool Does3dDesign { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public string Status { get; set; } = "Active";
    }

    public class DashboardViewModel
    {
        public int TotalUsers { get; set; }
        public int TotalDesigners { get; set; }
        public int TotalViewers { get; set; }
        public List<UserViewModel> Users { get; set; } = new List<UserViewModel>();
    }
}