using Core.Register.QuerySubsystem;
using Microsoft.Practices.EnterpriseLibrary.Common.Utility;
using ObjectModel.Insur;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CIPJS.DAL.Dictionaries
{
    /// <summary>
    /// Сборный класс для всех справочников
    /// </summary>
    public static class DictionaryService
    {
        /// <summary>
        /// Получить список базовых тарифов
        /// </summary>
        /// <returns></returns>
        public static Dictionary<long, decimal> GetBaseTariff()
        {
            return OMBaseTariff.Where(x => true).SelectAll().Execute().ToDictionary(x => x.EmpId, x => x.Value.Value);
        }

        /// <summary>
        /// Получить долю ответственности страховой компании
        /// </summary>
        /// <returns>Доля ответственности страховой компании</returns>
        public static decimal? GetPartCompensationIncuranceCompany(DateTime date)
        {
            QSCondition condition = new QSConditionSimple
            {
                ConditionType = QSConditionType.Equal,
                LeftOperand = OMPartCompensation.GetColumn(x => x.DateBegin),
                RightOperand = new QSColumnQuery
                {
                    SubQuery = new QSQuery
                    {
                        MainRegisterID = OMPartCompensation.GetRegisterId(),
                        Columns = new List<QSColumn>
                        {
                            new QSColumnFunction
                            {
                                FunctionType = QSColumnFunctionType.Max,
                                Operands = new List<QSColumn>
                                {
                                    OMPartCompensation.GetColumn(x => x.DateBegin)
                                }
                            }
                        },
                        Condition = OMPartCompensation.GetCondition(x => x.DateBegin <= date)
                    }
                }
            };

            var query = OMPartCompensation.Where(x => true).And(condition).Select(x => x.IcValue);

            var partCompensation = query.Execute().FirstOrDefault();

            if(partCompensation != null)
                return partCompensation.IcValue;

            return 0;
        }

        /// <summary>
        /// Получить долю ответственности города Москвы
        /// </summary>
        /// <returns>Доля ответственности города Москвы</returns>
        public static decimal? GetPartCompensationCity(DateTime date)
        {
            QSCondition condition = new QSConditionSimple
            {
                ConditionType = QSConditionType.Equal,
                LeftOperand = OMPartCompensation.GetColumn(x => x.DateBegin),
                RightOperand = new QSColumnQuery
                {
                    SubQuery = new QSQuery
                    {
                        MainRegisterID = OMPartCompensation.GetRegisterId(),
                        Columns = new List<QSColumn>
                        {
                            new QSColumnFunction
                            {
                                FunctionType = QSColumnFunctionType.Max,
                                Operands = new List<QSColumn>
                                {
                                    OMPartCompensation.GetColumn(x => x.DateBegin)
                                }
                            }
                        },
                        Condition = OMPartCompensation.GetCondition(x => x.DateBegin <= date)
                    }
                }
            };

            var query = OMPartCompensation.Where(x => true).And(condition).Select(x => x.CityValue);

            var partCompensation = query.Execute().FirstOrDefault();

            if (partCompensation != null)
                return partCompensation.CityValue;

            return 0;
        }

        /// <summary>
        /// Получить список страховых компаний
        /// </summary>
        /// <returns>Список страховых компаний</returns>
        public static Dictionary<long, string> GetIncuranceCompanyList()
        {
            var query = OMInsuranceOrganization.Where(x => true).SelectAll();
            var insuranceList = query.Execute().ToDictionary(x => x.Id, x => x.FullName);

            return insuranceList;
        }

        /// <summary>
        /// Получить страховую компанию по ее коду
        /// </summary>
        /// <param name="code">Код страховой компании</param>
        /// <returns>Страховая компания</returns>
        public static OMInsuranceOrganization GetIncuranceCompany(long code)
        {
            OMInsuranceOrganization insuranceCompany = OMInsuranceOrganization.Where(x => x.Code == code).SelectAll().Execute().FirstOrDefault();

            return insuranceCompany;
        }

        /// <summary>
        /// Получить страховую компанию по идентификатору округа
        /// </summary>
        /// <param name="okrugId">Идентификатор округа</param>
        /// <returns>Страховая компания</returns>
        public static OMInsuranceOrganization GetIncuranceCompanyByOkrug(long okrugId)
        {            
            QSCondition condition = new QSConditionSimple
            {
                ConditionType = QSConditionType.Equal,
                LeftOperand = OMInsuranceOrganization.GetColumn(x => x.Id),
                RightOperand = new QSColumnQuery
                {
                    SubQuery = new QSQuery
                    {
                        MainRegisterID = ObjectModel.Bti.OMBtiOkrug.GetRegisterId(),
                        Columns = new List<QSColumn>
                        {
                            ObjectModel.Bti.OMBtiOkrug.GetColumn(x => x.InsuranceCompanyId)
                        },
                        Condition = ObjectModel.Bti.OMBtiOkrug.GetCondition(x => x.Id == okrugId)
                    }
                }
            };

            OMInsuranceOrganization insuranceCompany = OMInsuranceOrganization.Where(x => true).And(condition)
                .SelectAll().Execute().FirstOrDefault();

            return insuranceCompany;
        }

        /// <summary>
        /// Получить управляющую компанию по ее коду
        /// </summary>
        /// <param name="code">Код управляющей компании</param>
        /// <returns>Управляющая компания</returns>
        public static OMSubject GetSubjectById(long id)
        {
            OMSubject subject = OMSubject.Where(x => x.EmpId == id).SelectAll().Execute().FirstOrDefault();

            return subject;
        }

        /// <summary>
        /// Получить управляющую компанию по ее коду
        /// </summary>
        /// <param name="code">Код управляющей компании</param>
        /// <returns>Управляющая компания</returns>
        public static OMSubject GetSubject(long code)
        {
            OMSubject subject = OMSubject.Where(x => x.KodOrg == code).SelectAll().Execute().FirstOrDefault();

            return subject;
        }

        /// <summary>
        /// Получить административный округ по его коду
        /// </summary>
        /// <param name="code">Код административного округа</param>
        /// <returns>Административный округ</returns>
        public static OMOkrug GetOkrug(long code)
        {
            OMOkrug okrug = OMOkrug.Where(x => x.Code == code).SelectAll().Execute().FirstOrDefault();

            return okrug;
        }

        public static decimal GetActualCostRatio(DateTime dateBegin)
        {
            OMActualCostRatio actualCostRatio = OMActualCostRatio.Where(x => x.DateBegin <= dateBegin)
                .SelectAll()
                .OrderByDescending(x => x.DateBegin)
                .Execute()
                .FirstOrDefault();

            if (actualCostRatio != null && actualCostRatio.Value.HasValue)
            {
                return actualCostRatio.Value.Value;
            }

            return 0m;
        }
    }
}
