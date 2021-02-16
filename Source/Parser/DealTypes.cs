using System;
using System.Collections.Generic;
using System.Text;

namespace Parser
{

    class DealTypes
    {

        public long Id { get; set; }                                            //Идентификатор
        public long Code { get; set; }                                          //Код типа сделки, используемый в основной системе
        public long ReferenceId { get; set; }                                   //Код реестра, используемый в основной системе
        public string Name { get; set; }                                        //Наименование типа сделки
        public string Value { get; set; }                                       //Название типа сделки, используемое в основной системе
        public string Comment { get; set; }                                     //Комментарий, показывающий тип сделки
        public string Cian_url_code { get; set; }                               //URL-параметр, отображающий тип сделки на ЦИАН-е
        public string Avito_url_code { get; set; }                              //URL-параметр, отображающий тип сделки на Авито
        public string Yandex_property_url_code { get; set; }                    //URL-параметр, отображающий тип сделки на сайте Яндекс недвижимость

        public List<DealTypes> GetAllCashe(string connectionString, string tableName) =>
            new PG(connectionString).ReadHash<DealTypes>(tableName);

        public string ToString(DealTypes obj) =>
            new DP().ToString(obj);

    }

}
