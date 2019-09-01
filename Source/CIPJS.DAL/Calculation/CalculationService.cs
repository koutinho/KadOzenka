using Core.Numerator;
using Core.Shared.Extensions;
using Core.Shared.Misc;
using Core.SRD;
using Microsoft.Practices.EnterpriseLibrary.Data;
using ObjectModel.Directory;
using ObjectModel.Insur;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Globalization;
using System.Linq;
using System.Transactions;

namespace CIPJS.DAL.Calculation
{
    public class CalculationService
    {
        private readonly CultureInfo _cultureRu = new CultureInfo("ru-RU");
        /// <summary>
        /// Получить расчет по его идентификатору
        /// </summary>
        /// <param name="id">Идентификатор расчета</param>
        /// <returns>Расчет</returns>
        public OMParamCalculation GetById(long? id)
        {
            OMParamCalculation entity = OMParamCalculation.Where(x => x.EmpId == id).SelectAll().Execute().FirstOrDefault();

            if (entity == null) throw new Exception(string.Format("Сущность \"Расчет параметров объектов общего имущества\" с EmpId={0} не найдена", id));

            return entity;
        }

        /// <summary>
        /// Выполнение связи всех расчетов с договорами и проставление доли города.
        /// </summary>
        public string LinkСalculationToContract()
        {
            decimal linkCount = 0;
            decimal partMoscowCount = 0;
            using (TransactionScope ts = TransactionScopeWrapper.OpenTransaction())
            {

                string query = 
@"update insur_param_calculation calculationResult set contract_id = 
(select property.emp_id from insur_param_calculation calculation
inner join insur_agreement_project project on calculation.emp_id = project.calculation_id 
inner join insur_all_property property on (lower(project.progect_num) = lower(property.ndog) or 
replace(
replace(
replace(
replace(
replace(
replace(
replace(
replace(
replace(
translate(lower(project.progect_num), 
'абвгдеёзийклмнопрстуфхць', 'abvgdeezijklmnoprstufхc`'),
'ж', 'zh'),
'ч', 'ch'),
'ш', 'sh'),
'щ', 'shh'),
'ъ', '``'),
'ы', 'y`'),
'э', 'e`'),
'ю', 'yu'),
'я', 'ya') =
replace(
replace(
replace(
replace(
replace(
replace(
replace(
replace(
replace(
translate(lower(property.ndog), 
'абвгдеёзийклмнопрстуфхць', 'abvgdeezijklmnoprstufхc`'),
'ж', 'zh'),
'ч', 'ch'),
'ш', 'sh'),
'щ', 'shh'),
'ъ', '``'),
'ы', 'y`'),
'э', 'e`'),
'ю', 'yu'),
'я', 'ya')
) 
and calculation.obj_id = property.obj_id 
and project.size_bonus_mkd = property.ras_pripay
and calculation.emp_id = calculationResult.emp_id)
where calculationResult.contract_id is null";
                DbCommand command = DBMngr.Realty.GetSqlStringCommand(query);
                linkCount = DBMngr.Realty.ExecuteNonQuery(command);

                query =
 @"update insur_all_property propertyResult set part_city = 
(select project.part_moscow from insur_param_calculation calculation
inner join insur_agreement_project project on calculation.emp_id = project.calculation_id 
inner join insur_all_property property on calculation.contract_id = property.emp_id and calculation.obj_id = property.obj_id  and project.size_bonus_mkd = property.ras_pripay  and property.emp_id = propertyResult.emp_id)
where propertyResult.part_city = 0 or propertyResult.part_city is null";
                command = DBMngr.Realty.GetSqlStringCommand(query);
                partMoscowCount = DBMngr.Realty.ExecuteNonQuery(command);

                ts.Complete();
            }

            return $@" Количество связанных расчетов: {linkCount} Количество установленных долей города: {partMoscowCount}";
        }

        /// <summary>
        /// Удалить расчет
        /// </summary>
        /// <param name="id">Идентификатор расчета</param>
        public void DeleteCalculation(long? id)
        {
            OMParamCalculation entity = OMParamCalculation.Where(x => x.EmpId == id).SelectAll().Execute().FirstOrDefault();

            if (entity == null) throw new Exception(string.Format("Сущность \"Расчет параметров объектов общего имущества\" с EmpId={0} не найдена", id));

            entity.Deleted = true;
            entity.Save();
        }

        /// <summary>
        /// Получить расчет по его идентификатору
        /// </summary>
        /// <param name="id">Идентификатор проекта договора</param>
        /// <returns>Проект договора</returns>
        public OMAgreementProject GetAgreementProjectById(long? id)
        {
            OMAgreementProject entity = OMAgreementProject.Where(x => x.EmpId == id).SelectAll().Execute().FirstOrDefault();

            if (entity == null) throw new Exception(string.Format("Сущность \"\" с EmpId={0} не найдена", id));

            return entity;
        }
        
