using Core.Register;
using Core.Register.RegisterEntities;
using Core.Shared.Extensions;
using ObjectModel.Directory;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ObjectModel.KO
{
    public class ALLTmpItem
    {
        #region Поля
        public string KK { get; set; }
        public PropertyTypes Type { get; set; }
        public decimal UPKSZ { get; set; }
        public string Calc_obj { get; set; }
        #endregion

        #region Конструкторы и инициализация
        // создание по составляющим
        public ALLTmpItem(string aKK, PropertyTypes aType, decimal aUpks, string aCalc_obj)
        {
            KK = aKK;
            Type = aType;
            UPKSZ = aUpks;
            Calc_obj = aCalc_obj;
        }
        #endregion

    }
    public class ALLStatOKS
    {
        #region Поля
        public List<ALLTmpItem> Valuetmp { get; set; }
        #endregion

        #region Конструкторы и инициализация
        // создание по составляющим
        public ALLStatOKS()
        {
            Valuetmp = new List<ALLTmpItem>();
        }

        public void Add(string aKK, PropertyTypes aType, decimal aUpks, string aCalc_obj)
        {
            if (!String.IsNullOrEmpty(aKK))
            {
                bool find = false;
                {
                    foreach (ALLTmpItem item in Valuetmp)
                    {
                        if ((aKK == item.KK) & (aType == item.Type))
                        {
                            find = true;
                            break;
                        }
                    }
                    if (!find)
                    {
                        Valuetmp.Add(new ALLTmpItem(aKK, aType, aUpks, aCalc_obj));
                    }
                }

            }
        }
        public bool Get(string aKK, PropertyTypes aType, out decimal aUpks, out string aCalc_obj)
        {
            bool find = false;
            aUpks = 0;
            aCalc_obj = string.Empty;
            if (!String.IsNullOrEmpty(aKK))
            {
                ALLTmpItem item = Valuetmp.Find(x => ((x.KK == aKK) && (x.Type == aType)));
                if (item != null)
                {
                    find = true;
                    aUpks = item.UPKSZ;
                    aCalc_obj = item.Calc_obj;
                }
            }
            return find;
        }
        #endregion

    }
    public class CalcItem
    {
        public long FactorId { get; set; }
        public string Value { get; set; }

        public CalcItem(long factorid, string value)
        {
            Value = value;
            FactorId = factorid;
        }
    }


    public partial class OMUnit
    {
        public long OldId { get; set; }
        public void SaveAndCreate()
        {
            ObjectModel.Gbu.OMMainObject mainObject = ObjectModel.Gbu.OMMainObject
            .Where(x => x.CadastralNumber == this.CadastralNumber)
            .SelectAll()
            .ExecuteFirstOrDefault();
            if (mainObject == null)
            {
                mainObject = new ObjectModel.Gbu.OMMainObject
                {
                    Id = -1,
                    CadastralNumber = this.CadastralNumber,
                    IsActive = true,
                    ObjectType_Code = this.PropertyType_Code,
                };
                mainObject.Save();
            }
            else
            {
                if (mainObject.ObjectType_Code != PropertyType_Code)
                {
                    mainObject.ObjectType_Code = PropertyType_Code;
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

    public partial class OMModelFactor
    {
        public List<OMMarkCatalog> MarkCatalogs { get; set; }
        public void FillMarkCatalogs(OMModel model)
        {
            MarkCatalogs = new List<OMMarkCatalog>();
            MarkCatalogs.AddRange(OMMarkCatalog.Where(x=>x.GroupId==model.GroupId&&x.FactorId==this.FactorId).SelectAll().Execute());
        }
    }

    public partial class OMGroup
    {
        public static void GetAvgValue(long tourId, string kk, PropertyTypes type, List<long> calcChildGroups, out decimal upks, out string parentCalcObject)
        {
            upks = 0;
            parentCalcObject = string.Empty;

            List<OMUnit> units = OMUnit.Where(x => x.TourId == tourId && x.Status_Code==KoUnitStatus.Initial && x.GroupId==calcChildGroups[0]).SelectAll().Execute();
            int countKK = 0;
            int countKR = 0;
            foreach (OMUnit unit in units)
            {

            }

        }

        public void Calculate(List<ObjectModel.KO.OMUnit> units)
        {
            List<long> CalcParentGroup = new List<long>();
            CalcParentGroup.Add(300003);


            #region Эталонный
            //if (this.GroupAlgoritm_Code == KoGroupAlgoritm.Etalon)
            //{
            //    ALLSubGroupFormulaItem formula = ALLSubGroupFormulaItem.GetSubGroupFormula(this);
            //    if (formula != null)
            //    {
            //        frmWait fw = new frmWait();
            //        fw.ShowText("Загрузка данных", 0, 0);
            //        ALLSubGroupFormulaWeightItem[] weights = ALLSubGroupFormulaWeightItem.GetSubGroupFormulaWeight(this);
            //        ALLObjectItem[] objs1 = ALLObjectItem.GetObjectsBetween(0, 700000, this.CurGroup.Id, Id, building, flat, construction, underconstruction, parcel, true, false, false, false, ALLParamItem.Change_Object);

            //        List<ALLObjectItem> objs = new List<ALLObjectItem>();
            //        objs.AddRange(objs1);
            //        List<ALLFactorItem> factors = new List<ALLFactorItem>();
            //        factors.AddRange(ALLFactorItem.GetFactors(this.CurGroup.Id, true));
            //        bool usesquarefactor = false;
            //        Int64 idsquarefactor = 0;
            //        bool useanalogfactor = false;
            //        Int64 idanalogfactor = 0;

            //        foreach (ALLFactorItem factor in factors)
            //        {
            //            factor.FillMetka(this);
            //            if (factor.TYPE_FACTOR == euFactorType.ftSquare)
            //            {
            //                usesquarefactor = true;
            //                idsquarefactor = factor.Id;
            //            }
            //            if (factor.TYPE_FACTOR == euFactorType.ftAnalog)
            //            {
            //                useanalogfactor = true;
            //                idanalogfactor = factor.Id;
            //            }
            //        }



            //        int ccc = 0;
            //        foreach (ALLObjectItem obj in objs)
            //        {
            //            if (obj.CheckDocInput(id_doc))
            //            {

            //                fw.ShowText(FullName_SubGroup, objs.Count, ccc);
            //                ccc++;
            //                if (obj.ETALON)
            //                {
            //                    decimal D = 0;
            //                    decimal Dm = 1;
            //                    decimal De = 1;
            //                    decimal Dss = 0;
            //                    List<ALLFactorValueItem> fvs = new List<ALLFactorValueItem>();
            //                    fvs.AddRange(ALLFactorValueItem.GetAllFactors(obj.TYPE_OBJECT, obj.Id, obj.ID_GROUP, false));
            //                    if (usesquarefactor)
            //                    {
            //                        bool findsquarevalue = false;
            //                        foreach (ALLFactorValueItem fvi in fvs)
            //                        {
            //                            if (fvi.ID_FACTOR == idsquarefactor)
            //                            {
            //                                findsquarevalue = true;
            //                                fvi.TYPE_FACTOR = euFactorType.ftSquare;
            //                                if (obj._main)
            //                                {
            //                                    fvi.VALUE_FACTOR = obj.SQUARE_OBJECT.ToString();
            //                                }
            //                                else
            //                                {
            //                                    ALLExplicationValueItem[] exps = ALLExplicationValueItem.GetExplications(obj.Id, obj.TYPE_OBJECT, false);
            //                                    double exsquare = 0;
            //                                    foreach (ALLExplicationValueItem exp in exps)
            //                                    {
            //                                        if ((exp.ID_SUBGROUP == Id) && (exp.Id == obj._id_exp))
            //                                        {
            //                                            exsquare = exp.SQUARE;
            //                                        }
            //                                    }
            //                                    fvi.VALUE_FACTOR = (exsquare.ToString());
            //                                }
            //                            }
            //                        }
            //                        if (!findsquarevalue)
            //                        {
            //                            if (obj._main)
            //                            {
            //                                fvs.Add(new ALLFactorValueItem(euFactorType.ftSquare, obj.Id, idsquarefactor, obj.SQUARE_OBJECT.ToString()));
            //                            }
            //                            else
            //                            {
            //                                ALLExplicationValueItem[] exps = ALLExplicationValueItem.GetExplications(obj.Id, obj.TYPE_OBJECT, false);
            //                                double exsquare = 0;
            //                                foreach (ALLExplicationValueItem exp in exps)
            //                                {
            //                                    if ((exp.ID_SUBGROUP == Id) && (exp.Id == obj._id_exp))
            //                                    {
            //                                        exsquare = exp.SQUARE;
            //                                    }
            //                                }
            //                                fvs.Add(new ALLFactorValueItem(euFactorType.ftSquare, obj.Id, idsquarefactor, exsquare.ToString()));
            //                            }
            //                        }
            //                    }
            //                    if (useanalogfactor)
            //                    {
            //                        bool findanalogvalue = false;
            //                        foreach (ALLFactorValueItem fvi in fvs)
            //                        {
            //                            if (fvi.ID_FACTOR == idanalogfactor)
            //                            {
            //                                findanalogvalue = true;
            //                                fvi.TYPE_FACTOR = euFactorType.ftAnalog;
            //                                if (!obj._main)
            //                                {
            //                                    ALLExplicationValueItem[] exps = ALLExplicationValueItem.GetExplications(obj.Id, obj.TYPE_OBJECT, false);
            //                                    string exanalog = string.Empty;
            //                                    foreach (ALLExplicationValueItem exp in exps)
            //                                    {
            //                                        if ((exp.ID_SUBGROUP == Id) && (exp.Id == obj._id_exp))
            //                                        {
            //                                            exanalog = exp.CALC_PARENT;
            //                                        }
            //                                    }
            //                                    fvi.VALUE_FACTOR = exanalog;
            //                                }
            //                            }
            //                        }
            //                        if (!findanalogvalue)
            //                        {
            //                            if (!obj._main)
            //                            {
            //                                ALLExplicationValueItem[] exps = ALLExplicationValueItem.GetExplications(obj.Id, obj.TYPE_OBJECT, false);
            //                                string exanalog = string.Empty;
            //                                foreach (ALLExplicationValueItem exp in exps)
            //                                {
            //                                    if ((exp.ID_SUBGROUP == Id) && (exp.Id == obj._id_exp))
            //                                    {
            //                                        exanalog = exp.CALC_PARENT;
            //                                    }
            //                                }
            //                                fvs.Add(new ALLFactorValueItem(euFactorType.ftAnalog, obj.Id, idanalogfactor, exanalog));
            //                            }
            //                        }
            //                    }



            //                    string strerror = "";

            //                    foreach (ALLSubGroupFormulaWeightItem weight in weights)
            //                    {
            //                        bool prFind = false;
            //                        foreach (ALLFactorValueItem fv in fvs)
            //                        {
            //                            if (fv.ID_FACTOR == weight.Id_Factor) prFind = true;
            //                        }
            //                        if ((!prFind) & (weight.FactorType != euFactorType.ftSquare) & (weight.FactorType != euFactorType.ftAnalog))
            //                            strerror = strerror + "Отсутствует значение фактора " + weight.Name_Factor + Environment.NewLine;
            //                    }

            //                    foreach (ALLFactorValueItem fv in fvs)
            //                    {
            //                        foreach (ALLFactorItem factor in factors)
            //                        {
            //                            if (factor.Id == fv.ID_FACTOR)
            //                            {
            //                                foreach (ALLSubGroupFormulaWeightItem weight in weights)
            //                                {
            //                                    if (weight.Id_Factor == fv.ID_FACTOR)
            //                                    {
            //                                        if (weight.Pr_ADD)
            //                                        {
            //                                            if (weight.Pr_Metka)
            //                                            {
            //                                                bool mok = false;
            //                                                decimal d = 0;
            //                                                foreach (ALLFactorMetkaItem fmi in factor.Metkas)
            //                                                {
            //                                                    if (fmi.Value_Factor.ToUpper() == fv.VALUE_FACTOR.ToUpper())
            //                                                    {
            //                                                        d = fmi.Metka_Factor;
            //                                                        mok = true;
            //                                                    }
            //                                                }
            //                                                if (!mok)
            //                                                    strerror = strerror + "Отсутствует значение метки фактора " + factor.NAME_FACTOR + " для значения " + fv.VALUE_FACTOR + Environment.NewLine;



            //                                                if (formula.Type_Formula == 1)
            //                                                {
            //                                                    if (!weight.Pr_DIV)
            //                                                        De = De * (weight.B0 + weight.Weight_Factor * d);
            //                                                    else
            //                                                        De = De * 1 / (weight.B0 + weight.Weight_Factor * d);
            //                                                }

            //                                                if (formula.Type_Formula == 3)
            //                                                {
            //                                                    if (!weight.Pr_DIV)
            //                                                        Dss = Dss + (weight.B0 + weight.Weight_Factor * d);
            //                                                    else
            //                                                        Dss = Dss + 1 / (weight.B0 + weight.Weight_Factor * d);
            //                                                    Dm = Dss;
            //                                                }
            //                                            }
            //                                            else
            //                                            {
            //                                                decimal d = 0;
            //                                                bool dok = decimal.TryParse(fv.VALUE_FACTOR, out d);
            //                                                if (!dok)
            //                                                    strerror = strerror + "Неверное значение фактора " + factor.NAME_FACTOR + " : " + fv.VALUE_FACTOR + Environment.NewLine;

            //                                                if (formula.Type_Formula == 1)
            //                                                {
            //                                                    if (!weight.Pr_DIV)
            //                                                        De = De * (weight.B0 + weight.Weight_Factor * d);
            //                                                    else
            //                                                        De = De * 1 / (weight.B0 + weight.Weight_Factor * d);
            //                                                }

            //                                                if (formula.Type_Formula == 3)
            //                                                {
            //                                                    if (!weight.Pr_DIV)
            //                                                        Dss = Dss + (weight.B0 + weight.Weight_Factor * d);
            //                                                    else
            //                                                        Dss = Dss + 1 / (weight.B0 + weight.Weight_Factor * d);
            //                                                    Dm = Dss;
            //                                                }
            //                                            }
            //                                        }
            //                                    }
            //                                }
            //                            }
            //                        }

            //                    }

            //                    foreach (ALLFactorValueItem fv in fvs)
            //                    {
            //                        foreach (ALLFactorItem factor in factors)
            //                        {
            //                            if (factor.Id == fv.ID_FACTOR)
            //                            {
            //                                foreach (ALLSubGroupFormulaWeightItem weight in weights)
            //                                {
            //                                    if (weight.Id_Factor == fv.ID_FACTOR)
            //                                    {
            //                                        if (!weight.Pr_ADD)
            //                                        {

            //                                            if (weight.Pr_Metka)
            //                                            {
            //                                                decimal d = 0;
            //                                                bool mok = false;
            //                                                foreach (ALLFactorMetkaItem fmi in factor.Metkas)
            //                                                {
            //                                                    if (fmi.Value_Factor.ToUpper() == fv.VALUE_FACTOR.ToUpper())
            //                                                    {
            //                                                        d = fmi.Metka_Factor;
            //                                                        mok = true;
            //                                                    }
            //                                                }

            //                                                if (!mok)
            //                                                    strerror = strerror + "Отсутствует значение метки фактора " + factor.NAME_FACTOR + " для значения " + fv.VALUE_FACTOR + Environment.NewLine;

            //                                                if (!weight.Pr_DIV)
            //                                                    D = D + weight.Weight_Factor * d;
            //                                                else
            //                                                    D = D + 1 / (weight.Weight_Factor * d);

            //                                                if (formula.Type_Formula == 3)
            //                                                {
            //                                                    if (!weight.Pr_DIV)
            //                                                        Dm = Dm * (weight.B0 + weight.Weight_Factor * d);
            //                                                    else
            //                                                        Dm = Dm / (weight.B0 + weight.Weight_Factor * d);
            //                                                }
            //                                            }
            //                                            else
            //                                            {
            //                                                decimal d = 0;
            //                                                bool dok = decimal.TryParse(fv.VALUE_FACTOR, out d);
            //                                                if (!dok)
            //                                                    strerror = strerror + "Неверное значение фактора " + factor.NAME_FACTOR + " : " + fv.VALUE_FACTOR + Environment.NewLine;

            //                                                if (!weight.Pr_DIV)
            //                                                    D = D + weight.Weight_Factor * d;
            //                                                else
            //                                                    D = D + 1 / (weight.Weight_Factor * d);

            //                                                if (formula.Type_Formula == 3)
            //                                                {
            //                                                    if (!weight.Pr_DIV)
            //                                                        Dm = Dm * (weight.B0 + weight.Weight_Factor * d);
            //                                                    else
            //                                                        Dm = Dm / (weight.B0 + weight.Weight_Factor * d);
            //                                                }
            //                                            }
            //                                        }
            //                                    }
            //                                }
            //                            }
            //                        }

            //                    }

            //                    if (strerror != "")
            //                    {
            //                        sw.WriteLine(obj.KN_OBJECT);
            //                        sw.WriteLine(strerror);
            //                    }

            //                    if (formula.Type_Formula == 1)
            //                    {
            //                        double UPKS = Math.Exp(Convert.ToDouble(formula.A0 + D)) * Convert.ToDouble(De);
            //                        UPKS = Math.Round(UPKS, 2);
            //                        ALLObjectItem[] chlobjs = ALLObjectItem.GetObjectsBetweenAllKK(obj.ID_GROUP, obj.ID_SUBGROUP, obj.KN_KK, ALLParamItem.Change_Object);
            //                        foreach (ALLObjectItem chlobj in chlobjs)
            //                        {
            //                            chlobj.UpdateCalc(UPKS, UPKS * chlobj.SQUARE_CALC);
            //                            chlobj.UpdateCalcRez(0, 0);
            //                            if (strerror != string.Empty) chlobj.UpdateCalc(0, 0);
            //                        }
            //                    }
            //                    if (formula.Type_Formula == 2)
            //                    {
            //                        double UPKS = Convert.ToDouble(formula.A0 + D);
            //                        UPKS = Math.Round(UPKS, 2);
            //                        ALLObjectItem[] chlobjs = ALLObjectItem.GetObjectsBetweenAllKK(obj.ID_GROUP, obj.ID_SUBGROUP, obj.KN_KK, ALLParamItem.Change_Object);
            //                        foreach (ALLObjectItem chlobj in chlobjs)
            //                        {
            //                            chlobj.UpdateCalc(UPKS, UPKS * chlobj.SQUARE_CALC);
            //                            chlobj.UpdateCalcRez(0, 0);
            //                            if (strerror != string.Empty) chlobj.UpdateCalc(0, 0);
            //                        }
            //                    }
            //                    if (formula.Type_Formula == 3)
            //                    {
            //                        double UPKS = Convert.ToDouble(formula.A0 * Dm);
            //                        UPKS = Math.Round(UPKS, 2);
            //                        ALLObjectItem[] chlobjs = ALLObjectItem.GetObjectsBetweenAllKK(obj.ID_GROUP, obj.ID_SUBGROUP, obj.KN_KK, ALLParamItem.Change_Object);
            //                        foreach (ALLObjectItem chlobj in chlobjs)
            //                        {
            //                            chlobj.UpdateCalc(UPKS, UPKS * chlobj.SQUARE_CALC);
            //                            chlobj.UpdateCalcRez(0, 0);
            //                            if (strerror != string.Empty) chlobj.UpdateCalc(0, 0);
            //                        }
            //                    }
            //                }
            //            }
            //        }
            //        fw.HideText();
            //    }
            //}
            #endregion
            
            #region Моделирование
            if (this.GroupAlgoritm_Code == KoGroupAlgoritm.Model)
            {
                OMModel model = OMModel.Where(x => x.GroupId == this.Id).SelectAll().ExecuteFirstOrDefault();
                if (model != null)
                {
                    if (model.ModelFactor.Count == 0)
                        model.ModelFactor = OMModelFactor.Where(x => x.ModelId == model.Id).SelectAll().Execute();

                    foreach (OMModelFactor weight in model.ModelFactor)
                    {
                        weight.FillMarkCatalogs(model);
                    }


                    //TO DO: Добавить объекты с экспликацией

                    bool usesquarefactor = false;
                    Int64 idsquarefactor = 0;
                    bool useanalogfactor = false;
                    Int64 idanalogfactor = 0;

                    //foreach (ALLFactorItem factor in factors)
                    //{
                    //    factor.FillMetka(this);
                    //    if (factor.TYPE_FACTOR == euFactorType.ftSquare)
                    //    {
                    //        usesquarefactor = true;
                    //        idsquarefactor = factor.Id;
                    //    }
                    //    if (factor.TYPE_FACTOR == euFactorType.ftAnalog)
                    //    {
                    //        useanalogfactor = true;
                    //        idanalogfactor = factor.Id;
                    //    }
                    //}



                    int ccc = 0;

                    CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
                    ParallelOptions options = new ParallelOptions
                    {
                        CancellationToken = cancelTokenSource.Token,
                        MaxDegreeOfParallelism = 40
                    };


                    Parallel.ForEach(units, options, unit =>
                    {
                        List<CalcItem> FactorValues = new List<CalcItem>();
                        DataTable data = RegisterStorage.GetAttributes((int)unit.Id, 252);
                        if (data != null)
                        {
                            foreach (DataRow row in data.Rows)
                            {
                                FactorValues.Add(new CalcItem(row.ItemArray[1].ParseToLong(), row.ItemArray[6].ParseToString()));
                            }
                        }

                        {
                            ccc++;
                            if (ccc % 500 == 0)
                                Console.WriteLine(ccc);
                            decimal D = 0;
                            decimal Dm = 1;
                            decimal De = 1;
                            decimal Dss = 0;
                            //List<ALLFactorValueItem> fvs = new List<ALLFactorValueItem>();
                            //fvs.AddRange(ALLFactorValueItem.GetAllFactors(obj.TYPE_OBJECT, obj.Id, obj.ID_GROUP, false));
                            //if (usesquarefactor)
                            //{
                            //    bool findsquarevalue = false;
                            //    foreach (ALLFactorValueItem fvi in fvs)
                            //    {
                            //        if (fvi.ID_FACTOR == idsquarefactor)
                            //        {
                            //            findsquarevalue = true;
                            //            fvi.TYPE_FACTOR = euFactorType.ftSquare;
                            //            if (obj._main)
                            //            {
                            //                fvi.VALUE_FACTOR = obj.SQUARE_OBJECT.ToString();
                            //            }
                            //            else
                            //            {
                            //                ALLExplicationValueItem[] exps = ALLExplicationValueItem.GetExplications(obj.Id, obj.TYPE_OBJECT, false);
                            //                double exsquare = 0;
                            //                foreach (ALLExplicationValueItem exp in exps)
                            //                {
                            //                    if ((exp.ID_SUBGROUP == Id) && (exp.Id == obj._id_exp))
                            //                    {
                            //                        exsquare = exp.SQUARE;
                            //                    }
                            //                }
                            //                fvi.VALUE_FACTOR = (exsquare.ToString());
                            //            }
                            //        }
                            //    }
                            //    if (!findsquarevalue)
                            //    {
                            //        if (obj._main)
                            //        {
                            //            fvs.Add(new ALLFactorValueItem(euFactorType.ftSquare, obj.Id, idsquarefactor, obj.SQUARE_OBJECT.ToString()));
                            //        }
                            //        else
                            //        {
                            //            ALLExplicationValueItem[] exps = ALLExplicationValueItem.GetExplications(obj.Id, obj.TYPE_OBJECT, false);
                            //            double exsquare = 0;
                            //            foreach (ALLExplicationValueItem exp in exps)
                            //            {
                            //                if ((exp.ID_SUBGROUP == Id) && (exp.Id == obj._id_exp))
                            //                {
                            //                    exsquare = exp.SQUARE;
                            //                }
                            //            }
                            //            fvs.Add(new ALLFactorValueItem(euFactorType.ftSquare, obj.Id, idsquarefactor, exsquare.ToString()));
                            //        }
                            //    }
                            //}
                            //if (useanalogfactor)
                            //{
                            //    bool findanalogvalue = false;
                            //    foreach (ALLFactorValueItem fvi in fvs)
                            //    {
                            //        if (fvi.ID_FACTOR == idanalogfactor)
                            //        {
                            //            findanalogvalue = true;
                            //            fvi.TYPE_FACTOR = euFactorType.ftAnalog;
                            //            if (!obj._main)
                            //            {
                            //                ALLExplicationValueItem[] exps = ALLExplicationValueItem.GetExplications(obj.Id, obj.TYPE_OBJECT, false);
                            //                string exanalog = string.Empty;
                            //                foreach (ALLExplicationValueItem exp in exps)
                            //                {
                            //                    if ((exp.ID_SUBGROUP == Id) && (exp.Id == obj._id_exp))
                            //                    {
                            //                        exanalog = exp.CALC_PARENT;
                            //                    }
                            //                }
                            //                fvi.VALUE_FACTOR = exanalog;
                            //            }
                            //        }
                            //    }
                            //    if (!findanalogvalue)
                            //    {
                            //        if (!obj._main)
                            //        {
                            //            ALLExplicationValueItem[] exps = ALLExplicationValueItem.GetExplications(obj.Id, obj.TYPE_OBJECT, false);
                            //            string exanalog = string.Empty;
                            //            foreach (ALLExplicationValueItem exp in exps)
                            //            {
                            //                if ((exp.ID_SUBGROUP == Id) && (exp.Id == obj._id_exp))
                            //                {
                            //                    exanalog = exp.CALC_PARENT;
                            //                }
                            //            }
                            //            fvs.Add(new ALLFactorValueItem(euFactorType.ftAnalog, obj.Id, idanalogfactor, exanalog));
                            //        }
                            //    }
                            //}



                            string strerror = "";

                            //foreach (OMModelFactor weight in model.ModelFactor)
                            //{
                            //    bool prFind = false;
                            //    DataTable data = RegisterStorage.GetAttribute((int)unit.Id, 252, (int)weight.FactorId);
                            //    if (data!=null)
                            //    {
                            //        if (data.Rows.Count>0)
                            //        {
                            //            if (data.Rows[0].ItemArray[6].ParseToString() != string.Empty)
                            //                prFind = true;
                            //        }
                            //    }
                            //    //if (!prFind) //& (weight.FactorType != euFactorType.ftSquare) & (weight.FactorType != euFactorType.ftAnalog)
                            //    //    strerror = strerror + "Отсутствует значение фактора " + weight.Name_Factor + Environment.NewLine;
                            //}

                            foreach (OMModelFactor weight in model.ModelFactor)
                            {
                                if (weight.SignAdd)
                                {
                                    string factorValue = string.Empty;//TODO: значение фактора для данного объекта
                                    CalcItem ci = FactorValues.Find(x => x.FactorId == weight.FactorId);
                                    if (ci != null) factorValue = ci.Value;


                                    //DataTable data = RegisterStorage.GetAttribute((int)unit.Id, 252, (int)weight.FactorId);
                                    //if (data != null)
                                    //{
                                    //    if (data.Rows.Count > 0)
                                    //    {
                                    //        if (data.Rows[0].ItemArray[6].ParseToString() != string.Empty)
                                    //            factorValue = data.Rows[0].ItemArray[6].ParseToString();
                                    //    }
                                    //}

                                    string factorName = string.Empty;//TODO: наименование фактора





                                    if (weight.SignMarket)
                                    {
                                        bool mok = false;
                                        decimal d = 0;
                                        OMMarkCatalog mc = weight.MarkCatalogs.Find(x => x.ValueFactor.ToUpper() == factorValue.ToUpper());
                                        if (mc != null)
                                        {
                                            d = mc.MetkaFactor.ParseToDecimal();
                                            mok = true;
                                        }
                                        if (!mok)
                                        {
                                            strerror = strerror + "Отсутствует значение метки фактора " + factorName + " для значения " + factorValue + Environment.NewLine;
                                        }



                                        if (model.AlgoritmType_Code == KoAlgoritmType.Exp)
                                        {
                                            if (!weight.SignDiv)
                                                De = De * (weight.B0 + weight.Weight * d);
                                            else
                                                De = De * 1 / (weight.B0 + weight.Weight * d);
                                        }

                                        if (model.AlgoritmType_Code == KoAlgoritmType.Multi)
                                        {
                                            if (!weight.SignDiv)
                                                Dss = Dss + (weight.B0 + weight.Weight * d);
                                            else
                                                Dss = Dss + 1 / (weight.B0 + weight.Weight * d);
                                            Dm = Dss;
                                        }
                                    }
                                    else
                                    {
                                        decimal d = 0;
                                        bool dok = decimal.TryParse(factorValue, out d);
                                        if (!dok)
                                            strerror = strerror + "Неверное значение фактора " + factorName + " : " + factorValue + Environment.NewLine;

                                        if (model.AlgoritmType_Code == KoAlgoritmType.Exp)
                                        {
                                            if (!weight.SignDiv)
                                                De = De * (weight.B0 + weight.Weight * d);
                                            else
                                                De = De * 1 / (weight.B0 + weight.Weight * d);
                                        }

                                        if (model.AlgoritmType_Code == KoAlgoritmType.Multi)
                                        {
                                            if (!weight.SignDiv)
                                                Dss = Dss + (weight.B0 + weight.Weight * d);
                                            else
                                                Dss = Dss + 1 / (weight.B0 + weight.Weight * d);
                                            Dm = Dss;
                                        }
                                    }
                                }
                            }

                            foreach (OMModelFactor weight in model.ModelFactor)
                            {
                                if (!weight.SignAdd)
                                {
                                    string factorValue = string.Empty;//TODO: значение фактора для данного объекта
                                    CalcItem ci = FactorValues.Find(x => x.FactorId == weight.FactorId);
                                    if (ci != null) factorValue = ci.Value;
                                    //DataTable data = RegisterStorage.GetAttribute((int)unit.Id, 252, (int)weight.FactorId);
                                    //if (data != null)
                                    //{
                                    //    if (data.Rows.Count > 0)
                                    //    {
                                    //        if (data.Rows[0].ItemArray[6].ParseToString() != string.Empty)
                                    //            factorValue = data.Rows[0].ItemArray[6].ParseToString();
                                    //    }
                                    //}
                                    string factorName = string.Empty;//TODO: наименование фактора

                                    if (weight.SignMarket)
                                    {
                                        decimal d = 0;
                                        bool mok = false;

                                        OMMarkCatalog mc = weight.MarkCatalogs.Find(x => x.ValueFactor.ToUpper() == factorValue.ToUpper());
                                        if (mc != null)
                                        {
                                            d = mc.MetkaFactor.ParseToDecimal();
                                            mok = true;
                                        }
                                        if (!mok)
                                            strerror = strerror + "Отсутствует значение метки фактора " + factorName + " для значения " + factorValue + Environment.NewLine;

                                        if (!weight.SignDiv)
                                            D = D + weight.Weight * d;
                                        else
                                            D = D + 1 / (weight.Weight * d);

                                        if (model.AlgoritmType_Code == KoAlgoritmType.Multi)
                                        {
                                            if (!weight.SignDiv)
                                                Dm = Dm * (weight.B0 + weight.Weight * d);
                                            else
                                                Dm = Dm / (weight.B0 + weight.Weight * d);
                                        }
                                    }
                                    else
                                    {
                                        decimal d = 0;
                                        bool dok = decimal.TryParse(factorValue, out d);
                                        if (!dok)
                                            strerror = strerror + "Неверное значение фактора " + factorName + " : " + factorValue + Environment.NewLine;

                                        if (!weight.SignDiv)
                                            D = D + weight.Weight * d;
                                        else
                                        {
                                            if (dok)
                                                D = D + 1 / (weight.Weight * d);
                                            else
                                                D = 0;
                                        }

                                        if (model.AlgoritmType_Code == KoAlgoritmType.Multi)
                                        {
                                            if (!weight.SignDiv)
                                                Dm = Dm * (weight.B0 + weight.Weight * d);
                                            else
                                            {

                                                if (dok)
                                                    Dm = Dm / (weight.B0 + weight.Weight * d);
                                                else
                                                    Dm = 0;
                                            }
                                        }
                                    }
                                }
                            }

                            if (strerror != "")
                            {
                                //sw.WriteLine(unit.CadastralNumber);
                                //sw.WriteLine(strerror);
                            }

                            if (model.AlgoritmType_Code == KoAlgoritmType.Exp)
                            {
                                decimal UPKS = Math.Round(Convert.ToDecimal(Math.Exp(Convert.ToDouble(model.A0 + D))) * De, 2, MidpointRounding.AwayFromZero);
                                decimal Cost = Math.Round((UPKS * unit.Square).ParseToDecimal(), 2, MidpointRounding.AwayFromZero);
                                if (unit.UpksPre != UPKS)
                                    Console.WriteLine("УПКС:{0} УПКС:{1}", unit.UpksPre, UPKS);
                                if (unit.CadastralCostPre != Cost)
                                    Console.WriteLine("УПКС:{0} УПКС:{1} КС:{2} КС:{3} SQ:{4}", unit.UpksPre, UPKS, unit.CadastralCostPre, Cost, unit.Square);
                                if (true)//obj._main
                                {
                                    if (strerror != string.Empty)
                                    {
                                        unit.UpksPre = 0;
                                        unit.CadastralCostPre = 0;
                                    }
                                    else
                                    {
                                        unit.UpksPre = UPKS;
                                        unit.CadastralCostPre = Cost;
                                    }
                                    unit.Upks = 0;
                                    unit.CadastralCost = 0;
                                    //unit.Save();
                                }
                                else
                                {
                                    //ALLExplicationValueItem[] exps = ALLExplicationValueItem.GetExplications(obj.Id, obj.TYPE_OBJECT, false);
                                    //foreach (ALLExplicationValueItem exp in exps)
                                    //{
                                    //    if ((exp.ID_SUBGROUP == Id) && (exp.Id == obj._id_exp))
                                    //    {
                                    //        if (strerror == string.Empty)
                                    //            exp.Update(UPKS, Math.Round(UPKS * exp.SQUARE, 2));
                                    //    }
                                    //}
                                }
                            }
                            if (model.AlgoritmType_Code == KoAlgoritmType.Line)
                            {
                                decimal UPKS = Math.Round(Convert.ToDecimal(model.A0 + D), 2);
                                decimal Cost = Math.Round((UPKS * unit.Square).ParseToDecimal(), 2);

                                if (unit.UpksPre != UPKS || unit.CadastralCostPre != Cost)
                                    Console.WriteLine("УПКС:{0} УПКС:{1} КС:{2} КС:{3}", unit.UpksPre, UPKS, unit.CadastralCostPre, Cost);

                                if (true)//obj._main
                                {
                                    if (strerror != string.Empty)
                                    {
                                        unit.UpksPre = 0;
                                        unit.CadastralCostPre = 0;
                                    }
                                    else
                                    {
                                        unit.UpksPre = UPKS;
                                        unit.CadastralCostPre = Cost;
                                    }
                                    unit.Upks = 0;
                                    unit.CadastralCost = 0;
                                    //unit.Save();
                                }
                                else
                                {
                                    //ALLExplicationValueItem[] exps = ALLExplicationValueItem.GetExplications(obj.Id, obj.TYPE_OBJECT, false);
                                    //foreach (ALLExplicationValueItem exp in exps)
                                    //{
                                    //    if ((exp.ID_SUBGROUP == Id) && (exp.Id == obj._id_exp))
                                    //    {
                                    //        if (strerror == string.Empty)
                                    //            exp.Update(UPKS, Math.Round(UPKS * exp.SQUARE, 2));
                                    //    }
                                    //}
                                }
                            }
                            if (model.AlgoritmType_Code == KoAlgoritmType.Multi)
                            {
                                decimal UPKS = Math.Round(Convert.ToDecimal(model.A0 * Dm), 2);
                                decimal Cost = Math.Round((UPKS * unit.Square).ParseToDecimal(), 2);
                                if (unit.UpksPre != UPKS || unit.CadastralCostPre != Cost)
                                    Console.WriteLine("УПКС:{0} УПКС:{1} КС:{2} КС:{3}", unit.UpksPre, UPKS, unit.CadastralCostPre, Cost);
                                if (true)//obj._main
                                {
                                    if (strerror != string.Empty)
                                    {
                                        unit.UpksPre = 0;
                                        unit.CadastralCostPre = 0;
                                    }
                                    else
                                    {
                                        unit.UpksPre = UPKS;
                                        unit.CadastralCostPre = Cost;
                                    }
                                    unit.Upks = 0;
                                    unit.CadastralCost = 0;
                                    //unit.Save();
                                }
                                else
                                {
                                    //ALLExplicationValueItem[] exps = ALLExplicationValueItem.GetExplications(obj.Id, obj.TYPE_OBJECT, false);
                                    //foreach (ALLExplicationValueItem exp in exps)
                                    //{
                                    //    if ((exp.ID_SUBGROUP == Id) && (exp.Id == obj._id_exp))
                                    //    {
                                    //        if (strerror == string.Empty)
                                    //            exp.Update(UPKS, Math.Round(UPKS * exp.SQUARE, 2));
                                    //    }
                                    //}
                                }
                            }
                        }
                    });
                }
            }
            #endregion
            /*
            #region Здания
            #region Здания по помещениям (КР, КЛАДР)
            if (this.GroupAlgoritm_Code == KoGroupAlgoritm.BuildingOnFlat)
            {
                frmWait fw = new frmWait();
                fw.ShowText("Расчет зданий подгруппы " + this.FullName_SubGroup, 0, 0);
                ALLObjectItem[] objs = ALLObjectItem.GetObjectsBetween(0, 700000, this.CurGroup.Id, Id, true, false, false, false, false, true, false, false, false, ALLParamItem.Change_Object);
                int ccc = 0;
                foreach (ALLObjectItem obj in objs)
                {
                    if (obj.CheckDocInput(id_doc))
                    {
                        fw.ShowText("Расчет зданий подгруппы " + this.FullName_SubGroup, objs.Length, ccc);
                        ccc++;
                        CalcBuildKR(obj.Id, obj.KN_OBJECT, obj.KN_KK, obj.KLADR_OBJECT_RF, 1);
                    }
                }
                fw.HideText();
            }
            #endregion
            #region Среднее (КР, КЛАДР)
            if (this.GroupAlgoritm_Code == KoGroupAlgoritm.AVG)
            {
                frmWait fw = new frmWait();
                fw.ShowText("Расчет зданий подгруппы " + this.FullName_SubGroup, 0, 0);
                ALLObjectItem[] objs = ALLObjectItem.GetObjectsBetween(0, 700000, this.CurGroup.Id, Id, true, false, false, false, false, true, false, false, false, ALLParamItem.Change_Object);
                int ccc = 0;
                ALLStatOKS sts = new ALLStatOKS();
                foreach (ALLObjectItem obj in objs)
                {
                    if (obj.CheckDocInput(id_doc))
                    {
                        fw.ShowText("Расчет зданий подгруппы " + this.FullName_SubGroup, objs.Length, ccc);
                        ccc++;

                        double upksz = 0;
                        double cupksz = 0;
                        string calc_obj = string.Empty;
                        if (sts.Get4("77", obj.KN_KK, 1, out cupksz, out calc_obj))
                        {
                            obj.UpdateCalc(cupksz, cupksz * obj.SQUARE_CALC);
                            obj.UpdateCalcParent(calc_obj);
                            obj.UpdateCalcRez(0, 0);
                        }
                        else
                        {
                            CalcNoHarAVG(obj.Id, obj.KN_OBJECT, obj.KN_KK, "77", 1, 1, out upksz, out calc_obj);
                            obj.UpdateCalc(upksz, upksz * obj.SQUARE_CALC);
                            obj.UpdateCalcParent(calc_obj);
                            obj.UpdateCalcRez(0, 0);
                            sts.Add4("77", obj.KN_KK, 1, upksz, calc_obj);
                        }
                    }
                }
                fw.HideText();
            }
            #endregion
            #region Минимальное (КР, КЛАДР)
            if (this.GroupAlgoritm_Code == KoGroupAlgoritm.Min)
            {
                frmWait fw = new frmWait();
                fw.ShowText("Расчет зданий подгруппы " + this.FullName_SubGroup, 0, 0);
                ALLObjectItem[] objs = ALLObjectItem.GetObjectsBetween(0, 700000, this.CurGroup.Id, Id, true, false, false, false, false, true, false, false, false, ALLParamItem.Change_Object);
                int ccc = 0;
                ALLStatOKS sts = new ALLStatOKS();
                foreach (ALLObjectItem obj in objs)
                {
                    if (obj.CheckDocInput(id_doc))
                    {
                        fw.ShowText("Расчет зданий подгруппы " + this.FullName_SubGroup, objs.Length, ccc);
                        ccc++;

                        double upksz = 0;
                        double cupksz = 0;
                        string calc_obj = string.Empty;
                        if (sts.Get4("77", obj.KN_KK, 1, out cupksz, out calc_obj))
                        {
                            obj.UpdateCalc(cupksz, cupksz * obj.SQUARE_CALC);
                            obj.UpdateCalcParent(calc_obj);
                            obj.UpdateCalcRez(0, 0);
                        }
                        else
                        {
                            CalcNoHarMin(obj.Id, obj.KN_OBJECT, obj.KN_KK, "77", 1, 1, out upksz, out calc_obj);
                            obj.UpdateCalc(upksz, upksz * obj.SQUARE_CALC);
                            obj.UpdateCalcParent(calc_obj);
                            obj.UpdateCalcRez(0, 0);
                            sts.Add4("77", obj.KN_KK, 1, upksz, calc_obj);
                        }
                    }
                }
                fw.HideText();
            }
            #endregion
            #endregion

            #region Помещения
            #region Помещения по зданиям (КР, КЛАДР)
            if ((this.Type_SubGroup == 9) & (flat) & ((this.Id != 18) || (ALLParamItem.Change_Object == ALLParamItem.New_Object) || (ALLParamItem.Change_Object == ALLParamItem.God_Object)))
            {
                frmWait fw = new frmWait();
                fw.ShowText("Расчет помещений подгруппы " + this.FullName_SubGroup, 0, 0);
                ALLStatOKS sts = new ALLStatOKS();

                ALLObjectItem[] objs = ALLObjectItem.GetObjectsBetween(0, 700000, this.CurGroup.Id, Id, false, true, false, false, false, true, false, false, false, ALLParamItem.Change_Object);
                int ccc = 0;
                foreach (ALLObjectItem obj in objs)
                {
                    if (obj.CheckDocInput(id_doc))
                    {
                        fw.ShowText("Расчет помещений подгруппы " + this.FullName_SubGroup, objs.Length, ccc);
                        ccc++;



                        double upksz = 0;
                        double cupksz = 0;
                        string calc_obj = string.Empty;
                        if (sts.Get4(obj.KN_PARENT, obj.KN_KK, 0, out cupksz, out calc_obj))  //if (sts.Get3(obj.KN_PARENT, obj.KN_KK, 0, out cupksz))
                        {
                            obj.UpdateCalc(cupksz, cupksz * obj.SQUARE_CALC);
                            obj.UpdateCalcParent(calc_obj);
                            obj.UpdateCalcRez(0, 0);
                        }
                        else
                        {
                            int first_change = (ALLParamItem.Change_Object == ALLParamItem.Exist_Object) ? ALLParamItem.Exist_Object : ALLParamItem.Calc_Object;
                            int second_change = (ALLParamItem.Change_Object == ALLParamItem.Exist_Object) ? ALLParamItem.Exist_Object : ALLParamItem.Calc_Object;
                            bool prFind = false;
                            DateTime objapp = obj.GetDocInputApp();

                            List<ALLObjectItem> allparentobj = new List<ALLObjectItem>();

                            ALLObjectItem objparentCalcN1 = ALLObjectItem.GetObjectBuilding(obj.KN_PARENT, ALLParamItem.New_Object, false, objapp);
                            ALLObjectItem objparentCalcN2 = ALLObjectItem.GetObjectConstruction(obj.KN_PARENT, ALLParamItem.New_Object, false, objapp);
                            ALLObjectItem objparentCalcC1 = ALLObjectItem.GetObjectBuilding(obj.KN_PARENT, ALLParamItem.Calc_Object, false, objapp);
                            ALLObjectItem objparentCalcC2 = ALLObjectItem.GetObjectConstruction(obj.KN_PARENT, ALLParamItem.Calc_Object, false, objapp);
                            ALLObjectItem objparentCalcE1 = ALLObjectItem.GetObjectBuilding(obj.KN_PARENT, ALLParamItem.Exist_Object, false, objapp);
                            ALLObjectItem objparentCalcE2 = ALLObjectItem.GetObjectConstruction(obj.KN_PARENT, ALLParamItem.Exist_Object, false, objapp);
                            if (objparentCalcN1 != null) allparentobj.Add(objparentCalcN1);
                            if (objparentCalcN2 != null) allparentobj.Add(objparentCalcN2);
                            if (objparentCalcC1 != null) allparentobj.Add(objparentCalcC1);
                            if (objparentCalcC2 != null) allparentobj.Add(objparentCalcC2);
                            if (objparentCalcE1 != null) allparentobj.Add(objparentCalcE1);
                            if (objparentCalcE2 != null) allparentobj.Add(objparentCalcE2);

                            ALLObjectItem objparentCalc = null;
                            TimeSpan tt = TimeSpan.MaxValue;
                            foreach (ALLObjectItem pobj in allparentobj)
                            {
                                DateTime pdateapp = pobj.GetDocInputApp();
                                if ((objapp.Date >= pdateapp.Date) && (pobj.UPKSZ_OBJECT > 0))
                                {
                                    if (objapp - pdateapp < tt)
                                    {
                                        tt = objapp - pdateapp;
                                        objparentCalc = pobj;
                                    }
                                }
                            }

                            if (objparentCalc != null)
                            {
                                if (groups.Count > 0)
                                {
                                    foreach (ALLSubGroupItem sg in groups)
                                    {
                                        if (sg.Id == objparentCalc.ID_SUBGROUP)
                                        {
                                            prFind = true;
                                            upksz = objparentCalc.UPKSZ_OBJECT;
                                            calc_obj = objparentCalc.KN_OBJECT;
                                        }
                                    }
                                }
                            }
                            if (!prFind)
                            {
                                CalcFlatKR(obj.Id, obj.KN_PARENT, obj.KN_KK, obj.KLADR_OBJECT_RF, 1, first_change, second_change, ALLParamItem.Exist_Object, out upksz, out calc_obj);
                            }

                            obj.UpdateCalc(upksz, upksz * obj.SQUARE_CALC);
                            obj.UpdateCalcParent(calc_obj);
                            obj.UpdateCalcRez(0, 0);
                            sts.Add4(obj.KN_PARENT, obj.KN_KK, 0, upksz, calc_obj);
                        }
                    }
                }
                fw.HideText();
            }
            #endregion
            #region Помещения по зданиям (КР, КЛАДР) (квартиры)
            if ((this.Type_SubGroup == 9) & (flat) & ((this.Id == 18) && (ALLParamItem.Change_Object == ALLParamItem.Exist_Object)))
            {
                frmWait fw = new frmWait();
                fw.ShowText("Расчет помещений подгруппы " + this.FullName_SubGroup, 0, 0);
                ALLStatOKS sts = new ALLStatOKS();


                int count = 300000;
                bool next = true;
                int start = 11800000;

                next = true;
                while (next)
                {
                    string fwtext = "Обработка данных с " + start.ToString() + " по " + (start + count).ToString();
                    fw.ShowText(fwtext, 0, 0);
                    next = false;
                    ALLObjectItem[] objs = ALLObjectItem.GetObjectsBetweenFlatGroup(start, count, Id, ALLParamItem.Change_Object);
                    if (objs.Length > 0)
                    {
                        int ccc = 0;
                        foreach (ALLObjectItem obj in objs)
                        {
                            if (obj.CheckDocInput(id_doc))
                            {
                                ccc++;
                                fw.ShowText("Расчет помещений подгруппы " + this.FullName_SubGroup, objs.Length, ccc);
                                {
                                    double upksz = 0;
                                    double cupksz = 0;
                                    string ccalc = "";
                                    if (sts.Get3(obj.KN_PARENT, obj.KN_KK, 0, out cupksz))
                                    {
                                        obj.UpdateCalc(cupksz, cupksz * obj.SQUARE_CALC);
                                        obj.UpdateCalcRez(0, 0);
                                    }
                                    else
                                    {
                                        int first_change = (ALLParamItem.Change_Object == ALLParamItem.Exist_Object) ? ALLParamItem.Exist_Object : ALLParamItem.Calc_Object;
                                        int second_change = (ALLParamItem.Change_Object == ALLParamItem.Exist_Object) ? ALLParamItem.Exist_Object : ALLParamItem.Calc_Object;
                                        CalcFlatKR(obj.Id, obj.KN_PARENT, obj.KN_KK, obj.KLADR_OBJECT_RF, 1, first_change, second_change, ALLParamItem.Exist_Object, out upksz, out ccalc);
                                        obj.UpdateCalcRez(0, 0);
                                        sts.Add3(obj.KN_PARENT, obj.KN_KK, 0, upksz);
                                    }
                                }
                            }
                        }
                        next = true;
                    }
                    fw.HideText();
                    if (next)
                    {
                        start += count;
                    }
                }
                fw.HideText();
            }
            #endregion
            #region Среднее (КР, КЛАДР)
            if (this.GroupAlgoritm_Code == KoGroupAlgoritm.AVG)
            {
                frmWait fw = new frmWait();
                fw.ShowText("Расчет помещений подгруппы " + this.FullName_SubGroup, 0, 0);
                ALLObjectItem[] objs = ALLObjectItem.GetObjectsBetween(0, 700000, this.CurGroup.Id, Id, false, true, false, false, false, true, false, false, false, ALLParamItem.Change_Object);
                int ccc = 0;
                ALLStatOKS sts = new ALLStatOKS();
                foreach (ALLObjectItem obj in objs)
                {
                    if (obj.CheckDocInput(id_doc))
                    {
                        fw.ShowText("Расчет помещений подгруппы " + this.FullName_SubGroup, objs.Length, ccc);
                        ccc++;

                        double upksz = 0;
                        double cupksz = 0;
                        string calc_obj = string.Empty;
                        if (sts.Get4("77", obj.KN_KK, 0, out cupksz, out calc_obj))
                        {
                            if (cupksz <= 0)
                                break;
                            obj.UpdateCalc(cupksz, cupksz * obj.SQUARE_CALC);
                            obj.UpdateCalcParent(calc_obj);
                            obj.UpdateCalcRez(0, 0);
                        }
                        else
                        {
                            CalcNoHarAVG(obj.Id, obj.KN_OBJECT, obj.KN_KK, "77", 0, 1, out upksz, out calc_obj);
                            if (upksz <= 0)
                                break;
                            obj.UpdateCalc(upksz, upksz * obj.SQUARE_CALC);
                            obj.UpdateCalcParent(calc_obj);
                            obj.UpdateCalcRez(0, 0);
                            sts.Add4("77", obj.KN_KK, 0, upksz, calc_obj);
                        }
                    }
                }
                fw.HideText();
            }
            #endregion
            #region Минимальное (КР, КЛАДР)
            if (this.GroupAlgoritm_Code == KoGroupAlgoritm.Min)
            {
                frmWait fw = new frmWait();
                fw.ShowText("Расчет помещений подгруппы " + this.FullName_SubGroup, 0, 0);
                ALLObjectItem[] objs = ALLObjectItem.GetObjectsBetween(0, 700000, this.CurGroup.Id, Id, false, true, false, false, false, true, false, false, false, ALLParamItem.Change_Object);
                int ccc = 0;
                ALLStatOKS sts = new ALLStatOKS();
                foreach (ALLObjectItem obj in objs)
                {
                    if (obj.CheckDocInput(id_doc))
                    {
                        fw.ShowText("Расчет помещений подгруппы " + this.FullName_SubGroup, objs.Length, ccc);
                        ccc++;

                        double upksz = 0;
                        double cupksz = 0;
                        string calc_obj = string.Empty;
                        if (sts.Get4("77", obj.KN_KK, 0, out cupksz, out calc_obj))
                        {
                            if (cupksz <= 0)
                                break;
                            obj.UpdateCalc(cupksz, cupksz * obj.SQUARE_CALC);
                            obj.UpdateCalcParent(calc_obj);
                            obj.UpdateCalcRez(0, 0);
                        }
                        else
                        {
                            CalcNoHarMin(obj.Id, obj.KN_OBJECT, obj.KN_KK, "77", 0, 1, out upksz, out calc_obj);
                            if (upksz <= 0)
                                break;
                            obj.UpdateCalc(upksz, upksz * obj.SQUARE_CALC);
                            obj.UpdateCalcParent(calc_obj);
                            obj.UpdateCalcRez(0, 0);
                            sts.Add4("77", obj.KN_KK, 0, upksz, calc_obj);
                        }
                    }
                }
                fw.HideText();
            }
            #endregion
            #endregion

            #region Сооружения
            #region Среднее (КР, КЛАДР)
            if ((this.Type_SubGroup == 10) & (construction))
            {
                frmWait fw = new frmWait();
                fw.ShowText("Расчет сооружений подгруппы " + this.FullName_SubGroup, 0, 0);
                ALLObjectItem[] objs = ALLObjectItem.GetObjectsBetween(0, 700000, this.CurGroup.Id, Id, false, false, true, false, false, true, false, false, false, ALLParamItem.Change_Object);
                int ccc = 0;
                ALLStatOKS sts = new ALLStatOKS();
                foreach (ALLObjectItem obj in objs)
                {
                    if (obj.CheckDocInput(id_doc))
                    {
                        fw.ShowText("Расчет сооружений подгруппы " + this.FullName_SubGroup, objs.Length, ccc);
                        ccc++;

                        double upksz = 0;
                        double cupksz = 0;
                        string calc_obj = string.Empty;
                        if (sts.Get4("77", obj.KN_KK, 2, out cupksz, out calc_obj))
                        {
                            obj.UpdateCalc(cupksz, cupksz * obj.SQUARE_CALC);
                            obj.UpdateCalcParent(calc_obj);
                            obj.UpdateCalcRez(0, 0);
                        }
                        else
                        {
                            CalcNoHarAVG(obj.Id, obj.KN_OBJECT, obj.KN_KK, "77", 2, 1, out upksz, out calc_obj);
                            obj.UpdateCalc(upksz, upksz * obj.SQUARE_CALC);
                            obj.UpdateCalcParent(calc_obj);
                            obj.UpdateCalcRez(0, 0);
                            sts.Add4("77", obj.KN_KK, 2, upksz, calc_obj);
                        }
                    }
                }
                fw.HideText();
            }
            #endregion
            #region Минимальное (КР, КЛАДР)
            if ((this.Type_SubGroup == 12) & (construction))
            {
                frmWait fw = new frmWait();
                fw.ShowText("Расчет сооружений подгруппы " + this.FullName_SubGroup, 0, 0);
                ALLObjectItem[] objs = ALLObjectItem.GetObjectsBetween(0, 700000, this.CurGroup.Id, Id, false, false, true, false, false, true, false, false, false, ALLParamItem.Change_Object);
                int ccc = 0;
                ALLStatOKS sts = new ALLStatOKS();
                foreach (ALLObjectItem obj in objs)
                {
                    if (obj.CheckDocInput(id_doc))
                    {
                        fw.ShowText("Расчет сооружений подгруппы " + this.FullName_SubGroup, objs.Length, ccc);
                        ccc++;

                        double upksz = 0;
                        double cupksz = 0;
                        string calc_obj = string.Empty;
                        if (sts.Get4("77", obj.KN_KK, 2, out cupksz, out calc_obj))
                        {
                            obj.UpdateCalc(cupksz, cupksz * obj.SQUARE_CALC);
                            obj.UpdateCalcParent(calc_obj);
                            obj.UpdateCalcRez(0, 0);
                        }
                        else
                        {
                            CalcNoHarMin(obj.Id, obj.KN_OBJECT, obj.KN_KK, "77", 2, 1, out upksz, out calc_obj);
                            obj.UpdateCalc(upksz, upksz * obj.SQUARE_CALC);
                            obj.UpdateCalcParent(calc_obj);
                            obj.UpdateCalcRez(0, 0);
                            sts.Add4("77", obj.KN_KK, 2, upksz, calc_obj);
                        }
                    }
                }
                fw.HideText();
            }
            #endregion
            #endregion

            #region Участки
            #region Среднее (КР, КЛАДР)
            if ((this.Type_SubGroup == 10) & (parcel))
            {
                frmWait fw = new frmWait();
                fw.ShowText("Расчет учатков подгруппы " + this.FullName_SubGroup, 0, 0);
                ALLObjectItem[] objs = ALLObjectItem.GetObjectsBetween(0, 700000, this.CurGroup.Id, Id, false, false, false, false, true, true, false, false, false, ALLParamItem.Change_Object);
                int ccc = 0;
                ALLStatOKS sts = new ALLStatOKS();
                foreach (ALLObjectItem obj in objs)
                {
                    if (obj.CheckDocInput(id_doc))
                    {
                        fw.ShowText("Расчет учатков подгруппы " + this.FullName_SubGroup, objs.Length, ccc);
                        ccc++;

                        double upksz = 0;
                        double cupksz = 0;
                        string calc_obj = string.Empty;
                        if (sts.Get4("77", obj.KN_KK, 2, out cupksz, out calc_obj))
                        {
                            obj.UpdateCalc(cupksz, cupksz * obj.SQUARE_CALC);
                            obj.UpdateCalcParent(calc_obj);
                            obj.UpdateCalcRez(0, 0);
                        }
                        else
                        {
                            CalcNoHarAVG(obj.Id, obj.KN_OBJECT, obj.KN_KK, "77", 3, 1, out upksz, out calc_obj);
                            obj.UpdateCalc(upksz, upksz * obj.SQUARE_CALC);
                            obj.UpdateCalcParent(calc_obj);
                            obj.UpdateCalcRez(0, 0);
                            sts.Add4("77", obj.KN_KK, 2, upksz, calc_obj);
                        }
                    }
                }
                fw.HideText();
            }
            #endregion
            #region Минимальное (КР, КЛАДР)
            if ((this.Type_SubGroup == 12) & (parcel))
            {
                frmWait fw = new frmWait();
                fw.ShowText("Расчет учатков подгруппы " + this.FullName_SubGroup, 0, 0);
                ALLObjectItem[] objs = ALLObjectItem.GetObjectsBetween(0, 700000, this.CurGroup.Id, Id, false, false, false, false, true, true, false, false, false, ALLParamItem.Change_Object);
                int ccc = 0;
                ALLStatOKS sts = new ALLStatOKS();
                foreach (ALLObjectItem obj in objs)
                {
                    if (obj.CheckDocInput(id_doc))
                    {
                        fw.ShowText("Расчет учатков подгруппы " + this.FullName_SubGroup, objs.Length, ccc);
                        ccc++;

                        double upksz = 0;
                        double cupksz = 0;
                        string calc_obj = string.Empty;
                        if (sts.Get4("77", obj.KN_KK, 2, out cupksz, out calc_obj))
                        {
                            obj.UpdateCalc(cupksz, cupksz * obj.SQUARE_CALC);
                            obj.UpdateCalcParent(calc_obj);
                            obj.UpdateCalcRez(0, 0);
                        }
                        else
                        {
                            CalcNoHarMin(obj.Id, obj.KN_OBJECT, obj.KN_KK, "77", 3, 1, out upksz, out calc_obj);
                            obj.UpdateCalc(upksz, upksz * obj.SQUARE_CALC);
                            obj.UpdateCalcParent(calc_obj);
                            obj.UpdateCalcRez(0, 0);
                            sts.Add4("77", obj.KN_KK, 2, upksz, calc_obj);
                        }
                    }
                }
                fw.HideText();
            }
            #endregion
            #endregion
            */
            #region ОНС
            if (this.GroupAlgoritm_Code == KoGroupAlgoritm.UnComplited)
            {
                OMTourGroup tourgroup = OMTourGroup.Where(x=>x.GroupId==this.Id).SelectAll().ExecuteFirstOrDefault();
                if (tourgroup != null)
                {
                    ALLStatOKS sts = new ALLStatOKS();

                    foreach (ObjectModel.KO.OMUnit unit in units)
                    {
                        OMUnitParamsOks2016 param = OMUnitParamsOks2016.Where(x => x.Id == unit.Id).SelectAll().ExecuteFirstOrDefault();
                        if (param != null)
                        {
                            decimal procent = 50;
                            if (param.Field153 != String.Empty) procent = Convert.ToInt32(param.Field153);

                            decimal square = Convert.ToDecimal(param.Field146);




                            decimal upksz = 0;
                            decimal pp = procent / 100;
                            string calc_obj = string.Empty;

                            if (!sts.Get(unit.CadastralBlock, PropertyTypes.Building, out upksz, out calc_obj))
                            {
                                GetAvgValue(tourgroup.TourId, unit.CadastralBlock, PropertyTypes.Building, CalcParentGroup, out upksz, out calc_obj);
                                sts.Add(unit.CadastralBlock, PropertyTypes.Building, upksz, calc_obj);
                            }

                            unit.UpksPre = upksz * pp;
                            unit.CadastralCostPre = unit.UpksPre * square;
                            unit.Upks = 0;
                            unit.CadastralCost = 0;
                            //obj.UpdateCalcParent(calc_obj);
                            unit.Save();

                        }
                    }
                }
            }
            #endregion

            //UpdateCalc(groups);

        }
    }

}
