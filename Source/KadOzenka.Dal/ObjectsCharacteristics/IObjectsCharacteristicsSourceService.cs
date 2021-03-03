using System;
using System.Collections.Generic;
using System.Text;
using KadOzenka.Dal.ObjectsCharacteristics.Dto;

namespace KadOzenka.Dal.ObjectsCharacteristics
{
	public interface IObjectsCharacteristicsSourceService
	{
		SourceDto GetSource(long registerId);
		long AddSource(SourceDto sourceDto);
		void EditSource(SourceDto sourceDto);
	}
}