         /// <summary>
         /// Получить проект договора по идентификатору расчета
         /// </summary>
         /// <param name="id">Идентификатор проекта договора</param>
         /// <returns>Проект договора</returns>
        public OMAgreementProject GetAgreementProjectsByCalculationId(long calculationId)
        {
            return OMAgreementProject.Where(x => x.CalculationId == calculationId).SelectAll().Execute().FirstOrDefault();
        }

        /// <summary>
        /// Сохранить расчет
        /// </summary>
        /// <param name="data">Данные расчета</param>
        /// <returns></returns>
        public long Save(OMParamCalculation data)
        {
            if (data == null) throw new Exception(string.Format("Параметр \"data\" равен null"));

            return data.Save();
        }

        /// <summary>
        /// Сохранить проект договора
        /// </summary>
        /// <param name="data">Данные проекта договора</param>
        /// <returns></returns>
        public long SaveAgreementProject(OMAgreementProject data)
        {
            if (data == null) throw new Exception(string.Format("Параметр \"data\" равен null"));

            return data.Save();
        }

        public object Agreed(long calculationId)
        {
            if (calculationId <= 0)
            {
                throw new Exception("Невозможно согласовать несохраненный отчет");
            }

            OMParamCalculation paramCalculation = OMParamCalculation.Where(x => x.EmpId == calculationId)
                .Select(x => x.Status)
                .Select(x => x.Status_Code)
                .Execute()
                .FirstOrDefault();

            if (paramCalculation == null)
            {
                throw new Exception($"Не удалось определить расчет с идентификатором: {calculationId}");
            }

            paramCalculation.Status_Code = CalculationStatus.Agreed;
            paramCalculation.AgreementId2 = SRDSession.GetCurrentUserId();
            paramCalculation.DateFill2 = DateTime.Now;
            paramCalculation.Save();

            return new
            {
                Status = CalculationStatus.Agreed.GetEnumDescription(),
                StatusCode = (long) CalculationStatus.Agreed,
                InspectorPersonFIO = SRDSession.Current.User.FullName,
                InspectorPersonPost = SRDSession.Current.User.Position,
                InspectorPersonId = SRDSession.GetCurrentUserId(),
                DateFill2 = paramCalculation.DateFill2.Value.ToString("dd.MM.yyyy HH:mm", _cultureRu)
            };
        }

        public object ProjectReceived(long agreementId)
        {
            if (agreementId <= 0)
            {
                throw new Exception("Невозможно получить несохраненный проект договора");
            }

            OMAgreementProject agreementProject = OMAgreementProject.Where(x => x.EmpId == agreementId).SelectAll().Execute().FirstOrDefault();

            if (agreementProject == null)
            {
                throw new Exception($"Не удалось определить проект договора с идентификатором: {agreementId}");
            }

            if (!agreementProject.CalculationId.HasValue)
            {
                throw new Exception("Невозможно согласовать проект договора, т.к. для него не заполнен идентификатор расчета");
            }

            OMParamCalculation paramCalculation = OMParamCalculation.Where(x => x.EmpId == agreementProject.CalculationId.Value)
                .Select(x => x.Status)
                .Select(x => x.Status_Code)
                .Execute()
                .FirstOrDefault();

            if (paramCalculation == null)
            {
                throw new Exception($"Не удалось определить расчет с идентификатором: {agreementProject.CalculationId.Value}");
            }

            using (TransactionScope ts = TransactionScopeWrapper.OpenTransaction())
            {
                paramCalculation.Status_Code = CalculationStatus.ProjectAgreement;
                paramCalculation.Save();

                agreementProject.GotDate = DateTime.Now;
                agreementProject.GotUserId = SRDSession.GetCurrentUserId();
                agreementProject.Save();

                ts.Complete();
            }

            return new
            {
                Status = CalculationStatus.ProjectAgreement.GetEnumDescription(),
                StatusCode = (long) CalculationStatus.ProjectAgreement,
                AgreementGotDate = agreementProject.GotDate.Value.ToString("dd.MM.yyyy HH:mm", _cultureRu),
                AgreementGotUserName = SRDSession.Current.User.FullName,
                AgreementGotUserPost = SRDSession.Current.User.Position,
                AgreementGotUserId = SRDSession.GetCurrentUserId()
            };
        }
        
        public object ProjectAgreed(long agreementId)
        {
            if (agreementId <= 0)
            {
                throw new Exception("Невозможно согласовать несохраненный проект договора");
            }

            OMAgreementProject agreementProject = OMAgreementProject.Where(x => x.EmpId == agreementId).SelectAll().Execute().FirstOrDefault();

            if (agreementProject == null)
            {
                throw new Exception($"Не удалось определить проект договора с идентификатором: {agreementId}");
            }

            if (!agreementProject.CalculationId.HasValue)
            {
                throw new Exception("Невозможно согласовать проект договора, т.к. для него не заполнен идентификатор расчета");
            }

