using Core.Shared.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Npgsql;

namespace KadOzenka.Web.Attributes
{
    public class JsonExceptionOverwriteAttribute : ExceptionFilterAttribute
    {
        public int StatusCode { get; set; } = 500;

        public string ExceptionType { get; set; }

        public string ExceptionCode { get; set; }

        public string Message { get; set; }

        public override void OnException(ExceptionContext context)
        {
            var isAjax = context.HttpContext.Request.Headers["X-Requested-With"] == "XMLHttpRequest";
            if (!isAjax) return;

            var type = context.Exception.GetType();

            if (ExceptionType != type.Name) return;

            if (!ExceptionCode.IsNullOrEmpty())
            {
                if (CheckCode(context))
                {
                    HandleException(context);
                }
            }
            else
            {
                HandleException(context);
            }
        }
        private void HandleException(ExceptionContext context)
        {
            var result = new ObjectResult(Message) {StatusCode = StatusCode};
            context.Result = result;
            context.ExceptionHandled = true;
        }

        private bool CheckCode(ExceptionContext context)
        {
            return context.Exception switch
            {
                PostgresException pgException => pgException.SqlState == ExceptionCode,
                _ => false
            };
        }
    }
}