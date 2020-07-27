using Core.SRD;
using Microsoft.AspNetCore.Mvc.Filters;

namespace KadOzenka.Web.Attributes
{
    public class SRDFunctionAttribute : ActionFilterAttribute
    {
        public string Tag { get; set; }

        /// <summary>
        /// Default true
        /// </summary>
        public bool ExceptionOnAccessDenied { get; set; } = true;

        /// <summary>
        /// Default false
        /// </summary>
        public bool LogOnSuccess { get; set; } = false;

        /// <summary>
        /// Default true
        /// </summary>
        public bool LogOnAccessDenied { get; set; } = true;

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (Tag != "")
            {
                SRDSession.Current.CheckAccessToFunction(Tag, ExceptionOnAccessDenied, LogOnSuccess, LogOnAccessDenied);
            }

            base.OnActionExecuting(filterContext);
        }
    }
}
