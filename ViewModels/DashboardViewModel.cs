namespace Senior_Project_Graphic_Design_Portfolio.ViewModels
{
    public class DashboardViewModel
    {
        public DashboardViewModel()
        {
            ProjectStats = new List<ProjectStatsViewModel>();
        }

        public List<ProjectStatsViewModel> ProjectStats { get; set; }
        public string UserId { get; set; } = string.Empty;
        public int TotalUsers { get; set; }
        public int TotalDesigners { get; set; }
        public int TotalViewers { get; set; }
        public int Users { get; set; }
    }

    public class ProjectStatsViewModel
    {
        public string Category { get; set; } = string.Empty;
        public int Count { get; set; }
        public int Views { get; set; }
        public double Rating { get; set; }
    }
}