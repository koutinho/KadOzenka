using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace KadOzenka.Dal.GbuObject.Dto
{
	public class GbuAttributesTreeDto
	{
		public string Text { get; set; }
		public string Value { get; set; }

		public List<SelectListItem> Items { get; set; }
	}

}