            OMParamCalculation paramCalculation = OMParamCalculation.Where(x => x.EmpId == agreementProject.CalculationId.Value)
                .Select(x => x.Status)
                .Select(x => x.Status_Code)
                .Execute()
                .FirstOrDefault();

            if (paramCalculation == null)
            {
                throw new Exception($"Не удалось определить расчет с идентификатором: {agreementProject.CalculationId.Value}");
            }

            using (TransactionScope ts = TransactionScopeWrapper.OpenTransaction())
            {
                paramCalculation.Status_Code = CalculationStatus.ProjectAgreed;
                paramCalculation.Save();

                agreementProject.ApprovalDate = DateTime.Now;
                agreementProject.ApprovalUserId = SRDSession.GetCurrentUserId();
                agreementProject.Save();

                ts.Complete();
            }

            return new
            {
                Status = CalculationStatus.ProjectAgreed.GetEnumDescription(),
                StatusCode = (long) CalculationStatus.ProjectAgreed,
                AgreementApprovalDate = agreementProject.ApprovalDate.Value.ToString("dd.MM.yyyy HH:mm", _cultureRu), 
                AgreementApprovalUserName = SRDSession.Current.User.FullName,
                AgreementApprovalUserPost = SRDSession.Current.User.Position,
                AgreementApprovalUserId = SRDSession.GetCurrentUserId()
            };
        }

        public void UpdateAgreementProjectCategory(long agreementId, int type, bool include)
        {
            if (agreementId <= 0)
            {
                throw new Exception("Невозможно обновить несохраненный проект договора");
            }

            OMAgreementProject agreementProject = OMAgreementProject.Where(x => x.EmpId == agreementId).SelectAll().Execute().FirstOrDefault();

            if (agreementProject == null)
            {
                throw new Exception($"Не удалось определить проект договора с идентификатором: {agreementId}");
            }

            OMParamCalculation calculation = OMParamCalculation.Where(x => x.EmpId == agreementProject.CalculationId)
                .Select(x => x.AnnualBonus1)
                .Select(x => x.AnnualBonus2)
                .Select(x => x.AnnualBonus3)
                .ExecuteFirstOrDefault();

            switch (type)
            {
                case 1:
                    agreementProject.Kat1 = include;
                    break;
                case 2:
                    agreementProject.Kat2 = include;
                    break;
                case 3:
                    agreementProject.Kat3 = include;
                    break;
                default:
                    throw new Exception("Неподдерживаемый тип операции");
            }

            agreementProject.SizeBonusMkd = 0;

            if (agreementProject.Kat1 ?? false) agreementProject.SizeBonusMkd += calculation?.AnnualBonus1 ?? 0m;
            if (agreementProject.Kat2 ?? false) agreementProject.SizeBonusMkd += calculation?.AnnualBonus2 ?? 0m;
            if (agreementProject.Kat3 ?? false) agreementProject.SizeBonusMkd += calculation?.AnnualBonus3 ?? 0m;

            agreementProject.Save();
        }

        public void UpdateAgreementSizeBonusMkd(long agreementId, decimal? sizeBonusMkd)
        {
            if (agreementId <= 0)
            {
                throw new Exception("Невозможно обновить несохраненный проект договора");
            }

            OMAgreementProject agreementProject = OMAgreementProject.Where(x => x.EmpId == agreementId).Select(x => x.SizeBonusMkd).Execute().FirstOrDefault();
            agreementProject.SizeBonusMkd = sizeBonusMkd;
            agreementProject.Save();
        }

        /// <summary>
        /// Получение связи между расчетом и договором
        /// </summary>
        /// <param name="id">id расчета</param>
        /// <returns></returns>
        public CalculationContractLinkDto GetLink(long? id)
        {
            CalculationContractLinkDto model = new CalculationContractLinkDto
            {
                CalculationId = id
            };
            if (id.HasValue)
            {
                OMParamCalculation calc = OMParamCalculation
                    .Where(x => x.EmpId == id)
                    .Select(x => x.ContractId)
                    .Select(x => x.ParentAllProperty.Ndog)
                    .Select(x => x.ParentAllProperty.Ndogdat)
                    .Execute().FirstOrDefault();
                if(calc != null && calc.ContractId.HasValue)
                {
                    model.ContractId = calc.ContractId;
                    OMAllProperty prop = OMAllProperty.Where(x => x.EmpId == calc.ContractId).Select(x => x.Ndog).Select(x => x.Ndogdat).Execute().FirstOrDefault();
                    model.ContractNumerAndDate = string.Format("{0} от {1}", prop?.Ndog, prop?.Ndogdat?.ToShortDateString());
                }
            }

            return model;
        }

        public void SaveLink(CalculationContractLinkDto model)
        {
            if (model.CalculationId.HasValue)
            {
                OMParamCalculation calc = OMParamCalculation.Where(x => x.EmpId == model.CalculationId).SelectAll(false).Execute().FirstOrDefault();
                if (calc != null)
                {
                    calc.ContractId = model.ContractId;
                    calc.Status_Code = CalculationStatus.ContractConcluded;
                    calc.Save();
                }
            }
        }

