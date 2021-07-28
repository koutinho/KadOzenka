using System;
using System.Collections.Generic;
using Core.Shared.Extensions;
using ModelingBusiness.Objects.Entities;
using Newtonsoft.Json;
using ObjectModel.Directory;
using Serilog;

namespace ObjectModel.KO
{
    public partial class OMModel
    {
        public string InternalName => $"model_{Id}";
        public bool IsAutomatic => Type_Code == KoModelType.Automatic;
        public decimal A0ForMultiplicativeInFormula => Math.Round(A0ForMultiplicative.GetValueOrDefault(), ORM.Consts.ObjectModelConsts.ModelFormulaPrecision);
        public decimal A0ForExponentialInFormula => Math.Round(A0ForExponential.GetValueOrDefault(), ORM.Consts.ObjectModelConsts.ModelFormulaPrecision);
        public decimal A0ForLinearInFormula => Math.Round(A0.GetValueOrDefault(), ORM.Consts.ObjectModelConsts.ModelFormulaPrecision);
        public bool IsModelWasTrained => HasLinearTrainingResult || HasExponentialTrainingResult || HasMultiplicativeTrainingResult;
        public bool HasLinearTrainingResult => !string.IsNullOrWhiteSpace(LinearTrainingResult);
        public bool HasExponentialTrainingResult => !string.IsNullOrWhiteSpace(ExponentialTrainingResult);
        public bool HasMultiplicativeTrainingResult => !string.IsNullOrWhiteSpace(MultiplicativeTrainingResult);

        public decimal? GetA0(KoAlgoritmType? type = null)
        {
            var resultType = type ?? AlgoritmType_Code;
            switch (resultType)
            {
                case KoAlgoritmType.Exp:
                    return A0ForExponential;
                case KoAlgoritmType.Line:
                    return A0;
                case KoAlgoritmType.Multi:
                    return A0ForMultiplicative;
            }

            return null;
        }

        public void SetA0(decimal? a0)
        {
            switch (AlgoritmType_Code)
            {
                case KoAlgoritmType.Exp:
                    A0ForExponential = a0;
                    break;
                case KoAlgoritmType.Line:
                    A0 = a0;
                    break;
                case KoAlgoritmType.Multi:
                    A0ForMultiplicative = a0;
                    break;
                default:
                    A0 = a0;
                    break;
            }
        }

        public void SetA0(decimal? a0, KoAlgoritmType algoritmType)
        {
            switch (algoritmType)
            {
                case KoAlgoritmType.Exp:
                    A0ForExponential = a0;
                    break;
                case KoAlgoritmType.Line:
                    A0 = a0;
                    break;
                case KoAlgoritmType.Multi:
                    A0ForMultiplicative = a0;
                    break;
                default:
                    A0 = a0;
                    break;
            }
        }

        public string GetTrainingResult(KoAlgoritmType type)
        {
            switch (type)
            {
                case KoAlgoritmType.Exp:
                    return ExponentialTrainingResult;
                case KoAlgoritmType.Line:
                    return LinearTrainingResult;
                case KoAlgoritmType.Multi:
                    return MultiplicativeTrainingResult;
                default:
                    throw new Exception($"Неизвестный тип алгоритма модели {type.GetEnumDescription()}");
            }
        }

        //public string GetFormulaFull(bool upks)
        //{
        //    string str_koeff = GetFormulaKoeff(false, string.Empty);
        //    return (upks ? ("УПКС=") : string.Empty) + ((str_koeff != string.Empty) ? "(" : string.Empty) + GetFormulaMain(false) + ((str_koeff != string.Empty) ? ")" : string.Empty) + str_koeff;
        //}

        //public string GetFormulaMain(bool upks)
        //{
        //    string res = string.Empty;
        //    #region Моделирование
        //    if (ParentGroup == null)
        //        ParentGroup = ObjectModel.KO.OMGroup.Where(x => x.Id == GroupId).SelectAll().ExecuteFirstOrDefault();
        //    if (ModelFactor.Count == 0)
        //        ModelFactor = OMModelFactor.Where(x => x.ModelId == this.Id && x.AlgorithmType_Code==this.AlgoritmType_Code).SelectAll().Execute();

        //    if (ParentGroup.GroupAlgoritm_Code == KoGroupAlgoritm.Etalon || this.ParentGroup.GroupAlgoritm_Code == KoGroupAlgoritm.Model)
        //    {
        //        string D_string = "";
        //        string Dm_string = "";
        //        string De_string = "";
        //        string Dss_string = "";


