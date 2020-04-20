using System;
using System.Collections.Generic;
using System.Linq;
using Core.ErrorManagment;
using Core.Register.QuerySubsystem;
using Core.Shared.Extensions;
using KadOzenka.Dal.Enum;
using KadOzenka.Dal.ExpressScore.Dto;
using ObjectModel.Directory;
using ObjectModel.ES;
using ObjectModel.KO;
using ObjectModel.Market;

namespace KadOzenka.Dal.ExpressScore
{
	public class ExpressScoreService
	{
		private List<long> GetUnitsIdsByKn(string kn)
		{
			//TODO Маг числа: 2018 - ид тура в котором ищем
			return OMUnit.Where(x => x.CadastralNumber == kn && x.TourId == 2018)
				.Select(x => x.Id).Execute().Select(x => x.Id).ToList();
		}

		public EstimatedDto GetEstimateParametersByKn(string kn)
		{
			var unitsIds = GetUnitsIdsByKn(kn);
			return GetEstimateParameters(unitsIds);
		}

		public EstimatedDto GetEstimateParametersById(int id)
		{
			return GetEstimateParameters(new List<long>{id});
		}

		private EstimatedDto GetEstimateParameters(List<long> ids)
		{
			QSQuery query = GetQsQuery(OMUnitParamsOks2018.GetRegisterId(), 25000100, ids);
			query.AddColumn(new QSColumnSimple(25018500, nameof(EstimatedDto.WallMaterial)));
			query.AddColumn(new QSColumnSimple(25018800, nameof(EstimatedDto.DistanceToMetro)));
			query.AddColumn(new QSColumnSimple(25018900, nameof(EstimatedDto.DistanceToHistoryCityCenter)));
			query.AddColumn(new QSColumnSimple(25018700, nameof(EstimatedDto.DistanceToHighway)));
			query.AddColumn(new QSColumnSimple(25018600, nameof(EstimatedDto.IndustrialZone)));
			query.AddColumn(new QSColumnSimple(25019100, nameof(EstimatedDto.CoefficientTerritoryValue)));
			query.AddColumn(new QSColumnSimple(25021600, nameof(EstimatedDto.Floor)));
			return query.ExecuteQuery<EstimatedDto>().OrderByDescending(x => x.Id).FirstOrDefault();
		}


		public string GetSearchParamForNearestObject(string address, decimal square, out OMYearConstruction yearRange, out OMSquare squareRange, out int targetObjectId)
		{
			yearRange = null;
			squareRange = null;
			targetObjectId = 0;

			var yandexAddress = OMYandexAddress.Where(x => x.FormalizedAddress.Contains(address)).Select(x => x.CadastralNumber).ExecuteFirstOrDefault();

			if (yandexAddress == null)
			{
				return "Адрес для объекта не найден";
			}

			//TODO Маг числа: 25000100 - ид атрибута ID для KO_UNIT_PARAMS_OKS_2018, 25018400 - ид атрибута год постройки для KO_UNIT_PARAMS_OKS_2018
			// в двльнейшем будет какой то GUI для настройки этих параметров

			var unitsIds = GetUnitsIdsByKn(yandexAddress.CadastralNumber);

			QSQuery query = GetQsQuery(OMUnitParamsOks2018.GetRegisterId(), 25000100, unitsIds);
			query.AddColumn(new QSColumnSimple(25018400, nameof(YearDto.Year)));
			List<YearDto> years = query.ExecuteQuery<YearDto>().Where(x => x.Year.HasValue).OrderByDescending(x => x.Id).ToList();

			if (years.Count == 0)
			{
				return "Не найдены данные для выбраного объекта";
			}

			var year = years[0].Year;
			targetObjectId = years[0].Id;

			yearRange = OMYearConstruction.Where(x => x.YearFrom < year.Value && year.Value < x.YearTo)
				.SelectAll().ExecuteFirstOrDefault();

			squareRange = OMSquare.Where(x => x.SquareFrom <= square && square < x.SquareTo).SelectAll()
				.ExecuteFirstOrDefault();

			return "";
		}

