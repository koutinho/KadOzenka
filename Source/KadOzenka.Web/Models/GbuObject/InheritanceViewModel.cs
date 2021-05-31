﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using KadOzenka.Dal.Enum;
using KadOzenka.Dal.GbuObject.Dto;

namespace KadOzenka.Web.Models.GbuObject
{
	public class PartialAttribute
	{
		public long Attributes { get; set; }
	}

	public class AttributeMapping
	{
		public long IdFrom { get; set; }
		public long IdTo { get; set; }
	}

	public class InheritanceViewModel : IValidatableObject
	{
		public long RatingTour { get; set; }

		/// <summary>
		/// Список заданий на оценку
		/// </summary>
		[Required(ErrorMessage = "Задание на оценку обязательное поле")]
		[Display(Name = "Задание на оценку")]
		public List<long> TaskFilter { get; set; }

		[Display(Name = "Статус")]
		public List<ObjectChangeStatus> ObjectChangeStatus { get; set; }

		/// <summary>
		/// Тип наследования: Кадастровый квартал -> Земельный участок
		/// </summary>
		public bool CadastralBlockToParcel { get; set; }
		/// <summary>
		/// Тип наследования: Кадастровый квартал -> Здание
		/// </summary>
		public bool CadastralBlockToBuilding { get; set; }
		/// <summary>
		/// Тип наследования: Кадастровый квартал -> Сооружение
		/// </summary>
		public bool CadastralBlockToConstruction { get; set; }
		/// <summary>
		/// Тип наследования: Кадастровый квартал -> Объект незавершенного строительства
		/// </summary>
		public bool CadastralBlockToUncomplited { get; set; }
		/// <summary>
		/// Тип наследования: Земельный участок -> Здание
		/// </summary>
		public bool ParcelToBuilding { get; set; }
		/// <summary>
		/// Тип наследования: Земельный участок -> Сооружение
		/// </summary>
		public bool ParcelToConstruction { get; set; }
		/// <summary>
		/// Тип наследования: Земельный участок -> Объект незавершенного строительства
		/// </summary>
		public bool ParcelToUncomplited { get; set; }
		/// <summary>
		/// Тип наследования: Здание -> Помещение
		/// </summary>
		public bool BuildToFlat { get; set; }

		/// <summary>
		/// Фактор, содержащий родительский кадастровый номер, по которому осуществляется сопоставление с родительским объектом
		/// </summary>
		[Required(ErrorMessage = "Родительский фактор обязательное обязательное поле")]
		public long? ParentCadastralNumberAttribute { get; set; }

		/// <summary>
		/// Список выбранных атрибутов
		/// </summary>
		[Required(ErrorMessage = "Заполните атрибуты")]
		public List<AttributeMapping> Attributes { get; set; }

		private const int StartAttributesCount = 5;



		public InheritanceViewModel()
		{
			Attributes = Enumerable.Repeat(new AttributeMapping(), StartAttributesCount).ToList();
		}


		public GbuInheritanceAttributeSettings ToAttributeSettings()
		{
			return new GbuInheritanceAttributeSettings
			{
				TaskFilter = TaskFilter,
				ObjectChangeStatus = ObjectChangeStatus,
				//TODO
				//Attributes = Attributes,
				ParcelToConstruction = ParcelToConstruction,
				ParcelToUncomplited = ParcelToUncomplited,
				ParcelToBuilding = ParcelToBuilding,
				ParentCadastralNumberAttribute = ParentCadastralNumberAttribute.GetValueOrDefault(),
				CadastralBlockToParcel = CadastralBlockToParcel,
				CadastralBlockToBuilding = CadastralBlockToBuilding,
				CadastralBlockToConstruction = CadastralBlockToConstruction,
				CadastralBlockToUncomplited = CadastralBlockToUncomplited
			};
		}

		public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
		{
			if (CadastralBlockToConstruction == false && ParcelToConstruction == false && ParcelToUncomplited == false && ParcelToBuilding == false && CadastralBlockToUncomplited == false 
			    && CadastralBlockToBuilding == false && CadastralBlockToParcel == false && BuildToFlat == false)
			{
				yield return
						new ValidationResult(errorMessage: "Выберите тип наследования");
			}

			if (!Attributes.Any())
			{
				yield return
					new ValidationResult(errorMessage: "Заполните наследуемые факторы");
			}
		}
	}
}