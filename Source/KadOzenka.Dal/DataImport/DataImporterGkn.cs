using Core.ErrorManagment;
using Core.Main.FileStorages;
using Core.Shared.Extensions;
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

namespace KadOzenka.Dal.DataImport
{
    public static class DataImporterGkn
    {
              //"4"     "Вид использования по документам"
              //"5"     "Вид использования по классификатору"
              //"16"    "Год ввода в эксплуатацию"
              //"15"    "Год постройки"
              //"13"    "Дата образования"
              //"43"    "Дата прекращения"
              //"6"     "Кадастровая стоимость"
              //"3"     "Категория земель"
              //"18"    "Количество подземных этажей"
              //"17"    "Количество этажей"
              //"21"    "Материал стен"
              //"8"     "Местоположение"
              //"600"   "Адрес"
              //"14"    "Назначение здания"
              //"23"    "Назначение помещения"
              //"22"    "Назначение сооружения"
              //"1"     "Наименование земельного участка"
              //"19"    "Наименование объекта"
              //"24"    "Номер этажа"
              //"2"     "Площадь"
              //"46"    "Процент готовности"
              //"26"    "Тип объекта"
              //"25"    "Тип этажа"
              //"44"    "Характеристика сооружения"
              //"45"    "Этаж"



        //ID фактора Материал стен итоговый
        public static Int64 Id_Factor_Wall = -1;
        //ID фактора Год постройки итоговый
        public static Int64 Id_Factor_Year = -1;


        /// <summary>
        /// Импорт данных ГКН из Xml
        /// xmlFile - файл xml
        /// pathSchema - путь к каталогу где хранится схема
        /// task - ссылка на задание на оценку
        /// </summary>
        public static void ImportDataGknFromXml(Stream xmlFile, string pathSchema, ObjectModel.KO.OMTask task)
        {
            ImportDataGknFromXml(xmlFile, pathSchema, task.CreationDate.Value, task.TourId.Value, task.Id, task.CreationDate.Value, task.CreationDate.Value, task.DocumentId.Value);
        }


