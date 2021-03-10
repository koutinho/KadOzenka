using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using KadOzenka.Dal.LongProcess.Reports.AdditionalForms.MarketDataInfo.Entities;
using Kendo.Mvc.UI;

namespace KadOzenka.Web.Models.ManagementDecisionSupport.AdditionalFormViewModel
{
	public class MarketDataInfoReportViewModel
	{
		public List<long> TaskIds { get; set; }

		[Required(ErrorMessage = @"Дата ""от"" обязательный параметр")]
		[Display(Name = "Дата от")]
		public DateTime? DateFrom { get; set; }

		[Required(ErrorMessage = @"Дата ""до"" обязательный параметр")]
		[Display(Name = "Дата до")]
		public DateTime? DateTo { get; set; }

		[Required(ErrorMessage = @"""Код вида использования"" обязательный параметр")]
		[Display(Name = "Код вида использования")]
		public long? TypeOfUseCodeAttributeId { get; set; }

		[Required(ErrorMessage = @"""Группа ОКС"" обязательный параметр")]
		[Display(Name = "Группа ОКС")]
		public long? OksGroupAttributeId { get; set; }

		[Required(ErrorMessage = @"""Вид использования (функциональное назначение)"" обязательный параметр")]
		[Display(Name = "Вид использования (функциональное назначение)")]
		public long? TypeOfUseAttributeId { get; set; }

		public List<DropDownTreeItemModel> GbuAttributes { get; set; }


		public ReportInputParams MapToInputParameters()
		{
			return new ReportInputParams
			{
				DateFrom = DateFrom,
				DateTo = DateTo,
				TypeOfUseCodeAttributeId = TypeOfUseCodeAttributeId.GetValueOrDefault(),
				OksGroupAttributeId = OksGroupAttributeId.GetValueOrDefault(),
				TypeOfUseAttributeId = TypeOfUseAttributeId.GetValueOrDefault()
			};
		}
	}
}