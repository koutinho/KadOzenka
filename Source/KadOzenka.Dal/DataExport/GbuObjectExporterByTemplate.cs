using System;
using System.Collections.Generic;
using System.Linq;

namespace KadOzenka.Dal.DataExport
{
	public class GbuObjectExporterByTemplate : DataExporterByTemplate
	{
		public override void ValidateColumns(List<DataExportColumn> columns)
		{
			base.ValidateColumns(columns);

			if (columns.Count(x => x.IsKey) > 1)
			{
				throw new Exception("Должен быть выбран только один ключевой параметр");
			}
		}
    }
}
