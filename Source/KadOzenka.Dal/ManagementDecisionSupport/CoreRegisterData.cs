using System;
using System.Collections.Generic;
using System.Text;

namespace KadOzenka.Dal.ManagementDecisionSupport
{

    class CoreRegisterData
    {
        public long registerid { get; set; }
        public string registername { get; set; }
        public string registerdescription { get; set; }
        public string allpri_table { get; set; }
        public string object_table { get; set; }
        public string quant_table { get; set; }
        public string track_changes_column { get; set; }
        public long? storage_type { get; set; }
        public string object_sequence { get; set; }
        public long? is_virtual { get; set; }
        public long? contains_quant_in_future { get; set; }
        public string db_connection_name { get; set; }
        public string track_changes_userid { get; set; }
        public string track_changes_date { get; set; }
        public long? is_deleted { get; set; }
        public long? allpri_partitioning { get; set; }
        public long? main_register { get; set; }
    }

}
