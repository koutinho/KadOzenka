using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using Core.Register;

namespace KadOzenka.Web.Models.GbuObject
{
	public class PartialCharacteristicViewModel
	{
		/// <summary>
		/// Идентификатор атрибута, куда будет записан результат 
		/// </summary>
		[Display(Name = "Характеристика")]
		public long? IdAttributeResult { get; set; }

		/// <summary>
		/// Имя нового атрибута
		/// </summary>
		[Display(Name = "Имя характеристики")]
		public string NameNewAttribute { get; set; }

		/// <summary>
		/// Тип нового атрибута
		/// </summary>
		[Display(Name = "Тип")]
		public RegisterAttributeType? TypeNewAttribute { get; set; }

		/// <summary>
		/// Ид реестра куда добавим атрибут
		/// </summary>
		[Display(Name = "Реестр")]
		public long? RegistryId { get; set; }

		/// <summary>
		/// Флаг указывающий используем старый или новый атрибут
		/// </summary>
		public bool IsNewAttribute { get; set; } = false;

        public string SelectEventName { get; set; }


	}
}