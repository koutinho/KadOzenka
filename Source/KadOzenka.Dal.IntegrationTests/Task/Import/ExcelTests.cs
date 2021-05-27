using System.Collections.Generic;
using KadOzenka.Dal.DataImport.DataImporterGknNew;
using KadOzenka.Dal.DataImport.DataImporterGknNew.Attributes;
using KadOzenka.Dal.IntegrationTests.Task._Builders;
using NUnit.Framework;
using ObjectModel.Directory;

namespace KadOzenka.Dal.IntegrationTests.Task.Import
{
	public class ExcelTests : BaseImportTests
	{
		[Test]
		public void Can_Import_Task_Document()
		{
			var fileName = "one_parcel_success.xlsx";
			var columnsMapping = GetColumnsMapping();
			var task = new TaskBuilder().Document(Document.Id).Build();

			StartImport(task.Id, fileName, columnsMapping);

			CheckDataLog(task.Id, 1, 1);
			CheckCreatedUnits(task.Id, 1, PropertyTypes.Stead);
		}

		[Test]
		public void If_CanNot_Parse_Data_From_File_So_Include_Object_In_Total_Counter()
		{
			var fileName = "two_parcels_error.xlsx";
			var columnsMapping = GetColumnsMapping();
			var task = new TaskBuilder().Document(Document.Id).Build();

			StartImport(task.Id, fileName, columnsMapping);

			CheckDataLog(task.Id, 2, 1);
			CheckCreatedUnits(task.Id, 1, PropertyTypes.Stead);
		}

		
		#region Support Methods

		private List<ColumnToAttributeMapping> GetColumnsMapping()
		{
			return new List<ColumnToAttributeMapping>
			{
				new ColumnToAttributeMapping
					{AttributeId = RequiredFieldsForExcelMapping.CadastralNumberAttributeId, ColumnIndex = 0},
				new ColumnToAttributeMapping
					{AttributeId = RequiredFieldsForExcelMapping.ObjectTypeAttributeId, ColumnIndex = 1},
				new ColumnToAttributeMapping
					{AttributeId = RequiredFieldsForExcelMapping.AssessmentDateAttributeId, ColumnIndex = 2},
				new ColumnToAttributeMapping
					{AttributeId = RequiredFieldsForExcelMapping.SquareAttributeId, ColumnIndex = 3},
				new ColumnToAttributeMapping {AttributeId = Consts.CadastralCostDocDateAttributeId, ColumnIndex = 5}
			};
		}

		#endregion
	}
}
