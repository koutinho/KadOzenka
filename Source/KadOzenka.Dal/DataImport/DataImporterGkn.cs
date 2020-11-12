using Core.ErrorManagment;
using Core.Main.FileStorages;
using Core.Register;
using Core.Shared.Exceptions;
using Core.Shared.Extensions;
using Core.Shared.Misc;
using Core.SRD;
using GemBox.Spreadsheet;
using KadOzenka.Dal.GbuObject;
using KadOzenka.Dal.XmlParser;
using ObjectModel.Commission;
using ObjectModel.Directory;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using ObjectModel.KO;

namespace KadOzenka.Dal.DataImport
{
    public class DataImporterGkn
    {
        //Объект-блокиратор для многопоточки
        private static object locked = new object();
        //ID фактора Материал стен итоговый
        public Int64 Id_Factor_Wall { get; private set; }
        //ID фактора Год постройки итоговый
        public Int64 Id_Factor_Year { get; private set; }

        /// <summary>
        /// Колличество зданий в Xml
        /// </summary>
        public Int32 CountXmlBuildings { get; private set; }
        /// <summary>
        /// Колличество участков в Xml
        /// </summary>
        public Int32 CountXmlParcels { get; private set; }
        /// <summary>
        /// Колличество сооружений в Xml
        /// </summary>
        public Int32 CountXmlConstructions { get; private set; }
        /// <summary>
        /// Колличество объектов незавершенного строительства в Xml
        /// </summary>
        public Int32 CountXmlUncompliteds { get; private set; }
        /// <summary>
        /// Колличество помещений в Xml
        /// </summary>
        public Int32 CountXmlFlats { get; private set; }
        /// <summary>
        /// Колличество машиномест в Xml
        /// </summary>
        public Int32 CountXmlCarPlaces { get; private set; }

        /// <summary>
        /// Колличество загруженных зданий из Xml
        /// </summary>
        public Int32 CountImportBuildings { get; private set; }
        /// <summary>
        /// Колличество загруженных участков из Xml
        /// </summary>
        public Int32 CountImportParcels { get; private set; }
        /// <summary>
        /// Колличество загруженных сооружений из Xml
        /// </summary>
        public Int32 CountImportConstructions { get; private set; }
        /// <summary>
        /// Колличество загруженных объектов незавершенного строительства из Xml
        /// </summary>
        public Int32 CountImportUncompliteds { get; private set; }
        /// <summary>
        /// Колличество загруженных помещений из Xml
        /// </summary>
        public Int32 CountImportFlats { get; private set; }
        /// <summary>
        /// Колличество загруженных машиномест из Xml
        /// </summary>
        public Int32 CountImportCarPlaces { get; private set; }
        
        private AbstractHandler _unitStatusHandler { get; set; }
        /// <summary>
        /// Обработчик статусов Юнита (паттерн Цепочка обязанностей)
        /// </summary>
        private AbstractHandler UnitStatusHandler
        {
	        get
	        {
		        if (_unitStatusHandler != null)
			        return _unitStatusHandler;

		        var groupAndFsAndCharacteristicHandler = new GroupAndFsAndCharacteristicChangesHandler();
		        var groupAndFsCharacteristicHandler = new GroupAndFsChangesHandler();
		        var groupAndEgrnChangesHandler = new GroupAndEgrnChangesHandler();

                var groupHandler = new GroupChangesHandler();
		        var egrnHandler = new EgrnChangesHandler();
		        var fsHandler = new FsChangesHandler();

		        groupAndFsAndCharacteristicHandler
			        .SetNext(groupAndFsCharacteristicHandler)
			        .SetNext(groupAndEgrnChangesHandler)
			        .SetNext(groupHandler)
			        .SetNext(egrnHandler)
			        .SetNext(fsHandler);

		        _unitStatusHandler = groupAndFsAndCharacteristicHandler;

		        return _unitStatusHandler;
	        }
        }

        public bool AreCountersInitialized { get; private set; }

        #region Attributes Ids

        /// <summary>
        /// Аттрибут "Земельный участок"
        /// </summary>
        private const long ParcelAttributeId = 602;
        private const long WallMaterialAttributeId = 21;

        #endregion


        public DataImporterGkn()
        {
            Id_Factor_Wall = -1;
            Id_Factor_Year = -1;
        }


        /// <summary>
        /// Импорт данных ГКН из Xml
        /// xmlFile - файл xml
        /// pathSchema - путь к каталогу где хранится схема
        /// task - ссылка на задание на оценку
        /// </summary>
        public void ImportDataGknFromXml(Stream xmlFile, string pathSchema, ObjectModel.KO.OMTask task)
        {
            ImportDataGknFromXml(xmlFile, pathSchema, task.EstimationDate.Value, task.TourId.Value, task.Id, task.NoteType_Code, task.EstimationDate.Value, task.EstimationDate.Value, task.DocumentId.Value);
        }

        /// <summary>
        /// Импорт данных ГКН из Excel
        /// excelFile - файл Excel
        /// pathSchema - путь к каталогу где хранится схема
        /// task - ссылка на задание на оценку
        /// </summary>
        public void ImportDataGknFromExcel(ExcelFile excelFile, string pathSchema, ObjectModel.KO.OMTask task)
        {
            ImportDataGknFromExcel(excelFile, pathSchema, task.CreationDate.Value, task.TourId.Value, task.Id, task.NoteType_Code, task.EstimationDate.Value, task.EstimationDate.Value, task.DocumentId.Value);
        }

