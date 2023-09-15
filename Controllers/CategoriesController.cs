using AppLogger;
using Business;
using DocumentFormat.OpenXml.Office2010.Excel;
using Enums;
using Humanizer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserHelpPageTemplate.Infrastructure.Alerts;
using ViewModels;

namespace UserHelpPageTemplate.Controllers
{
    [Authorize(Roles = "Admin, Supervisor")]
    public class CategoriesController : BaseController
    {
        public CategoriesController(IBiz biz, IUserHelpPageLogger logger) : base(biz, logger) { }

        public IActionResult Index()
        {
            return View();
        }
        //Action method to fetch categories all categories in the table
        public async Task<IActionResult> CategoriesTable()
        {
            try
            {
                Logger.LogMessage(LogLevel.Error, "Application", "Create", "Failed to create application", "AppName", "test");

                var allApplications = await Biz.GetCategories();
                return View(allApplications);
            }
            catch (Exception ex)
            {

                //Log message and exception
                if (ex is AppException)
                {
                    return RedirectToAction(nameof(Index)).WithError("Getting Category Details", ex.Message);
                }
                else
                {
                    return RedirectToAction(nameof(Index)).WithError("Getting Category Details", "Unexpected error occurred!");
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
        public async Task<IActionResult> Create(CategoryVM categoryVM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // Use the Biz class to create a new application
                    await Biz.CreateCategory(categoryVM);
                    return RedirectToAction("CategoriesTable", "Categories").WithSuccess("Create Data", "Data has been added successfully!");
                }
                else
                {
                    return RedirectToAction("CategoriesTable", "Categories").WithError("Import Data", "Something went wrong while creating the application!");
                    //Add log 
                }
            }
            catch (Exception ex)
            {
                // Log message and exception
                return RedirectToAction("CategoriesTable", "Categories").WithError("Error creating application", ex.Message);
            }
        }

        // GET: ApplicationController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var app = await Biz.GetCategoryById(id);
                return View(app);
            }
            catch (Exception ex)
            {
                // Log message and exception
                return RedirectToAction("CategoriesTable", "Categories").WithError("Error retrieving application for editing", ex.Message);
            }
        }

        // POST: ApplicationController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CategoryVM categoryVM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // Use the Biz class to update the application

                    await Biz.UpdateCategory(categoryVM);
                    return RedirectToAction("CategoriesTable", "Categories").WithSuccess("Update Data", "Data has been updated successfully!");
                }

                return View(categoryVM);
            }
            catch (Exception ex)
            {
                // Log message and exception
                return RedirectToAction("CategoriesTable", "Categories").WithError("Error updating application", ex.Message);
            }
        }
        [Authorize(Roles = "Supervisor")]
        // GET: ApplicationController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                // Use the Biz class to get the application for deletion confirmation
                var app = await Biz.GetCategoryById(id);
                return View(app);
            }
            catch (Exception ex)
            {
                // Log message and exception
                return RedirectToAction("CategoriesTable", "Categories").WithError("Error retrieving application for deletion", ex.Message);
            }
        }

        // POST: ApplicationController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, CategoryVM categoryVM)
        {
            try
            {
                // Use the Biz class to delete the application
                if (await Biz.DeleteCategory(id) > 0)
                {
                    return RedirectToAction("CategoriesTable", "Categories").WithSuccess("Delete Data", "Data has been removed successfully!");
                }
                else
                {
                    return RedirectToAction("CategoriesTable", "Categories").WithError("Import Data", "Something went wrong while deleting the application!");
                    //Add log 
                }

            }
            catch (Exception ex)
            {
                if (ex is AppException)
                {
                    // Log message and exception
                    return RedirectToAction("CategoriesTable", "Categories").WithError("Error deleting application", ex.Message);
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
