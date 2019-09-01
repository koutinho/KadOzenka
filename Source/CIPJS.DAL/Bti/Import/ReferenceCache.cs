using CIPJS.DAL.Bti.Import.Mapping;
using Core.Shared.Extensions;
using Microsoft.Practices.EnterpriseLibrary.Data;
using ObjectModel.Core.Shared;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Linq;

namespace CIPJS.DAL.Bti.Import
{
    public class ReferenceCacheItem
    {
        #region Поля классификатора
        public long BtiId { get; set; }
        public long Nk { get; set; }
        public string Kod { get; set; }
        #endregion

        #region Поля OMReferenceItem
        public long ItemId { get; set; }
        public string Code { get; set; }
        public string Value { get; set; }
        #endregion
    }

    public class StreetCacheItem
    {
        public long? Id;
        public string Name;
    }

    public class DistrictCacheItem
    {
        public long? Id;
        public string Name;
    }

    public class OkrugCacheItem
    {
        public long? Id;
        public string Name;
    }

    public class ReferenceCache
    {
        private readonly Dictionary<string, ReferenceCacheItem> _referenceItemCacheKod;
        private readonly Dictionary<string, ReferenceCacheItem> _referenceItemCacheBtiId;

        /// <summary>
        /// Ключ - CODE_GIVC, Значение - Наименование
        /// </summary>
        private readonly Dictionary<string, StreetCacheItem> _streetCache;

        /// <summary>
        /// Ключ - STEKS_CODE
        /// </summary>
        private readonly Dictionary<string, DistrictCacheItem> _districtCache;

        /// <summary>
        /// Ключ - STEKS_CODE
        /// </summary>
        private readonly Dictionary<string, OkrugCacheItem> _okrugCache;

        private readonly DictionaryMapping _dictionaryMapping;
		
        public DictionarySqlMapping SqlDictionaryMapping { private set; get; }

        //основной метод инициализации кэша
        private void InitCache()
        {
            InitReferenceCache();
            Console.WriteLine("{0: dd.MM.yyyy HH:mm:ss}: Закончена загрузка справочников", DateTime.Now);

            InitStreetCache();
            Console.WriteLine("{0: dd.MM.yyyy HH:mm:ss}: Закончена инициализация кэша улиц", DateTime.Now);

            InitDistrictCache();
            Console.WriteLine("{0: dd.MM.yyyy HH:mm:ss}: Закончена инициализация кэша районов", DateTime.Now);

            InitOkrugCache();
            Console.WriteLine("{0: dd.MM.yyyy HH:mm:ss}: Закончена инициализация кэша округов", DateTime.Now);
        }

