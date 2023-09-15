using AppLogger;
using Business;
using Microsoft.AspNetCore.Mvc;
using UserHelpPageTemplate.Business;
using UserHelpPageTemplate.Infrastructure.Alerts;
using ViewModels;

namespace UserHelpPageTemplate.Controllers
{
    public class AppGuidesController : BaseController
    {
        public AppGuidesController(IBiz biz, IExcelService excel, IUserHelpPageLogger logger):base(biz,excel, logger)
        {                
        }

        public IActionResult Index()
        {         
            return View();
        }

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

    }
}

