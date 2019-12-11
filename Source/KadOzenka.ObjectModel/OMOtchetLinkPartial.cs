using ObjectModel.Directory.Sud;
using System;
using System.Collections.Generic;
using System.Text;

namespace ObjectModel.Sud
{
	public partial class OMOtchetLink
	{
		/// <summary>
		/// Сохранение данных с анализом данных
		/// </summary>
		public long SaveAndCheckParam()
		{
			bool _new = Id <= 0;

			bool pUse = false;
			bool pDescr = false;
			bool pRs = false;
			bool pUprs = false;
			bool pIdOtchet = false;

			bool cUse = false;
			bool cRs = false;
			bool cIdOtchet = false;


			if (!_new)
			{
				var old = OMOtchetLink
				.Where(x => x.Id == this.Id)
				.SelectAll()
				.ExecuteFirstOrDefault();

				if (old != null)
				{
					pUse = OMParam.GetParamStr(OMTableParam.OtchetLink, Id, "use", Use, out bool aUse, out cUse);
					pDescr = OMParam.GetParamStrActual(OMTableParam.OtchetLink, Id, "descr", Descr);
					pRs = OMParam.GetParamDecimal(OMTableParam.OtchetLink, Id, "rs", Rs, out bool aRs, out cRs);
					pUprs = OMParam.GetParamDecimalActual(OMTableParam.OtchetLink, Id, "uprs", Uprs);
					pIdOtchet = OMParam.GetParamInt(OMTableParam.OtchetLink, Id, "id_otchet", IdOtchet, out bool aIdOtchet, out cIdOtchet);

					Use = (pUse && !aUse && cUse) ? old.Use : Use;
					Descr = Descr;
					Uprs = Uprs;
					Rs = (pRs && !aRs && cRs) ? old.Rs : Rs;
					IdOtchet = (pIdOtchet && !aIdOtchet && cIdOtchet) ? old.IdOtchet : IdOtchet;
				}
			}
			long res = Save();
			if (_new)
			{
				OMOtchetLinkStatus objStatus = new OMOtchetLinkStatus
				{
					Id = this.Id,
					Use = 1,
					Descr = 1,
					Rs = 1,
					Uprs = 1,
					IdOtchet = 1,
					IdObject = 1,
					Status = 1,
				};
				objStatus.Save();
				OMParam.AddChar(OMTableParam.OtchetLink, this.Id, "use", Use, ProcessingStatus.Processed);
				OMParam.AddChar(OMTableParam.OtchetLink, this.Id, "descr", Descr, ProcessingStatus.Processed);
				OMParam.AddDouble(OMTableParam.OtchetLink, this.Id, "rs", Rs, ProcessingStatus.Processed);
				OMParam.AddDouble(OMTableParam.OtchetLink, this.Id, "uprs", Uprs, ProcessingStatus.Processed);
				OMParam.AddInt(OMTableParam.OtchetLink, this.Id, "id_otchet", IdOtchet, ProcessingStatus.Processed);
			}
			else
			{
				OMOtchetLinkStatus objStatus = OMOtchetLinkStatus
				.Where(x => x.Id == this.Id)
				.SelectAll()
				.ExecuteFirstOrDefault();
				if (objStatus != null)
				{
					objStatus.Use = (pUse && cUse) ? 1 : 0;
					objStatus.Descr = 1;
					objStatus.Rs = (pRs && cRs) ? 1 : 0;
					objStatus.Uprs = 1;
					objStatus.IdOtchet = (pIdOtchet && cIdOtchet) ? 1 : 0;
					objStatus.IdObject = 1;
					objStatus.Status = (pRs && pIdOtchet && cRs && cIdOtchet && pUse && cUse) ? 1 : 0;
					objStatus.Save();
				}
			}
			return res;
		}
		/// <summary>
		/// Получение утвержденной характеристики Текущее использование
		/// </summary>
		public OMParam GetActualUse()
		{
			return OMParam.GetActual(OMTableParam.OtchetLink, Id, "use");
		}
		/// <summary>
		/// Получение утвержденной характеристики Примечание
		/// </summary>
		public OMParam GetActualDescr()
		{
			return OMParam.GetActual(OMTableParam.OtchetLink, Id, "descr");
		}
		/// <summary>
		/// Получение утвержденной характеристики Рыночная стоимость
		/// </summary>
		public OMParam GetActualRs()
		{
			return OMParam.GetActual(OMTableParam.OtchetLink, Id, "rs");
		}
		/// <summary>
		/// Получение утвержденной характеристики Удельная стоимость
		/// </summary>
		public OMParam GetActualUprs()
		{
			return OMParam.GetActual(OMTableParam.OtchetLink, Id, "uprs");
		}
		/// <summary>
		/// Получение утвержденной характеристики Отчет
		/// </summary>
		public OMParam GetActualOtchet()
		{
			return OMParam.GetActual(OMTableParam.OtchetLink, Id, "id_otchet");
		}


