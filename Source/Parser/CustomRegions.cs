using System;
using System.Collections.Generic;
using System.Text;

namespace Parser
{

    class CustomRegions
    {

        public long Id { get; set; }                                            //Идентификатор
        public long? Code { get; set; }                                         //Код района, используемый в основной системе
        public long District_id { get; set; }                                   //Идентификатор административного округа
        public long ReferenceId { get; set; }                                   //Код реестра, используемый в основной системе
        public string Name { get; set; }                                        //Наименование района
        public string Value { get; set; }                                       //Название района, используемое в основной системе
        public long? Cian_code { get; set; }                                    //Код района, используемый в агрегаторе ЦИАН
        public long? Avito_code { get; set; }                                   //Код района, используемый в агрегаторе Авито
        public long? Yandex_property_code { get; set; }                         //Код района, используемый в агрегаторе Яндекс недвижимость

        public List<CustomRegions> GetAllCashe(string connectionString, string tableName) =>
            new PG(connectionString).ReadHash<CustomRegions>(tableName);

        public string ToString(CustomRegions obj) =>
            new DP().ToString(obj);

    }

}
