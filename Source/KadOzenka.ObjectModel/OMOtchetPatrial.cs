using System;
using System.Collections.Generic;
using System.Text;

namespace ObjectModel.Sud
{
	public partial class OMOtchet
	{
		/// <summary>
		/// Сохранение данных с анализом данных
		/// </summary>
		public long SaveAndCheckParam()
		{
			bool _new = Id <= 0;

			bool pNumber = false;
			bool pDate = false;
			bool pDateIn = false;
			bool pJalob = false;
			bool pOrg = true;
			bool pFio = true;
			bool pSro = true;

			bool cNumber = false;
			bool cDate = false;
			bool cDateIn = false;
			bool cJalob = false;
			bool cOrg = true;
			bool cFio = true;
			bool cSro = true;


			if (!_new)
			{
				var old = OMOtchet
				.Where(x => x.Id == this.Id)
				.SelectAll()
				.ExecuteFirstOrDefault();

				if (old != null)
				{
					pNumber = OMParam.GetParamStr(OMTableParam.Otchet, Id, "number", Number, out bool aNumber, out cNumber);
					pDate = OMParam.GetParamDate(OMTableParam.Otchet, Id, "date", Date, out bool aDate, out cDate);
					pDateIn = OMParam.GetParamDate(OMTableParam.Otchet, Id, "date_in", DateIn, out bool aDateIn, out cDateIn);
					pJalob = OMParam.GetParamInt(OMTableParam.Otchet, Id, "jalob", Jalob, out bool aJalob, out cJalob);
					pOrg = OMParam.GetParamInt(OMTableParam.Otchet, Id, "id_org", IdOrg, out bool aOrg, out cOrg);
					pFio = OMParam.GetParamInt(OMTableParam.Otchet, Id, "id_fio", IdFio, out bool aFio, out cFio);
					pSro = OMParam.GetParamInt(OMTableParam.Otchet, Id, "id_sro", IdSro, out bool aSro, out cSro);

					Number = (pNumber && !aNumber && cNumber) ? old.Number : Number;
					Date = (pDate && !aDate && cDate) ? old.Date : Date;
					DateIn = (pDateIn && !aDateIn && cDateIn) ? old.DateIn : DateIn;
					Jalob = (pJalob && !aJalob && cJalob) ? old.Jalob : Jalob;
					IdOrg = (pOrg && !aOrg && cOrg) ? old.IdOrg : IdOrg;
					IdFio = (pFio && !aFio && cFio) ? old.IdFio : IdFio;
					IdSro = (pSro && !aSro && cSro) ? old.IdSro : IdSro;
					Org = (pOrg && !aOrg && cOrg) ? old.Org : Org;
					Fio = (pFio && !aFio && cFio) ? old.Fio : Fio;
					Sro = (pSro && !aSro && cSro) ? old.Sro : Sro;
				}
			}
			long res = Save();
			if (_new)
			{
				OMOtchetStatus objStatus = new OMOtchetStatus
				{
					Id = this.Id,
					Number = 1,
					DateIn = 1,
					Date = 1,
					Jalob = 1,
					IdOrg = 1,
					IdFio = 1,
					IdSro = 1,
					Status = 1
				};
				objStatus.Save();

				OMParam.AddChar(OMTableParam.Otchet, this.Id, "number", Number, 1);
				OMParam.AddDate(OMTableParam.Otchet, this.Id, "date", Date, 1);
				OMParam.AddDate(OMTableParam.Otchet, this.Id, "date_in", DateIn, 1);
				OMParam.AddInt(OMTableParam.Otchet, this.Id, "jalob", Jalob, 1);
				OMParam.AddInt(OMTableParam.Otchet, this.Id, "id_org", IdOrg, 1);
				OMParam.AddInt(OMTableParam.Otchet, this.Id, "id_sro", IdSro, 1);
				OMParam.AddInt(OMTableParam.Otchet, this.Id, "id_fio", IdFio, 1);
			}
			else
			{
				OMOtchetStatus objStatus = OMOtchetStatus
				.Where(x => x.Id == this.Id)
				.SelectAll()
				.ExecuteFirstOrDefault();
				if (objStatus != null)
				{
					objStatus.Number = (pNumber && cNumber) ? 1 : 0;
					objStatus.Date = (pDate && cDate) ? 1 : 0;
					objStatus.DateIn = (pDateIn && cDateIn) ? 1 : 0;
					objStatus.Jalob = (pJalob && cJalob) ? 1 : 0;
					objStatus.IdOrg = (pOrg && cOrg) ? 1 : 0;
					objStatus.IdFio = (pFio && cFio) ? 1 : 0;
					objStatus.IdSro = (pSro && cSro) ? 1 : 0;
					objStatus.Status = (pNumber && pDate && pDateIn && pJalob && pOrg && pFio && pSro && cNumber && cDate && cDateIn && cJalob && cOrg && cFio && cSro) ? 1 : 0;
					objStatus.Save();
				}
			}
			return res;
		}

