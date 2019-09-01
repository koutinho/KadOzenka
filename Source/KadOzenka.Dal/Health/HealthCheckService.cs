using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using Core.Messages;
using Core.Shared.Extensions;
using DynamicExpresso;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Platform.Main.ConfigParam.HealthCheck;

namespace CIPJS.DAL.Health
{
    public class HealthCheckService
    {
        private readonly Interpreter interp;
        private readonly HealthCheck config;
        private Dictionary<string, object> variables;

        public HealthCheckService(HealthCheck config)
        {
            this.config = config;
            this.interp = new Interpreter();
        }

		/// <summary>
		/// True - если все хорошо
		/// </summary>
		/// <returns></returns>
		public bool Start()
        {
            if (!NeedStartCheck())
                return true;

            switch (this.config.Type)
            {
                case HealthCheckType.SQL:
                    StartSQLCheck();
                    break;
                default:
                    throw new NotImplementedException();
            }

            return CheckResult();
        }

		/// <summary>
		/// True - если все хорошо
		/// </summary>
		/// <returns></returns>
        private bool CheckResult()
        {
            bool error = this.interp.Eval<bool>(this.config.CheckCondition, CreateCheckConditionParameters());
            if (error)
                SendMessage();

			return !error;
		}

        private Parameter[] CreateCheckConditionParameters()
        {
            if (this.variables.Count == 0)
                return new Parameter[0];
            else
                return this.variables.Select(x => new Parameter(x.Key, x.Value.GetType(), x.Value)).ToArray();
        }

        private void StartSQLCheck()
        {
            using (var rdr = DBMngr.Realty.ExecuteReader(CommandType.Text, this.config.SQL))
            {
                if (rdr.Read())
                {
                    this.variables = new Dictionary<string, object>(rdr.FieldCount);
                    for (int i = 0; i < rdr.FieldCount; i++)
                        this.variables.Add(rdr.GetName(i), rdr.GetValue(i));
                }
                else
                {
                    throw new Exception("sql запрос вернул 0 строк: " + this.config.SQL);
                }
            }
        }

        private void SendMessage()
        {
            var msgConfig = this.config.Message;
            var messageSvc = new MessageService();
            var messages = new MessageDto
            {
                IsEmail = msgConfig.SendEmail,
                UserIds = msgConfig.To,
                Subject = FormatString(msgConfig.Subject, this.variables),
                Message = FormatString(msgConfig.Body, this.variables)
            };
            messageSvc.SendMessages(messages);
        }

        private bool NeedStartCheck() => this.config.StartCondition.IsNullOrEmpty() || this.interp.Eval<bool>(this.config.StartCondition);

        private static string FormatString(string format, Dictionary<string, object> vars)
        {
            if (format == null)
                throw new ArgumentNullException(nameof(format));
            if (vars.Count == 0 || !format.Contains("{"))
                return format;
 
            Regex r = new Regex(@"(?<start>\{)+(?<property>[\w\.\[\]]+)(?<format>:[^}]+)?(?<end>\})+", RegexOptions.CultureInvariant | RegexOptions.IgnoreCase);
            List<object> values = new List<object>();
            string rewrittenFormat = r.Replace(format, m =>
            {
                Group startGroup = m.Groups["start"];
                Group propertyGroup = m.Groups["property"];
                Group formatGroup = m.Groups["format"];
                Group endGroup = m.Groups["end"];
                values.Add(vars[propertyGroup.Value]);
                return new string('{', startGroup.Captures.Count) + (values.Count - 1) + formatGroup.Value + new string('}', endGroup.Captures.Count);
            });
            return string.Format(rewrittenFormat, values.ToArray());
        }
    }
}
