using Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserHelpPageTemplate.Areas.Identity.Data;
using ViewModels;

namespace UserHelpPageTemplate.Controllers
{
    [Authorize(Roles = "Supervisor")]
    public class RoleManagerController : Controller
    {     
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public RoleManagerController(RoleManager<ApplicationRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            return View(roles);
        }
        [HttpPost]
        public async Task<IActionResult> AddRole(string roleName)
        {

            if (roleName != null)
            {
                var user = await _userManager.GetUserAsync(User);

                var userInput = new ApplicationRole(roleName.Trim())
                {
                    RoleName = roleName.Trim(),
                    Cloak = false,
                    CreatedBy = user.UserName,        // Use the username of the logged-in user
                    CreatedOn = DateTime.UtcNow,
                    UpdatedBy = user.UserName,        // Use the username of the logged-in user
                    UpdatedOn = DateTime.UtcNow
                };
                await _roleManager.CreateAsync(userInput);
                
            }
            return RedirectToAction("Index");
        }
    }
}
