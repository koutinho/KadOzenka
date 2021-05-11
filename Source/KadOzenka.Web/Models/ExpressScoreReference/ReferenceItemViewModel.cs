//using System;
//using System.ComponentModel.DataAnnotations;
//using System.Globalization;
//using Core.SRD;
//using KadOzenka.Dal.ExpressScore.Dto;
//using ObjectModel.Directory.ES;
//using ObjectModel.ES;

//namespace KadOzenka.Web.Models.ExpressScoreReference
//{
//    public class ReferenceItemViewModel
//    {
//        public long Id { get; set; }

//        [Display(Name = "Справочник")]
//        [Required(ErrorMessage = "Поле Справочник обязательное")]
//        public long ReferenceId { get; set; }

//        public string ReferenceName { get; set; }
//        public ReferenceItemCodeType ReferenceValueType { get; set; }

//        /// <summary>
//        /// Общий код справочника
//        /// </summary>
//        [Display(Name = "Общий код справочника")]
//        public string CommonValue { get; set; }

//        /// <summary>
//        /// Код справочника
//        /// </summary>
//        [Display(Name = "Код справочника")]
//        public string Value { get; set; }

//        public decimal? NumberValue { get; set; }

//        public DateTime? DateTimeValue { get; set; }

//        /// <summary>
//        ///Число значение от
//        /// </summary>
//        public decimal? NumberValueFrom { get; set; }
//        /// <summary>
//        /// Числовое значение до
//        /// </summary>
//        public decimal? NumberValueTo { get; set; }

//        /// <summary>
//        /// Значение даты от
//        /// </summary>
//        public DateTime? DateTimeValueFrom { get; set; }

//        /// <summary>
//        /// Значение даты до
//        /// </summary>
//        public DateTime? DateTimeValueTo { get; set; }

//        /// <summary>
//        /// Значение для расчета
//        /// </summary>
//        [Display(Name = "Значение для расчета")]
//        public decimal? CalcValue { get; set; }

//        public bool IsEditItem { get; set; }

//        /// <summary>
//        /// Признак интервального справочника
//        /// </summary>
//        public bool UseInterval { get; set; }

//        public static ReferenceItemViewModel ToModel(OMEsReferenceItem referenceItem, OMEsReference reference)
//        {
//            if (referenceItem == null)
//            {
//                return new ReferenceItemViewModel
//                {
//                    Id = -1,
//                    ReferenceId = reference.Id,
//                    ReferenceName = reference.Name,
//                    UseInterval = reference.UseInterval.GetValueOrDefault(),
//                    ReferenceValueType = reference.ValueType_Code,
//                    IsEditItem = SRDSession.Current.CheckAccessToFunction(ObjectModel.SRD.SRDCoreFunctions.EXPRESSSCORE_REFERENCES_EDIT)
//            };
//            }

//            var res = new ReferenceItemViewModel
//            {
//	            Id = referenceItem.Id,
//	            ReferenceId = referenceItem.ReferenceId,
//	            ReferenceName = reference.Name,
//	            UseInterval = reference.UseInterval.GetValueOrDefault(),
//	            ReferenceValueType = reference.ValueType_Code,
//	            CalcValue = referenceItem.CalculationValue,
//                CommonValue = referenceItem.CommonValue,
//	            IsEditItem = SRDSession.Current.CheckAccessToFunction(ObjectModel.SRD.SRDCoreFunctions.EXPRESSSCORE_REFERENCES_EDIT)
//            };
//            if(reference.UseInterval.GetValueOrDefault()) SetValueIntervalReference(ref res, reference, referenceItem);
//            if(!reference.UseInterval.GetValueOrDefault()) SetValueNoIntervalReference(ref res, reference, referenceItem);
//            return res;
//        }

//        public ReferenceItemDto ToDto()
//        {
//	        var res = new ReferenceItemDto
//	        {
//		        Id = Id,
//		        ReferenceId = ReferenceId,
//                UseInterval = UseInterval,
//		        CommonValue = CommonValue,
//		        CalcValue = CalcValue
//	        };
//	        if (!UseInterval) SetDtoValueNoIntervalReference(ref res);
//	        if (UseInterval) SetDtoValueIntervalReference(ref res);
//            return res;
//        }

//        #region support methods

//        private static void SetValueNoIntervalReference(ref ReferenceItemViewModel vModel, OMEsReference reference, OMEsReferenceItem referenceItem)
//        {
//	        switch (reference.ValueType_Code)
//	        {
//		        case ReferenceItemCodeType.String:
//			        vModel.Value = referenceItem.Value;
//			        break;
//		        case ReferenceItemCodeType.Number:
//			        vModel.NumberValue = decimal.TryParse(referenceItem.Value, out var dVal) ? dVal : (decimal?)null;
//			        break;
//		        case ReferenceItemCodeType.Date:
//			        vModel.DateTimeValue = DateTime.TryParse(referenceItem.Value, out var date) ? date : (DateTime?)null;
//			        break;
//	        }
//        }

//        private static void SetValueIntervalReference(ref ReferenceItemViewModel vModel, OMEsReference reference, OMEsReferenceItem referenceItem)
//        {
//	        switch (reference.ValueType_Code)
//	        {
//		        case ReferenceItemCodeType.Number:
//			        vModel.NumberValueFrom = decimal.TryParse(referenceItem.ValueFrom, out var dValFrom) ? dValFrom : (decimal?)null;
//			        vModel.NumberValueTo = decimal.TryParse(referenceItem.ValueTo, out var dValTo) ? dValTo : (decimal?)null;
//			        break;
//		        case ReferenceItemCodeType.Date:
//			        vModel.DateTimeValueFrom = DateTime.TryParse(referenceItem.ValueFrom, out var dateFrom) ? dateFrom : (DateTime?)null;
//			        vModel.DateTimeValueTo = DateTime.TryParse(referenceItem.ValueTo, out var dateTo) ? dateTo : (DateTime?)null;
//			        break;
//	        }
//        }

//        private void SetDtoValueNoIntervalReference(ref ReferenceItemDto refItemDto)
//        {
//	        refItemDto.Value = Value;

//            if (ReferenceValueType == ReferenceItemCodeType.Date)
//	        {
//		        refItemDto.Value = DateTimeValue?.Date.ToShortDateString();
//	        }
//	        else if (ReferenceValueType == ReferenceItemCodeType.Number)
//	        {
//		        refItemDto.Value = NumberValue?.ToString();
//	        } 

//        }

//        private void SetDtoValueIntervalReference(ref ReferenceItemDto refItemDto)
//        {
//	        if (ReferenceValueType == ReferenceItemCodeType.Date)
//	        {
//		        refItemDto.ValueFrom = DateTimeValueFrom?.Date.ToShortDateString();
//		        refItemDto.ValueTo = DateTimeValueTo?.Date.ToShortDateString();
//	        }
//	        else if (ReferenceValueType == ReferenceItemCodeType.Number)
//	        {
//		        refItemDto.ValueFrom = NumberValueFrom?.ToString();
//		        refItemDto.ValueTo = NumberValueTo?.ToString();
//	        }

//        }

//        #endregion
//    }
//}
