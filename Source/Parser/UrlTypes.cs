using System;
using System.Collections.Generic;
using System.Text;

namespace Parser
{

    class UrlTypes
    {

        public long Id { get; set; }                                            //Идентификатор
        public long Code { get; set; }                                          //Код типа ссылки
        public string Name { get; set; }                                        //Наименование типа ссылки
        public string Value { get; set; }                                       //Название типа ссылки

        public List<UrlTypes> GetAllCashe(string connectionString, string tableName) =>
            new PG(connectionString).ReadHash<UrlTypes>(tableName);

        public string ToString(UrlTypes obj) => 
            new DP().ToString(obj);

    }

}
