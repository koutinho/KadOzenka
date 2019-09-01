using Core.Shared.Extensions;
using ObjectModel.Directory;
using ObjectModel.Insur;
using System.ComponentModel.DataAnnotations;

namespace CIPJS.Models.InputFile
{
    public class InsurFileProcessLogDto
    {
        public long Id { get; set; }

        [Display(Name = "Статус")]
        public string Status { get; set; }

        [Display(Name = "Прогресс")]
        public double Percent { get; set; }

        [Display(Name = "Связано с ФСП")]
        public long? ProcessedCount { get; set; }

        [Display(Name = "Выполнен учет зачисления/начисления")]
        public long? ProcessedFspCont { get; set; }

        [Display(Name = "Не создано")]
        public long? ErrorCount { get; set; }

        [Display(Name = "Создано, не связано с объектом")]
        public long? ObjectErrorCount { get; set; }

        [Display(Name = "Журнал ошибок")]
        public string ErrorLog { get; set; }

        public static InsurFileProcessLogDto OMMap(OMFileProcessLog entity)
        {
            string status;
            double percent;

            switch(entity.Status_Code)
            {
                case FileProcessStatus.Prepare:
                    status = entity.Status;
                    percent = 0;
                    break;
                case FileProcessStatus.CreateBindFsp:
                    status = entity.TotalCount.HasValue ? $"{entity.Status} ({entity.ProcessedCount ?? 0}/{entity.TotalCount})" : entity.Status;
                    percent = entity.TotalCount.HasValue ?
                        entity.Status_Code.GetEnumCode().ParseToDouble() * ((double)(entity.ProcessedCount ?? 0) / entity.TotalCount.Value) :
                        0;
                    break;
                case FileProcessStatus.RecalcFsp:
                    status = entity.TotalFspCount.HasValue ? $"{entity.Status} ({entity.ProcessedFspCont ?? 0}/{entity.TotalFspCount})" : entity.Status;
                    percent = entity.TotalFspCount.HasValue ?
                        FileProcessStatus.CreateBindFsp.GetEnumCode().ParseToDouble() + ((entity.Status_Code.GetEnumCode().ParseToDouble() - FileProcessStatus.CreateBindFsp.GetEnumCode().ParseToDouble()) * ((double)(entity.ProcessedFspCont ?? 0) / entity.TotalFspCount.Value)) :
                        0;
                    break;
                default:
                    status = entity.Status;
                    percent = 100;
                    break;
            }

            return new InsurFileProcessLogDto
            {
                Id = entity.EmpId,
                Percent = percent,
                Status = status,
                ProcessedCount = entity.ProcessedCount,
                ProcessedFspCont = entity.ProcessedFspCont,
                ErrorCount = entity.ErrorCount,
                ObjectErrorCount = entity.ObjectErrorCount,
                ErrorLog = entity.ErrorLog
            };
        }
    }
}
