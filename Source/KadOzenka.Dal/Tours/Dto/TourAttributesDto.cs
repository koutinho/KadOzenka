using System.Collections.Generic;
using ObjectModel.Core.Register;

namespace KadOzenka.Dal.Tours.Dto
{
	public class TourAttributesDto
	{
		public List<OMAttribute> Oks { get; set; }
		public List<OMAttribute> Zu { get; set; }


		public TourAttributesDto()
		{
			Oks = new List<OMAttribute>();
			Zu = new List<OMAttribute>();
		}
	}
}
