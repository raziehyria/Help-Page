using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UserHelpPageTemplate.Infrastructure.Alerts
{
    public class Alert
    {
        public string AlertCategory { get; set; }
        public string AlertMessage { get; set; }

        public Alert(string alertCategory, string alertMessage)
        {
            AlertCategory = alertCategory;
            AlertMessage = alertMessage;
        }
    }
}
