using System.Collections.Generic;
using KadOzenka.Dal.Integration._Builders.Model;
using NUnit.Framework;
using System.IO;
using System.Linq;
using KadOzenka.Dal.Modeling.Dictionaries.Entities;
using ObjectModel.Common;
using ObjectModel.KO;

namespace KadOzenka.Dal.IntegrationTests.Modeling.Dictionaries
{
	public class ImportTests : BaseModelingTests
	{
		[Test]
		public void Can_Import_Model_Dictionary_Via_Excel()
		{
			OMImportDataLog import;
			var fileName = "marks.xlsx";
			using (var stream = File.OpenRead($"{PathToFileFolder}{fileName}"))
			{
				import = ModelDictionaryService.CreateDataFileImport(stream, fileName);
			}
			var fileInfo = new DictionaryImportFileInfoDto
			{
				FileName = fileName,
				ValueColumnName = "Значение",
				CalcValueColumnName = "Метка"
			};
			var dictionary = new DictionaryBuilder().Build();


			ModelDictionaryService.UpdateDictionaryFromExcel(import, fileInfo, dictionary.Id, true);


			var marks = OMModelingDictionariesValues.Where(x => x.DictionaryId == dictionary.Id).SelectAll().Execute();
			Assert.That(marks.Count, Is.EqualTo(2));
			CheckMark(marks, "Метка_2", 2);
			CheckMark(marks, "Метка_3", 2.1m);
		}


		#region Support Methods

		private static void CheckMark(List<OMModelingDictionariesValues> marks, string value, decimal calculationValue)
		{
			var mark = marks.FirstOrDefault(x => x.Value == value);
			Assert.That(mark, Is.Not.Null);
			Assert.That(mark.CalculationValue, Is.EqualTo(calculationValue));
		}

		#endregion
	}
}