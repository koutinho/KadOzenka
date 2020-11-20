using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Npgsql;

namespace KadOzenka.Web.Attributes
{
    public class JsonExceptionHandlerAttribute : ExceptionFilterAttribute
    {
        public int StatusCode { get; set; } = 500;

        public override void OnException(ExceptionContext context)
        {
            var result = ExceptionToMessage(context);

            result.StatusCode = StatusCode;
            context.Result = result;
        }

        private ObjectResult ExceptionToMessage(ExceptionContext context)
        {
            switch (context.Exception)
            {
                case PostgresException ex: return PostgresExceptionToMessage(ex);
                case Exception ex: return BasicExceptionToMessage(ex);
                default: return new ObjectResult("Неизвестная ошибка");
            }
        }

        private ObjectResult PostgresExceptionToMessage(PostgresException ex)
        {
            string message;
            switch (ex.SqlState)
            {
                case "23505": message = "Нарушение ограничения уникальности записи"; break;

                case "42501": message = "Нет доступа к таблице"; break;
                case "42601": message = "Ошибка синтаксиса запроса"; break;

                case "53000": message = "Недостаточно ресурсов"; break;
                case "53100": message = "Диск заполнен"; break;
                case "53200": message = "Недостаточно памяти"; break;
                case "53300": message = "Лимит соединений исчерпан"; break;

                default: message = "Неизвестная ошибка"; break;
            }

            return new ObjectResult("[База данных] "+message);
        }

        private ObjectResult BasicExceptionToMessage(Exception ex)
        {
            string message = ex.Message;

            return new ObjectResult("[Ошибка] "+message);
        }
    }
}