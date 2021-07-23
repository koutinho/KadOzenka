using System;

namespace KadOzenka.Dal.Exceptions
{

    class EmptyActiveModelForGroupWithFactorValueCheckException : Exception
    {

        public EmptyActiveModelForGroupWithFactorValueCheckException(string message) : base(message) { }

    }

}