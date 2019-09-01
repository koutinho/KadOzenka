using CIPJS.DAL.StrahNach;
using Core.ErrorManagment;
using Core.Register.QuerySubsystem;
using Core.Shared.Extensions;
using Core.Shared.Misc;
using Core.SRD;
using ObjectModel.Bti;
using ObjectModel.Directory;
using ObjectModel.Insur;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Transactions;
using CIPJS.DAL.Building;
using CIPJS.DAL.Flat;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;

namespace CIPJS.DAL.Fsp
{
	public class FspService
	{
		private readonly StrahNachService _strahNachService;

		private List<OMDistrict> _districtCache;
		private List<OMDistrict> Districts
		{
			get
			{
				if (_districtCache == null)
				{
					_districtCache = OMDistrict.Where(x => true).SelectAll().Execute();
				}

				return _districtCache;
			}
		}

		private List<OMFlatType> _flatTypes;
		private List<OMFlatType> FlatTypes
		{
			get
			{
				if (_flatTypes == null)
				{
					_flatTypes = OMFlatType.Where(x => true).SelectAll().Execute();
				}

				return _flatTypes;
			}
		}

		private long? SeparateFlatId
		{
			get
			{
				return FlatTypes.FirstOrDefault(x => x.Code == 0)?.Id;
			}
		}

		private long? CommunalFlatId
		{
			get
			{
				return FlatTypes.FirstOrDefault(x => x.Code == 1)?.Id;
			}
		}

		public FspService()
		{
			_strahNachService = new StrahNachService();
		}

		/// <summary>
		/// Получить ФСП по его идентификатору
		/// </summary>
		/// <param name="id">Идентификатор ФСП</param>
		/// <returns>ФСП</returns>
		public OMFsp Get(long? id)
		{
			return GetById(id);
		}

		/// <summary>
		/// Получить ФСП по его идентификатору
		/// </summary>
		/// <param name="id">Идентификатор ФСП</param>
		/// <returns>ФСП</returns>
		public OMFsp GetById(long? id, bool parentFlat = false, bool balance = false, bool plat = false)
		{
			OMFsp entity = null;

			if (id.HasValue)
			{
				entity = OMFsp
					.Where(x => x.EmpId == id)
					.SelectAll()
					.Select(x => x.ParentPolicySvd.Kodpl)
					.Select(x => x.ParentPolicySvd.Npol)
					.Select(x => x.ParentPolicySvd.Dat)
					.Select(x => x.ParentPolicySvd.Opl)
					.Select(x => x.ParentPolicySvd.Ss)
					.Select(x => x.ParentPolicySvd.Pr)
					.Select(x => x.ParentPolicySvd.Pralt_Code)
					.Execute()
					.FirstOrDefault();
				if (entity != null && entity.FspType_Code == FSPType.EPD)
					entity.ParentPolicySvd = OMPolicySvd
						.Where(x => x.FspId == entity.EmpId)
						.Select(x => x.Kodpl)
						.Select(x => x.Npol)
						.Select(x => x.Dat)
						.Select(x => x.Opl)
						.Select(x => x.Ss)
						.Select(x => x.Pr)
						.Select(x => x.Pralt_Code)
						.ExecuteFirstOrDefault();

				FillFspAddData(entity, parentFlat, balance, plat);
			}

			return entity;
		}

		/// <summary>
		/// Возвращает emp_id ФСП у которых была изменена площадь.
		/// </summary>
		/// <param name="fspId"></param>
		/// <param name="periodRegDate"></param>
		/// <param name="tariff"></param>
		/// <returns></returns>
		public HashSet<long> SetGbuInputNachWithFspByPeriod()
		{
			HashSet<long> fspIds = new HashSet<long>();
			string sql = $@"select distinct fsp1.emp_id from insur_fsp_q fsp1
                            where fsp1.actual = 1 and exists(select fsp2.emp_id from insur_fsp_q fsp2 where fsp2.opl_kodpl != fsp1.opl_kodpl and fsp2.emp_id = fsp1.emp_id and fsp2.actual = 0)";

			DbCommand command = DBMngr.Realty.GetSqlStringCommand(sql);
			using (var reader = DBMngr.Realty.ExecuteReader(command))
			{
				while (reader.Read())
				{
					var emp_id = reader.GetInt64(0);
					fspIds.Add(emp_id);
				}
			}
			return fspIds;
		}

        /// <summary>
        /// Получаем все фсп определенного пакета загрузки через начисления.
        /// </summary>
        /// <param name="packageId"></param>
        /// <returns></returns>
        public HashSet<long> GetNachFspOfPackage(long packageId)
        {
            HashSet<long> fspIds = new HashSet<long>();
            string sql = $@"select distinct n.fsp_id from insur_input_nach n where n.link_id_file = {packageId} and n.fsp_id > 0 
                            and n.status_identif_code in 
                            (
                            {(long)StatusIdentifikacii.None},
                            {(long)StatusIdentifikacii.NotIdentified},
                            {(long)StatusIdentifikacii.Identified}
                            );";

            DbCommand command = DBMngr.Realty.GetSqlStringCommand(sql);
            using (var reader = DBMngr.Realty.ExecuteReader(command))
            {
                while (reader.Read())
                {
                    var fsp_id = reader.GetInt64(0);
                    fspIds.Add(fsp_id);
                }
            }
            return fspIds;
        }

        /// <summary>
        /// Получаем все фсп определенного пакета загрузки через зачисления.
        /// </summary>
        /// <param name="packageId"></param>
        /// <returns></returns>
        public HashSet<long> GetStrahFspOfPackage(long packageId)
        {
            HashSet<long> fspIds = new HashSet<long>();
            string sql = $@"select distinct n.fsp_id from insur_input_plat n where n.link_id_file = {packageId} and n.fsp_id > 0 
                            and n.status_identif_code in 
                            (
                            {(long)StatusIdentifikacii.PartiallyIdentified},
                            {(long)StatusIdentifikacii.Identified}
                            );";

            DbCommand command = DBMngr.Realty.GetSqlStringCommand(sql);
            using (var reader = DBMngr.Realty.ExecuteReader(command))
            {
                while (reader.Read())
                {
                    var fsp_id = reader.GetInt64(0);
                    fspIds.Add(fsp_id);
                }
            }
            return fspIds;
        }





        /// <summary>
        /// Возвращает emp_id ФСП определенных округов.
        /// </summary>
        /// <param name="fspId"></param>
        /// <param name="periodRegDate"></param>
        /// <param name="tariff"></param>
        /// <returns></returns>
        public HashSet<long> GetFspByOkrug(HashSet<long?> okrugIds)
		{
			HashSet<long> fspIds = new HashSet<long>();

			var condition = okrugIds != null && okrugIds.Count > 0 ? $"and flat.okrug_id in ({string.Join(',', okrugIds)})" : string.Empty;

			string sql = $@"select fsp.emp_id from insur_fsp_q fsp
                            inner join insur_flat_q flat
                            on fsp.obj_id = flat.emp_id and fsp.actual = 1 and flat.actual = 1 {condition} ;";

			DbCommand command = DBMngr.Realty.GetSqlStringCommand(sql);
			using (var reader = DBMngr.Realty.ExecuteReader(command))
			{
				while (reader.Read())
				{
					var emp_id = reader.GetInt64(0);
					fspIds.Add(emp_id);
				}
			}
			return fspIds;
		}

		/// <summary>
		/// Получить ФСП по идентификатору полиса/свидетельства
		/// </summary>
		/// <param name="contractId">Идентификатор полиса/свидетельства</param>
		/// <returns>ФСП</returns>
		public OMFsp GetByContractId(long? contractId)
		{
			OMFsp entity = null;

			entity = OMFsp.Where(x => x.ContractId == contractId)
					.SelectAll()
					.Select(x => x.SpecialS)                           //По умолчанию, для историческиих реестров, 
					.SetJoins(new List<QSJoin>                         //мы всегда получаем сроки с Actual = 1.
                    {                                                  //Этот блок нужен для того, чтобы 
                        new QSJoin                                     //это отключить и получить все записи
                        {                                              //
                            RegisterId = OMFsp.GetRegisterId(),        //
                            ActualDate = null                          //
                        }                                              //
                    })                                                 //
					.Execute()
					.FirstOrDefault();

			//entity.Save(fromDate: ..., todate: ...);

			return entity;
		}

		/// <summary>
		/// Получить ФСП по коду плательщика
		/// </summary>
		/// <param name="kodpl">код плательщика</param>
		/// <returns>ФСП</returns>
		public OMFsp GetByKodpl(string kodpl)
		{
			OMFsp entity = null;

			entity = OMFsp.Where(x => x.Kodpl == kodpl)
					.SelectAll()
					.Execute()
					.FirstOrDefault();

			return entity;
		}

		/// <summary>
		/// Заполнить информацию по помещению, ведомостям учета страховых взносов, страховым взносам
		/// </summary>
		private void FillFspAddData(OMFsp entity, bool parentFlat = false, bool balance = false, bool plat = false)
		{
			if (entity != null)
			{
				var date_now = DateTime.Now;
				var lastDayMonthDate = new DateTime(date_now.Year, date_now.Month, DateTime.DaysInMonth(date_now.Year, date_now.Month));

				if (parentFlat)
				{
					entity.ParentFlat = OMFlat.Where(x => x.EmpId == entity.ObjId)
					  .SelectAll().Execute().FirstOrDefault();
				}

				if (balance)
				{
					entity.Balance = OMBalance
					  .Where(x => x.FspId == entity.EmpId && x.PeriodRegDate >= entity.DateOpen && x.PeriodRegDate <= lastDayMonthDate)
					  .SelectAll().Execute();
				}

				if (plat)
				{
					entity.InputPlat = OMInputPlat.Where(x => x.FspId == entity.EmpId).SelectAll().Execute();
				}
			}
		}

		/// <summary>
		/// Получить список ФСП по идентификатору помещения
		/// </summary>
		/// <param name="id">Идентификатор помещения</param>
		/// <returns>Список ФСП</returns>
		public List<OMFsp> GetByFlatId(long? id, out bool fspFlagManyObj, bool parentFlat = false, bool balance = false, bool plat = false)
		{
			long[] fspManyObjIds = OMLinkFspFlat.Where(x => x.ObjId == id && x.FspId != null)
				.Select(x => x.FspId)
				.Execute()
				.Where(x => x.FspId.HasValue)
				.Select(x => x.FspId.Value)
				.ToArray();

			QSQuery<OMFsp> fspQuery = OMFsp.Where(x => x.ObjId == id).SelectAll();

			if (fspManyObjIds.Length > 0)
			{
				fspFlagManyObj = true;
				fspQuery.Or(x => fspManyObjIds.Contains(x.EmpId));
			}
			else
			{
				fspFlagManyObj = false;
			}

			List<OMFsp> fspList = fspQuery.Execute();

			if (fspList.IsNotEmpty())
			{
				foreach (var fsp in fspList)
				{
					FillFspAddData(fsp, parentFlat, balance, plat);
				}
			}

			return fspList;
		}

		/// <summary>
		/// Получить количество ФСП по идентификатору помещения
		/// </summary>
		/// <param name="id">Идентификатор помещения</param>
		/// <returns>количество ФСП</returns>
		public int CountByFlatId(long? id)
		{
			if (id.HasValue)
				return OMFsp.Where(x => x.ObjId == id).GetCountQuery().ExecuteQuery().Rows[0]["Count"].ParseToInt();

			return 0;
		}

        /// <summary>
        /// Генерация номера ФСП.
        /// </summary>
        /// <param name="kodPl"></param>
        /// <param name="unom"></param>
        /// <param name="kvNom"></param>
        /// <param name="flatObjectId"></param>
        /// <returns></returns>
		public string GetFspNumber(string kodPl, long? unom, string kvNom, long? flatObjectId = null)
		{
			int flatCount = 0;

			if (flatObjectId.HasValue)
			{
				flatCount = OMFsp.Where(x => x.ObjId == flatObjectId.Value).GetCountQuery().ExecuteQuery().Rows[0]["Count"].ParseToInt();
			}

			return $"{(kodPl.IsNotEmpty() ? kodPl : "0")}-{(unom.HasValue ? unom : 0)}-{(kvNom.IsNotEmpty() ? kvNom : "0")}-{(++flatCount).ToString().PadLeft(3, '0')}";
		}

		public long timingCreateFspCount = 0;
		public long timingCreateFspFindFspObject = 0;
		public long timingCreateFspFindFlat = 0;
		public long timingCreateFspCreateFlat = 0;
		public long timingCreateFspGetFspNumber = 0;
		public string timingLastFlatSql = "";
		public long timingLastFlatFindTime = 0;

