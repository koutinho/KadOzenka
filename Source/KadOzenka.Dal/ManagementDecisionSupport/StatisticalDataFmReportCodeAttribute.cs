using System;

namespace KadOzenka.Dal.ManagementDecisionSupport
{
	public class StatisticalDataFmReportCodeAttribute : Attribute
	{
		public string Code { get; }

		public StatisticalDataFmReportCodeAttribute(int code)
		{
			Code = code.ToString();
		}
	}
}
