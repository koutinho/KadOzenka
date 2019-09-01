using ObjectModel.Insur;
using System;

namespace CIPJS.Models.ChangesLog
{
    public class ChangesLogDto
    {
        public long Id { get; set; }

        public DateTime? LoadDate { get; set; }

        public string Reason { get; set; }

        public static ChangesLogDto OMMap(OMChangesLog entity)
        {
            return new ChangesLogDto
            {
                Id = entity.EmpId,
                LoadDate = entity.LoadDate,
                Reason = entity.Reason
            };
        }
    }
}
