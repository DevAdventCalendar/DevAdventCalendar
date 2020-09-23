using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DevAdventCalendarCompetition.Controllers
{
    [Route("[controller]/[action]")]
    public class SubscriptionController : Controller
    {
        [HttpGet]
        public ActionResult Success()
        {
            return this.View();
        }

        [HttpGet]
        public ActionResult Confirmed()
        {
            return this.View();
        }

        [HttpGet]
        public ActionResult Duplicated()
        {
            return this.View();
        }

        [HttpGet]
        public ActionResult GDPRInvalid()
        {
            return this.View();
        }

        [HttpGet]
        public ActionResult Removed()
        {
            return this.View();
        }
    }
}