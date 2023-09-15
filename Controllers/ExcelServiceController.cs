using UserHelpPageTemplate.Models; // Assuming ApplicationUser and IdentityRole are defined in this namespace
using Microsoft.AspNetCore.Identity; // For UserManager and RoleManager
using Microsoft.AspNetCore.Mvc; // For IActionResult
using System.IO; // For Path.Combine and Directory
using System.Threading.Tasks; // For Task
using AppLogger;
using Business;
using UserHelpPageTemplate.Areas.Identity.Data;
using UserHelpPageTemplate.Business;
using UserHelpPageTemplate.Infrastructure.Alerts;

namespace UserHelpPageTemplate.Controllers
{
    // Controller for handling Excel service related actions
    public class ExcelServiceController : BaseController
    {
        // Inject UserManager and RoleManager for user and role management
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;

        // Constructor with dependencies injection
        public ExcelServiceController(IBiz biz, IExcelService excelService, IUserHelpPageLogger logger,
            UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
            : base(biz, excelService, logger)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        // Action to populate database from an Excel file
        public async Task<IActionResult> PopulateDatabase()
        {
            // Construct the file path
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "files", "MasterAppDescriptions.xlsx");

            // Call the ExcelService to populate database from the Excel file
            await ExcelService.PopulateDatabaseFromExcel(filePath);

            // Redirect to Index action of Home controller with success message
            return RedirectToAction("Index", "Home").WithSuccess("Import Data", "Data has been imported successfully!");
        }

        // Action to seed 4 base roles into the database (check enum)
        public async Task<IActionResult> SeedRoles()
        {
            // Call the ContextSeed class to seed roles using UserManager and RoleManager
            await ContextSeed.SeedRolesAsync(_userManager, _roleManager);
            await ContextSeed.SeedAdminAsync(_userManager, _roleManager);
            await ContextSeed.SeedSupervisorAsync(_userManager, _roleManager);
            await ContextSeed.SeedUserAsync(_userManager, _roleManager);


            // Optionally, redirect to Index action of Home controller with success message
            return RedirectToAction("Index", "Home").WithSuccess("Seed Data", "Data has been seeded successfully!");
        }
    }
}