		/// <summary>
		/// Объект для блокировки потоков
		/// </summary>
		private static readonly object LockObject = new Object();

		/// <summary>
		/// Формирование ФСП для Реестра начислений INSUR_INPUT_NACH.
		/// !!!Из ментода убрано сохранение ФСП для оптимизации. Сохранение должно производиться вне данной функции.
		/// </summary>
		/// <param name="inputNach">Объект начисления</param>
		/// <returns></returns>
		private OMFsp CreateNachFsp(OMInputNach inputNach, out List<string> errors)
		{
			errors = new List<string>();

			if (inputNach == null)
			{
				errors.Add("Не удалось создать ФСП по начислению, т.к. не удалось определить запись начисления");
				return null;
			}

			if (inputNach.Kodpl.IsNullOrEmpty())
			{
				errors.Add($"Не удалось создать ФСП по начислению, т.к. не удалось определить код плательщика для начисления с идентификатором {inputNach.EmpId}");
				return null;
			}

			if (inputNach.Kodpl.Length < 3)
			{
				errors.Add($"Не удалось создать ФСП по начислению с идентификатором {inputNach.EmpId}, т.к. код плательщика имеет неверный формат (содержит меньше трех символов)");
				return null;
			}

			OMFsp fsp = new OMFsp
			{
				FspNumber = inputNach.Kodpl,
				FspType_Code = FSPType.EPD,
				Ls = inputNach.Ls,
				//INSUR_FSP.DATE_OPEN = PERIOD_REG_DATE (это должно быть первое число загружаемого периода , например, 01.10.2018)
				//Этот параметр в БД должен быть БЕЗ времени
				DateOpen = inputNach.PeriodRegDate.HasValue ?
					(DateTime?)new DateTime(inputNach.PeriodRegDate.Value.Year, inputNach.PeriodRegDate.Value.Month, 1) :
					null,
				Kodpl = inputNach.Kodpl,
				OplKodpl = GetOplKodpl(inputNach.DistrictId, inputNach.PeriodRegDate, inputNach.Kodpl, inputNach.Opl, inputNach.SumNach, inputNach.FlatTypeId, inputNach.Fopl)
			};

			timingCreateFspCount++;

			return fsp;
		}

		/// <summary>
		/// Формирование ФСП для Реестра зачислений INSUR_INPUT_PLAT
		/// </summary>
		/// <param name="inputPlat">Объект зачислений</param>
		/// <returns></returns>
		private OMFsp CreatePlatFsp(OMInputPlat inputPlat, out List<string> errors)
		{
			errors = new List<string>();

			if (inputPlat == null)
			{
				errors.Add("Не удалось создать ФСП по зачислению, т.к. не удалось определить запись зачисления");
				return null;
			}

			if (inputPlat.Kodpl.IsNullOrEmpty())
			{
				errors.Add($"Не удалось создать ФСП по зачислению, т.к. не удалось определить код плательщика для зачисления с идентификатором {inputPlat.EmpId}");
				return null;
			}

			if (inputPlat.Kodpl.Length < 3)
			{
				errors.Add($"Не удалось создать ФСП по зачислению с идентификатором {inputPlat.EmpId}, т.к. код плательщика имеет неверный формат (содержит меньше трех символов)");
				return null;
			}

			OMFsp fsp = new OMFsp
			{
				FspType_Code = FSPType.EPD,
				Ls = inputPlat.Ls,
				//INSUR_FSP.DATE_OPEN = PERIOD_REG_DATE (это должно быть первое число загружаемого периода , например, 01.10.2018)
				//Этот параметр в БД должен быть БЕЗ времени
				DateOpen = inputPlat.PeriodRegDate.HasValue ?
					(DateTime?)new DateTime(inputPlat.PeriodRegDate.Value.Year, inputPlat.PeriodRegDate.Value.Month, 1) :
					null,
				Kodpl = inputPlat.Kodpl,
				OplKodpl = GetOplKodpl(inputPlat.DistrictId, inputPlat.PeriodRegDate, inputPlat.Kodpl, inputPlat.Opl, inputPlat.SumOpl, inputPlat.FlatTypeId, null)
			};

			return fsp;
		}

		private OMFsp CreatePolicySvdFsp(OMPolicySvd policySvd)
		{
			if (policySvd == null)
			{
				return null;
			}

			OMFsp fsp = new OMFsp();
			fsp.FspNumber = policySvd.Npol;
			fsp.FspType_Code = policySvd.TypeDoc_Code == DocumentType.Polis ?
				FSPType.Polis :
				FSPType.Svidetelstvo;
			fsp.Ls = policySvd.Ls;
			fsp.ContractId = policySvd.EmpId;
			fsp.IdReestrContr = OMPolicySvd.GetRegisterId();

			//long fspObject = FindFspObject(inputNach.Unom.Value, inputNach.Kvnom);
			//if (fspObject != null)
			//{
			//    fsp.ObjId = fspObject.Id;
			//    fsp.ObjReestrId = fspObject.ObjReestrId;
			//}

			fsp.Save();
			return fsp;
		}

		private OMFsp CreateAllPropertyFsp(OMAllProperty allProperty)
		{
			if (allProperty == null)
			{
				return null;
			}

			OMFsp fsp = new OMFsp();
			fsp.FspNumber = allProperty.Ndog;
			fsp.FspType_Code = FSPType.ObscheeImuschestvo;
			fsp.ContractId = allProperty.EmpId;
			fsp.IdReestrContr = OMAllProperty.GetRegisterId();

			//TODO поиск объекта
			//object fspObject = FindFspObject(inputNach.Unom.Value, inputNach.Kvnom);
			//if (fspObject != null)
			//{
			//    fsp.ObjId = fspObject.Id;
			//    fsp.ObjReestrId = fspObject.ObjReestrId;
			//}

			fsp.Save();
			return fsp;
		}

		public List<OMFlat> LinkFspFlat(OMFsp fsp, long? unom, string kvnom, out List<string> errors, Dictionary<long, OMBuilding> cacheBuildings = null)
		{
			List<OMFlat> flatList = null;
			errors = new List<string>();

			if (fsp == null)
			{
				errors.Add("Был передан пустой объект ФСП");
				return flatList;
			}

			if (fsp.EmpId <= 0)
			{
				errors.Add("Невозможно связать объект с несохраненным ФСП");
				return flatList;
			}

			if (fsp.Kodpl.IsNullOrEmpty())
			{
				errors.Add($"Не удалось связать объект с ФСП, т.к. не удалось определить код плательщика (идентификатор ФСП {fsp.EmpId})");
				return flatList;
			}

			if (fsp.Kodpl.Length < 3)
			{
				errors.Add($"Не удалось связать объект с ФСП, т.к. код плательщика имеет неверный формат (содержит меньше трех символов) (идентификатор ФСП {fsp.EmpId})");
				return flatList;
			}

			if (kvnom.IsNullOrEmpty())
			{
				errors.Add($"Не удалось связать объект с ФСП, т.к. передан не пустой номер квартиры (идентификатор ФСП {fsp.EmpId})");
				return flatList;
			}

			Stopwatch sw = Stopwatch.StartNew();

			OMBuilding fspObject = FindFspObject(unom, cacheBuildings);

			if (fspObject == null)
			{
				errors.Add($"Не удалось определить объект (здание) по UNOM {unom} (идентификатор ФСП {fsp.EmpId})");
			}
			else
			{
				//CIPJS-674 определяем район по коду плательщика
				OMDistrict mfcDistict = Districts.FirstOrDefault(x => x.Code.HasValue && x.Code.Value.ToString() == fsp.Kodpl.Substring(0, 3));

				if (mfcDistict == null)
				{
					errors.Add($"Не удалось связать объект с ФСП, т.к не удалось определить район по справочнику МФЦ (insur_distict) по коду плательщика {fsp.Kodpl} (идентификатор ФСП {fsp.EmpId})");
				}
				else if (!mfcDistict.RefAddrDistrictId.HasValue)
				{
					errors.Add($"Не удалось связать объект с ФСП, т.к не заполнена связка с районом БТИ по справочнику МФЦ (insur_distict) с кодом {mfcDistict.Code} (идентификатор ФСП {fsp.EmpId})");
				}
				else if (!fspObject.DistrictId.HasValue)
				{
					errors.Add($"Не удалось связать объект с ФСП, т.к не заполнен район для объекта с UNOM {fspObject.Unom} (идентификтор объекта {fspObject.EmpId}) (идентификатор ФСП {fsp.EmpId})");
				}
				//сравниваем по связке района МФЦ с районом БТИ
				else if (fspObject.DistrictId != mfcDistict.RefAddrDistrictId)
				{
					errors.Add($"Не удалось связать объект с ФСП, т.к районы для объекта с UNOM {fspObject.Unom} (идентификтор {fspObject.EmpId}) и с кодом плательщика {fsp.Kodpl} (идентификатор ФСП {fsp.EmpId}) не совпадают");
				}
				else
				{
					sw.Stop();

					timingCreateFspFindFspObject += sw.Elapsed.Ticks;

					sw.Restart();

					flatList = FindFspFlats(fspObject.EmpId, kvnom);

					sw.Stop();

					timingCreateFspFindFlat += sw.Elapsed.Ticks;
					timingLastFlatFindTime = sw.Elapsed.Ticks;

					sw.Restart();

					if (flatList.Count == 0)
					{
						errors.Add($"Не удалось определить объект (квартира) по зданиию с идентификатором с UNOM {fspObject.Unom} (идентификтор {fspObject.EmpId}) и номеру квартиры {kvnom} (идентификатор ФСП {fsp.EmpId})");
					}
				}
			}

			timingCreateFspCreateFlat += sw.Elapsed.Ticks;
			sw.Stop();

			if (flatList != null && flatList.Count > 0)
			{
				fsp.ObjId = flatList.First().EmpId;
				fsp.ObjReestrId = OMFlat.GetRegisterId();

				//CIPJS-746 Заполняем FLAG_MANY_OBJ(логическое) = 1, NUM_OBJ(целое число) = N
				if (flatList.Count > 1)
				{
					fsp.FlagManyObj = true;
					fsp.NumObj = flatList.Count;
					//CIPJS-777 flag_many_obj = 1, то insur_fsp_q.obj_id = insur_link_fsp_flat.FSP_ID and insur_fsp_q.obj_reestr_id = 383
					fsp.ObjId = fsp.EmpId;
					fsp.ObjReestrId = OMLinkFspFlat.GetRegisterId();

					foreach (OMFlat flat in flatList)
					{
						OMLinkFspFlat linkFspFlat = OMLinkFspFlat.Where(x => x.FspId == fsp.EmpId && x.ObjId == flat.EmpId).ExecuteFirstOrDefault();

						if (linkFspFlat == null)
						{
							linkFspFlat = new OMLinkFspFlat
							{
								FspId = fsp.EmpId,
								ObjId = flat.EmpId
							};

							linkFspFlat.Save();
						}
					}
				}
			}

			return flatList;
		}

		public void SetFspNumber(OMFsp fsp, string kodPl, long? unom, string kvNom, long? flatObjectId = null, OMFlat flatObject = null)
		{
			Stopwatch sw = Stopwatch.StartNew();

			if (fsp == null)
			{
				throw new Exception("Был передан пустой объект ФСП");
			}

			if (flatObject == null
				&& fsp.ObjId.HasValue
				&& fsp.ObjReestrId == OMFlat.GetRegisterId())
			{
				flatObject = OMFlat.Where(x => x.EmpId == fsp.ObjId.Value)
					.Select(x => x.Unom)
					.Select(x => x.Kvnom)
					.Select(x => x.ParentBuilding.Unom)
					.ExecuteFirstOrDefault();
			}

			// Получаем номер в однопоточном режиме для гарантии уникальности номера ФСП
			lock (LockObject)
			{
				//ОЧЕНЬ ВАЖНО ЭТО ИМЕННО UNOM здания, с которым связано помещение, а не значение UNOM  из строки в реестре начисление/зачисление !!!!) 
				fsp.FspNumber = flatObject != null ?
					GetFspNumber(kodPl.IsNotEmpty() ? kodPl : fsp.Kodpl, flatObject.ParentBuilding?.Unom, flatObject.Kvnom, flatObject.EmpId) :
					GetFspNumber(kodPl.IsNotEmpty() ? kodPl : fsp.Kodpl, unom, kvNom);
			}

			sw.Stop();

			timingCreateFspGetFspNumber += sw.Elapsed.Ticks;
		}

