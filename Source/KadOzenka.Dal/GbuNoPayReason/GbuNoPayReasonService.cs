using Core.Shared.Extensions;
using ObjectModel.Insur;
using System.Linq;

namespace CIPJS.DAL.GbuNoPayReason
{
    public class GbuNoPayReasonService
    {
        private GbuNoPayReasonDto CreateEmpty(string typeInsur = null)
        {
            return new GbuNoPayReasonDto
            {
                Id = -1,
                TypeInsur = typeInsur.IsNotEmpty() ? typeInsur : "ЖП"
            };
        }

        public GbuNoPayReasonDto Get(long? id, string typeInsur = null)
        {
            if (id.HasValue && id.Value > 0)
            {
                OMGbuNoPayReason reason = OMGbuNoPayReason.Where(x => x.Id == id).SelectAll().Execute().FirstOrDefault();
                if (reason != null)
                {
                    return GbuNoPayReasonDto.OMMap(reason);
                }
            }

            return CreateEmpty(typeInsur);
        }

        public void Update(OMGbuNoPayReason gbuNoPayReason)
        {
            gbuNoPayReason.Save();
        }
    }
}
