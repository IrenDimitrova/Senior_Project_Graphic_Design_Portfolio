namespace Senior_Project_Graphic_Design_Portfolio.ViewModels
{
    public class ProfileViewModel
    {
        // Basic user information
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        // Designer-specific information
        public string Bio { get; set; }
        public string Website { get; set; }
        public string ProfileImagePath { get; set; }

        // Service offerings (for designers)
        public bool DoesPrintDesign { get; set; }
        public bool DoesDigitalDesign { get; set; }
        public bool DoesBrandingDesign { get; set; }
        public bool Does3dDesign { get; set; }

        // Collections for displaying projects
        public ICollection<PrintProjectViewModel> PrintProjects { get; set; }
        public ICollection<DigitalProjectViewModel> DigitalProjects { get; set; }
        public ICollection<BrandingProjectViewModel> BrandingProjects { get; set; }
        public ICollection<ThreeDProjectViewModel> ThreeDProjects { get; set; }

        // Statistics
        public int TotalProjects { get; set; }
        public double AverageRating { get; set; }
        public int TotalViews { get; set; }
    }
}