		public List<string> Validate(FSPType? fspType, OMFlat flat, string kodpl,
			string npol, decimal? oplKodpl,
			DateTime? dateBegin, DateTime? dateEnd, InsuranceTerms? pralt,
			out DocumentType documentType,
			out long? existPolicyId)
		{
			List<string> errors = new List<string>();
			existPolicyId = null;
			DocumentType docType = documentType = DocumentType.None;

			if (flat == null)
			{
				errors.Add("Не удалось определить объект страхования");
				return errors;
			}

			switch (fspType)
			{
				case FSPType.EPD:
					if (kodpl.IsNullOrEmpty())
					{
						errors.Add("Поле код плательщика обязательно для заполенения");
					}
					if (!oplKodpl.HasValue)
					{
						errors.Add("Поле площадь по договору обязательно для заполенения");
					}
					if (!dateBegin.HasValue)
					{
						errors.Add("Поле дата начала действия договора обязательно для заполенения");
					}
					break;
				case FSPType.Polis:
				case FSPType.Svidetelstvo:
					if (npol.IsNullOrEmpty())
					{
						errors.Add("Поле номер полиса/свидетельства обязательно для заполенения");
					}
					if (!oplKodpl.HasValue)
					{
						errors.Add("Поле площадь по договору обязательно для заполенения");
					}
					if (!dateBegin.HasValue)
					{
						errors.Add("Поле дата начала действия договора обязательно для заполенения");
					}
					if (fspType == FSPType.Polis && (!pralt.HasValue || pralt.Value == InsuranceTerms.None))
					{
						errors.Add("Поле условия страхования обязательно для заполенения");
					}
					docType = documentType = fspType == FSPType.Polis ? DocumentType.Polis : DocumentType.Certificate;
					break;
				case FSPType.ObscheeImuschestvo:
					errors.Add("Неподдерживаемый тип договора ");
					break;
				default:
					errors.Add("Тип договора обязателен для заполнения");
					break;
			}

			if (errors.Count == 0
				&& (fspType == FSPType.Polis || fspType == FSPType.Svidetelstvo))
			{
				existPolicyId = OMPolicySvd.Where(x => x.Npol == npol
					&& x.Dat == dateBegin
					&& x.TypeDoc_Code == docType)
					.ExecuteFirstOrDefault()?.EmpId;
			}

			return errors;
		}

		public OMFsp Create(FSPType? fspType, long objId, string kodpl, string npol, decimal? oplKodpl,
			DateTime? dateBegin, DateTime? dateEnd, InsuranceTerms? pralt, long? insuranceOrganizationId,
			decimal? ss, decimal? soplvz,
			out DocumentType documentType,
			out List<string> errors,
			out long? existPolicyId)
		{
			OMFlat flat = OMFlat.Where(x => x.EmpId == objId)
				.Select(x => x.Unom)
				.Select(x => x.Kvnom)
				.Select(x => x.Fopl)
				.Select(x => x.ParentBuilding.OkrugId)
				.Select(x => x.ParentBuilding.DistrictId)
				.ExecuteFirstOrDefault();

			errors = Validate(fspType, flat, kodpl, npol, oplKodpl, dateBegin, dateEnd, pralt, out documentType, out existPolicyId);

			if (errors.Count > 0)
			{
				return null;
			}

			if (existPolicyId.HasValue)
			{
				return null;
			}

			switch (fspType)
			{
				case FSPType.EPD:
					OMFsp fspEpd = new OMFsp
					{
						FspType_Code = FSPType.EPD,
						OplKodpl = oplKodpl,
						Kodpl = kodpl,
						DateOpen = dateBegin,
						ObjReestrId = OMFlat.GetRegisterId(),
						ObjId = flat.EmpId,
						FspNumber = GetFspNumber(kodpl, flat.Unom, flat.Kvnom)
					};
					fspEpd.Save();

					if (dateBegin.HasValue)
					{
						AccountFsp(fspEpd.EmpId, dateBegin.Value, fsp: fspEpd);
						CalcBalanceSumFromPeriod(fspEpd.EmpId, dateBegin.Value, fsp: fspEpd);
					}
					return fspEpd;
				case FSPType.Polis:
				case FSPType.Svidetelstvo:
					OMFsp fspPolicy;
					using (TransactionScope ts = TransactionScopeWrapper.OpenTransaction())
					{
						OMPolicySvd policy = new OMPolicySvd
						{
							TypeDoc_Code = documentType,
							InsuranceOrganizationId = insuranceOrganizationId,
							OkrugId = flat.ParentBuilding?.OkrugId,
							DistrictId = flat.ParentBuilding?.DistrictId,
							Unom = flat.Unom,
							Kvnom = flat.Kvnom,
							Fopl = flat.Fopl,
							Opl = oplKodpl,
							Npol = npol,
							Dat = dateBegin,
							DatEnd = dateEnd,
							Pralt_Code = pralt.HasValue ? pralt.Value : InsuranceTerms.None,
							Soplvz = soplvz,
							Ss = ss,
							InputUser = SRDSession.GetCurrentUserId()
						};
						policy.Save();

						fspPolicy = new OMFsp
						{
							FspType_Code = fspType.HasValue ? fspType.Value : FSPType.Polis,
							Kodpl = kodpl,
							OplKodpl = oplKodpl,
							DateOpen = dateBegin,
							ObjReestrId = OMFlat.GetRegisterId(),
							ObjId = flat.EmpId,
							FspNumber = GetFspNumber(npol, flat.Unom, flat.Kvnom),
							IdReestrContr = OMPolicySvd.GetRegisterId(),
							ContractId = policy.EmpId
						};
						fspPolicy.Save();

						policy.FspId = fspPolicy.EmpId;
						policy.Save();

						if (dateBegin.HasValue)
						{
							AccountFsp(fspPolicy.EmpId, dateBegin.Value, fsp: fspPolicy, policySvd: policy);
							CalcBalanceSumFromPeriod(fspPolicy.EmpId, dateBegin.Value, fsp: fspPolicy, policySvd: policy);
						}

						ts.Complete();
					}

					return fspPolicy;
				default:
					throw new Exception("Передан неподдерживаемый тип ФСП");
			}
		}

		/// <summary>
		/// Поиск ФСП, на котором начисление должно быть отражено
		/// </summary>
		/// <param name="inputNach">Начисление</param>
		public void SetNachFsp(OMInputNach inputNach, out List<string> errors, Dictionary<long, OMBuilding> cacheBuildings)
		{
			errors = new List<string>();

			if (inputNach == null)
			{
				errors.Add("Не удалось связать начисление с ФСП, т.к. не удалось определить запись начисления");
				return;
			}

			if (inputNach.Kodpl.IsNullOrEmpty())
			{
				errors.Add($"Не удалось связать начисление с ФСП, т.к. не удалось определить код плательщика для начисления с идентификатором {inputNach.EmpId}");
				return;
			}

			OMFsp fsp = OMFsp.Where(x => x.Kodpl == inputNach.Kodpl)
				.Select(x => x.DateOpen)
				.Select(x => x.OplKodpl)
				.Select(x => x.OplKodplUpdateDate)
                .Select(x => x.UnomFsp)
                .Select(x => x.KvnomFsp)
				.Execute().FirstOrDefault();

			if (fsp == null)
			{
				List<string> createErrors;
				fsp = CreateNachFsp(inputNach, out createErrors);
				errors.AddRange(createErrors);

				if (fsp != null)
				{
					//CIPJS-752 сохраняем объект перед связкой с объектом, т.к. фсп может быть связан с несколькими объектами
					fsp.Save();

					List<string> linkErrors;
					List<OMFlat> flatList = LinkFspFlat(fsp, inputNach.Unom, inputNach.Kvnom, out linkErrors, cacheBuildings);
					errors.AddRange(linkErrors);

					SetFspNumber(fsp, inputNach.Kodpl, inputNach.Unom, inputNach.Kvnom, flatList?.FirstOrDefault()?.EmpId, flatList?.FirstOrDefault());

					fsp.Save();
				}
			}

			if (fsp == null)
			{
				return;
			}

            //CIPJS-952: Исправить ошибку - при создании ФСП ( на примере загрузке МФЦ за март 2019) не заполняются поля UNOM_MFC, KVNOM_MFC
            if (inputNach?.Unom != null)
            {
                fsp.UnomFsp = inputNach.Unom;
            }

            if (inputNach?.Kvnom != null)
            {
                fsp.KvnomFsp = inputNach.Kvnom;
            }

            //CIPJS-241 Когда ищем ФСП для того чтобы учесть начисление/зачисление и не находим ,
            //то запускается функция по созданию ФСП , добавить обработку даты "Дата открытия ФСП"
            //сопоставляем PERIOD_REG_DATE начисления / зачисления И Дату открытия ФСП,
            //если ДАТА открытия ФСП < PERIOD_REG_DATE начисления / зачисления, то обновлять " Дата открытия ФСП" = PERIOD_REG_DATE начисления / зачисления
            if (fsp.DateOpen > inputNach.PeriodRegDate)
			{
				fsp.DateOpen = inputNach.PeriodRegDate;
			}

			//CIPJS-781 Реализовать алгоритм учета изменения площади ФСП , 
			//при поступлении новых начислений МФЦ ( обратить внимание только НАЧИСЛЕНИЙ).
			//Если значение INSUR_INPUT_NACH.OPL <> INSUR_FSP.OPL_KODPL ( для ACTUAL=1) И при этом INSUR_INPUT_NACH.OPL<>0 , то
			//INSUR_FSP.OPL_KODPL=INSUR_INPUT_NACH.OPL
			if (inputNach.Opl.HasValue && inputNach.Opl.Value != 0
				&& inputNach.Opl != fsp.OplKodpl)
			{
				fsp.OplKodpl = inputNach.Opl;
				fsp.OplKodplUpdateDate = inputNach.PeriodRegDate;
			}

			if (fsp.PropertyChangedList.Count > 0)
			{
				fsp.Save();
			}

			inputNach.FspId = fsp.EmpId;
			inputNach.StatusIdentif_Code = StatusIdentifikacii.Identified;
		}

		/// <summary>
		/// Поиск ФСП, на котором зачисление должно быть отражено
		/// </summary>
		/// <param name="inputPlat">Зачисление</param>
		public void SetPlatFsp(OMInputPlat inputPlat, out List<string> errors)
		{
			errors = new List<string>();

			if (inputPlat == null)
			{
				errors.Add("Не удалось связать зачисление с ФСП, т.к. не удалось определить запись зачисления");
				return;
			}

			if (inputPlat.Kodpl.IsNullOrEmpty())
			{
				errors.Add($"Не удалось связать зачисление с ФСП, т.к. не удалось определить код плательщика для зачисления с идентификатором {inputPlat.EmpId}");
				return;
			}

			OMFsp fsp = OMFsp.Where(x => x.Kodpl == inputPlat.Kodpl).SelectAll().Execute().FirstOrDefault();

			if (fsp == null)
			{
				List<string> createErrors;
				fsp = CreatePlatFsp(inputPlat, out createErrors);
				errors.AddRange(createErrors);

				if (fsp != null)
				{
					//CIPJS-752 сохраняем объект перед связкой с объектом, т.к. фсп может быть связан с несколькими объектами
					fsp.Save();

					List<string> linkErrors;
					List<OMFlat> flatList = LinkFspFlat(fsp, inputPlat.Unom, inputPlat.Nom, out linkErrors);
					errors.AddRange(linkErrors);

					SetFspNumber(fsp, inputPlat.Kodpl, inputPlat.Unom, inputPlat.Nom, flatList?.FirstOrDefault()?.EmpId, flatList?.FirstOrDefault());

					fsp.Save();
				}
			}

			if (fsp == null)
			{
				return;
			}

            if(inputPlat.Unom != null)
            {
                fsp.UnomFsp = inputPlat.Unom;
            }
            if (inputPlat.Nom != null)
            {
                fsp.KvnomFsp = inputPlat.Nom;
            }
            //CIPJS-241 Когда ищем ФСП для того чтобы учесть начисление/зачисление и не находим ,
            //то запускается функция по созданию ФСП , добавить обработку даты "Дата открытия ФСП"
            //сопоставляем PERIOD_REG_DATE начисления / зачисления И Дату открытия ФСП,
            //если ДАТА открытия ФСП < PERIOD_REG_DATE начисления / зачисления, то обновлять " Дата открытия ФСП" = PERIOD_REG_DATE начисления / зачисления
            if (fsp.DateOpen > inputPlat.PeriodRegDate)
			{
				fsp.DateOpen = inputPlat.PeriodRegDate;
			}

			if (fsp.PropertyChangedList.Count > 0)
			{
				fsp.Save();
			}

			inputPlat.FspId = fsp.EmpId;
		}

