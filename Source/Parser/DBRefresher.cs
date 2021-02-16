using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace Parser
{

    class DBRefresher
    {

        public void SetCookies(string cookies, AgregatorsDataExtra agregatorsDataExtra)
        {
            List<KeyValuePair<string, string>> parameters = new List<KeyValuePair<string, string>>();
            List<KeyValuePair<string, string>> conditions = new List<KeyValuePair<string, string>>();
            parameters.Add(new KeyValuePair<string, string>("cookies", cookies));
            conditions.Add(new KeyValuePair<string, string>("id", agregatorsDataExtra.Id.ToString()));
            new PG(ConfigurationManager.AppSettings["ConnectionString"])
                .UpdateValue("parser_markert_agregators_specifications", parameters, conditions);
            Program._hash.RefreshAgregatorsDataExtra();
        }



    }

}
