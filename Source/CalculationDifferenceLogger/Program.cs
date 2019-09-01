using CIPJS.DAL.Building;
using CIPJS.DAL.Calculation;
using CIPJS.DAL.Dictionaries;
using ObjectModel.Bti;
using ObjectModel.Ehd;
using ObjectModel.Insur;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CalculationDifferenceLogger
{
    class Program
    {
        static void Main(string[] args)
        {
            StringBuilder sb = new StringBuilder();
            CalculationService calculationService = new CalculationService();
            BuildingService _buildingService = new BuildingService();
            long rowCount = 1;

            List<OMParamCalculation> calculations = OMParamCalculation.Where(x => true).SelectAll().Execute();
            foreach (OMParamCalculation calculation in calculations)
            {
                OMAgreementProject project = calculationService.GetAgreementProjectsByCalculationId(calculation.EmpId);

                long? agreementId = null;
                decimal? partMoscow = null;
                decimal? bpl = null;
                long? kolGp = null;
                decimal? krovpl = null;
                decimal? lpl = null;
                decimal? opl = null;
                decimal? oplBti = null;
                decimal? oplEgrn = null;
                decimal? pizn = null;
                decimal? stroiPrice = null;

                if (project != null)
                {
                    agreementId = project.EmpId;
                    partMoscow = project.PartMoscow;
                }
                else
                {
                    if (calculation.RequestDate.HasValue)
                        partMoscow = DictionaryService.GetPartCompensationCity(calculation.RequestDate.Value);
                }

                if (calculation.ObjId.HasValue)
                {
                    OMBuilding building = _buildingService.GetById(calculation.ObjId.Value);

                    if (building != null)
                    {
                        opl = building.Opl;
                        kolGp = building.KolGp;
                        bpl = building.Bpl;
                        lpl = building.Lpl;
                        krovpl = building.Krovpl;
                        stroiPrice = building.StroiPrice;
                        pizn = building.Pizn;

                        oplBti = OMBtiBuilding.Where(x => x.LinkBuildBti[0].IdInsurBuild == building.EmpId)
                            .Select(x => x.Opl).ExecuteFirstOrDefault()?.Opl;

                        if (building.LinkEgrnBild.HasValue)
                        {
                           oplEgrn = OMBuildParcel.Where(x => x.EmpId == building.LinkEgrnBild.Value).Select(x => x.Area).ExecuteFirstOrDefault()?.Area;
                        }
                    }
                }

                CalculationValuesInDto calculationValuesInDto = new CalculationValuesInDto
                {
                    AgreementId = agreementId,
                    AgreementPartMoscow = partMoscow,
                    BasicRate1 = calculation.BasicRate1,
                    BasicRate2 = calculation.BasicRate2,
                    BasicRate3 = calculation.BasicRate3,
                    Bpl = bpl,
                    CoefActualCost = calculation.CoefActualCost,
                    FlagOkrugl = calculation.FlagOkrugl,
                    KolGp = kolGp,
                    Krovpl = krovpl,
                    Lpl = lpl,
                    Opl = opl,
                    OplBti = oplBti,
                    OplEgrn = oplEgrn,
                    PartCompensation = calculation.PartСоmpensation,
                    Pizn = pizn,
                    RecalcSizeBonusMkd = true,
                    StroiPrice = stroiPrice,
                    Ui1 = calculation.Ui1,
                    Ui2 = calculation.Ui2,
                    Ui3 = calculation.Ui3,
                    Ui4 = calculation.Ui4,
                    Ui5 = calculation.Ui5,
                    Ui6 = calculation.Ui6,
                    Ui7 = calculation.Ui7,
                    Ui8 = calculation.Ui8,
                    Ui9 = calculation.Ui9,
                    Ui10 = calculation.Ui10
                };

                CalculationValuesOutDto calculationValuesOut = calculationService.Calculate(calculationValuesInDto);

                bool hasDifference = false;
                sb.AppendLine($"{rowCount++}. Расчет номер {(!string.IsNullOrEmpty(calculation.PackageNum) ? calculation.PackageNum : "(-)")}. Идентификатор {calculation.EmpId}");

                WriteDifferenceToLog(sb, "Расхождение действительной стоимости дома (ActualCost)", 
                    calculationValuesOut.ActualCost, calculation.ActualCost, ref hasDifference);
                WriteDifferenceToLog(sb, "Действительная стоимость дома (в пересчете на текущую цену) (ActualCostCurrent)",
                    calculationValuesOut.ActualCostCurrent, calculation.ActualCostCurrent, ref hasDifference);
                WriteDifferenceToLog(sb, "Годовая премия первой категории (AnnualBonus1)",
                    calculationValuesOut.AnnualBonus1Category, calculation.AnnualBonus1, ref hasDifference);
                WriteDifferenceToLog(sb, "Годовая премия второй категории (AnnualBonus2)",
                    calculationValuesOut.AnnualBonus2Category, calculation.AnnualBonus2, ref hasDifference);
                WriteDifferenceToLog(sb, "Годовая премия третьей категории (AnnualBonus3)",
                    calculationValuesOut.AnnualBonus3Category, calculation.AnnualBonus3, ref hasDifference);
                WriteDifferenceToLog(sb, "Расчетная площадь для определения стоимости общего имущества в МКД (CalculatedArea)",
                    calculationValuesOut.CalculatedArea, calculation.CalculatedArea, ref hasDifference);
                WriteDifferenceToLog(sb, "Сумма конструкций без санитарно-технических работ и внутридомового инженерного оборудования (DesignCost1)",
                    calculationValuesOut.DesignCost1Category, calculation.DesignCost1, ref hasDifference);
                WriteDifferenceToLog(sb, "Сумма конструкций по санитарно-техническим работам и внутридомовому инженерному оборудованию (DesignCost2)",
                    calculationValuesOut.DesignCost2Category, calculation.DesignCost2, ref hasDifference);
                WriteDifferenceToLog(sb, "Сумма конструкций лифтов и лифтового оборудования (DesignCost3)",
                    calculationValuesOut.DesignCost3Category, calculation.DesignCost3, ref hasDifference);
                WriteDifferenceToLog(sb, "Показатель рациональности объемно-планировочного решения, R (IndicatorR)",
                    calculationValuesOut.IndicatorR, calculation.IndicatorR, ref hasDifference);
                WriteDifferenceToLog(sb, "Общая площадь по зданию (Oplc)",
                    calculationValuesOut.Oplc, calculation.AllArea, ref hasDifference);
                WriteDifferenceToLog(sb, "Размер годовой премии по дому (SizeAnnualBonus)",
                    calculationValuesOut.SizeAnnualBonus, calculation.SizeAnnualBonus, ref hasDifference);
                WriteDifferenceToLog(sb, "Размер годовой страховой премии по дому (SizeBonusMkd)",
                    calculationValuesOut.SizeBonusMkd, project?.SizeBonusMkd, ref hasDifference);
                WriteDifferenceToLog(sb, "Общая стоимость конструкций без санитарно-технических работ и внутридомового инженерного оборудования (TotalCost1)",
                    calculationValuesOut.TotalCost1Category, calculation.TotalCost1, ref hasDifference);
                WriteDifferenceToLog(sb, "Общая стоимость конструкций по санитарно-техническим работам и внутридомовому инженерному оборудованию (TotalCost2)",
                    calculationValuesOut.TotalCost2Category, calculation.TotalCost2, ref hasDifference);
                WriteDifferenceToLog(sb, "Общая стоимость конструкций лифтов и лифтового оборудования (TotalCost3)",
                    calculationValuesOut.TotalCost3Category, calculation.TotalCost3, ref hasDifference);
                WriteDifferenceToLog(sb, "Общий удельный вес конструкции (Ui11)",
                    calculationValuesOut.Ui11, calculation.Ui11, ref hasDifference);

                if (!hasDifference)
                {
                    sb.AppendLine("Расхождений не найдено");
                }
            }

            File.AppendAllText($"log-{DateTime.Now:yyyyMMdd-HHmmss}.txt", sb.ToString());
        }

        private static void WriteDifferenceToLog(StringBuilder sb, string fieldName, decimal? calcValue, decimal? bdValue, ref bool hasDifference)
        {
            if (calcValue != bdValue)
            {
                hasDifference = true;
                sb.AppendLine($"{fieldName}. " +
                    $"Значение в БД: {(bdValue.HasValue ? bdValue.Value : 0m):N2}. " +
                    $"Расчетное значение: {(calcValue.HasValue ? calcValue.Value : 0m):N2}. " +
                    $"Дельта: { (Math.Abs((calcValue ?? 0) - (bdValue ?? 0))):N2}");
            }
        }
    }
}
