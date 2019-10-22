using System;
using System.Text;
using System.Collections.Generic;

namespace OuterMarketParser.Exceptions
{

    class ParserFullFillException : Exception
    {

        public ParserFullFillException(string message) : base(message) { }

    }

}