		private QSQuery GetQsQuery(int registerId,int filterId, List<long> filterValues)
		{
			return new QSQuery
			{
				MainRegisterID = registerId,
				Condition = new QSConditionSimple
				{
					ConditionType = QSConditionType.In,
					LeftOperand = new QSColumnSimple(filterId),
					RightOperand = new QSColumnConstant(filterValues)
				}.And(new QSConditionSimple
				{
					ConditionType = QSConditionType.IsNotNull,
					LeftOperand = new QSColumnSimple(25018400) // год постройки
				}).And(new QSConditionSimple
					{
						ConditionType = QSConditionType.IsNotNull,
						LeftOperand = new QSColumnSimple(25018500) // материал стен
					}).And(new QSConditionSimple
					{
						ConditionType = QSConditionType.IsNotNull,
						LeftOperand = new QSColumnSimple(25018600) // нахождение в пром зоне
					}).And(new QSConditionSimple
					{
						ConditionType = QSConditionType.IsNotNull,
						LeftOperand = new QSColumnSimple(25018700) // расстояние до магистрали
					}).And(new QSConditionSimple
					{
						ConditionType = QSConditionType.IsNotNull,
						LeftOperand = new QSColumnSimple(25018800) // расстояние до метро
					}).And(new QSConditionSimple
					{
						ConditionType = QSConditionType.IsNotNull,
						LeftOperand = new QSColumnSimple(25018900) // расстояние до исторического центра
					}).And(new QSConditionSimple
				{
					ConditionType = QSConditionType.IsNotNull,
					LeftOperand = new QSColumnSimple(25019100) // коэф ценности территории
				})
			};
		}

		
		public List<CoordinatesDto> GetNearestCoordinates(Dictionary<long, CoordinatesDto> coordinates, Dictionary<long, double> distances, int quantity)
		{
			List<KeyValuePair<long, double>> myList = distances.ToList();

			myList.Sort((pair1, pair2) => pair1.Value.CompareTo(pair2.Value));

			var ids = myList.Take(quantity).Select(x => x.Key);

			return coordinates.Where(x => ids.Contains(x.Key)).Select(x => x.Value).ToList();
		}

		/// <summary>
		/// Считаем длинну дуги зная широту и долготу исходной точки
		/// </summary>
		/// <returns>
		/// возвращаем нужное количество точек в радиусе 1 км или 3 ближайшие точки
		/// </returns>
		public List<CoordinatesDto> GetCoordinatesPointAtSelectedDistance(Dictionary<long, CoordinatesDto> coordinates, decimal selectedLat, decimal selectedLng, int quantity)
		{
			var selectedCoordinates = new Dictionary<long, CoordinatesDto>();
			var distances = new Dictionary<long, double>();
			int r = 6371; //км — средний радиус земного шара.
			int searchRad = 1; //км - радиус поиска объектов;
			int reservedCount = 3; //Количество объектов которое показываем если не нашли в пределах нужного радиуса

			foreach (var item in coordinates.Where(x => x.Value.Lng != selectedLng && x.Value.Lat != selectedLat))
			{
				// 1 градус = 0.0174533 рад
				var coordinate = item.Value;
				var lat = (double)coordinate.Lat * 0.0174533;
				var lng = (double)coordinate.Lng * 0.0174533;
				var sLat = (double)selectedLat * 0.0174533;
				var sLng = (double)selectedLng * 0.0174533;

				var delLng = Math.Pow(Math.Sin((lng - sLng) / 2),2);
				var delLat = Math.Pow(Math.Sin((lat - sLat) / 2),2);

				var d1 = 2 *r * Math.Asin(Math.Sqrt(delLat + Math.Cos(lat)* Math.Cos(sLat)* delLng)); // км - расстояние от исходной точки до одного из найденных объектов

				distances.Add(item.Key, d1);
				if (d1 <= searchRad)
				{
					selectedCoordinates.Add(item.Key, item.Value);
				}
			}
			if (selectedCoordinates.Count > 0 && selectedCoordinates.Count <= quantity)
			{
				return selectedCoordinates.Values.ToList();
			}

			if (selectedCoordinates.Count == 0)
			{
				return  GetNearestCoordinates(coordinates, distances, reservedCount);
			}

			if (selectedCoordinates.Count > quantity)
			{
				return GetNearestCoordinates(coordinates, distances, quantity);
			}

			return new List<CoordinatesDto>();
		}

