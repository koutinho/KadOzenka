using System;
using System.Collections.Generic;
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

            // Если есть обычный объект исключения, то приоритет на него даже при наличии сообщения
            if (!string.IsNullOrWhiteSpace(Message) && !(context.Exception.GetType() == typeof(Exception)))
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
            return context.Exception switch
            {
                PostgresException ex => PostgresExceptionToMessage(ex),
                { } ex => BasicExceptionToMessage(ex),
                _ => new ObjectResult("Неизвестная ошибка")
            };
        }

        private ObjectResult PostgresExceptionToMessage(PostgresException ex)
        {
            var message = ex.SqlState switch
            {
                "23505" => "Нарушение ограничения уникальности записи",
                "42501" => "Нет доступа к таблице",
                "42601" => "Ошибка синтаксиса запроса",
                "53000" => "Недостаточно ресурсов",
                "53100" => "Диск заполнен",
                "53200" => "Недостаточно памяти",
                "53300" => "Лимит соединений исчерпан",
                "57014" => "Выполнение запроса отменено по команде пользователя",
                _ => "Неизвестная ошибка"
            };

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