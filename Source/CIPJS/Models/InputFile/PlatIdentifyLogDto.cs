using ObjectModel.Directory;
using ObjectModel.Insur;
using System;
using System.ComponentModel.DataAnnotations;

namespace CIPJS.Models.InputFile
{
    public class PlatIdentifyLogDto
    {
        public long Id { get; set; }

        [Display(Name = "Статус")]
        public string Status { get; set; }

        [Display(Name = "Прогресс")]
        public double Percent { get; set; }

        [Display(Name = "Идентифицированых записей")]
        public long? IdentiedCount { get; set; }

        [Display(Name = "Не идентифицированых записей")]
        public long? NotIdentiedCount { get; set; }

        public static PlatIdentifyLogDto OMMap(OMFilePlatIdentifyLog entity)
        {
            long updatedCount = (entity.IdentifiedCount ?? 0) + (entity.NotIdentiedCount ?? 0);

            double percent = entity.Status_Code == IdentifyPlatStatus.None
                || entity.Status_Code == IdentifyPlatStatus.Prepare ? 0 :
                    entity.Status_Code == IdentifyPlatStatus.Finished ? 100 :
                        entity.PlatCount.HasValue ? Math.Round((double)updatedCount / entity.PlatCount.Value, 2, MidpointRounding.AwayFromZero) * 100 : 0;

            return new PlatIdentifyLogDto
            {
                Id = entity.EmpId,
                IdentiedCount = entity.IdentifiedCount,
                NotIdentiedCount = entity.NotIdentiedCount,
                Percent = percent,
                Status = entity.Status_Code == IdentifyPlatStatus.Identify ?
                    $"{entity.Status}{(entity.PlatCount.HasValue ? $" {updatedCount}/{entity.PlatCount.Value}" : string.Empty)}" :
                    entity.Status
            };
        }
    }
}