        public string GetNumber(long buildingId)
        {
            OMBuilding omBuilding = OMBuilding.Where(x => x.EmpId == buildingId)
                .Select(x => x.ParentBtiOkrug.CodeGivc).ExecuteFirstOrDefault();

            if (omBuilding == null) return string.Empty;

            string inputXml = $"<root>" +
                $"<Okrug>{omBuilding.ParentBtiOkrug?.CodeGivc ?? "0000"}</Okrug>" +
                $"<Year>{DateTime.Now:yy}</Year>" +
                "</root>";

            return Generator.GetRegNumber(inputXml, 3121);
        }

        public List<string> RequestNumberAutocomplete(string searchString)
        {
            if (searchString.IsNullOrEmpty())
                return null;
            var searchStringLower = searchString.ToLower();
            List<string> result = OMParamCalculation
                .Where(x => x.RequestNumber.ToLower().Contains(searchStringLower))
                .Select(x => x.RequestNumber)
                .OrderBy(x => x.RequestNumber)
                .Execute()
                .Select(x => x.RequestNumber)
                .Distinct()
                .Take(100)
                .ToList();
            return result;
        }

        #region Расчет значений расчета параметров объекта
        /// <summary>
        /// Расчет значений расчета параметров объекта
        /// </summary>
        /// <param name="calculationValuesInDto"></param>
        /// <returns></returns>
        public CalculationValuesOutDto Calculate(CalculationValuesInDto calculationValuesInDto)
        {
            CalculationValuesOutDto calculationValuesOutDto = new CalculationValuesOutDto();

            if (calculationValuesInDto == null)
            {
                return calculationValuesOutDto;
            }

            if (calculationValuesInDto.OplcSet)
            {
                calculationValuesOutDto.Oplc = calculationValuesInDto.Oplc;
                calculationValuesOutDto.OplcIncludeHpl = calculationValuesInDto.OplcIncludeHpl;
            }
            else
            {
                bool oplcIncludeHpl;
                calculationValuesOutDto.Oplc = CalculateOplc(
                    calculationValuesInDto.OplBti.HasValue && calculationValuesInDto.OplBti.Value > 0 ? calculationValuesInDto.OplBti : calculationValuesInDto.OplEgrn, 
                    calculationValuesInDto.Bpl, calculationValuesInDto.Lpl, calculationValuesInDto.Hpl, out oplcIncludeHpl);
                calculationValuesOutDto.OplcIncludeHpl = oplcIncludeHpl;
            }

            if (calculationValuesInDto.CalculatedAreaSet)
            {
                calculationValuesOutDto.CalculatedArea = calculationValuesInDto.CalculatedArea;
                calculationValuesOutDto.CalculatedAreaIncludeHpl = calculationValuesInDto.CalculatedAreaIncludeHpl;
            }
            else
            {
                bool calculatedAreaIncludeHpl;
                calculationValuesOutDto.CalculatedArea = CalculateArea(calculationValuesInDto.Krovpl, calculationValuesInDto.Epl, calculationValuesInDto.Bpl, calculationValuesInDto.Lpl, calculationValuesInDto.Hpl, out calculatedAreaIncludeHpl);
                calculationValuesOutDto.CalculatedAreaIncludeHpl = calculatedAreaIncludeHpl;
            }

            if (calculationValuesInDto.IndicatorRSet)
            {
                calculationValuesOutDto.IndicatorR = calculationValuesInDto.IndicatorR;
            }
            else
            {
                calculationValuesOutDto.IndicatorR = CalculateIndicatorR(calculationValuesOutDto.Oplc, calculationValuesOutDto.CalculatedArea, calculationValuesInDto.FlagOkrugl);
            }
            
            if (calculationValuesInDto.ActualCostSet)
            {
                calculationValuesOutDto.ActualCost = calculationValuesInDto.ActualCost;
            }
            else
            {
                calculationValuesOutDto.ActualCost = CalculateActualCost(calculationValuesInDto.StroiPrice, calculationValuesInDto.Pizn);
            }

            if (calculationValuesInDto.ActualCostCurrentSet)
            {
                calculationValuesOutDto.ActualCostCurrent = calculationValuesInDto.ActualCostCurrent;
            }
            else
            {
                calculationValuesOutDto.ActualCostCurrent = CalculateActualCostCurrent(calculationValuesOutDto.ActualCost, calculationValuesInDto.CoefActualCost);
            }

            calculationValuesOutDto.Ci1 = CalculateBuildingCost(calculationValuesInDto.Ui1, calculationValuesOutDto.ActualCostCurrent);
            calculationValuesOutDto.Ci2 = CalculateBuildingCost(calculationValuesInDto.Ui2, calculationValuesOutDto.ActualCostCurrent);
            calculationValuesOutDto.Ci3 = CalculateBuildingCost(calculationValuesInDto.Ui3, calculationValuesOutDto.ActualCostCurrent);
            calculationValuesOutDto.Ci4 = CalculateBuildingCost(calculationValuesInDto.Ui4, calculationValuesOutDto.ActualCostCurrent);
            calculationValuesOutDto.Ci5 = CalculateBuildingCost(calculationValuesInDto.Ui5, calculationValuesOutDto.ActualCostCurrent);
            calculationValuesOutDto.Ci6 = CalculateBuildingCost(calculationValuesInDto.Ui6, calculationValuesOutDto.ActualCostCurrent);
            calculationValuesOutDto.Ci7 = CalculateBuildingCost(calculationValuesInDto.Ui7, calculationValuesOutDto.ActualCostCurrent);
            calculationValuesOutDto.Ci8 = CalculateBuildingCost(calculationValuesInDto.Ui8, calculationValuesOutDto.ActualCostCurrent);
            calculationValuesOutDto.Ci9 = CalculateBuildingCost(calculationValuesInDto.Ui9, calculationValuesOutDto.ActualCostCurrent);
            calculationValuesOutDto.Ci10 = CalculateBuildingCost(calculationValuesInDto.Ui10, calculationValuesOutDto.ActualCostCurrent);

            calculationValuesOutDto.Cim1 = CalculateAreaCost(calculationValuesOutDto.Ci1, calculationValuesOutDto.IndicatorR);
            calculationValuesOutDto.Cim2 = CalculateAreaCost(calculationValuesOutDto.Ci2, calculationValuesOutDto.IndicatorR);
            calculationValuesOutDto.Cim3 = CalculateAreaCost(calculationValuesOutDto.Ci3, calculationValuesOutDto.IndicatorR);
            calculationValuesOutDto.Cim4 = CalculateAreaCost(calculationValuesOutDto.Ci4, calculationValuesOutDto.IndicatorR);
            calculationValuesOutDto.Cim5 = CalculateAreaCost(calculationValuesOutDto.Ci5, calculationValuesOutDto.IndicatorR);
            calculationValuesOutDto.Cim6 = CalculateAreaCost(calculationValuesOutDto.Ci6, calculationValuesOutDto.IndicatorR);
            calculationValuesOutDto.Cim7 = CalculateAreaCost(calculationValuesOutDto.Ci7, calculationValuesOutDto.IndicatorR);
            calculationValuesOutDto.Cim8 = calculationValuesOutDto.Ci8;
            calculationValuesOutDto.Cim9 = CalculateAreaCost(calculationValuesOutDto.Ci9, calculationValuesOutDto.IndicatorR);
            calculationValuesOutDto.Cim10 = calculationValuesOutDto.Ci10;

            calculationValuesOutDto.Ui11 = new decimal?[]
            {
                calculationValuesInDto.Ui1,
                calculationValuesInDto.Ui2,
                calculationValuesInDto.Ui3,
                calculationValuesInDto.Ui4,
                calculationValuesInDto.Ui5,
                calculationValuesInDto.Ui6,
                calculationValuesInDto.Ui7,
                calculationValuesInDto.Ui8,
                calculationValuesInDto.Ui9,
                calculationValuesInDto.Ui10
            }.Sum();

            if (calculationValuesOutDto.Ui11.HasValue && calculationValuesOutDto.Ui11.Value == 100)
            {
                calculationValuesOutDto.Ci11 = new decimal?[]
                {
                    calculationValuesOutDto.Ci1,
                    calculationValuesOutDto.Ci2,
                    calculationValuesOutDto.Ci3,
                    calculationValuesOutDto.Ci4,
                    calculationValuesOutDto.Ci5,
                    calculationValuesOutDto.Ci6,
                    calculationValuesOutDto.Ci7,
                    calculationValuesOutDto.Ci8,
                    calculationValuesOutDto.Ci9,
                    calculationValuesOutDto.Ci10
                }.Sum();
                calculationValuesOutDto.Cim11 = new decimal?[]
                {
                    calculationValuesOutDto.Cim1,
                    calculationValuesOutDto.Cim2,
                    calculationValuesOutDto.Cim3,
                    calculationValuesOutDto.Cim4,
                    calculationValuesOutDto.Cim5,
                    calculationValuesOutDto.Cim6,
                    calculationValuesOutDto.Cim7,
                    calculationValuesOutDto.Cim8,
                    calculationValuesOutDto.Cim9,
                    calculationValuesOutDto.Cim10
                }.Sum();

                calculationValuesOutDto.TotalCost1Category = new decimal?[]
                {
                    calculationValuesOutDto.Cim1,
                    calculationValuesOutDto.Cim2,
                    calculationValuesOutDto.Cim3,
                    calculationValuesOutDto.Cim4,
                    calculationValuesOutDto.Cim5,
                    calculationValuesOutDto.Cim6,
                    calculationValuesOutDto.Cim7,
                    calculationValuesOutDto.Cim8
                }.Sum();
                calculationValuesOutDto.TotalCost2Category = calculationValuesOutDto.Cim9;
                calculationValuesOutDto.TotalCost3Category = calculationValuesOutDto.Cim10;

                calculationValuesOutDto.DesignCost1Category = CalculateDesignCostCategory(calculationValuesOutDto.TotalCost1Category, calculationValuesInDto.PartCompensation);
                calculationValuesOutDto.DesignCost2Category = CalculateDesignCostCategory(calculationValuesOutDto.TotalCost2Category, calculationValuesInDto.PartCompensation);
                calculationValuesOutDto.DesignCost3Category = CalculateDesignCostCategory(calculationValuesOutDto.TotalCost3Category, calculationValuesInDto.PartCompensation);

                calculationValuesOutDto.AnnualBonus1Category = CalculateAnnualBonusCategory(calculationValuesOutDto.DesignCost1Category, calculationValuesInDto.BasicRate1);
                calculationValuesOutDto.AnnualBonus2Category = CalculateAnnualBonusCategory(calculationValuesOutDto.DesignCost2Category, calculationValuesInDto.BasicRate2);
                calculationValuesOutDto.AnnualBonus3Category = CalculateAnnualBonusCategory(calculationValuesOutDto.DesignCost3Category, calculationValuesInDto.BasicRate3);

                calculationValuesOutDto.MonthlyBonus1Category = CalculateMonthlyValue(calculationValuesOutDto.AnnualBonus1Category);
                calculationValuesOutDto.MonthlyBonus2Category = CalculateMonthlyValue(calculationValuesOutDto.AnnualBonus2Category);
                calculationValuesOutDto.MonthlyBonus3Category = CalculateMonthlyValue(calculationValuesOutDto.AnnualBonus3Category);

                calculationValuesOutDto.SizeAnnualBonus = new decimal?[]
                {
                calculationValuesOutDto.AnnualBonus1Category,
                calculationValuesOutDto.AnnualBonus2Category,
                calculationValuesOutDto.AnnualBonus3Category
                }.Sum();

                calculationValuesOutDto.SizeAnnualBonusMonthlyBonus = new decimal?[]
                {
                calculationValuesOutDto.MonthlyBonus1Category,
                calculationValuesOutDto.MonthlyBonus2Category,
                calculationValuesOutDto.MonthlyBonus3Category
                }.Sum();

                calculationValuesOutDto.SizeAnnualBonusFlat = CalculateFlatValue(calculationValuesOutDto.SizeAnnualBonus, calculationValuesInDto.KolGp);
                calculationValuesOutDto.SizeAnnualBonusMonthlyBonusFlat = CalculateFlatValue(calculationValuesOutDto.SizeAnnualBonusMonthlyBonus, calculationValuesInDto.KolGp);

                decimal? sizeBonusMkd = null;
                if (calculationValuesInDto.AgreementId.HasValue && calculationValuesInDto.AgreementId.Value > 0)
                {
                    OMAgreementProject agreementProject = OMAgreementProject.Where(x => x.EmpId == calculationValuesInDto.AgreementId.Value).SelectAll().ExecuteFirstOrDefault();
                    if (agreementProject != null)
                    {
                        if (calculationValuesInDto.RecalcSizeBonusMkd || !agreementProject.SizeBonusMkd.HasValue)
                        {
                            if (calculationValuesOutDto.AnnualBonus1Category.HasValue && agreementProject.Kat1.HasValue && agreementProject.Kat1.Value)
                            {
                                sizeBonusMkd = (sizeBonusMkd ?? 0) + calculationValuesOutDto.AnnualBonus1Category.Value;
                            }
                            if (calculationValuesOutDto.AnnualBonus2Category.HasValue && agreementProject.Kat2.HasValue && agreementProject.Kat2.Value)
                            {
                                sizeBonusMkd = (sizeBonusMkd ?? 0) + calculationValuesOutDto.AnnualBonus2Category.Value;
                            }
                            if (calculationValuesOutDto.AnnualBonus3Category.HasValue && agreementProject.Kat3.HasValue && agreementProject.Kat3.Value)
                            {
                                sizeBonusMkd = (sizeBonusMkd ?? 0) + calculationValuesOutDto.AnnualBonus3Category.Value;
                            }
                        }
                        else
                        {
                            sizeBonusMkd = agreementProject.SizeBonusMkd;
                        }

                    }
                }

                calculationValuesOutDto.SizeBonusMkd = sizeBonusMkd;
                calculationValuesOutDto.SizeBonusMkdMonthly = CalculateMonthlyValue(sizeBonusMkd);
                calculationValuesOutDto.Contribution = CalculateContribution(sizeBonusMkd, calculationValuesInDto.AgreementPartMoscow);
            }

            return calculationValuesOutDto;
        }

