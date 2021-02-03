using System;
using System.Collections.Generic;
using System.Linq;
using Core.Shared.Extensions;
using KadOzenka.Dal.DataImport.DataImporterGknNew.Attributes;
using KadOzenka.Dal.XmlParser;
using ObjectModel.Directory;
using ObjectModel.Directory.KO;
using ObjectModel.KO;

namespace KadOzenka.Dal.DataImport.DataImporterGknNew
{
	public class ImportObjectBuild : ImportObjectBase<xmlObjectBuild>
	{
		private long? _ftWallInheritanceFactorId;
		private long? _ftYearInheritanceFactorId;

        public override PropertyTypes PropertyType => PropertyTypes.Building;

        public override string ErrorMessage => "Ошибка импорта данных ГКН во время загрузки Зданий";
        public override string CancelMessage => "Импорт данных ГКН был отменен во время загрузки Зданий";
        public override string SuccessMessage => "Импорт Зданий завершен";

        public ImportObjectBuild(DateTime unitDate, long idTour, long idTask, KoNoteType koNoteType, DateTime sDate,
	        DateTime otDate, long idDocument, Action increaseImportedObjectsCountAction, Action<long, long> updateObjectsAttributesAction) 
	        : base(unitDate, idTour, idTask, koNoteType, sDate, otDate, idDocument, increaseImportedObjectsCountAction, updateObjectsAttributesAction)
        {
	        InitFactorInheritanceSettings();
        }

        private void InitFactorInheritanceSettings()
        {
	        var factorSettings = OMFactorSettings
		        .Where(x => x.Inheritance_Code == FactorInheritance.ftWall || x.Inheritance_Code == FactorInheritance.ftYear)
		        .Select(x => new { x.Inheritance_Code, x.FactorId})
		        .Execute();

	        if (factorSettings.Any(x => x.Inheritance_Code == FactorInheritance.ftWall))
		        _ftWallInheritanceFactorId = factorSettings.First(x => x.Inheritance_Code == FactorInheritance.ftWall).FactorId;

	        if (factorSettings.Any(x => x.Inheritance_Code == FactorInheritance.ftYear))
		        _ftYearInheritanceFactorId = factorSettings.First(x => x.Inheritance_Code == FactorInheritance.ftYear).FactorId;
        }

        protected override void  InitAttributeChangeStatusList()
        {
            base.InitAttributeChangeStatusList();
            AttributeChangeStatuses.Add(KoChangeStatus.Square, new ImportedAttribute(2));
            AttributeChangeStatuses.Add(KoChangeStatus.Assignment, new ImportedAttribute(14));
            AttributeChangeStatuses.Add(KoChangeStatus.YearBuild, new ImportedAttribute(15));
            AttributeChangeStatuses.Add(KoChangeStatus.YearUse, new ImportedAttribute(16));
            AttributeChangeStatuses.Add(KoChangeStatus.Floors, new ImportedAttribute(17));
            AttributeChangeStatuses.Add(KoChangeStatus.DownFloors, new ImportedAttribute(18));
            AttributeChangeStatuses.Add(KoChangeStatus.Name, new ImportedAttribute(19));
            AttributeChangeStatuses.Add(KoChangeStatus.Walls, new ImportedAttribute(21));
            AttributeChangeStatuses.Add(KoChangeStatus.NumberParcel, new ImportedAttribute(602));
        }

        protected override void InitTaskFormingAttributes()
        {
	        base.InitTaskFormingAttributes();
            TaskFormingAttributes.Add(new ImportedAttribute(662));
            TaskFormingAttributes.Add(new ImportedAttribute(663));
        }

