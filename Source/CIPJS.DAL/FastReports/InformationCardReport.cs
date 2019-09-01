using ObjectModel.Insur;
using Platform.Reports;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;

namespace CIPJS.DAL.FastReports
{
    public class InformationCardReport : FastReportBase
    {
        protected override string TemplateName(NameValueCollection query)
        {
            return "InformationCardReport";
        }

        public override string GetTitle(long? objectId)
        {
            return ReportType.Title;
        }

        protected override DataSet GetData(NameValueCollection query, HashSet<long> objectList = null)
        {
            DataSet dataSet = new DataSet();

            DataTable dataTable = new DataTable("Insur");
            dataTable.Columns.Add("NumDog");
            dataTable.Columns.Add("Address");
            dataTable.Columns.Add("InsurSum");
            dataTable.Columns.Add("MoscowPart");
            dataTable.Columns.Add("Payable");
            dataTable.Columns.Add("IDFILE");

            DataTable dtGroup = new DataTable("InsurGroup");
            dtGroup.Columns.Add("IDFILE");
            dtGroup.Columns.Add("PayableTotal");

            string address = "";
            string insurer = "";
            decimal payableTotal = 0;
            decimal payable = 0;

            if (objectList == null)
            {
                objectList = new HashSet<long>();
                objectList.Add(ObjectId.Value);
            }

            //эта карточка строится только для счетов, связанных с ОДНИМ получателем , то есть для каждого счета нужно по связке с договором, 
            //определить значение ORG_ID_FILE, для всех записей оно должно быть одинаковым. Если разное то таблиц должно быть в отчете несколько 
            //одна для одного Получателя, вторая для другого и т.д

            List<long?> LinkAllPropertyList = OMInvoice.Where(x => objectList.Contains(x.EmpId)).Select(x => x.LinkAllProperty).Execute().ToList().Select(x => x.LinkAllProperty).ToList();

            if (LinkAllPropertyList.Count > 0)
            {
                List<OMAllProperty> oMAllProperties = OMAllProperty.Where(x => LinkAllPropertyList.Contains(x.EmpId) /* && oMInvoice.ReestrContractId == 310*/)
                    .Select(x => x.ParentBuilding.AddressId)
                    .Select(x => x.RasPripay)
                    .Select(x => x.PartCity)
                    .Select(x => x.Ndog)
                    .Select(x => x.OrgIdFile)
                    .Select(x => x.Name)
                    .Execute();

                foreach (OMAllProperty oMAllProperty in oMAllProperties)
                {
                    OMAddress oMAddress = oMAllProperty.ParentBuilding != null ? OMAddress.Where(x => x.EmpId == oMAllProperty.ParentBuilding.AddressId).Select(x => x.FullAddress).Execute().FirstOrDefault() : null;
                    address = oMAddress != null ? oMAddress.FullAddress : string.Empty;
                    insurer = oMAllProperty.Name;
                    payable = Math.Round((oMAllProperty.RasPripay ?? 0) * ((oMAllProperty.PartCity ?? 0) / 100), 2, MidpointRounding.AwayFromZero);
                    payableTotal += payable;

                    dataTable.Rows.Add(
                        oMAllProperty.Ndog,
                        address,
                        oMAllProperty.RasPripay?.ToString("n2"),
                        oMAllProperty.PartCity?.ToString("n2"),
                        payable.ToString("n2"),
                        oMAllProperty.OrgIdFile
                        );
                }

                if (oMAllProperties.Count > 0)
                {
                    List<ResultLine> group = oMAllProperties
                                    .GroupBy(x => x.OrgIdFile)
                                    .Select(cl => new ResultLine
                                    {
                                        IDFile = cl.First().OrgIdFile,
                                        PayableTotal = cl.Sum(c => Math.Round((c.RasPripay ?? 0) * ((c.PartCity ?? 0) / 100), 2, MidpointRounding.AwayFromZero))
                                    }).ToList();

                    foreach (var row in group)
                    {
                        dtGroup.Rows.Add(
                                row.IDFile,
                                row.PayableTotal?.ToString("n2")
                                );
                    }
                }

                DataView dv = dataTable.DefaultView;
                dv.Sort = "NumDog";
                dataTable = dv.ToTable();
            }
            else
            {
                dataTable.Rows.Add("", "", "", "", "");
            }

            DataTable dtCommon = new DataTable("COMMON");
            dtCommon.Columns.Add("FIO");
            dtCommon.Columns.Add("DEP");
            dtCommon.Columns.Add("Insurer");

            string podpisant = GetQueryParam<string>("Podpisant", query);
            bool noSign = GetQueryParam<bool>("NoSign", query);
            string[] sep = podpisant.Split('-');
            string department = !noSign ? sep[1].ToString().Trim() : string.Empty,
                   fio = !noSign ? sep[0].ToString().Trim() : string.Empty;

            dtCommon.Rows.Add(
                fio,
                department,
                insurer
                );

            dataSet.Tables.Add(dataTable);
            dataSet.Tables.Add(dtCommon);
            dataSet.Tables.Add(dtGroup);

            return dataSet;
        }

        public class ResultLine
        {
            public long? IDFile { get; set; }
            public decimal? PayableTotal { get; set; }
        }
    }
}