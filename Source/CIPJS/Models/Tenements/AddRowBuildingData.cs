using System;

namespace CIPJS.Models.Tenements
{
    public class AddRowBuildingData
    {
        public string Name { get; set; }

        public string BuildingColumn { get; set; }

        public int BuildingReestrId { get; set; }

        public string BuildingObjectId { get; set; }

        public int? BuildingAttributeId { get; set; }

        public string EgrnColumn { get; set; }

        public int EgrnReestrId { get; set; }

        public string EgrnObjectId { get; set; }

        public int? EgrnAttributeId { get; set; }

        public string BtiColumn { get; set; }

        public int BtiReestrId { get; set; }

        public string BtiObjectId { get; set; }

        public int? BtiAttributeId { get; set; }

        public Type Type { get; set; }
        
        public object MfcValue { get; set; }

        public object MkdValue { get; set; }

        public object EgrnValue { get; set; }

        public object BtiValue { get; set; }

        public DateTime? MfcData { get; set; }

        public bool CheckInsurFlag { get; set; }

        public AddRowBuildingData()
        {
            Type = null;
            MfcValue = null;
            MkdValue = null;
            CheckInsurFlag = false;
        }
    }
}