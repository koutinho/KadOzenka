using Core.Shared.Extensions;
using Core.SRD;
using ObjectModel.Directory.Sud;
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
        public override string ToString()
        {
			long? localParamInt = ParamInt;

			switch (IdTable)
            {
                case 1://Object
                    switch (ParamName)
                    {
                        case "kn":          return ParamChar;
                        case "date":        return (ParamDate == null)  ? string.Empty:ParamDate.Value.ToShortDateString();
                        case "square":      return (ParamDouble == null)? string.Empty:ParamDouble?.ToString("N3");
                        case "kc":          return (ParamDouble == null) ? string.Empty : ParamDouble?.ToString("N3"); 
                        case "stat_dgi":    return ParamChar;
                        case "owner":       return ParamChar;
                        case "adres":       return ParamChar;
                        case "typeobj":     return (ParamInt==null) ? string.Empty:((Directory.Sud.SudObjectType)ParamInt).GetEnumDescription();
                        case "name_center": return ParamChar;
                        default:            return string.Empty;
                    }
                case 2://Otchet
                    switch (ParamName)
                    {
                        case "number":  return ParamChar;
                        case "date":    return (ParamDate == null) ? string.Empty : ParamDate.Value.ToShortDateString();
                        case "date_in": return (ParamDate == null) ? string.Empty : ParamDate.Value.ToShortDateString();
                        case "jalob":   return (ParamInt == null) ? string.Empty : ((ParamInt==1)?"Да":"Нет");
                        case "id_org":  return (ParamInt == null) ? string.Empty : OMDict.Where(x => x.Id == localParamInt).SelectAll().ExecuteFirstOrDefault().Name;
                        case "id_sro":  return (ParamInt == null) ? string.Empty : OMDict.Where(x => x.Id == localParamInt).SelectAll().ExecuteFirstOrDefault().Name;
                        case "id_fio":  return (ParamInt == null) ? string.Empty : OMDict.Where(x => x.Id == localParamInt).SelectAll().ExecuteFirstOrDefault().Name;
                        default:        return string.Empty;
                    }
                case 3://OtchetLink
                    switch (ParamName)
                    {
                        case "use":       return ParamChar;
                        case "descr":     return ParamChar;
                        case "rs":        return (ParamDouble == null) ? string.Empty : ParamDouble?.ToString("N3");
                        case "uprs":      return (ParamDouble == null) ? string.Empty : ParamDouble?.ToString("N3");
                        case "id_otchet": return (ParamInt == null) ? string.Empty : OMOtchet.Where(x => x.Id == localParamInt).SelectAll().ExecuteFirstOrDefault().Number;
                        default:          return string.Empty;
                    }
                case 4://Zak
                    switch (ParamName)
                    {
                        case "number":     return ParamChar;
                        case "date":       return (ParamDate == null) ? string.Empty : ParamDate.Value.ToShortDateString();
                        case "rec_date":   return (ParamDate == null) ? string.Empty : ParamDate.Value.ToShortDateString();
                        case "rec_letter": return ParamChar;
                        case "rec_user":   return ParamChar;
                        case "id_org":     return (ParamInt == null) ? string.Empty : OMDict.Where(x => x.Id == localParamInt).SelectAll().ExecuteFirstOrDefault().Name;
                        case "id_sro":     return (ParamInt == null) ? string.Empty : OMDict.Where(x => x.Id == localParamInt).SelectAll().ExecuteFirstOrDefault().Name;
                        case "id_fio":     return (ParamInt == null) ? string.Empty : OMDict.Where(x => x.Id == localParamInt).SelectAll().ExecuteFirstOrDefault().Name;
                        case "rec_before": return (ParamInt == null) ? string.Empty : ((ParamInt == 1) ? "Да" : "Нет");
                        case "rec_after":  return (ParamInt == null) ? string.Empty : ((ParamInt == 1) ? "Да" : "Нет");
                        case "rec_soglas": return (ParamInt == null) ? string.Empty : ((ParamInt == 1) ? "Да" : "Нет");
                        default:           return string.Empty;
                    }
                case 5://ZakLink
                    switch (ParamName)
                    {
                        case "use":     return ParamChar;
                        case "descr":   return ParamChar;
                        case "rs":      return (ParamDouble == null) ? string.Empty : ParamDouble?.ToString("N3");
                        case "uprs":    return (ParamDouble == null) ? string.Empty : ParamDouble?.ToString("N3");
                        case "id_zak":	return (ParamInt == null) ? string.Empty : OMZak.Where(x => x.Id == localParamInt).SelectAll().ExecuteFirstOrDefault().Number;
                        default:        return string.Empty;
                    }
                case 6://Sud
                    switch (ParamName)
                    {
                        case "number": return ParamChar;
                        case "name": return ParamChar;
                        case "date": return (ParamDate == null) ? string.Empty : ParamDate.Value.ToShortDateString();
                        case "sud_date": return (ParamDate == null) ? string.Empty : ParamDate.Value.ToShortDateString();
                        case "status": return (ParamInt == null) ? string.Empty : ((ParamInt == 0) ? "Без статуса" : ((ParamInt == 1) ? "Удовлетворено" : ((ParamInt == 2) ? "Отказано" : ((ParamInt == 3) ? "Приостановлено" : ((ParamInt == 4) ? "Частично удовлетворено" : string.Empty)))));
                        default: return string.Empty;
                    }
                case 7://SudLink
                    switch (ParamName)
                    {
                        case "use": return ParamChar;
                        case "descr": return ParamChar;
                        case "rs": return (ParamDouble == null) ? string.Empty : ParamDouble?.ToString("N3");
                        case "uprs": return (ParamDouble == null) ? string.Empty : ParamDouble?.ToString("N3");
                        case "id_sud": return (ParamInt == null) ? string.Empty : OMSud.Where(x => x.Id == localParamInt).SelectAll().ExecuteFirstOrDefault().Number;
                        default: return string.Empty;
                    }
                default: return string.Empty;
            }
        }

        /// <summary>
        /// Добавление значения параметра
        /// </summary>
        public static void Add(OMTableParam idTable, long id, string paramName, string paramChar, DateTime paramDate, decimal paramDouble, int paramInt, long idUser, DateTime dateUser, ProcessingStatus paramStatus, OMTypeParam paramType)
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
				ParamStatus_Code = paramStatus,
			};
			param.Save();
		}
		/// <summary>
		/// Добавление строкового параметра
		/// </summary>
		public static void AddChar(OMTableParam idTable, long id, string paramName, string paramChar, ProcessingStatus paramStatus)
		{
			OMParam param = new OMParam
			{
				IdTable = (long)idTable,
				Id = id,
				ParamName = paramName,
				ParamChar = paramChar,
				IdUser = SRDSession.Current.UserID,
				DateUser = DateTime.Now,
				ParamStatus_Code = paramStatus,
			};
			param.Save();
		}
		/// <summary>
		/// Добавление параметра даты
		/// </summary>
		public static void AddDate(OMTableParam idTable, long id, string paramName, DateTime? paramDate, ProcessingStatus paramStatus)
		{
			OMParam param = new OMParam
			{
				IdTable = (long)idTable,
				Id = id,
				ParamName = paramName,
				ParamDate = paramDate,
				IdUser = SRDSession.Current.UserID,
				DateUser = DateTime.Now,
				ParamStatus_Code = paramStatus,
			};
			param.Save();
		}
		/// <summary>
		/// Добавление дробного параметра
		/// </summary>
		public static void AddDouble(OMTableParam idTable, long id, string paramName, decimal? paramDouble, ProcessingStatus paramStatus)
		{
			OMParam param = new OMParam
			{
				IdTable = (long)idTable,
				Id = id,
				ParamName = paramName,
				ParamDouble = paramDouble,
				IdUser = SRDSession.Current.UserID,
				DateUser = DateTime.Now,
				ParamStatus_Code = paramStatus,
			};
			param.Save();
		}
		/// <summary>
		/// Добавление целочисленного параметра
		/// </summary>
		public static void AddInt(OMTableParam idTable, long id, string paramName, long? paramInt, ProcessingStatus paramStatus)
		{
			OMParam param = new OMParam
			{
				IdTable = (long)idTable,
				Id = id,
				ParamName = paramName,
				ParamInt = paramInt,
				IdUser = SRDSession.Current.UserID,
				DateUser = DateTime.Now,
				ParamStatus_Code = paramStatus,
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
					if (item.ParamStatus_Code == ProcessingStatus.Processed)
					{
						current = true;
					}
				}
				if (item.ParamStatus_Code == ProcessingStatus.Processed)
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
					if (item.ParamStatus_Code == ProcessingStatus.Processed)
					{
						current = true;
					}
				}
				if (item.ParamStatus_Code == ProcessingStatus.Processed)
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
					if (item.ParamStatus_Code == ProcessingStatus.Processed)
					{
						current = true;
					}
				}
				if (item.ParamStatus_Code == ProcessingStatus.Processed)
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
					if (item.ParamStatus_Code == ProcessingStatus.Processed)
					{
						current = true;
					}
				}
				if (item.ParamStatus_Code == ProcessingStatus.Processed)
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
						if (item.ParamStatus_Code == ProcessingStatus.Processed) actual = true;
					}
					if (item.ParamStatus_Code == ProcessingStatus.Processed)
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

					var newstatus = ProcessingStatus.InWork;
					if (Items.Count == 1)
					{
						if (Items[0].ParamInt == 0)
						{
							newstatus = ProcessingStatus.Processed;
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
						if (item.ParamStatus_Code == ProcessingStatus.Processed) actual = true;
					}
					if (item.ParamStatus_Code == ProcessingStatus.Processed)
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
					item.UpdateStatus(ProcessingStatus.Processed);
				}
			}
			if (!find)
			{
				AddChar(table, id, paramName, paramChar, ProcessingStatus.Processed);
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
					item.UpdateStatus(ProcessingStatus.Processed);
				}
			}
			if (!find)
			{
				AddDouble(table, id, paramName, paramDouble, ProcessingStatus.Processed);
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
					item.UpdateStatus(ProcessingStatus.Processed);
				}
			}
			if (!find)
			{
				AddDate(table, id, paramName, paramDate, ProcessingStatus.Processed);
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
					item.UpdateStatus(ProcessingStatus.Processed);
				}
			}
			if (!find)
			{
				AddInt(table, id, paramName, paramInt, ProcessingStatus.Processed);
			}
			return true;
		}
		/// <summary>
		/// Обновление статуса параметра
		/// </summary>
		public void UpdateStatus(ProcessingStatus status)
		{
			ParamStatus_Code = status;
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
        /// Получение списка параметров по ид
        /// </summary>
        public static List<OMParam> GetAllParamsById(OMTableParam table, long id)
        {
            return OMParam
                .Where(x => x.IdTable == (long)table && x.Id == id)
                .SelectAll()
                .Execute().ToList();
        }
        /// <summary>
        /// Получение утвержденного параметров по объекту и полю
        /// </summary>
        public static OMParam GetActual(OMTableParam table, long id, string paramName)
		{
			return OMParam
				.Where(x => x.IdTable == (long)table && x.Id == id && x.ParamName == paramName && x.ParamStatus_Code == ProcessingStatus.Processed)
				.SelectAll()
				.ExecuteFirstOrDefault();
		}
	}
}
