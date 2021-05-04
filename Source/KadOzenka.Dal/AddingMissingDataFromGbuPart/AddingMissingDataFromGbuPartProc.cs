using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Core.Shared.Extensions;
using KadOzenka.Dal.GbuObject;
using KadOzenka.Dal.Logger;
using KadOzenka.Dal.Registers.GbuRegistersServices;
using MarketPlaceBusiness;
using ObjectModel.Directory;
using ObjectModel.Gbu;
using ObjectModel.Market;

namespace KadOzenka.Dal.AddingMissingDataFromGbuPart
{
	/// <summary>
	/// Получение дополнительных данных из ГБУ части (используется в KadOzenka.BlFrontEnd)
	/// По КН находит ОН, из него забирает Год постройки и Материал стен и присваивает их Аналогу
	/// </summary>
	public class AddingMissingDataFromGbuPartProc
	{
		private GbuObjectService GbuObjectService { get; }
		private RosreestrRegisterService RosreestrRegisterService { get; }
		public IMarketObjectService MarketObjectService { get; set; }

		public AddingMissingDataFromGbuPartProc()
		{
			GbuObjectService = new GbuObjectService();
			RosreestrRegisterService = new RosreestrRegisterService();
			MarketObjectService = new MarketObjectService();
		}

		public void PerformProc(bool fillInitialObjects = true)
		{
			ConsoleLog.WriteFotter("Получение дополнительных данных из ГБУ части для объектов аналогов");
			var buildYearAttributeId = RosreestrRegisterService.GetBuildYearAttribute().Id;
			var wallMaterialAttributeId = RosreestrRegisterService.GetWallMaterialAttribute().Id;
			var coreObjects = fillInitialObjects ? GetInitialObjects() : GetExistingObjects();
			Console.WriteLine($"Выбрано объектов: {coreObjects.Count}");

			int totalCount = coreObjects.Count, currentCount = 0, correctCount = 0, updateCount = 0, errorCount = 0;
			foreach (var omCoreObject in coreObjects)
			{
				try
				{
					var gbuObject = OMMainObject.Where(x => x.CadastralNumber == omCoreObject.CadastralNumber)
						.ExecuteFirstOrDefault();
					if (gbuObject != null)
					{
						var attributesValues = GbuObjectService.GetAllAttributes(gbuObject.Id,
							new List<long> { RosreestrRegisterService.RegisterId },
							new List<long>
							{
								buildYearAttributeId,
								wallMaterialAttributeId
							},
							DateTime.Now.GetEndOfTheDay());

						var buildingYearWasUpdated = FillBuildingYearData(omCoreObject,
							attributesValues.FirstOrDefault(x => x.AttributeId == buildYearAttributeId));
						var wallMaterialWasUpdated = FillWallMaterialData(omCoreObject,
							attributesValues.FirstOrDefault(x => x.AttributeId == wallMaterialAttributeId));

						if (buildingYearWasUpdated || wallMaterialWasUpdated)
						{
							omCoreObject.Save();
							updateCount++;
						}
					}
					correctCount++;
				}
				catch (Exception ex)
				{
					Console.WriteLine($"\nНе удалось обработать объект '{omCoreObject.Id}':{ex.Message}, {ex.StackTrace}");
					errorCount++;
				}
				currentCount++;
				ConsoleLog.WriteData("Обработка объектов", totalCount, currentCount, correctCount, errorCount, updated: updateCount);
			}

			ConsoleLog.WriteFotter("Получение дополнительных данных из ГБУ части для объектов аналогов завершено");
		}

		private List<OMCoreObject> GetInitialObjects()
		{
			Expression<Func<OMCoreObject, bool>> whereExpression = x =>
				x.LastDateUpdate == null && (x.BuildingYear == null || x.WallMaterial == null);
			
			Expression<Func<OMCoreObject, object>> selectExpression = x => new
			{
				x.BuildingYear,
				x.WallMaterial,
				x.CadastralNumber
			};

			return MarketObjectService.GetObjectsByCondition(whereExpression, selectExpression);
		}

		private List<OMCoreObject> GetExistingObjects()
		{
			Expression<Func<OMCoreObject, bool>> whereExpression = x =>
				(x.ProcessType_Code == ProcessStep.InProcess || x.ProcessType_Code == ProcessStep.Dealed) &&
				(x.BuildingYear == null || x.WallMaterial == null);

			Expression<Func<OMCoreObject, object>> selectExpression = x => new
			{
				x.BuildingYear,
				x.WallMaterial,
				x.CadastralNumber
			};

			return MarketObjectService.GetObjectsByCondition(whereExpression, selectExpression);
		}

		private bool FillBuildingYearData(OMCoreObject omCoreObject, GbuObjectAttribute attributeValue)
		{
			if (!omCoreObject.BuildingYear.HasValue)
			{
				if (long.TryParse(attributeValue?.GetValueInString(), out var year))
				{
					omCoreObject.BuildingYear = year;
					return true;
				}
			}

			return false;
		}

		private bool FillWallMaterialData(OMCoreObject omCoreObject, GbuObjectAttribute attributeValue)
		{
			if (string.IsNullOrEmpty(omCoreObject.WallMaterial))
			{
				var attrValue = attributeValue?.GetValueInString()?.Replace(";", ",");
				var enumValue = EnumExtensions.GetEnumByDescription<WallMaterial>(attrValue);
				if (enumValue != 0)
				{
					omCoreObject.WallMaterial_Code = (WallMaterial)enumValue;
					return true;
				}
			}

			return false;
		}
	}
}
