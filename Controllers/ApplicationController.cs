using AppLogger;
using Business;
using Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using UserHelpPageTemplate.Infrastructure.Alerts;
using ViewModels;

namespace UserHelpPageTemplate.Controllers
{
    [Authorize(Roles = "Admin, Supervisor")]
    public class ApplicationController : BaseController
    {
        public ApplicationController(IBiz biz, IUserHelpPageLogger logger) : base(biz, logger) { }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> AppTable()
        {
            var allApplications = await Biz.GetApps();


            return View(allApplications);
        }

        // GET: CRUDOperations    

        // GET: ApplicationController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var app = await Biz.GetAppById(id);
                return View(app);
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

        // GET: ApplicationController/Create
        [HttpGet]
        public IActionResult Create()
        {
            return PartialView();
        }


        
        // POST: ApplicationController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ApplicationVM appVM)
        {
            try
            {
                
                if (ModelState.IsValid)
                {
                    // Use the Biz class to create a new application
                    await Biz.CreateApp(appVM);
                    return RedirectToAction("AppTable", "Application").WithSuccess("Create Data", "Data has been added successfully!");
                }
                else
                {
                    return View(appVM).WithError("Create Application", "Please fill up all the required field!");
                }
            }
            catch (Exception ex)
            {
                Logger.LogMessage(LogLevel.Error, "Application", "Create", "Failed to create application", "AppName", appVM.Name, ex);
                return RedirectToAction("AppTable", "Application").WithError("Error creating application", ex.Message);
            }
        }

        
        // GET: ApplicationController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var app = await Biz.GetAppById(id);
                return View(app);
            }
            catch (Exception ex)
            {
                // Log message and exception
                return RedirectToAction("AppTable", "Application").WithError("Error retrieving application for editing", ex.Message);
            }
        }

        // POST: ApplicationController/Edit/5
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ApplicationVM appVM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // Use the Biz class to update the application

                    await Biz.UpdateApp(appVM);
                    return RedirectToAction("AppTable", "Application").WithSuccess("Update Data", "Data has been updated successfully!");
                }

                return View(appVM);
            }
            catch (Exception ex)
            {
                // Log message and exception
                return RedirectToAction("AppTable", "Application").WithError("Error updating application", ex.Message);
            }
        }

        [Authorize(Roles = "Supervisor")]
        // GET: ApplicationController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                // Use the Biz class to get the application for deletion confirmation
                var app = await Biz.GetAppById(id);
                return View(app);
            }
            catch (Exception ex)
            {
                // Log message and exception
                return RedirectToAction("AppTable", "Application").WithError("Error retrieving application for deletion", ex.Message);
            }
        }

        // POST: ApplicationController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, ApplicationVM appVM)
        {
            try
            {
                // Use the Biz class to delete the application
                if (await Biz.DeleteApp(id) > 0)
                {
                    return RedirectToAction("AppTable", "Application").WithSuccess("Delete Data", "Data has been removed successfully!");
                }
                else
                {
                    return RedirectToAction("AppTable", "Application").WithError("Import Data", "Something went wrong while deleting the application!");
                    //Add log 
                }

            }
            catch (Exception ex)
            {
                if (ex is AppException)
                {
                    // Log message and exception
                    return RedirectToAction("AppTable", "Application").WithError("Error deleting application", ex.Message);
                }
                else
                {
                    return View("Error");
                    //log exceptions
                }
            }
        }
    }
}
