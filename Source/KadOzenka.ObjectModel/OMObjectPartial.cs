using ObjectModel.Directory;
using ObjectModel.Directory.Sud;
using System;
using System.Collections.Generic;
using System.Text;



namespace ObjectModel.Sud
{
	public partial class OMObject
	{
		/// <summary>
		/// Сохранение данных с анализом данных
		/// </summary>
		public long SaveAndCheckParam()
		{
			bool _new = Id <= 0;

			bool pKn = false;
			bool pNameCenter = false;
			bool pStatDgi = false;
			bool pOwner = true;
			bool pAdres = false;
			bool pDate = false;
			bool pSquare = false;
			bool pKc = false;
			bool pTypeobj = false;

			bool cKn = false;
			bool cNameCenter = false;
			bool cStatDgi = false;
			bool cAdres = false;
			bool cDate = false;
			bool cSquare = false;
			bool cKc = false;
			bool cType = false;


			if (!_new)
			{
				var old = OMObject
				.Where(x => x.Id == this.Id)
				.SelectAll()
				.ExecuteFirstOrDefault();

				if (old != null)
				{
					pKn = OMParam.GetParamStr(OMTableParam.Object, Id, "kn", Kn, out bool aKn, out cKn);
					pNameCenter = OMParam.GetParamStr(OMTableParam.Object, Id, "name_center", NameCenter, out bool aNameCenter, out cNameCenter);
					pStatDgi = OMParam.GetParamStr(OMTableParam.Object, Id, "stat_dgi", StatDgi, out bool aStatDgi, out cStatDgi);
					pAdres = OMParam.GetParamStr(OMTableParam.Object, Id, "adres", Adres, out bool aAdres, out cAdres);
					pOwner = OMParam.GetParamStrActual(OMTableParam.Object, Id, "owner", Owner);
					pDate = OMParam.GetParamDate(OMTableParam.Object, Id, "date", Date, out bool aDate, out cDate);
					pSquare = OMParam.GetParamDecimal(OMTableParam.Object, Id, "square", Square, out bool aSquare, out cSquare);
					pKc = OMParam.GetParamDecimal(OMTableParam.Object, Id, "kc", Kc, out bool aKc, out cKc);
					pTypeobj = OMParam.GetParamInt(OMTableParam.Object, Id, "typeobj", (long)Typeobj_Code, out bool aType, out cType);

					Kn = (pKn && !cKn && aKn) ? old.Kn : Kn;
					NameCenter = (pNameCenter && !cNameCenter && aNameCenter) ? old.NameCenter : NameCenter;
					StatDgi = (pStatDgi && !cStatDgi && aStatDgi) ? old.StatDgi : StatDgi;
					Adres = (pAdres && !aAdres && cAdres) ? old.Adres : Adres;
					Date = (pDate && !cDate && aDate) ? old.Date : Date;
					Square = (pSquare && !aSquare && cSquare) ? old.Square : Square;
					Kc = (pKc && !aKc && cKc) ? old.Kc : Kc;
					Typeobj = (pTypeobj && !aType && cType) ? old.Typeobj : Typeobj;
				}
			}
			long res = Save();
			if (_new)
			{
				OMObjectStatus objStatus = new OMObjectStatus
				{
					Id = this.Id,
					Kn = 1,
					Date = 1,
					Square = 1,
					Kc = 1,
					NameCenter = 1,
					StatDgi = 1,
					Owner = 1,
					Adres = 1,
					Typeobj = 1,
					Status = 1
				};
				objStatus.Save();
				OMParam.AddChar(OMTableParam.Object, this.Id, "kn", Kn, ProcessingStatus.Processed);
				OMParam.AddChar(OMTableParam.Object, this.Id, "name_center", NameCenter, ProcessingStatus.Processed);
				OMParam.AddChar(OMTableParam.Object, this.Id, "stat_dgi", StatDgi, ProcessingStatus.Processed);
				OMParam.AddChar(OMTableParam.Object, this.Id, "adres", Adres, ProcessingStatus.Processed);
				OMParam.AddDate(OMTableParam.Object, this.Id, "date", Date, ProcessingStatus.Processed);
				OMParam.AddDouble(OMTableParam.Object, this.Id, "square", Square, ProcessingStatus.Processed);
				OMParam.AddDouble(OMTableParam.Object, this.Id, "kc", Kc, ProcessingStatus.Processed);
				OMParam.AddInt(OMTableParam.Object, this.Id, "typeobj", (long)Typeobj_Code, ProcessingStatus.Processed);
			}
			else
			{
				OMObjectStatus objStatus = OMObjectStatus
				.Where(x => x.Id == this.Id)
				.SelectAll()
				.ExecuteFirstOrDefault();
				if (objStatus != null)
				{
					objStatus.Kn = (pKn && cKn) ? 1 : 0;
					objStatus.Date = (pDate && cDate) ? 1 : 0;
					objStatus.Square = (pSquare && cSquare) ? 1 : 0;
					objStatus.Kc = (pKc && cKc) ? 1 : 0;
					objStatus.NameCenter = (pNameCenter && cNameCenter) ? 1 : 0;
					objStatus.StatDgi = (pStatDgi && cStatDgi) ? 1 : 0;
					objStatus.Owner = (pOwner) ? 1 : 0;
					objStatus.Adres = (pAdres && cAdres) ? 1 : 0;
					objStatus.Typeobj = (pTypeobj && cType) ? 1 : 0;
					objStatus.Status = (pKn && pDate && pSquare && pKc && pNameCenter && pStatDgi && pOwner && pAdres && pTypeobj && cKn && cDate && cSquare && cKc && cNameCenter && cStatDgi && cAdres && cType) ? 1 : 0;
					objStatus.Save();
				}
			}
			return res;
		}
		/// <summary>
		/// Получение утвержденной характеристики Кадастровый номер
		/// </summary>
		public OMParam GetActualKn()
		{
			return OMParam.GetActual(OMTableParam.Object, Id, "kn");
		}
		/// <summary>
		/// Получение утвержденной характеристики Наименование ТЦ/БЦ
		/// </summary>
		public OMParam GetActualNameCenter()
		{
			return OMParam.GetActual(OMTableParam.Object, Id, "name_center");
		}
		/// <summary>
		/// Получение утвержденной характеристики Внесено в статистику ДГИ
		/// </summary>
		public OMParam GetActualStatDgi()
		{
			return OMParam.GetActual(OMTableParam.Object, Id, "stat_dgi");
		}
		/// <summary>
		/// Получение утвержденной характеристики Адрес
		/// </summary>
		public OMParam GetActualAdres()
		{
			return OMParam.GetActual(OMTableParam.Object, Id, "adres");
		}
		/// <summary>
		/// Получение утвержденной характеристики Заказчик/Истец
		/// </summary>
		public OMParam GetActualOwner()
		{
			return OMParam.GetActual(OMTableParam.Object, Id, "owner");
		}
		/// <summary>
		/// Получение утвержденной характеристики Дата определения кадастровой стоимости
		/// </summary>
		public OMParam GetActualDate()
		{
			return OMParam.GetActual(OMTableParam.Object, Id, "date");
		}
		/// <summary>
		/// Получение утвержденной характеристики Площадь
		/// </summary>
		public OMParam GetActualSquare()
		{
			return OMParam.GetActual(OMTableParam.Object, Id, "square");
		}
		/// <summary>
		/// Получение утвержденной характеристики Кадастровая стоимость
		/// </summary>
		public OMParam GetActualKc()
		{
			return OMParam.GetActual(OMTableParam.Object, Id, "kc");
		}
		/// <summary>
		/// Получение утвержденной характеристики Тип объекта
		/// </summary>
		public OMParam GetActualTypeobj()
		{
			return OMParam.GetActual(OMTableParam.Object, Id, "typeobj");
		}