        private static void ImportDataGknFromXml(Stream xmlFile, string pathSchema, DateTime unitDate, long idTour, long idTask, DateTime sDate, DateTime otDate, long idDocument)
        {
            xmlImportGkn.FillDictionary(pathSchema);
            xmlObjectList GknItems = xmlImportGkn.GetXmlObject(xmlFile);

            CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
            ParallelOptions options = new ParallelOptions
            {
                CancellationToken = cancelTokenSource.Token,
                MaxDegreeOfParallelism = 10
            };

            Parallel.ForEach(GknItems.Buildings, options, item => ImportObjectBuild(item, unitDate, idTour, idTask, sDate, otDate, idDocument));
            Parallel.ForEach(GknItems.Parcels, options, item => ImportObjectParcel(item, unitDate, idTour, idTask, sDate, otDate, idDocument));
            Parallel.ForEach(GknItems.Constructions, options, item => ImportObjectConstruction(item, unitDate, idTour, idTask, sDate, otDate, idDocument));
            Parallel.ForEach(GknItems.Uncompliteds, options, item => ImportObjectUncomplited(item, unitDate, idTour, idTask, sDate, otDate, idDocument));

        }
        private static void SetAttributeValue_String(long idAttribute, string value, long idObject, long idDocument, DateTime sDate, DateTime otDate, long idUser, DateTime changeDate)
        {
            var attributeValue = new GbuObjectAttribute
            {
                Id = -1,
                AttributeId = idAttribute,
                ObjectId = idObject,
                ChangeDocId = idDocument,
                S = sDate,
                ChangeUserId = idUser,
                ChangeDate = changeDate,
                Ot= otDate,
                StringValue=value,
            };
            attributeValue.Save();
        }
        private static void SetAttributeValue_Numeric(long idAttribute, decimal? value, long idObject, long idDocument, DateTime sDate, DateTime otDate, long idUser, DateTime changeDate)
        {
            var attributeValue = new GbuObjectAttribute
            {
                Id = -1,
                AttributeId = idAttribute,
                ObjectId = idObject,
                ChangeDocId = idDocument,
                S = sDate,
                ChangeUserId = idUser,
                ChangeDate = changeDate,
                Ot = otDate,
                NumValue = value,
            };
            attributeValue.Save();
        }
        private static void SetAttributeValue_Date(long idAttribute, DateTime? value, long idObject, long idDocument, DateTime sDate, DateTime otDate, long idUser, DateTime changeDate)
        {
            var attributeValue = new GbuObjectAttribute
            {
                Id = -1,
                AttributeId = idAttribute,
                ObjectId = idObject,
                ChangeDocId = idDocument,
                S = sDate,
                ChangeUserId = idUser,
                ChangeDate = changeDate,
                Ot = otDate,
                DtValue = value,
            };
            attributeValue.Save();
        }
        public static void ImportObjectBuild(xmlObjectBuild current, DateTime unitDate, long idTour, long idTask, DateTime sDate, DateTime otDate, long idDocument)
        {
            xmlObjectBuild prev = null;

            #region Получение данных о прошлой оценке данного объекта
            //TODO: prev=????????
            // 
            //
            //
            //
            #endregion

            //Если данные о прошлой оценке найдены
            if (prev != null)
            {
                #region Импорт нового объекта
                // TODO: Импорт нового объекта
                //
                //
                //
                //
                #endregion

                //Признак было ли по данному объекту обращение?
                bool prCheckObr = false;
                //TODO: получить историю по объекту и узнать был ли он получен в рамках обращения (по типу входящего документа)
                //надо перебрать все документы и узнать это
                // CheckObr = ????
                //
                //
                //

                //Признак не поменялся ли тип объекта?
                bool prTypeObjectCheck = prev.TypeObject == current.TypeObject;
                //Признак не поменялось ли наименование объекта
                bool prNameObjectCheck = prev.Name == current.Name;
                //Признак не поменялось ли назначение объекта
                bool prAssignationObjectCheck = (prev.AssignationBuilding != null && current.AssignationBuilding != null) ? (prev.AssignationBuilding.Code == current.AssignationBuilding.Code) : false;

                //Если не было изменений типа, наименования и назначения и не было обращения
                if (prTypeObjectCheck && prNameObjectCheck && prAssignationObjectCheck && !prCheckObr)
                {
                    #region Наследование группы и подгруппы предыдущего объекта
                    // TODO: Пронаследовать группу и подгруппу предыдущего объекта
                    // если статус предыдущего расчета не ошибочный
                    //
                    //
                    //
                    #endregion
                }

                //Признак не поменялся ли материал стен?
                bool prWallObjectCheck = ObjectCheckItem.Check(prev.Walls, current.Walls);
                //Признак не поменялся ли год ввода в эксплуатацию?
                bool prYearUsedObjectCheck = ObjectCheckItem.Check(prev.Years.Year_Used, current.Years.Year_Used);
                //Признак не поменялся ли год завершения строительства?
                bool prYearBuiltObjectCheck = ObjectCheckItem.Check(prev.Years.Year_Built, current.Years.Year_Built);



                //Если не было обращения по объекту
                if (!prCheckObr)
                {
                    //Если материал стен не поменялся
                    if (prWallObjectCheck)
                    {
                        if (Id_Factor_Wall > 0)
                        {
                            //TODO: Если в предыдущем объекте есть фактор Материал стен итоговый
                            //      его надо скопировать в новый объект, если нет, добавить надо.
                            //
                            //
                        }
                    }
                    else
                    {
                        if (Id_Factor_Wall > 0)
                        {
                            //TODO: добавить фактор Материал стен итоговый
                            //      
                            //
                            //
                        }
                    }

                    //Если год ввода в эксплуатацию и год завершения строительства  не поменялся
                    if (prYearUsedObjectCheck && prYearBuiltObjectCheck)
                    {
                        if (Id_Factor_Wall > 0)
                        {
                            //TODO: Если в предыдущем объекте есть фактор Год постройки итоговый
                            //      его надо скопировать в новый объект, если нет, надо добавить
                            //      в соответствии с приоритетом 
                            //      1.Год ввода в эксплуатацию
                            //      2.Год завершения строительства
                        }
                    }
                    else
                    {
                        if (Id_Factor_Wall > 0)
                        {
                            //TODO: добавить фактор Год постройки итоговый
                            //      в соответствии с приоритетом 
                            //      1.Год ввода в эксплуатацию
                            //      2.Год завершения строительства
                        }
                    }
                }
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

                #region Сохранение данных ГКН
                //Площадь
                SetAttributeValue_Numeric(2, Convert.ToDecimal(current.Area), gbuObject.Id, idDocument, sDate, otDate, SRDSession.Current.UserID, otDate);
                //Дата образования
                SetAttributeValue_Date(13, (current.DateCreate == DateTime.MinValue) ? (DateTime?)null : current.DateCreate, gbuObject.Id, idDocument, sDate, otDate, SRDSession.Current.UserID, otDate);
                //Назначение здания
                if (current.AssignationBuilding != null) SetAttributeValue_String(14, current.AssignationBuilding.Name, gbuObject.Id, idDocument, sDate, otDate, SRDSession.Current.UserID, otDate);
                //Наименование объекта
                SetAttributeValue_String(19, current.Name, gbuObject.Id, idDocument, sDate, otDate, SRDSession.Current.UserID, otDate);

                if (current.Floors != null)
                {
                    //Количество этажей
                    SetAttributeValue_String(17, current.Floors.Floors, gbuObject.Id, idDocument, sDate, otDate, SRDSession.Current.UserID, otDate);
                    //Количество подземных этажей
                    SetAttributeValue_String(18, current.Floors.Underground_Floors, gbuObject.Id, idDocument, sDate, otDate, SRDSession.Current.UserID, otDate);
                }
                if (current.Years != null)
                {
                    //Год ввода в эксплуатацию
                    SetAttributeValue_String(16, current.Years.Year_Used, gbuObject.Id, idDocument, sDate, otDate, SRDSession.Current.UserID, otDate);
                    //Год постройки
                    SetAttributeValue_String(15, current.Years.Year_Built, gbuObject.Id, idDocument, sDate, otDate, SRDSession.Current.UserID, otDate);
                }
                //Тип объекта
                SetAttributeValue_String(26, current.TypeRealty, gbuObject.Id, idDocument, sDate, otDate, SRDSession.Current.UserID, otDate);
                //Материал стен
                SetAttributeValue_String(21, xmlCodeName.GetNames(current.Walls), gbuObject.Id, idDocument, sDate, otDate, SRDSession.Current.UserID, otDate);
                //Местоположение
                SetAttributeValue_String(8, xmlAdress.GetTextPlace(current.Adress), gbuObject.Id, idDocument, sDate, otDate, SRDSession.Current.UserID, otDate);
                //Адрес
                SetAttributeValue_String(600, xmlAdress.GetTextAdress(current.Adress), gbuObject.Id, idDocument, sDate, otDate, SRDSession.Current.UserID, otDate);
                //Кадастровая стоимость
                if (current.CadastralCost != null) SetAttributeValue_Numeric(6, Convert.ToDecimal(current.CadastralCost.Value), gbuObject.Id, idDocument, sDate, otDate, SRDSession.Current.UserID, otDate);
                //Кадастровый квартал
                SetAttributeValue_String(601, current.CadastralNumberBlock, gbuObject.Id, idDocument, sDate, otDate, SRDSession.Current.UserID, otDate);
                //Земельный участок
                SetAttributeValue_String(602, xmlCodeName.GetNames(current.ParentCadastralNumbers), gbuObject.Id, idDocument, sDate, otDate, SRDSession.Current.UserID, otDate);
                #endregion

                #region Задание на оценку
                ObjectModel.KO.OMUnit koUnit = ObjectModel.KO.OMUnit.Where(x => x.ObjectId == gbuObject.Id && x.TaskId == idTask && x.TourId == idTour).SelectAll().ExecuteFirstOrDefault();
                if (koUnit == null)
                {
                    koUnit = new ObjectModel.KO.OMUnit
                    {
                        Id = -1,
                        ModelId = -1,
                        TourId = idTour,
                        TaskId = idTask,
                        GroupId = -1,
                        Status_Code = KoUnitStatus.Initial,
                        ObjectId = gbuObject.Id,
                        CreationDate = unitDate,
                        CadastralNumber = current.CadastralNumber,
                        CadastralBlock = current.CadastralNumberBlock,
                        Square = current.Area.ParseToDecimal(),
                        PropertyType_Code = PropertyTypes.Building,
                        StatusRepeatCalc_Code = KoStatusRepeatCalc.Initial,
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
                    ObjectModel.KO.OMCostRosreestr cost = new ObjectModel.KO.OMCostRosreestr
                    {
                        Id=-1,
                        Applicationdate = (current.CadastralCost.ApplicationDate == DateTime.MinValue) ? (DateTime?)null : current.CadastralCost.ApplicationDate,
                        Dateapproval = (current.CadastralCost.DateApproval == DateTime.MinValue) ? (DateTime?)null : current.CadastralCost.DateApproval,
                        Dateentering = (current.CadastralCost.DateEntering == DateTime.MinValue) ? (DateTime?)null : current.CadastralCost.DateEntering,
                        Datevaluation = (current.CadastralCost.DateValuation == DateTime.MinValue) ? (DateTime?)null : current.CadastralCost.DateValuation,
                        Docdate = (current.CadastralCost.DocDate == DateTime.MinValue) ? (DateTime?)null : current.CadastralCost.DocDate,
                        Docnumber = current.CadastralCost.DocNumber,
                        Docname = current.CadastralCost.DocName,
                        IdObject = koUnit.Id,
                        Costvalue = Convert.ToDecimal(current.CadastralCost.Value),
                        Revisalstatementdate = (current.CadastralCost.RevisalStatementDate == DateTime.MinValue) ? (DateTime?)null : current.CadastralCost.RevisalStatementDate,
                    };
                    cost.Save();
                }
                #endregion
                #endregion

                #region Заполнение фактора Материал стен на основании данных ГКН
                if (Id_Factor_Wall > 0)
                {
                    if (current.Walls.Count > 0)
                    {
                        //TODO: Добавить фактор Материал стен на основании данных ГКН
                        //
                        //
                        //
                    }
                }
                #endregion

                #region Заполнение фактора Год постройки
                if (Id_Factor_Year > 0)
                {
                    if (current.Years != null)
                    {
                        if (current.Years.Year_Used != null && current.Years.Year_Used != string.Empty)
                        {
                            //TODO: Добавить фактор Год постройки на основании данных года ввода в эксплуатацию
                            //
                            //
                            //
                        }
                        else
                        if (current.Years.Year_Built != null && current.Years.Year_Built != string.Empty)
                        {
                            //TODO: Добавить фактор Год постройки на основании данных года завершения строительства
                            //
                            //
                            //
                        }
                    }
                }
                #endregion
            }

        }
        public static void ImportObjectParcel(xmlObjectParcel current, DateTime unitDate, long idTour, long idTask, DateTime sDate, DateTime otDate, long idDocument)
        {
            xmlObjectParcel prev = null;

            #region Получение данных о прошлой оценке данного объекта
            //TODO: prev=????????
            // 
            //
            //
            //
            #endregion

            //Если данные о прошлой оценке найдены
            if (prev != null)
            {
                #region Импорт нового объекта
                // TODO: Импорт нового объекта
                //
                //
                //
                //
                #endregion

                //Признак было ли по данному объекту обращение?
                bool prCheckObr = false;
                //TODO: получить историю по объекту и узнать был ли он получен в рамках обращения (по типу входящего документа)
                //надо перебрать все документы и узнать это
                // CheckObr = ????
                //
                //
                //

                //Признак не поменялся ли тип объекта?
                bool prTypeObjectCheck = prev.TypeObject == current.TypeObject;
                //Признак не поменялось ли наименование объекта
                bool prNameObjectCheck = prev.Name == current.Name;
                //Признак не поменялось ли назначение объекта
                bool prAssignationObjectCheck = (prev.Utilization != null) ? (prev.Utilization.ByDoc == current.Utilization.ByDoc && prev.Utilization.Utilization.Code == current.Utilization.Utilization.Code) : false;

                //Если не было изменений типа, наименования и назначения и не было обращения
                if (prTypeObjectCheck && prNameObjectCheck && prAssignationObjectCheck && !prCheckObr)
                {
                    #region Наследование группы и подгруппы предыдущего объекта
                    // TODO: Пронаследовать группу и подгруппу предыдущего объекта
                    // если статус предыдущего расчета не ошибочный
                    //
                    //
                    //
                    #endregion
                }

                //Если не было обращения по объекту
                if (!prCheckObr)
                {
                }
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

                #region Сохранение данных ГКН
                //Наименование участка
                SetAttributeValue_String(1, current.Name.Name, gbuObject.Id, idDocument, sDate, otDate, SRDSession.Current.UserID, otDate);
                //Площадь
                SetAttributeValue_Numeric(2, Convert.ToDecimal(current.Area), gbuObject.Id, idDocument, sDate, otDate, SRDSession.Current.UserID, otDate);
                //Дата образования
                SetAttributeValue_Date(13, (current.DateCreate == DateTime.MinValue) ? (DateTime?)null : current.DateCreate, gbuObject.Id, idDocument, sDate, otDate, SRDSession.Current.UserID, otDate);
                //Тип объекта
                SetAttributeValue_String(26, current.TypeRealty, gbuObject.Id, idDocument, sDate, otDate, SRDSession.Current.UserID, otDate);
                //Местоположение
                SetAttributeValue_String(8, xmlAdress.GetTextPlace(current.Adress), gbuObject.Id, idDocument, sDate, otDate, SRDSession.Current.UserID, otDate);
                //Адрес
                SetAttributeValue_String(600, xmlAdress.GetTextAdress(current.Adress), gbuObject.Id, idDocument, sDate, otDate, SRDSession.Current.UserID, otDate);
                //Кадастровая стоимость
                if (current.CadastralCost != null) SetAttributeValue_Numeric(6, Convert.ToDecimal(current.CadastralCost.Value), gbuObject.Id, idDocument, sDate, otDate, SRDSession.Current.UserID, otDate);
                //Кадастровый квартал
                SetAttributeValue_String(601, current.CadastralNumberBlock, gbuObject.Id, idDocument, sDate, otDate, SRDSession.Current.UserID, otDate);
                //Категория земель
                SetAttributeValue_String(3, current.Category.Name, gbuObject.Id, idDocument, sDate, otDate, SRDSession.Current.UserID, otDate);
                //Вид использования по документам
                SetAttributeValue_String(4, current.Utilization.ByDoc, gbuObject.Id, idDocument, sDate, otDate, SRDSession.Current.UserID, otDate);
                //Вид использования по классификатору
                SetAttributeValue_String(5, current.Utilization.Utilization.Name, gbuObject.Id, idDocument, sDate, otDate, SRDSession.Current.UserID, otDate);
                #endregion

                #region Задание на оценку
                ObjectModel.KO.OMUnit koUnit = ObjectModel.KO.OMUnit.Where(x => x.ObjectId == gbuObject.Id && x.TaskId == idTask && x.TourId == idTour).SelectAll().ExecuteFirstOrDefault();
                if (koUnit == null)
                {
                    koUnit = new ObjectModel.KO.OMUnit
                    {
                        Id = -1,
                        ModelId = -1,
                        TourId = idTour,
                        TaskId = idTask,
                        GroupId = -1,
                        Status_Code = KoUnitStatus.Initial,
                        ObjectId = gbuObject.Id,
                        CreationDate = unitDate,
                        CadastralNumber = current.CadastralNumber,
                        CadastralBlock = current.CadastralNumberBlock,
                        Square=current.Area.ParseToDecimal(),
                        PropertyType_Code = PropertyTypes.Stead,
                        StatusRepeatCalc_Code = KoStatusRepeatCalc.Initial,
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
                    ObjectModel.KO.OMCostRosreestr cost = new ObjectModel.KO.OMCostRosreestr
                    {
                        Id=-1,
                        Applicationdate = (current.CadastralCost.ApplicationDate == DateTime.MinValue) ? (DateTime?)null : current.CadastralCost.ApplicationDate,
                        Dateapproval = (current.CadastralCost.DateApproval == DateTime.MinValue) ? (DateTime?)null : current.CadastralCost.DateApproval,
                        Dateentering = (current.CadastralCost.DateEntering == DateTime.MinValue) ? (DateTime?)null : current.CadastralCost.DateEntering,
                        Datevaluation = (current.CadastralCost.DateValuation == DateTime.MinValue) ? (DateTime?)null : current.CadastralCost.DateValuation,
                        Docdate = (current.CadastralCost.DocDate == DateTime.MinValue) ? (DateTime?)null : current.CadastralCost.DocDate,
                        Docnumber = current.CadastralCost.DocNumber,
                        Docname = current.CadastralCost.DocName,
                        IdObject = koUnit.Id,
                        Costvalue = Convert.ToDecimal(current.CadastralCost.Value),
                        Revisalstatementdate = (current.CadastralCost.RevisalStatementDate == DateTime.MinValue) ? (DateTime?)null : current.CadastralCost.RevisalStatementDate,
                    };
                    cost.Save();
                }
                #endregion
                #endregion

            }

        }
        public static void ImportObjectConstruction(xmlObjectConstruction current, DateTime unitDate, long idTour, long idTask, DateTime sDate, DateTime otDate, long idDocument)
        {
            xmlObjectConstruction prev = null;

            #region Получение данных о прошлой оценке данного объекта
            //TODO: prev=????????
            // 
            //
            //
            //
            #endregion

            //Если данные о прошлой оценке найдены
            if (prev != null)
            {
                #region Импорт нового объекта
                // TODO: Импорт нового объекта
                //
                //
                //
                //
                #endregion

                //Признак было ли по данному объекту обращение?
                bool prCheckObr = false;
                //TODO: получить историю по объекту и узнать был ли он получен в рамках обращения (по типу входящего документа)
                //надо перебрать все документы и узнать это
                // CheckObr = ????
                //
                //
                //

                //Признак не поменялся ли тип объекта?
                bool prTypeObjectCheck = prev.TypeObject == current.TypeObject;
                //Признак не поменялось ли наименование объекта
                bool prNameObjectCheck = prev.Name == current.Name;
                //Признак не поменялось ли назначение объекта
                bool prAssignationObjectCheck = (prev.AssignationName != null && current.AssignationName != null) ? (prev.AssignationName == current.AssignationName) : false;

                //Если не было изменений типа, наименования и назначения и не было обращения
                if (prTypeObjectCheck && prNameObjectCheck && prAssignationObjectCheck && !prCheckObr)
                {
                    #region Наследование группы и подгруппы предыдущего объекта
                    // TODO: Пронаследовать группу и подгруппу предыдущего объекта
                    // если статус предыдущего расчета не ошибочный
                    //
                    //
                    //
                    #endregion
                }

                //Признак не поменялся ли год ввода в эксплуатацию?
                bool prYearUsedObjectCheck = ObjectCheckItem.Check(prev.Years.Year_Used, current.Years.Year_Used);
                //Признак не поменялся ли год завершения строительства?
                bool prYearBuiltObjectCheck = ObjectCheckItem.Check(prev.Years.Year_Built, current.Years.Year_Built);



                //Если не было обращения по объекту
                if (!prCheckObr)
                {
                    //Если год ввода в эксплуатацию и год завершения строительства  не поменялся
                    if (prYearUsedObjectCheck && prYearBuiltObjectCheck)
                    {
                        if (Id_Factor_Year > 0)
                        {
                            //TODO: Если в предыдущем объекте есть фактор Год постройки итоговый
                            //      его надо скопировать в новый объект, если нет, надо добавить
                            //      в соответствии с приоритетом 
                            //      1.Год ввода в эксплуатацию
                            //      2.Год завершения строительства
                        }
                    }
                    else
                    {
                        if (Id_Factor_Year > 0)
                        {
                            //TODO: добавить фактор Год постройки итоговый
                            //      в соответствии с приоритетом 
                            //      1.Год ввода в эксплуатацию
                            //      2.Год завершения строительства
                        }
                    }
                }
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

                #region Сохранение данных ГКН
                //Площадь
                SetAttributeValue_String(44, xmlCodeNameValue.GetNames(current.KeyParameters), gbuObject.Id, idDocument, sDate, otDate, SRDSession.Current.UserID, otDate);
                //Дата образования
                SetAttributeValue_Date(13, (current.DateCreate == DateTime.MinValue) ? (DateTime?)null : current.DateCreate, gbuObject.Id, idDocument, sDate, otDate, SRDSession.Current.UserID, otDate);
                //Назначение сооружения
                SetAttributeValue_String(22, current.AssignationName, gbuObject.Id, idDocument, sDate, otDate, SRDSession.Current.UserID, otDate);
                //Наименование объекта
                SetAttributeValue_String(19, current.Name, gbuObject.Id, idDocument, sDate, otDate, SRDSession.Current.UserID, otDate);

                if (current.Floors != null)
                {
                    //Количество этажей
                    SetAttributeValue_String(17, current.Floors.Floors, gbuObject.Id, idDocument, sDate, otDate, SRDSession.Current.UserID, otDate);
                    //Количество подземных этажей
                    SetAttributeValue_String(18, current.Floors.Underground_Floors, gbuObject.Id, idDocument, sDate, otDate, SRDSession.Current.UserID, otDate);
                }
                if (current.Years != null)
                {
                    //Год ввода в эксплуатацию
                    SetAttributeValue_String(16, current.Years.Year_Used, gbuObject.Id, idDocument, sDate, otDate, SRDSession.Current.UserID, otDate);
                    //Год постройки
                    SetAttributeValue_String(15, current.Years.Year_Built, gbuObject.Id, idDocument, sDate, otDate, SRDSession.Current.UserID, otDate);
                }
                //Тип объекта
                SetAttributeValue_String(26, current.TypeRealty, gbuObject.Id, idDocument, sDate, otDate, SRDSession.Current.UserID, otDate);
                //Местоположение
                SetAttributeValue_String(8, xmlAdress.GetTextPlace(current.Adress), gbuObject.Id, idDocument, sDate, otDate, SRDSession.Current.UserID, otDate);
                //Адрес
                SetAttributeValue_String(600, xmlAdress.GetTextAdress(current.Adress), gbuObject.Id, idDocument, sDate, otDate, SRDSession.Current.UserID, otDate);
                //Кадастровая стоимость
                if (current.CadastralCost != null) SetAttributeValue_Numeric(6, Convert.ToDecimal(current.CadastralCost.Value), gbuObject.Id, idDocument, sDate, otDate, SRDSession.Current.UserID, otDate);
                //Кадастровый квартал
                SetAttributeValue_String(601, current.CadastralNumberBlock, gbuObject.Id, idDocument, sDate, otDate, SRDSession.Current.UserID, otDate);
                //Земельный участок
                SetAttributeValue_String(602, xmlCodeName.GetNames(current.ParentCadastralNumbers), gbuObject.Id, idDocument, sDate, otDate, SRDSession.Current.UserID, otDate);
                #endregion

                #region Задание на оценку
                ObjectModel.KO.OMUnit koUnit = ObjectModel.KO.OMUnit.Where(x => x.ObjectId == gbuObject.Id && x.TaskId == idTask && x.TourId == idTour).SelectAll().ExecuteFirstOrDefault();
                if (koUnit == null)
                {

                    koUnit = new ObjectModel.KO.OMUnit
                    {
                        Id = -1,
                        ModelId = -1,
                        TourId = idTour,
                        TaskId = idTask,
                        GroupId = -1,
                        Status_Code = KoUnitStatus.Initial,
                        ObjectId = gbuObject.Id,
                        CreationDate = unitDate,
                        CadastralNumber = current.CadastralNumber,
                        CadastralBlock = current.CadastralNumberBlock,
                        PropertyType_Code = PropertyTypes.Building,
                        StatusRepeatCalc_Code = KoStatusRepeatCalc.Initial,
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
                    ObjectModel.KO.OMCostRosreestr cost = new ObjectModel.KO.OMCostRosreestr
                    {
                        Id=-1,
                        Applicationdate = (current.CadastralCost.ApplicationDate == DateTime.MinValue) ? (DateTime?)null : current.CadastralCost.ApplicationDate,
                        Dateapproval = (current.CadastralCost.DateApproval == DateTime.MinValue) ? (DateTime?)null : current.CadastralCost.DateApproval,
                        Dateentering = (current.CadastralCost.DateEntering == DateTime.MinValue) ? (DateTime?)null : current.CadastralCost.DateEntering,
                        Datevaluation = (current.CadastralCost.DateValuation == DateTime.MinValue) ? (DateTime?)null : current.CadastralCost.DateValuation,
                        Docdate = (current.CadastralCost.DocDate == DateTime.MinValue) ? (DateTime?)null : current.CadastralCost.DocDate,
                        Docnumber = current.CadastralCost.DocNumber,
                        Docname = current.CadastralCost.DocName,
                        IdObject = koUnit.Id,
                        Costvalue = Convert.ToDecimal(current.CadastralCost.Value),
                        Revisalstatementdate = (current.CadastralCost.RevisalStatementDate == DateTime.MinValue) ? (DateTime?)null : current.CadastralCost.RevisalStatementDate,
                    };
                    cost.Save();
                }
                #endregion
                #endregion

                #region Заполнение фактора Год постройки
                if (Id_Factor_Year > 0)
                {
                    if (current.Years != null)
                    {
                        if (current.Years.Year_Used != null && current.Years.Year_Used != string.Empty)
                        {
                            //TODO: Добавить фактор Год постройки на основании данных года ввода в эксплуатацию
                            //
                            //
                            //
                        }
                        else
                        if (current.Years.Year_Built != null && current.Years.Year_Built != string.Empty)
                        {
                            //TODO: Добавить фактор Год постройки на основании данных года завершения строительства
                            //
                            //
                            //
                        }
                    }
                }
                #endregion
            }

        }
        public static void ImportObjectUncomplited(xmlObjectUncomplited current, DateTime unitDate, long idTour, long idTask, DateTime sDate, DateTime otDate, long idDocument)
        {
            xmlObjectUncomplited prev = null;

            #region Получение данных о прошлой оценке данного объекта
            //TODO: prev=????????
            // 
            //
            //
            //
            #endregion

            //Если данные о прошлой оценке найдены
            if (prev != null)
            {
                #region Импорт нового объекта
                // TODO: Импорт нового объекта
                //
                //
                //
                //
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

                #region Сохранение данных ГКН
                //Процент готовности
                SetAttributeValue_Numeric(46, (current.DegreeReadiness == string.Empty || current.DegreeReadiness == null) ? (decimal?)null : Convert.ToDecimal(current.DegreeReadiness), gbuObject.Id, idDocument, sDate, otDate, SRDSession.Current.UserID, otDate);
                //Площадь
                SetAttributeValue_String(44, xmlCodeNameValue.GetNames(current.KeyParameters), gbuObject.Id, idDocument, sDate, otDate, SRDSession.Current.UserID, otDate);
                //Дата образования
                SetAttributeValue_Date(13, (current.DateCreate == DateTime.MinValue) ? (DateTime?)null : current.DateCreate, gbuObject.Id, idDocument, sDate, otDate, SRDSession.Current.UserID, otDate);
                //Наименование объекта
                SetAttributeValue_String(19, current.AssignationName, gbuObject.Id, idDocument, sDate, otDate, SRDSession.Current.UserID, otDate);

                //Тип объекта
                SetAttributeValue_String(26, current.TypeRealty, gbuObject.Id, idDocument, sDate, otDate, SRDSession.Current.UserID, otDate);
                //Местоположение
                SetAttributeValue_String(8, xmlAdress.GetTextPlace(current.Adress), gbuObject.Id, idDocument, sDate, otDate, SRDSession.Current.UserID, otDate);
                //Адрес
                SetAttributeValue_String(600, xmlAdress.GetTextAdress(current.Adress), gbuObject.Id, idDocument, sDate, otDate, SRDSession.Current.UserID, otDate);
                //Кадастровая стоимость
                if (current.CadastralCost != null) SetAttributeValue_Numeric(6, Convert.ToDecimal(current.CadastralCost.Value), gbuObject.Id, idDocument, sDate, otDate, SRDSession.Current.UserID, otDate);
                //Кадастровый квартал
                SetAttributeValue_String(601, current.CadastralNumberBlock, gbuObject.Id, idDocument, sDate, otDate, SRDSession.Current.UserID, otDate);
                //Земельный участок
                SetAttributeValue_String(602, xmlCodeName.GetNames(current.ParentCadastralNumbers), gbuObject.Id, idDocument, sDate, otDate, SRDSession.Current.UserID, otDate);
                #endregion

                #region Задание на оценку
                ObjectModel.KO.OMUnit koUnit = ObjectModel.KO.OMUnit.Where(x => x.ObjectId == gbuObject.Id && x.TaskId == idTask && x.TourId == idTour).SelectAll().ExecuteFirstOrDefault();
                if (koUnit == null)
                {
                    koUnit = new ObjectModel.KO.OMUnit
                    {
                        Id = -1,
                        ModelId = -1,
                        TourId = idTour,
                        TaskId = idTask,
                        GroupId = -1,
                        Status_Code = KoUnitStatus.Initial,
                        ObjectId = gbuObject.Id,
                        CreationDate = unitDate,
                        CadastralNumber = current.CadastralNumber,
                        CadastralBlock = current.CadastralNumberBlock,
                        PropertyType_Code = PropertyTypes.UncompletedBuilding,
                        StatusRepeatCalc_Code = KoStatusRepeatCalc.Initial,
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
                    ObjectModel.KO.OMCostRosreestr cost = new ObjectModel.KO.OMCostRosreestr
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
                        Costvalue = Convert.ToDecimal(current.CadastralCost.Value),
                        Revisalstatementdate = (current.CadastralCost.RevisalStatementDate == DateTime.MinValue) ? (DateTime?)null : current.CadastralCost.RevisalStatementDate,
                    };
                    cost.Save();
                }
                #endregion
                #endregion
            }

        }

    }
}