		/// <summary>
		/// Поиск объекта ФСП по UNOM и KVNOM в 
		/// </summary>
		/// <param name="unom"></param>
		/// <param name="kvNom"></param>
		/// <returns></returns>
		public OMBuilding FindFspObject(long? unom, Dictionary<long, OMBuilding> cacheBuildings = null)
		{
			if (!unom.HasValue)
			{
				return null;
			}

			//Сначала ищем в Реестре INSUR_BUILDING запись, для которой INSUR_BUILDING.UNOM = INSUR_INPUT_NACH. UNOM 
			//или INPUT_INPUT_ PLAT.UNOM (смотря что обрабатываем начисления/ или зачисления)
			OMBuilding insurBuilding = null;
			if (cacheBuildings != null)
			{
				insurBuilding = cacheBuildings.ContainsKey(unom.Value) ? cacheBuildings[unom.Value] : null;
			}

			if (insurBuilding == null)
			{
				insurBuilding = OMBuilding.Where(x => x.Unom == unom.Value).SelectAll().Execute().FirstOrDefault();
			}

			//сли не нашли МКД, то пытаемся искать по UNOM  через BTI_BILDING.UNOM, если находим в БТИ такое здание,
			//то через INSUR_LINK_BUILD_BTI устанавливаем связь между INSUR_BUILDING И BTI_BUILDING ,
			//и находим соответствующий INSUR_BUILDING.EMP_ID (нашли МКД)
			if (insurBuilding == null)
			{
				OMLinkBuildBti linkBuildBti = OMLinkBuildBti.Where(x => x.ParentBtiBuilding.Unom == unom.Value).SelectAll().Execute().FirstOrDefault();

				if (linkBuildBti != null && linkBuildBti.IdInsurBuild.HasValue)
				{
					insurBuilding = OMBuilding.Where(x => x.EmpId == linkBuildBti.IdInsurBuild.Value).SelectAll().Execute().FirstOrDefault();
				}
			}

			return insurBuilding;
		}

		public List<OMFlat> FindFspFlats(long buildingId, string kvnom)
		{
			List<OMFlat> flatList = new List<OMFlat>();

			if (kvnom.IsNullOrEmpty())
			{
				return flatList;
			}

			QSQuery<OMFlat> flatQuery = OMFlat.Where(x => x.LinkObjectMkd == buildingId
					&& (x.Kvnom.ToUpper() == kvnom.ReplaceCharactersWithRussian()
					|| x.Kvnom.ToUpper() == kvnom.ReplaceCharactersWithEnglish()))
						.Select(x => x.FlatStatus)
						.Select(x => x.Prkom)
						.Select(x => x.Kvnom)
						.Select(x => x.ParentBuilding.Unom);

			//Поиск в INSUR_FLAT записи, для которой   выполняются два условия:
			//INSUR_FLAT.KVNOM = INSUR_INPUT_NACH.KVNOM / или INPUT_INPUT_PLAT.NOM(смотря что обрабатываем начисления / или зачисления)
			//INSUR_FLAT.LINK_OBJECT_MKD = INSUR_BUILDING.EMP_ID
			OMFlat flatObject = flatQuery.ExecuteFirstOrDefault();

			if (flatObject != null)
			{
				timingLastFlatSql = flatQuery.GetSql();
				flatList.Add(flatObject);
				return flatList;
			}

			List<string> kvnomList = new List<string>();

			//CIPJS-746 Если в 306 (INSUR_FLAT) не нашли по номеру квартиры подходящую строку то пытаемся разобрать поле KVNOM по следующему алгоритму
			//Если номер прописан по следующему шаблону:
			//Номер1 - Номер2(например, 188 - 189) , то
			//выделяем отдельно Номер1, Номер2
			//переводим Номер1, Номер2 в число, если перевести невозможно то выход
			//Если Номер 2 < Номер1 то выход, иначе Номер2 - Номер1 = N , если N -не целое число, то выход , иначе N-кол - во квартир, которое нужно найти
			//Осуществляем поиск N квартир: UNOM + KVNOM = Номер1(вернули в строку), UNOM + KVNOM = Номер1 + 1(перевели в строку).........UNOM + KVNOM = Номер1 + N = Номер2
			//Заполняем реестр связей INSUR_LINK_FSP_FLAT
			//Заполняем
			//FLAG_MANY_OBJ(логическое) = 1
			//NUM_OBJ(целое число) = N
			MatchCollection rangeMatches = Regex.Matches(kvnom, @"(\d+)\s*-\s*(\d+)?");
			if (rangeMatches.Count > 0)
			{
				foreach (Match rangeMatch in rangeMatches)
				{
					int firstKvnom;
					if (!int.TryParse(rangeMatch.Groups[1].Value, out firstKvnom))
					{
						continue;
					}

					int lastKvnom;
					if (rangeMatch.Groups.Count < 2
						|| !int.TryParse(rangeMatch.Groups[2].Value, out lastKvnom))
					{
						lastKvnom = firstKvnom;
					}

					if (firstKvnom > lastKvnom)
					{
						continue;
					}

					for (int i = firstKvnom; i <= lastKvnom; i++)
					{
						string currentKvnom = i.ToString();
						if (!kvnomList.Contains(currentKvnom))
						{
							kvnomList.Add(currentKvnom);
						}
					}
				}
			}

			//3.2) Если номер прописан по следующему шаблону: Номер1, Номер2, Номер3 и т.д , числа через запятую (Например, KVNOM= 160,161
			//или KVNOM = 160,161,178
			if (kvnom.Contains(','))
			{
				string[] kvnomParts = kvnom.Split(',');
				foreach (string kvnomPart in kvnomParts)
				{
					if (kvnomPart.IsNullOrEmpty())
					{
						continue;
					}

					int kvnomInt;
					if (!int.TryParse(kvnomPart.Trim(), out kvnomInt))
					{
						continue;
					}

					string currentKvnom = kvnomInt.ToString();
					if (!kvnomList.Contains(currentKvnom))
					{
						kvnomList.Add(kvnomInt.ToString());
					}
				}
			}

			if (kvnomList.Count > 0)
			{
				flatQuery = OMFlat.Where(x => x.LinkObjectMkd == buildingId && kvnomList.Contains(x.Kvnom))
						.Select(x => x.FlatStatus)
						.Select(x => x.Prkom)
						.Select(x => x.Kvnom)
						.Select(x => x.ParentBuilding.Unom);

				//timingLastFlatSql = flatQuery.GetSql();

				return flatQuery.Execute();
			}


			return flatList;
		}

		/// <summary>
		/// Учет начисления/зачисления на ФСП
		/// </summary>
		/// <param name="fspId">Идентификатор ФСП</param>
		/// <param name="periodRegDate">Период учета</param>
		public void AccountFsp(long fspId, DateTime periodRegDate,
			OMBalance balance = null, List<OMInputPlat> inputPlats = null, OMInputNach strahNach = null, List<OMBalance> balances = null,
			OMFsp fsp = null, OMPolicySvd policySvd = null, OMTariff tariff = null)
		{
			if (fsp == null)
			{
				fsp = OMFsp.Where(x => x.EmpId == fspId).SelectAll().ExecuteFirstOrDefault();
				if (fsp == null)
				{
					throw new Exception("Не удалось определить ФСП для учета начисления/зачисления");
				}
			}

			if (fsp.FspType_Code == FSPType.Polis && policySvd == null)
			{
				//получаем самый актуальный полис на текущую дату
				policySvd = GetPolicyByDate(fspId);
			}

			//Осуществляется поиск записи в INSUR_BALANCE
			if (balance == null)
			{
				balance = OMBalance
					.Where(x => x.FspId == fspId && x.PeriodRegDate == periodRegDate)
					.Select(x => x.OstatokSum)
					.ExecuteFirstOrDefault();
			}

			if (strahNach == null)
			{
				strahNach = _strahNachService.GetByFspIdPeriodFromGbuCache(fspId, periodRegDate);
			}

			if (strahNach == null)
			{
				_strahNachService.CreateByFspIdPeriodInGbuCache(fspId, periodRegDate, fsp, tariff);
			}

			//Если такой записи нет, создается новая запись со следующими атрибутами
			if (balance == null)
			{
				balance = new OMBalance
				{
					FspId = fspId,
					PeriodRegDate = periodRegDate,
					FlagOpl = false,
					OstatokSum = CalcUndestributedBalanceOnPeriodBegin(fspId, periodRegDate, balances)
				};
				balance.Save();
			}

			//CIPJS-276 Нужно еще доработать процедуру обработки начислений/ зачислений. 
			//Вы сейчас сделали чтобы проверялось наличие периода +1 , относительно периода учета 
			//и создаете строку в балансовой ведомости если такого периода нет! Теперь нужно расширить это место. 
			//Вам нужно написать чтобы строки в балансовой ведомости были сформированы от период= PERIOD_REG_DATE = периоду учета 
			//операции ПО PERIOD_REG_DATE= актуальный месяц .
			//То есть например, сейчас ДЕКАБРЬ 2018 ( актуальный месяц) , 
			//я обрабатываю начисления АПРЕЛЯ 2018 , 
			//ВЫ при обработке должны создать в балансовой ведомости ( в СЛУЧАЕ ИХ ОТСУТСТВИЯ - НИВ КОЕМ СЛУЧАЕ НЕ ЗАДУБЛИРОВАТЬ) 
			//апрель2018, май2018, июнь2018, июль2018, август2018, сентябрь 2018 , октябрь 2018, ноябрь 2018, декабрь2018. 
			//и с даты учета операции произвести ПЕРЕРАСЧЕТ каждой из этих строк !!!
			//CIPJS-630 если фсп типа полис, то создаем записи баланса на весь период действия полиса
			DateTime actualPeriod = fsp.FspType_Code == FSPType.Polis && policySvd != null && policySvd.DatEnd.HasValue ?
				new DateTime(policySvd.DatEnd.Value.Year, policySvd.DatEnd.Value.Month, 1) :
				new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
			DateTime nextPeriod = fsp.FspType_Code == FSPType.Polis && policySvd != null && policySvd.Dat.HasValue ?
				new DateTime(policySvd.Dat.Value.Year, policySvd.Dat.Value.Month, 1) :
				new DateTime(periodRegDate.Year, periodRegDate.Month, 1);
			while (actualPeriod > nextPeriod)
			{
				nextPeriod = nextPeriod.AddMonths(1);
				OMBalance nextBalance = GetBalance(fspId, nextPeriod);
				if (nextBalance == null)
				{
					nextBalance = CreateBalance(fspId, nextPeriod);
				}

				//CIPJS-778 создание контрольных начислений совмещено с созданием балансов
				OMInputNach nextStrahNach = _strahNachService.GetByFspIdPeriodFromGbuCache(fspId, nextPeriod);
				if (nextStrahNach == null)
				{
					_strahNachService.CreateByFspIdPeriodInGbuCache(fspId, nextPeriod, fsp, tariff);
				}
			}

			decimal? sumZach = GetSumZachByPeriod(fspId, periodRegDate, inputPlats);
			//decimal? sumNach = GetSumNachByPeriod(fspId, periodRegDate, inputNach);
			decimal? sumNach = strahNach == null ? _strahNachService.GetByFspIdPeriodFromGbuCache(fspId, periodRegDate)?.SumNach : strahNach.SumNach;

			//Определяется значение временной переменной  ITOGO_SUM
			//ITOGO_SUM= SUM_ZACH + INSUR_BALANCE.OSTATOK_SUM
			decimal? totalSum = sumZach.HasValue && balance.OstatokSum.HasValue ?
				sumZach.Value + balance.OstatokSum.Value :
				sumZach.HasValue ? sumZach :
				balance.OstatokSum.HasValue ? balance.OstatokSum : null;

			//Если ITOGO_SUM>= SUM_NACH, то  INSUR_BALANCE.FLAG_OP=1 (начисление оплачено), 
			//иначе INSUR_BALANCE.FLAG_OP=0 (начисление не оплачено)
			balance.FlagOpl = (fsp.FspType_Code == FSPType.Polis && policySvd != null) || (totalSum.HasValue && totalSum.Value > 0m
				&& (!sumNach.HasValue || sumNach.Value <= totalSum.Value));

			//Выполняется процедура «Определение статуса страхования периода» для PERIOD = INSUR_INPUT_NACH.PERIOD +1
			balance.FlagInsur = (fsp.FspType_Code == FSPType.Polis && policySvd != null) || GetInsureStatusByPeriod(fspId, periodRegDate.AddMonths(1), balances);

			balance.Save();
		}

