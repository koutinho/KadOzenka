using System;
using System.Collections.Generic;
using System.Text;

using Newtonsoft.Json;

namespace DebugApplication.Parser.Cian
{
    class Client
    {
        public IParser Parser { get; set; }
        public Client(IParser parser) => Parser = parser;
        public override string ToString() => JsonConvert.SerializeObject(Parser.GetProperty(), Formatting.Indented);
    }
}
