using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ObjectModel.Sud
{
	/// <summary>
	/// Тип параметров
	/// </summary>
	public enum OMTypeParam : long
	{
		Char = 0,
		Date = 1,
		Double = 2,
		Int = 3,
		TypeObj = 4,
		Bool = 5,
		BigInt = 6,
		Dict = 7,
		Otchet = 8,
		Zak = 9,
		Sud = 10,
		SudStatus = 11
	}
	/// <summary>
	/// Тип таблиц
	/// </summary>
	public enum OMTableParam : long
	{
		Object = 1,
		Otchet = 2,
		OtchetLink = 3,
		Zak = 4,
		ZakLink = 5,
		Sud = 6,
		SudLink = 7
	}

	public partial class OMParam
	{
		/// <summary>
		/// Добавление значения параметра
		/// </summary>
		public static void Add(OMTableParam idTable, long id, string paramName, string paramChar, DateTime paramDate, decimal paramDouble, int paramInt, long idUser, DateTime dateUser, int paramStatus, OMTypeParam paramType)
		{
			OMParam param = new OMParam
			{
				IdTable = (long)idTable,
				Id = id,
				ParamName = paramName,
				ParamChar = paramChar,
				ParamDate = paramDate,
				ParamDouble = paramDouble,
				ParamInt = paramInt,
				IdUser = idUser,
				DateUser = dateUser,
				ParamStatus = paramStatus,
			};
			param.Save();
		}
		/// <summary>
		/// Добавление строкового параметра
		/// </summary>
		public static void AddChar(OMTableParam idTable, long id, string paramName, string paramChar, int paramStatus)
		{
			OMParam param = new OMParam
			{
				IdTable = (long)idTable,
				Id = id,
				ParamName = paramName,
				ParamChar = paramChar,
				IdUser = 1,
				DateUser = DateTime.Now,
				ParamStatus = paramStatus,
			};
			param.Save();
		}
		/// <summary>
		/// Добавление параметра даты
		/// </summary>
		public static void AddDate(OMTableParam idTable, long id, string paramName, DateTime? paramDate, int paramStatus)
		{
			OMParam param = new OMParam
			{
				IdTable = (long)idTable,
				Id = id,
				ParamName = paramName,
				ParamDate = paramDate,
				IdUser = 1,
				DateUser = DateTime.Now,
				ParamStatus = paramStatus,
			};
			param.Save();
		}
		/// <summary>
		/// Добавление дробного параметра
		/// </summary>
		public static void AddDouble(OMTableParam idTable, long id, string paramName, decimal? paramDouble, int paramStatus)
		{
			OMParam param = new OMParam
			{
				IdTable = (long)idTable,
				Id = id,
				ParamName = paramName,
				ParamDouble = paramDouble,
				IdUser = 1,
				DateUser = DateTime.Now,
				ParamStatus = paramStatus,
			};
			param.Save();
		}
		/// <summary>
		/// Добавление целочисленного параметра
		/// </summary>
		public static void AddInt(OMTableParam idTable, long id, string paramName, long? paramInt, int paramStatus)
		{
			OMParam param = new OMParam
			{
				IdTable = (long)idTable,
				Id = id,
				ParamName = paramName,
				ParamInt = paramInt,
				IdUser = 1,
				DateUser = DateTime.Now,
				ParamStatus = paramStatus,
			};
			param.Save();
		}
		/// <summary>
		/// Получение статуса параметра по его значению (возвращаемое значение - true(найден)/false(не найден), actual - есть/нет утвержденное значение, current - текущее значение утвержденное да/нет)
		/// </summary>
		public static bool GetParamStr(OMTableParam table, long id, string paramName, string paramChar, out bool actual, out bool current)
		{
			actual = false;
			current = false;
			bool find = false;
			List<OMParam> Items = OMParam
		   .Where(x => x.IdTable == (long)table && x.Id == id && x.ParamName == paramName)
		   .SelectAll()
		   .Execute().ToList();

			foreach (OMParam item in Items)
			{
				if (item.ParamChar == paramChar)
				{
					find = true;
					if (item.ParamStatus == 1)
					{
						current = true;
					}
				}
				if (item.ParamStatus == 1)
				{
					actual = true;
				}
			}
			if (!find)
			{
				foreach (OMParam item in Items)
				{
					item.UpdateStatus(0);
				}
				AddChar(table, id, paramName, paramChar, 0);
			}
			return find;
		}
		/// <summary>
		/// Получение статуса параметра по его значению (возвращаемое значение - true(найден)/false(не найден), actual - есть/нет утвержденное значение, current - текущее значение утвержденное да/нет)
		/// </summary>
		public static bool GetParamDate(OMTableParam table, long id, string paramName, DateTime? paramDate, out bool actual, out bool current)
		{
			actual = false;
			current = false;
			bool find = false;
			List<OMParam> Items = OMParam
		   .Where(x => x.IdTable == (long)table && x.Id == id && x.ParamName == paramName)
		   .SelectAll()
		   .Execute().ToList();

			foreach (OMParam item in Items)
			{
				if (item.ParamDate == paramDate)
				{
					find = true;
					if (item.ParamStatus == 1)
					{
						current = true;
					}
				}
				if (item.ParamStatus == 1)
				{
					actual = true;
				}
			}
			if (!find)
			{
				foreach (OMParam item in Items)
				{
					item.UpdateStatus(0);
				}
				AddDate(table, id, paramName, paramDate, 0);
			}
			return find;
		}
		/// <summary>
		/// Получение статуса параметра по его значению (возвращаемое значение - true(найден)/false(не найден), actual - есть/нет утвержденное значение, current - текущее значение утвержденное да/нет)
		/// </summary>
		public static bool GetParamDecimal(OMTableParam table, long id, string paramName, decimal? paramDouble, out bool actual, out bool current)
		{
			actual = false;
			current = false;
			bool find = false;
			List<OMParam> Items = OMParam
		   .Where(x => x.IdTable == (long)table && x.Id == id && x.ParamName == paramName)
		   .SelectAll()
		   .Execute().ToList();

			foreach (OMParam item in Items)
			{
				if (item.ParamDouble == paramDouble)
				{
					find = true;
					if (item.ParamStatus == 1)
					{
						current = true;
					}
				}
				if (item.ParamStatus == 1)
				{
					actual = true;
				}
			}
			if (!find)
			{
				foreach (OMParam item in Items)
				{
					item.UpdateStatus(0);
				}
				AddDouble(table, id, paramName, paramDouble, 0);
			}
			return find;
		}
		/// <summary>
		/// Получение статуса параметра по его значению (возвращаемое значение - true(найден)/false(не найден), actual - есть/нет утвержденное значение, current - текущее значение утвержденное да/нет)
		/// </summary>
		public static bool GetParamInt(OMTableParam table, long id, string paramName, long? paramInt, out bool actual, out bool current)
		{
			actual = false;
			current = false;
			bool find = false;
			List<OMParam> Items = OMParam
		   .Where(x => x.IdTable == (long)table && x.Id == id && x.ParamName == paramName)
		   .SelectAll()
		   .Execute().ToList();

			foreach (OMParam item in Items)
			{
				if (item.ParamInt == paramInt)
				{
					find = true;
					if (item.ParamStatus == 1)
					{
						current = true;
					}
				}
				if (item.ParamStatus == 1)
				{
					actual = true;
				}
			}
			if (!find)
			{
				foreach (OMParam item in Items)
				{
					item.UpdateStatus(0);
				}
				AddInt(table, id, paramName, paramInt, 0);
			}
			return find;
		}
		/// <summary>
		/// Получение статуса параметра по его значению (возвращаемое значение - true(найден)/false(не найден), actual - есть/нет утвержденное значение, current - текущее значение утвержденное да/нет)
		/// </summary>
		public static bool GetParamBigInt(OMTableParam table, long id, string paramName, long? paramInt, long change_value, out bool actual, out bool current, out bool setactual)
		{
			if (paramInt != change_value && change_value == 0)
			{
				setactual = false;
				actual = false;
				current = false;
				bool find = false;
				List<OMParam> Items = OMParam
			   .Where(x => x.IdTable == (long)table && x.Id == id && x.ParamName == paramName)
			   .SelectAll()
			   .Execute().ToList();
				foreach (OMParam item in Items)
				{
					if (item.ParamInt == paramInt)
					{
						find = true;
						if (item.ParamStatus == 1) actual = true;
					}
					if (item.ParamStatus == 1)
					{
						current = true;
					}
				}
				if (!find)
				{
					foreach (OMParam item in Items)
					{
						item.UpdateStatus(0);
					}

					int newstatus = 0;
					if (Items.Count == 1)
					{
						if (Items[0].ParamInt == 0)
						{
							newstatus = 1;
							setactual = true;
						}
					}

					OMParam.AddInt(table, id, paramName, paramInt, newstatus);
				}
				return find;
			}
			else
			{
				setactual = false;
				actual = false;
				current = false;
				bool find = false;
				List<OMParam> Items = OMParam
			   .Where(x => x.IdTable == (long)table && x.Id == id && x.ParamName == paramName)
			   .SelectAll()
			   .Execute().ToList();
				foreach (OMParam item in Items)
				{
					if (item.ParamInt == paramInt)
					{
						find = true;
						if (item.ParamStatus == 1) actual = true;
					}
					if (item.ParamStatus == 1)
					{
						current = true;
					}
				}
				if (!find)
				{
					foreach (OMParam item in Items)
					{
						item.UpdateStatus(0);
					}
					OMParam.AddInt(table, id, paramName, paramInt, 0);
				}
				return find;
			}
		}
		/// <summary>
		/// Получение статуса параметра по его значению (возвращаемое значение - true(найден)/false(не найден) с одновременным утверждением текущего значения)
		/// </summary>
		public static bool GetParamStrActual(OMTableParam table, long id, string paramName, string paramChar)
		{
			bool find = false;
			List<OMParam> Items = OMParam
		   .Where(x => x.IdTable == (long)table && x.Id == id && x.ParamName == paramName)
		   .SelectAll()
		   .Execute().ToList();

			foreach (OMParam item in Items)
			{
				item.UpdateStatus(0);
			}
			foreach (OMParam item in Items)
			{
				if (item.ParamChar == paramChar)
				{
					find = true;
					item.UpdateStatus(1);
				}
			}
			if (!find)
			{
				AddChar(table, id, paramName, paramChar, 1);
			}
			return true;
		}
		/// <summary>
		/// Получение статуса параметра по его значению (возвращаемое значение - true(найден)/false(не найден) с одновременным утверждением текущего значения)
		/// </summary>
		public static bool GetParamDecimalActual(OMTableParam table, long id, string paramName, decimal? paramDouble)
		{
			bool find = false;
			List<OMParam> Items = OMParam
		   .Where(x => x.IdTable == (long)table && x.Id == id && x.ParamName == paramName)
		   .SelectAll()
		   .Execute().ToList();

			foreach (OMParam item in Items)
			{
				item.UpdateStatus(0);
			}
			foreach (OMParam item in Items)
			{
				if (item.ParamDouble == paramDouble)
				{
					find = true;
					item.UpdateStatus(1);
				}
			}
			if (!find)
			{
				AddDouble(table, id, paramName, paramDouble, 1);
			}
			return true;
		}
		/// <summary>
		/// Получение статуса параметра по его значению (возвращаемое значение - true(найден)/false(не найден) с одновременным утверждением текущего значения)
		/// </summary>
		public static bool GetParamDateActual(OMTableParam table, long id, string paramName, DateTime? paramDate)
		{
			bool find = false;
			List<OMParam> Items = OMParam
		   .Where(x => x.IdTable == (long)table && x.Id == id && x.ParamName == paramName)
		   .SelectAll()
		   .Execute().ToList();

			foreach (OMParam item in Items)
			{
				item.UpdateStatus(0);
			}
			foreach (OMParam item in Items)
			{
				if (item.ParamDate == paramDate)
				{
					find = true;
					item.UpdateStatus(1);
				}
			}
			if (!find)
			{
				AddDate(table, id, paramName, paramDate, 1);
			}
			return true;
		}
		/// <summary>
		/// Получение статуса параметра по его значению (возвращаемое значение - true(найден)/false(не найден) с одновременным утверждением текущего значения)
		/// </summary>
		public static bool GetParamIntActual(OMTableParam table, long id, string paramName, long? paramInt)
		{
			bool find = false;
			List<OMParam> Items = OMParam
		   .Where(x => x.IdTable == (long)table && x.Id == id && x.ParamName == paramName)
		   .SelectAll()
		   .Execute().ToList();

			foreach (OMParam item in Items)
			{
				item.UpdateStatus(0);
			}
			foreach (OMParam item in Items)
			{
				if (item.ParamInt == paramInt)
				{
					find = true;
					item.UpdateStatus(1);
				}
			}
			if (!find)
			{
				AddInt(table, id, paramName, paramInt, 1);
			}
			return true;
		}
		/// <summary>
		/// Обновление статуса параметра
		/// </summary>
		public void UpdateStatus(long status)
		{
			ParamStatus = status;
			Save();
		}
		/// <summary>
		/// Получение списка параметров по объекту и полю
		/// </summary>
		public static List<OMParam> GetParams(OMTableParam table, long id, string paramName)
		{
			return OMParam
		   .Where(x => x.IdTable == (long)table && x.Id == id && x.ParamName == paramName)
		   .SelectAll()
		   .Execute().ToList();
		}
		/// <summary>
		/// Получение утвержденного параметров по объекту и полю
		/// </summary>
		public static OMParam GetActual(OMTableParam table, long id, string paramName)
		{
			return OMParam
				.Where(x => x.IdTable == (long)table && x.Id == id && x.ParamName == paramName && x.ParamStatus == 1)
				.SelectAll()
				.ExecuteFirstOrDefault();
		}
	}
}
