using Core.Register;
using Core.Register.RegisterEntities;
using Core.Shared.Extensions;
using DocumentFormat.OpenXml.Drawing;
using ObjectModel.Directory;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ObjectModel.KO
{
    public struct CalcErrorItem
    {
        public string CadastralNumber;
        public string Error;
    }

    public class ALLTmpItem
    {
        #region Поля
        public string KK { get; set; }
        public string KN { get; set; }
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
            KN = string.Empty;
            Type = aType;
            UPKSZ = aUpks;
            Calc_obj = aCalc_obj;
            Calc_obj_code = aCalc_obj_code;
        }
        public ALLTmpItem(string aKK, string aKN, PropertyTypes aType, decimal aUpks, string aCalc_obj, KoParentCalcType aCalc_obj_code)
        {
            KK = aKK;
            KN = aKN;
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
            Task = OMTask.Where(x => x.Id == unit.TaskId).SelectAll().ExecuteFirstOrDefault();
            if (Task != null)
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
            return Unit.CreationDate.Value.ToString("dd.MM.yyyy") + ", " + Unit.CadastralCost.Value.ToString() + ", " + Task.NoteType + ", " + Unit.StatusResultCalc;
        }

        public static List<HistoryUnit> GetHistory(string cadastralNumber)
        {
            List<HistoryUnit> Items = new List<HistoryUnit>();
            List<OMUnit> units = OMUnit.Where(x => x.CadastralNumber == cadastralNumber).SelectAll().Execute();
            foreach (OMUnit unit in units)
            {
                Items.Add(new HistoryUnit(unit));
            }

            if (Items.Count > 0)
            {
                Items.Sort(Items[0]);

                int indexActual = -1;
                int startIndex = -1;
                for (int i = Items.Count - 1; i >= 0; i--)
                {
                    if (Items[i].Unit.CadastralCost != null)
                    {
                        if (Items[i].Unit.CadastralCost.Value > 0 && !Items[i].IsBad)
                        {
                            indexActual = i;
                            startIndex = i;
                            break;
                        }
                    }
                }

                if (indexActual >= 0)
                {
                    decimal cost = Items[indexActual].Unit.CadastralCost.Value;
                    for (int i = startIndex - 1; i >= 0; i--)
                    {
                        if (Items[i].Unit.CadastralCost != null)
                        {
                            if (Items[i].Unit.CadastralCost.Value == cost && !Items[i].IsBad)
                            {
                                indexActual = i;
                            }
                            else
                            if (Items[i].IsBad)
                            {
                            }
                            else break;
                        }
                        else
                            break;
                    }
                    if (indexActual >= 0)
                        Items[indexActual].IsActual = true;
                }
            }
            return Items;
        }
        public static List<HistoryUnit> GetPrevHistoryTour(OMUnit current)
        {
            List<HistoryUnit> Items = new List<HistoryUnit>();
            List<OMUnit> units = OMUnit.Where(x => x.CadastralNumber == current.CadastralNumber && x.TourId == current.TourId && x.Id != current.Id).SelectAll().Execute();
            foreach (OMUnit unit in units)
            {
                Items.Add(new HistoryUnit(unit));
            }

            if (Items.Count > 0)
            {
                Items.Sort(Items[0]);

                int indexActual = -1;
                int startIndex = -1;
                for (int i = Items.Count - 1; i >= 0; i--)
                {
                    if (Items[i].Unit.CadastralCost != null)
                    {
                        if (Items[i].Unit.CadastralCost.Value > 0 && !Items[i].IsBad)
                        {
                            indexActual = i;
                            startIndex = i;
                            break;
                        }
                    }
                }

                if (indexActual >= 0)
                {
                    decimal cost = Items[indexActual].Unit.CadastralCost.Value;
                    for (int i = startIndex - 1; i >= 0; i--)
                    {
                        if (Items[i].Unit.CadastralCost != null)
                        {
                            if (Items[i].Unit.CadastralCost.Value == cost && !Items[i].IsBad)
                            {
                                indexActual = i;
                            }
                            else
                            if (Items[i].IsBad)
                            {
                            }
                            else break;
                        }
                        else
                            break;
                    }
                    if (indexActual >= 0)
                        Items[indexActual].IsActual = true;
                }
            }
            return Items;
        }
        public static HistoryUnit GetPrevUnit(List<HistoryUnit> items)
        {
            if (items.Count > 0)
            {
                int indexActual = items.Count - 1;
                for (int i = items.Count - 1; i >= 0; i--)
                {
                    if (!items[i].IsBad)
                    {
                        indexActual = i;
                        break;
                    }
                }
                return items[indexActual];
            }
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
        public static List<ObjectModel.KO.OMAutoCalculationSettings> GetListGroupRobot(long tourId, bool parcel)
        {
            return ObjectModel.KO.OMAutoCalculationSettings.Where(x=>x.CalcParcel==parcel && x.IdTour==tourId).SelectAll().OrderBy(x=>x.NumberPriority).Execute();
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
                }
            }
            if (sumSquare > 0)
                upks = Math.Round(sumCost / sumSquare, 2, MidpointRounding.AwayFromZero);
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
        private static void GetAvgValue(ref ALLStatOKS avgKK, ref ALLStatOKS avgKR, ref ALLStatOKS avgKS, long tourId, string kb, string kk, PropertyTypes type, List<long> calcChildGroups, out decimal upks, out string parentCalcObject, out KoParentCalcType parentCalcType)
        {
            upks = 0;
            parentCalcType = KoParentCalcType.None;
            parentCalcObject = string.Empty;
            bool prFindInBuilding = false;
            bool prFindInCadastralBlock = false;
            bool prFindInCadastralRaion = false;
            bool prFindInCadastralRegion = false;
            string kr = kk.Substring(0, 5);
            decimal sumSquare = 0;
            decimal sumCost = 0;

            #region поиск по зданию
            upks = 0;
            foreach (long calcChildGroup in calcChildGroups)
            {
                List<OMUnit> unitsKB = OMUnit.Where(x => x.TourId == tourId && x.Status_Code == KoUnitStatus.Initial && x.GroupId == calcChildGroup && x.CadastralNumber == kb && x.PropertyType_Code == PropertyTypes.Building).SelectAll().Execute();
                if (unitsKB.Count > 0)
                {
                    prFindInBuilding = true;
                    foreach (OMUnit unit in unitsKB)
                    {
                        if (unit.Upks != null)
                        {
                            upks = unit.Upks.Value;
                        }
                    }
                }
            }
            sumSquare = 0;
            sumCost = 0;
            #endregion
            #region поиск по кварталу
            if (!prFindInBuilding)
            {
                if (!avgKK.Get(kk, PropertyTypes.Pllacement, out upks, out parentCalcObject, out parentCalcType))
                {
                    upks = 0;
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
                        }
                    }
                    if (sumSquare > 0)
                        upks = Math.Round(sumCost / sumSquare, 2, MidpointRounding.AwayFromZero);
                    sumSquare = 0;
                    sumCost = 0;
                    avgKK.Add(kk, PropertyTypes.Pllacement, upks, kk, KoParentCalcType.CadastralBlock);
                }
                else
                {
                    prFindInCadastralBlock = true;
                }
            }
            #endregion
            #region поиск по району
            if (!prFindInCadastralBlock)
            {
                if (!avgKR.Get(kr, PropertyTypes.Pllacement, out upks, out parentCalcObject, out parentCalcType))
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
                    sumSquare = 0;
                    sumCost = 0;
                    avgKR.Add(kr, PropertyTypes.Pllacement, upks, kr, KoParentCalcType.CadastralRegion);
                }
                else
                {
                    prFindInCadastralRaion = true;
                }
            }
            #endregion
            #region поиск по субъекту
            if (!prFindInBuilding && !prFindInCadastralRaion && !prFindInCadastralBlock)
            {
                if (!avgKS.Get("Субъект РФ", PropertyTypes.Pllacement, out upks, out parentCalcObject, out parentCalcType))
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
                    sumSquare = 0;
                    sumCost = 0;
                    avgKS.Add("Субъект РФ", PropertyTypes.Pllacement, upks, kr, KoParentCalcType.RfSubject);
                }
                else
                {
                    prFindInCadastralRegion = true;
                }
            }
            #endregion

            if (prFindInBuilding || prFindInCadastralBlock || prFindInCadastralRaion || prFindInCadastralRegion)
            {
                if (prFindInBuilding)
                {
                    parentCalcObject = kb;
                    parentCalcType = KoParentCalcType.None;
                }
                else
                if (prFindInCadastralBlock)
                {
                    parentCalcObject = kk;
                    parentCalcType = KoParentCalcType.CadastralBlock;
                }
                else
                if (prFindInCadastralRaion)
                {
                    parentCalcObject = kr;
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
        private List<CalcErrorItem> Calculate(List<ObjectModel.KO.OMUnit> units, List<long> CalcParentGroup, PropertyTypes curTypeObject)
        {
            List<CalcErrorItem> res = new List<CalcErrorItem>();
            if (units.Count == 0) return res;

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

                        if (weight.SignMarket)
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
                                decimal D = 0;
                                decimal Dm = 1;
                                decimal De = 1;
                                decimal Dss = 0;

                                bool error = false;

                                foreach (OMModelFactor weight in model.ModelFactor)
                                {
                                    if (weight.SignAdd)
                                    {
                                        string factorValue = string.Empty;
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


                                        string factorName = weight.FactorId.ParseToString();//TODO: наименование фактора

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
                                                error = true;
                                                lock (res)
                                                {
                                                    res.Add(new CalcErrorItem() { CadastralNumber = unit.CadastralNumber, Error = "Отсутствует значение метки фактора " + factorName + " для значения " + factorValue });
                                                }
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
                                            {
                                                error = true;
                                                lock (res)
                                                {
                                                    res.Add(new CalcErrorItem() { CadastralNumber = unit.CadastralNumber, Error = "Неверное значение фактора " + factorName + " : " + factorValue });
                                                }
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
                                    }
                                }

                                foreach (OMModelFactor weight in model.ModelFactor)
                                {
                                    if (!weight.SignAdd)
                                    {
                                        string factorValue = string.Empty;

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

                                        string factorName = weight.FactorId.ParseToString();//TODO: наименование фактора

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
                                            {
                                                error = true;
                                                lock (res)
                                                {
                                                    res.Add(new CalcErrorItem() { CadastralNumber = unit.CadastralNumber, Error = "Отсутствует значение метки фактора " + factorName + " для значения " + factorValue });
                                                }
                                            }

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
                                            {
                                                error = true;
                                                lock (res)
                                                {
                                                    res.Add(new CalcErrorItem() { CadastralNumber = unit.CadastralNumber, Error = "Неверное значение фактора " + factorName + " : " + factorValue });
                                                }
                                            }

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

                                if (model.AlgoritmType_Code == KoAlgoritmType.Exp)
                                {
                                    decimal UPKS = Math.Round(Convert.ToDecimal(Math.Exp(Convert.ToDouble(model.A0 + D))) * De, 2, MidpointRounding.AwayFromZero);
                                    decimal Cost = Math.Round((UPKS * unit.Square).ParseToDecimal(), 2, MidpointRounding.AwayFromZero);
                                    if (!unit.isExplication)
                                    {
                                        if (error)
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
                                            if (error)
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

                                    if (!unit.isExplication)
                                    {
                                        if (error)
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
                                            if (error)
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

                                    if (!unit.isExplication)
                                    {
                                        if (error)
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
                                            if (error)
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
            #region Среднее (КР, КЛАДР)
            if (this.GroupAlgoritm_Code == KoGroupAlgoritm.AVG && curTypeObject == PropertyTypes.Building)
            {
                OMTourGroup tourgroup = OMTourGroup.Where(x => x.GroupId == this.Id).SelectAll().ExecuteFirstOrDefault();
                if (tourgroup != null)
                {
                    ALLStatOKS avgKK = new ALLStatOKS();
                    ALLStatOKS avgKR = new ALLStatOKS();
                    ALLStatOKS avgKS = new ALLStatOKS();
                    foreach (ObjectModel.KO.OMUnit unit in units)
                    {
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
                    foreach (ObjectModel.KO.OMUnit unit in units)
                    {
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
            #endregion
            #endregion

            #region Помещения
            #region Помещения по зданиям (КР, КЛАДР) (квартиры)
            if (this.GroupAlgoritm_Code == KoGroupAlgoritm.FlatOnBuilding && curTypeObject == PropertyTypes.Pllacement)
            {
                OMTourGroup tourgroup = OMTourGroup.Where(x => x.GroupId == this.Id).SelectAll().ExecuteFirstOrDefault();
                if (tourgroup != null)
                {
                    ALLStatOKS flatKN = new ALLStatOKS();
                    ALLStatOKS avgKK = new ALLStatOKS();
                    ALLStatOKS avgKR = new ALLStatOKS();
                    ALLStatOKS avgKS = new ALLStatOKS();



                    foreach (ObjectModel.KO.OMUnit unit in units)
                    {
                        decimal upksz = 0;
                        string calc_obj = string.Empty;
                        KoParentCalcType calc_obj_code = KoParentCalcType.None;

                        if (!flatKN.Get(unit.BuildingCadastralNumber, PropertyTypes.Pllacement, out upksz, out calc_obj, out calc_obj_code))
                        {
                            GetAvgValue(ref avgKK, ref avgKR, ref avgKS, tourgroup.TourId, unit.BuildingCadastralNumber, unit.CadastralBlock, PropertyTypes.Pllacement, CalcParentGroup, out upksz, out calc_obj, out calc_obj_code);
                            flatKN.Add(unit.BuildingCadastralNumber, PropertyTypes.Pllacement, upksz, calc_obj, calc_obj_code);
                        }

                        decimal square = (unit.Square == null) ? 1 : unit.Square.Value;
                        decimal cost = Math.Round(upksz * square, 2, MidpointRounding.AwayFromZero);

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
            #endregion
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
                    foreach (ObjectModel.KO.OMUnit unit in units)
                    {
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
                    foreach (ObjectModel.KO.OMUnit unit in units)
                    {
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
                    foreach (ObjectModel.KO.OMUnit unit in units)
                    {
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
                    foreach (ObjectModel.KO.OMUnit unit in units)
                    {
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
                    foreach (ObjectModel.KO.OMUnit unit in units)
                    {
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
            if (this.GroupAlgoritm_Code == KoGroupAlgoritm.UnComplited && this.Id < 300000)
            {
                OMTourGroup tourgroup = OMTourGroup.Where(x => x.GroupId == this.Id).SelectAll().ExecuteFirstOrDefault();
                if (tourgroup != null)
                {
                    ALLStatOKS avgKK = new ALLStatOKS();
                    ALLStatOKS avgKR = new ALLStatOKS();
                    ALLStatOKS avgKS = new ALLStatOKS();
                    foreach (ObjectModel.KO.OMUnit unit in units)
                    {
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
            #endregion

            return res;
        }
        public List<CalcErrorItem> Calculate(List<ObjectModel.KO.OMUnit> units, List<long> CalcParentGroup)
        {
            List<CalcErrorItem> res = new List<CalcErrorItem>();
            res.AddRange(Calculate(units.FindAll(x => x.PropertyType_Code == PropertyTypes.Building), CalcParentGroup, PropertyTypes.Building));
            res.AddRange(Calculate(units.FindAll(x => x.PropertyType_Code == PropertyTypes.Construction), CalcParentGroup, PropertyTypes.Construction));
            res.AddRange(Calculate(units.FindAll(x => x.PropertyType_Code == PropertyTypes.UncompletedBuilding), CalcParentGroup, PropertyTypes.UncompletedBuilding));
            res.AddRange(Calculate(units.FindAll(x => x.PropertyType_Code == PropertyTypes.Stead), CalcParentGroup, PropertyTypes.Stead));
            res.AddRange(Calculate(units.FindAll(x => x.PropertyType_Code == PropertyTypes.Pllacement), CalcParentGroup, PropertyTypes.Pllacement));
            return res;
        }
        public void CalculateResult(List<ObjectModel.KO.OMUnit> units)
        {
            CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
            ParallelOptions options = new ParallelOptions
            {
                CancellationToken = cancelTokenSource.Token,
                MaxDegreeOfParallelism = 10
            };
            int? factorReestrId = GetFactorReestrId(this);
            if (factorReestrId != null)
            {
                Parallel.ForEach(units, options, item => CalculateResult(item, factorReestrId.Value));
            }
        }
        private void CalculateResult(ObjectModel.KO.OMUnit unit, int factorReestrId)
        {
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
                            if (groupFactor.SignMarket.ParseToBoolean())
                            {
                                List<OMMarkCatalog> MarkCatalogs = new List<OMMarkCatalog>();
                                MarkCatalogs.AddRange(OMMarkCatalog.Where(x => x.GroupId == this.Id && x.FactorId == groupFactor.FactorId).SelectAll().Execute());

                                string t6 = row.ItemArray[6].ParseToString();
                                if (t6 != string.Empty && t6 != null)
                                {
                                    OMMarkCatalog mc = MarkCatalogs.Find(x => x.ValueFactor.ToUpper() == t6.ToUpper());
                                    if (mc != null)
                                    {
                                        koeff = mc.MetkaFactor.ParseToDecimal();
                                    }
                                }
                                else
                                {
                                    string t7 = row.ItemArray[7].ParseToString();
                                    if (t7 != string.Empty && t7 != null)
                                    {
                                        OMMarkCatalog mc = MarkCatalogs.Find(x => x.ValueFactor.ToUpper() == t7.ToUpper());
                                        if (mc != null)
                                        {
                                            koeff = mc.MetkaFactor.ParseToDecimal();
                                        }
                                    }
                                }
                            }
                            else
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

            if (setting.CalcAllGroups)
            {
                List<ObjectModel.KO.OMAutoCalculationSettings> RobotCalcGroups = GetListGroupRobot(setting.IdTour, setting.CalcParcel);

                foreach (ObjectModel.KO.OMAutoCalculationSettings AutoCalcGroup in RobotCalcGroups)
                {
                    if (AutoCalcGroup != null)
                    {
                        var CalcGroup = ObjectModel.KO.OMGroup.Where(x => x.Id == AutoCalcGroup.IdGroup).SelectAll().ExecuteFirstOrDefault();
                        if (CalcGroup != null)
                        {
                            List<ObjectModel.KO.OMUnit> Units = new List<ObjectModel.KO.OMUnit>();
                            foreach (long taskId in setting.TaskFilter)
                            {
                                Units.AddRange(ObjectModel.KO.OMUnit.Where(x => (setting.CalcParcel ? (x.PropertyType_Code == PropertyTypes.Stead) : (x.PropertyType_Code != PropertyTypes.Stead)) && x.TaskId == taskId && x.GroupId == CalcGroup.Id).SelectAll().Execute());
                            }

                            if (Units.Count > 0)
                            {
                                List<long> calcParentGroup = new List<long>();
                                List<ObjectModel.KO.OMCalcGroup> parentsGroups = ObjectModel.KO.OMCalcGroup.Where(x => x.GroupId == CalcGroup.Id).SelectAll().Execute();
                                foreach (ObjectModel.KO.OMCalcGroup parentsGroup in parentsGroups)
                                {
                                    if (parentsGroup.ParentCalcGroupId != null)
                                        calcParentGroup.Add(parentsGroup.ParentCalcGroupId.Value);
                                }
                                if (AutoCalcGroup.CalcStage1)
                                    CalcGroup.Calculate(Units, calcParentGroup);
                                if (AutoCalcGroup.CalcStage3)
                                    CalcGroup.CalculateResult(Units);
                            }
                        }
                    }
                }
            }
            else
            {
                List<ObjectModel.KO.OMGroup> CalcGroups = new List<OMGroup>();

                foreach (long idGroup in setting.CalcGroups)
                {
                    var group = ObjectModel.KO.OMGroup.Where(x => x.Id == idGroup).SelectAll().ExecuteFirstOrDefault();
                    if (group != null) CalcGroups.Add(group);
                }
                foreach (ObjectModel.KO.OMGroup CalcGroup in CalcGroups)
                {
                    if (CalcGroup != null)
                    {
                        List<ObjectModel.KO.OMUnit> Units = new List<ObjectModel.KO.OMUnit>();
                        foreach (long taskId in setting.TaskFilter)
                        {
                            Units.AddRange(ObjectModel.KO.OMUnit.Where(x => (setting.CalcParcel ? (x.PropertyType_Code == PropertyTypes.Stead) : (x.PropertyType_Code != PropertyTypes.Stead)) && x.TaskId == taskId && x.GroupId == CalcGroup.Id).SelectAll().Execute());
                        }

                        if (Units.Count > 0)
                        {
                            List<long> calcParentGroup = new List<long>();
                            List<ObjectModel.KO.OMCalcGroup> parentsGroups = ObjectModel.KO.OMCalcGroup.Where(x => x.GroupId == CalcGroup.Id).SelectAll().Execute();
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
        }
        public static string GetFormulaKoeff(OMGroup _parent_group, bool upks, string value)
        {
            string res = string.Empty;
            if (_parent_group == null) return res;

            if (_parent_group.GroupFactor.Count == 0)
                _parent_group.GroupFactor = OMGroupFactor.Where(x => x.GroupId == _parent_group.Id).SelectAll().Execute();
            foreach (OMGroupFactor koeff in _parent_group.GroupFactor)
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

            return (upks ? "УПКС=" : string.Empty) + value + res;
        }
        public static string GetFormulaFull(OMGroup _parent_group, bool upks)
        {
            string str_koeff = GetFormulaKoeff(_parent_group, false, string.Empty);
            return (upks ? ("УПКС=") : string.Empty) + ((str_koeff != string.Empty) ? "(" : string.Empty) + GetFormulaMain(_parent_group, false) + ((str_koeff != string.Empty) ? ")" : string.Empty) + str_koeff;
        }
        private static string GetFormulaPart(string val, string znak, double empty)
        {
            string res = Convert.ToDouble(val).ToString() + " " + znak + " ";
            double rval = Convert.ToDouble(val);
            if (rval == empty)
                res = string.Empty;
            return res;
        }
        public static string GetFormulaMain(OMGroup _parent_group, bool upks)
        {
            string res = string.Empty;
            #region Моделирование
            if (_parent_group == null) return res;

            OMModel model = OMModel.Where(x => x.GroupId == _parent_group.Id).SelectAll().ExecuteFirstOrDefault();
            if (model != null)
            {
                if (model.ModelFactor.Count == 0)
                    model.ModelFactor = OMModelFactor.Where(x => x.ModelId == model.Id).SelectAll().Execute();
            }

            if (_parent_group.GroupAlgoritm_Code == KoGroupAlgoritm.Etalon || _parent_group.GroupAlgoritm_Code == KoGroupAlgoritm.Model)
            {
                string D_string = "";
                string Dm_string = "";
                string De_string = "";
                string Dss_string = "";


                foreach (OMModelFactor weight in model.ModelFactor)
                {
                    if (weight.SignAdd)
                    {
                        RegisterAttribute attributeData = RegisterCache.GetAttributeData((int)(weight.FactorId));
                        if (attributeData != null)
                        {
                            string d_string = (weight.SignMarket) ? ("метка" + "(" + attributeData.Name + ")") : (attributeData.Name);
                            switch (model.AlgoritmType_Code)
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

                foreach (OMModelFactor weight in model.ModelFactor)
                {
                    if (!weight.SignAdd)
                    {
                        RegisterAttribute attributeData = RegisterCache.GetAttributeData((int)(weight.FactorId));
                        if (attributeData != null)
                        {
                            string d_string = (weight.SignMarket) ? ("метка" + "(" + attributeData.Name + ")") : (attributeData.Name);

                            switch (model.AlgoritmType_Code)
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


                switch (model.AlgoritmType_Code)
                {
                    case KoAlgoritmType.Exp:
                        if (De_string != string.Empty)
                            res = "exp(" + model.A0.ToString() + D_string + ")" + De_string;
                        else
                            res = "exp(" + model.A0.ToString() + D_string + ")";
                        break;
                    case KoAlgoritmType.Line:
                        res = model.A0.ToString() + D_string;
                        break;
                    case KoAlgoritmType.Multi:
                        res = GetFormulaPart(model.A0.ToString(), "*", 1) + Dm_string.TrimStart(' ').TrimStart('*').TrimStart(' ');
                        break;
                    default:
                        res = string.Empty;
                        break;
                }
            }
            #endregion
            return (upks ? ("УПКС=") : string.Empty) + res;
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
        /// Исходящий документ
        /// </summary>
        public long IdResponseDocument;
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
        [KoUnloadResultType(KoUnloadResultType.UnloadChange)]
        public bool UnloadChange;
        /// <summary>
        /// Выгрузка истории по объектам
        /// </summary>
        [KoUnloadResultType(KoUnloadResultType.UnloadHistory)]
        public bool UnloadHistory;
        /// <summary>
        /// Таблица 4. Группировка объектов недвижимости
        /// </summary>
        [KoUnloadResultType(KoUnloadResultType.UnloadTable04)]
        public bool UnloadTable04;
        /// <summary>
        /// Таблица 5. Результаты моделирования
        /// </summary>
        [KoUnloadResultType(KoUnloadResultType.UnloadTable05)]
        public bool UnloadTable05;
        /// <summary>
        /// Таблица 7. Обобщенные показатели по кадастровым районам
        /// </summary>
        [KoUnloadResultType(KoUnloadResultType.UnloadTable07)]
        public bool UnloadTable07;
        /// <summary>
        /// Таблица 8. Минимальные, максимальные, средние УПКС по кадастровым кварталам
        /// </summary>
        [KoUnloadResultType(KoUnloadResultType.UnloadTable08)]
        public bool UnloadTable08;
        /// <summary>
        /// Таблица 9. Результаты определения кадастровой стоимости
        /// </summary>
        [KoUnloadResultType(KoUnloadResultType.UnloadTable09)]
        public bool UnloadTable09;
        /// <summary>
        /// Таблица 10. Результаты государственной кадастровой оценки
        /// </summary>
        [KoUnloadResultType(KoUnloadResultType.UnloadTable10)]
        public bool UnloadTable10;
        /// <summary>
        /// Таблица 11. Сводные результаты по кадастровому району
        /// </summary>
        [KoUnloadResultType(KoUnloadResultType.UnloadTable11)]
        public bool UnloadTable11;
        /// <summary>
        /// Выгрузка XML 1: КНомер, УПКСЗ, КСтоимость
        /// </summary>
        [KoUnloadResultType(KoUnloadResultType.UnloadXML1)]
        public bool UnloadXML1;
        /// <summary>
        /// Выгрузка XML 2 результатов Кадастровой оценки по группам.
        /// </summary>
        [KoUnloadResultType(KoUnloadResultType.UnloadXML2)]
        public bool UnloadXML2;
        /// <summary>
        /// Выгрузка XML результатов Кадастровой оценки по исходящим документам.
        /// </summary>
        [KoUnloadResultType(KoUnloadResultType.UnloadDEKOResponseDocExportToXml)]
        public bool UnloadDEKOResponseDocExportToXml;
        /// <summary>
        /// Выгрузка XML результатов Кадастровой оценки для ВУОН.
        /// </summary>
        [KoUnloadResultType(KoUnloadResultType.UnloadDEKOVuonExportToXml)]
        public bool UnloadDEKOVuonExportToXml;

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

	    public bool NoResult { get; set; }

		public long TaskId { get; set; }
    }

    public class KoUnloadResultTypeAttribute : Attribute
    {
        public KoUnloadResultType UnloadType { get; }

        public KoUnloadResultTypeAttribute(KoUnloadResultType unloadType)
        {
	        UnloadType = unloadType;
        }
    }
}
