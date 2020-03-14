using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
namespace HuskySite.Views.Bookkeeping
{
    public static class BookkeepingNavPages
    {
        public static string ActivePageKey => "ActivePage";
        public static string Index => "Index";
        public static string IndexNavClass(ViewContext viewContext) => PageNavClass(viewContext, Index);

        public static string Wallet => "Wallet";
        public static string WalletNavClass(ViewContext viewContext) => PageNavClass(viewContext, Wallet);

        public static string Accounting => "Accounting";
        public static string AccountingNavClass(ViewContext viewContext) => PageNavClass(viewContext, Accounting);

        public static string Account => "Account";
        public static string AccountNavClass(ViewContext viewContext) => PageNavClass(viewContext, Account);

        public static string PatternSMS => "PatternSMS";
        public static string PatternSMSNavClass(ViewContext viewContext) => PageNavClass(viewContext, PatternSMS);

        public static string OperationType => "OperationType";
        public static string OperationTypeNavClass(ViewContext viewContext) => PageNavClass(viewContext, OperationType);

        public static string PageNavClass(ViewContext viewContext, string page)
        {
            var activePage = viewContext.ViewData["ActivePage"] as string;
            return string.Equals(activePage, page, StringComparison.OrdinalIgnoreCase) ? "active" : null;
        }
        public static void AddActivePage(this ViewDataDictionary viewData, string activePage) => viewData[ActivePageKey] = activePage;

    }

}
