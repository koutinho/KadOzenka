using Core.Register;
using Core.Register.RegisterEntities;
using ObjectModel.Directory;
using System;
using System.Collections.Generic;
using System.Text;

namespace ObjectModel.KO
{
    public partial class OMUnit
    {
        public string Kn { get; set; }
        public PropertyTypes ObjectType { get; set; }
        public long OldId { get; set; }
        public void SaveAndCreate()
        {
            ObjectModel.Gbu.OMMainObject mainObject = ObjectModel.Gbu.OMMainObject
            .Where(x => x.CadastralNumber == Kn)
            .SelectAll()
            .ExecuteFirstOrDefault();
            if (mainObject == null)
            {
                mainObject = new ObjectModel.Gbu.OMMainObject
                {
                    Id = -1,
                    CadastralNumber = Kn,
                    IsActive = true,
                    ObjectType_Code = ObjectType,
                };
                mainObject.Save();
            }
            else
            {
                if (mainObject.ObjectType_Code != ObjectType)
                {
                    mainObject.ObjectType_Code = ObjectType;
                    mainObject.Save();
                }
            }
            this.ObjectId = mainObject.Id;
            this.Save();
        }
    }

    public partial class OMModel
    {
        public string GetFormulaFull(bool upks)
        {
            string str_koeff = GetFormulaKoeff(false, string.Empty);
            return (upks ? ("УПКС=") : string.Empty) + ((str_koeff!=string.Empty) ?"(":string.Empty) + GetFormulaMain(false) + ((str_koeff != string.Empty) ? ")" : string.Empty) + str_koeff;
        }
        public string GetFormulaMain(bool upks)
        {
            string res = string.Empty;
            #region Моделирование
            if (ParentGroup == null)
                ParentGroup = ObjectModel.KO.OMGroup.Where(x => x.Id == GroupId).SelectAll().ExecuteFirstOrDefault();
            if (ModelFactor.Count==0)
                ModelFactor = OMModelFactor.Where(x => x.ModelId == this.Id).SelectAll().Execute();

            if (ParentGroup.GroupAlgoritm_Code == KoGroupAlgoritm.Etalon || this.ParentGroup.GroupAlgoritm_Code == KoGroupAlgoritm.Model)
            {
                string D_string = "";
                string Dm_string = "";
                string De_string = "";
                string Dss_string = "";


                foreach (OMModelFactor weight in this.ModelFactor)
                {
                    if (weight.SignAdd)
                    {
                        RegisterAttribute attributeData = RegisterCache.GetAttributeData((int)(weight.FactorId));
                        if (attributeData != null)
                        {
                            string d_string = (weight.SignMarket) ? ("метка" + "(" + attributeData.Name + ")") : (attributeData.Name);
                            switch (AlgoritmType_Code)
                            {
                                case KoAlgoritmType.Exp:
                                case KoAlgoritmType.Line:
                                    if (!weight.SignDiv)
                                        De_string = De_string + " * " + "(" + GetFormulaPart(weight.B0.ToString(), "+", 0) + GetFormulaPart(weight.Weight.ToString(), "*", 1) + d_string + ")";
                                    else
                                        De_string = De_string + " * " + "1/(" + GetFormulaPart(weight.B0.ToString(), "+", 0) + GetFormulaPart(weight.Weight.ToString(), "*", 1) + d_string + ")";
                                    break;
                                case KoAlgoritmType.Multi:
                                    if (!weight.SignDiv)
                                        Dss_string = Dss_string + " + " + "(" + GetFormulaPart(weight.B0.ToString(), "+", 0) + GetFormulaPart(weight.Weight.ToString(), "*", 1) + d_string + ")";
                                    else
                                        Dss_string = Dss_string + " + " + "1/(" + GetFormulaPart(weight.B0.ToString(), "+", 0) + GetFormulaPart(weight.Weight.ToString(), "*", 1) + d_string + ")";

                                    Dss_string = Dss_string.TrimStart(' ').TrimStart('+').TrimStart(' ');
                                    Dm_string = "(" + Dss_string + ")";
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                }

                foreach (OMModelFactor weight in this.ModelFactor)
                {
                    if (!weight.SignAdd)
                    {
                        RegisterAttribute attributeData = RegisterCache.GetAttributeData((int)(weight.FactorId));
                        if (attributeData != null)
                        {
                            string d_string = (weight.SignMarket) ? ("метка" + "(" + attributeData.Name + ")") : (attributeData.Name);

                            switch (AlgoritmType_Code)
                            {
                                case KoAlgoritmType.Exp:
                                case KoAlgoritmType.Line:
                                    if (!weight.SignDiv)
                                        D_string = D_string + " + " + GetFormulaPart(weight.Weight.ToString(), "*", 1) + d_string;
                                    else
                                        D_string = D_string + " + " + "1 / (" + GetFormulaPart(weight.Weight.ToString(), "*", 1) + d_string + ")";
                                    break;
                                case KoAlgoritmType.Multi:
                                    if (!weight.SignDiv)
                                        Dm_string = Dm_string + " * (" + GetFormulaPart(weight.B0.ToString(), "+", 0) + GetFormulaPart(weight.Weight.ToString(), "*", 1) + d_string + ")";
                                    else
                                        Dm_string = Dm_string + " / (" + GetFormulaPart(weight.B0.ToString(), "+", 0) + GetFormulaPart(weight.Weight.ToString(), "*", 1) + d_string + ")";
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                }


                switch (AlgoritmType_Code)
                {
                    case KoAlgoritmType.Exp:
                        if (De_string != string.Empty)
                            res = "exp(" + A0.ToString() + D_string + ")" + De_string;
                        else
                            res = "exp(" + A0.ToString() + D_string + ")";
                        break;
                    case KoAlgoritmType.Line:
                        res = A0.ToString() + D_string;
                        break;
                    case KoAlgoritmType.Multi:
                        res = GetFormulaPart(A0.ToString(), "*", 1) + Dm_string.TrimStart(' ').TrimStart('*').TrimStart(' ');
                        break;
                    default:
                        res = string.Empty;
                        break;
                }
            }
            #endregion
            return (upks ? ("УПКС=") : string.Empty) + res;
        }
        public string GetFormulaKoeff(bool upks, string value)
        {
            string res = string.Empty;
            if (ParentGroup == null)
                ParentGroup = ObjectModel.KO.OMGroup.Where(x => x.Id == GroupId).SelectAll().ExecuteFirstOrDefault();
            if (ParentGroup != null)
            {
                if (ParentGroup.GroupFactor.Count==0)
                ParentGroup.GroupFactor = OMGroupFactor.Where(x => x.GroupId == ParentGroup.Id).SelectAll().Execute();
                foreach (OMGroupFactor koeff in ParentGroup.GroupFactor)
                {
                    RegisterAttribute attributeData = RegisterCache.GetAttributeData((int)(koeff.FactorId));
                    if (attributeData != null)
                    {
                        res += ((/*koeff.IS_METKA*/true ? ("Корректировка(" + attributeData.Name + ")") : (attributeData.Name)) + "*");
                    }

                }
                if (res.Length > 1)
                {
                    res = res.TrimEnd('*');
                    res = "*" + res;
                }
            }
            return (upks ? "УПКС=" : string.Empty) + value + res;
        }
        private string GetFormulaPart(string val, string znak, double empty)
        {
            string res = Convert.ToDouble(val).ToString() + " " + znak + " ";
            double rval = Convert.ToDouble(val);
            if (rval == empty)
                res = string.Empty;
            return res;
        }
    }

}
