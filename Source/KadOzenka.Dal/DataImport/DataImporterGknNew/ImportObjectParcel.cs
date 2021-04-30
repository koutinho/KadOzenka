using System;
using System.Collections.Generic;
using System.Linq;
using Core.Shared.Extensions;
using KadOzenka.Dal.DataImport.DataImporterGknNew.Attributes;
using KadOzenka.Dal.GbuObject;
using KadOzenka.Dal.XmlParser;
using ObjectModel.Directory;
using ObjectModel.KO;

namespace KadOzenka.Dal.DataImport.DataImporterGknNew
{
	public class ImportObjectParcel : ImportObjectBase<xmlObjectParcel>
    {
	    public override PropertyTypes PropertyType => PropertyTypes.Stead;

        public override string ErrorMessage => "Ошибка импорта данных ГКН во время загрузки Участков";
		public override string CancelMessage => "Импорт данных ГКН был отменен во время загрузки Участков";
		public override string SuccessMessage => "Импорт Участков завершен";

		public ImportObjectParcel(List<ImportedAttributeGkn> parcelAttributes, DateTime unitDate, OMTask task,
			Action increaseImportedObjectsCountAction, Action<long, long> updateObjectsAttributesAction,
			GbuReportService gbuReportService, object locked)
			: base(unitDate, task, increaseImportedObjectsCountAction, updateObjectsAttributesAction, parcelAttributes,
				gbuReportService, locked)
		{
		}

		protected override void InitAttributeChangeStatusList()
		{
			base.InitAttributeChangeStatusList();
			AttributeChangeStatuses.Add(KoChangeStatus.Name, new ImportedAttribute(Consts.ParcelNameAttributeId));
			AttributeChangeStatuses.Add(KoChangeStatus.Square, new ImportedAttribute(Consts.SquareAttributeId));
			AttributeChangeStatuses.Add(KoChangeStatus.Category, new ImportedAttribute(Consts.LandCategoryAttributeId));
			AttributeChangeStatuses.Add(KoChangeStatus.Use, new ImportedAttribute(Consts.TypeOfUseByDocumentsAttributeId));
		}

        protected override void DoInheritanceFromPrevUnit(OMUnit lastUnit, OMUnit koUnit, Dictionary<KoChangeStatus, bool> unitChangesDictionary)
		{
			//Признак не поменялся ли тип объекта?
			bool prTypeObjectCheck = lastUnit.PropertyType_Code == koUnit.PropertyType_Code;

			//Если не было изменений типа, наименования и назначения и не было обращения
			if (prTypeObjectCheck && unitChangesDictionary[KoChangeStatus.Name]
                && unitChangesDictionary[KoChangeStatus.Use])
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
				SaveDataAttribute(TaskFormingAttributes.First(x => x.AttributeId == Consts.P1GroupAttributeId), true, gbuObjectId);
			}
			if (!unitChangesDictionary[KoChangeStatus.CadastralBlock] || !unitChangesDictionary[KoChangeStatus.Place])
			{
				SaveDataAttribute(TaskFormingAttributes.First(x => x.AttributeId == Consts.P2FsAttributeId), true, gbuObjectId);
			}
		}

        protected override void SetAdditionalUnitProperties(OMUnit koUnit, xmlObjectParcel current)
        {
	        koUnit.Square = current.Area.ParseToDecimal();
        }
    }
}
