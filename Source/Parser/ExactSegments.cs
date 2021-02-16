using System;
using System.Collections.Generic;
using System.Text;

namespace Parser
{

    class ExactSegments
    {

        public long Id { get; set; }                                            //Идентификатор
        public string Type { get; set; }                                        //Тип обобщённого сегмента
        public long Property_type_id { get; set; }                              //Идентификатор типа объекта недвижимости по умолчанию
        public long Market_segment_id { get; set; }                             //Идентификатор типа сегмента рынка по умолчанию
        public string Comment { get; set; }                                     //Комментарий, показывающий сегмент рынка
        public long? Url_type_id_cian { get; set; }                             //Идентификатор типа URL адреса для ЦИАН-а
        public long? Url_type_id_avito { get; set; }                            //Идентификатор типа URL адреса для Авито
        public long? Url_type_id_yandex_property { get; set; }                  //Идентификатор типа URL адреса для сайта Яндекс Недвижимость
        public string Url_main_part_cian { get; set; }                          //Главная часть URL адреса, специфическая для ЦИАН-а
        public string Url_main_part_avito { get; set; }                         //Главная часть URL адреса, специфическая для Авито
        public string Url_main_part_yandex_property { get; set; }               //Главная часть URL адреса, специфическая для сайта Яндекс Недвижимость
        public string Url_exect_part_cian { get; set; }                         //Дополнительная часть URL адреса, специфическая для ЦИАН-а
        public string Url_exect_part_avito { get; set; }                        //Дополнительная часть URL адреса, специфическая для Авито
        public string Url_exect_part_yandex_property { get; set; }              //Дополнительная часть URL адреса, специфическая для сайта Яндекс Недвижимость
        public string Url_as_parameter_type_cian { get; set; }                  //Тип дополнительного URL параметра для ЦИАН-а
        public string Url_as_parameter_type_avito { get; set; }                 //Тип дополнительного URL параметра для Авито
        public string Url_as_parameter_type_yandex_property { get; set; }       //Тип дополнительного URL параметра для сайта Яндекс Недвижимость
        public string Url_as_parameter_value_cian { get; set; }                 //Значение дополнительного URL параметра для ЦИАН-а
        public string Url_as_parameter_value_avito { get; set; }                //Значение дополнительного URL параметра для Авито
        public string Url_as_parameter_value_yandex_property { get; set; }      //Значение дополнительного URL параметра для сайта Яндекс Недвижимость
        public string Cian_property_type { get; set; }                          //Тип недвижимости, использующийся при парсинге ЦИАН-а
        public string Avito_property_type { get; set; }                         //Тип недвижимости, использующийся при парсинге Авито
        public string Yandex_property_property_type { get; set; }               //Тип недвижимости, использующийся при парсинге Яндекс Недвижимости

        public List<ExactSegments> GetAllCashe(string connectionString, string tableName) =>
            new PG(connectionString).ReadHash<ExactSegments>(tableName);

        public string ToString(ExactSegments obj) =>
            new DP().ToString(obj);

    }

}