		/// <summary>
		/// Создание контрольный начислений.
		/// </summary>
		/// <param name="fspId"></param>
		/// <param name="periodRegDate"></param>
		/// <param name="fsp"></param>
		/// <param name="tariff"></param>
		/// <returns></returns>
		public OMInputNach CreateGbuInGbuCache(long fspId, DateTime periodRegDate, OMFsp fsp = null, OMTariff tariff = null)
		{
			return _strahNachService.CreateByFspIdPeriodInGbuCache(fspId, periodRegDate, fsp, tariff);
		}

		/// <summary>
		/// Расчета значения Нераспределенного остатка на начало периода
		/// </summary>
		/// <param name="fspId">Идентификатор ФСП</param>
		/// <param name="periodRegDate">Период</param>
		/// <returns></returns>
		public decimal CalcUndestributedBalanceOnPeriodBegin(long fspId, DateTime periodRegDate, List<OMBalance> balanceList = null, List<OMInputPlat> inputPlatList = null)
		{
			//получаем список периодов, с оплаченными начислениями для заданного ФСП
			if (balanceList == null)
			{
				balanceList = OMBalance.Where(x => x.FspId == fspId && x.PeriodRegDate < periodRegDate && x.FlagOpl == true)
									   .OrderByDescending(x => x.PeriodRegDate)
									   .Execute();
			}

			//определить временную переменную ITOGO_SUM_NACH (всего начислено в оплаченных периодах)
			decimal sumNachTotal = 0;

			// Для каждого периода из п.1) с помощью Процедуры 
			// «Определения суммы начисления страхового взноса в периоде» определить значение SUM_NACH.
			// ITOGO_SUM_NACH = SUM_NACH 1 + SUM_NACH 2….+SUM_NACH i, где  1…i - периоды выбранные в п.1)
			// TODO: почему в цикле не используются поля переменной balance?
			foreach (OMBalance balance in balanceList)
			{
				decimal? periodSumNach = GetSumNachByPeriodByCache(fspId, periodRegDate);
				if (periodSumNach.HasValue)
				{
					sumNachTotal += periodSumNach.Value;
				}
			}

			//определить сумму всего зачислено за период с даты открытия ФСП по предыдущий период
			decimal sumPlatTotal = 0;

			if (inputPlatList == null)
			{
				inputPlatList = OMInputPlat.Where(x => x.FspId == fspId
					&& x.StatusIdentif_Code == StatusIdentifikacii.Identified
					&& x.PeriodRegDate < periodRegDate)
					.Select(x => x.SumOpl)
					.Execute();
			}

			foreach (OMInputPlat inputPlat in inputPlatList)
			{
				if (inputPlat.SumOpl.HasValue)
				{
					sumPlatTotal += inputPlat.SumOpl.Value;
				}
			}

			return sumPlatTotal - sumNachTotal;
		}

		public decimal CalcUndestributedOstatokOnPeriodBegin(long fspId, DateTime periodRegDate)
		{
			DateTime actualPeriod = periodRegDate.AddMonths(-1);
			DateTime prevActualPeriod = actualPeriod.AddMonths(-1);

			OMBalance omBalancePrev = OMBalance.Where(w => w.FspId == fspId && w.PeriodRegDate == prevActualPeriod).SelectAll().ExecuteFirstOrDefault();
			if (omBalancePrev == null)
				return -1;
			OMBalance omBalanceActual = OMBalance.Where(w => w.FspId == fspId && w.PeriodRegDate == actualPeriod).SelectAll().ExecuteFirstOrDefault();

			decimal sumOplActual = omBalanceActual.SumOpl.HasValue ? omBalanceActual.SumOpl.Value : 0;
			decimal sumOplPrev = omBalancePrev.SumOpl.HasValue ? omBalancePrev.SumOpl.Value : 0;
			decimal ostatokSumActual = omBalanceActual.OstatokSum.HasValue ? omBalanceActual.OstatokSum.Value : 0;
			decimal sumNachGbyActual = omBalanceActual.SumNachGby.HasValue ? omBalanceActual.SumNachGby.Value : 0;
			decimal sumNachGbyPrev = omBalancePrev.SumNachGby.HasValue ? omBalancePrev.SumNachGby.Value : 0;

			decimal result1 = ostatokSumActual + (sumOplActual - sumOplPrev);
			decimal result2 = sumNachGbyActual - sumNachGbyPrev;

			return result1 - result2;
		}

		/// <summary>
		/// Определение суммы начисления страхового взноса в периоде
		/// </summary>
		/// <param name="fspId">Идентификатор ФСП</param>
		/// <param name="periodRegDate">Период</param>
		/// <param name="sourceType">Тип источника</param>
		/// <param name="withFlagOpl">Только с флагом оплачено</param>
		/// <returns></returns>
		public decimal? GetSumNachByPeriod(long fspId, DateTime periodRegDate, InsuranceSourceType sourceType = InsuranceSourceType.Gbu, bool withFlagOpl = false)
		{
			//Отобрать в Реестре начислений INSUR_INPUT_NACH записи, для которых:
			//INSUR_INPUT_ NACH.PERIOD = Период
			//INSUR_INPUT_ NACH.FSP_ID = INSUR_BALANCE.FSP_ID
			//INSUR_INPUT_ NACH.STATUS_IDENTIF = «Идентифицирован»
			return OMInputNach
				.Where(x => x.FspId == fspId
					&& x.StatusIdentif_Code == StatusIdentifikacii.Identified
					&& x.TypeSource_Code == sourceType
					&& x.PeriodRegDate == periodRegDate)
				.Select(x => x.SumNach)
				.Execute()
				.FirstOrDefault()?.SumNach;
		}

		/// <summary>
		/// Определение суммы контрольных начислений страхового взноса в периоде из кэша.
		/// </summary>
		/// <param name="fspId">Идентификатор ФСП</param>
		/// <param name="periodRegDate">Период</param>
		/// <param name="sourceType">Тип источника</param>
		/// <param name="withFlagOpl">Только с флагом оплачено</param>
		/// <returns></returns>
		public decimal? GetSumNachByPeriodByCache(long fspId, DateTime periodRegDate, bool withFlagOpl = false)
		{
			return _strahNachService.GetSumNachByPeriodByCache(fspId, periodRegDate, withFlagOpl);
		}

		/// <summary>
		/// Определение суммы зачисления в периоде
		/// </summary>
		/// <param name="fspId">Идентификатор ФСП</param>
		/// <param name="periodRegDate">Период</param>
		/// <returns></returns>
		public decimal? GetSumZachByPeriod(long fspId, DateTime periodRegDate, List<OMInputPlat> inputPlats = null)
		{
			decimal? sumZach = null;

			//Отобрать в Реестре зачислений (платежей) INSUR_INPUT_PLAT записи, для которых:
			//INSUR_INPUT_PLAT.PERIOD = Период
			//INSUR_INPUT_PLAT.FSP_ID = INSUR_BALANCE.FSP_ID
			//INSUR_INPUT_PLAT.STATUS_IDENTIF = «Идентифицирован»
			if (inputPlats == null)
			{
				inputPlats = OMInputPlat.Where(x => x.FspId == fspId &&
					x.StatusIdentif_Code == StatusIdentifikacii.Identified &&
					x.PeriodRegDate == periodRegDate)
					.Select(x => x.SumOpl)
					.Execute();
			}

			//SUM_ZACH = INSUR_INPUT_PLAT.SUM1 + INSUR_INPUT_PLAT.SUM2 + …+INSUR_INPUT_PLAT.SUMi
			foreach (OMInputPlat inputPlat in inputPlats)
			{
				if (inputPlat.SumOpl.HasValue)
				{
					if (sumZach.HasValue)
					{
						sumZach += inputPlat.SumOpl.Value;
					}
					else
					{
						sumZach = inputPlat.SumOpl.Value;
					}
				}
			}

			return sumZach;
		}

		/// <summary>
		/// Определение статуса страхования периода
		/// </summary>
		/// <param name="fspId">Идентификатор ФСП</param>
		/// <param name="periodRegDate">Период</param>
		/// <returns>Статус страхования периода = 1/0 (Застраховано/Не застраховано)</returns>
		public bool GetInsureStatusByPeriod(long fspId, DateTime periodRegDate, List<OMBalance> balanceList = null)
		{
			if (balanceList == null)
			{
				balanceList = OMBalance
					.Where(x => x.FspId == fspId && x.PeriodRegDate < periodRegDate)
					.OrderByDescending(x => x.PeriodRegDate)
					.Select(x => x.SumOpl)
					.Select(x => x.SumNachGby)
					.Select(x => x.OstatokSum)
					.Execute();
			}

			OMBalance previosPeriodBalance = balanceList.FirstOrDefault();
			OMBalance beforePreviousPeriodBalance = balanceList.Count > 1 ? balanceList[1] : null;

			return GetInsureStatus(previosPeriodBalance, beforePreviousPeriodBalance);
		}

