using System;
using System.Collections.Generic;
using System.Text;

namespace ObjectModel.Sud
{
	public partial class OMSud
	{
		/// <summary>
		/// Сохранение данных с анализом данных
		/// </summary>
		public long SaveAndCheckParam()
		{
			bool _new = Id <= 0;

			bool pNumber = false;
			bool pName = false;
			bool pDate = false;
			bool pSudDate = false;
			bool pStatus = true;

			bool cNumber = false;
			bool cName = false;
			bool cDate = false;
			bool cSudDate = false;
			bool cStatus = true;


			if (!_new)
			{
				var old = OMSud
				.Where(x => x.Id == this.Id)
				.SelectAll()
				.ExecuteFirstOrDefault();

				if (old != null)
				{
					pNumber = OMParam.GetParamStr(OMTableParam.Sud, Id, "number", Number, out bool aNumber, out cNumber);
					pName = OMParam.GetParamStr(OMTableParam.Sud, Id, "name", Name, out bool aName, out cName);
					pDate = OMParam.GetParamDate(OMTableParam.Sud, Id, "date", Date, out bool aDate, out cDate);
					pSudDate = OMParam.GetParamDate(OMTableParam.Sud, Id, "sud_date", SudDate, out bool aSudDate, out cSudDate);
					pStatus = OMParam.GetParamBigInt(OMTableParam.Sud, Id, "status", Status, 0, out bool aStatus, out cStatus, out bool setStatusActual);

					Name = (pName && !aName && cName) ? old.Name : Name;
					Number = (pNumber && !aNumber && cNumber) ? old.Number : Number;
					Date = (pDate && !aDate && cDate) ? old.Date : Date;
					SudDate = (pSudDate && !aSudDate && cSudDate) ? old.SudDate : SudDate;
					Status = (setStatusActual) ? (Status) : ((pStatus && !aStatus && cStatus) ? old.Status : Status);
				}
			}
			long res = Save();
			if (_new)
			{
				OMSudStatus objStatus = new OMSudStatus
				{
					Id = this.Id,
					Number = 1,
					Name = 1,
					Date = 1,
					SudDate = 1,
					Status = 1,
					Astatus = 1
				};
				objStatus.Save();
				OMParam.AddChar(OMTableParam.Sud, this.Id, "number", Number, 1);
				OMParam.AddChar(OMTableParam.Sud, this.Id, "name", Name, 1);
				OMParam.AddDate(OMTableParam.Sud, this.Id, "date", Date, 1);
				OMParam.AddDate(OMTableParam.Sud, this.Id, "sud_date", SudDate, 1);
				OMParam.AddInt(OMTableParam.Sud, this.Id, "status", Status, 1);
			}
			else
			{
				OMSudStatus objStatus = OMSudStatus
				.Where(x => x.Id == this.Id)
				.SelectAll()
				.ExecuteFirstOrDefault();
				if (objStatus != null)
				{
					objStatus.Number = (pNumber && cNumber) ? 1 : 0;
					objStatus.Name = (pName && cName) ? 1 : 0;
					objStatus.Date = (pDate && cDate) ? 1 : 0;
					objStatus.SudDate = (pSudDate && cSudDate) ? 1 : 0;
					objStatus.Status = (pStatus && cStatus) ? 1 : 0;
					objStatus.Astatus = (pNumber && pName && pDate && pSudDate && pStatus && cNumber && cName && cDate && cSudDate && cStatus) ? 1 : 0;
					objStatus.Save();
				}
			}
			return res;
		}
		/// <summary>
		/// Получение утвержденной характеристики Номер судебного дела
		/// </summary>
		public OMParam GetActualNumber()
		{
			return OMParam.GetActual(OMTableParam.Sud, Id, "number");
		}
		/// <summary>
		/// Получение утвержденной характеристики Наименование суда
		/// </summary>
		public OMParam GetActualName()
		{
			return OMParam.GetActual(OMTableParam.Sud, Id, "name");
		}
		/// <summary>
		/// Получение утвержденной характеристики Дата заседания
		/// </summary>
		public OMParam GetActualDate()
		{
			return OMParam.GetActual(OMTableParam.Sud, Id, "date");
		}
		/// <summary>
		/// Получение утвержденной характеристики Дата решения
		/// </summary>
		public OMParam GetActualSudDate()
		{
			return OMParam.GetActual(OMTableParam.Sud, Id, "sud_date");
		}
		/// <summary>
		/// Получение утвержденной характеристики Статус дела
		/// </summary>
		public OMParam GetActualStatus()
		{
			return OMParam.GetActual(OMTableParam.Sud, Id, "status");
		}


		/// <summary>
		/// Получение всех вариантов характеристики Номер судебного дела
		/// </summary>
		public List<OMParam> GetVariantNumber()
		{
			return OMParam.GetParams(OMTableParam.Sud, Id, "number");
		}
		/// <summary>
		/// Получение всех вариантов характеристики Наименование суда
		/// </summary>
		public List<OMParam> GetVariantName()
		{
			return OMParam.GetParams(OMTableParam.Sud, Id, "name");
		}
		/// <summary>
		/// Получение всех вариантов характеристики Дата заседания
		/// </summary>
		public List<OMParam> GetVariantDate()
		{
			return OMParam.GetParams(OMTableParam.Sud, Id, "date");
		}
		/// <summary>
		/// Получение всех вариантов характеристики Дата решения
		/// </summary>
		public List<OMParam> GetVariantSudDate()
		{
			return OMParam.GetParams(OMTableParam.Sud, Id, "sud_date");
		}
		/// <summary>
		/// Получение всех вариантов характеристики Статус дела
		/// </summary>
		public List<OMParam> GetVariantStatus()
		{
			return OMParam.GetParams(OMTableParam.Sud, Id, "status");
		}



		/// <summary>
		/// Утверждение характеристики объекта из выбранных
		/// </summary>
		public bool UpdateAndCheckParam(OMParam pNumber, OMParam pName, OMParam pDate, OMParam pSudDate, OMParam pStatus)
		{
			if (pNumber == null || pName == null || pDate == null || pSudDate == null || pStatus == null)
			{
				throw new ArgumentNullException(nameof(OMParam));
			}

			#region Утверждение выбранных параметров
			pNumber.UpdateStatus(1);
			pName.UpdateStatus(1);
			pDate.UpdateStatus(1);
			pSudDate.UpdateStatus(1);
			pStatus.UpdateStatus(1);
			#endregion

			#region Обновление данных для объекта
			Number = pNumber.ParamChar;
			Name = pName.ParamChar;
			Date = pDate.ParamDate;
			SudDate = pSudDate.ParamDate;
			Status = pStatus.ParamInt;
			Save();
			#endregion

			#region Обновление статусов полей 
			OMSudStatus objStatus = OMSudStatus
			.Where(x => x.Id == this.Id)
			.SelectAll()
			.ExecuteFirstOrDefault();
			if (objStatus != null)
			{
				objStatus.Number = 1;
				objStatus.Name = 1;
				objStatus.Date = 1;
				objStatus.SudDate = 1;
				objStatus.Status = 1;
				objStatus.Astatus = 1;
				objStatus.Save();
			}
			#endregion

			return true;
		}
	}
}