        private void ImportDataGknFromExcel(ExcelFile excelFile, string pathSchema, DateTime unitDate, long idTour, long idTask, KoNoteType koNoteType, DateTime sDate, DateTime otDate, long idDocument)
        {
            xmlImportGkn.FillDictionary(pathSchema);
            xmlObjectList GknItems = xmlImportGkn.GetExcelObject(excelFile);

            CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
            ParallelOptions options = new ParallelOptions
            {
                CancellationToken = cancelTokenSource.Token,
                MaxDegreeOfParallelism = 10
            };

            CountXmlBuildings = GknItems.Buildings.Count;
            CountXmlParcels = GknItems.Parcels.Count;
            CountXmlConstructions = GknItems.Constructions.Count;
            CountXmlUncompliteds = GknItems.Uncompliteds.Count;
            CountXmlFlats = GknItems.Flats.Count;
            CountXmlCarPlaces = GknItems.CarPlaces.Count;
            CountImportBuildings = 0;
            CountImportParcels = 0;
            CountImportConstructions = 0;
            CountImportUncompliteds = 0;
            CountImportFlats = 0;
            CountImportCarPlaces = 0;
            AreCountersInitialized = true;

            Parallel.ForEach(GknItems.Buildings, options, item => ImportObjectBuild(item, unitDate, idTour, idTask, koNoteType, sDate, otDate, idDocument));
            Parallel.ForEach(GknItems.Parcels, options, item => ImportObjectParcel(item, unitDate, idTour, idTask, koNoteType, sDate, otDate, idDocument));
            Parallel.ForEach(GknItems.Constructions, options, item => ImportObjectConstruction(item, unitDate, idTour, idTask, koNoteType, sDate, otDate, idDocument));
            Parallel.ForEach(GknItems.Uncompliteds, options, item => ImportObjectUncomplited(item, unitDate, idTour, idTask, koNoteType, sDate, otDate, idDocument));
            Parallel.ForEach(GknItems.Flats, options, item => ImportObjectFlat(item, unitDate, idTour, idTask, koNoteType, sDate, otDate, idDocument));
            Parallel.ForEach(GknItems.CarPlaces, options, item => ImportObjectCarPlace(item, unitDate, idTour, idTask, koNoteType, sDate, otDate, idDocument));
        }
        
        private void ImportDataGknFromXml(Stream xmlFile, string pathSchema, DateTime unitDate, long idTour, long idTask, KoNoteType koNoteType, DateTime sDate, DateTime otDate, long idDocument)
        {
            xmlImportGkn.FillDictionary(pathSchema);
            xmlObjectList GknItems = xmlImportGkn.GetXmlObject(xmlFile);

            CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
            ParallelOptions options = new ParallelOptions
            {
                CancellationToken = cancelTokenSource.Token,
                MaxDegreeOfParallelism = 10
            };

            CountXmlBuildings = GknItems.Buildings.Count;
            CountXmlParcels = GknItems.Parcels.Count;
            CountXmlConstructions = GknItems.Constructions.Count;
            CountXmlUncompliteds = GknItems.Uncompliteds.Count;
            CountXmlFlats = GknItems.Flats.Count;
            CountXmlCarPlaces = GknItems.CarPlaces.Count;
            CountImportBuildings = 0;
            CountImportParcels = 0;
            CountImportConstructions = 0;
            CountImportUncompliteds = 0;
            CountImportFlats = 0;
            CountImportCarPlaces = 0;
            AreCountersInitialized = true;

            Parallel.ForEach(GknItems.Buildings, options, item => {
                try
                {
                    ImportObjectBuild(item, unitDate, idTour, idTask, koNoteType, sDate, otDate, idDocument);
                }
                catch (Exception ex)
                {
                    ErrorManager.LogError(ex);
                }
            });
            Parallel.ForEach(GknItems.Parcels, options, item => {
                try
                {
                    ImportObjectParcel(item, unitDate, idTour, idTask, koNoteType, sDate, otDate, idDocument);
                }
                catch (Exception ex)
                {
                    ErrorManager.LogError(ex);
                }
            });
            Parallel.ForEach(GknItems.Constructions, options, item => {
                try
                {
                    ImportObjectConstruction(item, unitDate, idTour, idTask, koNoteType, sDate, otDate, idDocument);
                }
                catch (Exception ex)
                {
                    ErrorManager.LogError(ex);
                }
            });
            Parallel.ForEach(GknItems.Uncompliteds, options, item => {
                try
                {
                    ImportObjectUncomplited(item, unitDate, idTour, idTask, koNoteType, sDate, otDate, idDocument);
                }
                catch (Exception ex)
                {
                    ErrorManager.LogError(ex);
                }
            });
            Parallel.ForEach(GknItems.Flats, options, item => {
                try
                {
                    ImportObjectFlat(item, unitDate, idTour, idTask, koNoteType, sDate, otDate, idDocument);
                }
                catch (Exception ex)
                {
                    ErrorManager.LogError(ex);
                }
            });
            Parallel.ForEach(GknItems.CarPlaces, options, item => {
                try
                {
                    ImportObjectCarPlace(item, unitDate, idTour, idTask, koNoteType, sDate, otDate, idDocument);
                }
                catch (Exception ex)
                {
                    ErrorManager.LogError(ex);
                }
            });

        }

        //TODO перенести в общий сервис, например, GbuObjectService
        public static void SaveAttributeValueWithCheck(GbuObjectAttribute attributeValue)
        {
            // TODO: разобраться почему перехваченное исключение все равно приводит к "25P02: текущая транзакция прервана, команды до конца блока транзакции игнорируются"
            //try
            //{
            //    attributeValue.Save();
            //}
            //catch (Exception ex)
            //{
            //    ErrorManager.LogError(ex);

            //    var existsAttributeValue = GbuObjectService.CheckExistsValueFromAttributeIdPartition(attributeValue.ObjectId, attributeValue.AttributeId, attributeValue.Ot);

            //    // Проблема не в наличии значения на ту же дату ОТ
            //    if (existsAttributeValue == null) throw ExceptionInitializer.Create("Ошибка сохранения знчения показателя", $"Значение: {attributeValue.SerializeToXml()}", ex);

            //    lock (locked)
            //    {
            //        // Проблема в наличии значения на ту же дату ОТ
            //        attributeValue.Ot = GbuObjectService.GetNextOtFromAttributeIdPartition(attributeValue.ObjectId, attributeValue.AttributeId, attributeValue.Ot);

            //        attributeValue.Save();
            //    }
            //}

            if (GbuObjectService.CheckExistsValueFromAttributeIdPartition(attributeValue.ObjectId, attributeValue.AttributeId, attributeValue.Ot) != null)
            {
                lock (locked)
                {
                    // Проблема в наличии значения на ту же дату ОТ
                    attributeValue.Ot = GbuObjectService.GetNextOtFromAttributeIdPartition(attributeValue.ObjectId, attributeValue.AttributeId, attributeValue.Ot);
                }
            }

            attributeValue.Save();
        }

