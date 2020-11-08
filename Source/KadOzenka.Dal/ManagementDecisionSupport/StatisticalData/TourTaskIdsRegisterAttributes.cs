using ObjectModel.Directory;
using System;
using System.Collections.Generic;
using System.Text;

namespace KadOzenka.Dal.ManagementDecisionSupport.StatisticalData
{

    class TourTaskIdsRegisterAttributes
    {
        public long? id { get; set; }
        public DateTime? creation_date { get; set; }
        public long? tour_id { get; set; }
        public KoNoteType note_type_code { get; set; }
        public KoTaskStatus status_code { get; set; }
        public long? response_document_id { get; set; }
        public long? document_id { get; set; }
        public DateTime? estimation_date { get; set; }
    }

}
