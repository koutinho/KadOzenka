using System.ComponentModel.DataAnnotations;
using ObjectModel.Directory;
using ObjectModel.KO;

namespace KadOzenka.Web.Models.Tour.EstimateGroup
{
	public class AddGroupViewModel
	{
		/// <summary>
		/// Ид записи из таблицы соответствия
		/// </summary>
		public long? Id { get; set; } = -1;

		/// <summary>
		/// Ид тура
		/// </summary>
		[Required(ErrorMessage = "Не заполнен ид тура")]
		public long? TourId { get; set; }

		/// <summary>
		/// Код справочника
		/// </summary>
		[Required(ErrorMessage = "Заполните поле код")]
		public string Code { get; set; }

		/// <summary>
		/// Тип объекта недвижимости
		/// </summary>
		[Required(ErrorMessage = "Выберите тип объекта недвижимости")]
		public PropertyTypes? ObjectType { get; set; }

		/// <summary>
		/// Группа 
		/// </summary>
		[Required(ErrorMessage = "Заполните группу")]
		public string Group { get; set; }

		/// <summary>
		/// Определяем устанавливается ли группа по типу помещения жилое/нежилое
		/// </summary>
		public bool IsByTypeRoom { get; set; } = false;

		/// <summary>
		/// Тип объекта помещения
		/// </summary>
		public KoTypeOfRoom TypeOfRoom { get; set; }

		public static AddGroupViewModel ToModel(OMComplianceGuide compliance)
		{
			var model = new AddGroupViewModel
			{
				Code = compliance.Code,
				IsByTypeRoom = compliance.TypeRoom_Code != KoTypeOfRoom.None,
				TypeOfRoom = compliance.TypeRoom_Code,
				Group = compliance.SubGroup,
				ObjectType = compliance.TypeProperty_Code,
				Id = compliance.Id
			};
			return model;
		}

		public static OMComplianceGuide ToEntity(OMComplianceGuide compliance, AddGroupViewModel model)
		{
			compliance.Code = model.Code;
			compliance.TypeRoom_Code = model.TypeOfRoom;
			compliance.SubGroup = model.Group;
			compliance.TypeProperty_Code = model.ObjectType.GetValueOrDefault(PropertyTypes.None);
			compliance.TourId = model.TourId.GetValueOrDefault();
			return compliance;
		}
	}
}