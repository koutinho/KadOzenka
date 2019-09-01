using CIPJS.DAL.Bti.Import.Model;
using Core.Shared.Extensions;
using Core.Shared.Misc;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Linq;

namespace CIPJS.DAL.Bti.Import
{
    /// <summary>
    /// адреса, а именно районы, округа и улицы не простые плоские справочники
    /// потому делаем для них особый функционал
    /// </summary>
    public class SynchronizationAddresses : IDisposable
    {
        static readonly object Locker = new object();

        /// <summary>
        /// идентификатор в классификаторе для районов
        /// </summary>
        readonly List<int> DistrictNk = new List<int> { 45, 532 };

        /// <summary>
        /// идентификатор в классификаторе для округов
        /// </summary>
        const int OkrugNk = 44;
		
        IEnumerable<DataRow> _fklsRows;

        /// <summary>
        /// округа и районы
        /// </summary>
        IEnumerable<DataRow> FklsRows
        {
            get
            {
                return _fklsRows;
            }
        }

        IEnumerable<DataRow> _fkunRows;

        /// <summary>
        /// улицы
        /// </summary>
        IEnumerable<DataRow> FkunRows
        {
            get
            {
                return _fkunRows;
            }
        }

        /// <summary>
        /// кешируем соответсвие идентификаторов округов БТИ (KOD) - РСМ
        /// </summary>
        ConcurrentDictionary<int, int> CasheOkrugs { get; set; }

        /// <summary>
        /// кешируем соответствие идентификаторов районов БТИ (KOD) - РСМ
        /// </summary>
        ConcurrentDictionary<int, int> CasheDistricts { get; set; }


        public SynchronizationAddresses()
        {
            LoadImportDictionaries();

            CasheOkrugs = new ConcurrentDictionary<int, int>();
            CasheDistricts = new ConcurrentDictionary<int, int>();
        }

        /// <summary>
        /// Загружаем данные в FklsRows, FkunRows
        /// </summary>
        private void LoadImportDictionaries()
        {
            DbCommand okrugDistrictCommand = CipjsDbManager.Dgi.GetSqlStringCommand(string.Format(@"select kls.* from {2} kls where kls.NK IN ({0},{1})", String.Join(",", DistrictNk), OkrugNk, Importer.KlsTableName));
            _fklsRows = CipjsDbManager.Dgi.ExecuteDataSet(okrugDistrictCommand).Tables[0].AsEnumerable();
			
            DbCommand fkunCommand = CipjsDbManager.Dgi.GetSqlStringCommand(string.Format(@"select fkun.* from {0} fkun", Importer.FkunTableName));
            _fkunRows = CipjsDbManager.Dgi.ExecuteDataSet(fkunCommand).Tables[0].AsEnumerable();
        }

        /// <summary>
        /// основной метод слияния адресных словарей
        /// </summary>
        public void Merge()
        {
            MergeOkrugs();
            Console.WriteLine("{0: dd.MM.yyyy HH:mm:ss}: Закончена загрузка округов", DateTime.Now);
            MergeDistricts();
            Console.WriteLine("{0: dd.MM.yyyy HH:mm:ss}: Закончена загрузка районов", DateTime.Now);
            MergeStreets();
            Console.WriteLine("{0: dd.MM.yyyy HH:mm:ss}: Закончена загрузка улиц", DateTime.Now);
        }

        public void MergeOkrugs()
        {
            List<Okrug> okrugs = GetOkrugs().ToList();

            foreach (Okrug okrug in GetOkrugs())
            {
                int okrugId = okrug.Merge().ParseToInt();

                lock (Locker)
                {
                    CasheOkrugs.TryAdd(okrug.SteksCode.ParseToInt(), okrugId);
                }
            }
        }

		public void MergeDistricts()
        {
            List<District> districts = GetDistricts().ToList();

            foreach (District district in districts)
            {
                int okrugIdRsm = CasheOkrugs[district.GetOkrugKodBti()];

                district.OKRUG_ID = okrugIdRsm;
                int districtId = district.Merge().ParseToInt();

                lock (Locker)
                {
                    CasheDistricts.TryAdd(district.SteksCode.ParseToInt(), districtId);
                }
            }
        }

		public void MergeStreets()
        {
            foreach (Street street in GetStreets())
            {
                SetStreetDicstrict(street);
                street.Merge();
            }
        }

        /// <summary>
        /// получаем районы
        /// </summary>
        /// <returns></returns>
        IEnumerable<District> GetDistricts()
        {
            return FklsRows
                 .Where(r => !r["NK"].IsNullOrDbNull() && DistrictNk.Contains(Convert.ToInt32(r["NK"])))
                 .Select(r => new District(r));
        }

        /// <summary>
        /// округа
        /// </summary>
        /// <returns></returns>
        IEnumerable<Okrug> GetOkrugs()
        {
            return FklsRows
                .Where(r => !r["NK"].IsNullOrDbNull() && Convert.ToInt32(r["NK"]) == OkrugNk)
                .Select(r => new Okrug(r));
        }

        /// <summary>
        /// улицы
        /// </summary>
        /// <returns></returns>
        IEnumerable<Street> GetStreets()
        {
            return FkunRows.Select(r => new Street(r));
        }

        public void Dispose()
        {
        }

        static readonly char[] Separator = { ',' };

        /// <summary>
        /// получаем список районов в которых находиться улица
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        static IEnumerable<int> GetDistrictsFromStreet(DataRow row)
        {
            if (row == null)
            {
                throw new ArgumentNullException("row");
            }

            string btiDistricts = row.GetValueOrDefault<string>("TERR");

            if (string.IsNullOrEmpty(btiDistricts))
            {
                yield break;
            }

            string[] arr = btiDistricts.Split(Separator);

            foreach (var s in arr)
            {
                int districtIdBti;
                if (int.TryParse(s, out districtIdBti))
                {
                    yield return districtIdBti;
                }
            }
        }

        void SetStreetDicstrict(Street street)
        {
            foreach (int districtIdBti in GetDistrictsFromStreet(street.Row))
            {
                if (CasheDistricts.ContainsKey(districtIdBti))
                {
                    street.AddDistrict(CasheDistricts[districtIdBti]);
                }

                District streetDistrict = new District(districtIdBti);

                long? res;
                if (streetDistrict.IsExists(out res))
                {
                    CasheDistricts.TryAdd(districtIdBti, res.ParseToInt());
                    street.AddDistrict(res);
                }

                int okrug = CasheOkrugs[streetDistrict.GetOkrugKodBti()];

                street.AddOkrug(okrug);
            }
        }
    }
}