        /// <summary>
        /// Перестроения остатков и оборотов в INSUR_ BALANCE
        /// </summary>
        /// <param name="fspId">Идентификатор ФСП</param>
        /// <param name="periodRegDate">Период</param>
        /// <returns>Статус страхования периода = 1/0 (Застраховано/Не застраховано)</returns>
        public void CalcBalanceSumFromPeriod(long fspId, DateTime periodRegDate, List<OMBalance> calcBalanceList = null, OMFsp fsp = null, OMPolicySvd policySvd = null, bool renewStrahEnd = false)
        {
            if (fsp == null)
            {
                fsp = OMFsp.Where(x => x.EmpId == fspId).SelectAll().ExecuteFirstOrDefault();
                if (fsp == null)
                {
                    throw new Exception("Не удалось определить ФСП перестроения остатков и оборотов");
                }
            }

            //Осуществляется поиск записи в INSUR_BALANCE

            //требуется отобрать записи из отсортированного списка 
            //от ПЕРИОД до последней самой актуальной
            if (calcBalanceList == null)
            {
                calcBalanceList = OMBalance
                        .Where(x => x.FspId == fspId && x.PeriodRegDate >= periodRegDate)
                        .SelectAll()
                        .OrderBy(x => x.PeriodRegDate)
                        .Execute();
            }

            //CIPJS-321 По текущему алгоритму, после учета начисления/зачисления на ФСП по итогу 
            //сформирована в INSUR_BALANCE строка с максимальным периодом PERIOD_REG_DATE = актуальный период.
            DateTime? actualPeriod = calcBalanceList.LastOrDefault()?.PeriodRegDate;

            foreach (OMBalance calcBalance in calcBalanceList)
            {
                if (!calcBalance.PeriodRegDate.HasValue)
                {
                    continue;
                }

                if (fsp.FspType_Code == FSPType.Polis && policySvd == null)
                {
                    //получаем самый актуальный полис на текущую дату
                    policySvd = GetPolicyByDate(fspId, policySvd: policySvd);
                }

                OMBalance previousPeriodBalance = calcBalanceList
                    .Where(x => x.PeriodRegDate < calcBalance.PeriodRegDate)
                    .OrderByDescending(x => x.PeriodRegDate)
                    .FirstOrDefault();

                OMBalance beforePreviousPeriodBalance = null;

                //для первого в списке периода получем (PERIOD-1) из базы
                if (previousPeriodBalance == null)
                {
                    previousPeriodBalance = OMBalance
                        .Where(x => x.FspId == fspId && x.PeriodRegDate < calcBalance.PeriodRegDate)
                        .OrderByDescending(x => x.PeriodRegDate)
                        .SelectAll()
                        .Execute()
                        .FirstOrDefault();
                }

                //получем (PERIOD-2)
                if (previousPeriodBalance != null && previousPeriodBalance.PeriodRegDate.HasValue)
                {
                    beforePreviousPeriodBalance = calcBalanceList
                        .Where(x => x.PeriodRegDate < previousPeriodBalance.PeriodRegDate.Value)
                        .OrderByDescending(x => x.PeriodRegDate)
                        .FirstOrDefault();

                    if (beforePreviousPeriodBalance == null)
                    {
                        beforePreviousPeriodBalance = OMBalance
                            .Where(x => x.FspId == fspId && x.PeriodRegDate < previousPeriodBalance.PeriodRegDate.Value)
                            .OrderByDescending(x => x.PeriodRegDate)
                            .SelectAll()
                            .Execute()
                            .FirstOrDefault();
                    }
                }

                //3.1.)INSUR_BALANCE.SUM_OPL(PERIOD)) = INSUR_BALANCE.SUM_OPL(PERIOD-1) + FspService.GetSumZachByPeriod(PERIOD)
                decimal? sumZachPeriod = GetSumZachByPeriod(fspId, calcBalance.PeriodRegDate.Value);
                decimal? previousPeriodSumOpl = previousPeriodBalance?.SumOpl;
                calcBalance.SumOpl = previousPeriodSumOpl.HasValue && sumZachPeriod.HasValue ?
                    previousPeriodSumOpl + sumZachPeriod :
                    previousPeriodSumOpl.HasValue ? previousPeriodSumOpl :
                    sumZachPeriod.HasValue ? sumZachPeriod :
                    0m;

                //3.2)INSUR_BALANCE.SUM_NACH_MFC(PERIOD) = INSUR_BALANCE.SUM_NACH_MFC(PERIOD-1) + FspService.GetSumNachByPeriod(PERIOD)
                decimal? sumNachMfcPeriod = GetSumNachByPeriod(fspId, calcBalance.PeriodRegDate.Value, InsuranceSourceType.Mfc);
                decimal? previousPeriodSumNachMfc = previousPeriodBalance?.SumNachMfc;
                calcBalance.SumNachMfc = previousPeriodSumNachMfc.HasValue && sumNachMfcPeriod.HasValue ?
                    previousPeriodSumNachMfc + sumNachMfcPeriod :
                    previousPeriodSumNachMfc.HasValue ? previousPeriodSumNachMfc :
                    sumNachMfcPeriod.HasValue ? sumNachMfcPeriod :
                    0m;

                //3.3) INSUR_BALANCE.SUM_NACH_GBY(PERIOD) = INSUR_BALANCE.SUM_NACH_GBY(PERIOD-1) + rate
                decimal? previousPeriodSumNachGbu = previousPeriodBalance?.SumNachGby;
                //Рассчитываем актуальное значение ставки на актуальный период
                //Ставка = INSUR_FSP.OPL_KODPL * Размер страхового тарифа, действующего на дату, попадающую в АКТУАЛЬНЫЙ ПЕРИОД
                //Если есть, берем контрольное начисление ГБУ SUM_NACH, если нет, то рассчитываем
                decimal rate;
                //Рассчитываем актуальное значение ставки на актуальный период
                //Ставка = INSUR_FSP.OPL_KODPL * Размер страхового тарифа, действующего на дату, попадающую в АКТУАЛЬНЫЙ ПЕРИОД
                decimal tariff = GetTariff(calcBalance.PeriodRegDate.Value);
                decimal oplKodpl = 0m;
                if (renewStrahEnd)
                {
                    DbCommand command = DBMngr.Realty.GetSqlStringCommand($"SELECT opl_kodpl FROM insur_fsp_q WHERE emp_id = {fspId} AND s_ <= '{calcBalance.PeriodRegDate.Value.Date}' AND '{calcBalance.PeriodRegDate.Value.Date}' <= po_");
                    var o_oplKodpl = DBMngr.Realty.ExecuteScalar(command);
                    oplKodpl = o_oplKodpl != null ? (decimal)o_oplKodpl : 0m;
                    //oplKodpl = OMFsp.Where(x => x.EmpId == fspId && x.SpecialS <= calcBalance.PeriodRegDate && calcBalance.PeriodRegDate <= x.SpecialPo)
                    //    .Select(x => x.OplKodpl)
                    //    .Join(OMFsp.GetRegisterId(), null, QSJoinType.Left, null)
                    //    .ExecuteFirstOrDefault()?.OplKodpl ?? 0m;
                }
                else
                    oplKodpl = OMFsp.Where(x => x.EmpId == fspId).Select(x => x.OplKodpl).ExecuteFirstOrDefault()?.OplKodpl ?? 0m;

                rate = Math.Round(tariff * oplKodpl, 2, MidpointRounding.AwayFromZero);
                decimal? sumNachGbuPeriod = rate;

                //calcBalance.SumNachGby = previousPeriodBalance?.SumNachGby + rate;

                calcBalance.SumNachGby = previousPeriodSumNachGbu.HasValue && sumNachGbuPeriod.HasValue ?
                    previousPeriodSumNachGbu + sumNachGbuPeriod :
                    previousPeriodSumNachGbu.HasValue ? previousPeriodSumNachGbu :
                    sumNachGbuPeriod.HasValue ? sumNachGbuPeriod :
                    0m;
                //3.4) INSUR_BALANCE.SUM_NACH_OPL(PERIOD) = INSUR_BALANCE.SUM_NACH_OPL(PERIOD-1) + FspService.GetSumNachByPeriod(PERIOD)
                //При этом, если  для PERIOD  значение FLAG_OPL = 0, то FspService.GetSumNachByPeriod(PERIOD) = 0
                decimal? previousPeriodSumNachOpl = previousPeriodBalance?.SumNachOpl;
                if (calcBalance.FlagOpl.HasValue && calcBalance.FlagOpl.Value)
                {
                    calcBalance.SumNachOpl = previousPeriodSumNachOpl.HasValue && sumNachGbuPeriod.HasValue ?
                        previousPeriodSumNachOpl + sumNachGbuPeriod :
                        previousPeriodSumNachOpl.HasValue ? previousPeriodSumNachOpl :
                        sumNachGbuPeriod.HasValue ? sumNachGbuPeriod :
                        0m;
                }
                else
                {
                    calcBalance.SumNachOpl = previousPeriodSumNachOpl.HasValue ? previousPeriodSumNachOpl.Value : 0m;
                }

                //3.5) INSUR_BALANCE.OSTATOK_SUM(PERIOD) = INSUR_BALANCE.SUM_OPL(PERIOD-1) - INSUR_BALANCE.SUM_NACH_OPL(PERIOD-1)
                decimal previousPeriodSumOplValue = previousPeriodSumOpl ?? 0m;
                decimal previousPeriodSumNachOplValue = previousPeriodSumNachOpl ?? 0m;
                //decimal previousPeriodOstatokSumValue = previousPeriodBalance != null ? previousPeriodBalance.OstatokSum ?? 0m : 0m;
                //decimal beforePreviousPeriodOstatokSumValue = beforePreviousPeriodBalance != null ? (beforePreviousPeriodBalance.OstatokSum.HasValue ? beforePreviousPeriodBalance.OstatokSum.Value : 0m) : 0m;
                //calcBalance.OstatokSum = previousPeriodSumOplValue > previousPeriodSumNachOplValue ?
                //    previousPeriodSumOplValue - previousPeriodSumNachOplValue : 0m;
                //calcBalance.OstatokSum = previousPeriodSumOplValue - previousPeriodSumNachOplValue >= 0 
                //    ? previousPeriodOstatokSumValue + (previousPeriodSumOplValue - previousPeriodSumNachOplValue) 
                //    : previousPeriodOstatokSumValue + (previousPeriodOstatokSumValue - beforePreviousPeriodOstatokSumValue);

                decimal ostatokSum1 = previousPeriodBalance != null ? (previousPeriodBalance.OstatokSum != null ? previousPeriodBalance.OstatokSum.Value : 0m) : 0m;
                decimal ostatokSum2 = beforePreviousPeriodBalance != null ? (beforePreviousPeriodBalance.OstatokSum != null ? beforePreviousPeriodBalance.OstatokSum.Value : 0m) : 0m;
                decimal sumOpl1 = previousPeriodBalance != null ? (previousPeriodBalance.SumOpl != null ? previousPeriodBalance.SumOpl.Value : 0m) : 0m;
                decimal sumOpl2 = beforePreviousPeriodBalance != null ? (beforePreviousPeriodBalance.SumOpl != null ? beforePreviousPeriodBalance.SumOpl.Value : 0m) : 0m;
                decimal sumNachGbu1 = previousPeriodBalance != null ? (previousPeriodBalance.SumNachGby != null ? previousPeriodBalance.SumNachGby.Value : 0m) : 0m;
                decimal sumNachGbu2 = beforePreviousPeriodBalance != null ? (beforePreviousPeriodBalance.SumNachGby != null ? beforePreviousPeriodBalance.SumNachGby.Value : 0m) : 0m;

                decimal deltaDelt = (sumOpl1 - sumOpl2) - (sumNachGbu1 - sumNachGbu2);

                if (ostatokSum1/*(ostatokSum1 - ostatokSum2)*/ + deltaDelt < 0)
                    deltaDelt = 0;

                calcBalance.OstatokSum = ostatokSum1 + /*(ostatokSum1 - ostatokSum2)*/ + deltaDelt;

                //3.6) Определяется значение FLAG_OPL ( PERIOD)
                decimal previousPeriodSumNachGbuValue = previousPeriodSumNachGbu ?? 0m;
                //CIPJS-276 если контрольное начисление в этом периоде = 0 то Флаг начисление оплачено = НЕТ, не с чем сравнить
                decimal currentPeriodSumNachGby = calcBalance.SumNachGby.Value - previousPeriodSumNachGbuValue;
                //CIPJS-630 если фсп типа полис, то сразу проставляем период оплачен
                calcBalance.FlagOpl = (fsp.FspType_Code == FSPType.Polis && policySvd != null) || (currentPeriodSumNachGby != 0 && (calcBalance.SumOpl.Value - previousPeriodSumOplValue + calcBalance.OstatokSum.Value)
                    >= currentPeriodSumNachGby);

                //3.6) Вызов процедуры FspService.GetInsureStatusByPeriod для PERIOD
                //не вызываем для периода, т.к. уже есть предыдущие сущности баланса для расчетов
                //CIPJS-630 если фсп типа полис, то сразу проставляем период застрахован
                calcBalance.FlagInsur = (fsp.FspType_Code == FSPType.Polis && policySvd != null) ||
                    (previousPeriodBalance != null && previousPeriodBalance.FlagOpl.HasValue && previousPeriodBalance.FlagOpl.Value);
                //GetInsureStatus(previousPeriodBalance, beforePreviousPeriodBalance);

                //CIPJS-321 Найти строку с  INSUR_BALANCE.PERIOD_REG_DATE = Актуальный период для заданного ФСП
                if ((actualPeriod.HasValue
                    && calcBalance.PeriodRegDate.Value == actualPeriod.Value) || renewStrahEnd == true)
                {
                    DateTime nextPeriod = calcBalance.PeriodRegDate.Value.AddMonths(1);

                    //CIPJS-877 : некорректно отображается strah_end в insur_balance (замена процедуры расчета)
                    //Не создавая новую строку в INSUR_BALANCE
                    //рассчитываем по стандартному алгоритму значение Остаток на след период
                    //INSUR_BALANCE.OSTATOK_SUM для PERIOD_REG_DATE= Актуальный период+1
                    decimal nextPeriodOstatokSum = 0;
                    decimal sumOplDelta = 0;
                    decimal sumNachGbyDelta = 0;
                    {
                        decimal sumOplActual = calcBalance.SumOpl.HasValue ? calcBalance.SumOpl.Value : 0m;
                        decimal sumOplPrev = previousPeriodBalance != null ? (previousPeriodBalance.SumOpl.HasValue ? previousPeriodBalance.SumOpl.Value : 0m) : 0m;
                        decimal ostatokSumActual = calcBalance.OstatokSum.HasValue ? calcBalance.OstatokSum.Value : 0m;
                        decimal sumNachGbyActual = calcBalance.SumNachGby.HasValue ? calcBalance.SumNachGby.Value : 0m;
                        decimal sumNachGbyPrev = previousPeriodBalance != null ? (previousPeriodBalance.SumNachGby.HasValue ? previousPeriodBalance.SumNachGby.Value : 0m) : 0m;

                        sumOplDelta = sumOplActual - sumOplPrev;
                        sumNachGbyDelta = sumNachGbyActual - sumNachGbyPrev;
                        nextPeriodOstatokSum = ostatokSumActual + sumOplDelta - sumNachGbyDelta;
                    }

                    if (nextPeriodOstatokSum >= rate)
                    {
                        //Рассчитываем кол- во периодов
                        //N = Остаток на след период / Ставка , полученное значение обрезаем до целого
                        if (oplKodpl == 0 && sumNachGbyDelta == 0)
                            calcBalance.StrahEnd = null;
                        else
                        {
                            //strah_end = actualPeriod + n + 1
                            int months = rate > 0 ? Convert.ToInt32(Math.Floor(nextPeriodOstatokSum / rate)) : 0;
                            calcBalance.StrahEnd = calcBalance.PeriodRegDate.Value.AddMonths(months + 1);
                        }
                    }
                    else if (nextPeriodOstatokSum < rate && (previousPeriodBalance != null ? previousPeriodBalance.FlagOpl.Value : false) == true)
                    {
                        //strah_end = actualPeriod
                        calcBalance.StrahEnd = calcBalance.PeriodRegDate.Value;
                    }
                    else if (nextPeriodOstatokSum < rate && (previousPeriodBalance != null ? previousPeriodBalance.FlagOpl.Value : false) == false)
                    {
                        //strah_end = null
                        calcBalance.StrahEnd = null;
                    }
                }

                calcBalance.Save();
            }
        }

