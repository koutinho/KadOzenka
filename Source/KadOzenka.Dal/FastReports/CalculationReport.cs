using Core.Shared.Extensions;
using ObjectModel.Core.SRD;
using ObjectModel.Insur;
using Platform.Reports;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;

namespace CIPJS.DAL.FastReports
{
    public class CalculationReport : FastReportBase
    {
        private string ReportNumber { get; set; }

        protected override string TemplateName(NameValueCollection query)
        {
            if (GetQueryParam<bool>("PagesThree", query))
                return "CalculationPagesReport";
            else
                return "CalculationReport";
        }

        public override string GetTitle(long? objectId)
        {
            long? unom;
            OMParamCalculation oMParamCalculation = OMParamCalculation.Where(x => x.EmpId == objectId.Value).SelectAll().Execute().FirstOrDefault();
            if (oMParamCalculation != null)
            {
                OMBuilding oMBuilding = OMBuilding.Where(x => x.EmpId == oMParamCalculation.ObjId).SelectAll().Execute().FirstOrDefault();
                if (oMBuilding != null)
                {
                    unom = oMBuilding.Unom;
                    return string.Format("UNOM {0} {1}", unom, ReportType.Title);
                }
            }
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
            #region variables
            DataTable dataTable = new DataTable("Common");
            dataTable.Columns.Add("NAMEORG");
            dataTable.Columns.Add("DATE");
            dataTable.Columns.Add("UNOM");
            dataTable.Columns.Add("ADDRESS");
            dataTable.Columns.Add("YEARSTROI");
            dataTable.Columns.Add("COUNTFLOOR");
            dataTable.Columns.Add("KOLGP");
            dataTable.Columns.Add("LFPQ");
            dataTable.Columns.Add("LFGPQ");
            dataTable.Columns.Add("OPL");
            dataTable.Columns.Add("CALCULATEDAREA");
            dataTable.Columns.Add("ACTUALCOST");
            dataTable.Columns.Add("COEFACTUALCOST");
            dataTable.Columns.Add("UI1");
            dataTable.Columns.Add("UI2");
            dataTable.Columns.Add("UI3");
            dataTable.Columns.Add("UI4");
            dataTable.Columns.Add("UI5");
            dataTable.Columns.Add("UI6");
            dataTable.Columns.Add("UI7");
            dataTable.Columns.Add("UI8");
            dataTable.Columns.Add("UI9");
            dataTable.Columns.Add("UI10");
            dataTable.Columns.Add("UI11");
            dataTable.Columns.Add("INSURPERSENT");
            dataTable.Columns.Add("BASEINSURPERSENT1");
            dataTable.Columns.Add("BASEINSURPERSENT2");
            dataTable.Columns.Add("BASEINSURPERSENT3");
            dataTable.Columns.Add("YEARINSURBUILDING");
            dataTable.Columns.Add("ACTUALCOST_COEF");
            dataTable.Columns.Add("AREA_OPL");
            dataTable.Columns.Add("COST_COEF_UI1");
            dataTable.Columns.Add("COST_COEF_UI2");
            dataTable.Columns.Add("COST_COEF_UI3");
            dataTable.Columns.Add("COST_COEF_UI4");
            dataTable.Columns.Add("COST_COEF_UI5");
            dataTable.Columns.Add("COST_COEF_UI6");
            dataTable.Columns.Add("COST_COEF_UI7");
            dataTable.Columns.Add("COST_COEF_UI8");
            dataTable.Columns.Add("COST_COEF_UI9");
            dataTable.Columns.Add("COST_COEF_UI10");
            dataTable.Columns.Add("COST_COEF_UI11");
            dataTable.Columns.Add("CIM1");
            dataTable.Columns.Add("CIM2");
            dataTable.Columns.Add("CIM3");
            dataTable.Columns.Add("CIM4");
            dataTable.Columns.Add("CIM5");
            dataTable.Columns.Add("CIM6");
            dataTable.Columns.Add("CIM7");
            dataTable.Columns.Add("CIM8");
            dataTable.Columns.Add("CIM9");
            dataTable.Columns.Add("CIM10");
            dataTable.Columns.Add("CIM11");
            dataTable.Columns.Add("CIMFULL");
            dataTable.Columns.Add("INSSUMKOSTROBSHZONI");
            dataTable.Columns.Add("ITOGOOBSHZONI");
            dataTable.Columns.Add("INSSUMSANITARNTEHN");
            dataTable.Columns.Add("YEARINSURSANITTEHN");
            dataTable.Columns.Add("INSSUMLIFT");
            dataTable.Columns.Add("YEARINSURLIFT");
            dataTable.Columns.Add("PHONE");

            string unom = string.Empty,
                address = string.Empty,
                orgName = string.Empty,
                dateFill2 = string.Empty,
                phone = string.Empty;

            decimal? yearStroi = null,
                countFloor = null,
                kolGp = null,
                lfpq = null,
                lfgpq = null,
                opl = 0,
                calculatedArea = 0,
                actualCost = 0,
                coefActualCost = 0,
                ui1 = 0,
                ui2 = 0,
                ui3 = 0,
                ui4 = 0,
                ui5 = 0,
                ui6 = 0,
                ui7 = 0,
                ui8 = 0,
                ui9 = 0,
                ui10 = 0,
                ui11 = 0,
                insurPersent = 0,
                yearInsurBuilding = 0,
                baseInsurPersent1 = 0,
                baseInsurPersent2 = 0,
                baseInsurPersent3 = 0,
                actualCost_coef = 0,
                area_opl = 0,
                cost_coef_ui1 = 0,
                cost_coef_ui2 = 0,
                cost_coef_ui3 = 0,
                cost_coef_ui4 = 0,
                cost_coef_ui5 = 0,
                cost_coef_ui6 = 0,
                cost_coef_ui7 = 0,
                cost_coef_ui8 = 0,
                cost_coef_ui9 = 0,
                cost_coef_ui10 = 0,
                cost_coef_ui11 = 0,
                cim1 = 0,
                cim2 = 0,
                cim3 = 0,
                cim4 = 0,
                cim5 = 0,
                cim6 = 0,
                cim7 = 0,
                cim8 = 0,
                cim9 = 0,
                cim10 = 0,
                cim11 = 0,
                cimFull = 0,
                insSumKostrObshZoni = 0,
                itogoObshZoni = 0,
                insSumSanitarnTehn = 0,
                yearInsurSanitTehn = 0,
                insSumLift = 0,
                yearInsurLift = 0;
            #endregion

            OMParamCalculation oMParamCalculation = OMParamCalculation.Where(x => x.EmpId == ObjectId.Value).SelectAll().Execute().FirstOrDefault();

            if (oMParamCalculation != null)
            {
                if (oMParamCalculation.InsuranceId.HasValue)
                {
                    OMInsuranceOrganization oMInsuranceOrganization = OMInsuranceOrganization.Where(x => x.Id == oMParamCalculation.InsuranceId.Value).SelectAll().Execute().FirstOrDefault();
                    if (oMInsuranceOrganization != null)
                    {
                        orgName = oMInsuranceOrganization.FullName;
                    }
                }

                OMUser user = OMUser.Where(x => x.Id == oMParamCalculation.AgreementId1).Select(x => x.Phone).Execute().FirstOrDefault();
                phone = user?.Phone;

                dateFill2 = oMParamCalculation.DateFill2?.ToShortDateString();
                calculatedArea = oMParamCalculation.CalculatedArea;
                actualCost = oMParamCalculation.ActualCost;
                coefActualCost = oMParamCalculation.CoefActualCost;
                ui1 = oMParamCalculation.Ui1;
                ui2 = oMParamCalculation.Ui2;
                ui3 = oMParamCalculation.Ui3;
                ui4 = oMParamCalculation.Ui4;
                ui5 = oMParamCalculation.Ui5;
                ui6 = oMParamCalculation.Ui6;
                ui7 = oMParamCalculation.Ui7;
                ui8 = oMParamCalculation.Ui8;
                ui9 = oMParamCalculation.Ui9;
                ui10 = oMParamCalculation.Ui10;
                ui11 = oMParamCalculation.Ui11;

                OMBuilding oMBuilding = OMBuilding.Where(x => x.EmpId == oMParamCalculation.ObjId).SelectAll().Execute().FirstOrDefault();

                if (oMBuilding != null)
                {             
                    OMAddress oMAddress = OMAddress.Where(x => x.EmpId == oMBuilding.AddressId).Select(x => x.FullAddress).Execute().FirstOrDefault();
                    address = oMAddress != null ? oMAddress.FullAddress : string.Empty;

                    unom = oMBuilding.Unom.ToString();
                    yearStroi = oMBuilding.YearStroi;
                    countFloor = oMBuilding.CountFloor;
                    kolGp = oMBuilding.KolGp;
                    lfpq = oMBuilding.Lfpq;
                    lfgpq = oMBuilding.Lfgpq;
                    opl = (oMBuilding.Opl ?? 0) + (oMBuilding.Bpl ?? 0) + (oMBuilding.Lpl ?? 0);
                        //OMPremase.Where(x => x.ParentFloor.BuildingId == oMParamCalculation.ObjId).Select(x => x.TotalAreaWithSummer).Execute().Sum(x => x.TotalAreaWithSummer);
                }
                opl = oMParamCalculation.AllArea ?? opl;
                actualCost_coef = oMParamCalculation.ActualCostCurrent;
                area_opl = oMParamCalculation.IndicatorR;
                insurPersent = oMParamCalculation.PartСоmpensation;
                baseInsurPersent1 = oMParamCalculation.BasicRate1;
                baseInsurPersent2 = oMParamCalculation.BasicRate2;
                baseInsurPersent3 = oMParamCalculation.BasicRate3;

                //Стоимость констр. по зданию, Ci=Cб*Ui
                cost_coef_ui1 = Math.Round((actualCost_coef * ui1 / 100).ParseToDouble(), 2, MidpointRounding.AwayFromZero).ParseToDecimal();
                cost_coef_ui2 = Math.Round((actualCost_coef * ui2 / 100).ParseToDouble(), 2, MidpointRounding.AwayFromZero).ParseToDecimal();
                cost_coef_ui3 = Math.Round((actualCost_coef * ui3 / 100).ParseToDouble(), 2, MidpointRounding.AwayFromZero).ParseToDecimal();
                cost_coef_ui4 = Math.Round((actualCost_coef * ui4 / 100).ParseToDouble(), 2, MidpointRounding.AwayFromZero).ParseToDecimal();
                cost_coef_ui5 = Math.Round((actualCost_coef * ui5 / 100).ParseToDouble(), 2, MidpointRounding.AwayFromZero).ParseToDecimal();
                cost_coef_ui6 = Math.Round((actualCost_coef * ui6 / 100).ParseToDouble(), 2, MidpointRounding.AwayFromZero).ParseToDecimal();
                cost_coef_ui7 = Math.Round((actualCost_coef * ui7 / 100).ParseToDouble(), 2, MidpointRounding.AwayFromZero).ParseToDecimal();
                cost_coef_ui8 = Math.Round((actualCost_coef * ui8 / 100).ParseToDouble(), 2, MidpointRounding.AwayFromZero).ParseToDecimal();
                cost_coef_ui9 = Math.Round((actualCost_coef * ui9 / 100).ParseToDouble(), 2, MidpointRounding.AwayFromZero).ParseToDecimal();
                cost_coef_ui10 = Math.Round((actualCost_coef * ui10 / 100).ParseToDouble(), 2, MidpointRounding.AwayFromZero).ParseToDecimal();
                cost_coef_ui11 = Math.Round((actualCost_coef * ui11 / 100).ParseToDouble(), 2, MidpointRounding.AwayFromZero).ParseToDecimal();

                //Стоим. констр. общей зоны, Cim=Ci*R,
                cim1 = Math.Round((cost_coef_ui1 * area_opl).ParseToDouble(), 2, MidpointRounding.AwayFromZero).ParseToDecimal();
                cim2 = Math.Round((cost_coef_ui2 * area_opl).ParseToDouble(), 2, MidpointRounding.AwayFromZero).ParseToDecimal();
                cim3 = Math.Round((cost_coef_ui3 * area_opl).ParseToDouble(), 2, MidpointRounding.AwayFromZero).ParseToDecimal();
                cim4 = Math.Round((cost_coef_ui4 * area_opl).ParseToDouble(), 2, MidpointRounding.AwayFromZero).ParseToDecimal();
                cim5 = Math.Round((cost_coef_ui5 * area_opl).ParseToDouble(), 2, MidpointRounding.AwayFromZero).ParseToDecimal();
                cim6 = Math.Round((cost_coef_ui6 * area_opl).ParseToDouble(), 2, MidpointRounding.AwayFromZero).ParseToDecimal();
                cim7 = Math.Round((cost_coef_ui7 * area_opl).ParseToDouble(), 2, MidpointRounding.AwayFromZero).ParseToDecimal();
                cim8 = Math.Round((actualCost_coef * ui8 / 100).ParseToDouble(), 2, MidpointRounding.AwayFromZero).ParseToDecimal();
                cim9 = Math.Round((cost_coef_ui9 * area_opl).ParseToDouble(), 2, MidpointRounding.AwayFromZero).ParseToDecimal();
                cim10 = Math.Round((actualCost_coef * ui10 / 100).ParseToDouble(), 2, MidpointRounding.AwayFromZero).ParseToDecimal();
                cim11 = (cim1 ??0) + (cim2 ?? 0) + (cim3 ?? 0) + (cim4 ?? 0) + (cim5 ?? 0) + (cim6 ?? 0) + (cim7??0) + (cim8??0) + (cim9??0) + (cim10??0); //cost_coef_ui11 * area_opl;
                        
                //Общая стоимость конструкций общей зоны без санитарно-технических работ, внутридомового инженерного оборудования и лифтов:
                cimFull = oMParamCalculation.TotalCost1;

                //Страховая сумма конструкций общей зоны без санитарно-технических работ и внутридомового инженерного оборудования и лифтов:
                insSumKostrObshZoni = oMParamCalculation.DesignCost1;

                //Годовая страховая премия:
                itogoObshZoni = oMParamCalculation.AnnualBonus1;

                //Страховая сумма санитарно-технических работ и внутридомового инженерного оборудования:
                insSumSanitarnTehn = oMParamCalculation.DesignCost2; //cim9 * insurPersent;

                //Годовая страховая премия:
                yearInsurSanitTehn = oMParamCalculation.AnnualBonus2;

                //Страховая сумма лифтов и лифтового оборудования:
                insSumLift = oMParamCalculation.DesignCost3; //cim10 * insurPersent; 

                //Годовая страховая премия:
                yearInsurLift = oMParamCalculation.AnnualBonus3;

                //Годовая страховая премия по дому:
                yearInsurBuilding = oMParamCalculation.SizeAnnualBonus;

                if (ReportNumber.IsNullOrEmpty())
                {
                    ReportNumber = oMParamCalculation.PackageNum;
                }
            }

            dataTable.Rows.Add(
            orgName,
            dateFill2,
            unom,
            address,
            yearStroi,
            countFloor,
            kolGp,
            lfpq?.ToString("n0"),
            lfgpq?.ToString("n0"),
            (opl ?? 0).ToString("n2"),
            (calculatedArea ?? 0).ToString("n2"),
            (actualCost ?? 0).ToString("n2"),
            (coefActualCost ?? 0).ToString("n2"),
            (ui1 ?? 0).ToString("n2") + "%",
            (ui2 ?? 0).ToString("n2") + "%",
            (ui3 ?? 0).ToString("n2") + "%",
            (ui4 ?? 0).ToString("n2") + "%",
            (ui5 ?? 0).ToString("n2") + "%",
            (ui6 ?? 0).ToString("n2") + "%",
            (ui7 ?? 0).ToString("n2") + "%",
            (ui8 ?? 0).ToString("n2") + "%",
            (ui9 ?? 0).ToString("n2") + "%",
            (ui10 ?? 0).ToString("n2") + "%",
            (ui11 ?? 0).ToString("n2") + "%",
            (insurPersent * 100 ?? 0).ToString("n2") + "%",
            baseInsurPersent1?.ToString("n2") + "%",
            baseInsurPersent2?.ToString("n2") + "%",
            baseInsurPersent3?.ToString("n2") + "%",
            (yearInsurBuilding ?? 0).ToString("n2"),
            (actualCost_coef ?? 0).ToString("n2"),
            (area_opl ?? 0).ToString("n2"),
            (cost_coef_ui1 ?? 0).ToString("n2"),
            (cost_coef_ui2 ?? 0).ToString("n2"),
            (cost_coef_ui3 ?? 0).ToString("n2"),
            (cost_coef_ui4 ?? 0).ToString("n2"),
            (cost_coef_ui5 ?? 0).ToString("n2"),
            (cost_coef_ui6 ?? 0).ToString("n2"),
            (cost_coef_ui7 ?? 0).ToString("n2"),
            (cost_coef_ui8 ?? 0).ToString("n2"),
            (cost_coef_ui9 ?? 0).ToString("n2"),
            (cost_coef_ui10 ?? 0).ToString("n2"),
            (cost_coef_ui11 ?? 0).ToString("n2"),
            (cim1 ?? 0).ToString("n2"),
            (cim2 ?? 0).ToString("n2"),
            (cim3 ?? 0).ToString("n2"),
            (cim4 ?? 0).ToString("n2"),
            (cim5 ?? 0).ToString("n2"),
            (cim6 ?? 0).ToString("n2"),
            (cim7 ?? 0).ToString("n2"),
            (cim8 ?? 0).ToString("n2"),
            (cim9 ?? 0).ToString("n2"),
            (cim10 ?? 0).ToString("n2"),
            (cim11 ?? 0).ToString("n2"),
            (cimFull ?? 0).ToString("n2"),
            (insSumKostrObshZoni ?? 0).ToString("n2"),
            (itogoObshZoni ?? 0).ToString("n2"),
            (insSumSanitarnTehn ?? 0).ToString("n2"),
            (yearInsurSanitTehn ?? 0).ToString("n2"),
            (insSumLift ?? 0).ToString("n2"),
            (yearInsurLift ?? 0).ToString("n2"),
            phone
            );
            return dataTable;
        }
    }
}