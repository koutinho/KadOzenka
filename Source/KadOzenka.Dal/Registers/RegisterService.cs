using System;
using Core.Shared.Extensions;

namespace KadOzenka.Dal.Registers
{
    public class RegisterService
    {
        public long GetFirstAttributeId(long registerId)
        {
            return registerId * Math.Pow(10, (8 - Math.Floor(Math.Log10(registerId) + 1))).ParseToLong() + 100;
        }
    }
}
