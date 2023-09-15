using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using UserHelpPageTemplate.Models;
using Business;
using DocumentFormat.OpenXml.Office2010.Excel;
using UserHelpPageTemplate.Infrastructure.Alerts;
using ViewModels;
using AppLogger;

namespace UserHelpPageTemplate.Controllers
{
    public class HomeController : BaseController
    {


        public HomeController(IBiz biz, IUserHelpPageLogger logger) : base(biz, logger) 
        {

        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AppGuides()
        {
            return View();
        }

        public IActionResult FAQ()
        {
            return View();
        }
        public IActionResult About()
        {
            return View();
        }
        public async Task<IActionResult> AppsByCategory(int category)
        {
            // Retrieve the list of apps with the selected category from the database
            // and pass it to the view.
            // You can use Entity Framework or any other data access method to retrieve the apps.
            // For simplicity, let's assume you have a list of apps to display.          
            try
            {
                var selectedCategory = await Biz.GetCategoryById(category);
                ViewBag.SelectedCategoryName = selectedCategory.Name;
                
                var apps = await Biz.GetAppsByCategory(category);
                ViewBag.Apps = apps;
               
                return View(apps);
            }
            catch (Exception ex)
            {

                //Log message and exception
                if (ex is AppException)
                {
                    return RedirectToAction(nameof(Index)).WithError("Getting Application Details", ex.Message);
                }
                else
                {
                    return RedirectToAction(nameof(Index)).WithError("Getting Application Details", "Unexpected error occurred!");
                }

            }
        }


        [HttpGet]
        public async Task<IActionResult> Search(string query)
        {
            var results = await Biz.SearchApps(query);
            return PartialView("_SearchResults", results);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}