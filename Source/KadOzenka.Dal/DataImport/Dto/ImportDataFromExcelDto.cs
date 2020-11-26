using System;
using System.Collections.Generic;
using KadOzenka.Dal.DataImport.DataImportKoFactory;
using KadOzenka.Dal.DataImport.DataImportKoFactory.Interface;
using KadOzenka.Dal.Tours;
using ObjectModel.Directory;

namespace KadOzenka.Dal.DataImport.Dto
{
	public enum LoadType
	{
		/// <summary>
		/// Не определена
		/// </summary>
		None = 0,
		/// <summary>
		/// Оценочная группа
		/// </summary>
		Group = 1,
		/// <summary>
		/// Предварительная стоимость
		/// </summary>
		PreCost = 2,
		/// <summary>
		/// Окончательная стоимость
		/// </summary>
		EndCost = 3,
	}
	public class ImportDataFromExcelDto
	{
		public string FileName { get; set; }
		public long? TourId { get; set; }
		public bool IsUnitStatusUsed { get; set; }
		public KoUnitStatus? UnitStatus { get; set; }
		public List<long> TaskFilter { get; set; }
		public string RegisterViewId { get; set; }
		public int MainRegisterId { get; set; }
		public LoadType LoadType { get; set; }

	}
}
