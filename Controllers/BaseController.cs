using AppLogger;
using Business;
using Microsoft.AspNetCore.Mvc;
using UserHelpPageTemplate.Business;
using UserHelpPageTemplate.Infrastructure.Alerts;
namespace UserHelpPageTemplate.Controllers
{
    // Define a class named 'BaseController' that derives from 'ControllerBase'.
    public class BaseController : Controller
    {
        // Declare a private read-only field '_biz' of type 'IBiz'.
        private readonly IBiz _biz;
        private readonly IExcelService? _excelService;
        private readonly IUserHelpPageLogger? _logger;

        public BaseController(IBiz biz, IUserHelpPageLogger logger)
        {
            // Assign the 'biz' parameter to the '_biz' field.
            // The 'IBiz' instance will be provided through dependency injection by the ASP.NET Core framework.
            this._biz = biz;
            _logger = logger;
        }

        public BaseController(IBiz biz , IExcelService excelService , IUserHelpPageLogger logger)
        {
            // Assign the 'biz' parameter to the '_biz' field.
            // The 'IBiz' instance will be provided through dependency injection by the ASP.NET Core framework.
            this._biz = biz;
            this._excelService = excelService;
            _logger = logger;
        }


        // Define a protected property named 'Biz' that exposes the private '_biz' field to derived classes.
        // The 'protected' keyword makes this property accessible to derived classes, but not to other classes outside the inheritance hierarchy.
        protected IBiz Biz { get { return _biz; } }

        protected IExcelService ExcelService { get { return _excelService!; } }
        protected IUserHelpPageLogger Logger { get { return _logger!; } }

    }
}
