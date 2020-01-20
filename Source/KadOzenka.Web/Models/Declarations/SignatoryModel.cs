using System.ComponentModel.DataAnnotations;
using ObjectModel.Declarations;

namespace KadOzenka.Web.Models.Declarations
{
	public class SignatoryModel
	{
		/// <summary>
		/// Идентификатор (ID)
		/// </summary>
		[Display(Name = "Идентификатор")]
		public long Id { get; set; }

		/// <summary>
		/// Имя (FULL_NAME)
		/// </summary>
		[Display(Name = "Имя")]
		public string Name { get; set; }

		/// <summary>
		/// Должность (POSITION)
		/// </summary>
		[Display(Name = "Должность")]
		public string Position { get; set; }

		public static SignatoryModel FromEntity(OMSignatory entity)
		{
			if (entity == null)
			{
				return new SignatoryModel
				{
					Id = -1
				};
			}

			return new SignatoryModel
			{
				Id = entity.Id,
				Name = entity.FullName,
				Position = entity.Position
			};
		}

		public static void ToEntity(SignatoryModel signatoryViewModel, ref OMSignatory entity)
		{
			entity.FullName = signatoryViewModel.Name;
			entity.Position = signatoryViewModel.Position;
		}
	}
}
