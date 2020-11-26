using System;
using Platform.CalendarHolidays;

namespace KadOzenka.Dal.Extentions
{
	public static class DateTimeExtentions
	{
		public static DateTime GetStartWorkDate(this DateTime endDate, long workDaysCount)
		{
			var startDate = endDate.AddDays(-workDaysCount);
			while (CalendarHolidays.GetWorkDaysCount(startDate, endDate) < workDaysCount)
			{
				startDate = startDate.AddDays(-1);
			}

			return startDate;
		}
	}
}