		/// <summary>
		/// Определение статуса страхования периода
		/// </summary>
		/// <param name="currentBalance">Предыдущая запись баланса</param>
		/// <param name="previousPeriodBalance">Пред-предыдущая запись баланса</param>
		/// <returns>Статус страхования периода = 1/0 (Застраховано/Не застраховано)</returns>
		private bool GetInsureStatus(OMBalance previousPeriodBalance, OMBalance beforePreviousPeriodBalance)
		{
			//INSUR_BALANCE.SUM_OPL(PERIOD-1)
			decimal sumOplPreviousPeriod = previousPeriodBalance != null && previousPeriodBalance.SumOpl.HasValue ? previousPeriodBalance.SumOpl.Value : 0m;
			//INSUR_BALANCE.SUM_NACH_GBY(PERIOD-1)
			decimal sumGbuPreviousPeriod = previousPeriodBalance != null && previousPeriodBalance.SumNachGby.HasValue ? previousPeriodBalance.SumNachGby.Value : 0m;
			//INSUR_BALANCE.OSTATOK_SUM(PERIOD-1)
			decimal ostatokSumPreviousPeriod = previousPeriodBalance != null && previousPeriodBalance.OstatokSum.HasValue ? previousPeriodBalance.OstatokSum.Value : 0m;

			//INSUR_BALANCE.SUM_OPL(PERIOD-2)
			decimal sumOplBeforePreviousPeriod = beforePreviousPeriodBalance != null && beforePreviousPeriodBalance.SumOpl.HasValue ? beforePreviousPeriodBalance.SumOpl.Value : 0m;
			//INSUR_BALANCE.SUM_NACH_GBY(PERIOD-2)
			decimal sumGbuBeforePreviousPeriod = beforePreviousPeriodBalance != null && beforePreviousPeriodBalance.SumNachGby.HasValue ? beforePreviousPeriodBalance.SumNachGby.Value : 0m;

			//1) SUM_ZACH(PERIOD-1) = INSUR_BALANCE.SUM_OPL(PERIOD-1) - INSUR_BALANCE.SUM_OPL(PERIOD-2)
			decimal sumZach = sumOplPreviousPeriod - sumOplBeforePreviousPeriod;
			//2) SUM_NACH(PERIOD-1) = INSUR_BALANCE.SUM_NACH_GBY(PERIOD-1) - INSUR_BALANCE.SUM_NACH_ GBY (PERIOD-2)
			decimal sumNach = sumGbuPreviousPeriod - sumGbuBeforePreviousPeriod;

			//3)INSUR_BALANCE.OSTATOK_SUM(PERIOD - 1) + SUM_ZACH(PERIOD - 1) > = SUM_NACH(PERIOD - 1)
			return (ostatokSumPreviousPeriod + sumZach) >= sumNach;
		}

		/// <summary>
		/// Связывает начисления с ФСП и выполняет перерасчет баланса
		/// </summary>
		/// <param name="fspId">Идентификат ФСП</param>
		/// <param name="inputNachList">Список начислений</param>
		public void BindAndAccount(long fspId, List<OMInputNach> inputNachList)
		{
			if (inputNachList == null || inputNachList.Count == 0)
			{
				return;
			}

			if (inputNachList.Any(x => x.FspId != null))
			{
				throw new Exception("Невозможно выполнить учет начисления на ФСП, т.к. среди выбранных записей есть уже связанные с другим ФСП");
			}
			else
			{
				List<DateTime> periods = new List<DateTime>();

				using (TransactionScope ts = TransactionScopeWrapper.OpenTransaction())
				{
					foreach (OMInputNach inputNach in inputNachList)
					{
						if (inputNach.PeriodRegDate.HasValue)
						{
							if (!periods.Contains(inputNach.PeriodRegDate.Value))
							{
								periods.Add(inputNach.PeriodRegDate.Value);
							}
						}

						inputNach.FspId = fspId;
						inputNach.StatusIdentif_Code = StatusIdentifikacii.Identified;
						inputNach.Save();
					}

					foreach (DateTime period in periods)
					{
						//CIPJS-69 Проверяем, если  в PERIOD  ( в который мы должны учесть начисление/зачисление) «Контрольное начисление ГБУ»
						//Если такая запись отсутствует, то  запускается процедура  «Создание контрольного начисления»
						OMInputNach strahNach = _strahNachService.GetByFspIdPeriodFromGbuCache(fspId, period);
						if (strahNach == null)
						{
							_strahNachService.CreateByFspIdPeriodInGbuCache(fspId, period);
						}

						AccountFsp(fspId, period);
					}

					//6)Запуск процедуры «Перестроение остатков  и оборотов» 
					//для самого СТАРОГО периода по PERIOD_REG_DATE для всех обрабатываемых операций по начислению и зачислению
					CalcBalanceSumFromPeriod(fspId, periods.Min());

					ts.Complete();
				}
			}
		}

		/// <summary>
		/// Связывает зачисления с ФСП и выполняет перерасчет баланса
		/// </summary>
		/// <param name="fspId">Идентификат ФСП</param>
		/// <param name="inputPlatList">Список зачислений</param>
		public void BindAndAccount(long fspId, List<OMInputPlat> inputPlatList)
		{
			if (inputPlatList == null || inputPlatList.Count == 0)
			{
				return;
			}

			if (inputPlatList.Any(x => x.FspId != null))
			{
				throw new Exception("Невозможно выполнить учет зачислений на ФСП, т.к. среди выбранных записей есть уже связанные с другим ФСП");
			}
			else
			{
				List<DateTime> periods = new List<DateTime>();

				using (TransactionScope ts = TransactionScopeWrapper.OpenTransaction())
				{
					foreach (OMInputPlat inputPlat in inputPlatList)
					{
						if (inputPlat.PeriodRegDate.HasValue)
						{
							if (!periods.Contains(inputPlat.PeriodRegDate.Value))
							{
								periods.Add(inputPlat.PeriodRegDate.Value);
							}
						}

						inputPlat.FspId = fspId;
						inputPlat.StatusIdentif_Code = StatusIdentifikacii.Identified;
						inputPlat.Save();
					}

					foreach (DateTime period in periods)
					{
						//CIPJS-69 Проверяем, если  в PERIOD  ( в который мы должны учесть начисление/зачисление) «Контрольное начисление ГБУ»
						//Если такая запись отсутствует, то  запускается процедура  «Создание контрольного начисления»
						OMInputNach strahNach = _strahNachService.GetByFspIdPeriodFromGbuCache(fspId, period);
						if (strahNach == null)
						{
							_strahNachService.CreateByFspIdPeriodInGbuCache(fspId, period);
						}

						AccountFsp(fspId, period);
					}

					//6)Запуск процедуры «Перестроение остатков  и оборотов» 
					//для самого СТАРОГО периода по PERIOD_REG_DATE для всех обрабатываемых операций по начислению и зачислению
					CalcBalanceSumFromPeriod(fspId, periods.Min());

					ts.Complete();
				}
			}
		}

		/// <summary>
		/// Отвязывает начисления от ФСП и проводи перерасчет баланса
		/// </summary>
		/// <param name="inputNachList">Список начислений</param>
		public void UnbindAndAccount(List<OMInputNach> inputNachList)
		{
			if (inputNachList == null || inputNachList.Count == 0)
			{
				return;
			}

			using (TransactionScope ts = TransactionScopeWrapper.OpenTransaction())
			{
				List<Tuple<long, DateTime?>> fspIdPeriodList = new List<Tuple<long, DateTime?>>();

				foreach (OMInputNach inputNach in inputNachList)
				{
					if (!inputNach.FspId.HasValue)
					{
						continue;
					}

					if (!fspIdPeriodList.Any(x => x.Item1 == inputNach.FspId && x.Item2 == inputNach.PeriodRegDate))
					{
						fspIdPeriodList.Add(new Tuple<long, DateTime?>(inputNach.FspId.Value, inputNach.PeriodRegDate));
					}

					inputNach.FspId = null;
					inputNach.StatusIdentif_Code = StatusIdentifikacii.NotIdentified;
					inputNach.Save();
				}

				foreach (Tuple<long, DateTime?> fspIdPeriod in fspIdPeriodList)
				{
					if (!fspIdPeriod.Item2.HasValue)
					{
						continue;
					}

					AccountFsp(fspIdPeriod.Item1, fspIdPeriod.Item2.Value);

					//6)Запуск процедуры «Перестроение остатков  и оборотов» 
					//для самого СТАРОГО периода по PERIOD_REG_DATE для всех обрабатываемых операций по начислению и зачислению
					CalcBalanceSumFromPeriod(fspIdPeriod.Item1, fspIdPeriod.Item2.Value);
				}

				ts.Complete();
			}
		}

		/// <summary>
		/// Отвязывает начисления от ФСП и проводи перерасчет баланса
		/// </summary>
		/// <param name="inputNachList">Список начислений</param>
		public void UnbindAndAccount(List<OMInputPlat> inputPlatList)
		{
			if (inputPlatList == null || inputPlatList.Count == 0)
			{
				return;
			}

			using (TransactionScope ts = TransactionScopeWrapper.OpenTransaction())
			{
				List<Tuple<long, DateTime?>> fspIdPeriodList = new List<Tuple<long, DateTime?>>();

				foreach (OMInputPlat inputPlat in inputPlatList)
				{
					if (!inputPlat.FspId.HasValue)
					{
						continue;
					}

					if (!fspIdPeriodList.Any(x => x.Item1 == inputPlat.FspId && x.Item2 == inputPlat.PeriodRegDate))
					{
						fspIdPeriodList.Add(new Tuple<long, DateTime?>(inputPlat.FspId.Value, inputPlat.PeriodRegDate));
					}

					inputPlat.FspId = null;
					inputPlat.StatusIdentif_Code = inputPlat.LinkBankId.HasValue ?
						StatusIdentifikacii.PartiallyIdentified : StatusIdentifikacii.NotIdentified;
					inputPlat.Save();
				}

				foreach (Tuple<long, DateTime?> fspIdPeriod in fspIdPeriodList.OrderBy(x => x.Item2))
				{
					if (!fspIdPeriod.Item2.HasValue)
					{
						continue;
					}

					AccountFsp(fspIdPeriod.Item1, fspIdPeriod.Item2.Value);

					//6)Запуск процедуры «Перестроение остатков  и оборотов» 
					//для самого СТАРОГО периода по PERIOD_REG_DATE для всех обрабатываемых операций по начислению и зачислению
					CalcBalanceSumFromPeriod(fspIdPeriod.Item1, fspIdPeriod.Item2.Value);
				}

				ts.Complete();
			}
		}

		/// <summary>
		/// Создает список контрольных начислений для всех периодов между датами С и По
		/// </summary>
		/// <param name="fspId">Идентификатор ФСП</param>
		/// <param name="dateS">Дата С</param>
		/// <param name="datePo">Дата По</param>
		/// <param name="inputNachIds">Идентификаторы начислений</param>
		/// <returns>Список контрольных начислений</returns>
		public List<OMInputNach> CreateStrahNachByPeriods(long fspId, DateTime dateS, DateTime datePo)
		{
			List<OMInputNach> result = new List<OMInputNach>();

			if (fspId == 0)
			{
				throw new Exception("Передан неверный параметр идентификатор ФСП");
			}

			if (dateS == DateTime.MinValue)
			{
				throw new Exception("Передан неверный параметр дата С");
			}

			if (datePo == DateTime.MinValue)
			{
				throw new Exception("Передан неверный параметр дата По");
			}

			if (dateS > datePo)
			{
				throw new Exception("Дата С не может быть больше даты По");
			}

			List<DateTime> periods = new List<DateTime>();
			DateTime currentPeriod = new DateTime(dateS.Year, dateS.Month, 1);
			DateTime lastPeriod = new DateTime(datePo.Year, datePo.Month, 1);

			while (true)
			{
				periods.Add(currentPeriod);

				if (currentPeriod == lastPeriod)
				{
					break;
				}

				currentPeriod = currentPeriod.AddMonths(1);
			}

			using (TransactionScope ts = TransactionScopeWrapper.OpenTransaction())
			{
				foreach (DateTime period in periods)
				{
					OMInputNach strahNach = _strahNachService.GetByFspIdPeriodFromGbuCache(fspId, period);

					if (strahNach == null)
					{
						strahNach = _strahNachService.CreateByFspIdPeriodInGbuCache(fspId, period);
					}

					AccountFsp(fspId, period);

					result.Add(strahNach);
				}

				CalcBalanceSumFromPeriod(fspId, periods.Min());

				ts.Complete();
			}

			return result;
		}

