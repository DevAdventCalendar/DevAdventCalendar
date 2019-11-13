using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DevAdventCalendarCompetition.Controllers
{
    public class SubscriptionController : Controller
    {
        public ActionResult Success()
        {
            return this.View();
        }

        public ActionResult Confirmed()
        {
            return this.View();
        }

        public ActionResult Duplicated()
        {
            return this.View();
        }

        public ActionResult GDPRInvalid()
        {
            return this.View();
        }

        public ActionResult Removed()
        {
            return this.View();
        }
    }
}