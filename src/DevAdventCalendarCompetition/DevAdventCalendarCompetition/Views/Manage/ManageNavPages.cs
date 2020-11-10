using System;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace DevAdventCalendarCompetition.Views.Manage
{
    public static class ManageNavPages
    {
        public static string ActivePageKey => "ActivePage";

        public static string Index => "Index";

        public static string ChangePassword => "ChangePassword";

        public static string GoogleCalendarIntegration => "GoogleCalendarIntegration";

        public static string DisplayStatistics => "DisplayStatistics";

        public static string IndexNavClass(ViewContext viewContext) => PageNavClass(viewContext, Index);

        public static string ChangePasswordNavClass(ViewContext viewContext) => PageNavClass(viewContext, ChangePassword);

        public static string GoogleCalendarIntegrationNavClass(ViewContext viewContext) => PageNavClass(viewContext, GoogleCalendarIntegration);

        public static string PlayerStatisticsNavClass(ViewContext viewContext) => PageNavClass(viewContext, DisplayStatistics);

        public static string PageNavClass(ViewContext viewContext, string page)
        {
            if (viewContext is null)
            {
                throw new ArgumentNullException(nameof(viewContext));
            }

            var activePage = viewContext.ViewData["ActivePage"] as string;
            return string.Equals(activePage, page, StringComparison.OrdinalIgnoreCase) ? "active" : null;
        }

        public static void AddActivePage(this ViewDataDictionary viewData, string activePage)
        {
            if (viewData == null)
            {
                throw new ArgumentNullException(nameof(viewData));
            }

            if (viewData != null)
            {
                viewData[ActivePageKey] = activePage;
            }
        }
    }
}