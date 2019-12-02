using ObjectModel.Directory.Sud;
using ObjectModel.Sud;
using System;
using System.Collections.Generic;

namespace ObjectModel.Sud
{
	public partial class OMZak
	{
		/// <summary>
		/// Сохранение данных с анализом данных
		/// </summary>
		public long SaveAndCheckParam()
		{
			bool _new = Id <= 0;

			bool pNumber = false;
			bool pDate = false;
			bool pRecDate = false;
			bool pRecLetter = false;
			bool pRecUser = false;
			bool pRecBefore = false;
			bool pRecAfter = false;
			bool pRecSoglas = false;
			bool pOrg = false;
			bool pFio = false;
			bool pSro = false;

			bool cNumber = false;
			bool cDate = false;
			bool cRecLetter = false;
			bool cOrg = false;
			bool cFio = false;
			bool cSro = false;


			if (!_new)
			{
				var old = OMZak
				.Where(x => x.Id == this.Id)
				.SelectAll()
				.ExecuteFirstOrDefault();

				if (old != null)
				{
					pNumber = OMParam.GetParamStr(OMTableParam.Zak, Id, "number", Number, out bool aNumber, out cNumber);
					pDate = OMParam.GetParamDate(OMTableParam.Zak, Id, "date", Date, out bool aDate, out cDate);
					pRecDate = OMParam.GetParamDateActual(OMTableParam.Zak, Id, "rec_date", RecDate);
					pRecLetter = OMParam.GetParamStr(OMTableParam.Zak, Id, "rec_letter", RecLetter, out bool aRecLetter, out cRecLetter);
					pOrg = OMParam.GetParamInt(OMTableParam.Zak, Id, "id_org", IdOrg, out bool aOrg, out cOrg);
					pFio = OMParam.GetParamInt(OMTableParam.Zak, Id, "id_fio", IdFio, out bool aFio, out cFio);
					pSro = OMParam.GetParamInt(OMTableParam.Zak, Id, "id_sro", IdSro, out bool aSro, out cSro);

					pRecUser = OMParam.GetParamStrActual(OMTableParam.Zak, Id, "rec_user", RecUser);
					pRecBefore = OMParam.GetParamIntActual(OMTableParam.Zak, Id, "rec_before", RecBefore);
					pRecAfter = OMParam.GetParamIntActual(OMTableParam.Zak, Id, "rec_after", RecBefore);
					pRecSoglas = OMParam.GetParamIntActual(OMTableParam.Zak, Id, "rec_soglas", RecBefore);



					Number = (pNumber && !aNumber && cNumber) ? old.Number : Number;
					Date = (pDate && !aDate && cDate) ? old.Date : Date;
					RecLetter = (pRecLetter && !aRecLetter && cRecLetter) ? old.RecLetter : RecLetter;
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
				OMZakStatus objStatus = new OMZakStatus
				{
					Id = this.Id,
					Number = 1,
					Date = 1,
					RecDate = 1,
					RecLetter = 1,
					RecUser = 1,
					RecBefore = 1,
					RecAfter = 1,
					RecSoglas = 1,
					IdOrg = 1,
					IdFio = 1,
					IdSro = 1,
					Status = 1
				};
				objStatus.Save();

				OMParam.AddChar(OMTableParam.Zak, this.Id, "number", Number, ProcessingStatus.Processed);
				OMParam.AddDate(OMTableParam.Zak, this.Id, "date", Date, ProcessingStatus.Processed);
				OMParam.AddDate(OMTableParam.Zak, this.Id, "rec_date", RecDate, ProcessingStatus.Processed);
				OMParam.AddChar(OMTableParam.Zak, this.Id, "rec_letter", RecLetter, ProcessingStatus.Processed);
				OMParam.AddChar(OMTableParam.Zak, this.Id, "rec_user", RecUser, ProcessingStatus.Processed);
				OMParam.AddInt(OMTableParam.Zak, this.Id, "id_org", IdOrg, ProcessingStatus.Processed);
				OMParam.AddInt(OMTableParam.Zak, this.Id, "id_sro", IdSro, ProcessingStatus.Processed);
				OMParam.AddInt(OMTableParam.Zak, this.Id, "id_fio", IdFio, ProcessingStatus.Processed);
				OMParam.AddInt(OMTableParam.Zak, this.Id, "rec_before", RecBefore, ProcessingStatus.Processed);
				OMParam.AddInt(OMTableParam.Zak, this.Id, "rec_after", RecAfter, ProcessingStatus.Processed);
				OMParam.AddInt(OMTableParam.Zak, this.Id, "rec_soglas", RecSoglas, ProcessingStatus.Processed);
			}
			else
			{
				OMZakStatus objStatus = OMZakStatus
				.Where(x => x.Id == this.Id)
				.SelectAll()
				.ExecuteFirstOrDefault();
				if (objStatus != null)
				{
					objStatus.Number = (pNumber && cNumber) ? 1 : 0;
					objStatus.Date = (pDate && cDate) ? 1 : 0;
					objStatus.RecDate = 1;
					objStatus.RecLetter = (pRecLetter && cRecLetter) ? 1 : 0;
					objStatus.RecUser = 1;
					objStatus.IdOrg = (pOrg && cOrg) ? 1 : 0;
					objStatus.IdFio = (pFio && cFio) ? 1 : 0;
					objStatus.IdSro = (pSro && cSro) ? 1 : 0;
					objStatus.RecBefore = 1;
					objStatus.RecAfter = 1;
					objStatus.RecSoglas = 1;
					objStatus.Status = (pNumber && cNumber && pDate && cDate && pRecLetter && cRecLetter && pOrg && cOrg && pFio && cFio && pSro && cSro) ? 1 : 0;
					objStatus.Save();
				}
			}
			return res;
		}

		/// <summary>
		/// Получение утвержденной характеристики Номер заключения
		/// </summary>
		public OMParam GetActualNumber()
		{
			return OMParam.GetActual(OMTableParam.Zak, Id, "number");
		}
		/// <summary>
		/// Получение утвержденной характеристики Дата заключения
		/// </summary>
		public OMParam GetActualDate()
		{
			return OMParam.GetActual(OMTableParam.Zak, Id, "date");
		}
		/// <summary>
		/// Получение утвержденной характеристики Дата рецензии
		/// </summary>
		public OMParam GetActualRecDate()
		{
			return OMParam.GetActual(OMTableParam.Zak, Id, "rec_date");
		}
		/// <summary>
		/// Получение утвержденной характеристики Письмо
		/// </summary>
		public OMParam GetActualRecLetter()
		{
			return OMParam.GetActual(OMTableParam.Zak, Id, "rec_letter");
		}
		/// <summary>
		/// Получение утвержденной характеристики Автор рецензии
		/// </summary>
		public OMParam GetActualRecUser()
		{
			return OMParam.GetActual(OMTableParam.Zak, Id, "rec_user");
		}
		/// <summary>
		/// Получение утвержденной характеристики СРО
		/// </summary>
		public OMParam GetActualIdSro()
		{
			return OMParam.GetActual(OMTableParam.Zak, Id, "id_sro");
		}
		/// <summary>
		/// Получение утвержденной характеристики ФИО
		/// </summary>
		public OMParam GetActualIdFio()
		{
			return OMParam.GetActual(OMTableParam.Zak, Id, "id_fio");
		}
		/// <summary>
		/// Получение утвержденной характеристики Организация
		/// </summary>
		public OMParam GetActualIdOrg()
		{
			return OMParam.GetActual(OMTableParam.Zak, Id, "id_org");
		}
		/// <summary>
		/// Получение утвержденной характеристики Предварительная рецензия
		/// </summary>
		public OMParam GetActualRecBefore()
		{
			return OMParam.GetActual(OMTableParam.Zak, Id, "rec_before");
		}
		/// <summary>
		/// Получение утвержденной характеристики Рецензия после
		/// </summary>
		public OMParam GetActualRecAfter()
		{
			return OMParam.GetActual(OMTableParam.Zak, Id, "rec_after");
		}
		/// <summary>
		/// Получение утвержденной характеристики Согласование с руководством
		/// </summary>
		public OMParam GetActualRecSoglas()
		{
			return OMParam.GetActual(OMTableParam.Zak, Id, "rec_soglas");
		}




		/// <summary>
		/// Получение всех значений характеристики Номер заключения
		/// </summary>
		public List<OMParam> GetParamsNumber()
		{
			return OMParam.GetParams(OMTableParam.Zak, Id, "number");
		}
		/// <summary>
		/// Получение всех значений характеристики Дата заключения
		/// </summary>
		public List<OMParam> GetParamsDate()
		{
			return OMParam.GetParams(OMTableParam.Zak, Id, "date");
		}
		/// <summary>
		/// Получение всех значений характеристики Дата рецензии
		/// </summary>
		public List<OMParam> GetParamsRecDate()
		{
			return OMParam.GetParams(OMTableParam.Zak, Id, "rec_date");
		}
		/// <summary>
		/// Получение всех значений характеристики Письмо
		/// </summary>
		public List<OMParam> GetParamsRecLetter()
		{
			return OMParam.GetParams(OMTableParam.Zak, Id, "rec_letter");
		}
		/// <summary>
		/// Получение всех значений характеристики Автор рецензии
		/// </summary>
		public List<OMParam> GetParamsRecUser()
		{
			return OMParam.GetParams(OMTableParam.Zak, Id, "rec_user");
		}
		/// <summary>
		/// Получение всех значений характеристики СРО
		/// </summary>
		public List<OMParam> GetParamsIdSro()
		{
			return OMParam.GetParams(OMTableParam.Zak, Id, "id_sro");
		}
		/// <summary>
		/// Получение всех значений характеристики ФИО
		/// </summary>
		public List<OMParam> GetParamsIdFio()
		{
			return OMParam.GetParams(OMTableParam.Zak, Id, "id_fio");
		}
		/// <summary>
		/// Получение всех значений характеристики Организация
		/// </summary>
		public List<OMParam> GetParamsIdOrg()
		{
			return OMParam.GetParams(OMTableParam.Zak, Id, "id_org");
		}
		/// <summary>
		/// Получение всех значений характеристики Предварительная рецензия
		/// </summary>
		public List<OMParam> GetParamsRecBefore()
		{
			return OMParam.GetParams(OMTableParam.Zak, Id, "rec_before");
		}
		/// <summary>
		/// Получение всех значений характеристики Рецензия после
		/// </summary>
		public List<OMParam> GetParamsRecAfter()
		{
			return OMParam.GetParams(OMTableParam.Zak, Id, "rec_after");
		}
		/// <summary>
		/// Получение всех значений характеристики Согласование с руководством
		/// </summary>
		public List<OMParam> GetParamsRecSoglas()
		{
			return OMParam.GetParams(OMTableParam.Zak, Id, "rec_soglas");
		}


		/// <summary>
		/// Утверждение характеристики объекта из выбранных
		/// </summary>
		public bool UpdateAndCheckParam(OMParam pNumber, OMParam pDate, OMParam pRecDate, OMParam pRecLetter, OMParam pRecUser, OMParam pOrg, OMParam pFio, OMParam pSro, OMParam pRecBefore, OMParam pRecAfter, OMParam pRecSoglas)
		{
			if (pNumber == null || pDate == null || pRecDate == null || pRecLetter == null || pRecUser == null || pOrg == null || pFio == null || pSro == null || pRecBefore == null || pRecAfter == null || pRecSoglas == null)
			{
				throw new ArgumentNullException(nameof(OMParam));
			}

			#region Утверждение выбранных параметров
			pNumber.UpdateStatus(ProcessingStatus.Processed);
			pDate.UpdateStatus(ProcessingStatus.Processed);
			pRecDate.UpdateStatus(ProcessingStatus.Processed);
			pRecLetter.UpdateStatus(ProcessingStatus.Processed);
			pRecUser.UpdateStatus(ProcessingStatus.Processed);
			pOrg.UpdateStatus(ProcessingStatus.Processed);
			pFio.UpdateStatus(ProcessingStatus.Processed);
			pSro.UpdateStatus(ProcessingStatus.Processed);
			pRecBefore.UpdateStatus(ProcessingStatus.Processed);
			pRecAfter.UpdateStatus(ProcessingStatus.Processed);
			pRecSoglas.UpdateStatus(ProcessingStatus.Processed);
			#endregion

			#region Обновление данных для объекта
			Number = pNumber.ParamChar;
			Date = pDate.ParamDate;
			RecDate = pRecDate.ParamDate;
			RecLetter = pRecLetter.ParamChar;
			RecUser = pRecUser.ParamChar;
			IdOrg = pOrg.ParamInt;
			IdFio = pFio.ParamInt;
			IdSro = pSro.ParamInt;
			RecBefore = pRecBefore.ParamInt;
			RecAfter = pRecAfter.ParamInt;
			RecSoglas = pRecSoglas.ParamInt;
			Save();
			#endregion

			#region Обновление статусов полей 
			OMZakStatus objStatus = OMZakStatus
			.Where(x => x.Id == this.Id)
			.SelectAll()
			.ExecuteFirstOrDefault();
			if (objStatus != null)
			{
				objStatus.Number = 1;
				objStatus.Date = 1;
				objStatus.RecDate = 1;
				objStatus.RecLetter = 1;
				objStatus.RecUser = 1;
				objStatus.IdSro = 1;
				objStatus.IdFio = 1;
				objStatus.IdOrg = 1;
				objStatus.RecBefore = 1;
				objStatus.RecAfter = 1;
				objStatus.RecSoglas = 1;
				objStatus.Status = 1;
				objStatus.Save();
			}
			#endregion

			return true;
		}
	}
}