		/// <summary>
		/// Получение утвержденной характеристики Номер отчета
		/// </summary>
		public OMParam GetActualNumber()
		{
			return OMParam.GetActual(OMTableParam.Otchet, Id, "number");
		}
		/// <summary>
		/// Получение утвержденной характеристики Дата отчета
		/// </summary>
		public OMParam GetActualDate()
		{
			return OMParam.GetActual(OMTableParam.Otchet, Id, "date");
		}
		/// <summary>
		/// Получение утвержденной характеристики Дата получения
		/// </summary>
		public OMParam GetActualDateIn()
		{
			return OMParam.GetActual(OMTableParam.Otchet, Id, "date_in");
		}
		/// <summary>
		/// Получение утвержденной характеристики Жалоба
		/// </summary>
		public OMParam GetActualJalob()
		{
			return OMParam.GetActual(OMTableParam.Otchet, Id, "jalob");
		}
		/// <summary>
		/// Получение утвержденной характеристики СРО
		/// </summary>
		public OMParam GetActualIdSro()
		{
			return OMParam.GetActual(OMTableParam.Otchet, Id, "id_sro");
		}
		/// <summary>
		/// Получение утвержденной характеристики ФИО
		/// </summary>
		public OMParam GetActualIdFio()
		{
			return OMParam.GetActual(OMTableParam.Otchet, Id, "id_fio");
		}
		/// <summary>
		/// Получение утвержденной характеристики Организация
		/// </summary>
		public OMParam GetActualIdOrg()
		{
			return OMParam.GetActual(OMTableParam.Otchet, Id, "id_org");
		}


		/// <summary>
		/// Получение всех значений характеристики Номер отчета
		/// </summary>
		public List<OMParam> GetParamsNumber()
		{
			return OMParam.GetParams(OMTableParam.Otchet, Id, "number");
		}
		/// <summary>
		/// Получение всех значений характеристики Дата отчета
		/// </summary>
		public List<OMParam> GetParamsDate()
		{
			return OMParam.GetParams(OMTableParam.Otchet, Id, "date");
		}
		/// <summary>
		/// Получение всех значений характеристики Дата получения
		/// </summary>
		public List<OMParam> GetParamsDateIn()
		{
			return OMParam.GetParams(OMTableParam.Otchet, Id, "date_in");
		}
		/// <summary>
		/// Получение всех значений характеристики Жалоба
		/// </summary>
		public List<OMParam> GetParamsJalob()
		{
			return OMParam.GetParams(OMTableParam.Otchet, Id, "jalob");
		}
		/// <summary>
		/// Получение всех значений характеристики СРО
		/// </summary>
		public List<OMParam> GetParamsIdSro()
		{
			return OMParam.GetParams(OMTableParam.Otchet, Id, "id_sro");
		}
		/// <summary>
		/// Получение всех значений характеристики ФИО
		/// </summary>
		public List<OMParam> GetParamsIdFio()
		{
			return OMParam.GetParams(OMTableParam.Otchet, Id, "id_fio");
		}
		/// <summary>
		/// Получение всех значений характеристики Организация
		/// </summary>
		public List<OMParam> GetParamsIdOrg()
		{
			return OMParam.GetParams(OMTableParam.Otchet, Id, "id_org");
		}

		/// <summary>
		/// Утверждение характеристики объекта из выбранных
		/// </summary>
		public bool UpdateAndCheckParam(OMParam pNumber, OMParam pDate, OMParam pDateIn, OMParam pJalob, OMParam pOrg, OMParam pFio, OMParam pSro)
		{
			if (pNumber == null || pDate == null || pDateIn == null || pJalob == null || pOrg == null || pFio == null || pSro == null)
			{
				throw new ArgumentNullException(nameof(OMParam));
			}

			#region Утверждение выбранных параметров
			pNumber.UpdateStatus(1);
			pDate.UpdateStatus(1);
			pDateIn.UpdateStatus(1);
			pJalob.UpdateStatus(1);
			pOrg.UpdateStatus(1);
			pFio.UpdateStatus(1);
			pSro.UpdateStatus(1);
			#endregion

			#region Обновление данных для объекта
			Number = pNumber.ParamChar;
			Date = pDate.ParamDate;
			DateIn = pDateIn.ParamDate;
			Jalob = pJalob.ParamInt;
			IdOrg = pOrg.ParamInt;
			IdFio = pFio.ParamInt;
			IdSro = pSro.ParamInt;
			Save();
			#endregion

			#region Обновление статусов полей 
			OMOtchetStatus objStatus = OMOtchetStatus
			.Where(x => x.Id == this.Id)
			.SelectAll()
			.ExecuteFirstOrDefault();
			if (objStatus != null)
			{
				objStatus.Number = 1;
				objStatus.Date = 1;
				objStatus.DateIn = 1;
				objStatus.Jalob = 1;
				objStatus.IdSro = 1;
				objStatus.IdFio = 1;
				objStatus.IdOrg = 1;
				objStatus.Status = 1;
				objStatus.Save();
			}
			#endregion

			return true;
		}
	}
}
