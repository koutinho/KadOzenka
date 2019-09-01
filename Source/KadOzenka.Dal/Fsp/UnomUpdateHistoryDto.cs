using System;
using System.Data;
using CIPJS.DAL.Building;
using Core.SRD;
using ObjectModel.Insur;

namespace CIPJS.DAL.Fsp
{
    public class UnomUpdateHistoryDto
    {
        public long Id { get; set; }

        public long OldUnom { get; set; }

        public long NewUnom { get; set; }

        public long OldLinkMkd { get; set; }

        public long NewLinkMkd { get; set; }

        public string OldAddress { get; set; }

        public string NewAddress { get; set; }

        public string Comment { get; set; }

        public DateTime DateChange { get; set; }

        public string UserChange { get; set; }


        public static UnomUpdateHistoryDto FromOMObject(OMUnomUpdate obj)
        {
            var bs = new BuildingService();
            return new UnomUpdateHistoryDto
            {
                Id = obj.Id,
                OldUnom = obj.OldUnom,
                NewUnom = obj.NewUnom,
                OldLinkMkd = obj.OldLinkMkd,
                NewLinkMkd = obj.NewLinkMkd,
                OldAddress = bs.GetShortAddress(obj.OldLinkMkd),
                NewAddress = bs.GetShortAddress(obj.NewLinkMkd),
                Comment = obj.Note,
                DateChange = obj.DateChange,
                UserChange = obj.ParentUser.FullName
            };
        }

        public static UnomUpdateHistoryDto FromDataRow(DataRow data)
        {
            return new UnomUpdateHistoryDto
            {
                Id = (long) data["emp_id"],
                OldUnom = (long) data["unom_old"],
                NewUnom = (long) data["unom_new"],
                OldLinkMkd = (long) data["link_mkd_old"],
                NewLinkMkd = (long) data["link_mkd_new"],
                OldAddress = data["address_old"] as string,
                NewAddress = data["address_new"] as string,
                Comment = data["note"] as string,
                DateChange = (DateTime) data["date_change"],
                UserChange = data["user_fullname"] as string
            };
        }

        public OMUnomUpdate ToOMObject()
        {
            return new OMUnomUpdate
            {
                Id = this.Id,
                OldUnom = this.OldUnom,
                NewUnom = this.NewUnom,
                OldLinkMkd = this.OldLinkMkd,
                NewLinkMkd = this.NewLinkMkd,
                Note = Comment,
                DateChange = DateTime.Now,
                UserChange = SRDSession.Current.UserID
            };
        }
    }
}
