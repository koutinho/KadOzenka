using System;
using System.Collections.Generic;
using System.Linq;
using Core.ErrorManagment;
using Core.Register;
using Core.Register.QuerySubsystem;
using Core.Shared.Extensions;
using KadOzenka.Dal.ExpressScore.Dto;
using KadOzenka.Dal.ScoreCommon;
using KadOzenka.Dal.ScoreCommon.Dto;
using ObjectModel.Directory;
using ObjectModel.Directory.ES;
using ObjectModel.Es;
using ObjectModel.ES;
using ObjectModel.KO;
using ObjectModel.Market;

namespace KadOzenka.Dal.ExpressScore
{
	public class ExpressScoreService
	{
		public ScoreCommonService ScoreCommonService { get; set; }

		public ExpressScoreService(ScoreCommonService scoreCommonService)
		{
			ScoreCommonService = scoreCommonService;
		}


		public OMSettingsParams GetSetting(MarketSegment segmentType)
		{
			return OMSettingsParams.Where(x => x.SegmentType_Code == segmentType).SelectAll().ExecuteFirstOrDefault();
		}

		public ParameterDataDto GetEstimateParametersByKn(string kn, int tourId, int attributeId, MarketSegment segmentType, int registerId)
		{
			var unitsIds = ScoreCommonService.GetUnitsIdsByCadastralNumber(kn, tourId);
			var qsGroup = GetQsConditionForCostFactors(segmentType);
			return unitsIds.Count > 0 ? ScoreCommonService.GetParameters(unitsIds, attributeId, registerId, qsGroup) : null;
		}

		public ParameterDataDto GetEstimateParametersById(int id, int attributeId, MarketSegment segmentType, int registerId)
		{
			var qsGroup = GetQsConditionForCostFactors(segmentType);
			return ScoreCommonService.GetParameters(new List<long> { id }, attributeId, registerId, qsGroup);
		}