        public static void SetAttributeValue_String(long idAttribute, string value, long idObject, long idDocument, DateTime sDate, DateTime otDate, long idUser, DateTime changeDate)
        {
            var attributeValue = new GbuObjectAttribute
            {
                Id = -1,
                AttributeId = idAttribute,
                ObjectId = idObject,
                ChangeDocId = idDocument,
                S = sDate,
                ChangeUserId = idUser,
                ChangeDate = DateTime.Now,
                Ot = otDate,
                StringValue = value,
            };
            SaveAttributeValueWithCheck(attributeValue);
        }

        public static void SetAttributeValue_Numeric(long idAttribute, decimal? value, long idObject, long idDocument, DateTime sDate, DateTime otDate, long idUser, DateTime changeDate)
        {
            var attributeValue = new GbuObjectAttribute
            {
                Id = -1,
                AttributeId = idAttribute,
                ObjectId = idObject,
                ChangeDocId = idDocument,
                S = sDate,
                ChangeUserId = idUser,
                ChangeDate = DateTime.Now,
                Ot = otDate,
                NumValue = value,
            };

            SaveAttributeValueWithCheck(attributeValue);
        }
        public static void SetAttributeValue_Boolean(long idAttribute, bool value, long idObject, long idDocument, DateTime sDate, DateTime otDate, long idUser, DateTime changeDate)
        {
            var attributeValue = new GbuObjectAttribute
            {
                Id = -1,
                AttributeId = idAttribute,
                ObjectId = idObject,
                ChangeDocId = idDocument,
                S = sDate,
                ChangeUserId = idUser,
                ChangeDate = DateTime.Now,
                Ot = otDate,
                NumValue=(value?1:0)
            };

            SaveAttributeValueWithCheck(attributeValue);
        }


        public static void SetAttributeValue_Date(long idAttribute, DateTime? value, long idObject, long idDocument, DateTime sDate, DateTime otDate, long idUser, DateTime changeDate)
        {
            var attributeValue = new GbuObjectAttribute
            {
                Id = -1,
                AttributeId = idAttribute,
                ObjectId = idObject,
                ChangeDocId = idDocument,
                S = sDate,
                ChangeUserId = idUser,
                ChangeDate = DateTime.Now,
                Ot = otDate,
                DtValue = value,
            };

            SaveAttributeValueWithCheck(attributeValue);
        }

        public static KoStatusRepeatCalc GetNewtatusRepeatCalc(List<ObjectModel.KO.OMUnit> units)
        {
            KoStatusRepeatCalc koStatusRepeatCalc = KoStatusRepeatCalc.New;
            if (units.Count > 0)
            {
                foreach (ObjectModel.KO.OMUnit unit in units)
                {
                    if (unit.StatusRepeatCalc_Code == KoStatusRepeatCalc.Initial || unit.StatusRepeatCalc_Code == KoStatusRepeatCalc.RepeatedInitial)
                        koStatusRepeatCalc = KoStatusRepeatCalc.RepeatedInitial;
                    if (unit.StatusRepeatCalc_Code == KoStatusRepeatCalc.New || unit.StatusRepeatCalc_Code == KoStatusRepeatCalc.Repeated)
                        koStatusRepeatCalc = KoStatusRepeatCalc.Repeated;
                }
            }
            return koStatusRepeatCalc;
        }

        public static bool CheckChange(ObjectModel.KO.OMUnit unit, long idAttribute, KoChangeStatus status, List<GbuObjectAttribute> prevAttribs, List<GbuObjectAttribute> curAttribs)
        {
            bool res = true;
            string oldValue = string.Empty;
            string newValue = string.Empty;
            GbuObjectAttribute prevAttrib = prevAttribs.Find(x => x.AttributeId == idAttribute);
            GbuObjectAttribute curAttrib = curAttribs.Find(x => x.AttributeId == idAttribute);
            if (prevAttrib != null) oldValue = prevAttrib.GetValueInString();
            if (curAttrib != null) newValue = curAttrib.GetValueInString();
            if (oldValue != newValue)
            {
                res = false;
                ObjectModel.KO.OMUnitChange unitChange = new ObjectModel.KO.OMUnitChange
                {
                    Id = -1,
                    ChangeStatus_Code = status,
                    NewValue = newValue,
                    OldValue = oldValue,
                    UnitId = unit.Id
                };
                unitChange.Save();
            }
            return res;
        }


