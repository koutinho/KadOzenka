using KadOzenka.Dal.Groups.Dto;

namespace KadOzenka.Web.Models.Task
{
    public class CadastralPriceCalculationSettingsModel
    {
        public long Id { get; set; }
        public string GroupName { get; set; }
        public int Priority { get; set; }
        public bool Stage1 { get; set; }
        public bool Stage2 { get; set; }
        public bool Stage3 { get; set; }

        public static CadastralPriceCalculationSettingsModel ToModel(GroupCalculationSettingsDto dto)
        {
            return new CadastralPriceCalculationSettingsModel
            {
                Id = dto.Id,
                GroupName = $"{dto.GroupNumber}. {dto.GroupName}",
                Priority = dto.Priority,
                Stage1 = dto.Stage1,
                Stage2 = dto.Stage2,
                Stage3 = dto.Stage3
            };
        }

        public static GroupCalculationSettingsDto FromModel(CadastralPriceCalculationSettingsModel model)
        {
            return new GroupCalculationSettingsDto
            {
                Id = model.Id,
                Priority = model.Priority,
                Stage1 = model.Stage1,
                Stage2 = model.Stage2,
                Stage3 = model.Stage3
            };
        }
    }
}
