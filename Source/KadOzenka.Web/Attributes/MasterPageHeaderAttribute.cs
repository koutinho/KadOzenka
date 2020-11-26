using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace KadOzenka.Web.Attributes
{
    public class MasterPageHeaderAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            Controller c = context.Controller as Controller;
            var q = c?.Request.Query;

            if (q!=null && q["useMasterPage"] == true)
            {
                c.ViewBag.UseMasterPage = true;
            }
            base.OnActionExecuting(context);
        }
    }
}