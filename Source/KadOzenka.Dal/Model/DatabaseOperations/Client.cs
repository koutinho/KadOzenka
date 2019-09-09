using System;
using System.Collections.Generic;
using System.Text;

namespace OuterMarketParser.Model.DatabaseOperations
{
    class Client
    {
        public IDataToPostgreSQL Parser { get; set; }
        public Client(IDataToPostgreSQL parser) => Parser = parser;
    }
}
