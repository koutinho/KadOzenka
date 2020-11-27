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

            if (q!=null)
            {
                var val = q["useMasterPage"];
                var masterPage = val.ToString() == "true";
                c.ViewBag.UseMasterPage = masterPage;
            }
            base.OnActionExecuting(context);
        }
    }
}