		/// <summary>
		/// Получение всех вариантов характеристики Текущее использование
		/// </summary>
		public List<OMParam> GetVariantUse()
		{
			return OMParam.GetParams(OMTableParam.OtchetLink, Id, "use");
		}
		/// <summary>
		/// Получение всех вариантов характеристики Примечание
		/// </summary>
		public List<OMParam> GetVariantDescr()
		{
			return OMParam.GetParams(OMTableParam.OtchetLink, Id, "descr");
		}
		/// <summary>
		/// Получение всех вариантов характеристики Рыночная стоимость
		/// </summary>
		public List<OMParam> GetVariantRs()
		{
			return OMParam.GetParams(OMTableParam.OtchetLink, Id, "rs");
		}
		/// <summary>
		/// Получение всех вариантов характеристики Удельная стоимость
		/// </summary>
		public List<OMParam> GetVariantUprs()
		{
			return OMParam.GetParams(OMTableParam.OtchetLink, Id, "uprs");
		}
		/// <summary>
		/// Получение всех вариантов характеристики Отчет
		/// </summary>
		public List<OMParam> GetVariantOtchet()
		{
			return OMParam.GetParams(OMTableParam.OtchetLink, Id, "id_otchet");
		}



		/// <summary>
		/// Утверждение характеристики объекта из выбранных
		/// </summary>
		public bool UpdateAndCheckParam(OMParam pUse, OMParam pDescr, OMParam pRs, OMParam pUprs, OMParam pIdOtchet)
		{
			if (pUse == null || pDescr == null || pRs == null || pUprs == null || pIdOtchet == null)
			{
				throw new ArgumentNullException(nameof(OMParam));
			}

			#region Утверждение выбранных параметров
			pUse.UpdateStatus(ProcessingStatus.Processed);
			pDescr.UpdateStatus(ProcessingStatus.Processed);
			pRs.UpdateStatus(ProcessingStatus.Processed);
			pUprs.UpdateStatus(ProcessingStatus.Processed);
			pIdOtchet.UpdateStatus(ProcessingStatus.Processed);
			#endregion

			#region Обновление данных для объекта
			Use = pUse.ParamChar;
			Descr = pDescr.ParamChar;
			Rs = pRs.ParamDouble;
			Uprs = pUprs.ParamDouble;
			IdOtchet = pIdOtchet.ParamInt;
			Save();
			#endregion

			#region Обновление статусов полей 
			OMOtchetLinkStatus objStatus = OMOtchetLinkStatus
			.Where(x => x.Id == this.Id)
			.SelectAll()
			.ExecuteFirstOrDefault();
			if (objStatus != null)
			{
				objStatus.Use = 1;
				objStatus.Descr = 1;
				objStatus.Rs = 1;
				objStatus.Uprs = 1;
				objStatus.IdOtchet = 1;
				objStatus.Status = 1;
				objStatus.Save();
			}
			#endregion

			return true;
		}

        /// <summary>
        /// Ссылка на OMOtchet
        /// </summary>
        public ObjectModel.Sud.OMOtchet Otchet { get; set; }

    }
}
