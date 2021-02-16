using System;
using System.Collections.Generic;
using System.Text;

namespace Parser
{

    class CIPJSPropertyTypes
    {

        public long Id { get; set; }                                            //Идентификатор
        public long Code { get; set; }                                          //Код типова объекта недвижимости, используемый в основной системе
        public long ReferenceId { get; set; }                                   //Код реестра, используемый в основной системе
        public string Name { get; set; }                                        //Наименование типа объекта недвижимости
        public string Value { get; set; }                                       //Название типа объекта недвижимости, используемое в основной системе

        public List<CIPJSPropertyTypes> GetAllCashe(string connectionString, string tableName) =>
            new PG(connectionString).ReadHash<CIPJSPropertyTypes>(tableName);

        public string ToString(CIPJSPropertyTypes obj) =>
            new DP().ToString(obj);

    }

}