        private void InitReferenceCache()
        {
            int[] dictonaryMappingKeys = _dictionaryMapping.GetOuterIds().ToArray();

            //если xml c маппингом пустой - кэш не заполняем
            if (dictonaryMappingKeys.Any())
            {
                DbCommand klsCommand = CipjsDbManager.Dgi.GetSqlStringCommand(string.Format(@"select kls.ID, kls.NK, kls.KOD, kls.NM
from {0} kls where kls.NK IN ({1}) AND kls.KOD IS NOT NULL", Importer.KlsTableName, string.Join(",", dictonaryMappingKeys)));

                DataTable klsTable = CipjsDbManager.Dgi.ExecuteDataSet(klsCommand).Tables[0];

                foreach (DataRow row in klsTable.Rows)
                {
                    if (row == null)
                    {
                        throw new ArgumentNullException("row");
                    }

                    int nk = Convert.ToInt32(row["NK"]);
                    int? referenceid = _dictionaryMapping.GetValue(nk);

                    if (!referenceid.HasValue)
                    {
                        throw new Exception(string.Format("Не удалось найти сопоставление для классификатора {0} в конфигурации", nk));
                    }

                    if (row["KOD"] == DBNull.Value)
                    {
                        throw new Exception(string.Format("Не заполнено одно из значений KOD для классификатора {0}", nk));
                    }

                    if (row["ID"] == null || row["ID"] == DBNull.Value)
                    {
                        throw new Exception(string.Format("Не заполнено одно из значений ID для классификатора {0}", nk));
                    }

                    string kod = row["KOD"].ToString();
                    long btiId = row["ID"].ParseToLong();

                    OMReferenceItem oMReferenceItem = OMReferenceItem
                        .Where(x => x.ReferenceId == referenceid && x.Code == kod)
                        .Select(x => x.ReferenceId)
                        .Select(x => x.Code)
                        .Select(x => x.Value)
                        .Execute()
                        .FirstOrDefault();

                    if (oMReferenceItem == null)
                    {
                        oMReferenceItem = OMReferenceItem.CreateEmpty();
                        oMReferenceItem.ReferenceId = referenceid;
                        oMReferenceItem.Code = kod;
                        oMReferenceItem.Value = row["NM"].ToString();
                        oMReferenceItem.Username = "Импорт";
                        oMReferenceItem.DateS = DateTime.Now;
                        oMReferenceItem.Save();
                    }

                    ReferenceCacheItem referenceCacheItem = new ReferenceCacheItem
                    {
                        ItemId = oMReferenceItem.ItemId,
                        Code = oMReferenceItem.Code,
                        Value = oMReferenceItem.Value,
                        Nk = nk,
                        Kod = kod,
                        BtiId = btiId
                    };
                    if (!_referenceItemCacheKod.ContainsKey(nk + "_" + kod))
                    {
                        _referenceItemCacheKod.Add(nk + "_" + kod, referenceCacheItem);
                    }
                    if (!_referenceItemCacheBtiId.ContainsKey(nk + "_" + btiId))
                    {
                        _referenceItemCacheBtiId.Add(nk + "_" + btiId, referenceCacheItem);
                    }
                }
            }
        }

        private void InitStreetCache()
        {
            DbCommand refAddrStreetCommand = DBMngr.Realty.GetSqlStringCommand(string.Format(@"select ras.CODE_GIVC, ras.FULL_NAME, ras.STREET_ID from {0} ras", Importer.RefAddrStreetTableName));

            DataTable refAddrStreetTable = DBMngr.Realty.ExecuteDataSet(refAddrStreetCommand).Tables[0];

            foreach (DataRow row in refAddrStreetTable.Rows)
            {
                if (row["CODE_GIVC"] == null || row["CODE_GIVC"] == DBNull.Value)
                    continue;

                string codeGivc = Convert.ToString(row["CODE_GIVC"]);
                string fullName = Convert.ToString(row["FULL_NAME"]);
                long? streetId = row["STREET_ID"] != DBNull.Value ? (long?)row["STREET_ID"].ParseToLong() : null;

                if (!_streetCache.ContainsKey(codeGivc))
                {
                    _streetCache.Add(codeGivc, new StreetCacheItem
                    {
                        Id = streetId,
                        Name = fullName
                    });
                }
            }
        }

        private void InitDistrictCache()
        {
            DbCommand refAddrDistrictCommand = DBMngr.Realty.GetSqlStringCommand(string.Format(@"select rad.steks_code, rad.full_name, rad.district_id from {0} rad", Importer.RefAddrDistrictTableName));

            DataTable refAddrDistrictTable = DBMngr.Realty.ExecuteDataSet(refAddrDistrictCommand).Tables[0];

            foreach (DataRow row in refAddrDistrictTable.Rows)
            {
                if (row["steks_code"] == null || row["steks_code"] == DBNull.Value)
                    continue;

                string steksCode = Convert.ToString(row["steks_code"]);
                string fullName = Convert.ToString(row["full_name"]);
                long? districtId = row["district_id"] != DBNull.Value ? (long?)row["district_id"].ParseToLong() : null;

                if (!_districtCache.ContainsKey(steksCode))
                {
                    _districtCache.Add(steksCode, new DistrictCacheItem
                    {
                        Id = districtId,
                        Name = fullName
                    });
                }
            }
        }

        private void InitOkrugCache()
        {
            DbCommand refAddrOkrugCommand = DBMngr.Realty.GetSqlStringCommand(string.Format(@"select rao.steks_code, rao.full_name, rao.okrug_id from {0} rao", Importer.RefAddrOkrugTableName));

            DataTable refAddrOkrugTable = DBMngr.Realty.ExecuteDataSet(refAddrOkrugCommand).Tables[0];

            foreach (DataRow row in refAddrOkrugTable.Rows)
            {
                if (row["steks_code"] == null || row["steks_code"] == DBNull.Value)
                    continue;

                string steksCode = Convert.ToString(row["steks_code"]);
                string fullName = Convert.ToString(row["full_name"]);
                long? okrugId = row["okrug_id"] != DBNull.Value ? (long?)row["okrug_id"].ParseToLong() : null;

                if (!_okrugCache.ContainsKey(steksCode))
                {
                    _okrugCache.Add(steksCode, new OkrugCacheItem
                    {
                        Id = okrugId,
                        Name = fullName
                    });
                }
            }
        }

        public ReferenceCache()
        {
            _dictionaryMapping = new DictionaryMapping();
            _referenceItemCacheKod = new Dictionary<string, ReferenceCacheItem>();
            _referenceItemCacheBtiId = new Dictionary<string, ReferenceCacheItem>();
            _streetCache = new Dictionary<string, StreetCacheItem>();
            _districtCache = new Dictionary<string, DistrictCacheItem>();
            _okrugCache = new Dictionary<string, OkrugCacheItem>();

            SqlDictionaryMapping = new DictionarySqlMapping();

            InitCache();
        }

        public ReferenceCacheItem GetReferenceItem(long nk, string kod)
        {
            ReferenceCacheItem referenceCacheItem;
            if (_referenceItemCacheKod.TryGetValue(nk + "_" + kod, out referenceCacheItem))
            {
                return referenceCacheItem;
            }
            return null;
            //throw new Exception("Отсутствует справочное значение (GetReferenceItem): " + nk + "_" + kod);
        }

        public ReferenceCacheItem GetReferenceItemById(long nk, long? btiId)
        {
            if (btiId == null || btiId == 0)
            {
                return null;
            }

            ReferenceCacheItem referenceCacheItem;
            if (_referenceItemCacheBtiId.TryGetValue(nk + "_" + btiId, out referenceCacheItem))
            {
                return referenceCacheItem;
            }
            return null;
            //throw new Exception("Отсутствует справочное значение (GetReferenceItemById): " + nk + "_" + btiId);
        }

        public StreetCacheItem GetStreetCacheItem(string codeGivc)
        {
            if (codeGivc.ParseToInt() == 0)
            {
                return null;
            }

            if (!string.IsNullOrEmpty(codeGivc) && _streetCache.ContainsKey(codeGivc))
            {
                return _streetCache[codeGivc];
            }

            throw new Exception("Отсутствует справочное значение (GetStreetCacheItem): " + codeGivc);
        }

        public DistrictCacheItem GetDistrictCacheItem(string steksCode)
        {
            if (steksCode.ParseToInt() == 0)
            {
                return null;
            }

			if(!_districtCache.ContainsKey(steksCode))
			{
				throw new Exception("Не найден район с кодом: " + steksCode);
			}
			
			return _districtCache[steksCode];
		}

        public OkrugCacheItem GetOkrugCacheItem(string steksCode)
        {
            if (steksCode.ParseToInt() == 0)
            {
                return null;
            }

            if (!_okrugCache.ContainsKey(steksCode))
            {
				throw new Exception("Не найден округ с кодом: " + steksCode);
			}

			return _okrugCache[steksCode];
		}
    }
}
