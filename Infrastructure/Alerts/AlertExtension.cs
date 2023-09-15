using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UserHelpPageTemplate.Infrastructure.Alerts
{
    public static class AlertExtension
    {
        const string Alerts = "_Alerts";
        const string TestObject2 = "_Test2";

        public static List<Alert> GetAlerts(this TempDataDictionary TempData)
        {
            if (!TempData.ContainsKey(Alerts))
            {
                TempData[Alerts] = new List<Alert>();
            }
            TempData.Keep(Alerts);
            return (List<Alert>)TempData[Alerts];
        }
        public static IActionResult WithSuccess(this IActionResult result, string title, string body)
        {
            return Alert(result, "success", title, body);
        }

        public static IActionResult WithInfo(this IActionResult result, string title, string body)
        {
            return Alert(result, "info", title, body);
        }

        public static IActionResult WithWarning(this IActionResult result, string title, string body)
        {
            return Alert(result, "warning", title, body);
        }

        public static IActionResult WithDanger(this IActionResult result, string title, string body)
        {
            return Alert(result, "danger", title, body);
        }
        private static IActionResult Alert(IActionResult result, string type, string title, string body)
        {
            return new AlertDecoratorResult(result, type, title, body);
        }
        //public static ActionResult WithSuccess(this ActionResult  result, string message)
        //{
        //    return new AlertDecoratorResult(result, "alert-success", message);
        //}
        //public static ActionResult WithInfo(this ActionResult result, string message)
        //{
        //    return new AlertDecoratorResult(result, "alert-info", message);
        //}
        //public static ActionResult WithWarning(this ActionResult result, string message)
        //{
        //    return new AlertDecoratorResult(result, "alert-warning", message);
        //}
        public static IActionResult WithError(this IActionResult result, string title, string body)
        {
            return Alert(result, "danger", title, body);
        }
    }
}