		public List<AnalogDto> GetAnalogsByIds(List<int> ids)
		{
			return OMCoreObject.Where(x => ids.Contains((int)x.Id))
				.Select(x => new
				{
					x.Id,
					x.CadastralNumber,
					x.Price,
					x.Area,
					x.LastDateUpdate,
					x.FloorsCount,
					x.FloorNumber
				}).Execute().Select(x => new AnalogDto
				{
					Id = x.Id,
					Kn = x.CadastralNumber,
					Price = x.Price.GetValueOrDefault(),
					Square = x.Area.GetValueOrDefault(),
					LastDateUpdate = x.LastDateUpdate ?? DateTime.MinValue,
					FloorsCount = x.FloorsCount.GetValueOrDefault(),
					Floor = x.FloorNumber.GetValueOrDefault()
				}).ToList();
		}

		public string CalculateExpressScore(List<AnalogDto> analogs, int targetObjectId, int targetObjectFloor, decimal targetObjectSquare, out decimal squareCost, out decimal summaryCost)
		{
			squareCost = 0;
			summaryCost = 0;
			squareCost = CalculateSquareCost(analogs, targetObjectId, targetObjectFloor, out string msg, out List<long> successAnalogIds);

			summaryCost = squareCost * targetObjectSquare;
			if (squareCost == 0)
			{
				return string.IsNullOrEmpty(msg) ? "При расчете что то пошло не так" : msg;
			}

			msg = SaveSuccessExpressScore(targetObjectId, summaryCost, squareCost, out int id, square: targetObjectSquare, floor: targetObjectFloor);
			if (!string.IsNullOrEmpty(msg)) return msg;

			msg = AddDependenceEsFromMarketCoreObject(id, successAnalogIds);

			return msg;
		}

		public string RemoveAnalogAndRecalculateExpressScore(List<AnalogDto> analogs, int removeAnalogId,
			int targetObjectId, int targetObjectFloor, decimal square, int expressScoreId, out List<long> successAnalogIds)
		{
			successAnalogIds = new List<long>();
			if (analogs.Count <= 1)
			{
				return "Нельзя удалять последний анлог";
			}
			var squareCost = CalculateSquareCost(analogs.Where(x => (int)x.Id != removeAnalogId).ToList(),
				targetObjectId, targetObjectFloor, out string msg, out successAnalogIds);
			if (!string.IsNullOrEmpty(msg)) return msg;

			msg = SaveSuccessExpressScore(targetObjectId, squareCost * square, squareCost, out int id, expressScoreId);
			if (!string.IsNullOrEmpty(msg)) return msg;

			msg = RemoveDependenceEsFromMarketCoreObject(id, analogs.Select(x => x.Id).ToList());
			if (!string.IsNullOrEmpty(msg)) return msg;

			msg = AddDependenceEsFromMarketCoreObject(id, successAnalogIds);

			return msg;
		}


