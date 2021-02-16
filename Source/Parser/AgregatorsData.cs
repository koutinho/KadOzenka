using System;
using System.Collections.Generic;
using System.Text;

namespace Parser
{

    class AgregatorsData
    {

        public long Id { get; set; }                                            //Идентификатор
        public long Code { get; set; }                                          //Код агрегатора, используемый в основной системе
        public long Referenceid { get; set; }                                   //Код реестра, используемый в основной системе
        public string Name { get; set; }                                        //Наименование агрегатора
        public string Value { get; set; }                                       //Название агрегатора, используемое в основной системе
        public string Parser_value { get; set; }                                //Название агрегатора, используемое при генерации структуры БД парсера
        public string Link { get; set; }                                        //Ссылка на агрегатор

        public string ToString(AgregatorsData obj) =>
            new DP().ToString(obj);

        public List<AgregatorsData> GetAllCashe(string connectionString, string tableName) => 
            new PG(connectionString).ReadHash<AgregatorsData>(tableName);

    }

}
