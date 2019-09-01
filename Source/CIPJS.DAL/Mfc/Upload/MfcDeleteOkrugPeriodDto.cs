using ObjectModel.Insur;
using System;
using System.Collections.Generic;

namespace CIPJS.DAL.Mfc.Upload
{
    public class MfcDeleteOkrugPeriodDto
    {
        public DateTime? PeriodRegDate { get; set; }

        public OMOkrug Okrug { get; set; }

        public List<OMDistrict> Districts { get; set; }
    }
}
