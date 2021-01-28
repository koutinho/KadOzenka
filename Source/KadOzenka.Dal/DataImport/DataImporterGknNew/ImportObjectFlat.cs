using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;
using Core.Shared.Extensions;
using Core.Shared.Misc;
using Core.SRD;
using KadOzenka.Dal.GbuObject;
using KadOzenka.Dal.XmlParser;
using ObjectModel.Directory;

namespace KadOzenka.Dal.DataImport.DataImporterGknNew
{
	public class ImportObjectFlat : ImportObjectBase<xmlObjectFlat>
    {
		public override string ErrorMessage => "Ошибка импорта данных ГКН во время загрузки Помещений";
		public override string CancelMessage => "Импорт данных ГКН был отменен во время загрузки Помещений";
		public override string SuccessMessage => "Импорт Помещений завершен";

		protected override void ImportObject(xmlObjectFlat current, DateTime unitDate, long idTour, long idTask, KoNoteType koNoteType, DateTime sDate, DateTime otDate, long idDocument)
        {
            using (var ts = TransactionScopeWrapper.OpenTransaction(TransactionScopeOption.Required))
            {
                KoUnitStatus koUnitStatus = KoUnitStatus.New;
                KoStatusRepeatCalc koStatusRepeatCalc = KoStatusRepeatCalc.New;
                switch (koNoteType)
                {
                    case KoNoteType.Day:
                        koUnitStatus = KoUnitStatus.New;
                        break;
                    case KoNoteType.Petition:
                        koUnitStatus = KoUnitStatus.New;
                        break;
                    case KoNoteType.Year:
                        koUnitStatus = KoUnitStatus.Annual;
                        break;
                    case KoNoteType.Initial:
                        koUnitStatus = KoUnitStatus.Initial;
                        koStatusRepeatCalc = KoStatusRepeatCalc.Initial;
                        break;
                }

                #region Получение данных о прошлой оценке данного объекта
                List<ObjectModel.KO.OMUnit> prev = ObjectModel.KO.OMUnit.Where(x => x.CadastralNumber == current.CadastralNumber && x.TourId == idTour).SelectAll().Execute();
                #endregion

                //Если данные о прошлой оценке найдены
                if (prev.Count > 0)
                {
                    #region Импорт нового объекта
                    ObjectModel.Gbu.OMMainObject gbuObject = ObjectModel.Gbu.OMMainObject.Where(x => x.Id == prev[0].ObjectId).SelectAll().ExecuteFirstOrDefault();
                    #region Сохранение объекта
                    if (gbuObject == null)
                    {
                        gbuObject = new ObjectModel.Gbu.OMMainObject
                        {
                            Id = -1,
                            CadastralNumber = current.CadastralNumber,
                            IsActive = true,
                            ObjectType_Code = PropertyTypes.Pllacement,
                        };
                        gbuObject.Save();
                    }
                    else
                    {
                        if (gbuObject.ObjectType_Code != PropertyTypes.Pllacement)
                        {
                            gbuObject.ObjectType_Code = PropertyTypes.Pllacement;
                            gbuObject.Save();
                        }
                    }
                    #endregion

                    //Сохранение данных ГКН
                    SaveGknDataFlat(current, gbuObject.Id, sDate, otDate, idDocument);
                    //Задание на оценку
                    ObjectModel.KO.OMUnit koUnit = SaveUnitFlat(current, gbuObject.Id, unitDate, idTour, idTask, koUnitStatus, GetNewtatusRepeatCalc(prev));
                    #endregion

                    #region Анализ данных
                    //Признак было ли по данному объекту обращение?
                    bool prCheckObr = false;
                    //получить историю по объекту и узнать был ли он получен в рамках обращения (по типу входящего документа)
                    //надо перебрать все документы и узнать это
                    List<ObjectModel.KO.HistoryUnit> olds = ObjectModel.KO.HistoryUnit.GetPrevHistoryTour(koUnit);
                    foreach (ObjectModel.KO.HistoryUnit old in olds)
                    {
                        if (old.Task.NoteType_Code == KoNoteType.Petition && old.Unit.CreationDate < koUnit.CreationDate && koUnit.Id != old.Unit.Id)
                            prCheckObr = true;
                    }
                    ObjectModel.KO.OMUnit lastUnit = null;
                    if (olds.Count > 0) lastUnit = ObjectModel.KO.HistoryUnit.GetPrevUnit(olds).Unit;
                    if (lastUnit != null)
                    {
                        List<long> sourceIds = new List<long>
                    {
                        2
                    };
                        List<long> attribIds = new List<long>
                    {
                        2,   //Площадь 
                        23,  //Назначение помещения 
                        19,  //Наименование объекта 
                        14,  //Назначение здания 
                        15,  //Год постройки 
                        16,  //Год ввода в эксплуатацию 
                        17,  //Количество этажей 
                        18,  //Количество подземных этажей 
                        21,  //Материал стен 
                        8,   //Местоположение 
                        600, //Адрес 
                        601, //Кадастровый квартал 
                        24,  //Номер этажа 
                        604  //Кадастровый номер здания 
                    };

                        List<GbuObjectAttribute> prevAttrib = new GbuObjectService().GetAllAttributes(koUnit.ObjectId.Value, sourceIds, attribIds, lastUnit.CreationDate);
                        List<GbuObjectAttribute> curAttrib = new GbuObjectService().GetAllAttributes(koUnit.ObjectId.Value, sourceIds, attribIds, koUnit.CreationDate);
                        var squareDidNotChange = CheckChange(koUnit, 2, KoChangeStatus.Square, prevAttrib, curAttrib);
                        var purposeOksDidNotChange = CheckChange(koUnit, 23, KoChangeStatus.Assignment, prevAttrib, curAttrib);
                        var nameDidNotChange = CheckChange(koUnit, 19, KoChangeStatus.Name, prevAttrib, curAttrib);
                        var useDidNotChange = CheckChange(koUnit, 14, KoChangeStatus.Use, prevAttrib, curAttrib);
                        var buildYearDidNotChange = CheckChange(koUnit, 15, KoChangeStatus.YearBuild, prevAttrib, curAttrib);
                        var commissioningYearDidNotChange = CheckChange(koUnit, 16, KoChangeStatus.YearUse, prevAttrib, curAttrib);
                        var floorsCountDidNotChange = CheckChange(koUnit, 17, KoChangeStatus.Floors, prevAttrib, curAttrib);
                        var undergroundFloorsCountDidNotChange = CheckChange(koUnit, 18, KoChangeStatus.DownFloors, prevAttrib, curAttrib);
                        var wallMaterialDidNotChange = CheckChange(koUnit, 21, KoChangeStatus.Walls, prevAttrib, curAttrib);
                        var locationDidNotChange = CheckChange(koUnit, 8, KoChangeStatus.Place, prevAttrib, curAttrib);
                        var addressDidNotChange = CheckChange(koUnit, 600, KoChangeStatus.Adress, prevAttrib, curAttrib);
                        var cadastralQuartalDidNotChange = CheckChange(koUnit, 601, KoChangeStatus.CadastralBlock, prevAttrib, curAttrib);
                        CheckChange(koUnit, 604, KoChangeStatus.CadastralBuilding, prevAttrib, curAttrib);
                        CheckChange(koUnit, 24, KoChangeStatus.NumberFloor, prevAttrib, curAttrib);

                        #region Наследование
                        if (!prCheckObr)
                        {
                            //Признак не поменялся ли тип объекта?
                            bool prTypeObjectCheck = lastUnit.PropertyType_Code == koUnit.PropertyType_Code;

                            //Если не было изменений типа, наименования и назначения и не было обращения
                            if (prTypeObjectCheck && purposeOksDidNotChange && nameDidNotChange)
                            {
                                #region Наследование группы и подгруппы предыдущего объекта
                                koUnit.GroupId = lastUnit.GroupId;
                                koUnit.Save();
                                #endregion
                            }
                        }
                        #endregion

                        #region Признаки для формирования заданий ЦОД
                        if (!nameDidNotChange || !purposeOksDidNotChange)
                        {
                            SetAttributeValue_Boolean(660, true, gbuObject.Id, idDocument, sDate, otDate, SRDSession.Current.UserID, otDate);
                        }
                        if (!cadastralQuartalDidNotChange || !locationDidNotChange)
                        {
                            SetAttributeValue_Boolean(661, true, gbuObject.Id, idDocument, sDate, otDate, SRDSession.Current.UserID, otDate);
                        }
                        if (!wallMaterialDidNotChange)
                        {
                            SetAttributeValue_Boolean(662, true, gbuObject.Id, idDocument, sDate, otDate, SRDSession.Current.UserID, otDate);
                        }
                        if (!buildYearDidNotChange || !commissioningYearDidNotChange)
                        {
                            SetAttributeValue_Boolean(663, true, gbuObject.Id, idDocument, sDate, otDate, SRDSession.Current.UserID, otDate);
                        }
                        #endregion
                    }

                    #endregion
                }
                //Если данные о прошлой оценке не найдены
                else
                {
                    #region Импорт нового объекта
                    ObjectModel.Gbu.OMMainObject gbuObject = ObjectModel.Gbu.OMMainObject.Where(x => x.CadastralNumber == current.CadastralNumber).SelectAll().ExecuteFirstOrDefault();
                    #region Сохранение объекта
                    if (gbuObject == null)
                    {
                        gbuObject = new ObjectModel.Gbu.OMMainObject
                        {
                            Id = -1,
                            CadastralNumber = current.CadastralNumber,
                            IsActive = true,
                            ObjectType_Code = PropertyTypes.Pllacement,
                        };
                        gbuObject.Save();
                    }
                    else
                    {
                        if (gbuObject.ObjectType_Code != PropertyTypes.Pllacement)
                        {
                            gbuObject.ObjectType_Code = PropertyTypes.Pllacement;
                            gbuObject.Save();
                        }
                    }
                    #endregion

                    //Сохранение данных ГКН
                    SaveGknDataFlat(current, gbuObject.Id, sDate, otDate, idDocument);

                    //Задание на оценку
                    ObjectModel.KO.OMUnit koUnit = SaveUnitFlat(current, gbuObject.Id, unitDate, idTour, idTask, koUnitStatus, koStatusRepeatCalc);

                    SaveHistoryForNewObject(koUnit);

                    #region Признаки для формирования заданий ЦОД
                    SetAttributeValue_Boolean(660, true, gbuObject.Id, idDocument, sDate, otDate, SRDSession.Current.UserID, otDate);
                    SetAttributeValue_Boolean(661, true, gbuObject.Id, idDocument, sDate, otDate, SRDSession.Current.UserID, otDate);
                    SetAttributeValue_Boolean(662, true, gbuObject.Id, idDocument, sDate, otDate, SRDSession.Current.UserID, otDate);
                    SetAttributeValue_Boolean(663, true, gbuObject.Id, idDocument, sDate, otDate, SRDSession.Current.UserID, otDate);
                    #endregion

                    #endregion
                }

                ts.Complete();
            }
        }

