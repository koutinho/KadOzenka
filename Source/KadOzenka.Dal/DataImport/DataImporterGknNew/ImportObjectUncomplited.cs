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
			AttributeChangeStatuses.Add(KoChangeStatus.Procent, new ImportedAttribute(46));
			AttributeChangeStatuses.Add(KoChangeStatus.Square, new ImportedAttribute(44));
			AttributeChangeStatuses.Add(KoChangeStatus.Name, new ImportedAttribute(19));
			AttributeChangeStatuses.Add(KoChangeStatus.NumberParcel, new ImportedAttribute(602));
		}

		protected override void InitGknDataAttributes()
		{
			base.InitGknDataAttributes();
			GknDataAttributes.Add(new ImportedAttributeGkn(46, current => string.IsNullOrEmpty(((xmlObjectUncomplited)current).DegreeReadiness) ? (decimal?)null : ((xmlObjectUncomplited)current).DegreeReadiness.ParseToDecimal()));
			GknDataAttributes.Add(new ImportedAttributeGkn(44, current => xmlCodeNameValue.GetNames(((xmlObjectUncomplited)current).KeyParameters)));
			GknDataAttributes.Add(new ImportedAttributeGkn(19, current => ((xmlObjectUncomplited)current).AssignationName));
			GknDataAttributes.Add(new ImportedAttributeGkn(602, current => xmlCodeName.GetNames(((xmlObjectUncomplited)current).ParentCadastralNumbers)));
		}

        protected override void SetCODTasksFormingAttributesWithChecking(long gbuObjectId, Dictionary<KoChangeStatus, bool> unitChangesDictionary)
		{
			if (!unitChangesDictionary[KoChangeStatus.Name])
			{
				SaveDataAttribute(TaskFormingAttributes.First(x => x.AttributeId == 660), true, gbuObjectId);
			}
			if (!unitChangesDictionary[KoChangeStatus.CadastralBlock] || !unitChangesDictionary[KoChangeStatus.Place])
			{
				SaveDataAttribute(TaskFormingAttributes.First(x => x.AttributeId == 661), true, gbuObjectId);
			}
		}

        protected override void SetAdditionalUnitProperties(OMUnit koUnit, xmlObjectUncomplited current)
        {
	        if (!string.IsNullOrEmpty(current.DegreeReadiness))
		        koUnit.DegreeReadiness = current.DegreeReadiness.ParseToLong();
        }
    }
}
