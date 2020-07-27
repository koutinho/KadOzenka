using System.Collections.Generic;
using Core.Shared.Extensions;

namespace KadOzenka.Dal.Spd
{
	public class SpdApplicationRequest
	{

		/// <summary>
		/// Профиль (xml с параметрами СПД процесса)
		/// </summary>
		public string ProfileName { get; set; }

		/// <summary>
		/// Справочник для хранения параметров
		/// </summary>
		public Dictionary<string, object> Params { get; set; }

		/// <summary>
		/// Атрибуты заявки в СПД сервис
		/// </summary>
		public SpdApplicationRequest()
		{
			Params = new Dictionary<string, object>();
		}

		/// <summary>
		/// Тип претенионного процесса (626, 630, 986 и др.)
		/// </summary>
		public int? ReglamentId
		{
			get
			{
				if (!Params.ContainsKey("ReglamentId")) return null;
				return Params["ReglamentId"].ParseToInt();
			}
			set
			{
				if (!Params.ContainsKey("ReglamentId")) Params.Add("ReglamentId", value);
				else Params["ReglamentId"] = value;
			}
		}

		/// <summary>
		/// Номер типа шаблона для СПД
		/// </summary>
		public int? ReportId
		{
			get
			{
				if (!Params.ContainsKey("ReportId")) return null;
				return Params["ReportId"].ParseToInt();
			}
			set
			{
				if (!Params.ContainsKey("ReportId")) Params.Add("ReportId", value);
				else Params["ReportId"] = value;
			}
		}

		/// <summary>
		/// Идентификатор номера заявки
		/// </summary>
		public int? SpdTypeNum
		{
			get
			{
				if (!Params.ContainsKey("SpdTypeNum")) return null;
				return Params["SpdTypeNum"].ParseToInt();
			}
			set
			{
				if (!Params.ContainsKey("SpdTypeNum")) Params.Add("SpdTypeNum", value);
				else Params["SpdTypeNum"] = value;
			}
		}
	}
}