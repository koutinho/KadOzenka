using System;
using System.Collections.Generic;
using System.Text;

namespace Parser
{

    class CustomDistricts
    {

        public long Id { get; set; }                                            //Идентификатор
        public long Code { get; set; }                                          //Код района, используемый в основной системе
        public long ReferenceId { get; set; }                                   //Код реестра, используемый в основной системе
        public string Name { get; set; }                                        //Наименование округа
        public string Value { get; set; }                                        //Название округа, используемое в основной системе
        public long? Cian_code { get; set; }                                     //Код округа, используемый в агрегаторе ЦИАН
        public long? Avito_code { get; set; }                                    //Код округа, используемый в агрегаторе Авито
        public long? Yandex_property_code { get; set; }                          //Код округа, используемый в агрегаторе Яндекс недвижимость

        public List<CustomDistricts> GetAllCashe(string connectionString, string tableName) =>
            new PG(connectionString).ReadHash<CustomDistricts>(tableName);

        public string ToString(CustomDistricts obj) =>
            new DP().ToString(obj);

    }

}
