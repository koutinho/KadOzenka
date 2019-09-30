using System;
using System.Collections.Generic;
using System.Text;

namespace OuterMarketParser.Exceptions
{
    class SubcategoryException : Exception
    {
        public SubcategoryException(string message) : base(message) { }
    }
}