        private void SaveGknDataFlat(xmlObjectFlat current, long gbuObjectId, DateTime sDate, DateTime otDate, long idDocument)
        {
            //Площадь
            SetAttributeValue_Numeric(2, current.Area.ParseToDecimal(), gbuObjectId, idDocument, sDate, otDate, SRDSession.Current.UserID, otDate);
            //Дата образования
            SetAttributeValue_Date(13, (current.DateCreate == DateTime.MinValue) ? (DateTime?)null : current.DateCreate, gbuObjectId, idDocument, sDate, otDate, SRDSession.Current.UserID, otDate);
            //Назначение помещения
            if (current.AssignationFlatCode != null)
                SetAttributeValue_String(23, current.AssignationFlatCode.Name, gbuObjectId, idDocument, sDate, otDate, SRDSession.Current.UserID, otDate);
            //Наименование объекта
            SetAttributeValue_String(19, current.Name, gbuObjectId, idDocument, sDate, otDate, SRDSession.Current.UserID, otDate);

            //Назначение здания
            if (current.parentAssignationBuilding != null) SetAttributeValue_String(14, current.parentAssignationBuilding.Name, gbuObjectId, idDocument, sDate, otDate, SRDSession.Current.UserID, otDate);
            //Назначение сооружения
            if (current.parentAssignationName != null && current.parentAssignationName != string.Empty) SetAttributeValue_String(22, current.parentAssignationName, gbuObjectId, idDocument, sDate, otDate, SRDSession.Current.UserID, otDate);

            if (current.parentFloors != null)
            {
                //Количество этажей
                SetAttributeValue_String(17, current.parentFloors.Floors, gbuObjectId, idDocument, sDate, otDate, SRDSession.Current.UserID, otDate);
                //Количество подземных этажей
                SetAttributeValue_String(18, current.parentFloors.Underground_Floors, gbuObjectId, idDocument, sDate, otDate, SRDSession.Current.UserID, otDate);
            }
            if (current.parentYears != null)
            {
                //Год ввода в эксплуатацию
                SetAttributeValue_String(16, current.parentYears.Year_Used, gbuObjectId, idDocument, sDate, otDate, SRDSession.Current.UserID, otDate);
                //Год постройки
                SetAttributeValue_String(15, current.parentYears.Year_Built, gbuObjectId, idDocument, sDate, otDate, SRDSession.Current.UserID, otDate);
            }
            //Тип объекта
            SetAttributeValue_String(26, current.TypeRealty, gbuObjectId, idDocument, sDate, otDate, SRDSession.Current.UserID, otDate);
            //Материал стен
            string pWalls = xmlCodeName.GetNames(current.parentWalls);
            if (pWalls != string.Empty)
                SetAttributeValue_String(21, pWalls, gbuObjectId, idDocument, sDate, otDate, SRDSession.Current.UserID, otDate);
            //Местоположение
            SetAttributeValue_String(8, xmlAdress.GetTextPlace(current.Adress), gbuObjectId, idDocument, sDate, otDate, SRDSession.Current.UserID, otDate);
            //Адрес
            SetAttributeValue_String(600, xmlAdress.GetTextAdress(current.Adress), gbuObjectId, idDocument, sDate, otDate, SRDSession.Current.UserID, otDate);
            //Кадастровая стоимость
            if (current.CadastralCost != null) SetAttributeValue_Numeric(6, current.CadastralCost.Value.ParseToDecimal(), gbuObjectId, idDocument, sDate, otDate, SRDSession.Current.UserID, otDate);
            //Кадастровый квартал
            SetAttributeValue_String(601, current.CadastralNumberBlock, gbuObjectId, idDocument, sDate, otDate, SRDSession.Current.UserID, otDate);

            //Тип помещения
            if (current.AssignationFlatType != null) SetAttributeValue_String(603, current.AssignationFlatType.Name, gbuObjectId, idDocument, sDate, otDate, SRDSession.Current.UserID, otDate);
            //Кадастровый номер здания или сооружения, в котором расположено помещение
            SetAttributeValue_String(604, current.CadastralNumberOKS, gbuObjectId, idDocument, sDate, otDate, SRDSession.Current.UserID, otDate);
            //Кадастровый номер квартиры, в которой расположена комната
            SetAttributeValue_String(605, current.CadastralNumberFlat, gbuObjectId, idDocument, sDate, otDate, SRDSession.Current.UserID, otDate);

            if (current.PositionsInObject.Count > 0)
            {
                //Номер на плане
                SetAttributeValue_String(606, xmlCodeName.GetNames(current.PositionsInObject[0].NumbersOnPlan), gbuObjectId, idDocument, sDate, otDate, SRDSession.Current.UserID, otDate);
                //Тип этажа
                SetAttributeValue_String(25, current.PositionsInObject[0].Position.Name, gbuObjectId, idDocument, sDate, otDate, SRDSession.Current.UserID, otDate);
                //Номер этажа
                SetAttributeValue_String(24, current.PositionsInObject[0].Position.Value, gbuObjectId, idDocument, sDate, otDate, SRDSession.Current.UserID, otDate);
            }
        }
        private static ObjectModel.KO.OMUnit SaveUnitFlat(xmlObjectFlat current, long gbuObjectId, DateTime unitDate, long idTour, long idTask, KoUnitStatus unitStatus, KoStatusRepeatCalc calcStatus)
        {
            ObjectModel.KO.OMUnit koUnit = ObjectModel.KO.OMUnit.Where(x => x.ObjectId == gbuObjectId && x.TaskId == idTask && x.TourId == idTour).SelectAll().ExecuteFirstOrDefault();
            if (koUnit == null)
            {
                koUnit = new ObjectModel.KO.OMUnit
                {
                    Id = -1,
                    TourId = idTour,
                    TaskId = idTask,
                    GroupId = -1,
                    Status_Code = unitStatus,
                    ObjectId = gbuObjectId,
                    CreationDate = unitDate,
                    CadastralNumber = current.CadastralNumber,
                    CadastralBlock = current.CadastralNumberBlock,
                    Square = current.Area.ParseToDecimal(),
                    PropertyType_Code = PropertyTypes.Pllacement,
                    StatusRepeatCalc_Code = calcStatus,
                    StatusResultCalc_Code = KoStatusResultCalc.None,
                    CadastralCost = 0,
                    CadastralCostPre = 0,
                    Upks = 0,
                    UpksPre = 0,
                    BuildingCadastralNumber = current.CadastralNumberOKS
                };
                koUnit.Save();
            }

            if (current.CadastralCost != null)
            {
                ObjectModel.KO.OMCostRosreestr cost = ObjectModel.KO.OMCostRosreestr.Where(x => x.IdObject == koUnit.Id).SelectAll().ExecuteFirstOrDefault();
                if (cost == null)
                {
                    cost = new ObjectModel.KO.OMCostRosreestr
                    {
                        Id = -1,
                        Applicationdate = (current.CadastralCost.ApplicationDate == DateTime.MinValue) ? (DateTime?)null : current.CadastralCost.ApplicationDate,
                        Dateapproval = (current.CadastralCost.DateApproval == DateTime.MinValue) ? (DateTime?)null : current.CadastralCost.DateApproval,
                        Dateentering = (current.CadastralCost.DateEntering == DateTime.MinValue) ? (DateTime?)null : current.CadastralCost.DateEntering,
                        Datevaluation = (current.CadastralCost.DateValuation == DateTime.MinValue) ? (DateTime?)null : current.CadastralCost.DateValuation,
                        Docdate = (current.CadastralCost.DocDate == DateTime.MinValue) ? (DateTime?)null : current.CadastralCost.DocDate,
                        Docnumber = current.CadastralCost.DocNumber,
                        Docname = current.CadastralCost.DocName,
                        IdObject = koUnit.Id,
                        Costvalue = current.CadastralCost.Value.ParseToDecimal(),
                        Revisalstatementdate = (current.CadastralCost.RevisalStatementDate == DateTime.MinValue) ? (DateTime?)null : current.CadastralCost.RevisalStatementDate,
                    };
                    cost.Save();
                }
            }

            return koUnit;
        }
    }
}
