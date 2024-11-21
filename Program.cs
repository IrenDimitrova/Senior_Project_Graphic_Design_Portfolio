using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Senior_Project_Graphic_Design_Portfolio.Data;
using Senior_Project_Graphic_Design_Portfolio.Models;

namespace Senior_Project_Graphic_Design_Portfolio
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseMySql(
                    connectionString,
                    ServerVersion.AutoDetect(connectionString)
                )
            );

            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 6;
            })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            // Add MVC services
            builder.Services.AddControllersWithViews();
            builder.Services.AddRazorPages();

            builder.Services.AddScoped<RoleManager<IdentityRole>>();
            builder.Services.AddScoped<ApplicationDbContext>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            // Add this block for database initialization
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var logger = services.GetRequiredService<ILogger<Program>>();

                try
                {
                    var context = services.GetRequiredService<ApplicationDbContext>();
                    var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
                    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

                    // Ensure database is created
                    context.Database.EnsureCreated();

                    // First delete existing admin if exists (for testing purposes)
                    var existingAdmin = await userManager.FindByEmailAsync("admin@portfolio.com");
                    if (existingAdmin != null)
                    {
                        await userManager.DeleteAsync(existingAdmin);
                    }

                    // Create Admin role if it doesn't exist
                    if (!await roleManager.RoleExistsAsync("Admin"))
                    {
                        await roleManager.CreateAsync(new IdentityRole("Admin"));
                        logger.LogInformation("Created Admin role");
                    }

                    // Create admin user
                    var adminUser = new ApplicationUser
                    {
                        UserName = "admin@portfolio.com",
                        Email = "admin@portfolio.com",
                        EmailConfirmed = true,
                        FirstName = "Admin",
                        LastName = "User",
                        UserRole = "Admin"
                    };

                    // Remove the user if it exists and create it again
                    var user = await userManager.FindByEmailAsync(adminUser.Email);
                    if (user != null)
                    {
                        await userManager.DeleteAsync(user);
                    }

                    var result = await userManager.CreateAsync(adminUser, "Admin123!");

                    if (result.Succeeded)
                    {
                        logger.LogInformation("Admin user created successfully");

                        // Ensure admin is in admin role
                        if (!await userManager.IsInRoleAsync(adminUser, "Admin"))
                        {
                            var roleResult = await userManager.AddToRoleAsync(adminUser, "Admin");
                            if (roleResult.Succeeded)
                            {
                                logger.LogInformation("Added admin to Admin role");
                            }
                            else
                            {
                                logger.LogError($"Failed to add admin to role: {string.Join(", ", roleResult.Errors)}");
                            }
                        }
                    }
                    else
                    {
                        logger.LogError($"Failed to create admin user: {string.Join(", ", result.Errors)}");
                    }

                    // Other roles
                    var otherRoles = new[] { "Designer", "Viewer" };
                    foreach (var roleName in otherRoles)
                    {
                        if (!await roleManager.RoleExistsAsync(roleName))
                        {
                            await roleManager.CreateAsync(new IdentityRole(roleName));
                            logger.LogInformation($"Created role: {roleName}");
                        }
                    }
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "An error occurred while seeding the database");
                    throw;
                }
            }

            app.UseStaticFiles();
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            // Add areas routing first
            app.MapControllerRoute(
                name: "areas",
                pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}");

            // Then default routing
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.MapRazorPages();

            await app.RunAsync();
        }
    }
}