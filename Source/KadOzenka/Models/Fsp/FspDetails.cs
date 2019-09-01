using Core.Shared.Extensions;
using ObjectModel.Directory;
using ObjectModel.Insur;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace CIPJS.Models.Fsp
{
    public class FspDetails
    {
        public FspDetails()
        {
            Payments = new List<FspBalanceDetails>();
            Plats = new List<FspInputPlatDetails>();
        }

        #region properties
        /// <summary>
        /// Идентификатор
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// Заголовок ФСП
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// Тип ФСП
        /// </summary>
        public FSPType FspType { get; set; }
        /// <summary>
        /// Тип ФСП
        /// </summary>
        [Display(Name = "Тип ФСП ")]
        public string FspTypeName => FspType.GetEnumDescription();

        /// <summary>
        /// Номер ФСП 
        /// </summary>
        [Display(Name = "Номер ФСП ")]
        public string FspNumber { get; set; }
        /// <summary>
        /// Номер лицевого счета
        /// </summary>
        [Display(Name = "Номер лицевого счета")]
        public long? Ls { get; set; }
        /// <summary>
        /// Идентификатор объекта 
        /// </summary>
        [Display(Name = "Идентификатор объекта ")]
        public long? ObjId { get; set; }
        /// <summary>
        /// Номер реестра объекта 
        /// </summary>
        [Display(Name = "Номер реестра объекта ")]
        public long? ObjReestrId { get; set; }
        /// <summary>
        /// Идентификатор договора ( полиса/свидетельства/договора страхования общего имущества)
        /// </summary>
        [Display(Name = "Идентификатор договора")]
        public long? ContractId { get; set; }
        /// <summary>
        /// Номер реестра договоров
        /// </summary>
        [Display(Name = "Номер реестра договоров")]
        public long? IdReestrContr { get; set; }
        /// <summary>
        /// Номер полиса/свидетельства 
        /// </summary>
        [Display(Name = "Номер полиса/свидетельства ")]
        public string Npol { get; set; }
        /// <summary>
        /// Код плательщика
        /// </summary>
        [Display(Name = "Код плательщика")]
        public string Kodpl { get; set; }
        /// <summary>
        /// Площадь ФСП (площадь, подлежащая страхованию)
        /// </summary>
        [Display(Name = "Площадь ФСП")]
        public decimal? OplKodpl { get; set; }
        /// <summary>
        /// Дата начала действия договора 
        /// </summary>
        [Display(Name = "Дата начала действия договора ")]
        public DateTime? DateContractBegin { get; set; }
        /// <summary>
        /// Дата окончания действия договора 
        /// </summary>
        [Display(Name = "Дата окончания действия договора ")]
        public DateTime? DateContractEnd { get; set; }
        /// <summary>
        /// Адрес помещения
        /// </summary>
        [Display(Name = "Адрес помещения")]
        public string Address { get; set; }
        /// <summary>
        /// Общая площадь помещения
        /// </summary>
        [Display(Name = "Общая площадь помещения")]
        public decimal? Opl { get; set; }

        public DateTime? OplKodplUpdateDate { get; set; }
        /// <summary>
        /// Дата создания ФСП
        /// </summary>
        [Display(Name = "Дата открытия ФСП")]
        public DateTime? DateOpen { get; set; }   

        [Display(Name = "Последний период с флагом застраховано")]
        public DateTime? StrahEnd { get; set; }

        [Display(Name = "Страховая сумма")]
        public decimal? Ss { get; set; }

        [Display(Name = "Условия страхования")]
        public string Pr { get; set; }

        public List<FspBalanceDetails> Payments { get; private set; } 
        public List<FspInputPlatDetails> Plats { get; private set; } 

        #endregion

        public static FspDetails OMMap(OMFsp entity)
        {
            if (entity == null) return new FspDetails();

            var fspDetails = new FspDetails
            {
                Id = entity.EmpId,
                Title = $" ФСП № {entity.FspNumber}" +
                    $"{(entity.FlagManyObj.HasValue && entity.FlagManyObj.Value ? " (ФСП на несколько квартир)" : string.Empty)}",
                ObjId = entity.ObjId,
                FspType = entity.FspType_Code,
                FspNumber = entity.FspNumber,
                Ls = entity.Ls,
                ObjReestrId = entity.ObjReestrId,
                ContractId = entity.ContractId,
                IdReestrContr = entity.IdReestrContr,
                DateOpen = entity.DateOpen,
                Kodpl = entity.Kodpl,
                OplKodpl = entity.OplKodpl,
                OplKodplUpdateDate = entity.OplKodplUpdateDate
            };

            if (entity.IdReestrContr == 309 && entity.ParentPolicySvd != null)
            {
                var policySvdData = entity.ParentPolicySvd;

                fspDetails.Kodpl = policySvdData.Kodpl.HasValue ? policySvdData.Kodpl.Value.ToString() : null;
                fspDetails.Npol = policySvdData.Npol;
                fspDetails.DateContractBegin = policySvdData.Dat;
                fspDetails.DateContractEnd = policySvdData.Dat.HasValue ? policySvdData.Dat.Value.AddYears(1).AddDays(-1) : (DateTime?)null;
                fspDetails.OplKodpl = policySvdData.Opl;
            }

            if (entity.Balance.IsNotEmpty())
            {
                fspDetails.Payments = entity.Balance
                    .OrderByDescending(x => x.PeriodRegDate)
                    .Select(FspBalanceDetails.OMMap)
                    .ToList();

                for (int i = 1; i < fspDetails.Payments.Count; i++)
                {
                    if (fspDetails.Payments[i - 1].SumOpl.HasValue)
                    {
                        if (fspDetails.Payments[i].SumOpl.HasValue)
                            fspDetails.Payments[i - 1].SumOpl = fspDetails.Payments[i - 1].SumOpl.Value - fspDetails.Payments[i].SumOpl.Value;
                        else
                        {
                            decimal lastValue = getLastValue(i - 1, fspDetails.Payments.Select(x => x.SumOpl).ToList());
                            fspDetails.Payments[i - 1].SumOpl = lastValue;
                        }
                    }

                    if (fspDetails.Payments[i - 1].SumNachMfc.HasValue)
                    {
                        if (fspDetails.Payments[i].SumNachMfc.HasValue)
                            fspDetails.Payments[i - 1].SumNachMfc = fspDetails.Payments[i - 1].SumNachMfc.Value - fspDetails.Payments[i].SumNachMfc.Value;
                        else
                        {
                            decimal lastValue = getLastValue(i - 1, fspDetails.Payments.Select(x => x.SumNachMfc).ToList());
                            fspDetails.Payments[i - 1].SumNachMfc = lastValue;
                        }
                    }

                    if (fspDetails.Payments[i - 1].SumNachGby.HasValue)
                    {
                        if (fspDetails.Payments[i].SumNachGby.HasValue)
                            fspDetails.Payments[i - 1].SumNachGby = fspDetails.Payments[i - 1].SumNachGby.Value - fspDetails.Payments[i].SumNachGby.Value;
                        else
                        {
                            decimal lastValue = getLastValue(i - 1, fspDetails.Payments.Select(x => x.SumNachGby).ToList());
                            fspDetails.Payments[i - 1].SumNachGby = lastValue;
                        }
                    }
                }

                fspDetails.StrahEnd = fspDetails.Payments.FirstOrDefault()?.StrahEnd;
            }

            if (entity.FspType_Code == FSPType.Polis)
            {
                fspDetails.Ss = entity.ParentPolicySvd?.Ss;
                fspDetails.Pr = entity.ParentPolicySvd?.Pralt_Code.GetEnumDescription();
            }

            return fspDetails;
        }

        public static OMFsp OMMap(FspDetails entity)
        {
            if (entity is null)
                return null;

            return new OMFsp
            {
                EmpId = entity.Id,
                ObjId = entity.ObjId,
                FspType_Code = entity.FspType,
                FspNumber = entity.FspNumber,
                Ls = entity.Ls,
                ObjReestrId = entity.ObjReestrId,
                ContractId = entity.ContractId,
                IdReestrContr = entity.IdReestrContr,
                DateOpen = entity.DateOpen,
                Kodpl = entity.Kodpl,
                OplKodpl = entity.OplKodpl
            };
        }

        /// <summary>
        /// Предыдущее значение не равное null из коллекции 
        /// </summary>
        /// <param name="index">индекс элемента, для которого нужно получить предыдущее значение</param>
        /// <param name="list">коллекция значений (отсотированная в обратном порядке)</param>
        /// <returns>Значение элемента</returns>
        private static decimal getLastValue(int index, List<decimal?> list)
        {
            decimal result = 0M;

            if (list.IsNotEmpty() &&  list.Count - index > 0)
            {
                for (int i = index + 1; i < list.Count - 1; i++)
                {
                    if (list[i].HasValue)
                    {
                        result = list[i].Value;
                        break;
                    }
                }
            }

            return result;
        }
    }
}