using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Senior_Project_Graphic_Design_Portfolio.Models;
using Senior_Project_Graphic_Design_Portfolio.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Senior_Project_Graphic_Design_Portfolio.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<AccountController> _logger;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<AccountController> logger,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _roleManager = roleManager;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Ensure roles exist
                if (!await _roleManager.RoleExistsAsync("Designer"))
                {
                    await _roleManager.CreateAsync(new IdentityRole("Designer"));
                }
                if (!await _roleManager.RoleExistsAsync("Viewer"))
                {
                    await _roleManager.CreateAsync(new IdentityRole("Viewer"));
                }

                var user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Address = model.Address,
                    Website = model.Website,
                    Biography = model.AccountType == "Designer" ? model.Biography : null,
                    DoesPrintDesign = model.AccountType == "Designer" && model.DoesPrintDesign,
                    DoesBrandingDesign = model.AccountType == "Designer" && model.DoesBrandingDesign,
                    DoesDigitalDesign = model.AccountType == "Designer" && model.DoesDigitalDesign,
                    Does3dDesign = model.AccountType == "Designer" && model.Does3dDesign,
                    UserRole = model.AccountType
                };

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    // Add user to role
                    await _userManager.AddToRoleAsync(user, model.AccountType);

                    _logger.LogInformation($"User {user.Email} created successfully with role {model.AccountType}");

                    await _signInManager.SignInAsync(user, isPersistent: false);

                    if (model.AccountType == "Designer")
                    {
                        return RedirectToAction("Index", "Dashboard");
                    }
                    return RedirectToAction("Index", "Home");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                _logger.LogInformation("Attempting to log in user: {Email}", model.Email);

                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user == null)
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return View(model);
                }

                var result = await _signInManager.PasswordSignInAsync(
                    model.Email,
                    model.Password,
                    model.RememberMe,
                    lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    _logger.LogInformation("User logged in successfully: {Email}", model.Email);

                    if (user.UserRole == "Designer")
                    {
                        return RedirectToAction("Index", "Dashboard");
                    }
                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");
            return RedirectToAction("Index", "Home");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}