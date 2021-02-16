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
	public class ImportObjectUncomplited : ImportObjectBase<xmlObjectUncomplited>
    {
	    public override PropertyTypes PropertyType => PropertyTypes.UncompletedBuilding;

        public override string ErrorMessage => "Ошибка импорта данных ГКН во время загрузки Объектов незавершенного строительства";
		public override string CancelMessage => "Импорт данных ГКН был отменен во время загрузки Объектов незавершенного строительства";
		public override string SuccessMessage => "Импорт Объектов незавершенного строительства завершен";

		public ImportObjectUncomplited(DateTime unitDate, long idTour, long idTask, KoNoteType koNoteType, DateTime sDate, DateTime otDate, long idDocument, Action increaseImportedObjectsCountAction, Action<long, long> updateObjectsAttributesAction)
			: base(unitDate, idTour, idTask, koNoteType, sDate, otDate, idDocument, increaseImportedObjectsCountAction, updateObjectsAttributesAction)
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
			GknDataAttributes.Add(new ImportedAttributeGkn(Consts.ReadinessPercentageAttributeId, current => string.IsNullOrEmpty(((xmlObjectUncomplited)current).DegreeReadiness) ? (decimal?)null : ((xmlObjectUncomplited)current).DegreeReadiness.ParseToDecimal()));
			GknDataAttributes.Add(new ImportedAttributeGkn(Consts.PlacementCharacteristicAttributeId, current => xmlCodeNameValue.GetNames(((xmlObjectUncomplited)current).KeyParameters)));
			GknDataAttributes.Add(new ImportedAttributeGkn(Consts.ObjectNameAttributeId, current => ((xmlObjectUncomplited)current).AssignationName));
			GknDataAttributes.Add(new ImportedAttributeGkn(Consts.ParcelAttributeId, current => xmlCodeName.GetNames(((xmlObjectUncomplited)current).ParentCadastralNumbers)));
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
	        if (!string.IsNullOrEmpty(current.DegreeReadiness))
		        koUnit.DegreeReadiness = current.DegreeReadiness.ParseToLong();
        }
    }
}
