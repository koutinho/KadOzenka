using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Register.DAL;
using Platform.CalendarHolidays;

namespace KadOzenka.Web.Extensions
{
	/// <summary>
	/// Класс custom расширения DateTime.
	/// </summary>
	public static class DateTimeExtention
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