        /// <summary>
        /// Расчет общей площади по зданию, включая холодные помещения, кв.м.
        /// </summary>
        /// <param name="opl">Общая площадь по зданию</param>
        /// <param name="bpl">Площадь балконов</param>
        /// <param name="lpl">Площадь лоджий</param>
        /// <returns></returns>
        private decimal? CalculateOplc(decimal? opl, decimal? bpl, decimal? lpl, decimal? hpl, out bool oplcIncludeHpl)
        {
            oplcIncludeHpl = false;
            //CIPJS-767 Требуется дополнительно анализировать
            //Если | Площадь холодных помещений -(Площадь балконов + Площадь лоджий )| > 1
            //общая площадь +площадь балконов+площадь лоджий + площадь холодных помещений
            //иначе считаем как и ранее
            //общая площадь +площадь балконов+площадь лоджий
            if (opl.HasValue || bpl.HasValue || lpl.HasValue  || hpl.HasValue)
            {
                decimal bplLplSum = (bpl ?? 0) + (lpl ?? 0);
                if (hpl.HasValue && Math.Abs(hpl.Value - bplLplSum) > 1)
                {
                    oplcIncludeHpl = true;
                    return (opl ?? 0) + bplLplSum + hpl.Value;
                }
                else
                {
                    return (opl ?? 0) + bplLplSum;
                }
            }

            return null;
        }