		/// <summary>
		/// Получение всех вариантов характеристики Кадастровый номер
		/// </summary>
		public List<OMParam> GetVariantKn()
		{
			return OMParam.GetParams(OMTableParam.Object, Id, "kn");
		}
		/// <summary>
		/// Получение всех вариантов характеристики Наименование ТЦ/БЦ
		/// </summary>
		public List<OMParam> GetVariantNameCenter()
		{
			return OMParam.GetParams(OMTableParam.Object, Id, "name_center");
		}
		/// <summary>
		/// Получение всех вариантов характеристики Внесено в статистику ДГИ
		/// </summary>
		public List<OMParam> GetVariantStatDgi()
		{
			return OMParam.GetParams(OMTableParam.Object, Id, "stat_dgi");
		}
		/// <summary>
		/// Получение всех вариантов характеристики Адрес
		/// </summary>
		public List<OMParam> GetVariantAdres()
		{
			return OMParam.GetParams(OMTableParam.Object, Id, "adres");
		}
		/// <summary>
		/// Получение всех вариантов характеристики Заказчик/Истец
		/// </summary>
		public List<OMParam> GetVariantOwner()
		{
			return OMParam.GetParams(OMTableParam.Object, Id, "owner");
		}
		/// <summary>
		/// Получение всех вариантов характеристики Дата определения кадастровой стоимости
		/// </summary>
		public List<OMParam> GetVariantDate()
		{
			return OMParam.GetParams(OMTableParam.Object, Id, "date");
		}
		/// <summary>
		/// Получение всех вариантов характеристики Площадь
		/// </summary>
		public List<OMParam> GetVariantSquare()
		{
			return OMParam.GetParams(OMTableParam.Object, Id, "square");
		}
		/// <summary>
		/// Получение всех вариантов характеристики Кадастровая стоимость
		/// </summary>
		public List<OMParam> GetVariantKc()
		{
			return OMParam.GetParams(OMTableParam.Object, Id, "kc");
		}
		/// <summary>
		/// Получение всех вариантов характеристики Тип объекта
		/// </summary>
		public List<OMParam> GetVariantTypeobj()
		{
			return OMParam.GetParams(OMTableParam.Object, Id, "typeobj");
		}