        protected override void DoInheritanceFromPrevUnit(OMUnit lastUnit, OMUnit koUnit, Dictionary<KoChangeStatus, bool> unitChangesDictionary)
        {
            //Признак не поменялся ли тип объекта?
            bool prTypeObjectCheck = lastUnit.PropertyType_Code == koUnit.PropertyType_Code;

            //Если не было изменений типа, наименования и назначения и не было обращения
            if (prTypeObjectCheck && unitChangesDictionary[KoChangeStatus.Name]
                                  && unitChangesDictionary[KoChangeStatus.Assignment])
            {
                #region Наследование группы и подгруппы предыдущего объекта

                koUnit.GroupId = lastUnit.GroupId;
                koUnit.Save();

                #endregion
            }

            //Если материал стен не поменялся
            if (_ftWallInheritanceFactorId.HasValue)
            {
	            //Если в предыдущем объекте есть фактор Материал стен итоговый
                //его надо скопировать в новый объект, если нет, добавить надо.
                koUnit.AddKOFactor(_ftWallInheritanceFactorId.Value,
                    (unitChangesDictionary[KoChangeStatus.Walls])
                        ? lastUnit
                        : null, string.Empty);
            }

            //Если год ввода в эксплуатацию и год завершения строительства  не поменялся
            if (_ftYearInheritanceFactorId.HasValue)
            {
                //Если в предыдущем объекте есть фактор Год постройки итоговый
                //его надо скопировать в новый объект, если нет, добавить надо.
                koUnit.AddKOFactor(_ftYearInheritanceFactorId.Value,
                    (unitChangesDictionary[KoChangeStatus.YearUse] && unitChangesDictionary[KoChangeStatus.YearBuild])
                        ? lastUnit
                        : null, string.Empty);
            }
        }

        protected override void SetCODTasksFormingAttributesWithChecking(long gbuObjectId, Dictionary<KoChangeStatus, bool> unitChangesDictionary)
        {
	        if (!unitChangesDictionary[KoChangeStatus.Name] || !unitChangesDictionary[KoChangeStatus.Assignment])
	        {
		        SaveDataAttribute(TaskFormingAttributes.First(x => x.AttributeId == 660), true, gbuObjectId);
	        }
	        if (!unitChangesDictionary[KoChangeStatus.CadastralBlock] || !unitChangesDictionary[KoChangeStatus.Place])
	        {
		        SaveDataAttribute(TaskFormingAttributes.First(x => x.AttributeId == 661), true, gbuObjectId);
            }
	        if (!unitChangesDictionary[KoChangeStatus.Walls])
	        {
		        SaveDataAttribute(TaskFormingAttributes.First(x => x.AttributeId == 662), true, gbuObjectId);
            }
	        if (!unitChangesDictionary[KoChangeStatus.YearBuild] || !unitChangesDictionary[KoChangeStatus.YearUse])
	        {
		        SaveDataAttribute(TaskFormingAttributes.First(x => x.AttributeId == 663), true, gbuObjectId);
            }
        }

        protected override void InitGknDataAttributes()
        {
	        base.InitGknDataAttributes();
	        GknDataAttributes.Add(new ImportedAttributeGkn(2, current => ((xmlObjectBuild)current).Area.ParseToDecimal()));
	        GknDataAttributes.Add(new ImportedAttributeGkn(14, current => ((xmlObjectBuild)current).AssignationBuilding.Name, current => ((xmlObjectBuild)current).AssignationBuilding != null));
	        GknDataAttributes.Add(new ImportedAttributeGkn(19, current => ((xmlObjectBuild)current).Name));
	        GknDataAttributes.Add(new ImportedAttributeGkn(17, current => ((xmlObjectBuild)current).Floors.Floors, current => ((xmlObjectBuild)current).Floors != null));
	        GknDataAttributes.Add(new ImportedAttributeGkn(18, current => ((xmlObjectBuild)current).Floors.Underground_Floors, current => ((xmlObjectBuild)current).Floors != null));
	        GknDataAttributes.Add(new ImportedAttributeGkn(16, current => ((xmlObjectBuild)current).Years.Year_Used, current => ((xmlObjectBuild)current).Years != null));
	        GknDataAttributes.Add(new ImportedAttributeGkn(15, current => ((xmlObjectBuild)current).Years.Year_Built, current => ((xmlObjectBuild)current).Years != null));
	        GknDataAttributes.Add(new ImportedAttributeGkn(21, current => xmlCodeName.GetNames(((xmlObjectBuild)current).Walls)));
	        GknDataAttributes.Add(new ImportedAttributeGkn(602, current => xmlCodeName.GetNames(((xmlObjectBuild)current).ParentCadastralNumbers)));
        }

        protected override void SetAdditionalUnitProperties(OMUnit koUnit, xmlObjectBuild current)
        {
	        koUnit.Square = current.Area.ParseToDecimal();
        }
	}
}
