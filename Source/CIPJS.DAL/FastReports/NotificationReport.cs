using CIPJS.DAL.Dictionaries;
using Core.Shared.Extensions;
using ObjectModel.Insur;
using Platform.Reports;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;

namespace CIPJS.DAL.FastReports
{
    public class NotificationReport : FastReportBase
    {
        private string ReportNumber { get; set; }

        protected override string TemplateName(NameValueCollection query)
        {
            if (GetQueryParam<bool>("NoSign", query))
             return "NotificationReport_NoSign"; 
            else
             return "NotificationReport"; 
        }        

        public override string GetTitle(long? objectId)
        {
            return ReportType.Title;
        }

        protected override DataSet GetData(NameValueCollection query, HashSet<long> objectList = null)
        {            
            DataSet dataSet = new DataSet();
            dataSet.Tables.Add(GetInsurDataTable(query));

            return dataSet;
        }

        public override void SetReportNumber(ref string reportNumber, DateTime reportDate, NameValueCollection query)
        {
            reportNumber = ReportNumber;
        }

        private DataTable GetInsurDataTable(NameValueCollection query)
        {
            DataTable dataTable = new DataTable("Insur");
            dataTable.Columns.Add("ADDRESS");
            dataTable.Columns.Add("INSURER");
            dataTable.Columns.Add("POLICYNUMBER");
            dataTable.Columns.Add("DATEINSUR");
            dataTable.Columns.Add("PREMIUMINSUR");
            dataTable.Columns.Add("SHAREPREMIUMINSUR");
            dataTable.Columns.Add("SHARESIZE");
            dataTable.Columns.Add("INSURENT");
            dataTable.Columns.Add("SIGNATURE_FIO");
            dataTable.Columns.Add("SIGNATURE_DEP");

            string guidFiasMkd = string.Empty,
                   fullName = string.Empty,
                   insurWorker = string.Empty,
                   ndog = string.Empty,
                   ndogdat = string.Empty,
                   rasPripay = string.Empty,
                   partCity = string.Empty,
                   part = string.Empty;
            string subjectName = string.Empty;

            string podpisant = GetQueryParam<string>("Podpisant", query);
            bool noSign = GetQueryParam<bool>("NoSign", query);
            string[] sep = podpisant.Split('-');
            string sign = !noSign ? "/" + sep[0].ToString().Trim() + "/" : "/                   /",
                   department = !noSign 
                                ? sep[1].ToString().Trim()
                                .Replace(" ГБУ", Environment.NewLine + "ГБУ")
                                .Replace(" и жилищного", Environment.NewLine + "и жилищного")
                                .Replace(" перспективного", Environment.NewLine + "перспективного")
                                : string.Empty;


            OMAgreementProject oMAgreementProject = OMAgreementProject.Where(x => x.CalculationId == ObjectId).SelectAll().Execute().FirstOrDefault();            

            if (oMAgreementProject != null)
            {
                ndog = oMAgreementProject.ProgectNum;
                ndogdat = oMAgreementProject.ApprovalDate?.ToShortDateString();
                rasPripay = parseCostToString(oMAgreementProject.SizeBonusMkd);
                part = oMAgreementProject.PartMoscow?.ToString();
                partCity = parseCostToString(((oMAgreementProject.SizeBonusMkd ?? 0) * (oMAgreementProject.PartMoscow ?? 0)) / 100);               
            }

            OMParamCalculation oMParamCalculation = OMParamCalculation.Where(x => x.EmpId == ObjectId.Value).SelectAll().Execute().FirstOrDefault();

            if (oMParamCalculation != null)
            {
                if (oMParamCalculation.InsuranceId.HasValue)
                {
                    OMInsuranceOrganization oMInsuranceOrganization = OMInsuranceOrganization.Where(x => x.Id == oMParamCalculation.InsuranceId.Value).SelectAll().Execute().FirstOrDefault();
                    if (oMInsuranceOrganization != null)
                    {
                        fullName = oMInsuranceOrganization.FullName;//  Страховщик 
                    }
                }
                // subjectName = "Департамент финансов города Москвы (Государственное бюджетное учреждение города Москвы Жилищник района Раменки л/с 2691142000680799)";
                //subjectName = @"ГБУ 'Жилищник района Марьина рощаа'";
                // insurWorker = SubjectLineBreak(subjectName);
                if (oMParamCalculation.SubjectId.HasValue)
                    insurWorker = SubjectLineBreak(DictionaryService.GetSubjectById(oMParamCalculation.SubjectId.Value)?.SubjectName); //Страхователь 

                OMBuilding oMBuilding = OMBuilding.Where(x => x.EmpId == oMParamCalculation.ObjId).Select(x => x.AddressId).Execute().FirstOrDefault();

                if (oMBuilding != null)
                {
                    OMAddress oMAddress = OMAddress.Where(x => x.EmpId == oMBuilding.AddressId).Select(x => x.FullAddress).Execute().FirstOrDefault();
                    guidFiasMkd = oMAddress != null ? oMAddress.FullAddress : string.Empty;
                }

                if (ReportNumber.IsNullOrEmpty())
                {
                    ReportNumber = oMParamCalculation.PackageNum;
                }
            }

            dataTable.Rows.Add(
                    guidFiasMkd,
                    fullName,
                    ndog,
                    ndogdat,
                    rasPripay,
                    partCity,
                    part,
                    insurWorker,                    
                    sign,
                    department
                    );

            return dataTable;
        }

        private string parseCostToString(decimal? cost)
        {
            string costToString = "0 руб.";
            string[] splitSum;

            splitSum = cost.HasValue ? cost.Value.ToString("n2").Replace(".", ", ").Split(",") : null;
            costToString = splitSum != null ? string.Format("{0} руб. {1} коп.", splitSum[0], splitSum[1] != null ? splitSum[1] : "00") : null;

            return costToString;
        }

        public string SubjectLineBreak(string subjectName)
        {            
            if (subjectName.Length > 0)
            {
                
                Regex regular = new Regex(".{0,25}(?=\\s|$)", RegexOptions.Singleline);
                MatchCollection matches = regular.Matches(subjectName.Trim());
                subjectName = matches[0].ToString().Trim();
                for (int i = 1; i < matches.Count - 1; i++)
                {
                    if (matches[i].ToString().IndexOf("л/с") > -1)
                    {
                        subjectName += matches[i].ToString().Trim().IndexOf("л/с") == 0
                            ? matches[i].ToString().Trim().Replace("л/с", Environment.NewLine + "л/с")
                            : Environment.NewLine + matches[i].ToString().Trim().Replace(" л/с", Environment.NewLine + "л/с");
                        subjectName += matches[i + 1].ToString();
                        i += 1;
                    }
                    else
                        subjectName += Environment.NewLine + matches[i].ToString().Trim();
                }
            }
            return subjectName;
        }
    }
}