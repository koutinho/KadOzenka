using System;
using System.Collections.Generic;
using System.Linq;

namespace KadOzenka.Dal.DataExport
{
	public class UnitExporterByTemplate : DataExporterByTemplate
    {
        public override void ValidateColumns(List<DataExportColumn> columns)
        {
	        base.ValidateColumns(columns);

            var maxKeyCount = 2;
            if (columns.Count(x => x.IsKey) > maxKeyCount)
            {
                throw new Exception($"Максимальное число ключевых параметров: {maxKeyCount}");
            }
        }
    }
}
