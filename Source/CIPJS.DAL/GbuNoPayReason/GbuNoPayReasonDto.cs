using ObjectModel.Insur;
using System.ComponentModel.DataAnnotations;

namespace CIPJS.DAL.GbuNoPayReason
{
    public class GbuNoPayReasonDto
    {
        public long Id { get; set; }

        /// <summary>
        /// Причина
        /// </summary>
        [Display(Name = "Причина")]
        public string Reason { get; set; }

        /// <summary>
        /// Вид страхования
        /// </summary>
        public string TypeInsur { get; set; }

        /// <summary>
        /// Краткое пояснение (должно быть напечатано на заключении)
        /// </summary>
        [Display(Name = "Краткое пояснение")]
        public string ShortExplanation { get; set; }

        public static GbuNoPayReasonDto OMMap(OMGbuNoPayReason gbuNoPayReason)
        {
            return new GbuNoPayReasonDto
            {
                Id = gbuNoPayReason.Id,
                TypeInsur = gbuNoPayReason.TypeInsur,
                Reason = gbuNoPayReason.Reason,
                ShortExplanation = gbuNoPayReason.ShortExplanation
            };
        }
        public static OMGbuNoPayReason OMMap(GbuNoPayReasonDto gbuNoPayReason)
        {
            return new OMGbuNoPayReason
            {
                Id = gbuNoPayReason.Id,
                TypeInsur = gbuNoPayReason.TypeInsur,
                Reason = gbuNoPayReason.Reason,
                ShortExplanation = gbuNoPayReason.ShortExplanation
            };
        }
    }
}
