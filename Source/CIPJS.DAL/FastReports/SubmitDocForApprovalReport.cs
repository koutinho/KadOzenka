using CIPJS.DAL.Dictionaries;
using Core.Shared.Extensions;
using Core.SRD;
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
    public class SubmitDocForApprovalReport : FastReportBase
    {
        private string ReportNumber { get; set; }

        protected override string TemplateName(NameValueCollection query) { { return "SubmitDocForApprovalReport"; } }

        public override string GetTitle(long? objectId)
        {
            return ReportType.Title;
        }

        protected override DataSet GetData(NameValueCollection query, HashSet<long> objectList = null)
        {
            DataSet dataSet = new DataSet();
            dataSet.Tables.Add(GetCommonDataTable(query));

            return dataSet;
        }

        public override void SetReportNumber(ref string reportNumber, DateTime reportDate, NameValueCollection query)
        {
            reportNumber = ReportNumber;
        }

        private DataTable GetCommonDataTable(NameValueCollection query)
        {
            DataTable dataTable = new DataTable("Common");
            dataTable.Columns.Add("UNOM");
            dataTable.Columns.Add("NUMDOC");
            dataTable.Columns.Add("ADDRESS_FULL");
            dataTable.Columns.Add("INSURANCE");
            dataTable.Columns.Add("TOTAL_COST_1");
            dataTable.Columns.Add("TOTAL_COST_2");
            dataTable.Columns.Add("TOTAL_COST_3");
            dataTable.Columns.Add("DESIGN_COST_1");
            dataTable.Columns.Add("DESIGN_COST_2");
            dataTable.Columns.Add("DESIGN_COST_3");
            dataTable.Columns.Add("SIZE_ANNUAL_BONUS");
            dataTable.Columns.Add("PARTMOSCOW");
            dataTable.Columns.Add("INSPREMIUMPART");
            dataTable.Columns.Add("COMMENT_SPRAVKA");
            dataTable.Columns.Add("EXECUTOR");
            dataTable.Columns.Add("PODPISANTDEP");
            dataTable.Columns.Add("PODPISANTFIO");
            dataTable.Columns.Add("DATE");
            dataTable.Columns.Add("RESUME_SPRAVKA");
            dataTable.Columns.Add("NAME");
            dataTable.Columns.Add("INSURNUM");
            dataTable.Columns.Add("INSURDATE"); 

            string podpisant = GetQueryParam<string>("Podpisant", query);
            bool noSign = GetQueryParam<bool>("NoSign", query);
            string[] sep = podpisant.Split('-');
            string sign = !noSign ? "/" + sep[0].ToString().Trim() + "/" : "/                       /",
                   department = !noSign 
                   ? sep[1].ToString().Trim()
                   .Replace(" ГБУ", Environment.NewLine + "ГБУ")
                   .Replace(" и жилищного", Environment.NewLine + "и жилищного")
                   .Replace(" перспективного", Environment.NewLine + "перспективного")
                   : string.Empty;

            int userId = SRDSession.Current.User.ID;
            string user = SRDCacheUser.GetUser(userId).FullNameForDoc;
            decimal dPartCity = 0;

            string unom = string.Empty,
                   addressFiasMkd = string.Empty,
                   insuranceId = string.Empty,
                   totalCost1 = string.Empty,
                   totalCost2 = string.Empty,
                   totalCost3 = string.Empty,
                   designCost1 = string.Empty,
                   designCost2 = string.Empty,
                   designCost3 = string.Empty,
                   sizeAnnualBonus = string.Empty,
                   persentAnnualBonus = string.Empty,
                   commentSpravka = string.Empty,
                   resumeSpravka = string.Empty,
                   fullName = string.Empty,
                   partCity = string.Empty,
                   name = string.Empty,
                   numDoc = string.Empty,
                   insurNum = string.Empty,
                   insurDate = string.Empty;
            bool kat1 = false,
                 kat2 = false,
                 kat3 = false;

            OMAgreementProject oMAgreementProject = OMAgreementProject.Where(x => x.CalculationId == ObjectId).SelectAll().Execute().FirstOrDefault();
            if (oMAgreementProject != null)
            {
                commentSpravka = oMAgreementProject.CommentSpravka;
                resumeSpravka = oMAgreementProject.ResumeSpravka;
                partCity = oMAgreementProject.PartMoscow.HasValue ? oMAgreementProject.PartMoscow.Value.ToString() : string.Empty;
                dPartCity = oMAgreementProject.PartMoscow ?? 0;
                sizeAnnualBonus = parseCostToString(oMAgreementProject.SizeBonusMkd);
                //decimal? dPersentAnnualBonus = oMParamCalculation.SizeAnnualBonus.HasValue ? (oMParamCalculation.SizeAnnualBonus * dPartCity / 100) : null;
                //persentAnnualBonus = parseCostToString(dPersentAnnualBonus);
                kat1 = oMAgreementProject.Kat1 ?? false;
                kat2 = oMAgreementProject.Kat2 ?? false;
                kat3 = oMAgreementProject.Kat3 ?? false;

                insurNum = oMAgreementProject.ProgectNum;
                insurDate = oMAgreementProject.ApprovalDate?.ToShortDateString();
            }

            OMParamCalculation oMParamCalculation = OMParamCalculation.Where(x => x.EmpId == ObjectId.Value).SelectAll().Execute().FirstOrDefault();

            if (oMParamCalculation != null)
            {
                OMBuilding oMBuilding = OMBuilding.Where(x => x.EmpId == oMParamCalculation.ObjId)
                   .Select(x => x.Unom)
                   .Select(x => x.AddressId)
                   .Select(x => x.EmpId)
                   .Execute().FirstOrDefault();

                if (oMBuilding != null)
                {
                    unom = oMBuilding.Unom.ToString();
                    OMAddress oMAddress = OMAddress.Where(x => x.EmpId == oMBuilding.AddressId).Select(x => x.FullAddress).Execute().FirstOrDefault();
                    addressFiasMkd = oMAddress != null ? oMAddress.FullAddress : string.Empty;
                }

                if (oMParamCalculation.SubjectId.HasValue)
                    name = SubjectNameLineBreak(DictionaryService.GetSubjectById(oMParamCalculation.SubjectId.Value)?.SubjectName); //Страхователь 

                if (oMParamCalculation.InsuranceId.HasValue)
                {
                    OMInsuranceOrganization oMInsuranceOrganization = OMInsuranceOrganization.Where(x => x.Id == oMParamCalculation.InsuranceId.Value).SelectAll().Execute().FirstOrDefault();
                    if (oMInsuranceOrganization != null)
                    {
                        fullName = oMInsuranceOrganization.FullName;  //  Страховщик
                    }
                }

                insuranceId = fullName;
                if (kat1)
                {
                    totalCost1 = oMParamCalculation.TotalCost1.HasValue ? oMParamCalculation.TotalCost1.Value.ToString("N2") : "0";
                    designCost1 = oMParamCalculation.DesignCost1.HasValue ? oMParamCalculation.DesignCost1.Value.ToString("N2") : "0";
                }
                if (kat2)
                {
                    designCost2 = oMParamCalculation.DesignCost2.HasValue ? oMParamCalculation.DesignCost2.Value.ToString("N2") : "0";
                    totalCost2 = oMParamCalculation.TotalCost2.HasValue ? oMParamCalculation.TotalCost2.Value.ToString("N2") : "0";
                }
                if (kat3)
                {
                    totalCost3 = oMParamCalculation.TotalCost3.HasValue ? oMParamCalculation.TotalCost3.Value.ToString("N2") : "0";
                    designCost3 = oMParamCalculation.DesignCost3.HasValue ? oMParamCalculation.DesignCost3.Value.ToString("N2") : "0";
                }
                numDoc = oMParamCalculation.PackageNum;

                if (ReportNumber.IsNullOrEmpty())
                {
                    ReportNumber = numDoc;
                }
            }

            dataTable.Rows.Add(unom,
                numDoc,
                addressFiasMkd,
                insuranceId,
                totalCost1,
                totalCost2,
                totalCost3,
                designCost1,
                designCost2,
                designCost3,
                sizeAnnualBonus,
                partCity,
                persentAnnualBonus,
                commentSpravka,
                user,
                department,
                sign,
                DateTime.Today.ToShortDateString(),
                resumeSpravka,
                name,
                insurNum,
                insurDate
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

        public string SubjectNameLineBreak(string subjectName)
        {
            if (subjectName.Length > 93)
            {
                Regex regular = new Regex(".{0,93}(?=\\s|$)", RegexOptions.Singleline);
                MatchCollection matches = regular.Matches(subjectName.Trim());
                subjectName = "";
                for (int i = 0; i < matches.Count - 1; i++)
                {
                    if (matches[i].ToString().IndexOf("л/с") > 73)
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