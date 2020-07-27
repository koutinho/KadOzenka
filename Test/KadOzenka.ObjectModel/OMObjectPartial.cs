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
			bool pApplicantType = false;
			bool pTypeOfOwnership = false;
			bool pException = false;
			bool pAdditionalAnalysis = false;
			bool pIsSatisfied = false;

			bool cKn = false;
			bool cNameCenter = false;
			bool cStatDgi = false;
			bool cAdres = false;
			bool cDate = false;
			bool cSquare = false;
			bool cKc = false;
			bool cType = false;
			bool cApplicantType = false;
			bool cTypeOfOwnership = false;
			bool cException = false;
			bool cAdditionalAnalysis = false;
			bool cIsSatisfied = false;


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
					pApplicantType = OMParam.GetParamInt(OMTableParam.Object, Id, "applicanttype", (long)ApplicantType_Code, out bool aApplicantType, out cApplicantType);
					pTypeOfOwnership = OMParam.GetParamInt(OMTableParam.Object, Id, "typeofownership", (long)TypeOfOwnership_Code, out bool aTypeOfOwnership, out cTypeOfOwnership);
					pException = OMParam.GetParamInt(OMTableParam.Object, Id, "exception", Exception, out bool aException, out cException);
					pAdditionalAnalysis = OMParam.GetParamInt(OMTableParam.Object, Id, "additional_analysis", AdditionalAnalysis, out bool aAdditionalAnalysis, out cAdditionalAnalysis);
					pIsSatisfied = OMParam.GetParamInt(OMTableParam.Object, Id, "is_satisfied", IsSatisfied, out bool aIsSatisfied, out cIsSatisfied);

					Kn = (pKn && !cKn && aKn) ? old.Kn : Kn;
					NameCenter = (pNameCenter && !cNameCenter && aNameCenter) ? old.NameCenter : NameCenter;
					StatDgi = (pStatDgi && !cStatDgi && aStatDgi) ? old.StatDgi : StatDgi;
					Adres = (pAdres && !aAdres && cAdres) ? old.Adres : Adres;
					Date = (pDate && !cDate && aDate) ? old.Date : Date;
					Square = (pSquare && !aSquare && cSquare) ? old.Square : Square;
					Kc = (pKc && !aKc && cKc) ? old.Kc : Kc;
					Typeobj = (pTypeobj && !aType && cType) ? old.Typeobj : Typeobj;
					ApplicantType = (pApplicantType && !aApplicantType && cApplicantType) ? old.ApplicantType : ApplicantType;
					TypeOfOwnership = (pTypeOfOwnership && !aTypeOfOwnership && cTypeOfOwnership) ? old.TypeOfOwnership : TypeOfOwnership;
					Exception = (pException && !aException && cException) ? old.Exception : Exception;
					AdditionalAnalysis = (pAdditionalAnalysis && !aAdditionalAnalysis && cAdditionalAnalysis) ? old.AdditionalAnalysis : AdditionalAnalysis;
					IsSatisfied = (pIsSatisfied && !aIsSatisfied && cIsSatisfied) ? old.IsSatisfied : IsSatisfied;
				}
			}
			long res = Save();
			if (_new)
			{
				OMObjectStatus objStatus = new OMObjectStatus
				{
					Id = this.Id,
					Kn = true,
					Date = true,
					Square = true,
					Kc = true,
					NameCenter = true,
					StatDgi = true,
					Owner = true,
					Adres = true,
					Typeobj = true,
					Status = true,
					ApplicantType = true,
					TypeOfOwnership = true,
					Exception = true,
					AdditionalAnalysis = true,
					IsSatisfied = true
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
				OMParam.AddInt(OMTableParam.Object, this.Id, "applicanttype", (long)ApplicantType_Code, ProcessingStatus.Processed);
				OMParam.AddInt(OMTableParam.Object, this.Id, "typeofownership", (long)TypeOfOwnership_Code, ProcessingStatus.Processed);
				OMParam.AddInt(OMTableParam.Object, this.Id, "exception", Exception, ProcessingStatus.Processed);
				OMParam.AddInt(OMTableParam.Object, this.Id, "additional_analysis", AdditionalAnalysis, ProcessingStatus.Processed);
				OMParam.AddInt(OMTableParam.Object, this.Id, "is_satisfied", IsSatisfied, ProcessingStatus.Processed);
				OMParam.AddChar(OMTableParam.Object, this.Id, "owner", Owner, ProcessingStatus.Processed);
			}
			else
			{
				OMObjectStatus objStatus = OMObjectStatus
				.Where(x => x.Id == this.Id)
				.SelectAll()
				.ExecuteFirstOrDefault();
				if (objStatus != null)
				{
					objStatus.Kn = (pKn && cKn) ? true : false;
					objStatus.Date = (pDate && cDate) ? true : false;
                    objStatus.Square = (pSquare && cSquare) ? true : false;
                    objStatus.Kc = (pKc && cKc) ? true : false;
                    objStatus.NameCenter = (pNameCenter && cNameCenter) ? true : false;
                    objStatus.StatDgi = (pStatDgi && cStatDgi) ? true : false;
                    objStatus.Owner = (pOwner) ? true : false;
                    objStatus.Adres = (pAdres && cAdres) ? true : false;
                    objStatus.Typeobj = (pTypeobj && cType) ? true : false;
                    objStatus.ApplicantType = (pApplicantType && cApplicantType) ? true : false;
                    objStatus.TypeOfOwnership = (pTypeOfOwnership && cTypeOfOwnership) ? true : false;
                    objStatus.Exception = (pException && cException) ? true : false;
                    objStatus.AdditionalAnalysis = (pAdditionalAnalysis && cAdditionalAnalysis) ? true : false;
                    objStatus.IsSatisfied = (pIsSatisfied && cIsSatisfied) ? true : false;
                    objStatus.Status = (pKn && pDate && pSquare && pKc && pNameCenter && pStatDgi && pOwner && pAdres && pTypeobj 
                                        && cKn && cDate && cSquare && cKc && cNameCenter && cStatDgi && cAdres && cType 
                                        && cApplicantType && cTypeOfOwnership && cException && cAdditionalAnalysis && cIsSatisfied) ? true : false;
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
		public bool UpdateAndCheckParam(OMParam pKn, OMParam pType, OMParam pSquare, OMParam pKc, OMParam pDate, OMParam pNameCenter,
			OMParam pStatDgi, OMParam pAdres, OMParam pOwner, OMParam pApplicantType, OMParam pTypeOfOwnership, OMParam pAdditionalAnalysis,
			OMParam pException, OMParam pSatisfied)
		{
			if (pKn == null || pType == null || pSquare == null || pKc == null || pDate == null || pNameCenter == null || pStatDgi == null 
			    || pAdres == null || pOwner == null || pApplicantType == null || pTypeOfOwnership == null || pAdditionalAnalysis == null || pException == null || pSatisfied == null)
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
			pApplicantType.UpdateStatus(ProcessingStatus.Processed);
			pTypeOfOwnership.UpdateStatus(ProcessingStatus.Processed);
			pAdditionalAnalysis.UpdateStatus(ProcessingStatus.Processed);
			pException.UpdateStatus(ProcessingStatus.Processed);
			pSatisfied.UpdateStatus(ProcessingStatus.Processed);
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
			ApplicantType_Code = (ApplicantType)pApplicantType.ParamInt;
			TypeOfOwnership_Code = (TypeOfOwnership) pTypeOfOwnership.ParamInt;
			AdditionalAnalysis = pAdditionalAnalysis.ParamInt;
			Exception = pException.ParamInt;
			IsSatisfied = pSatisfied.ParamInt;
			Save();
			#endregion

			#region Обновление статусов полей 
			OMObjectStatus objStatus = OMObjectStatus
			.Where(x => x.Id == this.Id)
			.SelectAll()
			.ExecuteFirstOrDefault();
			if (objStatus != null)
			{
				objStatus.Kn = true;
				objStatus.Date = true;
				objStatus.Square = true;
				objStatus.Kc = true;
				objStatus.NameCenter = true;
				objStatus.StatDgi = true;
				objStatus.Owner = true;
				objStatus.Adres = true;
				objStatus.Typeobj = true;
				objStatus.Status = true;
				objStatus.AdditionalAnalysis = true;
				objStatus.ApplicantType = true;
				objStatus.TypeOfOwnership = true;
				objStatus.Exception = true;
				objStatus.IsSatisfied = true;
				objStatus.Save();
			}
			#endregion

			return true;
		}
	}
}