        /// <summary>
        /// Расчет расчетной площади для определения стоимости общего имущества в МКД, кв.м.
        /// </summary>
        /// <param name="krovpl">Площадь кровли</param>
        /// <param name="epl">Площадь помещений, не входящих в общую площадь помещения</param>
        /// <param name="bpl">Площадь балконов</param>
        /// <param name="lpl">Площадь лоджий</param>
        /// <returns></returns>
        private decimal? CalculateArea(decimal? krovpl, decimal? epl, decimal? bpl, decimal? lpl, decimal? hpl, out bool calculatedAreaIncludeHpl)
        {
            calculatedAreaIncludeHpl = false;
            //CIPJS-767 Требуется дополнительно анализировать
            //Если | Площадь холодных помещений -(Площадь балконов + Площадь лоджий )| > 1
            //"Расчетная площадь" = площадь балконов+ площадь лоджий+ площадь кровли + ZPL+ площадь холодных помещений
            //иначе считаем как и ранее
            //"Расчетная площадь" = площадь балконов+ площадь лоджий+ площадь кровли + ZPL
            if (krovpl.HasValue || epl.HasValue || bpl.HasValue || lpl.HasValue || hpl.HasValue)
            {
                decimal bplLplSum = (bpl ?? 0) + (lpl ?? 0);
                if (hpl.HasValue && Math.Abs(hpl.Value - bplLplSum) > 1)
                {
                    calculatedAreaIncludeHpl = true;
                    return (krovpl ?? 0) + (epl ?? 0) + bplLplSum + hpl.Value;
                }
                else
                {
                    return (krovpl ?? 0) + (epl ?? 0) + bplLplSum;
                }
            }

            return null;
        }

