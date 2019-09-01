using CIPJS.DAL.InputFile;
using CIPJS.Models.InputFile;
using Core.ErrorManagment;
using Core.Register.LongProcessManagment;
using Core.Register.QuerySubsystem;
using Core.Shared.Extensions;
using Core.Shared.Misc;
using Core.UI.Registers.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Newtonsoft.Json;
using ObjectModel.Directory;
using ObjectModel.Insur;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Transactions;

namespace CIPJS.Controllers
{
    public class InputFileController : BaseController
    {
        private readonly InputFileService _inputFileService;

        public InputFileController(InputFileService mfcUploadService)
        {
            _inputFileService = mfcUploadService;
        }

        public ContentResult GetStatisticNachList(long? okrugId, DateTime? periodRegDate)
        {
            List<InputFileStatisticsNachDto> result = new List<InputFileStatisticsNachDto>();

            if (okrugId.HasValue && periodRegDate.HasValue)
            {
                QSQuery query = new QSQuery
                {
                    MainRegisterID = OMDistrict.GetRegisterId(),
                    Columns = new List<QSColumn>
                    {
                        OMInputFile.GetColumn(x => x.DistrictId, "DistrictId"),
                        OMDistrict.GetColumn(x => x.Name, "DistrictName"),
                        OMDistrict.GetColumn(x => x.Code, "DistrictCode"),
                        OMInputFile.GetColumn(x => x.EmpId, "InputFileId"),
                        OMInputFile.GetColumn(x => x.FileName, "FileName"),
                        OMInputFile.GetColumn(x => x.Status, "Status"),
                        OMInputFile.GetColumn(x => x.Status_Code, "StatusCode"),
                        OMInputFile.GetColumn(x => x.CountStrLoad, "CountStrLoad"),
                        OMInputFile.GetColumn(x => x.CountStr, "CountStr"),
                        OMInputFile.GetColumn(x => x.SumAll, "SumNach")
                    },
                    Joins = new List<QSJoin>
                    {
                        new QSJoin
                        {
                            RegisterId = OMInputFile.GetRegisterId(),
                            JoinType = QSJoinType.Left,
                            JoinCondition = new QSConditionGroup
                            {
                                Type = QSConditionGroupType.And,
                                Conditions = new List<QSCondition>
                                {
                                    OMInputFile.GetCondition(x => x.DistrictId == x.ParentDistrict.Id),
                                    OMInputFile.GetCondition(x => x.PeriodRegDate == periodRegDate.Value),
                                    OMInputFile.GetCondition(x => x.TypeFile_Code == TypeFile.Nach)
                                }
                            }
                        }
                    },
                    Condition = new QSConditionGroup
                    {
                        Type = QSConditionGroupType.And,
                        Conditions = new List<QSCondition>
                        {
                            OMDistrict.GetCondition(x => x.OkrugId == okrugId.Value)
                        }
                    },
                    OrderBy = new List<QSOrder>
                    {
                        new QSOrder { Column = OMDistrict.GetColumn(x => x.Code), Order = QSOrderType.ASC }
                    }
                };

                DataTable dt = query.ExecuteQuery();

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        result.Add(new InputFileStatisticsNachDto
                        {
                            DistrictId = row["DistrictId"].ParseToLong(),
                            DistrictName = row["DistrictName"] != DBNull.Value ? row["DistrictName"].ToString() : null,
                            DistrictCode = row["DistrictCode"] != DBNull.Value ? (long?)row["DistrictCode"].ParseToLong() : null,
                            InputFileId = row["InputFileId"].ParseToLong(),
                            FileName = row["FileName"] != DBNull.Value ? row["FileName"].ToString() : null,
                            Status = row["Status"] != DBNull.Value ? row["Status"].ToString() : null,
                            StatusCode = row["StatusCode"] != DBNull.Value ? (long?)row["StatusCode"].ParseToLong() : null,
                            CountStrLoad = row["CountStrLoad"].ParseToLong(),
                            CountStr = row["CountStr"].ParseToLong(),
                            CountError = 0,
                            SumNach = row["SumNach"] != DBNull.Value ? (decimal?)row["SumNach"].ParseToDecimal() : null
                        });
                    }
                }
            }

