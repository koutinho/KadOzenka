using System;
using ObjectModel.Directory;

namespace CIPJS.Models.DamageAnalysis
{
    public class CheckObjectAndDateDto
    {
        public long ObjId { get; set; }

        public DateTime Date { get; set; }

        public ContractType Type { get; set; }

        public long? Id { get; set; }
    }
}