		public string GetSearchParamForNearestObject(string address, decimal square, MarketSegment segmentType, out OMYearConstruction yearRange, out OMSquare squareRange, out int targetObjectId)
		{
			yearRange = null;
			squareRange = null;
			targetObjectId = 0;

			var yandexAddress = OMYandexAddress.Where(x => x.FormalizedAddress.Contains(address)).Select(x => x.CadastralNumber).ExecuteFirstOrDefault();

			if (yandexAddress == null)
			{
				return "Адрес для объекта не найден";
			}

			var setting = OMSettingsParams.Where(x => x.SegmentType_Code == segmentType).SelectAll().ExecuteFirstOrDefault();

			if (setting == null)
			{
				return "Не найдены настройки для выбрвного сегмента";
			}

			var unitsIds = ScoreCommonService.GetUnitsIdsByCadastralNumber(yandexAddress.CadastralNumber, (int)setting.TourId);
			if (unitsIds.Count == 0)
			{
				return "Для выбранного тура и объекта в Ко части нет данных";
			}

			var idAttribute = RegisterCache.RegisterAttributes.Values.FirstOrDefault(x => x.RegisterId == setting.Registerid && x.IsPrimaryKey)?.Id;

			var costFactor = setting.CostFacrors.DeserializeFromXml<CostFactorsDto>();

			if (costFactor.YearBuildId == null && costFactor.YearBuildId == 0)
			{
				return "В настройках не задан атрибут для года постройки.";
			}

			QSQuery query;
			try
			{
				query = ScoreCommonService.GetQsQuery((int)setting.Registerid, (int)idAttribute.GetValueOrDefault(), unitsIds, GetQsConditionForCostFactors(segmentType));
			}
			catch (Exception e)
			{
				ErrorManager.LogError(e);
				Console.WriteLine(e);
				throw;
			}
		
			query.AddColumn(new QSColumnSimple((int)costFactor.YearBuildId.GetValueOrDefault(), nameof(YearDto.Year)));
			List<YearDto> years = query.ExecuteQuery<YearDto>().Where(x => x.Year.HasValue).OrderByDescending(x => x.Id).ToList();

			if (years.Count == 0)
			{
				return "Не найдены данные для выбраного объекта";
			}

			var year = years[0].Year;
			targetObjectId = years[0].Id;

			yearRange = OMYearConstruction.Where(x => x.YearFrom <= year.Value && year.Value <= x.YearTo)
				.SelectAll().ExecuteFirstOrDefault();

			squareRange = OMSquare.Where(x => x.SquareFrom <= square && square <= x.SquareTo).SelectAll()
				.ExecuteFirstOrDefault();

			return "";
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
					x.ParserTime,
					x.LastDateUpdate,
					x.FloorsCount,
					x.FloorNumber
				}).Execute().Select(x => new AnalogDto
				{
					Id = x.Id,
					Kn = x.CadastralNumber,
					Price = x.Price.GetValueOrDefault(),
					Square = x.Area.GetValueOrDefault(),
					Date = x.LastDateUpdate ?? x.ParserTime ?? DateTime.MinValue,
					FloorsCount = x.FloorsCount.GetValueOrDefault(),
					Floor = x.FloorNumber.GetValueOrDefault()
				}).ToList();
		}

		public string CalculateExpressScore(List<AnalogDto> analogs, int targetObjectId, int targetObjectFloor, decimal targetObjectSquare, 
			out ResultCalculateDto resultCalculate, ScenarioType scenarioType, MarketSegment marketSegment)
		{
			resultCalculate = new ResultCalculateDto();
			var squareCost = CalculateSquareCost(analogs, targetObjectId, targetObjectFloor, marketSegment, out string msg, out List<long> successAnalogIds, scenarioType);

			var summaryCost = Math.Round(squareCost * targetObjectSquare, 2);
			if (squareCost == 0)
			{
				return string.IsNullOrEmpty(msg) ? "При расчете что то пошло не так" : msg;
			}

			msg = SaveSuccessExpressScore(targetObjectId, summaryCost, squareCost, out int id, square: targetObjectSquare, floor: targetObjectFloor, scenarioType: scenarioType, segmentType: marketSegment);
			if (!string.IsNullOrEmpty(msg)) return msg;

			msg = AddDependenceEsFromMarketCoreObject(id, successAnalogIds);

			resultCalculate.SquareCost = Math.Round(squareCost, 2);
			resultCalculate.SummaryCost = summaryCost;
			resultCalculate.Id = id;

			var resultAnalogs = OMCoreObject.Where(x => successAnalogIds.Contains(x.Id)).SelectAll().Execute().Select(x => new AnalogResultDto
			{
				Id = x.Id,
				Address = x.Address,
				Floor = x.FloorNumber.GetValueOrDefault(),
				Source = x.Market,
				Square = x.Area.GetValueOrDefault(),
				Price = x.Price.GetValueOrDefault(),
			}).ToList();

			resultCalculate.Analogs = resultAnalogs;

			return msg;
		}

		public string RecalculateExpressScore(List<AnalogDto> analogs, List<int> analogIds,
			int targetObjectId, int targetObjectFloor, decimal square, int expressScoreId, ScenarioType scenarioType, MarketSegment marketSegment, out decimal cost, out decimal squareCost)
		{
			cost = 0;
			squareCost = 0;

			squareCost = CalculateSquareCost(analogs.Where(x => analogIds.Contains((int)x.Id)).ToList(),
				targetObjectId, targetObjectFloor, marketSegment, out string msg, out var successAnalogIds, scenarioType);
			if (!string.IsNullOrEmpty(msg)) return msg;

			cost = Math.Round(squareCost * square, 2);
			squareCost = Math.Round(squareCost, 2);

			msg = SaveSuccessExpressScore(targetObjectId, cost, squareCost, out int id, expressScoreId);
			if (!string.IsNullOrEmpty(msg)) return msg;

			return msg;
		}


		private decimal CalculateSquareCost(List<AnalogDto> analogs, int targetObjectId, int targetObjectFloor,
			MarketSegment marketSegment, out string msg, out List<long> successAnalogIds,
			ScenarioType? scenarioType = null)
		{
			msg = "";
			List<decimal> res = new List<decimal>();
			successAnalogIds = new List<long>();

			var exSettingsCostFactors = OMSettingsParams.Where(x => x.SegmentType_Code == marketSegment).SelectAll()
				.ExecuteFirstOrDefault();

			CostFactorsDto exCostFactors;

			if (exSettingsCostFactors == null)
			{
				msg = "Не найденны настройки для выбраного сегмента";
				return 0;
			}

			try
			{
				exCostFactors = exSettingsCostFactors.CostFacrors.DeserializeFromXml<CostFactorsDto>();
			}
			catch (Exception e)
			{
				ErrorManager.LogError(e);
				Console.WriteLine(e);
				msg = "Не найденны настройки для выбраного сегмента";
				return 0;
			}

			if (exCostFactors.LandShareDicId == null || exCostFactors.IndexDateDicId == null)
			{
				msg = "Не заданы обязательные настройки";
				return 0;
			}

			foreach (var analog in analogs)
			{
				decimal yPrice = 0; // Удельный показатель стоимости


				if (analog.Price != 0 && analog.Square != 0)
				{
					yPrice = analog.Price / analog.Square;
				}

				// Обязательные факторы

				#region Корректировка на дату

				//Корректировка на дату 
				var dateDict = OMEsReferenceItem.Where(x => x.ReferenceId == exCostFactors.IndexDateDicId).SelectAll().Execute()
					.Select(ScoreCommonService.ReferenceToDate).ToList();

				var dateEstimate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);

				var indexDateEstimate = dateDict.FirstOrDefault(x => x.Key == dateEstimate) ??
				                        dateDict.OrderByDescending(x => x.Key).First();

				DateReference indexAnalogDate = null;
				if (analog.Date != DateTime.MinValue)
				{
					var analogDate = new DateTime(analog.Date.Year, analog.Date.Month,
						1);

					indexAnalogDate = dateDict.FirstOrDefault(x => x.Key == analogDate);
				}

				if (indexAnalogDate == null)
				{
					// TODO Временное решение на время показов
					indexAnalogDate = dateDict.OrderByDescending(x => x.Key).First();
					//continue;
				}

				decimal kDate = indexAnalogDate.Value / indexDateEstimate.Value; // Корректировка на дату

				var cost = kDate * yPrice;

				#endregion

				#region Корректировка на долю ЗУ

				//Корректировка на долю ЗУ
				if (scenarioType != null && scenarioType == ScenarioType.Oks)
				{
					var dateNumb = OMEsReferenceItem.Where(x => x.ReferenceId == exCostFactors.LandShareDicId).SelectAll().Execute()
						.Select(ScoreCommonService.ReferenceToNumber).ToList();

					if (analog.FloorsCount != 0)
					{
						var coefficient = dateNumb.FirstOrDefault(x => x.Key == analog.FloorsCount)?.Value ??
						                  dateNumb.Last()?.Value ?? 1;

						cost = cost * coefficient;
					}

				}
				//end Корректировка на долю ЗУ

				#endregion

				#region Корректировка на этаж

				{
					var floor = (int) analog.Floor;
					var floors = OMEsReferenceItem.Where(x => x.ReferenceId == 44205175).SelectAll().Execute()
						.Select(ScoreCommonService.ReferenceToNumber).OrderByDescending(x => x.Value).ToList().ToList();

					var floorFactor = floor != 0
						? floor > floors[0].Key ? floors[0].Value :
						floors.FirstOrDefault(x => x.Key == floor)?.Value
						: 0;

					var targetObjectFloorFactor =
						targetObjectFloor > floors[0].Key
							? floors[0]?.Value
							: floors.FirstOrDefault(x => x.Key == targetObjectFloor)?.Value;

					if (floorFactor != null && floorFactor != 0 && targetObjectFloorFactor != null &&
					    targetObjectFloorFactor != 0)
					{
						var factor = targetObjectFloorFactor / floorFactor;
						cost = cost * (decimal) factor;
					}

				}

				#endregion


				foreach (var simple in exCostFactors.SimpleCostFactors)
				{
					if (simple.Coefficient != null)
					{
						cost = cost * simple.Coefficient.GetValueOrDefault();
					}
				}

				bool isBreak = false;
				foreach (var complex in exCostFactors.ComplexCostFactors)
				{
					var targetObjectFactor = GetEstimateParametersById(targetObjectId,
						complex.AttributeId.GetValueOrDefault(), marketSegment, (int) exSettingsCostFactors.Registerid);

					if (targetObjectFactor == null)
					{
						msg = "Не найденны данные для целевого объекта";
						return 0;
					}


					var analogFactor = GetEstimateParametersByKn(analog.Kn, (int) exSettingsCostFactors.TourId,
						complex.AttributeId.GetValueOrDefault(), marketSegment, (int) exSettingsCostFactors.Registerid);

					if (analogFactor == null)
					{
						isBreak = true;
						break;
					}

					switch (analogFactor.Type)
					{
						case ParameterType.String:
						{
							if (complex.DictionaryId != null && complex.DictionaryId != 0)
							{
								decimal analogC = ScoreCommonService.GetCoefficientFromStringFactor(analogFactor,
									complex.DictionaryId.GetValueOrDefault());
								decimal targetObjectC = ScoreCommonService.GetCoefficientFromStringFactor(targetObjectFactor,
									complex.DictionaryId.GetValueOrDefault());

								if (analogC == 0 || targetObjectC == 0) break;

								var coeff = Math.Exp((double) (targetObjectC * complex.Coefficient.GetValueOrDefault())) /
								            Math.Exp((double) (analogC * complex.Coefficient.GetValueOrDefault()));
								cost = cost * (decimal) coeff;
							}

							break;
						}
						case ParameterType.Date:
						{
							if (complex.DictionaryId != null && complex.DictionaryId != 0)
							{
								decimal analogC = ScoreCommonService.GetCoefficientFromDateFactor(analogFactor,
									complex.DictionaryId.GetValueOrDefault());
								decimal targetObjectC = ScoreCommonService.GetCoefficientFromDateFactor(targetObjectFactor,
									complex.DictionaryId.GetValueOrDefault());

								if (analogC == 0 || targetObjectC == 0) break;

								var coeff = Math.Exp((double)(targetObjectC * complex.Coefficient.GetValueOrDefault())) / Math.Exp((double)(analogC * complex.Coefficient.GetValueOrDefault()));
								cost = cost * (decimal) coeff;
							}

							break;
						}
						case ParameterType.Number:
						{
							decimal analogC = ScoreCommonService.GetCoefficientFromNumberFactor(analogFactor,
								complex.DictionaryId.GetValueOrDefault());
							decimal targetObjectC = ScoreCommonService.GetCoefficientFromNumberFactor(targetObjectFactor,
								complex.DictionaryId.GetValueOrDefault());

							if (analogC == 0 || targetObjectC == 0) break;

							var coeff = Math.Exp((double) (targetObjectC * complex.Coefficient.GetValueOrDefault())) /
							            Math.Exp((double) (analogC * complex.Coefficient.GetValueOrDefault()));
							cost = cost * (decimal) coeff;
							break;
						}
						default:
						{
							isBreak = true;
							break;
						}
					}
				}

				if (isBreak)
				{
						continue;
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
			decimal? square = null, int? floor = null, ScenarioType? scenarioType = null, MarketSegment? segmentType = null)
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
					? AddExpressScore(kn, summaryCost, costSquareMeter, square.Value, floor.Value, targetObjectId, scenarioType.GetValueOrDefault(), segmentType.GetValueOrDefault())
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

		private int AddExpressScore(string kn, decimal cost, decimal costSquareMeter, decimal square, int floor, int targetObjectId, ScenarioType scenarioType, MarketSegment segmentType)
		{
			return new OMExpressScore
			{
				KadastralNumber = kn,
				CostSquareMeter = costSquareMeter,
				DateCost = DateTime.Now.Date,
				SummaryCost = cost,
				Objectid = targetObjectId,
				Floor = floor,
				Square = square,
				ScenarioType_Code = scenarioType,
				SegmentType_Code = segmentType
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


		#region Wall material

		public long AddWallMaterial(string wallMaterial, long mark)
		{
			return new OMWallMaterial { WallMaterial = wallMaterial, Mark = mark }.Save();
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


		#endregion


		private CostFactorsDto GetCostFactorsBySegmentType(MarketSegment segmentType)
		{
			var setting = OMSettingsParams.Where(x => x.SegmentType_Code == segmentType).SelectAll().ExecuteFirstOrDefault();

			if (setting == null || string.IsNullOrEmpty(setting.CostFacrors))
			{
				return null;
			}
			return setting.CostFacrors.DeserializeFromXml<CostFactorsDto>();
		}

		private QSConditionGroup GetQsConditionForCostFactors(MarketSegment segmentType)
		{
			var costFactor = GetCostFactorsBySegmentType(segmentType);
			if (costFactor != null)
			{
				var complexFactors = costFactor.ComplexCostFactors;

				var qsCGroup = new QSConditionGroup(QSConditionGroupType.And);
				qsCGroup.Add(new QSConditionSimple
				{
					ConditionType = QSConditionType.IsNotNull,
					LeftOperand = new QSColumnSimple((long)costFactor.YearBuildId.GetValueOrDefault())
				});

				foreach (var factor in complexFactors)
				{
					qsCGroup.Add(new QSConditionSimple
					{
						ConditionType = QSConditionType.IsNotNull,
						LeftOperand = new QSColumnSimple(factor.AttributeId.GetValueOrDefault())
					});
				}

				return qsCGroup;

			}

			return new QSConditionGroup();
		}
	}
}