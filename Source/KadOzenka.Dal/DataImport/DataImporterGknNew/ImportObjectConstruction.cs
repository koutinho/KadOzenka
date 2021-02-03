using System;
using System.Collections.Generic;
using System.Linq;
using KadOzenka.Dal.DataImport.DataImporterGknNew.Attributes;
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

		/// <summary>
		/// Аттрибут "Земельный участок"
		/// </summary>
		private const long ParcelAttributeId = 602;
		private const long WallMaterialAttributeId = 21;


		public ImportObjectConstruction(DateTime unitDate, long idTour, long idTask, KoNoteType koNoteType, DateTime sDate,
			DateTime otDate, long idDocument, Action increaseImportedObjectsCountAction, Action<long, long> updateObjectsAttributesAction) : base(unitDate, idTour, idTask, koNoteType, sDate, otDate, idDocument, increaseImportedObjectsCountAction, updateObjectsAttributesAction)
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
			AttributeChangeStatuses.Add(KoChangeStatus.Square, new ImportedAttribute(44));
			AttributeChangeStatuses.Add(KoChangeStatus.Assignment, new ImportedAttribute(22));
			AttributeChangeStatuses.Add(KoChangeStatus.YearBuild, new ImportedAttribute(15));
			AttributeChangeStatuses.Add(KoChangeStatus.YearUse, new ImportedAttribute(16));
			AttributeChangeStatuses.Add(KoChangeStatus.Floors, new ImportedAttribute(17));
			AttributeChangeStatuses.Add(KoChangeStatus.DownFloors, new ImportedAttribute(18));
			AttributeChangeStatuses.Add(KoChangeStatus.Name, new ImportedAttribute(19));
			AttributeChangeStatuses.Add(KoChangeStatus.NumberParcel, new ImportedAttribute(602));
			AttributeChangeStatuses.Add(KoChangeStatus.Walls, new ImportedAttribute(21));
		}

        protected override void InitTaskFormingAttributes()
		{
			base.InitTaskFormingAttributes();
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
				SaveDataAttribute(TaskFormingAttributes.First(x => x.AttributeId == 660), true, gbuObjectId);
			}
			if (!unitChangesDictionary[KoChangeStatus.CadastralBlock] || !unitChangesDictionary[KoChangeStatus.Place])
			{
				SaveDataAttribute(TaskFormingAttributes.First(x => x.AttributeId == 661), true, gbuObjectId);
			}
			if (!unitChangesDictionary[KoChangeStatus.YearBuild] || !unitChangesDictionary[KoChangeStatus.YearUse])
			{
				SaveDataAttribute(TaskFormingAttributes.First(x => x.AttributeId == 663), true, gbuObjectId);
			}
		}


        protected override void InitGknDataAttributes()
        {
            base.InitGknDataAttributes();
            GknDataAttributes.Add(new ImportedAttributeGkn(44, current => xmlCodeNameValue.GetNames(((xmlObjectConstruction)current).KeyParameters)));
            GknDataAttributes.Add(new ImportedAttributeGkn(22, current =>((xmlObjectConstruction)current).AssignationName));
            GknDataAttributes.Add(new ImportedAttributeGkn(19, current =>((xmlObjectConstruction)current).Name));
            GknDataAttributes.Add(new ImportedAttributeGkn(17, current =>((xmlObjectConstruction)current).Floors.Floors, current => ((xmlObjectConstruction)current).Floors != null));
            GknDataAttributes.Add(new ImportedAttributeGkn(18, current =>((xmlObjectConstruction)current).Floors.Underground_Floors, current => ((xmlObjectConstruction)current).Floors != null));
            GknDataAttributes.Add(new ImportedAttributeGkn(16, current =>((xmlObjectConstruction)current).Years.Year_Used, current => ((xmlObjectConstruction)current).Years != null));
            GknDataAttributes.Add(new ImportedAttributeGkn(15, current =>((xmlObjectConstruction)current).Years.Year_Built, current => ((xmlObjectConstruction)current).Years != null));
            GknDataAttributes.Add(new ImportedAttributeGkn(602, current => xmlCodeName.GetNames(((xmlObjectConstruction)current).ParentCadastralNumbers)));
            GknDataAttributes.Add(new ImportedAttributeGkn(WallMaterialAttributeId, current => xmlCodeName.GetNames(((xmlObjectConstruction)current).Walls)));
        }
    }
}
