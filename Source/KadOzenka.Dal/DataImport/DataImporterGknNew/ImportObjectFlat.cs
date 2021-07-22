using System;
using System.Collections.Generic;
using System.Linq;
using CommonSdks.Excel;
using KadOzenka.Dal.DataImport.DataImporterGknNew.Attributes;
using KadOzenka.Dal.GbuObject;
using KadOzenka.Dal.XmlParser;
using ObjectModel.Directory;
using ObjectModel.KO;

namespace KadOzenka.Dal.DataImport.DataImporterGknNew
{
	public class ImportObjectFlat : ImportObjectBase<xmlObjectFlat>
    {
	    public override PropertyTypes PropertyType => PropertyTypes.Pllacement;

        public override string ErrorMessage => "Ошибка импорта данных ГКН во время загрузки Помещений";
		public override string CancelMessage => "Импорт данных ГКН был отменен во время загрузки Помещений";
		public override string SuccessMessage => "Импорт Помещений завершен";

		public ImportObjectFlat(List<ImportedAttributeGkn> flatAttributes, DateTime unitDate, OMTask task,
			Action increaseImportedObjectsCountAction, Action<long, long> updateObjectsAttributesAction,
			GbuReportService gbuReportService, object locked)
			: base(unitDate, task, increaseImportedObjectsCountAction, updateObjectsAttributesAction, flatAttributes,
				gbuReportService, locked)
		{
		}

		protected override void InitAttributeChangeStatusList()
		{
			base.InitAttributeChangeStatusList();
			AttributeChangeStatuses.Add(KoChangeStatus.Square, new ImportedAttribute(Consts.SquareAttributeId));
			AttributeChangeStatuses.Add(KoChangeStatus.Assignment, new ImportedAttribute(Consts.PlacementPurposeAttributeId));
			AttributeChangeStatuses.Add(KoChangeStatus.Name, new ImportedAttribute(Consts.ObjectNameAttributeId));
			AttributeChangeStatuses.Add(KoChangeStatus.Use, new ImportedAttribute(Consts.BuildingPurposeAttributeId));
			AttributeChangeStatuses.Add(KoChangeStatus.YearBuild, new ImportedAttribute(Consts.YearOfBuildAttributeId));
			AttributeChangeStatuses.Add(KoChangeStatus.YearUse, new ImportedAttribute(Consts.YearOfUseAttributeId));
			AttributeChangeStatuses.Add(KoChangeStatus.Floors, new ImportedAttribute(Consts.FloorCountAttributeId));
			AttributeChangeStatuses.Add(KoChangeStatus.DownFloors, new ImportedAttribute(Consts.FloorUndergroundCountAttributeId));
			AttributeChangeStatuses.Add(KoChangeStatus.Walls, new ImportedAttribute(Consts.WallMaterialAttributeId));
			AttributeChangeStatuses.Add(KoChangeStatus.CadastralBuilding, new ImportedAttribute(Consts.CadastralNumberOKSAttributeId));
            AttributeChangeStatuses.Add(KoChangeStatus.NumberFloor, new ImportedAttribute(Consts.FloorNumberAttributeId));
		}

        protected override void DoInheritanceFromPrevUnit(OMUnit lastUnit, OMUnit koUnit, Dictionary<KoChangeStatus, bool> unitChangesDictionary)
		{
			//Признак не поменялся ли тип объекта?
			bool prTypeObjectCheck = lastUnit.PropertyType_Code == koUnit.PropertyType_Code;

			//Если не было изменений типа, наименования и назначения и не было обращения
			if (prTypeObjectCheck && unitChangesDictionary[KoChangeStatus.Assignment]
                                  && unitChangesDictionary[KoChangeStatus.Name])
			{
				#region Наследование группы и подгруппы предыдущего объекта
				koUnit.GroupId = lastUnit.GroupId;
				koUnit.Save();
				#endregion
			}
		}

		protected override void InitTaskFormingAttributes()
		{
			base.InitTaskFormingAttributes();
			TaskFormingAttributes.Add(new ImportedAttribute(Consts.P3WallMaterialAttributeId));
			TaskFormingAttributes.Add(new ImportedAttribute(Consts.P4YearOfBuildAttributeId));
		}

        protected override void SetCODTasksFormingAttributesWithChecking(long gbuObjectId, Dictionary<KoChangeStatus, bool> unitChangesDictionary)
		{
			if (!unitChangesDictionary[KoChangeStatus.Name] || !unitChangesDictionary[KoChangeStatus.Assignment])
			{
				SaveDataAttribute(TaskFormingAttributes.First(x => x.AttributeId == Consts.P1GroupAttributeId), true, gbuObjectId);
			}
			if (!unitChangesDictionary[KoChangeStatus.CadastralBlock] || !unitChangesDictionary[KoChangeStatus.Place])
			{
				SaveDataAttribute(TaskFormingAttributes.First(x => x.AttributeId == Consts.P2FsAttributeId), true, gbuObjectId);
			}
			if (!unitChangesDictionary[KoChangeStatus.Walls])
			{
				SaveDataAttribute(TaskFormingAttributes.First(x => x.AttributeId == Consts.P3WallMaterialAttributeId), true, gbuObjectId);
			}
			if (!unitChangesDictionary[KoChangeStatus.YearBuild] || !unitChangesDictionary[KoChangeStatus.YearUse])
			{
				SaveDataAttribute(TaskFormingAttributes.First(x => x.AttributeId == Consts.P4YearOfBuildAttributeId), true, gbuObjectId);
			}
		}

        protected override void SetAdditionalUnitProperties(OMUnit koUnit, xmlObjectFlat current)
		{
			koUnit.BuildingCadastralNumber = current.CadastralNumberOKS;
		}
    }
}
