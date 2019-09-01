using System.Collections.Generic;


namespace CIPJS.DAL.Bti.Import
{
	public class BtiObjectsImportCondition
	{
		public string Column { get; set; }
		
		public List<string> AllowedValues { get; set; }
	}


	public class ConfigBtiObjectsImport
    {
		public List<BtiObjectsImportCondition> Conditions { get; set; }

		public int MaxDegreeOfParallelism { get; set; }

		public string import_bti_daily_Kls { get; set; }

		public string import_bti_daily_Fkun { get; set; }

		public string import_bti_daily_Fsks { get; set; }

		public string import_bti_daily_Fads { get; set; }

        public string import_bti_daily_DiapKv { get; set; }

        public string import_bti_daily_Ets { get; set; }

		public string import_bti_daily_Fkmn { get; set; }

		public string import_bti_daily_Fkva { get; set; }

		public string import_bti_daily_Log { get; set; }

		public string import_bti_daily_flat_Log { get; set; }

		public string import_bti_daily_address_Log { get; set; }

        public string import_bti_daily_fads_Log { get; set; }

        public string ref_addr_street_table { get; set; }

		public string ref_addr_district_table { get; set; }

		public string ref_addr_okrug_table { get; set; }

	}
}
