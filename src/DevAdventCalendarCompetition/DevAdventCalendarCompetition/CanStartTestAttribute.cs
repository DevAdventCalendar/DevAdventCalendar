using Microsoft.AspNetCore.Mvc.Filters;

namespace DevAdventCalendarCompetition
{
    public class CanStartTestAttribute : ActionFilterAttribute
    {
        public int TestNumber { get; set; }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            // TODO refactor
            //var context = new ApplicationDbContext();

            //var test = context.Test.First(el => el.Number == TestNumber);
            //if (test.Status != TestStatus.Started)
            //{
            //    var url = test.Status == TestStatus.NotStarted
            //        ? "/Home/TestHasNotStarted"
            //        : "/Home/TestHasEnded";
            //    url += "?number=" + test.Number;

            //    filterContext.Result = new RedirectResult(url);
            //    return;
            //}

            //var userId =  HttpContext.Current.User.Identity.GetUserId();
            //var testAnswer = context.Set<TestAnswer>()
            //    .FirstOrDefault(el => el.UserId == userId && el.TestId == test.Id);

            //if (testAnswer != null)
            //{
            //    var url = "/Home/TestAnswered?number=" + test.Number;
            //    filterContext.Result = new RedirectResult(url);
            //}
        }
    }
}