        /// <summary>
        /// Расчет показателя рациональности объемно-планировочного решения, R
        /// </summary>
        /// <param name="oplc">Расчетная площадь, для определения страховой стоимости общего имущества в МКД, кв.м</param>
        /// <param name="calculatedArea">Общая площадь по зданию(включая холодные помещения), кв.м</param>
        /// <returns></returns>
        private decimal? CalculateIndicatorR(decimal? oplc, decimal? calculatedArea, bool? flagOkrugl)
        {
            if (!calculatedArea.HasValue || !oplc.HasValue)
            {
                return null;
            }

            if (calculatedArea.Value == 0)
            {
                return 0;
            }

            decimal? indicatorR = calculatedArea / oplc;

            return indicatorR.HasValue ? (decimal?)Math.Round(indicatorR.Value, flagOkrugl.HasValue && flagOkrugl.Value ? 2 : 10, MidpointRounding.AwayFromZero) : null;
        }

        /// <summary>
        /// Расчет действительной стоимости дома, руб
        /// </summary>
        /// <param name="actualCost">Строительная стоимость, руб</param>
        /// <param name="pizn">% износа</param>
        /// <returns></returns>
        private decimal? CalculateActualCost(decimal? stroiPrice, decimal? pizn)
        {
            if (stroiPrice.HasValue && pizn.HasValue)
            {
                return Math.Round(stroiPrice.Value * pizn.Value / 100, 2, MidpointRounding.AwayFromZero); ;
            }

            return null;
        }

        /// <summary>
        /// Расчет действительной стоимости дома (в пересчете на текущие цены), руб
        /// </summary>
        /// <param name="actualCost">Действительная стоимость дома, руб</param>
        /// <param name="сoefActualCost">Коэффициент пересчета действительной стоимости</param>
        /// <returns></returns>
        private decimal? CalculateActualCostCurrent(decimal? actualCost, decimal? сoefActualCost)
        {
            if (actualCost.HasValue && сoefActualCost.HasValue)
            {
                return Math.Round(actualCost.Value * сoefActualCost.Value, 2, MidpointRounding.AwayFromZero); ;
            }

            return null;
        }

