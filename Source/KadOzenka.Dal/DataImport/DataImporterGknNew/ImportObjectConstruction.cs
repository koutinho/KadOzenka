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
	public class ImportObjectBaseConstruction : ImportObjectBase<xmlObjectConstruction>
    {
		public override string ErrorMessage => "Ошибка импорта данных ГКН во время загрузки Сооружений";
		public override string CancelMessage => "Импорт данных ГКН был отменен во время загрузки Сооружений";
		public override string SuccessMessage => "Импорт Сооружений завершен";

		/// <summary>
		/// Аттрибут "Земельный участок"
		/// </summary>
		private const long ParcelAttributeId = 602;
		private const long WallMaterialAttributeId = 21;


		protected override void ImportObject(xmlObjectConstruction current, DateTime unitDate, long idTour, long idTask, KoNoteType koNoteType, DateTime sDate, DateTime otDate, long idDocument)
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
                            ObjectType_Code = PropertyTypes.Construction,
                        };
                        gbuObject.Save();
                    }
                    else
                    {
                        if (gbuObject.ObjectType_Code != PropertyTypes.Construction)
                        {
                            gbuObject.ObjectType_Code = PropertyTypes.Construction;
                            gbuObject.Save();
                        }
                    }
                    #endregion

                    //Сохранение данных ГКН
                    SaveGknDataConstruction(current, gbuObject.Id, sDate, otDate, idDocument);
                    //Задание на оценку
                    ObjectModel.KO.OMUnit koUnit = SaveUnitConstruction(current, gbuObject.Id, unitDate, idTour, idTask, koUnitStatus, GetNewtatusRepeatCalc(prev));
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
                             WallMaterialAttributeId,
                             44,  //Площадь 
                             22,  //Назначение
                             15,  //Год постройки 
                             16,  //Год ввода в эксплуатацию 
                             17,  //Количество этажей 
                             18,  //Количество подземных этажей 
                             19,  //Наименование
                             8,   //Местоположение 
                             600, //Адрес 
                             601, //Кадастровый квартал 
                             602  //Земельный участок 
                         };

                        List<GbuObjectAttribute> prevAttrib = new GbuObjectService().GetAllAttributes(koUnit.ObjectId.Value, sourceIds, attribIds, lastUnit.CreationDate);
                        List<GbuObjectAttribute> curAttrib = new GbuObjectService().GetAllAttributes(koUnit.ObjectId.Value, sourceIds, attribIds, koUnit.CreationDate);
                        var squareDidNotChange = CheckChange(koUnit, 44, KoChangeStatus.Square, prevAttrib, curAttrib);
                        bool prAssignationObjectCheck = CheckChange(koUnit, 22, KoChangeStatus.Assignment, prevAttrib, curAttrib);
                        bool prYearBuiltObjectCheck = CheckChange(koUnit, 15, KoChangeStatus.YearBuild, prevAttrib, curAttrib);
                        bool prYearUsedObjectCheck = CheckChange(koUnit, 16, KoChangeStatus.YearUse, prevAttrib, curAttrib);
                        var floorsCountDidNotChange = CheckChange(koUnit, 17, KoChangeStatus.Floors, prevAttrib, curAttrib);
                        var undergroundFloorsCountDidNotChange = CheckChange(koUnit, 18, KoChangeStatus.DownFloors, prevAttrib, curAttrib);
                        bool prNameObjectCheck = CheckChange(koUnit, 19, KoChangeStatus.Name, prevAttrib, curAttrib);
                        var locationDidNotChange = CheckChange(koUnit, 8, KoChangeStatus.Place, prevAttrib, curAttrib);
                        var addressDidNotChange = CheckChange(koUnit, 600, KoChangeStatus.Adress, prevAttrib, curAttrib);
                        var cadastralQuartalDidNotChange = CheckChange(koUnit, 601, KoChangeStatus.CadastralBlock, prevAttrib, curAttrib);
                        var zuNumberDidNotChange = CheckChange(koUnit, 602, KoChangeStatus.NumberParcel, prevAttrib, curAttrib);
                        var wallMaterialDidNotChange = CheckChange(koUnit, WallMaterialAttributeId, KoChangeStatus.Walls, prevAttrib, curAttrib);

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

                            //Если год ввода в эксплуатацию и год завершения строительства  не поменялся
                            ObjectModel.KO.OMFactorSettings fsyear = ObjectModel.KO.OMFactorSettings.Where(x => x.Inheritance_Code == ObjectModel.Directory.KO.FactorInheritance.ftYear).SelectAll().ExecuteFirstOrDefault();
                            if (fsyear != null)
                            {
                                if (fsyear.FactorId != null)
                                {
                                    long id_factor = fsyear.FactorId.ParseToLong();
                                    //Если в предыдущем объекте есть фактор Год постройки итоговый
                                    //его надо скопировать в новый объект, если нет, добавить надо.
                                    koUnit.AddKOFactor(id_factor, (prYearUsedObjectCheck && prYearBuiltObjectCheck) ? lastUnit : null, string.Empty);
                                }
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
                        if (!prYearBuiltObjectCheck || !prYearUsedObjectCheck)
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
                            ObjectType_Code = PropertyTypes.Construction,
                        };
                        gbuObject.Save();
                    }
                    else
                    {
                        if (gbuObject.ObjectType_Code != PropertyTypes.Construction)
                        {
                            gbuObject.ObjectType_Code = PropertyTypes.Construction;
                            gbuObject.Save();
                        }
                    }
                    #endregion

                    //Сохранение данных ГКН
                    SaveGknDataConstruction(current, gbuObject.Id, sDate, otDate, idDocument);

                    //Задание на оценку
                    ObjectModel.KO.OMUnit koUnit = SaveUnitConstruction(current, gbuObject.Id, unitDate, idTour, idTask, koUnitStatus, koStatusRepeatCalc);

                    SaveHistoryForNewObject(koUnit);
                    #endregion

                    #region Признаки для формирования заданий ЦОД
                    SetAttributeValue_Boolean(660, true, gbuObject.Id, idDocument, sDate, otDate, SRDSession.Current.UserID, otDate);
                    SetAttributeValue_Boolean(661, true, gbuObject.Id, idDocument, sDate, otDate, SRDSession.Current.UserID, otDate);
                    SetAttributeValue_Boolean(663, true, gbuObject.Id, idDocument, sDate, otDate, SRDSession.Current.UserID, otDate);
                    #endregion

                }

                ts.Complete();
            }
        }
        private void SaveGknDataConstruction(xmlObjectConstruction current, long gbuObjectId, DateTime sDate, DateTime otDate, long idDocument)
        {
            //Площадь
            SetAttributeValue_String(44, xmlCodeNameValue.GetNames(current.KeyParameters), gbuObjectId, idDocument, sDate, otDate, SRDSession.Current.UserID, otDate);
            //Дата образования
            SetAttributeValue_Date(13, (current.DateCreate == DateTime.MinValue) ? (DateTime?)null : current.DateCreate, gbuObjectId, idDocument, sDate, otDate, SRDSession.Current.UserID, otDate);
            //Назначение сооружения
            SetAttributeValue_String(22, current.AssignationName, gbuObjectId, idDocument, sDate, otDate, SRDSession.Current.UserID, otDate);
            //Наименование объекта
            SetAttributeValue_String(19, current.Name, gbuObjectId, idDocument, sDate, otDate, SRDSession.Current.UserID, otDate);

            if (current.Floors != null)
            {
                //Количество этажей
                SetAttributeValue_String(17, current.Floors.Floors, gbuObjectId, idDocument, sDate, otDate, SRDSession.Current.UserID, otDate);
                //Количество подземных этажей
                SetAttributeValue_String(18, current.Floors.Underground_Floors, gbuObjectId, idDocument, sDate, otDate, SRDSession.Current.UserID, otDate);
            }
            if (current.Years != null)
            {
                //Год ввода в эксплуатацию
                SetAttributeValue_String(16, current.Years.Year_Used, gbuObjectId, idDocument, sDate, otDate, SRDSession.Current.UserID, otDate);
                //Год постройки
                SetAttributeValue_String(15, current.Years.Year_Built, gbuObjectId, idDocument, sDate, otDate, SRDSession.Current.UserID, otDate);
            }
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
            //Земельный участок
            SetAttributeValue_String(602, xmlCodeName.GetNames(current.ParentCadastralNumbers), gbuObjectId, idDocument, sDate, otDate, SRDSession.Current.UserID, otDate);
            //Материал стен
            SetAttributeValue_String(WallMaterialAttributeId, xmlCodeName.GetNames(current.Walls), gbuObjectId, idDocument, sDate, otDate, SRDSession.Current.UserID, otDate);
        }
        private static ObjectModel.KO.OMUnit SaveUnitConstruction(xmlObjectConstruction current, long gbuObjectId, DateTime unitDate, long idTour, long idTask, KoUnitStatus unitStatus, KoStatusRepeatCalc calcStatus)
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
                    PropertyType_Code = PropertyTypes.Construction,
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
