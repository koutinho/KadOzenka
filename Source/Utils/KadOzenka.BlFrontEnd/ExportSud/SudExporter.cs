using System;
using System.Collections.Generic;
using System.Text;

namespace KadOzenka.BlFrontEnd.ExportSud
{
    public static class SudExporter
    {
		public static void DoLoad()
		{
			ObjectModel.Sud.OMObject sudObject = new ObjectModel.Sud.OMObject
			{
				Kn = "77:22:0040107:1464"
			};

			ObjectModel.Sud.OMObject existingSudObject = ObjectModel.Sud.OMObject.Where(x => x.Kn == "77:22:0040107:1464").ExecuteFirstOrDefault();

			if(existingSudObject != null)
			{
				sudObject.Id = existingSudObject.Id;
			}

			sudObject.Save();
		}
    }
}
