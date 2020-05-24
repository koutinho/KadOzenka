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
        public KoParentCalcType Calc_obj_code { get; set; }
        #endregion

        #region Конструкторы и инициализация
        // создание по составляющим
        public ALLTmpItem(string aKK, PropertyTypes aType, decimal aUpks, string aCalc_obj, KoParentCalcType aCalc_obj_code)
        {
            KK = aKK;
            Type = aType;
            UPKSZ = aUpks;
            Calc_obj = aCalc_obj;
            Calc_obj_code = aCalc_obj_code;
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

        public void Add(string aKK, PropertyTypes aType, decimal aUpks, string aCalc_obj, KoParentCalcType aCalc_obj_code)
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
                        Valuetmp.Add(new ALLTmpItem(aKK, aType, aUpks, aCalc_obj, aCalc_obj_code));
                    }
                }

            }
        }
        public bool Get(string aKK, PropertyTypes aType, out decimal aUpks, out string aCalc_obj, out KoParentCalcType aCalc_obj_code)
        {
            bool find = false;
            aUpks = 0;
            aCalc_obj = string.Empty;
            aCalc_obj_code = KoParentCalcType.None;
            if (!String.IsNullOrEmpty(aKK))
            {
                ALLTmpItem item = Valuetmp.Find(x => ((x.KK == aKK) && (x.Type == aType)));
                if (item != null)
                {
                    find = true;
                    aUpks = item.UPKSZ;
                    aCalc_obj = item.Calc_obj;
                    aCalc_obj_code = item.Calc_obj_code;
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

        public CalcItem(long factorid, string value, string numeric)
        {
            if (value == string.Empty)
                Value = numeric;
            else
                Value = value;
            FactorId = factorid;
        }
    }
    public class HistoryUnit : IComparer<HistoryUnit>
    {
        public OMUnit Unit { get; set; }
        public OMTask Task { get; set; }
        public bool IsActual { get; set; }
        public bool IsBad { get; set; }
        public Core.TD.OMInstance InputDoc { get; set; }
        public Core.TD.OMInstance OutputDoc { get; set; }

        public HistoryUnit(OMUnit unit)
        {
            Unit = unit;
            IsBad = (Unit.StatusResultCalc_Code == KoStatusResultCalc.ErrorInData || Unit.StatusResultCalc_Code == KoStatusResultCalc.ErrorTechnical);
            IsActual = false;
            Task = OMTask.Where(x=>x.Id==unit.TaskId).SelectAll().ExecuteFirstOrDefault();
            if (Task!=null)
            {
                long? id_Indoc = Task.DocumentId;
                if (id_Indoc != null)
                {
                    InputDoc = Core.TD.OMInstance.Where(x => x.Id == id_Indoc.Value).SelectAll().ExecuteFirstOrDefault();
                }
                long? id_Outdoc = unit.ResponseDocId;
                if (id_Outdoc != null)
                {
                    OutputDoc = Core.TD.OMInstance.Where(x => x.Id == id_Outdoc.Value).SelectAll().ExecuteFirstOrDefault();
                }
            }
        }
        int IComparer<HistoryUnit>.Compare(HistoryUnit unit1, HistoryUnit unit2)
        {
            if (unit1.Unit.CreationDate.Value == unit2.Unit.CreationDate.Value)
            {
                return -1 * unit1.Unit.StatusResultCalc_Code.CompareTo(unit2.Unit.StatusResultCalc_Code);
            }
            else
                return unit1.Unit.CreationDate.Value.CompareTo(unit2.Unit.CreationDate.Value);
        }
        public override string ToString()
        {
            return Unit.CreationDate.Value.ToString("dd.MM.yyyy") + ", " + Unit.CadastralCost.Value.ToString() + ", " + Task.NoteType + ", "+Unit.StatusResultCalc;
        }

        public static List<HistoryUnit> GetHistory(string cadastralNumber)
        {
            List<HistoryUnit> Items = new List<HistoryUnit>();
            List<OMUnit> units = OMUnit.Where(x=>x.CadastralNumber==cadastralNumber).SelectAll().Execute();
            foreach(OMUnit unit in units)
            {
                Items.Add(new HistoryUnit(unit));
            }

            if (Items.Count > 0)
            {
                Items.Sort(Items[0]);
                int indexActual = Items.Count - 1;
                for (int i = Items.Count - 1; i > 0; i--)
                {
                    if (Items[i].Unit.CadastralCost == Items[i - 1].Unit.CadastralCost)
                    {
                        indexActual = i - 1;
                    }
                    else break;
                }
                Items[indexActual].IsActual = true;
            }
            return Items;
        }
        public static List<HistoryUnit> GetHistoryTour(OMUnit current)
        {
            List<HistoryUnit> Items = new List<HistoryUnit>();
            List<OMUnit> units = OMUnit.Where(x => x.CadastralNumber == current.CadastralNumber && x.TourId==current.TourId).SelectAll().Execute();
            foreach (OMUnit unit in units)
            {
                Items.Add(new HistoryUnit(unit));
            }

            if (Items.Count > 0)
            {
                Items.Sort(Items[0]);
                int indexActual = Items.Count - 1;
                for (int i = Items.Count - 1; i > 0; i--)
                {
                    if (Items[i].Unit.CadastralCost == Items[i - 1].Unit.CadastralCost)
                    {
                        indexActual = i - 1;
                    }
                    else break;
                }
                Items[indexActual].IsActual = true;
            }
            return Items;
        }
        public static List<HistoryUnit> GetPrevHistoryTour(OMUnit current)
        {
            List<HistoryUnit> Items = new List<HistoryUnit>();
            List<OMUnit> units = OMUnit.Where(x => x.CadastralNumber == current.CadastralNumber && x.TourId == current.TourId && x.Id!=current.Id).SelectAll().Execute();
            foreach (OMUnit unit in units)
            {
                Items.Add(new HistoryUnit(unit));
            }

            if (Items.Count > 0)
            {
                Items.Sort(Items[0]);
                int indexActual = Items.Count - 1;
                for (int i = Items.Count - 1; i > 0; i--)
                {
                    if (Items[i].Unit.CadastralCost == Items[i - 1].Unit.CadastralCost)
                    {
                        indexActual = i - 1;
                    }
                    else break;
                }
                Items[indexActual].IsActual = true;
            }
            return Items;
        }
        public static HistoryUnit GetPrevUnit(OMUnit current)
        {
            List<HistoryUnit> res= GetPrevHistoryTour(current);
            if (res.Count > 0) return res[0];
            else return null;
        }
    }
    public partial class OMUnit
    {
        private static readonly object LockObject = new Object();

        public long OldId { get; set; }
        public bool isExplication { get; set; }
        public void SaveAndCreate()
        {
            ObjectModel.Gbu.OMMainObject mainObject;

            lock (LockObject)
            {
                mainObject = ObjectModel.Gbu.OMMainObject
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
                        KadastrKvartal = this.CadastralBlock,
                    };

                    mainObject.Save();
                }
                else
                {
                    if (mainObject.ObjectType_Code != PropertyType_Code || mainObject.KadastrKvartal != CadastralBlock)
                    {
                        mainObject.ObjectType_Code = PropertyType_Code;
                        mainObject.KadastrKvartal = CadastralBlock;
                        mainObject.Save();
                    }
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
            return (upks ? ("УПКС=") : string.Empty) + ((str_koeff != string.Empty) ? "(" : string.Empty) + GetFormulaMain(false) + ((str_koeff != string.Empty) ? ")" : string.Empty) + str_koeff;
        }
        public string GetFormulaMain(bool upks)
        {
            string res = string.Empty;
            #region Моделирование
            if (ParentGroup == null)
                ParentGroup = ObjectModel.KO.OMGroup.Where(x => x.Id == GroupId).SelectAll().ExecuteFirstOrDefault();
            if (ModelFactor.Count == 0)
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
                if (ParentGroup.GroupFactor.Count == 0)
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
            MarkCatalogs.AddRange(OMMarkCatalog.Where(x => x.GroupId == model.GroupId && x.FactorId == this.FactorId).SelectAll().Execute());
        }
    }
    public partial class OMGroup
    {
        public static int? GetFactorReestrId(OMGroup current)
        {
            OMGroup ParentGroup = OMGroup.Where(x => x.Id == current.ParentId).SelectAll().ExecuteFirstOrDefault();
            if (ParentGroup != null) return GetFactorReestrId(ParentGroup);
            else
            {
                OMTourGroup tourgroup = OMTourGroup.Where(x => x.GroupId == current.Id).SelectAll().ExecuteFirstOrDefault();
                if (tourgroup != null)
                {
                    if (current.GroupAlgoritm_Code == KoGroupAlgoritm.MainOKS)
                    {
                        if (tourgroup.TourId == 2016) return 252;
                        else
                        if (tourgroup.TourId == 2018) return 250;
                        else
                            return null;
                    }
                    else
                    if (current.GroupAlgoritm_Code == KoGroupAlgoritm.MainParcel)
                    {
                        if (tourgroup.TourId == 2016) return 253;
                        else
                        if (tourgroup.TourId == 2018) return 251;
                        else
                            return null;
                    }
                    else
                        return null;

                }
                else
                    return null;
            }
        }
        public static int GetFactorReestrId(OMUnit current)
        {
            if (current != null)
            {
                if (current.PropertyType_Code != PropertyTypes.Stead)
                {
                    if (current.TourId == 2016) return 252;
                    else
                    if (current.TourId == 2018) return 250;
                    else
                        return 250;
                }
                else
                {
                    if (current.TourId == 2016) return 253;
                    else
                    if (current.TourId == 2018) return 251;
                    else
                        return 251;
                }
            }
            else
                return 251;
        }
        public static List<ObjectModel.KO.OMGroup> GetListGroupTour(long tourId, ObjectModel.Directory.KoGroupAlgoritm GroupAlgoritm)
        {
            List<ObjectModel.KO.OMGroup> res = new List<ObjectModel.KO.OMGroup>();
            List<ObjectModel.KO.OMTourGroup> tourGroups = ObjectModel.KO.OMTourGroup.Where(x => x.TourId == tourId).SelectAll().Execute();
            foreach (ObjectModel.KO.OMTourGroup tourGroup in tourGroups)
            {
                ObjectModel.KO.OMGroup group = ObjectModel.KO.OMGroup.Where(x => x.Id == tourGroup.GroupId).SelectAll().ExecuteFirstOrDefault();
                if (group != null)
                {
                    ObjectModel.KO.OMGroup parentgroup = ObjectModel.KO.OMGroup.Where(x => x.Id == group.ParentId).SelectAll().ExecuteFirstOrDefault();
                    if (parentgroup != null)
                    {
                        if (parentgroup.GroupAlgoritm_Code == GroupAlgoritm) res.Add(group);
                    }
                }
            }
            return res;
        }
        private static void GetMinValue(ref ALLStatOKS minKR, ref ALLStatOKS minKS, long tourId, string kk, PropertyTypes type, List<long> calcChildGroups, out decimal upks, out string parentCalcObject, out KoParentCalcType parentCalcType)
        {
            upks = decimal.MaxValue;
            parentCalcType = KoParentCalcType.None;
            parentCalcObject = string.Empty;
            bool prFindInCadastralBlock = false;
            bool prFindInCadastralRaion = false;
            bool prFindInCadastralRegion = false;
            string kr = kk.Substring(0, 5);

            #region поиск по кварталу
            foreach (long calcChildGroup in calcChildGroups)
            {
                List<OMUnit> unitsKK = OMUnit.Where(x => x.TourId == tourId && x.Status_Code == KoUnitStatus.Initial && x.GroupId == calcChildGroup && x.CadastralBlock == kk && x.PropertyType_Code == type).SelectAll().Execute();
                if (unitsKK.Count > 0)
                {
                    prFindInCadastralBlock = true;
                    foreach (OMUnit unit in unitsKK)
                    {
                        if (unit.Upks != null)
                        {
                            if (unit.Upks < upks)
                            {
                                upks = unit.Upks.ParseToDecimal();
                            }
                        }
                    }
                }
            }
            #endregion
            #region поиск по району
            if (!prFindInCadastralBlock)
            {
                if (!minKR.Get(kr, PropertyTypes.Building, out upks, out parentCalcObject, out parentCalcType))
                {
                    upks = decimal.MaxValue;
                    foreach (long calcChildGroup in calcChildGroups)
                    {
                        List<OMUnit> unitsKR = OMUnit.Where(x => x.TourId == tourId && x.Status_Code == KoUnitStatus.Initial && x.GroupId == calcChildGroup && x.PropertyType_Code == type && x.CadastralBlock.Contains(kr)).SelectAll().Execute();
                        if (unitsKR.Count > 0)
                        {
                            foreach (OMUnit unit in unitsKR)
                            {
                                if (unit.CadastralBlock.Substring(0, 5) == kr)
                                {
                                    if (unit.CadastralCost != null)
                                    {
                                        if (unit.Upks < upks && unit.Upks > 0)
                                        {
                                            prFindInCadastralRaion = true;
                                            upks = unit.Upks.ParseToDecimal();
                                        }
                                    }
                                }
                            }
                        }
                    }
                    minKR.Add(kr, PropertyTypes.Building, upks, kr, KoParentCalcType.CadastralRegion);
                }
                else
                {
                    prFindInCadastralRaion = true;
                }
            }
            #endregion
            #region поиск по субъекту
            if (!prFindInCadastralRaion && !prFindInCadastralBlock)
            {
                if (!minKS.Get("Субъект РФ", PropertyTypes.Building, out upks, out parentCalcObject, out parentCalcType))
                {
                    upks = decimal.MaxValue;
                    foreach (long calcChildGroup in calcChildGroups)
                    {
                        List<OMUnit> unitsKS = OMUnit.Where(x => x.TourId == tourId && x.Status_Code == KoUnitStatus.Initial && x.GroupId == calcChildGroup && x.PropertyType_Code == type).SelectAll().Execute();
                        if (unitsKS.Count > 0)
                        {
                            prFindInCadastralRegion = true;
                            foreach (OMUnit unit in unitsKS)
                            {
                                if (unit.CadastralCost != null)
                                {
                                    if (unit.Upks < upks)
                                    {
                                        upks = unit.Upks.ParseToDecimal();
                                    }
                                }
                            }
                        }
                    }
                    minKS.Add("Субъект РФ", PropertyTypes.Building, upks, kr, KoParentCalcType.RfSubject);
                }
                else
                {
                    prFindInCadastralRegion = true;
                }
            }
            #endregion

            if (prFindInCadastralBlock || prFindInCadastralRaion || prFindInCadastralRegion)
            {
                if (prFindInCadastralBlock)
                {
                    parentCalcObject = kk;
                    parentCalcType = KoParentCalcType.CadastralBlock;
                }
                else
                if (prFindInCadastralRaion)
                {
                    parentCalcObject = kk.Substring(0, 5);
                    parentCalcType = KoParentCalcType.CadastralRegion;
                }
                else
                if (prFindInCadastralRegion)
                {
                    parentCalcObject = "Субъект РФ";
                    parentCalcType = KoParentCalcType.RfSubject;
                }
            }
        }
        private static void GetAvgValue(ref ALLStatOKS avgKR, ref ALLStatOKS avgKS, long tourId, string kk, PropertyTypes type, List<long> calcChildGroups, out decimal upks, out string parentCalcObject, out KoParentCalcType parentCalcType)
        {
            upks = 0;
            parentCalcType = KoParentCalcType.None;
            parentCalcObject = string.Empty;
            bool prFindInCadastralBlock = false;
            bool prFindInCadastralRaion = false;
            bool prFindInCadastralRegion = false;
            string kr = kk.Substring(0, 5);
            decimal sumSquare = 0;
            decimal sumCost = 0;

            #region поиск по кварталу
            foreach (long calcChildGroup in calcChildGroups)
            {
                List<OMUnit> unitsKK = OMUnit.Where(x => x.TourId == tourId && x.Status_Code == KoUnitStatus.Initial && x.GroupId == calcChildGroup && x.CadastralBlock == kk && x.PropertyType_Code == type).SelectAll().Execute();
                if (unitsKK.Count > 0)
                {
                    prFindInCadastralBlock = true;
                    foreach (OMUnit unit in unitsKK)
                    {
                        if (unit.Square != null && unit.CadastralCost != null)
                        {
                            sumSquare += unit.Square.ParseToDecimal();
                            sumCost += unit.CadastralCost.ParseToDecimal();
                        }
                    }
                    if (sumSquare > 0)
                        upks = Math.Round(sumCost / sumSquare, 2, MidpointRounding.AwayFromZero);
                }
            }
            #endregion
            #region поиск по району
            if (!prFindInCadastralBlock)
            {
                if (!avgKR.Get(kr, PropertyTypes.Building, out upks, out parentCalcObject, out parentCalcType))
                {
                    upks = 0;
                    foreach (long calcChildGroup in calcChildGroups)
                    {
                        List<OMUnit> unitsKR = OMUnit.Where(x => x.TourId == tourId && x.Status_Code == KoUnitStatus.Initial && x.GroupId == calcChildGroup && x.PropertyType_Code == type && x.CadastralBlock.Contains(kr)).SelectAll().Execute();
                        if (unitsKR.Count > 0)
                        {
                            foreach (OMUnit unit in unitsKR)
                            {
                                if (unit.CadastralBlock.Substring(0, 5) == kr)
                                {
                                    if (unit.Square != null && unit.CadastralCost != null)
                                    {
                                        sumSquare += unit.Square.ParseToDecimal();
                                        sumCost += unit.CadastralCost.ParseToDecimal();
                                    }
                                }
                            }
                        }
                    }
                    if (sumSquare > 0)
                        upks = Math.Round(sumCost / sumSquare, 2, MidpointRounding.AwayFromZero);

                    avgKR.Add(kr, PropertyTypes.Building, upks, kr, KoParentCalcType.CadastralRegion);
                }
                else
                {
                    prFindInCadastralRaion = true;
                }
            }
            #endregion
            #region поиск по субъекту
            if (!prFindInCadastralRaion && !prFindInCadastralBlock)
            {
                if (!avgKS.Get("Субъект РФ", PropertyTypes.Building, out upks, out parentCalcObject, out parentCalcType))
                {
                    upks = 0;
                    foreach (long calcChildGroup in calcChildGroups)
                    {
                        List<OMUnit> unitsKS = OMUnit.Where(x => x.TourId == tourId && x.Status_Code == KoUnitStatus.Initial && x.GroupId == calcChildGroup && x.PropertyType_Code == type).SelectAll().Execute();
                        if (unitsKS.Count > 0)
                        {
                            prFindInCadastralRegion = true;
                            foreach (OMUnit unit in unitsKS)
                            {
                                if (unit.Square != null && unit.CadastralCost != null)
                                {
                                    sumSquare += unit.Square.ParseToDecimal();
                                    sumCost += unit.CadastralCost.ParseToDecimal();
                                }
                            }
                        }
                    }
                    if (sumSquare > 0)
                        upks = Math.Round(sumCost / sumSquare, 2, MidpointRounding.AwayFromZero);
                    avgKS.Add("Субъект РФ", PropertyTypes.Building, upks, kr, KoParentCalcType.RfSubject);
                }
                else
                {
                    prFindInCadastralRegion = true;
                }
            }
            #endregion

            if (prFindInCadastralBlock || prFindInCadastralRaion || prFindInCadastralRegion)
            {
                if (prFindInCadastralBlock)
                {
                    parentCalcObject = kk;
                    parentCalcType = KoParentCalcType.CadastralBlock;
                }
                else
                if (prFindInCadastralRaion)
                {
                    parentCalcObject = kk.Substring(0, 5);
                    parentCalcType = KoParentCalcType.CadastralRegion;
                }
                else
                if (prFindInCadastralRegion)
                {
                    parentCalcObject = "Субъект РФ";
                    parentCalcType = KoParentCalcType.RfSubject;
                }
            }
        }
        private void Calculate(List<ObjectModel.KO.OMUnit> units, List<long> CalcParentGroup, PropertyTypes curTypeObject)
        {
            #region Моделирование
            if (this.GroupAlgoritm_Code == KoGroupAlgoritm.Model || this.GroupAlgoritm_Code == KoGroupAlgoritm.Etalon)
            {
                int? factorReestrId = GetFactorReestrId(this);
                OMModel model = OMModel.Where(x => x.GroupId == this.Id).SelectAll().ExecuteFirstOrDefault();
                if (model != null && factorReestrId != null)
                {
                    if (model.ModelFactor.Count == 0)
                        model.ModelFactor = OMModelFactor.Where(x => x.ModelId == model.Id).SelectAll().Execute();

                    long? idsquarefactor = null;
                    long? idanalogfactor = null;

                    foreach (OMModelFactor weight in model.ModelFactor)
                    {
                        weight.FillMarkCatalogs(model);
                        if (weight.MarkerId == 1)
                        {
                            idsquarefactor = weight.FactorId;
                        }
                        if (weight.MarkerId == 2)
                        {
                            idanalogfactor = weight.FactorId;
                        }
                    }

                    int ccc = 0;

                    CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
                    ParallelOptions options = new ParallelOptions
                    {
                        CancellationToken = cancelTokenSource.Token,
                        MaxDegreeOfParallelism = 1
                    };


                    Parallel.ForEach(units, options, unit =>
                    {
                        bool useAsPrototype = false;
                        if (unit.UseAsPrototype != null) useAsPrototype = unit.UseAsPrototype.Value;
                        if (this.GroupAlgoritm_Code == KoGroupAlgoritm.Model || (this.GroupAlgoritm_Code == KoGroupAlgoritm.Etalon && useAsPrototype))
                        {
                            List<CalcItem> FactorValues = new List<CalcItem>();
                            DataTable data = RegisterStorage.GetAttributes((int)unit.Id, factorReestrId.Value);
                            if (data != null)
                            {
                                foreach (DataRow row in data.Rows)
                                {
                                    FactorValues.Add(new CalcItem(row.ItemArray[1].ParseToLong(), row.ItemArray[6].ParseToString(), row.ItemArray[7].ParseToString()));
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

                                string strerror = "";

                                foreach (OMModelFactor weight in model.ModelFactor)
                                {
                                    if (weight.SignAdd)
                                    {
                                        string factorValue = string.Empty;//TODO: значение фактора для данного объекта
                                        if (weight.FactorId == idsquarefactor)
                                        {
                                            decimal curSq = (unit.Square == null) ? 1 : unit.Square.Value;
                                            if (unit.isExplication)
                                            {
                                                decimal sumsq = 0;
                                                List<OMExplication> unitExplications = OMExplication.Where(x => x.ObjectId == unit.Id).SelectAll().Execute();
                                                foreach (OMExplication unitExplication in unitExplications)
                                                {
                                                    if (unitExplication.GroupId != this.Id)
                                                    {
                                                        if (unitExplication.Square != null)
                                                            sumsq += unitExplication.Square.Value;
                                                    }
                                                }
                                                factorValue = (curSq - sumsq).ParseToString();
                                            }
                                            else
                                            {
                                                factorValue = curSq.ParseToString();
                                            }
                                        }
                                        else
                                        if (weight.FactorId == idanalogfactor)
                                        {
                                            if (unit.isExplication)
                                            {
                                                List<OMExplication> unitExplications = OMExplication.Where(x => x.ObjectId == unit.Id && x.GroupId == this.Id).SelectAll().Execute();
                                                foreach (OMExplication unitExplication in unitExplications)
                                                {
                                                    factorValue = unitExplication.NameAnalog;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            CalcItem ci = FactorValues.Find(x => x.FactorId == weight.FactorId);
                                            if (ci != null) factorValue = ci.Value;
                                        }


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
                                            bool dok = decimal.TryParse(factorValue, out decimal d);
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

                                        if (weight.FactorId == idsquarefactor)
                                        {
                                            decimal curSq = (unit.Square == null) ? 1 : unit.Square.Value;
                                            if (unit.isExplication)
                                            {
                                                decimal sumsq = 0;
                                                List<OMExplication> unitExplications = OMExplication.Where(x => x.ObjectId == unit.Id).SelectAll().Execute();
                                                foreach (OMExplication unitExplication in unitExplications)
                                                {
                                                    if (unitExplication.GroupId != this.Id)
                                                    {
                                                        if (unitExplication.Square != null)
                                                            sumsq += unitExplication.Square.Value;
                                                    }
                                                }
                                                factorValue = (curSq - sumsq).ParseToString();
                                            }
                                            else
                                            if (weight.FactorId == idanalogfactor)
                                            {
                                                if (unit.isExplication)
                                                {
                                                    List<OMExplication> unitExplications = OMExplication.Where(x => x.ObjectId == unit.Id && x.GroupId == this.Id).SelectAll().Execute();
                                                    foreach (OMExplication unitExplication in unitExplications)
                                                    {
                                                        factorValue = unitExplication.NameAnalog;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                factorValue = curSq.ParseToString();
                                            }
                                        }
                                        else
                                        {
                                            CalcItem ci = FactorValues.Find(x => x.FactorId == weight.FactorId);
                                            if (ci != null) factorValue = ci.Value;
                                        }

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
                                            bool dok = decimal.TryParse(factorValue, out decimal d);
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
                                    if (unit.CadastralCostPre != Cost || unit.UpksPre != UPKS)
                                        Console.WriteLine("КН:{5} УПКС:{0} УПКС:{1} КС:{2} КС:{3} SQ:{4}", unit.UpksPre, UPKS, unit.CadastralCostPre, Cost, unit.Square, unit.CadastralNumber);
                                    if (!unit.isExplication)
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
                                        unit.Save();
                                        if (this.GroupAlgoritm_Code == KoGroupAlgoritm.Etalon)
                                            CalculateChildForEtalon(unit);
                                    }
                                    else
                                    {
                                        List<OMExplication> unitExplications = OMExplication.Where(x => x.ObjectId == unit.Id && x.GroupId == this.Id).SelectAll().Execute();
                                        foreach (OMExplication unitExplication in unitExplications)
                                        {
                                            if (strerror != string.Empty)
                                            {
                                                unitExplication.Upks = 0;
                                                unitExplication.Kc = 0;
                                            }
                                            else
                                            {
                                                unitExplication.Upks = UPKS;
                                                unitExplication.Kc = Math.Round((UPKS * unitExplication.Square).ParseToDecimal(), 2, MidpointRounding.AwayFromZero);
                                            }
                                            unitExplication.Save();
                                        }
                                    }
                                }
                                if (model.AlgoritmType_Code == KoAlgoritmType.Line)
                                {
                                    decimal UPKS = Math.Round(Convert.ToDecimal(model.A0 + D), 2);
                                    decimal Cost = Math.Round((UPKS * unit.Square).ParseToDecimal(), 2);

                                    if (unit.UpksPre != UPKS || unit.CadastralCostPre != Cost)
                                        Console.WriteLine("УПКС:{0} УПКС:{1} КС:{2} КС:{3}", unit.UpksPre, UPKS, unit.CadastralCostPre, Cost);

                                    if (!unit.isExplication)
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
                                        unit.Save();
                                        if (this.GroupAlgoritm_Code == KoGroupAlgoritm.Etalon)
                                            CalculateChildForEtalon(unit);
                                    }
                                    else
                                    {
                                        List<OMExplication> unitExplications = OMExplication.Where(x => x.ObjectId == unit.Id && x.GroupId == this.Id).SelectAll().Execute();
                                        foreach (OMExplication unitExplication in unitExplications)
                                        {
                                            if (strerror != string.Empty)
                                            {
                                                unitExplication.Upks = 0;
                                                unitExplication.Kc = 0;
                                            }
                                            else
                                            {
                                                unitExplication.Upks = UPKS;
                                                unitExplication.Kc = Math.Round((UPKS * unitExplication.Square).ParseToDecimal(), 2, MidpointRounding.AwayFromZero);
                                            }
                                            unitExplication.Save();
                                        }
                                    }
                                }
                                if (model.AlgoritmType_Code == KoAlgoritmType.Multi)
                                {
                                    decimal UPKS = Math.Round(Convert.ToDecimal(model.A0 * Dm), 2);
                                    decimal Cost = Math.Round((UPKS * unit.Square).ParseToDecimal(), 2);
                                    if (unit.UpksPre != UPKS || unit.CadastralCostPre != Cost)
                                        Console.WriteLine("УПКС:{0} УПКС:{1} КС:{2} КС:{3}", unit.UpksPre, UPKS, unit.CadastralCostPre, Cost);
                                    if (!unit.isExplication)
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
                                        unit.Save();
                                        if (this.GroupAlgoritm_Code == KoGroupAlgoritm.Etalon)
                                            CalculateChildForEtalon(unit);
                                    }
                                    else
                                    {
                                        List<OMExplication> unitExplications = OMExplication.Where(x => x.ObjectId == unit.Id && x.GroupId == this.Id).SelectAll().Execute();
                                        foreach (OMExplication unitExplication in unitExplications)
                                        {
                                            if (strerror != string.Empty)
                                            {
                                                unitExplication.Upks = 0;
                                                unitExplication.Kc = 0;
                                            }
                                            else
                                            {
                                                unitExplication.Upks = UPKS;
                                                unitExplication.Kc = Math.Round((UPKS * unitExplication.Square).ParseToDecimal(), 2, MidpointRounding.AwayFromZero);
                                            }
                                            unitExplication.Save();
                                        }
                                    }
                                }
                            }
                        }
                    });
                }
            }
            #endregion

            #region Здания
            /*
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
            */
            #region Среднее (КР, КЛАДР)
            if (this.GroupAlgoritm_Code == KoGroupAlgoritm.AVG && curTypeObject == PropertyTypes.Building)
            {
                OMTourGroup tourgroup = OMTourGroup.Where(x => x.GroupId == this.Id).SelectAll().ExecuteFirstOrDefault();
                if (tourgroup != null)
                {
                    ALLStatOKS avgKK = new ALLStatOKS();
                    ALLStatOKS avgKR = new ALLStatOKS();
                    ALLStatOKS avgKS = new ALLStatOKS();
                    int countIndex = 0;
                    foreach (ObjectModel.KO.OMUnit unit in units)
                    {
                        {
                            countIndex++;
                            if (countIndex % 50 == 0)
                                Console.WriteLine(countIndex);

                            decimal square = 1;
                            if (unit.Square != null && unit.Square != 0) square = unit.Square.ParseToDecimal();

                            decimal upksz = 0;
                            string calc_obj = string.Empty;
                            KoParentCalcType calc_obj_code = KoParentCalcType.None;

                            if (!avgKK.Get(unit.CadastralBlock, PropertyTypes.Building, out upksz, out calc_obj, out calc_obj_code))
                            {
                                GetAvgValue(ref avgKR, ref avgKS, tourgroup.TourId, unit.CadastralBlock, PropertyTypes.Building, CalcParentGroup, out upksz, out calc_obj, out calc_obj_code);
                                avgKK.Add(unit.CadastralBlock, PropertyTypes.Building, upksz, calc_obj, calc_obj_code);
                            }

                            decimal cost = Math.Round(upksz * square, 2, MidpointRounding.AwayFromZero);

                            if (unit.UpksPre != upksz || unit.CadastralCostPre != cost)
                                Console.WriteLine("КН:{4} УПКС:{0} УПКС:{1} КС:{2} КС:{3} po:{5} pn:{6}", unit.UpksPre, upksz, unit.CadastralCostPre, cost, unit.CadastralNumber, unit.ParentCalcNumber, calc_obj);
                            unit.UpksPre = upksz;
                            unit.CadastralCostPre = cost;
                            unit.Upks = 0;
                            unit.CadastralCost = 0;
                            unit.ParentCalcNumber = calc_obj;
                            unit.ParentCalcType_Code = calc_obj_code;
                            //unit.Save();
                        }
                    }
                }
            }
            #endregion
            #region Минимальное (КР, КЛАДР)
            if (this.GroupAlgoritm_Code == KoGroupAlgoritm.Min && curTypeObject == PropertyTypes.Building)
            {
                OMTourGroup tourgroup = OMTourGroup.Where(x => x.GroupId == this.Id).SelectAll().ExecuteFirstOrDefault();
                if (tourgroup != null)
                {
                    ALLStatOKS minKK = new ALLStatOKS();
                    ALLStatOKS minKR = new ALLStatOKS();
                    ALLStatOKS minKS = new ALLStatOKS();
                    int countIndex = 0;
                    foreach (ObjectModel.KO.OMUnit unit in units)
                    {
                        {

                            countIndex++;
                            if (countIndex % 50 == 0)
                                Console.WriteLine(countIndex);

                            decimal square = 1;
                            if (unit.Square != null && unit.Square != 0) square = unit.Square.ParseToDecimal();

                            decimal upksz = 0;
                            string calc_obj = string.Empty;
                            KoParentCalcType calc_obj_code = KoParentCalcType.None;

                            if (!minKK.Get(unit.CadastralBlock, PropertyTypes.Building, out upksz, out calc_obj, out calc_obj_code))
                            {
                                GetMinValue(ref minKR, ref minKS, tourgroup.TourId, unit.CadastralBlock, PropertyTypes.Building, CalcParentGroup, out upksz, out calc_obj, out calc_obj_code);
                                minKK.Add(unit.CadastralBlock, PropertyTypes.Building, upksz, calc_obj, calc_obj_code);
                            }

                            decimal cost = Math.Round(upksz * square, 2, MidpointRounding.AwayFromZero);

                            if (unit.UpksPre != upksz || unit.CadastralCostPre != cost)
                                Console.WriteLine("КН:{4} УПКС:{0} УПКС:{1} КС:{2} КС:{3} po:{5} pn:{6}", unit.UpksPre, upksz, unit.CadastralCostPre, cost, unit.CadastralNumber, unit.ParentCalcNumber, calc_obj);
                            unit.UpksPre = upksz;
                            unit.CadastralCostPre = cost;
                            unit.Upks = 0;
                            unit.CadastralCost = 0;
                            unit.ParentCalcNumber = calc_obj;
                            unit.ParentCalcType_Code = calc_obj_code;
                            //unit.Save();
                        }
                    }
                }
            }
            #endregion

            #endregion

            #region Помещения
            /*
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
            */

            #region Среднее (КР, КЛАДР)
            if (this.GroupAlgoritm_Code == KoGroupAlgoritm.AVG && curTypeObject == PropertyTypes.Pllacement)
            {
                OMTourGroup tourgroup = OMTourGroup.Where(x => x.GroupId == this.Id).SelectAll().ExecuteFirstOrDefault();
                if (tourgroup != null)
                {
                    ALLStatOKS avgKK = new ALLStatOKS();
                    ALLStatOKS avgKR = new ALLStatOKS();
                    ALLStatOKS avgKS = new ALLStatOKS();
                    int countIndex = 0;
                    foreach (ObjectModel.KO.OMUnit unit in units)
                    {
                        {
                            countIndex++;
                            if (countIndex % 50 == 0)
                                Console.WriteLine(countIndex);

                            decimal square = 1;
                            if (unit.Square != null && unit.Square != 0) square = unit.Square.ParseToDecimal();

                            decimal upksz = 0;
                            string calc_obj = string.Empty;
                            KoParentCalcType calc_obj_code = KoParentCalcType.None;

                            if (!avgKK.Get(unit.CadastralBlock, PropertyTypes.Pllacement, out upksz, out calc_obj, out calc_obj_code))
                            {
                                GetAvgValue(ref avgKR, ref avgKS, tourgroup.TourId, unit.CadastralBlock, PropertyTypes.Pllacement, CalcParentGroup, out upksz, out calc_obj, out calc_obj_code);
                                avgKK.Add(unit.CadastralBlock, PropertyTypes.Pllacement, upksz, calc_obj, calc_obj_code);
                            }

                            decimal cost = Math.Round(upksz * square, 2, MidpointRounding.AwayFromZero);

                            //if (unit.UpksPre != upksz || unit.CadastralCostPre != cost)
                            //    Console.WriteLine("КН:{4} УПКС:{0} УПКС:{1} КС:{2} КС:{3} po:{5} pn:{6}", unit.UpksPre, upksz, unit.CadastralCostPre, cost, unit.CadastralNumber, unit.ParentCalcNumber, calc_obj);
                            unit.UpksPre = upksz;
                            unit.CadastralCostPre = cost;
                            unit.Upks = 0;
                            unit.CadastralCost = 0;
                            unit.ParentCalcNumber = calc_obj;
                            unit.ParentCalcType_Code = calc_obj_code;
                            unit.Save();
                        }
                    }
                }
            }
            #endregion
            #region Минимальное (КР, КЛАДР)
            if (this.GroupAlgoritm_Code == KoGroupAlgoritm.Min && curTypeObject == PropertyTypes.Pllacement)
            {
                OMTourGroup tourgroup = OMTourGroup.Where(x => x.GroupId == this.Id).SelectAll().ExecuteFirstOrDefault();
                if (tourgroup != null)
                {
                    ALLStatOKS minKK = new ALLStatOKS();
                    ALLStatOKS minKR = new ALLStatOKS();
                    ALLStatOKS minKS = new ALLStatOKS();
                    int countIndex = 0;
                    foreach (ObjectModel.KO.OMUnit unit in units)
                    {
                        {

                            countIndex++;
                            if (countIndex % 50 == 0)
                                Console.WriteLine(countIndex);

                            decimal square = 1;
                            if (unit.Square != null && unit.Square != 0) square = unit.Square.ParseToDecimal();

                            decimal upksz = 0;
                            string calc_obj = string.Empty;
                            KoParentCalcType calc_obj_code = KoParentCalcType.None;

                            if (!minKK.Get(unit.CadastralBlock, PropertyTypes.Pllacement, out upksz, out calc_obj, out calc_obj_code))
                            {
                                GetMinValue(ref minKR, ref minKS, tourgroup.TourId, unit.CadastralBlock, PropertyTypes.Pllacement, CalcParentGroup, out upksz, out calc_obj, out calc_obj_code);
                                minKK.Add(unit.CadastralBlock, PropertyTypes.Pllacement, upksz, calc_obj, calc_obj_code);
                            }

                            decimal cost = Math.Round(upksz * square, 2, MidpointRounding.AwayFromZero);

                            //if (unit.UpksPre != upksz || unit.CadastralCostPre != cost)
                            //    Console.WriteLine("КН:{4} УПКС:{0} УПКС:{1} КС:{2} КС:{3} po:{5} pn:{6}", unit.UpksPre, upksz, unit.CadastralCostPre, cost, unit.CadastralNumber, unit.ParentCalcNumber, calc_obj);
                            unit.UpksPre = upksz;
                            unit.CadastralCostPre = cost;
                            unit.Upks = 0;
                            unit.CadastralCost = 0;
                            unit.ParentCalcNumber = calc_obj;
                            unit.ParentCalcType_Code = calc_obj_code;
                            unit.Save();
                        }
                    }
                }
            }
            #endregion
            #endregion

            #region Сооружения
            #region Среднее
            if (this.GroupAlgoritm_Code == KoGroupAlgoritm.AVG && curTypeObject == PropertyTypes.Construction)
            {
                OMTourGroup tourgroup = OMTourGroup.Where(x => x.GroupId == this.Id).SelectAll().ExecuteFirstOrDefault();
                if (tourgroup != null)
                {
                    ALLStatOKS avgKK = new ALLStatOKS();
                    ALLStatOKS avgKR = new ALLStatOKS();
                    ALLStatOKS avgKS = new ALLStatOKS();
                    int countIndex = 0;
                    foreach (ObjectModel.KO.OMUnit unit in units)
                    {
                        {
                            countIndex++;
                            if (countIndex % 50 == 0)
                                Console.WriteLine(countIndex);

                            decimal square = 1;
                            if (unit.Square != null && unit.Square != 0) square = unit.Square.ParseToDecimal();

                            decimal upksz = 0;
                            string calc_obj = string.Empty;
                            KoParentCalcType calc_obj_code = KoParentCalcType.None;

                            if (!avgKK.Get(unit.CadastralBlock, PropertyTypes.Construction, out upksz, out calc_obj, out calc_obj_code))
                            {
                                GetAvgValue(ref avgKR, ref avgKS, tourgroup.TourId, unit.CadastralBlock, PropertyTypes.Construction, CalcParentGroup, out upksz, out calc_obj, out calc_obj_code);
                                avgKK.Add(unit.CadastralBlock, PropertyTypes.Construction, upksz, calc_obj, calc_obj_code);
                            }

                            decimal cost = Math.Round(upksz * square, 2, MidpointRounding.AwayFromZero);

                            //if (unit.UpksPre != upksz || unit.CadastralCostPre != cost)
                            //    Console.WriteLine("КН:{4} УПКС:{0} УПКС:{1} КС:{2} КС:{3} po:{5} pn:{6}", unit.UpksPre, upksz, unit.CadastralCostPre, cost, unit.CadastralNumber, unit.ParentCalcNumber, calc_obj);
                            unit.UpksPre = upksz;
                            unit.CadastralCostPre = cost;
                            unit.Upks = 0;
                            unit.CadastralCost = 0;
                            unit.ParentCalcNumber = calc_obj;
                            unit.ParentCalcType_Code = calc_obj_code;
                            unit.Save();
                        }
                    }
                }
            }
            #endregion
            #region Минимальное
            if (this.GroupAlgoritm_Code == KoGroupAlgoritm.Min && curTypeObject == PropertyTypes.Construction)
            {
                OMTourGroup tourgroup = OMTourGroup.Where(x => x.GroupId == this.Id).SelectAll().ExecuteFirstOrDefault();
                if (tourgroup != null)
                {
                    ALLStatOKS minKK = new ALLStatOKS();
                    ALLStatOKS minKR = new ALLStatOKS();
                    ALLStatOKS minKS = new ALLStatOKS();
                    int countIndex = 0;
                    foreach (ObjectModel.KO.OMUnit unit in units)
                    {
                        {

                            countIndex++;
                            if (countIndex % 50 == 0)
                                Console.WriteLine(countIndex);

                            decimal square = 1;
                            if (unit.Square != null && unit.Square != 0) square = unit.Square.ParseToDecimal();

                            decimal upksz = 0;
                            string calc_obj = string.Empty;
                            KoParentCalcType calc_obj_code = KoParentCalcType.None;

                            if (!minKK.Get(unit.CadastralBlock, PropertyTypes.Construction, out upksz, out calc_obj, out calc_obj_code))
                            {
                                GetMinValue(ref minKR, ref minKS, tourgroup.TourId, unit.CadastralBlock, PropertyTypes.Construction, CalcParentGroup, out upksz, out calc_obj, out calc_obj_code);
                                minKK.Add(unit.CadastralBlock, PropertyTypes.Construction, upksz, calc_obj, calc_obj_code);
                            }

                            decimal cost = Math.Round(upksz * square, 2, MidpointRounding.AwayFromZero);

                            //if (unit.UpksPre != upksz || unit.CadastralCostPre != cost)
                            //    Console.WriteLine("КН:{4} УПКС:{0} УПКС:{1} КС:{2} КС:{3} po:{5} pn:{6}", unit.UpksPre, upksz, unit.CadastralCostPre, cost, unit.CadastralNumber, unit.ParentCalcNumber, calc_obj);
                            unit.UpksPre = upksz;
                            unit.CadastralCostPre = cost;
                            unit.Upks = 0;
                            unit.CadastralCost = 0;
                            unit.ParentCalcNumber = calc_obj;
                            unit.ParentCalcType_Code = calc_obj_code;
                            unit.Save();
                        }
                    }
                }
            }
            #endregion
            #endregion

            #region Участки
            #region Среднее
            if (this.GroupAlgoritm_Code == KoGroupAlgoritm.AVG && curTypeObject == PropertyTypes.Stead)
            {
                OMTourGroup tourgroup = OMTourGroup.Where(x => x.GroupId == this.Id).SelectAll().ExecuteFirstOrDefault();
                if (tourgroup != null)
                {
                    ALLStatOKS avgKK = new ALLStatOKS();
                    ALLStatOKS avgKR = new ALLStatOKS();
                    ALLStatOKS avgKS = new ALLStatOKS();
                    int countIndex = 0;
                    foreach (ObjectModel.KO.OMUnit unit in units)
                    {
                        {
                            countIndex++;
                            if (countIndex % 50 == 0)
                                Console.WriteLine(countIndex);

                            decimal square = 1;
                            if (unit.Square != null && unit.Square != 0) square = unit.Square.ParseToDecimal();

                            decimal upksz = 0;
                            string calc_obj = string.Empty;
                            KoParentCalcType calc_obj_code = KoParentCalcType.None;

                            if (!avgKK.Get(unit.CadastralBlock, PropertyTypes.Stead, out upksz, out calc_obj, out calc_obj_code))
                            {
                                GetAvgValue(ref avgKR, ref avgKS, tourgroup.TourId, unit.CadastralBlock, PropertyTypes.Stead, CalcParentGroup, out upksz, out calc_obj, out calc_obj_code);
                                avgKK.Add(unit.CadastralBlock, PropertyTypes.Stead, upksz, calc_obj, calc_obj_code);
                            }

                            decimal cost = Math.Round(upksz * square, 2, MidpointRounding.AwayFromZero);

                            //if (unit.UpksPre != upksz || unit.CadastralCostPre != cost)
                            //    Console.WriteLine("КН:{4} УПКС:{0} УПКС:{1} КС:{2} КС:{3} po:{5} pn:{6}", unit.UpksPre, upksz, unit.CadastralCostPre, cost, unit.CadastralNumber, unit.ParentCalcNumber, calc_obj);
                            unit.UpksPre = upksz;
                            unit.CadastralCostPre = cost;
                            unit.Upks = 0;
                            unit.CadastralCost = 0;
                            unit.ParentCalcNumber = calc_obj;
                            unit.ParentCalcType_Code = calc_obj_code;
                            unit.Save();
                        }
                    }
                }
            }
            #endregion
            #region Минимальное
            if (this.GroupAlgoritm_Code == KoGroupAlgoritm.Min && curTypeObject == PropertyTypes.Stead)
            {
                OMTourGroup tourgroup = OMTourGroup.Where(x => x.GroupId == this.Id).SelectAll().ExecuteFirstOrDefault();
                if (tourgroup != null)
                {
                    ALLStatOKS minKK = new ALLStatOKS();
                    ALLStatOKS minKR = new ALLStatOKS();
                    ALLStatOKS minKS = new ALLStatOKS();
                    int countIndex = 0;
                    foreach (ObjectModel.KO.OMUnit unit in units)
                    {
                        {

                            countIndex++;
                            if (countIndex % 50 == 0)
                                Console.WriteLine(countIndex);

                            decimal square = 1;
                            if (unit.Square != null && unit.Square != 0) square = unit.Square.ParseToDecimal();

                            decimal upksz = 0;
                            string calc_obj = string.Empty;
                            KoParentCalcType calc_obj_code = KoParentCalcType.None;

                            if (!minKK.Get(unit.CadastralBlock, PropertyTypes.Stead, out upksz, out calc_obj, out calc_obj_code))
                            {
                                GetMinValue(ref minKR, ref minKS, tourgroup.TourId, unit.CadastralBlock, PropertyTypes.Stead, CalcParentGroup, out upksz, out calc_obj, out calc_obj_code);
                                minKK.Add(unit.CadastralBlock, PropertyTypes.Stead, upksz, calc_obj, calc_obj_code);
                            }

                            decimal cost = Math.Round(upksz * square, 2, MidpointRounding.AwayFromZero);

                            //if (unit.UpksPre != upksz || unit.CadastralCostPre != cost)
                            //    Console.WriteLine("КН:{4} УПКС:{0} УПКС:{1} КС:{2} КС:{3} po:{5} pn:{6}", unit.UpksPre, upksz, unit.CadastralCostPre, cost, unit.CadastralNumber, unit.ParentCalcNumber, calc_obj);
                            unit.UpksPre = upksz;
                            unit.CadastralCostPre = cost;
                            unit.Upks = 0;
                            unit.CadastralCost = 0;
                            unit.ParentCalcNumber = calc_obj;
                            unit.ParentCalcType_Code = calc_obj_code;
                            unit.Save();
                        }
                    }
                }
            }
            #endregion
            #endregion

            #region ОНС
            if (this.GroupAlgoritm_Code == KoGroupAlgoritm.UnComplited && this.Id > 300000)
            {
                OMTourGroup tourgroup = OMTourGroup.Where(x => x.GroupId == this.Id).SelectAll().ExecuteFirstOrDefault();
                if (tourgroup != null)
                {
                    ALLStatOKS minKK = new ALLStatOKS();
                    ALLStatOKS minKR = new ALLStatOKS();
                    ALLStatOKS minKS = new ALLStatOKS();
                    int countIndex = 0;
                    foreach (ObjectModel.KO.OMUnit unit in units)
                    {
                        {

                            countIndex++;
                            if (countIndex % 50 == 0)
                                Console.WriteLine(countIndex);

                            decimal procent = 50;
                            if (unit.DegreeReadiness != null) procent = unit.DegreeReadiness.ParseToDecimal();

                            decimal square = 1;
                            if (unit.Square != null && unit.Square != 0) square = unit.Square.ParseToDecimal();

                            decimal upksz = 0;
                            decimal pp = procent / 100;
                            string calc_obj = string.Empty;
                            KoParentCalcType calc_obj_code = KoParentCalcType.None;

                            if (!minKK.Get(unit.CadastralBlock, PropertyTypes.Building, out upksz, out calc_obj, out calc_obj_code))
                            {
                                GetMinValue(ref minKR, ref minKS, tourgroup.TourId, unit.CadastralBlock, PropertyTypes.Building, CalcParentGroup, out upksz, out calc_obj, out calc_obj_code);
                                minKK.Add(unit.CadastralBlock, PropertyTypes.Building, upksz, calc_obj, calc_obj_code);
                            }

                            upksz = Math.Round(upksz * pp, 2, MidpointRounding.AwayFromZero);
                            decimal cost = Math.Round(upksz * square, 2, MidpointRounding.AwayFromZero);

                            //if (unit.UpksPre != upksz || unit.CadastralCostPre != cost)
                            //    Console.WriteLine("КН:{4} УПКС:{0} УПКС:{1} КС:{2} КС:{3} po:{5} pn:{6}", unit.UpksPre, upksz, unit.CadastralCostPre, cost, unit.CadastralNumber, unit.ParentCalcNumber, calc_obj);
                            unit.UpksPre = upksz;
                            unit.CadastralCostPre = cost;
                            unit.Upks = 0;
                            unit.CadastralCost = 0;
                            unit.ParentCalcNumber = calc_obj;
                            unit.ParentCalcType_Code = calc_obj_code;
                            unit.Save();
                        }
                    }
                }
            }
            if (this.GroupAlgoritm_Code == KoGroupAlgoritm.UnComplited && this.Id < 300000)
            {
                OMTourGroup tourgroup = OMTourGroup.Where(x => x.GroupId == this.Id).SelectAll().ExecuteFirstOrDefault();
                if (tourgroup != null)
                {
                    ALLStatOKS avgKK = new ALLStatOKS();
                    ALLStatOKS avgKR = new ALLStatOKS();
                    ALLStatOKS avgKS = new ALLStatOKS();
                    int countIndex = 0;
                    foreach (ObjectModel.KO.OMUnit unit in units)
                    {
                        {
                            countIndex++;
                            if (countIndex % 50 == 0)
                                Console.WriteLine(countIndex);

                            decimal procent = 50;
                            if (unit.DegreeReadiness != null) procent = unit.DegreeReadiness.ParseToDecimal();

                            decimal square = 1;
                            if (unit.Square != null && unit.Square != 0) square = unit.Square.ParseToDecimal();

                            decimal upksz = 0;
                            decimal pp = procent / 100;
                            string calc_obj = string.Empty;
                            KoParentCalcType calc_obj_code = KoParentCalcType.None;

                            if (!avgKK.Get(unit.CadastralBlock, PropertyTypes.Building, out upksz, out calc_obj, out calc_obj_code))
                            {
                                GetAvgValue(ref avgKR, ref avgKS, tourgroup.TourId, unit.CadastralBlock, PropertyTypes.Building, CalcParentGroup, out upksz, out calc_obj, out calc_obj_code);
                                avgKK.Add(unit.CadastralBlock, PropertyTypes.Building, upksz, calc_obj, calc_obj_code);
                            }

                            upksz = Math.Round(upksz * pp, 2, MidpointRounding.AwayFromZero);
                            decimal cost = Math.Round(upksz * square, 2, MidpointRounding.AwayFromZero);

                            //if (unit.UpksPre != upksz || unit.CadastralCostPre != cost)
                            //    Console.WriteLine("КН:{4} УПКС:{0} УПКС:{1} КС:{2} КС:{3} po:{5} pn:{6}", unit.UpksPre, upksz, unit.CadastralCostPre, cost, unit.CadastralNumber, unit.ParentCalcNumber, calc_obj);
                            unit.UpksPre = upksz;
                            unit.CadastralCostPre = cost;
                            unit.Upks = 0;
                            unit.CadastralCost = 0;
                            unit.ParentCalcNumber = calc_obj;
                            unit.ParentCalcType_Code = calc_obj_code;
                            unit.Save();
                        }
                    }
                }
            }
            #endregion
        }
        public void Calculate(List<ObjectModel.KO.OMUnit> units, List<long> CalcParentGroup)
        {
            Calculate(units.FindAll(x => x.PropertyType_Code == PropertyTypes.Building), CalcParentGroup, PropertyTypes.Building);
            Calculate(units.FindAll(x => x.PropertyType_Code == PropertyTypes.Construction), CalcParentGroup, PropertyTypes.Construction);
            Calculate(units.FindAll(x => x.PropertyType_Code == PropertyTypes.UncompletedBuilding), CalcParentGroup, PropertyTypes.UncompletedBuilding);
            Calculate(units.FindAll(x => x.PropertyType_Code == PropertyTypes.Stead), CalcParentGroup, PropertyTypes.Stead);
            Calculate(units.FindAll(x => x.PropertyType_Code == PropertyTypes.Pllacement), CalcParentGroup, PropertyTypes.Pllacement);
        }
        public void CalculateResult(List<ObjectModel.KO.OMUnit> units)
        {
            CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
            ParallelOptions options = new ParallelOptions
            {
                CancellationToken = cancelTokenSource.Token,
                MaxDegreeOfParallelism = 10
            };
            int count = 0;
            int? factorReestrId = GetFactorReestrId(this);
            if (factorReestrId != null)
            {
                Parallel.ForEach(units, options, item => CalculateResult(item, factorReestrId.Value, ref count));
            }
        }
        private void CalculateResult(ObjectModel.KO.OMUnit unit, int factorReestrId, ref int count)
        {
            count++;
            if (count % 50 == 0)
                Console.WriteLine(count);
            decimal? upks = unit.UpksPre;
            decimal? square = unit.Square;
            if (square == null) square = 1;
            if (upks != null)
            {
                List<ObjectModel.KO.OMGroupFactor> groupFactors = ObjectModel.KO.OMGroupFactor.Where(x => x.GroupId == this.Id).SelectAll().Execute();
                foreach (ObjectModel.KO.OMGroupFactor groupFactor in groupFactors)
                {
                    List<CalcItem> FactorValues = new List<CalcItem>();
                    decimal koeff = 0;
                    DataTable data = RegisterStorage.GetAttribute((int)unit.Id, factorReestrId, (int)groupFactor.FactorId);
                    if (data != null)
                    {
                        foreach (DataRow row in data.Rows)
                        {
                            string t6 = row.ItemArray[6].ParseToString();
                            if (t6 != string.Empty && t6 != null)
                                koeff = t6.ParseToDecimal();
                            else
                            {
                                string t7 = row.ItemArray[7].ParseToString();
                                if (t7 != string.Empty && t7 != null)
                                    koeff = t7.ParseToDecimal();
                            }
                        }
                    }
                    upks *= koeff;
                }
                upks = Math.Round(upks.Value, 2, MidpointRounding.AwayFromZero);
            }
            else
            {
                upks = 0;
            }
            decimal cost = Math.Round(upks.Value * square.Value, 2, MidpointRounding.AwayFromZero);
            //if (unit.Upks != upks || unit.CadastralCost != cost)
            //    Console.WriteLine("КН:{4} УПКС:{0} УПКС:{1} КС:{2} КС:{3}", unit.Upks, upks, unit.CadastralCost, cost, unit.CadastralNumber);

            unit.Upks = upks;
            unit.CadastralCost = cost;
            unit.Save();
        }
        private void CalculateChildForEtalon(ObjectModel.KO.OMUnit unit)
        {
            List<OMUnit> childs = OMUnit.Where(x=>x.GroupId==unit.GroupId && x.UseAsPrototype==false && x.TourId==unit.TourId && x.Status_Code==unit.Status_Code && x.CadastralBlock==unit.CadastralBlock).SelectAll().Execute();

            CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
            ParallelOptions options = new ParallelOptions
            {
                CancellationToken = cancelTokenSource.Token,
                MaxDegreeOfParallelism = 25
            };
            Parallel.ForEach(childs, options, child =>
            {
                child.UpksPre = unit.UpksPre;
                child.CadastralCostPre = Math.Round((child.UpksPre * child.Square).ParseToDecimal(), 2);
                child.Upks = 0;
                child.CadastralCost = 0;
                child.Save();
            });

        }
        public static void CalculateSelectGroup(KOCalcSettings setting)
        {
            List<ObjectModel.KO.OMGroup> CalcGroups = new List<OMGroup>();

            if (setting.CalcAllGroups)
            {
                CalcGroups = GetListGroupTour(setting.IdTour, (setting.CalcParcel ? KoGroupAlgoritm.MainParcel : KoGroupAlgoritm.MainOKS));
            }
            else
            {
                foreach (long idGroup in setting.CalcGroups)
                {
                    var group = ObjectModel.KO.OMGroup.Where(x => x.Id == idGroup).SelectAll().ExecuteFirstOrDefault();
                    if (group != null) CalcGroups.Add(group);
                }
            }

            foreach (ObjectModel.KO.OMGroup CalcGroup in CalcGroups)
            {
                if (CalcGroup != null)
                {
                    List<ObjectModel.KO.OMUnit> Units = new List<ObjectModel.KO.OMUnit>();
                    foreach (long taskId in setting.TaskFilter)
                    {
                        Units.AddRange(ObjectModel.KO.OMUnit.Where(x => (setting.CalcParcel ? (x.PropertyType_Code == PropertyTypes.Stead) : (x.PropertyType_Code != PropertyTypes.Stead)) && x.TaskId == taskId && x.GroupId== CalcGroup.Id).SelectAll().Execute());
                    }

                    List<long> calcParentGroup = new List<long>();
                    List<ObjectModel.KO.OMCalcGroup> parentsGroups = ObjectModel.KO.OMCalcGroup.Where(x=>x.GroupId==CalcGroup.Id).SelectAll().Execute();
                    foreach (ObjectModel.KO.OMCalcGroup parentsGroup in parentsGroups)
                    {
                        if (parentsGroup.ParentCalcGroupId != null)
                            calcParentGroup.Add(parentsGroup.ParentCalcGroupId.Value);
                    }
                    if (setting.CalcStage1)
                        CalcGroup.Calculate(Units, calcParentGroup);

                    if (setting.CalcStage3)
                        CalcGroup.CalculateResult(Units);
                }
            }
        }
    }

    /// <summary>
    /// Настройки расчета
    /// </summary>
    public struct KOCalcSettings
    {
        /// <summary>
        /// Тур оценки
        /// </summary>
        public long IdTour;
        /// <summary>
        /// Список заданий на оценку
        /// </summary>
        public List<long> TaskFilter;
        /// <summary>
        /// Объекты расчета: true-Земельный участок, false-ОКС
        /// </summary>
        public bool CalcParcel;
        /// <summary>
        /// Предварительный расчет
        /// </summary>
        public bool CalcStage1;
        /// <summary>
        /// Расчет поправок/коэффициентов
        /// </summary>
        public bool CalcStage2;
        /// <summary>
        /// Окончательный расчет
        /// </summary>
        public bool CalcStage3;
        /// <summary>
        /// Признак расчета всех групп: true - все группы, false - список выбранных групп
        /// </summary>
        public bool CalcAllGroups;
        /// <summary>
        /// Список выбранных групп
        /// </summary>
        public List<long> CalcGroups;
    }

    /// <summary>
    /// Настройки выгрузки отчетов
    /// </summary>
    public struct KOUnloadSettings
    {
        /// <summary>
        /// Тур оценки
        /// </summary>
        public long IdTour;
        /// <summary>
        /// Список заданий на оценку
        /// </summary>
        public List<long> TaskFilter;
        /// <summary>
        /// Объекты выгрузки: true-Земельный участок, false-ОКС
        /// </summary>
        public bool UnloadParcel;
        /// <summary>
        /// Путь сохранения отчетов
        /// </summary>
        public string DirectoryName;
        /// <summary>
        /// Выгрузка изменений
        /// </summary>
        public bool UnloadChange;
        /// <summary>
        /// Выгрузка истории по объектам
        /// </summary>
        public bool UnloadHistory;
        /// <summary>
        /// Таблица 4. Группировка объектов недвижимости
        /// </summary>
        public bool UnloadTable04;
        /// <summary>
        /// Таблица 5. Результаты моделирования
        /// </summary>
        public bool UnloadTable05;
        /// <summary>
        /// Таблица 7. Обобщенные показатели по кадастровым районам
        /// </summary>
        public bool UnloadTable07;
        /// <summary>
        /// Таблица 8. Минимальные, максимальные, средние УПКС по кадастровым кварталам
        /// </summary>
        public bool UnloadTable08;
        /// <summary>
        /// Таблица 9. Результаты определения кадастровой стоимости
        /// </summary>
        public bool UnloadTable09;
        /// <summary>
        /// Таблица 10. Результаты государственной кадастровой оценки
        /// </summary>
        public bool UnloadTable10;
        /// <summary>
        /// Таблица 11. Сводные результаты по кадастровому району
        /// </summary>
        public bool UnloadTable11;
        /// <summary>
        /// Выгрузка XML 1: КНомер, УПКСЗ, КСтоимость
        /// </summary>
        public bool UnloadXML1;
        /// <summary>
        /// Выгрузка XML 2 результатов Кадастровой оценки по группам.
        /// </summary>
        public bool UnloadXML2;

		/// <summary>
		/// Отправка результатов в РЕОН
		/// </summary>
		public bool SendResultToReon { get; set; }
    }

    public class ResultKoUnloadSettings
    {
	    public ResultKoUnloadSettings(bool noResult = false)
	    {
		    NoResult = noResult;
	    }

	    public long FileId { get; set; }

	    public string FileName { get; set; }

	    public bool IsXml { get; set; }

		public bool NoResult { get; set; }

		public long TaskId { get; set; }
    }
}
