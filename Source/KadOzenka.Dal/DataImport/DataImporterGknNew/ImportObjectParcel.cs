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
	public class ImportObjectParcel : ImportObjectBase<xmlObjectParcel>
    {
	    public override PropertyTypes PropertyType => PropertyTypes.Stead;

        public override string ErrorMessage => "Ошибка импорта данных ГКН во время загрузки Участков";
		public override string CancelMessage => "Импорт данных ГКН был отменен во время загрузки Участков";
		public override string SuccessMessage => "Импорт Участков завершен";

		public ImportObjectParcel(DateTime unitDate, long idTour, long idTask, KoNoteType koNoteType, DateTime sDate, DateTime otDate, long idDocument, Action increaseImportedObjectsCountAction, Action<long, long> updateObjectsAttributesAction) 
			: base(unitDate, idTour, idTask, koNoteType, sDate, otDate, idDocument, increaseImportedObjectsCountAction, updateObjectsAttributesAction)
		{
		}

		protected override void InitAttributeChangeStatusList()
		{
			base.InitAttributeChangeStatusList();
			AttributeChangeStatuses.Add(KoChangeStatus.Name, new ImportedAttribute(1));
			AttributeChangeStatuses.Add(KoChangeStatus.Square, new ImportedAttribute(2));
			AttributeChangeStatuses.Add(KoChangeStatus.Category, new ImportedAttribute(3));
			AttributeChangeStatuses.Add(KoChangeStatus.Use, new ImportedAttribute(4));
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
				SaveDataAttribute(TaskFormingAttributes.First(x => x.AttributeId == 660), true, gbuObjectId);
			}
			if (!unitChangesDictionary[KoChangeStatus.CadastralBlock] || !unitChangesDictionary[KoChangeStatus.Place])
			{
				SaveDataAttribute(TaskFormingAttributes.First(x => x.AttributeId == 661), true, gbuObjectId);
			}
		}

        protected override void InitGknDataAttributes()
        {
            base.InitGknDataAttributes();
            GknDataAttributes.Add(new ImportedAttributeGkn(1, current => ((xmlObjectParcel)current).Name.Name));
            GknDataAttributes.Add(new ImportedAttributeGkn(2, current => ((xmlObjectParcel)current).Area.ParseToDecimal()));
            GknDataAttributes.Add(new ImportedAttributeGkn(3, current => ((xmlObjectParcel)current).Category.Name));
            GknDataAttributes.Add(new ImportedAttributeGkn(4, current => ((xmlObjectParcel)current).Utilization.ByDoc));
            GknDataAttributes.Add(new ImportedAttributeGkn(5, current => ((xmlObjectParcel)current).Utilization.Utilization.Name));
            //TODO ждем ответа от заказчиков - что сохранять в качестве ЗУ
            //GknDataAttributes.Add(new ImportedAttributeGkn(5, current => xmlCodeName.GetNames(((xmlObjectParcel)current).InnerCadastralNumbers)));
        }

        protected override void SetAdditionalUnitProperties(OMUnit koUnit, xmlObjectParcel current)
        {
	        koUnit.Square = current.Area.ParseToDecimal();
        }
    }
}
