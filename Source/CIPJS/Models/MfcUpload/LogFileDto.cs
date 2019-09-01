using System.ComponentModel.DataAnnotations;

namespace CIPJS.Models.MfcUpload
{
    public class LogFileDto
    {
        public long Id { get; set; }

        [Display(Name="Статус загрузки")]
        public string Status { get; set; }

        [Display(Name = "Прогресс загрузки")]
        public decimal Percent { get; set; }
        
        [Display(Name = "Результаты загрузки")]
        public string TraceData { get; set; }
    }
}
