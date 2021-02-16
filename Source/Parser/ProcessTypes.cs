using System;
using System.Collections.Generic;
using System.Text;

namespace Parser
{

    class ProcessTypes
    {

        public long Id { get; set; }                                            //Идентификатор
        public long Code { get; set; }                                          //Код типов процесса, используемый в основной системе
        public long ReferenceId { get; set; }                                   //Код реестра, используемый в основной системе
        public string Name { get; set; }                                        //Наименования типов процессов
        public string Value { get; set; }                                       //Название типов процессов, используемое в основной системе

        public List<ProcessTypes> GetAllCashe(string connectionString, string tableName) =>
            new PG(connectionString).ReadHash<ProcessTypes>(tableName);

        public string ToString(ProcessTypes obj) =>
            new DP().ToString(obj);

    }

}