        //        foreach (OMModelFactor weight in this.ModelFactor)
        //        {
        //            if (weight.SignAdd)
        //            {
        //                RegisterAttribute attributeData = RegisterCache.GetAttributeData((int)(weight.FactorId));
        //                if (attributeData != null)
        //                {
        //                    string d_string = (weight.SignMarket) ? ("метка" + "(" + attributeData.Name + ")") : (attributeData.Name);
        //                    switch (AlgoritmType_Code)
        //                    {
        //                        case KoAlgoritmType.Exp:
        //                        case KoAlgoritmType.Line:
        //                            if (!weight.SignDiv)
        //                                De_string = De_string + " * " + "(" + GetFormulaPart(weight.B0.ToString(), "+", 0) + GetFormulaPart(weight.Weight.ToString(), "*", 1) + d_string + ")";
        //                            else
        //                                De_string = De_string + " * " + "1/(" + GetFormulaPart(weight.B0.ToString(), "+", 0) + GetFormulaPart(weight.Weight.ToString(), "*", 1) + d_string + ")";
        //                            break;
        //                        case KoAlgoritmType.Multi:
        //                            if (!weight.SignDiv)
        //                                Dss_string = Dss_string + " + " + "(" + GetFormulaPart(weight.B0.ToString(), "+", 0) + GetFormulaPart(weight.Weight.ToString(), "*", 1) + d_string + ")";
        //                            else
        //                                Dss_string = Dss_string + " + " + "1/(" + GetFormulaPart(weight.B0.ToString(), "+", 0) + GetFormulaPart(weight.Weight.ToString(), "*", 1) + d_string + ")";

        //                            Dss_string = Dss_string.TrimStart(' ').TrimStart('+').TrimStart(' ');
        //                            Dm_string = "(" + Dss_string + ")";
        //                            break;
        //                        default:
        //                            break;
        //                    }
        //                }
        //            }
        //        }

        //        foreach (OMModelFactor weight in this.ModelFactor)
        //        {
        //            if (!weight.SignAdd)
        //            {
        //                RegisterAttribute attributeData = RegisterCache.GetAttributeData((int)(weight.FactorId));
        //                if (attributeData != null)
        //                {
        //                    string d_string = (weight.SignMarket) ? ("метка" + "(" + attributeData.Name + ")") : (attributeData.Name);

        //                    switch (AlgoritmType_Code)
        //                    {
        //                        case KoAlgoritmType.Exp:
        //                        case KoAlgoritmType.Line:
        //                            if (!weight.SignDiv)
        //                                D_string = D_string + " + " + GetFormulaPart(weight.Weight.ToString(), "*", 1) + d_string;
        //                            else
        //                                D_string = D_string + " + " + "1 / (" + GetFormulaPart(weight.Weight.ToString(), "*", 1) + d_string + ")";
        //                            break;
        //                        case KoAlgoritmType.Multi:
        //                            if (!weight.SignDiv)
        //                                Dm_string = Dm_string + " * (" + GetFormulaPart(weight.B0.ToString(), "+", 0) + GetFormulaPart(weight.Weight.ToString(), "*", 1) + d_string + ")";
        //                            else
        //                                Dm_string = Dm_string + " / (" + GetFormulaPart(weight.B0.ToString(), "+", 0) + GetFormulaPart(weight.Weight.ToString(), "*", 1) + d_string + ")";
        //                            break;
        //                        default:
        //                            break;
        //                    }
        //                }
        //            }
        //        }


        //        switch (AlgoritmType_Code)
        //        {
        //            case KoAlgoritmType.Exp:
        //                if (De_string != string.Empty)
        //                    res = "exp(" + A0ForExponential.ToString() + D_string + ")" + De_string;
        //                else
        //                    res = "exp(" + A0ForExponential.ToString() + D_string + ")";
        //                break;
        //            case KoAlgoritmType.Line:
        //                res = A0.ToString() + D_string;
        //                break;
        //            case KoAlgoritmType.Multi:
        //                res = GetFormulaPart(A0ForMultiplicative.ToString(), "*", 1) + Dm_string.TrimStart(' ').TrimStart('*').TrimStart(' ');
        //                break;
        //            default:
        //                res = string.Empty;
        //                break;
        //        }
        //    }
        //    #endregion
        //    return (upks ? ("УПКС=") : string.Empty) + res;
        //}

        //public string GetFormulaKoeff(bool upks, string value)
        //{
        //    string res = string.Empty;
        //    if (ParentGroup == null)
        //        ParentGroup = ObjectModel.KO.OMGroup.Where(x => x.Id == GroupId).SelectAll().ExecuteFirstOrDefault();
        //    if (ParentGroup != null)
        //    {
        //        if (ParentGroup.GroupFactor.Count == 0)
        //            ParentGroup.GroupFactor = OMGroupFactor.Where(x => x.GroupId == ParentGroup.Id).SelectAll().Execute();
        //        foreach (OMGroupFactor koeff in ParentGroup.GroupFactor)
        //        {
        //            RegisterAttribute attributeData = RegisterCache.GetAttributeData((int)(koeff.FactorId));
        //            if (attributeData != null)
        //            {
        //                res += ((/*koeff.IS_METKA*/true ? ("Корректировка(" + attributeData.Name + ")") : (attributeData.Name)) + "*");
        //            }

