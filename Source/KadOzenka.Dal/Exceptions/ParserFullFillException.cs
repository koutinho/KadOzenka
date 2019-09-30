using System;
using System.Collections.Generic;
using System.Text;

namespace OuterMarketParser.Exceptions
{
    class ParserFullFillException : Exception
    {
        public ParserFullFillException(string message) : base(message) { }
    }
}
