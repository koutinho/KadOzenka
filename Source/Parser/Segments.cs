using System;
using System.Collections.Generic;
using System.Text;

namespace Parser
{

    class Segments
    {

        public long Id { get; set; }                                            //Идентификатор
        public long Code { get; set; }                                          //Код сегмента рынка, используемый в основной системе
        public long ReferenceId { get; set; }                                   //Код реестра, используемый в основной системе
        public string Name { get; set; }                                        //Наименование сегмента рынка
        public string Value { get; set; }                                       //Название сегмента рынка, используемое в основной системе

        public List<Segments> GetAllCashe(string connectionString, string tableName) =>
            new PG(connectionString).ReadHash<Segments>(tableName);

        public string ToString(Segments obj) =>
            new DP().ToString(obj);

    }

}
