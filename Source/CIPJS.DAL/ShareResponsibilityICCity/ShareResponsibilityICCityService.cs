using ObjectModel.Insur;
using System.Linq;

namespace CIPJS.DAL.ShareResponsibilityICCity
{
    public class ShareResponsibilityICCityService
    {
        public ShareResponsibilityICCityDto CreateEmpty()
        {
            return new ShareResponsibilityICCityDto
            {
                Id = -1
            };
        }

        public ShareResponsibilityICCityDto Get(long? id)
        {
            if (id.HasValue)
            {
                OMShareResponsibilityICCity share = OMShareResponsibilityICCity.Where(x => x.Id == id).SelectAll().Execute().FirstOrDefault();
                if (share != null)
                {
                    return new ShareResponsibilityICCityDto
                    {
                        Id = share.Id,
                        DateBegin = share.DateBegin,
                        Type = share.Type,
                        CityShare = share.CityShare,
                        ICShare = share.ICShare,
                        Note = share.Note
                    };
                }
            }

            return CreateEmpty();
        }
    }
}
