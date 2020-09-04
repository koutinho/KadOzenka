using System;
using System.Collections.Generic;
using System.Linq;

namespace KadOzenka.Dal.DataExport
{
	public class UnitExporterByTemplate : DataExporterByTemplate
    {
        public override void ValidateColumns(List<DataExportColumn> columns)
        {
            if (columns.All(x => x.IsKey == false))
            {
                throw new Exception("Должен быть выбран хотя бы один ключевой параметр");
            }

            var maxKeyCount = 2;
            if (columns.Count(x => x.IsKey) > maxKeyCount)
            {
                throw new Exception($"Максимальное число ключевых параметров: {maxKeyCount}");
            }
        }
    }
}
