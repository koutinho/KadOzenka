using ObjectModel.Directory.Sud;
using System;
using System.Collections.Generic;
using System.Text;

namespace ObjectModel.Sud
{
	public partial class OMZakLink
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
			bool pIdZak = false;

			bool cUse = false;
			bool cRs = false;
			bool cIdZak = false;


			if (!_new)
			{
				var old = OMZakLink
				.Where(x => x.Id == this.Id)
				.SelectAll()
				.ExecuteFirstOrDefault();

				if (old != null)
				{
					pUse = OMParam.GetParamStr(OMTableParam.ZakLink, Id, "use", Use, out bool aUse, out cUse);
					pDescr = OMParam.GetParamStrActual(OMTableParam.ZakLink, Id, "descr", Descr);
					pRs = OMParam.GetParamDecimal(OMTableParam.ZakLink, Id, "rs", Rs, out bool aRs, out cRs);
					pUprs = OMParam.GetParamDecimalActual(OMTableParam.ZakLink, Id, "uprs", Uprs);
					pIdZak = OMParam.GetParamInt(OMTableParam.ZakLink, Id, "id_zak", IdZak, out bool aIdZak, out cIdZak);

					Use = (pUse && !aUse && cUse) ? old.Use : Use;
					Descr = Descr;
					Uprs = Uprs;
					Rs = (pRs && !aRs && cRs) ? old.Rs : Rs;
					IdZak = (pIdZak && !aIdZak && cIdZak) ? old.IdZak : IdZak;
				}
			}
			long res = Save();
			if (_new)
			{
				OMZakLinkStatus objStatus = new OMZakLinkStatus
				{
					Id = this.Id,
					Use = 1,
					Descr = 1,
					Rs = 1,
					Uprs = 1,
					IdZak = 1,
					IdObject = 1,
					Status = 1,
				};
				objStatus.Save();
				OMParam.AddChar(OMTableParam.ZakLink, this.Id, "use", Use, ProcessingStatus.Processed);
				OMParam.AddChar(OMTableParam.ZakLink, this.Id, "descr", Descr, ProcessingStatus.Processed);
				OMParam.AddDouble(OMTableParam.ZakLink, this.Id, "rs", Rs, ProcessingStatus.Processed);
				OMParam.AddDouble(OMTableParam.ZakLink, this.Id, "uprs", Uprs, ProcessingStatus.Processed);
				OMParam.AddInt(OMTableParam.ZakLink, this.Id, "id_zak", IdZak, ProcessingStatus.Processed);
			}
			else
			{
				OMZakLinkStatus objStatus = OMZakLinkStatus
				.Where(x => x.Id == this.Id)
				.SelectAll()
				.ExecuteFirstOrDefault();
				if (objStatus != null)
				{
					objStatus.Use = (pUse && cUse) ? 1 : 0;
					objStatus.Descr = 1;
					objStatus.Rs = (pRs && cRs) ? 1 : 0;
					objStatus.Uprs = 1;
					objStatus.IdZak = (pIdZak && cIdZak) ? 1 : 0;
					objStatus.IdObject = 1;
					objStatus.Status = (pRs && pIdZak && cRs && cIdZak && pUse && cUse) ? 1 : 0;
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
			return OMParam.GetActual(OMTableParam.ZakLink, Id, "use");
		}
		/// <summary>
		/// Получение утвержденной характеристики Примечание
		/// </summary>
		public OMParam GetActualDescr()
		{
			return OMParam.GetActual(OMTableParam.ZakLink, Id, "descr");
		}
		/// <summary>
		/// Получение утвержденной характеристики Рыночная стоимость
		/// </summary>
		public OMParam GetActualRs()
		{
			return OMParam.GetActual(OMTableParam.ZakLink, Id, "rs");
		}
		/// <summary>
		/// Получение утвержденной характеристики Удельная стоимость
		/// </summary>
		public OMParam GetActualUprs()
		{
			return OMParam.GetActual(OMTableParam.ZakLink, Id, "uprs");
		}
		/// <summary>
		/// Получение утвержденной характеристики Заключение
		/// </summary>
		public OMParam GetActualZak()
		{
			return OMParam.GetActual(OMTableParam.ZakLink, Id, "id_zak");
		}


		/// <summary>
		/// Получение всех вариантов характеристики Текущее использование
		/// </summary>
		public List<OMParam> GetVariantUse()
		{
			return OMParam.GetParams(OMTableParam.ZakLink, Id, "use");
		}
		/// <summary>
		/// Получение всех вариантов характеристики Примечание
		/// </summary>
		public List<OMParam> GetVariantDescr()
		{
			return OMParam.GetParams(OMTableParam.ZakLink, Id, "descr");
		}
		/// <summary>
		/// Получение всех вариантов характеристики Рыночная стоимость
		/// </summary>
		public List<OMParam> GetVariantRs()
		{
			return OMParam.GetParams(OMTableParam.ZakLink, Id, "rs");
		}
		/// <summary>
		/// Получение всех вариантов характеристики Удельная стоимость
		/// </summary>
		public List<OMParam> GetVariantUprs()
		{
			return OMParam.GetParams(OMTableParam.ZakLink, Id, "uprs");
		}
		/// <summary>
		/// Получение всех вариантов характеристики Заключение
		/// </summary>
		public List<OMParam> GetVariantZak()
		{
			return OMParam.GetParams(OMTableParam.ZakLink, Id, "id_zak");
		}



		/// <summary>
		/// Утверждение характеристики объекта из выбранных
		/// </summary>
		public bool UpdateAndCheckParam(OMParam pUse, OMParam pDescr, OMParam pRs, OMParam pUprs, OMParam pIdZak)
		{
			if (pUse == null || pDescr == null || pRs == null || pUprs == null || pIdZak == null)
			{
				throw new ArgumentNullException(nameof(OMParam));
			}

			#region Утверждение выбранных параметров
			pUse.UpdateStatus(ProcessingStatus.Processed);
			pDescr.UpdateStatus(ProcessingStatus.Processed);
			pRs.UpdateStatus(ProcessingStatus.Processed);
			pUprs.UpdateStatus(ProcessingStatus.Processed);
			pIdZak.UpdateStatus(ProcessingStatus.Processed);
			#endregion

			#region Обновление данных для объекта
			Use = pUse.ParamChar;
			Descr = pDescr.ParamChar;
			Rs = pRs.ParamDouble;
			Uprs = pUprs.ParamDouble;
			IdZak = pIdZak.ParamInt;
			Save();
			#endregion

			#region Обновление статусов полей 
			OMZakLinkStatus objStatus = OMZakLinkStatus
			.Where(x => x.Id == this.Id)
			.SelectAll()
			.ExecuteFirstOrDefault();
			if (objStatus != null)
			{
				objStatus.Use = 1;
				objStatus.Descr = 1;
				objStatus.Rs = 1;
				objStatus.Uprs = 1;
				objStatus.IdZak = 1;
				objStatus.Status = 1;
				objStatus.Save();
			}
			#endregion

			return true;
		}

        /// <summary>
        /// Ссылка на OMZak
        /// </summary>
        public ObjectModel.Sud.OMZak Zak { get; set; }

    }
}
