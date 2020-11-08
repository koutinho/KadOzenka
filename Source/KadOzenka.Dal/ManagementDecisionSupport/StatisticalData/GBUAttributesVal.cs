using System;
using System.Collections.Generic;
using System.Text;

namespace KadOzenka.Dal.ManagementDecisionSupport.StatisticalData
{

    class GBUAttributesVal
    {
        public long id { get; set; }
        public string name { get; set; }
        public long registerid { get; set; }
        public long type { get; set; }
        public long? parentid { get; set; }
        public long? referenceid { get; set; }
        public string value_field { get; set; }
        public string code_field { get; set; }
        public string value_template { get; set; }
        public long? primary_key { get; set; }
        public long? user_key { get; set; }
        public string qscolumn { get; set; }
        public string internal_name { get; set; }
        public long? is_nullable { get; set; }
        public string description { get; set; }
        public string layout { get; set; }
        public string export_column_name { get; set; }
        public long? is_deleted { get; set; }
        public long? change_user_id { get; set; }
        public DateTime? change_date { get; set; }
        public long? hidden { get; set; }
    }

}