            return Content(JsonConvert.SerializeObject(result), "application/json");
        }

        public ContentResult GetStatisticPlatList(long? okrugId, DateTime? periodRegDate)
        {
            List<InputFileStatisticsPlatDto> result = new List<InputFileStatisticsPlatDto>();

            if (okrugId.HasValue && periodRegDate.HasValue)
            {
                string sql = $@"
SELECT
	L1_R321.ID AS ""ID"",
	L1_R321.ID AS ""DistrictId"",
	L1_R321.NAME AS ""DistrictName"",
	L1_R321.CODE AS ""DistrictCode"",
	L1_R301.EMP_ID AS ""InputFileId"",
	L1_R301.FILE_NAME AS ""FileName"",
	L1_R301.STATUS AS ""Status"",
	L1_R301.STATUS_CODE AS ""StatusCode"",
	L1_R301.COUNT_STR AS ""CountStr"",
	L1_R301.COUNT_STR_LOAD AS ""CountStrLoad"",
	L1_R301.SUM_ALL AS ""SumOpl"",

	(SELECT
	 COUNT(1)  AS ""IdentifiedCount""
FROM INSUR_INPUT_PLAT L2_R306
WHERE
(
L1_R301.EMP_ID IS NOT NULL
 AND
 L2_R306.LINK_ID_FILE = L1_R301.EMP_ID
 AND
 (
L2_R306.STATUS_IDENTIF_CODE = 10000107
 OR
 L2_R306.STATUS_IDENTIF_CODE = 10000109
)

)
) AS ""IdentifiedCount"",
	(SELECT
	 COUNT(1)  AS ""NotIdentifiedCount""
FROM INSUR_INPUT_PLAT L2_R306
WHERE
(
L1_R301.EMP_ID IS NOT NULL
 AND
 L2_R306.LINK_ID_FILE = L1_R301.EMP_ID
 AND
 (
L2_R306.STATUS_IDENTIF_CODE = 0
 OR
 L2_R306.STATUS_IDENTIF_CODE = 10000111
)

)
) AS ""NotIdentifiedCount"",

(select count(1)
from INSUR_BANK_PLAT L2_R303
WHERE
(
L1_R301.EMP_ID IS NOT NULL
 AND
 L2_R303.DISTRICT_ID = L1_R301.DISTRICT_ID
 AND
 L2_R303.PERIOD_REG_DATE = L1_R301.PERIOD_REG_DATE
 AND not exists(select 1 from INSUR_INPUT_PLAT L2_R306
 where L2_R306.LINK_BANK_ID = L2_R303.EMP_ID)

)
) AS ""NotIdentifiedBankCount"",

	(SELECT
	 COUNT(1)  AS ""IdentifiedFullCount""
FROM INSUR_INPUT_PLAT L2_R306
WHERE
(
L1_R301.EMP_ID IS NOT NULL
 AND
 L2_R306.LINK_ID_FILE = L1_R301.EMP_ID
 AND
 L2_R306.STATUS_IDENTIF_CODE = 10000107
)
) AS ""IdentifiedFullCount"",
	(SELECT
	 COUNT(1)  AS ""InputPlatSumNotNullCount""
FROM INSUR_INPUT_PLAT L2_R306
WHERE
(
L1_R301.EMP_ID IS NOT NULL
 AND
 L2_R306.LINK_ID_FILE = L1_R301.EMP_ID
 AND
 L2_R306.SUM_OPL IS NOT NULL
 AND
 L2_R306.SUM_OPL <> 0
)
) AS ""InputPlatSumNotNullCount"",
	(SELECT
	 COUNT(1)  AS ""PartiallyIdentifiedCount""
FROM INSUR_INPUT_PLAT L2_R306
WHERE
(
L1_R301.EMP_ID IS NOT NULL
 AND
 L2_R306.LINK_ID_FILE = L1_R301.EMP_ID
 AND
 L2_R306.STATUS_IDENTIF_CODE = 10000109
)
) AS ""PartiallyIdentifiedCount"",
	(SELECT
	 COUNT(1)  AS ""NotConfirmedByBankCount""
FROM INSUR_INPUT_PLAT L2_R306
WHERE
(
L1_R301.EMP_ID IS NOT NULL
 AND
 L2_R306.LINK_ID_FILE = L1_R301.EMP_ID
 AND
 L2_R306.STATUS_IDENTIF_CODE = 12093004
)
) AS ""NotConfirmedByBankCount""
FROM INSUR_DISTRICT L1_R321
 LEFT JOIN INSUR_INPUT_FILE L1_R301
 ON((
L1_R301.DISTRICT_ID = L1_R321.ID
 AND
 L1_R301.PERIOD_REG_DATE = {DBMngr.Realty.FormatDate(periodRegDate.Value)}
 AND
 L1_R301.TYPE_FILE_CODE = 12120004
)
)
WHERE
(
L1_R321.OKRUG_ID = {okrugId}
)
 ORDER BY L1_R321.CODE ASC";

                DbCommand command = DBMngr.Realty.GetSqlStringCommand(sql);
                DataTable dt = DBMngr.Realty.ExecuteDataSet(command).Tables[0];

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        result.Add(new InputFileStatisticsPlatDto
                        {
                            DistrictId = row["DistrictId"].ParseToLong(),
                            DistrictName = row["DistrictName"] != DBNull.Value ? row["DistrictName"].ToString() : null,
                            DistrictCode = row["DistrictCode"] != DBNull.Value ? (long?)row["DistrictCode"].ParseToLong() : null,
                            InputFileId = row["InputFileId"].ParseToLong(),
                            FileName = row["FileName"] != DBNull.Value ? row["FileName"].ToString() : null,
                            Status = row["Status"] != DBNull.Value ? row["Status"].ToString() : null,
                            StatusCode = row["StatusCode"] != DBNull.Value ? (long?)row["StatusCode"].ParseToLong() : null,
                            CountStr = row["CountStr"].ParseToLong(),
                            CountStrLoad = row["CountStrLoad"].ParseToLong(),
                            CountError = 0,
                            SumOpl = row["SumOpl"] != DBNull.Value ? (decimal?)row["SumOpl"].ParseToDecimal() : null,
                            IdentifiedCount = row["IdentifiedCount"].ParseToLong(),
                            NotIdentifiedCount = row["NotIdentifiedCount"].ParseToLong(),
                            NotIdentifiedBankCount = row["NotIdentifiedBankCount"].ParseToLong(),
                            IdentifiedFullCount = row["IdentifiedFullCount"].ParseToLong(),
                            InputPlatSumNotNullCount = row["InputPlatSumNotNullCount"].ParseToLong(),
                            PartiallyIdentifiedCount = row["PartiallyIdentifiedCount"].ParseToLong(),
                            NotConfirmedByBankCount = row["NotConfirmedByBankCount"].ParseToLong()
                        });
                    }
                }
            }

            return Content(JsonConvert.SerializeObject(result), "application/json");
        }

        public ContentResult GetStatisticSvodBankList(long? okrugId, DateTime? periodRegDate)
        {
            List<InputFileStatisticsSvodBankDto> result = new List<InputFileStatisticsSvodBankDto>();

            if (okrugId.HasValue && periodRegDate.HasValue)
            {
                // CIPJS-875
                string sql = $@"
with post as (
	select sp.kod_post||'' cod_post,
    	row_number() over (order by sp.date_start desc, sp.status_new desc, sp.emp_id desc) rn
    from insur_okrug io
    join insur_strah_post sp on sp.link_company = io.insurance_company_id
	where io.id = {okrugId.Value} and sp.date_start <= {DBMngr.Realty.FormatDate(periodRegDate.Value)}
),
bank as (
    select count(1) count_str, sum(case when ibp.flag_vozvr = 1 then 0 - ibp.sum_by_code else ibp.sum_by_code end) opl,
     iif.district_id, isb.cod_post
    from insur_district ds
    join insur_input_file iif on iif.district_id = ds.id and iif.period_reg_date = {DBMngr.Realty.FormatDate(periodRegDate.Value)}
    join insur_svod_bank isb on isb.link_id_file = iif.emp_id
    join insur_bank_plat ibp on ibp.link_id_file = iif.emp_id
    where ds.okrug_id = {okrugId.Value}
    group by iif.district_id, isb.cod_post
),
mfc_plats as (
    select count(1) count_str, sum(iip.sum_opl) opl, iif.district_id, iip.status_identif_code
    from insur_district ds
    join insur_input_file iif on iif.district_id = ds.id and iif.period_reg_date = {DBMngr.Realty.FormatDate(periodRegDate.Value)}
    join insur_input_plat iip on iip.link_id_file = iif.emp_id
    where ds.okrug_id = {okrugId.Value}
    group by iif.district_id, iip.status_identif_code
)
select insurd.id as ""DistrictId"",
insurd.name as ""DistrictName"",
insurd.code as ""DistrictCode"",
(select sum(mfc.count_str) from mfc_plats mfc where mfc.district_id = insurd.id) as ""CountStrMfc"",
(select sum(mfc.opl) from mfc_plats mfc where mfc.district_id = insurd.id) as ""SumOpl"",
(select sum(mfc.count_str) from mfc_plats mfc where mfc.district_id = insurd.id and mfc.status_identif_code = {(long)StatusIdentifikacii.NotConfirmedByBank}) as ""CountStrMfcError"",
(select sum(mfc.opl) from mfc_plats mfc where mfc.district_id = insurd.id and mfc.status_identif_code = {(long)StatusIdentifikacii.NotConfirmedByBank}) as ""SumOplError"",
(select sum(bnk.count_str) from bank bnk where bnk.district_id = insurd.id) as ""CountStrSvodBank"",
(select sum(bnk.opl) from bank bnk where bnk.district_id = insurd.id) as ""SumOplSvodBank"",
(select sum(bnk.count_str) from bank bnk where bnk.district_id = insurd.id and bnk.cod_post = post1.cod_post) as ""CountStrPost1"",
(select sum(bnk.count_str) from bank bnk where bnk.district_id = insurd.id and bnk.cod_post != post1.cod_post) as ""CountStrPost2"",
(select sum(bnk.opl) from bank bnk where bnk.district_id = insurd.id and bnk.cod_post = post1.cod_post) as ""SumOplPost1"",
(select sum(bnk.opl) from bank bnk where bnk.district_id = insurd.id and bnk.cod_post != post1.cod_post) as ""SumOplPost2"",
post1.cod_post as ""KodPost1"",
post2.cod_post as ""KodPost2""
from insur_district insurd
left
join post post1 on post1.rn = 1
left
join post post2 on post2.rn = 2
where insurd.okrug_id = { okrugId.Value}
                group by insurd.id, insurd.name, insurd.code, post1.cod_post, post2.cod_post
                order by insurd.code
";

                DbCommand command = DBMngr.Realty.GetSqlStringCommand(sql);
                DataTable dt = DBMngr.Realty.ExecuteDataSet(command).Tables[0];

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        long countStrMfc = row["CountStrMfc"].ParseToLong();
                        long countStrSvodBank = row["CountStrSvodBank"].ParseToLong();
                        long countStrMfcError = row["CountStrMfcError"].ParseToLong();
                        decimal? sumOpl = row["SumOpl"] != DBNull.Value ? (decimal?)row["SumOpl"].ParseToDecimal() : null;
                        decimal? sumOplSvodBank = row["SumOplSvodBank"] != DBNull.Value ? (decimal?)row["SumOplSvodBank"].ParseToDecimal() : null;
                        decimal? sumOplError = row["SumOplError"] != DBNull.Value ? (decimal?)row["SumOplError"].ParseToDecimal() : null;
                        result.Add(new InputFileStatisticsSvodBankDto
                        {
                            DistrictId = row["DistrictId"].ParseToLong(),
                            DistrictName = row["DistrictName"] != DBNull.Value ? row["DistrictName"].ToString() : null,
                            DistrictCode = row["DistrictCode"] != DBNull.Value ? (long?)row["DistrictCode"].ParseToLong() : null,
                            CountStrMfc = countStrMfc,
                            CountStrPost1 = row["CountStrPost1"].ParseToLong(),
                            CountStrPost2 = row["CountStrPost2"].ParseToLong(),
                            CountStrSvodBank = countStrSvodBank,
                            SumOpl = sumOpl,
                            SumOplPost1 = row["SumOplPost1"] != DBNull.Value ? (decimal?)row["SumOplPost1"].ParseToDecimal() : null,
                            SumOplPost2 = row["SumOplPost2"] != DBNull.Value ? (decimal?)row["SumOplPost2"].ParseToDecimal() : null,
                            SumOplSvodBank = sumOplSvodBank,
                            CountStrMfcError = countStrMfcError,
                            SumOplError = sumOplError,
                            ///CIPJS-508 убрать учет строк в статусе "Ошибочно" из колонок "Кол-во расхождений в строках" и "Сумма расхождений"
                            CountStrDifference = Math.Abs(countStrMfc - countStrSvodBank - countStrMfcError),
                            SumOplDifference = Math.Abs((sumOpl ?? 0) - (sumOplSvodBank ?? 0) - (sumOplError ?? 0)),
                            KodPost1 = row["KodPost1"] != DBNull.Value ? (long?)row["KodPost1"].ParseToLong() : null,
                            KodPost2 = row["KodPost2"] != DBNull.Value ? (long?)row["KodPost2"].ParseToLong() : null
                        });
                    }
                }
            }

            return Content(JsonConvert.SerializeObject(result), "application/json");
        }

        public ContentResult GetStatisticBankPlatList(long? okrugId, DateTime? periodRegDate)
        {
            List<InputFileStatisticsBankPlatDto> result = new List<InputFileStatisticsBankPlatDto>();

            if (okrugId.HasValue && periodRegDate.HasValue)
            {
                string sql = $@"
SELECT
	L1_R303.KOD_POST AS ""KodPost"",

     COUNT(1)  AS ""CountStr"",
     count(case when L1_R303.FLAG_VOZVR = 0 then 1 end) AS ""CountStrNotFlagVozvr"",
     count(case when L1_R303.FLAG_VOZVR = 1 then 1 end) AS ""CountStrFlagVozvr"",
	 Sum(L1_R303.SUM_BY_CODE)  AS ""Sum"",
     sum(case when L1_R303.FLAG_VOZVR = 0 then L1_R303.SUM_BY_CODE end) AS ""SumNotFlagVozvr"",
     sum(case when L1_R303.FLAG_VOZVR = 1 then L1_R303.SUM_BY_CODE end) AS ""SumFlagVozvr""

FROM INSUR_DISTRICT L1_R321
JOIN INSUR_BANK_PLAT L1_R303 ON L1_R303.DISTRICT_ID = L1_R321.ID
 AND L1_R303.PERIOD_REG_DATE = {DBMngr.Realty.FormatDate(periodRegDate.Value)}

WHERE L1_R321.OKRUG_ID = {okrugId}
GROUP BY L1_R303.KOD_POST

 ORDER BY L1_R303.KOD_POST DESC
";

                DbCommand command = DBMngr.Realty.GetSqlStringCommand(sql);
                DataTable dt = DBMngr.Realty.ExecuteDataSet(command).Tables[0];

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        decimal sumNotFlagVozvr = row["SumNotFlagVozvr"].ParseToDecimal();
                        decimal sumFlagVozvr = row["SumFlagVozvr"].ParseToDecimal();

                        result.Add(new InputFileStatisticsBankPlatDto
                        {
                            KodPost = row["KodPost"] != DBNull.Value ? (long?)row["KodPost"].ParseToLong() : null,
                            CountStr = row["CountStr"].ParseToLong(),
                            CountStrNotFlagVozvr = row["CountStrNotFlagVozvr"].ParseToLong(),
                            CountStrFlagVozvr = row["CountStrFlagVozvr"].ParseToLong(),
                            Sum = row["Sum"].ParseToDecimal(),
                            SumNotFlagVozvr = sumNotFlagVozvr,
                            SumFlagVozvr = sumFlagVozvr,
                            SumDifference = sumNotFlagVozvr - sumFlagVozvr
                        });
                    }
                }
            }

            return Content(JsonConvert.SerializeObject(result), "application/json");
        }

        [HttpPost]
        public ContentResult Process(long inputFileId)
        {
            try
            {
                OMInputFile inputFile = OMInputFile.Where(x => x.EmpId == inputFileId).SelectAll().Execute().FirstOrDefault();

                _inputFileService.StartProcess(inputFile);

                return EmptyResponse();
            }
            catch (Exception ex)
            {
                return ErrorResponse(ex.Message);
            }
        }

        [HttpPost]
        public ContentResult Delete(long inputFileId)
        {
            try
            {
                _inputFileService.Delete(inputFileId);

                return EmptyResponse();
            }
            catch (Exception ex)
            {
                return ErrorResponse(ex.Message);
            }
        }

        public ActionResult PlatIdentifyLog(long platIdentifyLogId)
        {
            OMFilePlatIdentifyLog log = OMFilePlatIdentifyLog
                    .Where(x => x.EmpId == platIdentifyLogId)
                    .SelectAll()
                    .Execute()
                    .FirstOrDefault();

            if (log == null)
            {
                throw new Exception("Не удалось определить лог идентификации");
            }

            return View(PlatIdentifyLogDto.OMMap(log));
        }

        public ContentResult GetPlatIdentifyLog(long platIdentifyLogId)
        {
            try
            {
                OMFilePlatIdentifyLog log = OMFilePlatIdentifyLog
                    .Where(x => x.EmpId == platIdentifyLogId)
                    .SelectAll()
                    .Execute()
                    .FirstOrDefault();

                if (log == null)
                {
                    throw new Exception("Не удалось определить лог идентификации");
                }

                return JsonResponse(PlatIdentifyLogDto.OMMap(log));
            }
            catch (Exception ex)
            {
                ErrorManager.LogError(ex);
                return ErrorResponse(ex.Message);
            }
        }

        public ActionResult InsurFileProcessLog(long fileProcessLogId)
        {
            OMFileProcessLog log = OMFileProcessLog
                .Where(x => x.EmpId == fileProcessLogId)
                .SelectAll()
                .Execute()
                .FirstOrDefault();

            if (log == null)
            {
                throw new Exception("Не удалось определить лог идентификации");
            }

            return View(InsurFileProcessLogDto.OMMap(log));
        }

        public ContentResult GetInsurFileProcessLog(long fileProcessLogId)
        {
            try
            {
                OMFileProcessLog log = OMFileProcessLog
                    .Where(x => x.EmpId == fileProcessLogId)
                    .SelectAll()
                    .Execute()
                    .FirstOrDefault();

                if (log == null)
                {
                    throw new Exception("Не удалось определить лог идентификации");
                }

                return JsonResponse(InsurFileProcessLogDto.OMMap(log));
            }
            catch (Exception ex)
            {
                ErrorManager.LogError(ex);
                return ErrorResponse(ex.Message);
            }
        }
    }
}