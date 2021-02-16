using System;
using System.Collections.Generic;
using System.Text;

namespace Parser
{

    class AgregatorsDataExtra
    {

        public long Id { get; set; }                                            //Идентификатор
        public long Agregator_id { get; set; }                                  //Идентификатор агрегатора
        public long? On_page_counter { get; set; }                              //Количество объявлений на странице
        public long? Maximum_page_counter { get; set; }                         //Максимальное количество страниц для агрегатора
        public string Ordinar_url_template { get; set; }                        //Обычный URL шаблон для агрегатора
        public string Root_url_template { get; set; }                           //Корневой URL шаблон для агрегатора
        public string As_parameter_url_template { get; set; }                   //URL шаблон с параметром для агрегатора
        public string Regex_script_get_count { get; set; }                      //Регулярное выражение, позволяющее получить количество объявлений с учётом задаррых фильтров
        public string Regex_script_check_for_empty_data { get; set; }           //Регулярное выражение, позволяющее определить, есть ли по заданной ссылке объекты недвижимости
        public string User_agent { get; set; }                                  //Юзер агент необходимый для парсинга
        public string Cookies { get; set; }                                     //Куки необходимые для парсинга
        public string Regex_script_check_capcha { get; set; }                   //Регулярное выражения, позволяющее проверить появилась ли капча

        public List<AgregatorsDataExtra> GetAllCashe(string connectionString, string tableName) => 
            new PG(connectionString).ReadHash<AgregatorsDataExtra>(tableName);

        public string ToString(AgregatorsDataExtra obj) =>
            new DP().ToString(obj);

    }

}