		private decimal CalculateSquareCost(List<AnalogDto> analogs, int targetObjectId, int targetObjectFloor, out string msg, out List<long> successAnalogIds)
		{
			msg = "";

			const double cTorg = 0.9231;
			List<decimal> res = new List<decimal>();
			successAnalogIds = new List<long>();

			var estimatedParametersTargetObject = GetEstimateParametersById(targetObjectId);

			if (estimatedParametersTargetObject == null)
			{
				 msg = "Не найденны данные для целевого объекта";
				 return 0;
			}

			foreach (var analog in analogs)
			{
				decimal yPrice = 0; // Удельный показатель стоимости


				if (analog.Price != 0 && analog.Square != 0)
				{
					yPrice = analog.Price / analog.Square;
				}

				//Корректировка на дату 
				var dateEstimate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);

				var indexDateEstimate = OMIdexDate.Where(x => x.Date == dateEstimate).SelectAll().ExecuteFirstOrDefault();
				if (indexDateEstimate == null)
				{
					indexDateEstimate = OMIdexDate.Where(x => x).SelectAll().Execute().OrderByDescending(x => x.Date).First();
				}

				OMIdexDate indexAnalogDate = null;
				if (analog.LastDateUpdate != DateTime.MinValue)
				{
					var analogDate = new DateTime(analog.LastDateUpdate.Year, analog.LastDateUpdate.Month,
						1);

					indexAnalogDate = OMIdexDate.Where(x => x.Date == analogDate).SelectAll().ExecuteFirstOrDefault();
				}

				if (indexAnalogDate == null)
				{
					continue;
				}

				decimal kDate = indexAnalogDate.Index / indexDateEstimate.Index; // Корректировка на дату

				var cost = kDate * yPrice;
				cost = cost * (decimal)cTorg;

				OMLandShare landShare = null;
				if (analog.FloorsCount != 0)
				{
					landShare = OMLandShare.Where(x => x.Floor == analog.FloorsCount && x.SegmentType_Code == MarketSegment.Office).SelectAll().ExecuteFirstOrDefault();
					if (landShare == null)
					{
						var tmpLandShares = OMLandShare.Where(x => x.SegmentType_Code == MarketSegment.Office)
							.SelectAll().Execute().OrderByDescending(x => x.Floor).ToList();
						landShare = tmpLandShares.Count > 1 ? tmpLandShares[1] : null;
						if (tmpLandShares.Count > 0 && tmpLandShares[1].Floor < analog.FloorsCount)
						{
							landShare = tmpLandShares[0];

						}
					}
				}

				if (landShare != null)
				{
					cost = cost * landShare.Factor;
				}


				var estimatedParameters = GetEstimateParametersByKn(analog.Kn);

				// Начинаются оценочные факторы
				var wallMaterial = OMWallMaterial.Where(x => x.WallMaterial.Contains(estimatedParameters.WallMaterial)).SelectAll()
					.ExecuteFirstOrDefault();

				var wallMaterialTargetObject = OMWallMaterial.Where(x => x.WallMaterial.Contains(estimatedParametersTargetObject.WallMaterial)).SelectAll()
					.ExecuteFirstOrDefault();

				if (wallMaterial != null && wallMaterialTargetObject != null)
				{
					var costFactor = OMCostFactor.Where(x => x.Id == 1).SelectAll().ExecuteFirstOrDefault();
					var factor = Math.Exp((double)(wallMaterialTargetObject.Mark * costFactor.Factor)) / Math.Exp((double)(wallMaterial.Mark * costFactor.Factor));
					cost = cost * (decimal)factor;
				}

				{
					var costFactor = OMCostFactor.Where(x => x.Id == 2).SelectAll().ExecuteFirstOrDefault();
					var factor = Math.Exp((double)(estimatedParametersTargetObject.DistanceToMetro * costFactor.Factor))
								 / Math.Exp((double)(estimatedParameters.DistanceToMetro * costFactor.Factor));
					cost = cost * (decimal)factor;
				}

				{
					var costFactor = OMCostFactor.Where(x => x.Id == 3).SelectAll().ExecuteFirstOrDefault();
					var factor = Math.Exp((double)(estimatedParametersTargetObject.DistanceToHistoryCityCenter * costFactor.Factor))
								 / Math.Exp((double)(estimatedParameters.DistanceToHistoryCityCenter * costFactor.Factor));
					cost = cost * (decimal)factor;
				}

				{
					var costFactor = OMCostFactor.Where(x => x.Id == 4).SelectAll().ExecuteFirstOrDefault();
					decimal distA = estimatedParametersTargetObject.DistanceToHighway > 500
						? 500
						: estimatedParametersTargetObject.DistanceToHighway; // нормируем расстояние по условию

					decimal distB = estimatedParameters.DistanceToHighway > 500
						? 500
						: estimatedParameters.DistanceToHighway; // нормируем расстояние по условию

					var factor = Math.Exp((double)(distA * costFactor.Factor)) / Math.Exp((double)(distB * costFactor.Factor));
					cost = cost * (decimal)factor;

				}

				{
					var costFactor = OMCostFactor.Where(x => x.Id == 5).SelectAll().ExecuteFirstOrDefault();
					var isIndustrialZoneTargetObject =
						estimatedParametersTargetObject.IndustrialZone == IndustrialZoneEnum.Yes.GetEnumDescription()
							? 1
							: 0;

					var isIndustrialZone =
						estimatedParameters.IndustrialZone == IndustrialZoneEnum.Yes.GetEnumDescription()
							? 1
							: 0;

					var factor = Math.Exp((double)(isIndustrialZoneTargetObject * costFactor.Factor)) / Math.Exp((double)(isIndustrialZone * costFactor.Factor));
					cost = cost * (decimal)factor;
				}

				{
					var costFactor = OMCostFactor.Where(x => x.Id == 6).SelectAll().ExecuteFirstOrDefault();
					var factor = Math.Exp((double)(estimatedParametersTargetObject.CoefficientTerritoryValue * costFactor.Factor))
								 / Math.Exp((double)(estimatedParameters.CoefficientTerritoryValue * costFactor.Factor));
					cost = cost * (decimal)factor;
				}

				{
					var floor = (int)analog.Floor;
					var floors = OMFloor.Where(x => x).SelectAll().Execute().OrderByDescending(x => x.Floor).ToList();

					var floorFactor = floor != 0
						? floor > floors[0].Floor ? floors[0].Factor :
						floors.FirstOrDefault(x => x.Floor == floor)?.Factor
						: 0;

					var targetObjectFloorFactor =
						targetObjectFloor > floors[0].Floor ? floors[0]?.Factor :
						floors.FirstOrDefault(x => x.Floor == targetObjectFloor)?.Factor;

					if (floorFactor != null && floorFactor != 0 && targetObjectFloorFactor != null)
					{
						var factor = targetObjectFloorFactor / floorFactor;
						cost = cost * (decimal)factor;
					}

				}
				res.Add(Math.Round(cost, 2));
				successAnalogIds.Add(analog.Id);

			}