		/// <summary>
		/// Утверждение характеристики объекта из выбранных
		/// </summary>
		public bool UpdateAndCheckParam(OMParam pKn, OMParam pType, OMParam pSquare, OMParam pKc, OMParam pDate, OMParam pNameCenter, OMParam pStatDgi, OMParam pAdres, OMParam pOwner)
		{
			if (pKn == null || pType == null || pSquare == null || pKc == null || pDate == null || pNameCenter == null || pStatDgi == null || pAdres == null || pOwner == null)
			{
				throw new ArgumentNullException(nameof(OMParam));
			}

			#region Утверждение выбранных параметров
			pKn.UpdateStatus(ProcessingStatus.Processed);
			pType.UpdateStatus(ProcessingStatus.Processed);
			pSquare.UpdateStatus(ProcessingStatus.Processed);
			pKc.UpdateStatus(ProcessingStatus.Processed);
			pDate.UpdateStatus(ProcessingStatus.Processed);
			pNameCenter.UpdateStatus(ProcessingStatus.Processed);
			pStatDgi.UpdateStatus(ProcessingStatus.Processed);
			pAdres.UpdateStatus(ProcessingStatus.Processed);
			pOwner.UpdateStatus(ProcessingStatus.Processed);
			#endregion

			#region Обновление данных для объекта
			Kn = pKn.ParamChar;
			Typeobj_Code = (SudObjectType)pType.ParamInt;
			Square = pSquare.ParamDouble;
			Kc = pKc.ParamDouble;
			Date = pDate.ParamDate;
			NameCenter = pNameCenter.ParamChar;
			StatDgi = pStatDgi.ParamChar;
			Adres = pAdres.ParamChar;
			Owner = pOwner.ParamChar;
			Save();
			#endregion

			#region Обновление статусов полей 
			OMObjectStatus objStatus = OMObjectStatus
			.Where(x => x.Id == this.Id)
			.SelectAll()
			.ExecuteFirstOrDefault();
			if (objStatus != null)
			{
				objStatus.Kn = 1;
				objStatus.Date = 1;
				objStatus.Square = 1;
				objStatus.Kc = 1;
				objStatus.NameCenter = 1;
				objStatus.StatDgi = 1;
				objStatus.Owner = 1;
				objStatus.Adres = 1;
				objStatus.Typeobj = 1;
				objStatus.Status = 1;
				objStatus.Save();
			}
			#endregion

			return true;
		}
	}
}

