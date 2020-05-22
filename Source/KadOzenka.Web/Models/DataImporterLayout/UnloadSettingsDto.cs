﻿using ObjectModel.KO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KadOzenka.Web.Models.DataImporterLayout
{
	public class UnloadSettingsDto
	{
		[Required(ErrorMessage = "Заполните тур")]
		public long? IdTour { get; set; }

		[Required(ErrorMessage = "Заполните задание на оценку")]
		public List<long> TaskFilter { get; set; }
		public bool UnloadParcel { get; set; }
		public bool UnloadChange { get; set; }
		public bool UnloadHistory { get; set; }
		public bool UnloadTable04 { get; set; }
		public bool UnloadTable05 { get; set; }
		public bool UnloadTable07 { get; set; }
		public bool UnloadTable08 { get; set; }
		public bool UnloadTable09 { get; set; }
		public bool UnloadTable10 { get; set; }
		public bool UnloadTable11 { get; set; }
		public bool UnloadXML1 { get; set; }
		public bool UnloadXML2 { get; set; }

		/// <summary>
		/// Отправка результатов в РЕОН
		/// </summary>
		public bool SendResultToReon { get; set; }

		public static KOUnloadSettings Map(UnloadSettingsDto entity)
		{
			KOUnloadSettings result = new KOUnloadSettings
			{
				IdTour = entity.IdTour.GetValueOrDefault(),
				TaskFilter = entity.TaskFilter,
				UnloadParcel = entity.UnloadParcel,
				UnloadChange = entity.UnloadChange,
				UnloadHistory = entity.UnloadHistory,
				UnloadTable04 = entity.UnloadTable04,
				UnloadTable05 = entity.UnloadTable05,
				UnloadTable07 = entity.UnloadTable07,
				UnloadTable08 = entity.UnloadTable08,
				UnloadTable09 = entity.UnloadTable09,
				UnloadTable10 = entity.UnloadTable10,
				UnloadTable11 = entity.UnloadTable11,
				UnloadXML1 = entity.UnloadXML1,
				UnloadXML2 = entity.UnloadXML2,
				SendResultToReon = entity.SendResultToReon
			};

			return result;
		}
	}
}
