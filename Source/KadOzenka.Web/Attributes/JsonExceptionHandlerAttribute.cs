using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Npgsql;

namespace KadOzenka.Web.Attributes
{
    public class JsonExceptionHandlerAttribute : ExceptionFilterAttribute
    {
        public int StatusCode { get; set; } = 500;

        public MessageType Type { get; set; } = MessageType.Preset;

        public string Message { get; set; }

        public override void OnException(ExceptionContext context)
        {
            var isAjax = context.HttpContext.Request.Headers["X-Requested-With"] == "XMLHttpRequest";
            if (!isAjax) return;

            if (!string.IsNullOrWhiteSpace(Message))
            {
                Type = MessageType.Custom;
            }
            ObjectResult result;
            switch (Type)
            {
                case MessageType.Custom: result = new ObjectResult(Message);
                    break;
                case MessageType.Preset: result = ExceptionToMessage(context);
                    break;
                case MessageType.Default:
                default: result = new ObjectResult(context.Exception.Message);
                    break;
            }

            result.StatusCode = StatusCode;
            context.Result = result;
            context.ExceptionHandled = true;
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

                case "57014": message = "Выполнение запроса отменено по команде пользователя"; break;

                default: message = "Неизвестная ошибка"; break;
            }

            return new ObjectResult("[База данных] "+message);
        }

        private ObjectResult BasicExceptionToMessage(Exception ex)
        {
            string message = ex.Message;

            return new ObjectResult("[Ошибка] "+message);
        }

        public enum MessageType
        {
            /// <summary>
            /// Использовать описания ошибок, определённые в самом атрибуте
            /// </summary>
            Preset,
            /// <summary>
            /// Использовать сообщение из поля Message в выброшенном исключении
            /// </summary>
            Default,
            /// <summary>
            /// Использовать параметр Message атрибута
            /// </summary>
            Custom
        }
    }
}