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
	public class ImportObjectBaseParcel : ImportObjectBase<xmlObjectParcel>
    {
		public override string ErrorMessage => "Ошибка импорта данных ГКН во время загрузки Участков";
		public override string CancelMessage => "Импорт данных ГКН был отменен во время загрузки Участков";
		public override string SuccessMessage => "Импорт Участков завершен";

		protected override void ImportObject(xmlObjectParcel current, DateTime unitDate, long idTour, long idTask, KoNoteType koNoteType, DateTime sDate, DateTime otDate, long idDocument)
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
                            ObjectType_Code = PropertyTypes.Stead,
                        };
                        gbuObject.Save();
                    }
                    else
                    {
                        if (gbuObject.ObjectType_Code != PropertyTypes.Stead)
                        {
                            gbuObject.ObjectType_Code = PropertyTypes.Stead;
                            gbuObject.Save();
                        }
                    }
                    #endregion

                    //Сохранение данных ГКН
                    SaveGknDataParcel(current, gbuObject.Id, sDate, otDate, idDocument);
                    //Задание на оценку
                    ObjectModel.KO.OMUnit koUnit = SaveUnitParcel(current, gbuObject.Id, unitDate, idTour, idTask, koUnitStatus, GetNewtatusRepeatCalc(prev));
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
                        //TODO CIPJSKO-535 ждем ответа от заказчиков - что сохранять в качестве ЗУ
                        List<long> attribIds = new List<long>
                    {
                        //ParcelAttributeId,
                        1,   //Наименование участка
                        2,   //Площадь 
                        3,   //Категория земель 
                        4,   //Вид использования по документам
                        5,   //Вид использования по классификатору
                        8,   //Местоположение 
                        600, //Адрес 
                        601  //Кадастровый квартал 
                    };

                        List<GbuObjectAttribute> prevAttrib = new GbuObjectService().GetAllAttributes(koUnit.ObjectId.Value, sourceIds, attribIds, lastUnit.CreationDate);
                        List<GbuObjectAttribute> curAttrib = new GbuObjectService().GetAllAttributes(koUnit.ObjectId.Value, sourceIds, attribIds, koUnit.CreationDate);
                        bool prNameObjectCheck = CheckChange(koUnit, 1, KoChangeStatus.Name, prevAttrib, curAttrib);
                        var squareDidNotChange = CheckChange(koUnit, 2, KoChangeStatus.Square, prevAttrib, curAttrib);
                        CheckChange(koUnit, 3, KoChangeStatus.Category, prevAttrib, curAttrib);
                        bool prAssignationObjectCheck = CheckChange(koUnit, 4, KoChangeStatus.Use, prevAttrib, curAttrib);
                        var locationDidNotChange = CheckChange(koUnit, 8, KoChangeStatus.Place, prevAttrib, curAttrib);
                        var addressDidNotChange = CheckChange(koUnit, 600, KoChangeStatus.Adress, prevAttrib, curAttrib);
                        var cadastralQuartalDidNotChange = CheckChange(koUnit, 601, KoChangeStatus.CadastralBlock, prevAttrib, curAttrib);
                        //var zuNumberDidNotChange = CheckChange(koUnit, ParcelAttributeId, KoChangeStatus.NumberParcel, prevAttrib, curAttrib);

                        #region Наследование
                        if (!prCheckObr)
                        {
                            //Признак не поменялся ли тип объекта?
                            bool prTypeObjectCheck = lastUnit.PropertyType_Code == koUnit.PropertyType_Code;

                            //Если не было изменений типа, наименования и назначения и не было обращения
                            if (prTypeObjectCheck && prNameObjectCheck && prAssignationObjectCheck)
                            {
                                #region Наследование группы и подгруппы предыдущего объекта
                                koUnit.GroupId = lastUnit.GroupId;
                                koUnit.Save();
                                #endregion
                            }
                        }
                        #endregion

                        #region Признаки для формирования заданий ЦОД
                        if (!prNameObjectCheck || !prAssignationObjectCheck)
                        {
                            SetAttributeValue_Boolean(660, true, gbuObject.Id, idDocument, sDate, otDate, SRDSession.Current.UserID, otDate);
                        }
                        if (!cadastralQuartalDidNotChange || !locationDidNotChange)
                        {
                            SetAttributeValue_Boolean(661, true, gbuObject.Id, idDocument, sDate, otDate, SRDSession.Current.UserID, otDate);
                        }
                        #endregion
                    }
                    #endregion
                }
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
                            ObjectType_Code = PropertyTypes.Stead,
                        };
                        gbuObject.Save();
                    }
                    else
                    {
                        if (gbuObject.ObjectType_Code != PropertyTypes.Stead)
                        {
                            gbuObject.ObjectType_Code = PropertyTypes.Stead;
                            gbuObject.Save();
                        }
                    }
                    #endregion

                    //Сохранение данных ГКН
                    SaveGknDataParcel(current, gbuObject.Id, sDate, otDate, idDocument);
                    //Задание на оценку
                    ObjectModel.KO.OMUnit koUnit = SaveUnitParcel(current, gbuObject.Id, unitDate, idTour, idTask, koUnitStatus, koStatusRepeatCalc);

                    SaveHistoryForNewObject(koUnit);

                    #region Признаки для формирования заданий ЦОД
                    SetAttributeValue_Boolean(660, true, gbuObject.Id, idDocument, sDate, otDate, SRDSession.Current.UserID, otDate);
                    SetAttributeValue_Boolean(661, true, gbuObject.Id, idDocument, sDate, otDate, SRDSession.Current.UserID, otDate);
                    #endregion

                    #endregion
                }

                ts.Complete();
            }
        }
        private void SaveGknDataParcel(xmlObjectParcel current, long gbuObjectId, DateTime sDate, DateTime otDate, long idDocument)
        {
            //Наименование участка
            SetAttributeValue_String(1, current.Name.Name, gbuObjectId, idDocument, sDate, otDate, SRDSession.Current.UserID, otDate);
            //Площадь
            SetAttributeValue_Numeric(2, current.Area.ParseToDecimal(), gbuObjectId, idDocument, sDate, otDate, SRDSession.Current.UserID, otDate);
            //Дата образования
            SetAttributeValue_Date(13, (current.DateCreate == DateTime.MinValue) ? (DateTime?)null : current.DateCreate, gbuObjectId, idDocument, sDate, otDate, SRDSession.Current.UserID, otDate);
            //Тип объекта
            SetAttributeValue_String(26, current.TypeRealty, gbuObjectId, idDocument, sDate, otDate, SRDSession.Current.UserID, otDate);
            //Местоположение
            SetAttributeValue_String(8, xmlAdress.GetTextPlace(current.Adress), gbuObjectId, idDocument, sDate, otDate, SRDSession.Current.UserID, otDate);
            //Адрес
            SetAttributeValue_String(600, xmlAdress.GetTextAdress(current.Adress), gbuObjectId, idDocument, sDate, otDate, SRDSession.Current.UserID, otDate);
            //Кадастровая стоимость
            if (current.CadastralCost != null) SetAttributeValue_Numeric(6, current.CadastralCost.Value.ParseToDecimal(), gbuObjectId, idDocument, sDate, otDate, SRDSession.Current.UserID, otDate);
            //Кадастровый квартал
            SetAttributeValue_String(601, current.CadastralNumberBlock, gbuObjectId, idDocument, sDate, otDate, SRDSession.Current.UserID, otDate);
            //Категория земель
            SetAttributeValue_String(3, current.Category.Name, gbuObjectId, idDocument, sDate, otDate, SRDSession.Current.UserID, otDate);
            //Вид использования по документам
            SetAttributeValue_String(4, current.Utilization.ByDoc, gbuObjectId, idDocument, sDate, otDate, SRDSession.Current.UserID, otDate);
            //Вид использования по классификатору
            SetAttributeValue_String(5, current.Utilization.Utilization.Name, gbuObjectId, idDocument, sDate, otDate, SRDSession.Current.UserID, otDate);
            //TODO ждем ответа от заказчиков - что сохранять в качестве ЗУ
            //Земельный участок
            //SetAttributeValue_String(ParcelAttributeId, xmlCodeName.GetNames(current.InnerCadastralNumbers), gbuObjectId, idDocument, sDate, otDate, SRDSession.Current.UserID, otDate);
        }
        private static ObjectModel.KO.OMUnit SaveUnitParcel(xmlObjectParcel current, long gbuObjectId, DateTime unitDate, long idTour, long idTask, KoUnitStatus unitStatus, KoStatusRepeatCalc calcStatus)
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
                    PropertyType_Code = PropertyTypes.Stead,
                    StatusRepeatCalc_Code = calcStatus,
                    StatusResultCalc_Code = KoStatusResultCalc.None,
                    CadastralCost = 0,
                    CadastralCostPre = 0,
                    Upks = 0,
                    UpksPre = 0,
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
