using System;
using System.Collections.Generic;
using System.Text;

namespace Parser
{

    class HashMarketCoreObjects
    {

        public long Id { get; set; }
        public long Market_code { get; set; }
        public string Market { get; set; }
        public string Url { get;set;} 
        public decimal? Price { get; set; }
        public long? Process_type_code { get; set; }
        public string Process_type { get; set; }
        public long? Exclusion_status_code { get; set; }
        public string Exclusion_status { get; set; }
        public decimal? Cct { get; set; }

        public string ToString(HashMarketCoreObjects obj) =>
            new DP().ToString(obj);

        public List<HashMarketCoreObjects> GetAllCashe(string connectionString, string tableName) =>
            new PG(connectionString).ReadHash<HashMarketCoreObjects>(tableName);

    }

}
