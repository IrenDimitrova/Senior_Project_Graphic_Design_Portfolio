using Microsoft.AspNetCore.Mvc;

namespace Senior_Project_Graphic_Design_Portfolio.Areas.Admin
{
    public class AdminAreaAttribute : AreaAttribute
    {
        public AdminAreaAttribute() : base("Admin") { }
    }
}