        /// <summary>
        /// Расчет стоимости конструкции по зданию, руб
        /// </summary>
        /// <param name="ui">Удельный вес конструкции, %</param>
        /// <param name="actualCostCurrent">Действительная стоимость дома (в пересчете на текущие цены), руб</param>
        /// <returns></returns>
        private decimal? CalculateBuildingCost(decimal? ui, decimal? actualCostCurrent)
        {
            if (actualCostCurrent.HasValue && ui.HasValue)
            {
                return Math.Round(actualCostCurrent.Value * ui.Value / 100, 2, MidpointRounding.AwayFromZero);
            }

            return null;
        }

        /// <summary>
        /// Расчет стоимости конструкции общей зоны, руб
        /// </summary>
        /// <param name="ci">Стоимость конструкции по зданию, руб</param>
        /// <param name="indicatorR">Показатель рациональности объемно-планировочного решения</param>
        /// <returns></returns>
        private decimal? CalculateAreaCost(decimal? ci, decimal? indicatorR)
        {
            if (ci.HasValue && indicatorR.HasValue)
            {
                return Math.Round(ci.Value * indicatorR.Value, 2, MidpointRounding.AwayFromZero);
            }

            return null;
        }

        /// <summary>
        /// Расчет страховой суммы для категории общего имущества, руб
        /// </summary>
        /// <param name="totalCostCategory">Страховая стоимость категории общего имущества, руб</param>
        /// <param name="partCompensation">Доля ответственности СК, %</param>
        /// <returns></returns>
        private decimal? CalculateDesignCostCategory(decimal? totalCostCategory, decimal? partCompensation)
        {
            if (totalCostCategory.HasValue && partCompensation.HasValue)
            {
                return Math.Round(totalCostCategory.Value * partCompensation.Value / 100, 2, MidpointRounding.AwayFromZero);
            }

            return null;
        }

        /// <summary>
        /// Расчет годовой страховой премии для категории общего имущества, руб
        /// </summary>
        /// <param name="designCostCategory">Страховая сумма категории общего имущества, руб</param>
        /// <param name="baseRateCategory">Базовый страховой тариф категории общего имущества, %</param>
        /// <returns></returns>
        private decimal? CalculateAnnualBonusCategory(decimal? designCostCategory, decimal? baseRateCategory)
        {
            if (designCostCategory.HasValue && baseRateCategory.HasValue)
            {
                return Math.Round(designCostCategory.Value * baseRateCategory.Value / 100, 2, MidpointRounding.AwayFromZero);
            }

            return null;
        }

        /// <summary>
        /// Расчет значения для одного месяца от года
        /// </summary>
        /// <param name="yearValue">Значение для года</param>
        /// <returns></returns>
        private decimal? CalculateMonthlyValue(decimal? yearValue)
        {
            if (yearValue.HasValue)
            {
                return Math.Round(yearValue.Value / 12, 2, MidpointRounding.AwayFromZero);
            }

            return null;
        }

        /// <summary>
        /// Расчет суммы премии для отдельной квартиры, руб
        /// </summary>
        /// <param name="value">Премия</param>
        /// <param name="kolGp">Количество квартир</param>
        /// <returns></returns>
        private decimal? CalculateFlatValue(decimal? value, decimal? kolGp)
        {
            if (value.HasValue && kolGp.HasValue)
            {
                return Math.Round(value.Value / kolGp.Value, 2, MidpointRounding.AwayFromZero);
            }

            return null;
        }

        /// <summary>
        /// Расчет взноса (проект), руб
        /// </summary>
        /// <param name="sizeBonusMkd"></param>
        /// <param name="agreementPartMoscow"></param>
        /// <returns></returns>
        private decimal? CalculateContribution(decimal? sizeBonusMkd, decimal? agreementPartMoscow)
        {
            if (sizeBonusMkd.HasValue && agreementPartMoscow.HasValue)
            {
                return Math.Round(sizeBonusMkd.Value * (agreementPartMoscow.Value / 100), 2, MidpointRounding.AwayFromZero);
            }

            return null;
        }
        #endregion

        public bool CanUpdate(OMParamCalculation model, out string error)
        {
            // Запретить создание расчета с одним и тем же набором Номер заявки + UNOM
            var requestNumber = model.RequestNumber?.Trim();
            var oldRecord = OMParamCalculation
                .Where(x => x.ObjId == model.ObjId && x.RequestNumber == requestNumber && x.RequestDate == model.RequestDate && x.EmpId != model.EmpId && x.Deleted == false)
                .ExecuteFirstOrDefault();
            if (oldRecord != null)
            {
                error = "Внимание! Невозможно сохранить расчет, в системе уже присутствует расчет страховой стоимости для пары UNOM + Номер заявки + Дата заявки";
                return false;
            }

            // Запретить сохранение расчета при показателе рациональности >=1
            if (model.IndicatorR >= 1m)
            {
                error = "Внимание! Расчет сохранить невозможно при показателе рациональности >=1";
                return false;
            }

            error = null;
            return true;
        }
    }
}