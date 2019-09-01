using Core.Register.LongProcessManagment;
using Core.Shared.Extensions;
using Microsoft.Practices.EnterpriseLibrary.Data;
using ObjectModel.Core.LongProcess;
using ObjectModel.Insur;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;

namespace CIPJS.DAL.OrgUnom
{
    /// <summary>
    /// CIPJS-732 - Наполнить реестр INSUR_ORG_UNOM (написать скрипт для заполнения) на основании реестра INSUR_BANK_PLAT
    /// </summary>
    public class OrgUnomUpdateProcess : ILongProcess
    {
        public void StartProcess(OMProcessType processType, OMQueue processQueue, CancellationToken cancellationToken)
        {
            int buildingPackageIndex = 0;

            ParallelOptions parallelOptions = new ParallelOptions()
            {
                MaxDegreeOfParallelism = 10
            };

            while (true)
            {
                //в 316 реестре отсортировать записи по EMP_ID
                List<OMBuilding> buildingList = OMBuilding
                    .Where(x => true)
                    .SetPackageSize(1000)
                    .SetPackageIndex(buildingPackageIndex)
                    .Execute();

                if (buildingList.Count == 0)
                {
                    break;
                }
                
                Parallel.ForEach(buildingList, parallelOptions, building =>
                {
                    //Как определить значение ORG_ID
                    //В INSUR_FLAT найти все записи, для которых INSUR_FLAT.LINK_MKD = INSUR_BUILDING.EMP_ID
                    //Для всех найденных квартир определяем INSUR_FLAT.EMP_ID
                    //Ищем ФСП, связанные с квартирами , найти все INSUR_FSP.OBJ_ID = INSUR_FLAT.EMP_ID
                    //Найти зачисления, связанные с найденными ФСП, найти все INSUR_INPUT_PLAT.FSP_ID = INSUR_FSP.EMP_ID + PERIOD_REG_DATE + INSUR_INPUT_PLAT.LINK_BANK_ID is not_null
                    //Для любой строки с зачислением, определить значение INSUR_INPUT_PLAT.LINK_BANK_ID
                    //Найти INSUR_BANK_PLAT.EMP_ID = INSUR_INPUT_PLAT.LINK_BANK_ID
                    DbCommand bankPlatCommand = DBMngr.Realty.GetSqlStringCommand(@"select ibp.kod_ypravl, ibp.kod_post, ibp.kod_ysl from insur_bank_plat ibp
join insur_input_plat iip on iip.link_bank_id = ibp.emp_id
join insur_fsp_q ifq on ifq.emp_id = iip.fsp_id and ifq.actual = 1
join insur_flat_q iflq on iflq.emp_id = ifq.obj_id and iflq.actual = 1
join insur_building_q ibq on ibq.emp_id = iflq.link_object_mkd and ibq.actual = 1
where ibq.emp_id = @p_buildingid order by ibp.period_reg_date desc limit 1");
                    DbParameter buildingIdParameter = bankPlatCommand.CreateParameter();
                    buildingIdParameter.ParameterName = "p_buildingid";
                    buildingIdParameter.DbType = DbType.Int64;
                    buildingIdParameter.Value = building.EmpId;
                    bankPlatCommand.Parameters.Add(buildingIdParameter);
                    
                    //TODO DbCommand.Prepare и CreateParameter для Parallel.ForEach
                    DataTable bankPlatDataTable = DBMngr.Realty.ExecuteDataSet(bankPlatCommand).Tables[0];

                    if (bankPlatDataTable.Rows.Count > 0)
                    {
                        DataRow bankPlatRow = bankPlatDataTable.Rows[0];

                        OMOrgUnom orgUnom = OMOrgUnom.Where(x => x.ObjId == building.EmpId).SelectAll().ExecuteFirstOrDefault();

                        if (orgUnom == null)
                        {
                            orgUnom = new OMOrgUnom
                            {
                                ObjId = building.EmpId
                            };
                        }

                        orgUnom.OrgId = bankPlatRow["kod_ypravl"] != DBNull.Value ? (long?)bankPlatRow["kod_ypravl"].ParseToLong() : null;
                        orgUnom.KodPost = bankPlatRow["kod_post"] != DBNull.Value ? (long?)bankPlatRow["kod_post"].ParseToLong() : null;
                        orgUnom.KodYsl = bankPlatRow["kod_ysl"] != DBNull.Value ? (long?)bankPlatRow["kod_ysl"].ParseToLong() : null;
                        orgUnom.DateInput = DateTime.Now;
                        orgUnom.Save();
                    }
                });

                buildingPackageIndex++;
            }
        }

        public void LogError(long? objectId, Exception ex, long? errorId = null)
        {

        }

        public bool Test()
        {
            return true;
        }
    }
}
