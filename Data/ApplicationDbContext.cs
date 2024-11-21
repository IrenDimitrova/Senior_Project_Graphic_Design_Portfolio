using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Senior_Project_Graphic_Design_Portfolio.Models;

namespace Senior_Project_Graphic_Design_Portfolio.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
                : base(options)
        {
        }

        public DbSet<PrintProject> PrintProjects { get; set; } = null!;
        public DbSet<DigitalDesignProject> DigitalDesignProjects { get; set; } = null!;
        public DbSet<BrandingProject> BrandingProjects { get; set; } = null!;
        public DbSet<ThreeDProject> ThreeDProjects { get; set; } = null!;
        public DbSet<ProjectView> ProjectViews { get; set; } = null!;
        public DbSet<ProjectComment> ProjectComments { get; set; } = null!;
        public DbSet<ProjectRating> ProjectRatings { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ApplicationUser>(entity =>
            {
                entity.Property(e => e.FirstName).HasMaxLength(50);
                entity.Property(e => e.LastName).HasMaxLength(50);
                entity.Property(e => e.Address).HasMaxLength(255);
                entity.Property(e => e.Website).HasMaxLength(255);
            });

            builder.Entity<ProjectComment>()
            .Property(p => p.ProjectType)
            .IsRequired();

            builder.Entity<ProjectRating>()
                .Property(p => p.ProjectType)
                .IsRequired();
        }
    }

}