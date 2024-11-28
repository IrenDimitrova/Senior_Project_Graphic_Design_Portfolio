namespace Senior_Project_Graphic_Design_Portfolio.Areas.Admin.ViewModels
{
    public class EditUserViewModel
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public bool DoesPrintDesign { get; set; }
        public bool DoesBrandingDesign { get; set; }
        public bool DoesDigitalDesign { get; set; }
        public bool Does3dDesign { get; set; }
    }
}