			if (res.Count == 0)
			{
				msg = "Не один аналог не подошел для расчета.";
				return 0;
			}
			return res.Sum(x => x) / res.Count;

		}

		public string SaveSuccessExpressScore(int targetObjectId, decimal summaryCost, decimal costSquareMeter, out int id, int? expressScoreId = null,
			decimal? square = null, int? floor = null)
		{
			id = 0;
			try
			{
				var kn = OMUnit.Where(x => x.Id == targetObjectId).Select(x => x.CadastralNumber).ExecuteFirstOrDefault()
					?.CadastralNumber;

				if (expressScoreId == null && (square == null || floor == null))
				{
					return "Невозможно выполнить сохранение. Не задана площадь или этаж";
				}

				id = expressScoreId == null
					? AddExpressScore(kn, summaryCost, costSquareMeter, square.Value, floor.Value, targetObjectId)
					:  UpdateCostsExpressScore(expressScoreId.Value, summaryCost, costSquareMeter);
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				ErrorManager.LogError(e);
				return "Сохранение результатов оценки не выполненно. Подробнее в журнале ошибок";
			}

			return "";
		}

		private int AddExpressScore(string kn, decimal cost, decimal costSquareMeter, decimal square, int floor, int targetObjectId)
		{
			return new OMExpressScore
			{
				KadastralNumber = kn,
				CostSquareMeter = costSquareMeter,
				DateCost = DateTime.Now.Date,
				SummaryCost = cost,
				Objectid = targetObjectId,
				Floor = floor,
				Square = square
			}.Save();
		}

		private int UpdateCostsExpressScore(int expressScoreId, decimal summaryCost, decimal costSquareMeter)
		{
			var obj = OMExpressScore.Where(x => x.Id == expressScoreId).SelectAll().ExecuteFirstOrDefault();
			if (obj == null)
			{
				return 0;
			}

			obj.CostSquareMeter = costSquareMeter;
			obj.SummaryCost = summaryCost;
			return obj.Save();
		}

		private string AddDependenceEsFromMarketCoreObject(int expressScoreId, List<long> objectIds)
		{
			try
			{
				foreach (var objId in objectIds)
				{
					new OMEsToMarketCoreObject
					{
						EsId = expressScoreId,
						MarketObjectId = objId
					}.Save();
				}

			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				ErrorManager.LogError(e);
				return "Добавление связи экспресс оценки и аналогв не выполненно. Подробнее в журнале ошибок";
			}
			return "";
		}

		private string RemoveDependenceEsFromMarketCoreObject(int expressScoreId, List<long> objectIds)
		{
			try
			{
				foreach (var objId in objectIds)
				{
					 OMEsToMarketCoreObject.Where(x => x.EsId == expressScoreId && x.MarketObjectId == objId).ExecuteFirstOrDefault().Destroy();
				}

			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				ErrorManager.LogError(e);
				return "Удаление связи экспресс оценки и аналогв не выполненно. Подробнее в журнале ошибок";
			}
			return "";
		}

		public long AddWallMaterial(string wallMaterial, long mark)
	    {
	        return new OMWallMaterial {WallMaterial = wallMaterial, Mark = mark}.Save();
	    }

	    public long UpdateEWallMaterial(long id, string wallMaterial, long mark)
	    {
	        var entity = OMWallMaterial.Where(x => x.Id == id).SelectAll().ExecuteFirstOrDefault();
	        if (entity == null)
	        {
	            throw new Exception($"Не найден материал стен с ИД {id}");
	        }

	        entity.WallMaterial = wallMaterial;
	        entity.Mark = mark;
	        entity.Save();

	        return entity.Id;
	    }
	}
}