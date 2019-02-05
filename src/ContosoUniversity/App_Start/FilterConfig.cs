using System.Web;
using System.Web.Mvc;
using ContosoUniversity.Filters;

namespace ContosoUniversity
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
