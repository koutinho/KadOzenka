using System;
using System.Collections.Generic;
using System.Linq;
using Core.Shared.Extensions;
using KadOzenka.Dal.ConfigurationManagers.KadOzenkaConfigManager.Models.DataImporterGknConfig;
using KadOzenka.Dal.DataImport.DataImporterGknNew.Attributes;
using KadOzenka.Dal.XmlParser;
using ObjectModel.Directory;
using ObjectModel.KO;

namespace KadOzenka.Dal.DataImport.DataImporterGknNew
{
	public class ImportObjectUncomplited : ImportObjectBase<xmlObjectUncomplited>
    {
	    public override PropertyTypes PropertyType => PropertyTypes.UncompletedBuilding;

        public override string ErrorMessage => "Ошибка импорта данных ГКН во время загрузки Объектов незавершенного строительства";
		public override string CancelMessage => "Импорт данных ГКН был отменен во время загрузки Объектов незавершенного строительства";
		public override string SuccessMessage => "Импорт Объектов незавершенного строительства завершен";

		public ImportObjectUncomplited(DateTime unitDate, long idTour, OMTask task, KoNoteType koNoteType, DateTime sDate, DateTime otDate, long idDocument, Action increaseImportedObjectsCountAction, Action<long, long> updateObjectsAttributesAction)
			: base(unitDate, idTour, task, koNoteType, sDate, otDate, idDocument, increaseImportedObjectsCountAction, updateObjectsAttributesAction)
		{
		}

		protected override void InitAttributeChangeStatusList()
		{
			base.InitAttributeChangeStatusList();
			AttributeChangeStatuses.Add(KoChangeStatus.Procent, new ImportedAttribute(Consts.ReadinessPercentageAttributeId));
			AttributeChangeStatuses.Add(KoChangeStatus.Square, new ImportedAttribute(Consts.PlacementCharacteristicAttributeId));
			AttributeChangeStatuses.Add(KoChangeStatus.Name, new ImportedAttribute(Consts.ObjectNameAttributeId));
			AttributeChangeStatuses.Add(KoChangeStatus.NumberParcel, new ImportedAttribute(Consts.ParcelAttributeId));
		}

		protected override void InitGknDataAttributes()
		{
			base.InitGknDataAttributes();
			TryAddGknDataAttribute(DataImporterGknConfig.GknDataAttributes.Uncompleted.ParentCadastralNumbersAttributeIdValue, current => xmlCodeName.GetNames(((xmlObjectUncomplited)current).ParentCadastralNumbers));
			TryAddGknDataAttribute(DataImporterGknConfig.GknDataAttributes.Uncompleted.AssignationNameAttributeIdValue, current => ((xmlObjectUncomplited)current).AssignationName);
			TryAddGknDataAttribute(DataImporterGknConfig.GknDataAttributes.Uncompleted.KeyParametersAttributeIdValue, current => xmlCodeNameValue.GetNames(((xmlObjectUncomplited)current).KeyParameters));
			if (DataImporterGknConfig.GknDataAttributes.Uncompleted.KeyParameters.Length > 0)
			{
				for (var i = 0; i < DataImporterGknConfig.GknDataAttributes.Uncompleted.KeyParameters.Length; i++)
				{
					var keyParameter = DataImporterGknConfig.GknDataAttributes.Uncompleted.KeyParameters[i];
					var iCounter = i;
					TryAddGknDataAttribute(keyParameter.KeyParameterAttributeIdValue, current => ((xmlObjectUncomplited)current).KeyParameters[iCounter]?.Name,
						current => ((xmlObjectUncomplited)current).KeyParameters.Count >= iCounter + 1);
					TryAddGknDataAttribute(keyParameter.KeyParameterValueAttributeIdValue, current => ((xmlObjectUncomplited)current).KeyParameters[iCounter]?.Value,
						current => ((xmlObjectUncomplited)current).KeyParameters.Count >= iCounter + 1);
				}
			}
			TryAddGknDataAttribute(DataImporterGknConfig.GknDataAttributes.Uncompleted.DegreeReadinessAttributeIdValue, current => ((xmlObjectUncomplited)current).DegreeReadiness);
			
			TryAddGknDataAttribute(DataImporterGknConfig.GknDataAttributes.Uncompleted.FacilityCadastralNumberAttributeIdValue, current => ((xmlObjectUncomplited)current).FacilityCadastralNumber);
			TryAddGknDataAttribute(DataImporterGknConfig.GknDataAttributes.Uncompleted.FacilityPurposeAttributeIdValue, current => ((xmlObjectUncomplited)current).FacilityPurpose);
		}

        protected override void SetCODTasksFormingAttributesWithChecking(long gbuObjectId, Dictionary<KoChangeStatus, bool> unitChangesDictionary)
		{
			if (!unitChangesDictionary[KoChangeStatus.Name])
			{
				SaveDataAttribute(TaskFormingAttributes.First(x => x.AttributeId == Consts.P1GroupAttributeId), true, gbuObjectId);
			}
			if (!unitChangesDictionary[KoChangeStatus.CadastralBlock] || !unitChangesDictionary[KoChangeStatus.Place])
			{
				SaveDataAttribute(TaskFormingAttributes.First(x => x.AttributeId == Consts.P2FsAttributeId), true, gbuObjectId);
			}
		}

        protected override void SetAdditionalUnitProperties(OMUnit koUnit, xmlObjectUncomplited current)
        {
	        koUnit.DegreeReadiness = current.DegreeReadiness;
        }
    }
}
