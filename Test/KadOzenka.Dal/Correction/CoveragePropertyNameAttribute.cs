using System;

namespace KadOzenka.Dal.Correction
{
    public class CoveragePropertyNameAttribute : Attribute
    {
        public string PropertyName { get; }

        public CoveragePropertyNameAttribute(string propertyName)
        {
            this.PropertyName = propertyName;
        }
    }
}
