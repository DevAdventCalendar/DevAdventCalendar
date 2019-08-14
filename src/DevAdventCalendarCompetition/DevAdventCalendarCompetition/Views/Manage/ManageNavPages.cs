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

        public static string IndexNavClass(ViewContext viewContext) => PageNavClass(viewContext, Index);

        public static string ChangePasswordNavClass(ViewContext viewContext) => PageNavClass(viewContext, ChangePassword);

        public static string PageNavClass(ViewContext viewContext, string page)
        {
#pragma warning disable CA1062 // Validate arguments of public methods
            var activePage = viewContext.ViewData["ActivePage"] as string;
#pragma warning restore CA1062 // Validate arguments of public methods
            return string.Equals(activePage, page, StringComparison.OrdinalIgnoreCase) ? "active" : null;
        }

        public static void AddActivePage(this ViewDataDictionary viewData, string activePage)
        {
#pragma warning disable CA1062 // Validate arguments of public methods
            viewData[ActivePageKey] = activePage;
#pragma warning restore CA1062 // Validate arguments of public methods
        }
    }
}