        public void ImportObjectBuild(xmlObjectBuild current, DateTime unitDate, long idTour, long idTask, KoNoteType koNoteType, DateTime sDate, DateTime otDate, long idDocument)
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
                            ObjectType_Code = PropertyTypes.Building,
                        };
                        gbuObject.Save();
                    }
                    else
                    {
                        if (gbuObject.ObjectType_Code != PropertyTypes.Building)
                        {
                            gbuObject.ObjectType_Code = PropertyTypes.Building;
                            gbuObject.Save();
                        }
                    }
                    #endregion

                    //Сохранение данных ГКН
                    SaveGknDataBuilding(current, gbuObject.Id, sDate, otDate, idDocument);
                    //Задание на оценку
                    ObjectModel.KO.OMUnit koUnit = SaveUnitBuilding(current, gbuObject.Id, unitDate, idTour, idTask, koUnitStatus, GetNewtatusRepeatCalc(prev));
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
                             8,   //Местоположение 
                             14,  //Назначение здания 
                             15,  //Год постройки 
                             16,  //Год ввода в эксплуатацию 
                             17,  //Количество этажей 
                             18,  //Количество подземных этажей 
                             19,  //Наименование объекта 
                             21,  //Материал стен 
                             600, //Адрес 
                             601, //Кадастровый квартал 
                             602  //Земельный участок 
                         };

                        List<GbuObjectAttribute> prevAttrib = new GbuObjectService().GetAllAttributes(koUnit.ObjectId.Value, sourceIds, attribIds, lastUnit.CreationDate);
                        List<GbuObjectAttribute> curAttrib = new GbuObjectService().GetAllAttributes(koUnit.ObjectId.Value, sourceIds, attribIds, koUnit.CreationDate);
                        var squareDidNotChange = CheckChange(koUnit, 2, KoChangeStatus.Square, prevAttrib, curAttrib);
                        var locationDidNotChange = CheckChange(koUnit, 8, KoChangeStatus.Place, prevAttrib, curAttrib);
                        bool prAssignationObjectCheck = CheckChange(koUnit, 14, KoChangeStatus.Assignment, prevAttrib, curAttrib);
                        bool prYearBuiltObjectCheck = CheckChange(koUnit, 15, KoChangeStatus.YearBuild, prevAttrib, curAttrib);
                        bool prYearUsedObjectCheck = CheckChange(koUnit, 16, KoChangeStatus.YearUse, prevAttrib, curAttrib);
                        var floorsCountDidNotChange = CheckChange(koUnit, 17, KoChangeStatus.Floors, prevAttrib, curAttrib);
                        var undergroundFloorsCountDidNotChange = CheckChange(koUnit, 18, KoChangeStatus.DownFloors, prevAttrib, curAttrib);
                        bool prNameObjectCheck = CheckChange(koUnit, 19, KoChangeStatus.Name, prevAttrib, curAttrib);
                        bool prWallObjectCheck = CheckChange(koUnit, 21, KoChangeStatus.Walls, prevAttrib, curAttrib);
                        var addressDidNotChange = CheckChange(koUnit, 600, KoChangeStatus.Adress, prevAttrib, curAttrib);
                        var cadastralQuartalDidNotChange = CheckChange(koUnit, 601, KoChangeStatus.CadastralBlock, prevAttrib, curAttrib);
                        var zuNumberDidNotChange = CheckChange(koUnit, 602, KoChangeStatus.NumberParcel, prevAttrib, curAttrib);

                        var changedProperties = new UnitChangedProperties
                        {
                            IsNameChanged = !prNameObjectCheck,
                            IsPurposeOksChanged = !prAssignationObjectCheck,
                            IsSquareChanged = !squareDidNotChange,
                            IsBuildYearChanged = !prYearBuiltObjectCheck,
                            IsCommissioningYearChanged = !prYearUsedObjectCheck,
                            IsFloorsCountChanged = !floorsCountDidNotChange,
                            IsUndergroundFloorsCountChanged = !undergroundFloorsCountDidNotChange,
                            IsWallMaterialChanged = !prWallObjectCheck,
                            IsZuNumberChanged = !zuNumberDidNotChange,
                            IsAddressChanged = !addressDidNotChange,
                            IsCadasrtalQuartalChanged = !cadastralQuartalDidNotChange,
                            IsLocationChanged = !locationDidNotChange
                        };
                        CalculateUnitUpdateStatus(changedProperties, koUnit);

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

                            //Если материал стен не поменялся
                            ObjectModel.KO.OMFactorSettings fswall = ObjectModel.KO.OMFactorSettings.Where(x => x.Inheritance_Code == ObjectModel.Directory.KO.FactorInheritance.ftWall).SelectAll().ExecuteFirstOrDefault();
                            if (fswall != null)
                            {
                                if (fswall.FactorId != null)
                                {
                                    long id_factor = fswall.FactorId.ParseToLong();
                                    //Если в предыдущем объекте есть фактор Материал стен итоговый
                                    //его надо скопировать в новый объект, если нет, добавить надо.
                                    koUnit.AddKOFactor(id_factor, (prWallObjectCheck) ? lastUnit : null, xmlCodeName.GetNames(current.Walls));
                                }
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
                                    koUnit.AddKOFactor(id_factor, (prYearUsedObjectCheck && prYearBuiltObjectCheck) ? lastUnit : null, string.IsNullOrEmpty(current.Years.Year_Used) ? current.Years.Year_Built : current.Years.Year_Used);
                                }
                            }
                        }
                        #endregion

                        #region Признаки для формирования заданий ЦОД
                        //if (!prNameObjectCheck || !prAssignationObjectCheck)
                        //{
                        //    SetAttributeValue_Boolean(660, true, gbuObject.Id, idDocument, sDate, otDate, SRDSession.Current.UserID, otDate);
                        //}
                        //if (!cadastralQuartalDidNotChange || !locationDidNotChange)
                        //{
                        //    SetAttributeValue_Boolean(661, true, gbuObject.Id, idDocument, sDate, otDate, SRDSession.Current.UserID, otDate);
                        //}
                        //if (!prWallObjectCheck)
                        //{
                        //    SetAttributeValue_Boolean(662, true, gbuObject.Id, idDocument, sDate, otDate, SRDSession.Current.UserID, otDate);
                        //}
                        //if (!prYearBuiltObjectCheck || !prYearUsedObjectCheck)
                        //{
                        //    SetAttributeValue_Boolean(663, true, gbuObject.Id, idDocument, sDate, otDate, SRDSession.Current.UserID, otDate);
                        //}
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
                            ObjectType_Code = PropertyTypes.Building,
                        };
                        gbuObject.Save();
                    }
                    else
                    {
                        if (gbuObject.ObjectType_Code != PropertyTypes.Building)
                        {
                            gbuObject.ObjectType_Code = PropertyTypes.Building;
                            gbuObject.Save();
                        }
                    }
                    #endregion

                    //Сохранение данных ГКН
                    SaveGknDataBuilding(current, gbuObject.Id, sDate, otDate, idDocument);
                    //Задание на оценку
                    ObjectModel.KO.OMUnit koUnit = SaveUnitBuilding(current, gbuObject.Id, unitDate, idTour, idTask, koUnitStatus, koStatusRepeatCalc);
                    #endregion

                    SetNewUnitUpdateStatus(koUnit);
                    SaveHistoryForNewObject(koUnit);

                    #region Заполнение фактора Материал стен на основании данных ГКН
                    ObjectModel.KO.OMFactorSettings fs = ObjectModel.KO.OMFactorSettings.Where(x => x.Inheritance_Code == ObjectModel.Directory.KO.FactorInheritance.ftWall).SelectAll().ExecuteFirstOrDefault();
                    if (fs != null)
                    {
                        if (fs.FactorId != null)
                        {
                            long id_factor = fs.FactorId.ParseToLong();
                            //добавить фактор Материал стен итоговый
                            koUnit.AddKOFactor(id_factor, null, xmlCodeName.GetNames(current.Walls));
                        }
                    }
                    #endregion

                    #region Заполнение фактора Год постройки
                    ObjectModel.KO.OMFactorSettings fsyear = ObjectModel.KO.OMFactorSettings.Where(x => x.Inheritance_Code == ObjectModel.Directory.KO.FactorInheritance.ftYear).SelectAll().ExecuteFirstOrDefault();
                    if (fsyear != null)
                    {
                        if (fsyear.FactorId != null)
                        {
                            long id_factor = fsyear.FactorId.ParseToLong();
                            //добавить фактор Год постройки итоговый
                            koUnit.AddKOFactor(id_factor, null, string.IsNullOrEmpty(current.Years.Year_Used) ? current.Years.Year_Built : current.Years.Year_Used);
                        }
                    }
                    #endregion

                    #region Признаки для формирования заданий ЦОД
                    //SetAttributeValue_Boolean(660, true, gbuObject.Id, idDocument, sDate, otDate, SRDSession.Current.UserID, otDate);
                    //SetAttributeValue_Boolean(661, true, gbuObject.Id, idDocument, sDate, otDate, SRDSession.Current.UserID, otDate);
                    //SetAttributeValue_Boolean(662, true, gbuObject.Id, idDocument, sDate, otDate, SRDSession.Current.UserID, otDate);
                    //SetAttributeValue_Boolean(663, true, gbuObject.Id, idDocument, sDate, otDate, SRDSession.Current.UserID, otDate);
                    #endregion
                }

                ts.Complete();
            }

            lock (locked)
            {
                CountImportBuildings++;
            }
        }

        private static void SaveGknDataBuilding(xmlObjectBuild current, long gbuObjectId, DateTime sDate, DateTime otDate, long idDocument)
        {
	        //Площадь
            SetAttributeValue_Numeric(2, current.Area.ParseToDecimal(), gbuObjectId, idDocument, sDate, otDate, SRDSession.Current.UserID, otDate);
            //Дата образования
            SetAttributeValue_Date(13, (current.DateCreate == DateTime.MinValue) ? (DateTime?)null : current.DateCreate, gbuObjectId, idDocument, sDate, otDate, SRDSession.Current.UserID, otDate);
            //Назначение здания
            if (current.AssignationBuilding != null) SetAttributeValue_String(14, current.AssignationBuilding.Name, gbuObjectId, idDocument, sDate, otDate, SRDSession.Current.UserID, otDate);
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
            //Материал стен
            SetAttributeValue_String(21, xmlCodeName.GetNames(current.Walls), gbuObjectId, idDocument, sDate, otDate, SRDSession.Current.UserID, otDate);
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
        }

        private static ObjectModel.KO.OMUnit SaveUnitBuilding(xmlObjectBuild current, long gbuObjectId, DateTime unitDate, long idTour, long idTask, KoUnitStatus unitStatus, KoStatusRepeatCalc calcStatus)
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
                    PropertyType_Code = PropertyTypes.Building,
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

        public void ImportObjectParcel(xmlObjectParcel current, DateTime unitDate, long idTour, long idTask, KoNoteType koNoteType, DateTime sDate, DateTime otDate, long idDocument)
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

                        var changedProperties = new UnitChangedProperties
                        {
	                        IsNameChanged = !prNameObjectCheck,
	                        IsTypeOfUserByDocumentsChanged = !prAssignationObjectCheck,
	                        IsSquareChanged = !squareDidNotChange,
	                        //IsZuNumberChanged = !zuNumberDidNotChange,
	                        IsAddressChanged = !addressDidNotChange,
	                        IsCadasrtalQuartalChanged = !cadastralQuartalDidNotChange,
	                        IsLocationChanged = !locationDidNotChange
                        };
                        CalculateUnitUpdateStatus(changedProperties, koUnit);

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
                    
                    SetNewUnitUpdateStatus(koUnit);
                    SaveHistoryForNewObject(koUnit);
                    #endregion
                }

                ts.Complete();
            }

            lock (locked)
            {
                CountImportParcels++;
            }
        }
        private static void SaveGknDataParcel(xmlObjectParcel current, long gbuObjectId, DateTime sDate, DateTime otDate, long idDocument)
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


        public void ImportObjectConstruction(xmlObjectConstruction current, DateTime unitDate, long idTour, long idTask, KoNoteType koNoteType, DateTime sDate, DateTime otDate, long idDocument)
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

                        //TODO CIPJSKO-535 площадь и характеристика имеют одинаковый Id, ждем ответа от аналитика - не ошибка ли это
                        var changedProperties = new UnitChangedProperties
                        {
	                        IsNameChanged = !prNameObjectCheck,
                            IsPurposeOksChanged = !prAssignationObjectCheck,
	                        IsSquareChanged = !squareDidNotChange,
                            IsBuildYearChanged = !prYearBuiltObjectCheck,
                            IsCommissioningYearChanged = !prYearUsedObjectCheck,
                            IsFloorsCountChanged = !floorsCountDidNotChange,
                            IsUndergroundFloorsCountChanged = !undergroundFloorsCountDidNotChange,
                            IsWallMaterialChanged = !wallMaterialDidNotChange,
                            IsZuNumberChanged = !zuNumberDidNotChange,
	                        IsAddressChanged = !addressDidNotChange,
	                        IsCadasrtalQuartalChanged = !cadastralQuartalDidNotChange,
	                        IsLocationChanged = !locationDidNotChange,
                            IsCharacteristicChanged = !squareDidNotChange
                        };
                        CalculateUnitUpdateStatus(changedProperties, koUnit);

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
                                    koUnit.AddKOFactor(id_factor, (prYearUsedObjectCheck && prYearBuiltObjectCheck) ? lastUnit : null, string.IsNullOrEmpty(current.Years.Year_Used) ? current.Years.Year_Built : current.Years.Year_Used);
                                }
                            }
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

                    SetNewUnitUpdateStatus(koUnit);
                    SaveHistoryForNewObject(koUnit);
                    #endregion

                    #region Заполнение фактора Год постройки
                    ObjectModel.KO.OMFactorSettings fsyear = ObjectModel.KO.OMFactorSettings.Where(x => x.Inheritance_Code == ObjectModel.Directory.KO.FactorInheritance.ftYear).SelectAll().ExecuteFirstOrDefault();
                    if (fsyear != null)
                    {
                        if (fsyear.FactorId != null)
                        {
                            long id_factor = fsyear.FactorId.ParseToLong();
                            //Если в предыдущем объекте есть фактор Год постройки итоговый
                            //его надо скопировать в новый объект, если нет, добавить надо.
                            koUnit.AddKOFactor(id_factor, null, string.IsNullOrEmpty(current.Years.Year_Used) ? current.Years.Year_Built : current.Years.Year_Used);
                        }
                    }
                    #endregion
                }

                ts.Complete();
            }

            lock (locked)
            {
                CountImportConstructions++;
            }

        }
        private static void SaveGknDataConstruction(xmlObjectConstruction current, long gbuObjectId, DateTime sDate, DateTime otDate, long idDocument)
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


        public void ImportObjectUncomplited(xmlObjectUncomplited current, DateTime unitDate, long idTour, long idTask, KoNoteType koNoteType, DateTime sDate, DateTime otDate, long idDocument)
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
                            ObjectType_Code = PropertyTypes.UncompletedBuilding,
                        };
                        gbuObject.Save();
                    }
                    else
                    {
                        if (gbuObject.ObjectType_Code != PropertyTypes.UncompletedBuilding)
                        {
                            gbuObject.ObjectType_Code = PropertyTypes.UncompletedBuilding;
                            gbuObject.Save();
                        }
                    }
                    #endregion

                    //Сохранение данных ГКН
                    SaveGknDataUncomplited(current, gbuObject.Id, sDate, otDate, idDocument);
                    //Задание на оценку
                    ObjectModel.KO.OMUnit koUnit = SaveUnitUncomplited(current, gbuObject.Id, unitDate, idTour, idTask, koUnitStatus, GetNewtatusRepeatCalc(prev));
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
                        46,  //Процент готовности 
                        44,  //Площадь 
                        19,  //Наименование
                        8,   //Местоположение 
                        600, //Адрес 
                        601, //Кадастровый квартал 
                        602  //Земельный участок 
                    };

                        List<GbuObjectAttribute> prevAttrib = new GbuObjectService().GetAllAttributes(koUnit.ObjectId.Value, sourceIds, attribIds, lastUnit.CreationDate);
                        List<GbuObjectAttribute> curAttrib = new GbuObjectService().GetAllAttributes(koUnit.ObjectId.Value, sourceIds, attribIds, koUnit.CreationDate);
                        var readinessPercentageDidNotChange = CheckChange(koUnit, 46, KoChangeStatus.Procent, prevAttrib, curAttrib);
                        var squareDidNotChange = CheckChange(koUnit, 44, KoChangeStatus.Square, prevAttrib, curAttrib);
                        var nameDidNotChange = CheckChange(koUnit, 19, KoChangeStatus.Name, prevAttrib, curAttrib);
                        var locationDidNotChange = CheckChange(koUnit, 8, KoChangeStatus.Place, prevAttrib, curAttrib);
                        var addressDidNotChange = CheckChange(koUnit, 600, KoChangeStatus.Adress, prevAttrib, curAttrib);
                        var cadastralQuartalDidNotChange = CheckChange(koUnit, 601, KoChangeStatus.CadastralBlock, prevAttrib, curAttrib);
                        var zuNumberDidNotChange = CheckChange(koUnit, 602, KoChangeStatus.NumberParcel, prevAttrib, curAttrib);

                        //TODO CIPJSKO-535 площадь и характеристика имеют одинаковый Id, ждем ответа от аналитика - не ошибка ли это
                        //параметры закомментированы по согласованию с аналитиком
                        var changedProperties = new UnitChangedProperties
                        {
	                        IsNameChanged = !nameDidNotChange,
	                        //IsPurposeOksChanged = !prAssignationObjectCheck,
	                        IsSquareChanged = !squareDidNotChange,
	                        //IsBuildYearChanged = !prYearBuiltObjectCheck,
	                        //IsCommissioningYearChanged = !prYearUsedObjectCheck,
	                        //IsFloorsCountChanged = !floorsCountDidNotChange,
	                        //IsUndergroundFloorsCountChanged = !undergroundFloorsCountDidNotChange,
	                        //IsWallMaterialChanged = !wallMaterialDidNotChange,
	                        IsZuNumberChanged = !zuNumberDidNotChange,
	                        IsAddressChanged = !addressDidNotChange,
	                        IsCadasrtalQuartalChanged = !cadastralQuartalDidNotChange,
	                        IsLocationChanged = !locationDidNotChange,
	                        IsReadinessPercentageChanged = !readinessPercentageDidNotChange
                        };
                        CalculateUnitUpdateStatus(changedProperties, koUnit);
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
                            ObjectType_Code = PropertyTypes.UncompletedBuilding,
                        };
                        gbuObject.Save();
                    }
                    else
                    {
                        if (gbuObject.ObjectType_Code != PropertyTypes.UncompletedBuilding)
                        {
                            gbuObject.ObjectType_Code = PropertyTypes.UncompletedBuilding;
                            gbuObject.Save();
                        }
                    }
                    #endregion

                    //Сохранение данных ГКН
                    SaveGknDataUncomplited(current, gbuObject.Id, sDate, otDate, idDocument);

                    //Задание на оценку
                    ObjectModel.KO.OMUnit koUnit = SaveUnitUncomplited(current, gbuObject.Id, unitDate, idTour, idTask, koUnitStatus, koStatusRepeatCalc);

                    SetNewUnitUpdateStatus(koUnit);
                    SaveHistoryForNewObject(koUnit);
                    #endregion
                }

                ts.Complete();
            }

            lock (locked)
            {
                CountImportUncompliteds++;
            }

        }

        private static void SaveGknDataUncomplited(xmlObjectUncomplited current, long gbuObjectId, DateTime sDate, DateTime otDate, long idDocument)
        {
	        //Процент готовности
            SetAttributeValue_Numeric(46, (current.DegreeReadiness == string.Empty || current.DegreeReadiness == null) ? (decimal?)null : current.DegreeReadiness.ParseToDecimal(), gbuObjectId, idDocument, sDate, otDate, SRDSession.Current.UserID, otDate);
            //Площадь
            SetAttributeValue_String(44, xmlCodeNameValue.GetNames(current.KeyParameters), gbuObjectId, idDocument, sDate, otDate, SRDSession.Current.UserID, otDate);
            //Дата образования
            SetAttributeValue_Date(13, (current.DateCreate == DateTime.MinValue) ? (DateTime?)null : current.DateCreate, gbuObjectId, idDocument, sDate, otDate, SRDSession.Current.UserID, otDate);
            //Наименование объекта
            SetAttributeValue_String(19, current.AssignationName, gbuObjectId, idDocument, sDate, otDate, SRDSession.Current.UserID, otDate);

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
        }

        private static ObjectModel.KO.OMUnit SaveUnitUncomplited(xmlObjectUncomplited current, long gbuObjectId, DateTime unitDate, long idTour, long idTask, KoUnitStatus unitStatus, KoStatusRepeatCalc calcStatus)
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
                    PropertyType_Code = PropertyTypes.UncompletedBuilding,
                    StatusRepeatCalc_Code = calcStatus,
                    StatusResultCalc_Code = KoStatusResultCalc.None,
                    CadastralCost = 0,
                    CadastralCostPre = 0,
                    Upks = 0,
                    UpksPre = 0,
                };
                if (!string.IsNullOrEmpty(current.DegreeReadiness))
                    koUnit.DegreeReadiness = current.DegreeReadiness.ParseToLong();
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

        public void ImportObjectFlat(xmlObjectFlat current, DateTime unitDate, long idTour, long idTask, KoNoteType koNoteType, DateTime sDate, DateTime otDate, long idDocument)
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
                        CheckChange(koUnit, 14, KoChangeStatus.Use, prevAttrib, curAttrib);
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

                        var changedProperties = new UnitChangedProperties
                        {
                            IsNameChanged = !nameDidNotChange,
                            IsPurposeOksChanged = !purposeOksDidNotChange,
                            IsSquareChanged = !squareDidNotChange,
                            IsBuildYearChanged = !buildYearDidNotChange,
                            IsCommissioningYearChanged = !commissioningYearDidNotChange,
                            IsFloorsCountChanged = !floorsCountDidNotChange,
                            IsUndergroundFloorsCountChanged = !undergroundFloorsCountDidNotChange,
                            IsWallMaterialChanged = !wallMaterialDidNotChange,
                            IsAddressChanged = !addressDidNotChange,
                            IsCadasrtalQuartalChanged = !cadastralQuartalDidNotChange,
                            IsLocationChanged = !locationDidNotChange
                        };
                        CalculateUnitUpdateStatus(changedProperties, koUnit);

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

                    SetNewUnitUpdateStatus(koUnit);
                    SaveHistoryForNewObject(koUnit);
                    #endregion
                }

                ts.Complete();
            }

            lock (locked)
            {
                CountImportFlats++;
            }
        }

        private static void SaveGknDataFlat(xmlObjectFlat current, long gbuObjectId, DateTime sDate, DateTime otDate, long idDocument)
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
                    BuildingCadastralNumber=current.CadastralNumberOKS
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


        public void ImportObjectCarPlace(xmlObjectCarPlace current, DateTime unitDate, long idTour, long idTask, KoNoteType koNoteType, DateTime sDate, DateTime otDate, long idDocument)
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
                            ObjectType_Code = PropertyTypes.Parking,
                        };
                        gbuObject.Save();
                    }
                    else
                    {
                        if (gbuObject.ObjectType_Code != PropertyTypes.Parking)
                        {
                            gbuObject.ObjectType_Code = PropertyTypes.Parking;
                            gbuObject.Save();
                        }
                    }
                    #endregion

                    //Сохранение данных ГКН
                    SaveGknDataCarPlace(current, gbuObject.Id, sDate, otDate, idDocument);
                    //Задание на оценку
                    ObjectModel.KO.OMUnit koUnit = SaveUnitCarPlace(current, gbuObject.Id, unitDate, idTour, idTask, koUnitStatus, GetNewtatusRepeatCalc(prev));
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
                        var nameDidNotChange = CheckChange(koUnit, 19, KoChangeStatus.Name, prevAttrib, curAttrib);
                        var purposeOksDidNotChange = CheckChange(koUnit, 14, KoChangeStatus.Use, prevAttrib, curAttrib);
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

                        var changedProperties = new UnitChangedProperties
                        {
	                        IsNameChanged = !nameDidNotChange,
	                        IsPurposeOksChanged = !purposeOksDidNotChange,
	                        IsSquareChanged = !squareDidNotChange,
	                        IsBuildYearChanged = !buildYearDidNotChange,
	                        IsCommissioningYearChanged = !commissioningYearDidNotChange,
	                        IsFloorsCountChanged = !floorsCountDidNotChange,
	                        IsUndergroundFloorsCountChanged = !undergroundFloorsCountDidNotChange,
	                        IsWallMaterialChanged = !wallMaterialDidNotChange,
	                        IsAddressChanged = !addressDidNotChange,
	                        IsCadasrtalQuartalChanged = !cadastralQuartalDidNotChange,
	                        IsLocationChanged = !locationDidNotChange
                        };
                        CalculateUnitUpdateStatus(changedProperties, koUnit);

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
                            ObjectType_Code = PropertyTypes.Parking,
                        };
                        gbuObject.Save();
                    }
                    else
                    {
                        if (gbuObject.ObjectType_Code != PropertyTypes.Parking)
                        {
                            gbuObject.ObjectType_Code = PropertyTypes.Parking;
                            gbuObject.Save();
                        }
                    }
                    #endregion

                    //Сохранение данных ГКН
                    SaveGknDataCarPlace(current, gbuObject.Id, sDate, otDate, idDocument);

                    //Задание на оценку
                    ObjectModel.KO.OMUnit koUnit = SaveUnitCarPlace(current, gbuObject.Id, unitDate, idTour, idTask, koUnitStatus, koStatusRepeatCalc);

                    SetNewUnitUpdateStatus(koUnit);
                    SaveHistoryForNewObject(koUnit);
                    #endregion
                }

                ts.Complete();
            }

            lock (locked)
            {
                CountImportCarPlaces++;
            }
        }

        private static void SaveGknDataCarPlace(xmlObjectCarPlace current, long gbuObjectId, DateTime sDate, DateTime otDate, long idDocument)
        {
	        //Площадь
            SetAttributeValue_Numeric(2, current.Area.ParseToDecimal(), gbuObjectId, idDocument, sDate, otDate, SRDSession.Current.UserID, otDate);
            //Дата образования
            SetAttributeValue_Date(13, (current.DateCreate == DateTime.MinValue) ? (DateTime?)null : current.DateCreate, gbuObjectId, idDocument, sDate, otDate, SRDSession.Current.UserID, otDate);
            //Назначение помещения
            //if (current. AssignationFlatCode != null)
            //    SetAttributeValue_String(23, current.AssignationFlatCode.Name, gbuObjectId, idDocument, sDate, otDate, SRDSession.Current.UserID, otDate);
            //Наименование объекта
            SetAttributeValue_String(19, current.TypeRealty, gbuObjectId, idDocument, sDate, otDate, SRDSession.Current.UserID, otDate);

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

            ////Тип помещения
            //if (current.AssignationFlatType != null) SetAttributeValue_String(603, current.AssignationFlatType.Name, gbuObjectId, idDocument, sDate, otDate, SRDSession.Current.UserID, otDate);
            //Кадастровый номер здания или сооружения, в котором расположено помещение
            SetAttributeValue_String(604, current.CadastralNumberOKS, gbuObjectId, idDocument, sDate, otDate, SRDSession.Current.UserID, otDate);
            //Кадастровый номер квартиры, в которой расположена комната
            //SetAttributeValue_String(605, current.CadastralNumberFlat, gbuObjectId, idDocument, sDate, otDate, SRDSession.Current.UserID, otDate);

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

        private static ObjectModel.KO.OMUnit SaveUnitCarPlace(xmlObjectCarPlace current, long gbuObjectId, DateTime unitDate, long idTour, long idTask, KoUnitStatus unitStatus, KoStatusRepeatCalc calcStatus)
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
                    PropertyType_Code = PropertyTypes.Parking,
                    StatusRepeatCalc_Code = calcStatus,
                    StatusResultCalc_Code = KoStatusResultCalc.None,
                    CadastralCost = 0,
                    CadastralCostPre = 0,
                    Upks = 0,
                    UpksPre = 0,
                    BuildingCadastralNumber=current.CadastralNumberOKS
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

        private void CalculateUnitUpdateStatus(UnitChangedProperties changedProperties, OMUnit unit)
        {
	        var unitUpdateStatus = UnitStatusHandler.Handle(changedProperties);
	        unit.UpdateStatus_Code = unitUpdateStatus;
	        unit.Save();
        }

        private void SetNewUnitUpdateStatus(OMUnit unit)
        {
	        unit.UpdateStatus_Code = UnitUpdateStatus.New;
	        unit.Save();
        }

        public static void SaveHistoryForNewObject(OMUnit unit)
        {
	        new OMUnitChange
	        {
		        Id = -1,
		        ChangeStatus_Code = KoChangeStatus.NewObjectAddition,
		        UnitId = unit.Id
	        }.Save();
        }
    }
}
