using System.Collections.Generic;
using System.Threading;
using GemBox.Spreadsheet;
using ObjectModel.Core.LongProcess;
using ObjectModel.Insur;

using System.Linq;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System;
using System.Data;
using ObjectModel.Directory;
using CIPJS.DAL.StrahNach;
using System.Transactions;
using Core.Shared.Misc;
using System.Threading.Tasks;
using CIPJS.DAL.Fsp;
using System.Configuration;

using System.Diagnostics;
using System.Collections.Concurrent;
using ObjectModel.ImportLog;
using Newtonsoft.Json.Linq;
using CIPJS.DAL.InsuranceObjectLoader;
using CIPJS.DAL.InputFile;
using ObjectModel.Core.LongProcess;
using CIPJS.DAL.Egas;
using Core.Register.LongProcessManagment;
using ObjectModel.Core.SRD;
using ObjectModel.Core;
using CIPJS.DAL.PostgresBackup;
using System.IO;
using DotNetDBF;
using Newtonsoft.Json;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using CIPJS.DAL.OrgUnom;
using CIPJS.DAL.Health;
using Platform.Main.ConfigParam.HealthCheck;
using Core.Numerator;
using Core.Register.RegisterEntities;
using Core.Register.DAL;
using System.Data;
using CIPJS.DAL.Building;
using CIPJS.Models.Tenements;
using Platform.Configurator;
//using DbConnectionClasses;

namespace DebugApplication
{
    class Program
    {
        private volatile static int iCounted = 0;
        private volatile static Dictionary<long, string> fspsErors = new Dictionary<long, string>();

        static void Main(string[] args)
        {
            long id = 32849400;


            BuildingService _buildingService = new BuildingService();
            _buildingService.UnLinkMkd(id);
        }

        static void Main2(string[] args)
        {
            Console.WriteLine("Started..."); ;

            FspService fspService = new FspService();

            string actualFsps = ConfigurationManager.AppSettings["fsps"];
            string queryFsps = string.Empty;
            if (actualFsps.Length > 0)
                queryFsps = $"SELECT t.fsp_id FROM insur_balance t WHERE t.fsp_id in ({actualFsps}) GROUP BY t.fsp_id";
            else
                queryFsps = $"SELECT t.fsp_id FROM insur_balance t GROUP BY t.fsp_id";

            DbCommand cmdFsps = DBMngr.Realty.GetSqlStringCommand(queryFsps);
            var dtFsps = DBMngr.Realty.ExecuteDataSet(cmdFsps).Tables[0];

            int page = 0;
            int pageSize = 10000;

            while (true)
            {
                List<long> fsps = dtFsps.AsEnumerable().Skip(page * pageSize).Take(pageSize).Select(s => s.Field<long>("FSP_ID")).ToList();

                if (fsps.Count == 0)
                    break;

                Dictionary<long, List<OMBalance>> dicBalances = new Dictionary<long, List<OMBalance>>();

                List<OMBalance> omBalances = OMBalance.Where(w => fsps.Contains((long)w.FspId)).SelectAll().Execute();

                omBalances.Select(s =>
                {
                    #region 1
                    //OMBalance omBalance = new OMBalance();
                    //{
                    //    omBalance.EmpId = Convert.ToInt64(s["EMP_ID"]);
                    //    omBalance.FspId = Convert.ToInt64(s["FSP_ID"]);
                    //    omBalance.FlagOpl = Convert.ToBoolean(s["FLAG_OPL"]);
                    //    omBalance.LinkInputNach = Convert.ToInt64(s["LINK_INPUT_NACH"] != DBNull.Value ? s["LINK_INPUT_NACH"] : null);
                    //    omBalance.FlagInsur = Convert.ToBoolean(s["FLAG_INSUR"] != DBNull.Value ? s["FLAG_INSUR"] : false);
                    //    omBalance.OstatokSum = Convert.ToDecimal(s["OSTATOK_SUM"]);
                    //    omBalance.PeriodRegDate = Convert.ToDateTime(s["PERIOD_REG_DATE"]);
                    //    omBalance.SumOpl = Convert.ToDecimal(s["SUM_OPL"] != DBNull.Value ? s["SUM_OPL"] : 0m);
                    //    omBalance.SumNachMfc = Convert.ToDecimal(s["SUM_NACH_MFC"] != DBNull.Value ? s["SUM_NACH_MFC"] : 0m);
                    //    omBalance.SumNachGby = Convert.ToDecimal(s["SUM_NACH_GBY"] != DBNull.Value ? s["SUM_NACH_GBY"] : 0m);
                    //    omBalance.SumNachOpl = Convert.ToDecimal(s["SUM_NACH_OPL"] != DBNull.Value ? s["SUM_NACH_OPL"] : 0m);
                    //    omBalance.StrahEnd = Convert.ToDateTime(s["STRAH_END"] != DBNull.Value ? s["STRAH_END"] : null);


                    //}

                    //OMBalance omBalance = OMBalance.Where(w => w.EmpId == Convert.ToInt64(s["EMP_ID"])).SelectAll().ExecuteFirstOrDefault();
                    #endregion 1

                    if (!dicBalances.ContainsKey(s.FspId.Value))
                        dicBalances.Add(s.FspId.Value, new List<OMBalance>() { s });
                    else
                        dicBalances[s.FspId.Value].Add(s);
                    //if(s.FspId.HasValue)
                    //    dicBalances.Add(s.FspId.Value, s);

                    return s.EmpId.Value;
                }).ToList();

                ParallelOptions options = new ParallelOptions
                {
                    CancellationToken = new CancellationToken(),
                    MaxDegreeOfParallelism = 2
                };

                Parallel.ForEach(dicBalances.Keys, delegate (long fspId)
                {
                    try
                    {
                        //List<OMFsp> omFsps = OMFsp.Where(w => w.EmpId == fspId).SelectAll().Select(s=> s.SpecialS).Select(s=> s.SpecialPo).Execute();
                        List<OMBalance> balances = dicBalances[fspId].OrderBy(o => o.PeriodRegDate).ToList();

                        //List<OMBalance> bals = new List<OMBalance>();
                        if (balances.Count > 0)
                            fspService.CalcBalanceSumFromPeriod(fspId, balances[0].PeriodRegDate.Value, balances, null, null, true);

                        //foreach (OMBalance omBalance in balances)
                        //{
                        //    bals.Add(omBalance);

                        //    //OMFsp omFsp = omFsps.Where(w => w.SpecialS <= omBalance.PeriodRegDate && omBalance.PeriodRegDate <= w.SpecialPo).FirstOrDefault();

                        //    fspService.CalcBalanceSumFromPeriod(fspId, omBalance.PeriodRegDate.Value, bals, omFsp);
                        //}
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"fspId={fspId}: {ex.Message}");
                        //fspsErors.Add(fspId, ex.Message);
                    }

                    iCounted++;
                    if (iCounted % pageSize == 0)
                        Console.WriteLine(iCounted);
                });

                page++;
            }


            if (fspsErors.Count > 0)
            {
                Console.WriteLine("Not processed fsps:");
                foreach (long fsp in fspsErors.Keys)
                {
                    Console.WriteLine($"{fsp}={fspsErors[fsp]}");
                }
            }

            Console.WriteLine("Complete! Press any key to exit...");
            Console.ReadKey();
        }
    }
}
