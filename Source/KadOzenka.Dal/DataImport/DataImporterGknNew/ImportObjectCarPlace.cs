using System;
using System.Collections.Generic;
using System.Linq;
using Core.Shared.Extensions;
using KadOzenka.Dal.DataImport.DataImporterGknNew.Attributes;
using KadOzenka.Dal.XmlParser;
using ObjectModel.Directory;
using ObjectModel.KO;

namespace KadOzenka.Dal.DataImport.DataImporterGknNew
{
	public class ImportObjectCarPlace : ImportObjectBase<xmlObjectCarPlace>
    {
	    public override PropertyTypes PropertyType => PropertyTypes.Parking;

        public override string ErrorMessage => "Ошибка импорта данных ГКН во время загрузки Машино-мест";
		public override string CancelMessage => "Импорт данных ГКН был отменен во время загрузки Машино-мест";
		public override string SuccessMessage => "Импорт Машино-мест завершен";

		public ImportObjectCarPlace(DateTime unitDate, long idTour, long idTask, KoNoteType koNoteType, DateTime sDate, DateTime otDate, long idDocument, Action increaseImportedObjectsCountAction, Action<long, long> updateObjectsAttributesAction)
			: base(unitDate, idTour, idTask, koNoteType, sDate, otDate, idDocument, increaseImportedObjectsCountAction, updateObjectsAttributesAction)
		{
		}

		protected override void InitAttributeChangeStatusList()
		{
			base.InitAttributeChangeStatusList();
			AttributeChangeStatuses.Add(KoChangeStatus.Square, new ImportedAttribute(2));
			AttributeChangeStatuses.Add(KoChangeStatus.Name, new ImportedAttribute(19));
			AttributeChangeStatuses.Add(KoChangeStatus.Use, new ImportedAttribute(14));
			AttributeChangeStatuses.Add(KoChangeStatus.YearBuild, new ImportedAttribute(15));
			AttributeChangeStatuses.Add(KoChangeStatus.YearUse, new ImportedAttribute(16));
			AttributeChangeStatuses.Add(KoChangeStatus.Floors, new ImportedAttribute(17));
			AttributeChangeStatuses.Add(KoChangeStatus.DownFloors, new ImportedAttribute(18));
			AttributeChangeStatuses.Add(KoChangeStatus.Walls, new ImportedAttribute(21));
			AttributeChangeStatuses.Add(KoChangeStatus.CadastralBuilding, new ImportedAttribute(604));
			AttributeChangeStatuses.Add(KoChangeStatus.NumberFloor, new ImportedAttribute(24));
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
	        if (prTypeObjectCheck && unitChangesDictionary[KoChangeStatus.Use]
	                              && unitChangesDictionary[KoChangeStatus.Name])
	        {
		        #region Наследование группы и подгруппы предыдущего объекта
		        koUnit.GroupId = lastUnit.GroupId;
		        koUnit.Save();
		        #endregion
	        }
        }

        protected override void SetCODTasksFormingAttributesWithChecking(long gbuObjectId, Dictionary<KoChangeStatus, bool> unitChangesDictionary)
		{
			if (!unitChangesDictionary[KoChangeStatus.Name] || !unitChangesDictionary[KoChangeStatus.Use])
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
			GknDataAttributes.Add(new ImportedAttributeGkn(2, current => ((xmlObjectCarPlace)current).Area.ParseToDecimal()));
			GknDataAttributes.Add(new ImportedAttributeGkn(19, current => ((xmlObjectCarPlace)current).TypeRealty));
			GknDataAttributes.Add(new ImportedAttributeGkn(14, current => ((xmlObjectCarPlace)current).parentAssignationBuilding.Name, current => ((xmlObjectCarPlace)current).parentAssignationBuilding != null));
			GknDataAttributes.Add(new ImportedAttributeGkn(22, current => ((xmlObjectCarPlace)current).parentAssignationName, current => !string.IsNullOrEmpty(((xmlObjectCarPlace)current).parentAssignationName)));
			GknDataAttributes.Add(new ImportedAttributeGkn(17, current => ((xmlObjectCarPlace)current).parentFloors.Floors, current => ((xmlObjectCarPlace)current).parentFloors != null));
			GknDataAttributes.Add(new ImportedAttributeGkn(18, current => ((xmlObjectCarPlace)current).parentFloors.Underground_Floors, current => ((xmlObjectCarPlace)current).parentFloors != null));
			GknDataAttributes.Add(new ImportedAttributeGkn(16, current => ((xmlObjectCarPlace)current).parentYears.Year_Used, current => ((xmlObjectCarPlace)current).parentYears != null));
			GknDataAttributes.Add(new ImportedAttributeGkn(15, current => ((xmlObjectCarPlace)current).parentYears.Year_Built, current => ((xmlObjectCarPlace)current).parentYears != null));
			GknDataAttributes.Add(new ImportedAttributeGkn(21, current => xmlCodeName.GetNames(((xmlObjectCarPlace)current).parentWalls), current => !string.IsNullOrEmpty(xmlCodeName.GetNames(((xmlObjectCarPlace)current).parentWalls))));
			GknDataAttributes.Add(new ImportedAttributeGkn(604, current => ((xmlObjectCarPlace)current).CadastralNumberOKS));
			GknDataAttributes.Add(new ImportedAttributeGkn(606, current => xmlCodeName.GetNames(((xmlObjectCarPlace)current).PositionsInObject[0].NumbersOnPlan), current => ((xmlObjectCarPlace)current).PositionsInObject.Count > 0));
			GknDataAttributes.Add(new ImportedAttributeGkn(25, current => ((xmlObjectCarPlace)current).PositionsInObject[0].Position.Name, current => ((xmlObjectCarPlace)current).PositionsInObject.Count > 0));
			GknDataAttributes.Add(new ImportedAttributeGkn(24, current => ((xmlObjectCarPlace)current).PositionsInObject[0].Position.Value, current => ((xmlObjectCarPlace)current).PositionsInObject.Count > 0));
		}

		protected override void SetAdditionalUnitProperties(OMUnit koUnit, xmlObjectCarPlace current)
		{
			koUnit.Square = current.Area.ParseToDecimal();
			koUnit.BuildingCadastralNumber = current.CadastralNumberOKS;
		}
    }
}