        //        }
        //        if (res.Length > 1)
        //        {
        //            res = res.TrimEnd('*');
        //            res = "*" + res;
        //        }
        //    }
        //    return (upks ? "УПКС=" : string.Empty) + value + res;
        //}

        //private string GetFormulaPart(string valInput, string znak, double empty)
        //{
        // var val = string.IsNullOrWhiteSpace(valInput) ? null : valInput;

        //    string res = Convert.ToDouble(val).ToString() + " " + znak + " ";
        //    double rval = Convert.ToDouble(val);
        //    if (rval == empty)
        //        res = string.Empty;
        //    return res;
        //}
    }

    public partial class OMModelFactor
    {
        static readonly ILogger _log = Serilog.Log.ForContext<OMModelFactor>();
        public List<OMModelingDictionariesValues> MarkCatalogs { get; set; }

       public decimal CorrectionInFormula => Math.Round(Correction, ORM.Consts.ObjectModelConsts.ModelFormulaPrecision);
       
       public decimal CorrectingTermInFormula => Math.Round(CorrectingTerm.GetValueOrDefault(), ORM.Consts.ObjectModelConsts.ModelFormulaPrecision);
       
       public decimal KInFormula => Math.Round(K.GetValueOrDefault(), ORM.Consts.ObjectModelConsts.ModelFormulaPrecision);

       public decimal GetCoefficientInFormula(KoAlgoritmType type)
       {
	       var coefficient = GetCoefficient(type);
	       return Math.Round(coefficient.GetValueOrDefault(), ORM.Consts.ObjectModelConsts.ModelFormulaPrecision);
       }

        public decimal? GetCoefficient(KoAlgoritmType type)
        {
	        switch (type)
	        {
		        case KoAlgoritmType.Exp:
			        return CoefficientForExponential;
		        case KoAlgoritmType.Line:
			        return CoefficientForLinear;
		        case KoAlgoritmType.Multi:
			        return CoefficientForMultiplicative;
		        default:
			        throw new Exception($"Передан неизвестный тип алгоритма '{type.GetEnumDescription()}'");
	        }
        }

        public void SetCoefficient(decimal? coefficient, KoAlgoritmType type)
        {
	        switch (type)
	        {
		        case KoAlgoritmType.Exp:
			        CoefficientForExponential = coefficient;
			        CoefficientForLinear = null;
			        CoefficientForMultiplicative = null;
                    break;
		        case KoAlgoritmType.Line:
			        CoefficientForLinear = coefficient;
			        CoefficientForExponential = null;
			        CoefficientForMultiplicative = null;
                    break;
		        case KoAlgoritmType.Multi:
			        CoefficientForMultiplicative = coefficient;
			        CoefficientForExponential = null;
			        CoefficientForLinear = null;
                    break;
		        default:
			        throw new Exception($"Передан неизвестный тип алгоритма '{type.GetEnumDescription()}'");
	        }
        }

        //public void FillMarkCatalogsFromList(List<OMMarkCatalog> list,long? groupId)
        //      {
        //          MarkCatalogs = new List<OMMarkCatalog>();
        //          MarkCatalogs.AddRange(list.Where(x => x.GroupId == groupId && x.FactorId == this.FactorId));
        //          //_log.Verbose("Заполнение каталогов меток для группы = {groupId} из имеющегося списка", groupId);
        //      }

        public void FillMarkCatalogsFromList(Dictionary<long, List<OMModelingDictionariesValues>> dict)
        {
            var success = dict.TryGetValue(FactorId, out var marks);
            MarkCatalogs = success ? marks : new List<OMModelingDictionariesValues>();
            //_log.Verbose("Заполнение каталогов меток для группы = {groupId} из имеющегося списка", groupId);
        }
    }
}


namespace ObjectModel.Modeling
{
	public partial class OMModelToMarketObjects
	{
		private List<CoefficientForObject> _deserializeCoefficients;
        public List<CoefficientForObject> DeserializedCoefficients => _deserializeCoefficients ??= DeserializeCoefficient();

        /// <summary>
        /// Флаг для массового обновления объектов во время расчета меток
        /// </summary>
        public bool IsCoefficientsChanged { get; set; }
        
        private List<CoefficientForObject> DeserializeCoefficient()
		{
			if (string.IsNullOrWhiteSpace(Coefficients))
				return new List<CoefficientForObject>();

			return JsonConvert.DeserializeObject<List<CoefficientForObject>>(Coefficients);
		}
	}
}
