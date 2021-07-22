using System;
using System.Collections.Generic;
using System.Linq;
using CommonSdks.Excel;
using KadOzenka.Dal.DataImport.DataImporterGknNew.Attributes;
using KadOzenka.Dal.GbuObject;
using KadOzenka.Dal.XmlParser;
using ObjectModel.Directory;
using ObjectModel.Directory.KO;
using ObjectModel.KO;

namespace KadOzenka.Dal.DataImport.DataImporterGknNew
{
	public class ImportObjectConstruction : ImportObjectBase<xmlObjectConstruction>
    {
	    private long? _ftYearInheritanceFactorId;

        public override PropertyTypes PropertyType => PropertyTypes.Construction;

        public override string ErrorMessage => "Ошибка импорта данных ГКН во время загрузки Сооружений";
		public override string CancelMessage => "Импорт данных ГКН был отменен во время загрузки Сооружений";
		public override string SuccessMessage => "Импорт Сооружений завершен";

		public ImportObjectConstruction(List<ImportedAttributeGkn> constructionAttributes, DateTime unitDate,
			OMTask task,
			Action increaseImportedObjectsCountAction, Action<long, long> updateObjectsAttributesAction,
			GbuReportService gbuReportService, object locked)
			: base(unitDate, task, increaseImportedObjectsCountAction, updateObjectsAttributesAction,
				constructionAttributes, gbuReportService, locked)
		{
			InitFactorInheritanceSettings();
        }

		private void InitFactorInheritanceSettings()
		{
			var factorSettings = OMFactorSettings
				.Where(x => x.Inheritance_Code == FactorInheritance.ftYear)
				.Select(x => x.FactorId)
				.ExecuteFirstOrDefault();

			if (factorSettings!= null)
				_ftYearInheritanceFactorId = factorSettings.FactorId;
		}

		protected override void InitAttributeChangeStatusList()
		{
			base.InitAttributeChangeStatusList();
			AttributeChangeStatuses.Add(KoChangeStatus.Square, new ImportedAttribute(Consts.PlacementCharacteristicAttributeId));
			AttributeChangeStatuses.Add(KoChangeStatus.Assignment, new ImportedAttribute(Consts.ConstructionPurposeAttributeId));
			AttributeChangeStatuses.Add(KoChangeStatus.YearBuild, new ImportedAttribute(Consts.YearOfBuildAttributeId));
			AttributeChangeStatuses.Add(KoChangeStatus.YearUse, new ImportedAttribute(Consts.YearOfUseAttributeId));
			AttributeChangeStatuses.Add(KoChangeStatus.Floors, new ImportedAttribute(Consts.FloorCountAttributeId));
			AttributeChangeStatuses.Add(KoChangeStatus.DownFloors, new ImportedAttribute(Consts.FloorUndergroundCountAttributeId));
			AttributeChangeStatuses.Add(KoChangeStatus.Name, new ImportedAttribute(Consts.ObjectNameAttributeId));
			AttributeChangeStatuses.Add(KoChangeStatus.NumberParcel, new ImportedAttribute(Consts.ParcelAttributeId));
			AttributeChangeStatuses.Add(KoChangeStatus.Walls, new ImportedAttribute(Consts.WallMaterialAttributeId));
		}

        protected override void InitTaskFormingAttributes()
		{
			base.InitTaskFormingAttributes();
			TaskFormingAttributes.Add(new ImportedAttribute(Consts.P4YearOfBuildAttributeId));
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

			//Если год ввода в эксплуатацию и год завершения строительства  не поменялся
			if (_ftYearInheritanceFactorId.HasValue)
			{
				//Если в предыдущем объекте есть фактор Год постройки итоговый
				//его надо скопировать в новый объект, если нет, добавить надо.
				koUnit.AddKOFactor(_ftYearInheritanceFactorId.Value, (unitChangesDictionary[KoChangeStatus.YearUse]
				                                                && unitChangesDictionary[KoChangeStatus.YearBuild]) ? lastUnit : null, string.Empty);
			}
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
			if (!unitChangesDictionary[KoChangeStatus.YearBuild] || !unitChangesDictionary[KoChangeStatus.YearUse])
			{
				SaveDataAttribute(TaskFormingAttributes.First(x => x.AttributeId == Consts.P4YearOfBuildAttributeId), true, gbuObjectId);
			}
		}
    }
}
