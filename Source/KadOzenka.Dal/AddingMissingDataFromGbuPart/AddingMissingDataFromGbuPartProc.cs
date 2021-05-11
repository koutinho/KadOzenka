using System;
using System.Collections.Generic;
using System.Linq;
using Core.Shared.Extensions;
using KadOzenka.Dal.GbuObject;
using KadOzenka.Dal.Logger;
using KadOzenka.Dal.Registers.GbuRegistersServices;
using MarketPlaceBusiness;
using MarketPlaceBusiness.Interfaces.Utils;
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
		public IMissingDataFromGbuService MarketObjectService { get; set; }

		public AddingMissingDataFromGbuPartProc()
		{
			GbuObjectService = new GbuObjectService();
			RosreestrRegisterService = new RosreestrRegisterService();
			MarketObjectService = new MarketObjectForUtilsService();
		}

		public void PerformProc(bool fillInitialObjects = true)
		{
			ConsoleLog.WriteFotter("Получение дополнительных данных из ГБУ части для объектов аналогов");
			var buildYearAttributeId = RosreestrRegisterService.GetBuildYearAttribute().Id;
			var wallMaterialAttributeId = RosreestrRegisterService.GetWallMaterialAttribute().Id;
			
			var coreObjects = fillInitialObjects
				? MarketObjectService.GetInitialObjects()
				: MarketObjectService.GetExistingObjects();
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

						var buildingYearAttribute = attributesValues.FirstOrDefault(x => x.AttributeId == buildYearAttributeId);
						var buildingYearWasUpdated = MarketObjectService.FillBuildingYearData(omCoreObject, buildingYearAttribute?.GetValueInString());
						
						var wallMaterialAttribute = attributesValues.FirstOrDefault(x => x.AttributeId == wallMaterialAttributeId);
						var wallMaterialWasUpdated = MarketObjectService.FillWallMaterialData(omCoreObject, wallMaterialAttribute?.GetValueInString());

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
	}
}
