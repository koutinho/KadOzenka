using ObjectModel.Directory.Sud;
using System;
using System.Collections.Generic;
using System.Text;

namespace ObjectModel.Sud
{
	public partial class OMSudLink
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
			bool pIdSud = true;

			bool cRs = false;
			bool cIdSud = true;


			if (!_new)
			{
				var old = OMSudLink
				.Where(x => x.Id == this.Id)
				.SelectAll()
				.ExecuteFirstOrDefault();

				if (old != null)
				{

					pUse = OMParam.GetParamStrActual(OMTableParam.SudLink, Id, "use", Use);
					pDescr = OMParam.GetParamStrActual(OMTableParam.SudLink, Id, "descr", Descr);
					pRs = OMParam.GetParamDecimal(OMTableParam.SudLink, Id, "rs", Rs, out bool aRs, out cRs);
					pUprs = OMParam.GetParamDecimalActual(OMTableParam.SudLink, Id, "uprs", Uprs);
					pIdSud = OMParam.GetParamInt(OMTableParam.SudLink, Id, "id_sud", IdSud, out bool aIdSud, out cIdSud);

					Use = Use;
					Descr = Descr;
					Uprs = Uprs;
					Rs = (pRs && !aRs && cRs) ? old.Rs : Rs;
					IdSud = (pIdSud && !aIdSud && cIdSud) ? old.IdSud : IdSud;
				}
			}
			long res = Save();
			if (_new)
			{
				OMSudLinkStatus objStatus = new OMSudLinkStatus
				{
					Id = this.Id,
					Use = 1,
					Descr = 1,
					Rs = 1,
					Uprs = 1,
					IdSud = 1,
					IdObject = 1,
					Status = 1,
				};
				objStatus.Save();
				OMParam.AddChar(OMTableParam.SudLink, this.Id, "use", Use, ProcessingStatus.Processed);
				OMParam.AddChar(OMTableParam.SudLink, this.Id, "descr", Descr, ProcessingStatus.Processed);
				OMParam.AddDouble(OMTableParam.SudLink, this.Id, "rs", Rs, ProcessingStatus.Processed);
				OMParam.AddDouble(OMTableParam.SudLink, this.Id, "uprs", Uprs, ProcessingStatus.Processed);
				OMParam.AddInt(OMTableParam.SudLink, this.Id, "id_sud", IdSud, ProcessingStatus.Processed);
			}
			else
			{
				OMSudLinkStatus objStatus = OMSudLinkStatus
				.Where(x => x.Id == this.Id)
				.SelectAll()
				.ExecuteFirstOrDefault();
				if (objStatus != null)
				{
					objStatus.Use = 1;
					objStatus.Descr = 1;
					objStatus.Rs = (pRs && cRs) ? 1 : 0;
					objStatus.Uprs = 1;
					objStatus.IdSud = (pIdSud && cIdSud) ? 1 : 0;
					objStatus.IdObject = 1;
					objStatus.Status = (pRs && pIdSud && cRs && cIdSud) ? 1 : 0;
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
			return OMParam.GetActual(OMTableParam.SudLink, Id, "use");
		}
		/// <summary>
		/// Получение утвержденной характеристики Примечание
		/// </summary>
		public OMParam GetActualDescr()
		{
			return OMParam.GetActual(OMTableParam.SudLink, Id, "descr");
		}
		/// <summary>
		/// Получение утвержденной характеристики Рыночная стоимость
		/// </summary>
		public OMParam GetActualRs()
		{
			return OMParam.GetActual(OMTableParam.SudLink, Id, "rs");
		}
		/// <summary>
		/// Получение утвержденной характеристики Удельная стоимость
		/// </summary>
		public OMParam GetActualUprs()
		{
			return OMParam.GetActual(OMTableParam.SudLink, Id, "uprs");
		}
		/// <summary>
		/// Получение утвержденной характеристики Судебное решение
		/// </summary>
		public OMParam GetActualSud()
		{
			return OMParam.GetActual(OMTableParam.SudLink, Id, "id_sud");
		}


		/// <summary>
		/// Получение всех вариантов характеристики Текущее использование
		/// </summary>
		public List<OMParam> GetVariantUse()
		{
			return OMParam.GetParams(OMTableParam.SudLink, Id, "use");
		}
		/// <summary>
		/// Получение всех вариантов характеристики Примечание
		/// </summary>
		public List<OMParam> GetVariantDescr()
		{
			return OMParam.GetParams(OMTableParam.SudLink, Id, "descr");
		}
		/// <summary>
		/// Получение всех вариантов характеристики Рыночная стоимость
		/// </summary>
		public List<OMParam> GetVariantRs()
		{
			return OMParam.GetParams(OMTableParam.SudLink, Id, "rs");
		}
		/// <summary>
		/// Получение всех вариантов характеристики Удельная стоимость
		/// </summary>
		public List<OMParam> GetVariantUprs()
		{
			return OMParam.GetParams(OMTableParam.SudLink, Id, "uprs");
		}
		/// <summary>
		/// Получение всех вариантов характеристики Судебное решение
		/// </summary>
		public List<OMParam> GetVariantSuds()
		{
			return OMParam.GetParams(OMTableParam.SudLink, Id, "id_sud");
		}



		/// <summary>
		/// Утверждение характеристики объекта из выбранных
		/// </summary>
		public bool UpdateAndCheckParam(OMParam pUse, OMParam pDescr, OMParam pRs, OMParam pUprs, OMParam pIdSud)
		{
			if (pUse == null || pDescr == null || pRs == null || pUprs == null || pIdSud == null)
			{
				throw new ArgumentNullException(nameof(OMParam));
			}

			#region Утверждение выбранных параметров
			pUse.UpdateStatus(ProcessingStatus.Processed);
			pDescr.UpdateStatus(ProcessingStatus.Processed);
			pRs.UpdateStatus(ProcessingStatus.Processed);
			pUprs.UpdateStatus(ProcessingStatus.Processed);
			pIdSud.UpdateStatus(ProcessingStatus.Processed);
			#endregion

			#region Обновление данных для объекта
			Use = pUse.ParamChar;
			Descr = pDescr.ParamChar;
			Rs = pRs.ParamDouble;
			Uprs = pUprs.ParamDouble;
			IdSud = pIdSud.ParamInt;
			Save();
			#endregion

			#region Обновление статусов полей 
			OMSudLinkStatus objStatus = OMSudLinkStatus
			.Where(x => x.Id == this.Id)
			.SelectAll()
			.ExecuteFirstOrDefault();
			if (objStatus != null)
			{
				objStatus.Use = 1;
				objStatus.Descr = 1;
				objStatus.Rs = 1;
				objStatus.Uprs = 1;
				objStatus.IdSud = 1;
				objStatus.Status = 1;
				objStatus.Save();
			}
			#endregion

			return true;
		}

        /// <summary>
        /// Ссылка на OMSud
        /// </summary>
        public ObjectModel.Sud.OMSud Sud { get; set; }
    }
}
