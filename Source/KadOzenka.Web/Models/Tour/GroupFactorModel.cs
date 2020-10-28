using System.ComponentModel.DataAnnotations;
using KadOzenka.Dal.Groups.Dto;

namespace KadOzenka.Web.Models.Tour
{
	public class GroupFactorModel
	{
		public GroupFactorModel()
		{
		}

		public GroupFactorModel(long groupId)
		{
			Id = -1;
			GroupId = groupId;
		}

		public long Id { get; set; }
		public long GroupId { get; set; }

		[Required(ErrorMessage = "Поле фактор обязательное")]
		public long? FactorId { get; set; }
		public string FactorName { get; set; }
		public bool SignMarket { get; set; }

		public static GroupFactorModel FromDto(GroupFactorDto dto)
		{
			return new GroupFactorModel
			{
				Id = dto.Id,
				GroupId = dto.GroupId,
				FactorId = dto.FactorId,
				FactorName = dto.FactorName,
				SignMarket = dto.SignMarket
			};
		}

		public GroupFactorDto ToDto()
		{
			return new GroupFactorDto
			{
				Id = Id,
				GroupId = GroupId,
				FactorId = FactorId.Value,
				FactorName = FactorName,
				SignMarket = SignMarket
			};
		}
	}
}
