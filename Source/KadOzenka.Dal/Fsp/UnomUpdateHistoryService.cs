using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Core.SRD;
using Microsoft.Practices.EnterpriseLibrary.Data;
using ObjectModel.Insur;

namespace CIPJS.DAL.Fsp
{
    public class UnomUpdateHistoryService
    {
        public IEnumerable<UnomUpdateHistoryDto> Get(long? id = null)
        {
            var query =
@"select
    uu.emp_id,
    uu.unom_old,
    uu.unom_new,
    uu.link_mkd_old,
    uu.link_mkd_new,
    ao.short_address as address_old,
    an.short_address as address_new,
    uu.date_change,
    uu.note,
    u.fullname as user_fullname
from insur_unom_update uu
join core_srd_user u on u.id=uu.user_change
join insur_building_q bo on bo.emp_id=uu.link_mkd_old
join insur_building_q bn on bn.emp_id=uu.link_mkd_new
join insur_address ao on ao.emp_id=bo.address_id
join insur_address an on an.emp_id=bn.address_id
where bo.actual=1 and bn.actual=1" + (id.HasValue ? "and uu.emp_id="+id : "");
            return DBMngr.Realty
                .ExecuteDataSet(CommandType.Text, query)
                .Tables[0]
                .AsEnumerable()
                .Select(UnomUpdateHistoryDto.FromDataRow);
        }

        public UnomUpdateHistoryDto GetById(long id)
        {
            return Get(id).First();
        }

        public void Insert(UnomUpdateHistoryDto dto)
        {
            dto.DateChange = DateTime.Now;
            dto.UserChange = SRDSession.Current.User.FullName;
            dto.Id = dto.ToOMObject().Save();
        }

        public void Update(UnomUpdateHistoryDto dto)
        {
            dto.DateChange = DateTime.Now;
            dto.UserChange = SRDSession.Current.User.FullName;
            dto.ToOMObject().Save();
        }

        public void Delete(UnomUpdateHistoryDto dto)
        {
            new OMUnomUpdate { Id = dto.Id }.Destroy();
        }
    }
}
