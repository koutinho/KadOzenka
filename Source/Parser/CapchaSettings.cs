using System;
using System.Collections.Generic;
using System.Text;

namespace Parser
{

    class CapchaSettings
    {

        public long Id { get; set; }                                            //Идентификатор
        public long Agregator_id { get; set; }                                  //Идентификатор агрегатора
        public string Driver_options { get; set; }                              //Список опций для хром-драйвера
        public string Capcha_key { get; set; }                                  //Идентификатор для сервиса капчи
        public string Capcha_type { get; set; }                                 //Тип капчи для сервиса капчи
        public string Capcha_method_type { get; set; }                          //Тип метода для сервиса капчи
        public long Capcha_timeout { get; set; }                                //Изначальный таймаут для решения капчи
        public long Capcha_timeout_step { get; set; }                           //Таймаут для переодического ожидания
        public string Capcha_retry_parameter { get; set; }                      //Текстовое значение, позволяющее определить, что капча ещё не решена
        public string Capcha_send_template { get; set; }                        //Шаблон URL адреса, необходимый для формирования ссылки для запроса выполнения капчи
        public string Capcha_get_template { get; set; }                         //Шаблон URL адреса, необходимый для формирования ссылки для проверки выполнена ли капча или нет
        public string Script_capcha_key { get; set; }                           //Скрипт, выполняющийся для получения ключа капчи
        public string Script_submit_capcha { get; set; }                        //Скрипт, выполняющийся для отправки капчи

        public List<CapchaSettings> GetAllCashe(string connectionString, string tableName) =>
            new PG(connectionString).ReadHash<CapchaSettings>(tableName);

        public string ToString(CapchaSettings obj) =>
            new DP().ToString(obj);

    }

}