		/// <summary>
		/// Сохранение связи фсп и помещения
		/// </summary>
		/// <param name="model"></param>
		public void LinkToFlat(SingleFspFlatLinkDto model)
		{
			OMFsp fsp = OMFsp
				.Where(x => x.EmpId == model.FspId)
				.ExecuteFirstOrDefault();
			if (fsp == null) return;

			bool manyObj = model.Flats.Count > 1;
			fsp.ObjId = manyObj ? fsp.EmpId : model.Flats[0];
			fsp.ObjReestrId = manyObj ? OMLinkFspFlat.GetRegisterId() : OMFlat.GetRegisterId();
			fsp.FlagManyObj = manyObj;
			fsp.NumObj = model.Flats.Count;

			if (manyObj)
				foreach (long flatId in model.Flats)
				{
					OMLinkFspFlat linkFspFlat = OMLinkFspFlat
						.Where(x => x.FspId == fsp.EmpId && x.ObjId == flatId)
						.ExecuteFirstOrDefault();
					if (linkFspFlat == null)
					{
						new OMLinkFspFlat
						{
							FspId = fsp.EmpId,
							ObjId = flatId
						}.Save();
					}
				}
			fsp.Save();
		}

		public string LinkToFlatConfirmMessage(SingleFspFlatLinkDto model)
		{
			var flats = OMFlat
				.Where(x => model.Flats.Contains(x.EmpId))
				.Select(x => x.Kvnom)
				.Select(x => x.Unom)
				.Execute();
			var kvnoms = string.Join(", ", flats.Select(x => x.Kvnom));
			return $"Внимание! Вы подтверждаете что ФСП будет связано с {model.Flats.Count} помещениями, под номерами {kvnoms} в МКД с UNOM {flats.First().Unom}?";
		}

		public OMBalance GetBalance(long fspId, DateTime periodRegDate)
		{
			return OMBalance
				.Where(x => x.FspId == fspId && x.PeriodRegDate == periodRegDate)
				.Select(x => x.OstatokSum)
				.ExecuteFirstOrDefault();
		}

		public OMBalance CreateBalance(long fspId, DateTime periodRegDate)
		{
			OMBalance balance = new OMBalance
			{
				FspId = fspId,
				PeriodRegDate = periodRegDate,
				FlagOpl = false,
				OstatokSum = CalcUndestributedBalanceOnPeriodBegin(fspId, periodRegDate)
			};
			balance.Save();

			return balance;
		}

		public decimal GetTariff(DateTime periodRegDate)
		{
			//TODO возможно требуется закешировать таблицу insur_tariff для FspService
			OMTariff tariff = OMTariff.Where(x => x.DateBegin <= periodRegDate)
				.OrderByDescending(x => x.DateBegin)
				.SetPackageIndex(0)
				.SetPackageSize(1)
				.SelectAll()
				.Execute()
				.FirstOrDefault();

			if (tariff == null)
			{
				throw new Exception($"Не удалось определить тариф для периода {periodRegDate:dd.MM.yyyy}");
			}

			if (!tariff.Value.HasValue)
			{
				throw new Exception($"Для периода {periodRegDate:dd.MM.yyyy} у тарифа не заполнено значение тарифа");
			}

			return tariff.Value.Value;
		}

		public decimal GetOplKodpl(long? districtId, DateTime? periodRegDate, string kodPl, decimal? opl, decimal? sum, long? flatTypeId, decimal? fopl)
		{
			if (opl.HasValue && opl.Value != 0)
			{
				return opl.Value;
			}

			if (!CommunalFlatId.HasValue || flatTypeId != CommunalFlatId.Value)
			{
				if (fopl.HasValue && fopl.Value != 0)
				{
					return fopl.Value;
				}

				if (kodPl.IsNotEmpty() && periodRegDate.HasValue && districtId.HasValue)
				{
					OMInputNach inputNach = OMInputNach
						.Where(x => x.DistrictId == districtId.Value
						&& x.PeriodRegDate == periodRegDate.Value
						&& x.TypeSource_Code == InsuranceSourceType.Mfc
						&& x.Kodpl == kodPl
						&& x.Fopl != null
						&& x.Fopl != 0)
						.Select(x => x.Fopl)
						.ExecuteFirstOrDefault();

					if (inputNach != null && inputNach.Fopl.HasValue)
					{
						return inputNach.Fopl.Value;
					}
				}

				return GetOplKodplByTariff(sum, periodRegDate);
			}
			else if (CommunalFlatId.HasValue && flatTypeId == CommunalFlatId.Value)
			{
				return GetOplKodplByTariff(sum, periodRegDate);
			}

			return 0m;
		}

		public string GetContractNumber(long fspId, DateTime? date = null)
		{
			OMFsp fsp = OMFsp.Where(x => x.EmpId == fspId)
					.Select(x => x.Kodpl)
					.Select(x => x.FspType)
					.Select(x => x.FspType_Code)
					.ExecuteFirstOrDefault();

			if (fsp != null)
			{
				OMPolicySvd policy = GetPolicyByDate(fspId, date);

				if (policy != null && fsp.FspType_Code == FSPType.EPD)
				{
					return $"{fsp.Kodpl}{(policy.Npol.IsNotEmpty() ? $" / {policy.Npol}" : string.Empty)}";
				}
				else if (policy != null && (fsp.FspType_Code == FSPType.Polis || fsp.FspType_Code == FSPType.Svidetelstvo))
				{
					return $"{(fsp.Kodpl.IsNotEmpty() ? fsp.Kodpl : string.Empty)}{(fsp.Kodpl.IsNotEmpty() && policy.Npol.IsNotEmpty() ? " / " : string.Empty)}{(policy.Npol.IsNotEmpty() ? policy.Npol : string.Empty)}";
				}
				else
				{
					return fsp.Kodpl;
				}
			}

			return null;
		}

		public OMPolicySvd GetPolicyByDate(long fspId, DateTime? date = null, OMPolicySvd policySvd = null)
		{
			if (policySvd != null && (!date.HasValue ||
				policySvd.Dat.HasValue && policySvd.Dat.Value <= date.Value && (!policySvd.DatEnd.HasValue || policySvd.DatEnd.Value > date.Value)))
			{
				return policySvd;
			}

			List<OMPolicySvd> policyList = OMPolicySvd.Where(x => x.FspId == fspId)
				.SelectAll()
				.OrderByDescending(x => x.Dat)
				.Execute();

			if (date.HasValue)
			{
				return policyList.FirstOrDefault(x => x.Dat.HasValue
					&& x.Dat.Value <= date.Value
					&& x.Dat.Value.AddYears(1) >= date.Value);
			}
			else
			{
				return policyList.FirstOrDefault();
			}
		}

		private decimal GetOplKodplByTariff(decimal? sum, DateTime? periodRegDate)
		{
			decimal tariff = 0;
			try
			{
				tariff = GetTariff(periodRegDate ?? DateTime.Now);
			}
			catch (Exception ex)
			{
				ErrorManager.LogError(ex);
			}

			return tariff != 0 ? (sum ?? 0) / tariff : 0;
		}

		/// <summary>
		/// Возвращает строку с UNOM и номером квартиры для помещения, связанного с ФСП, если оно есть
		/// </summary>
		/// <param name="fspId">Идентификатор ФСП</param>
		/// <param name="description">Описание связи</param>
		public bool HasLinkToFlat(long fspId, out string description)
		{
			OMFsp omFsp = OMFsp
				.Where(x => x.EmpId == fspId)
				.Select(x => x.ParentFlat.Unom)
				.Select(x => x.ParentFlat.Kvnom)
				.ExecuteFirstOrDefault();
			if (omFsp.ParentFlat != null)
			{
				description = $"UNOM: {omFsp.ParentFlat.Unom}, номер квартиры: {omFsp.ParentFlat.Kvnom}";
				return true;
			}
			else
			{
				description = null;
				return false;
			}
		}

		public void UnlinkFlat(IEnumerable<long> fspIds)
		{
			using (var ts = TransactionScopeWrapper.OpenTransaction())
			{
				foreach (var id in fspIds)
				{
					var links = OMLinkFspFlat.Where(x => x.FspId == id).Execute();
					foreach (var link in links)
						link.Destroy();
					new OMFsp
					{
						EmpId = id,
						FlagManyObj = false,
						NumObj = 0,
						ObjReestrId = null,
						ObjId = null
					}.Save();
				}
				ts.Complete();
			}
		}

		public bool HasLinkToFlat(IEnumerable<long> fspIds)
		{
			return OMFsp
				.Where(x => fspIds.Contains(x.EmpId) && x.ObjId != null)
				.ExecuteExists();
		}

		public bool CanMassLinkToFlat(IEnumerable<long> fspIds, out string reason)
		{
			var data = new QSQuery(OMFsp.GetRegisterId())
			{
				Columns = new List<QSColumn>
				{
					OMFsp.GetColumn(x => x.UnomFsp, "ID")
				},
				Condition = new QSConditionSimple(OMFsp.GetColumn(x => x.EmpId), QSConditionType.In, new QSColumnConstant(fspIds)),
				Distinct = true
			}.ExecuteQuery();
			if (data.Rows.Count == 0)
			{
				reason = "Не найдены ФСП";
				return false;
			}
			else if (data.Rows.Count > 1)
			{
				var unoms = string.Join(
					", ",
					data.AsEnumerable().Select(x => x[0] == DBNull.Value ? "NULL" : x[0].ToString())
				);
				reason = "Внимание! Массовая связка ФСП с объектами возможно только для ФСП с одинаковым UNOM " +
						 "по данным МФЦ, среди выбранных записей встречаются следующие значения " + unoms +
						 ". Выберите ФСП с одинаковыми значениями UNOM по данным МФЦ и повторите операцию";
				return false;
			}
			else if (data.Rows[0][0] == DBNull.Value)
			{
				reason = "Внимание! У выбранных ФСП не заполнен UNOM МФЦ";
				return false;
			}
			else
			{
				reason = null;
				return true;
			}
		}

		public int MassLinkToFlat(ManyFspFlatLinkDto dto)
		{
			long oldUnom = GetFspForLink(dto.FspIds[0]).UnomFsp.GetValueOrDefault();
			bool unomChanged = oldUnom != dto.Unom;
			int flatReg = OMFlat.GetRegisterId();
			int recordsChanged = 0;
			var buildingSvc = new BuildingService();
			var flatSvc = new FlatService();
			using (var ts = TransactionScopeWrapper.OpenTransaction())
			{
				if (unomChanged)
				{
					new OMUnomUpdate
					{
						OldUnom = oldUnom,
						NewUnom = dto.Unom,
						OldLinkMkd = buildingSvc.GetBuildingIdByUnom(oldUnom).GetValueOrDefault(-1),
						NewLinkMkd = buildingSvc.GetBuildingIdByUnom(dto.Unom).GetValueOrDefault(-1),
						UserChange = SRDSession.GetCurrentUserId().GetValueOrDefault(-1),
						Note = dto.Comment
					}.Save();
				}
				foreach (long fspId in dto.FspIds)
				{
					var fsp = GetFspForLink(fspId);
					var flatId = flatSvc.GetFlatIdByUnom(dto.Unom, fsp.KvnomFsp);
					if (!flatId.HasValue)
						continue;

					if (unomChanged)
					{
						fsp.FspNumber = fsp.FspNumber.Replace(oldUnom + "-", dto.Unom + "-");
						fsp.UnomFsp = dto.Unom;
					}
					fsp.ObjId = flatId;
					fsp.FlagManyObj = false;
					fsp.NumObj = 1;
					fsp.ObjReestrId = flatReg;
					fsp.Save();
					++recordsChanged;
				}
				ts.Complete();
			}
			return recordsChanged;
		}

		public OMInputNach GetInputNachForLink(long fspId)
		{
			return OMInputNach
				.Where(x => x.FspId == fspId && x.TypeSource_Code == InsuranceSourceType.Mfc)
				.Select(x => x.Unom)
				.Select(x => x.Kvnom)
				.OrderByDescending(x => x.PeriodRegDate)
				.SetPackageSize(1)
				.ExecuteFirstOrDefault();
		}

		public OMFsp GetFspForLink(long fspId)
		{
			return OMFsp
				.Where(x => x.EmpId == fspId)
				.Select(x => x.UnomFsp)
				.Select(x => x.KvnomFsp)
				.Select(x => x.FspNumber)
				.SetPackageSize(1)
				.ExecuteFirstOrDefault();
		}
	}
}
