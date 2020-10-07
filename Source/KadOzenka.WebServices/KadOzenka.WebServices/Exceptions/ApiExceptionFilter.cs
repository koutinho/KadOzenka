using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace KadOzenka.WebServices.Exceptions
{
	public class ApiExceptionFilter : ExceptionFilterAttribute
	{
		public override void OnException(ExceptionContext context)
		{
			if (context.Exception is NotFoundException)
			{
				SetExceptionContext(context, HttpStatusCode.NotFound);
			}
			else if (context.Exception is BadRequestException)
			{
				SetExceptionContext(context, HttpStatusCode.BadRequest);
			}
			else
			{
				SetExceptionContext(context, HttpStatusCode.InternalServerError);
			}

			base.OnException(context);
        }

		private static void SetExceptionContext(ExceptionContext context, HttpStatusCode httpStatusCode)
		{
			context.Result = new ContentResult
			{
				Content = context.Exception.Message
			};
			context.HttpContext.Response.StatusCode = (int) httpStatusCode;
			context.ExceptionHandled = true;
		}
	}
}
