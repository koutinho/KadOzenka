using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text.Json;
using Core.ErrorManagment;
using Core.Register;
using Core.Register.QuerySubsystem;
using Core.Shared.Extensions;
using Core.Shared.Misc;
using DevExpress.XtraPrinting.Native.WebClientUIControl;
using KadOzenka.Dal.Enum;
using KadOzenka.Dal.ExpressScore.Dto;
using KadOzenka.Dal.Registers;
using KadOzenka.Dal.ScoreCommon;
using KadOzenka.Dal.ScoreCommon.Dto;
using KadOzenka.Dal.Tours.Dto;
using KadOzenka.Dal.YandexParsing;
using Microsoft.Rest.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using ObjectModel.Directory;
using ObjectModel.Directory.ES;
using ObjectModel.Es;
using ObjectModel.ES;
using ObjectModel.KO;
using ObjectModel.Market;
using Serilog;

namespace KadOzenka.Dal.ExpressScore
{
	public class ExpressScoreService
	{

		private static readonly ILogger _log = Log.ForContext<ExpressScoreReportService>();

		public ScoreCommonService ScoreCommonService { get; set; }
		public RegisterAttributeService RegisterAttributeService { get; set; }
        private string DecimalFormatForCoefficientsFromConstructor => "0.########";

		/// <summary>
		/// Индекс дата словарь
		/// </summary>
		private List<DateReference> dateDict;
		/// <summary>
		/// Словарь доля ЗУ
		/// </summary>
		private List<NumberReference> dateNumb;
		/// <summary>
		/// Словарь этаж расположения
		/// </summary>
		private List<NumberReference> floorDict;

		private ExpressScoreReportService ReportService { get; }
		public ExpressScoreService(ScoreCommonService scoreCommonService, RegisterAttributeService registerAttributeService)
		{
			ReportService = new ExpressScoreReportService();
			ScoreCommonService = scoreCommonService;
            RegisterAttributeService = registerAttributeService;
		}

		public OMSettingsParams GetSetting(MarketSegment segmentType)
		{
			var setting = OMSettingsParams.Where(x => x.SegmentType_Code == segmentType).SelectAll().ExecuteFirstOrDefault();
			if (setting == null)
			{
				_log.Error("ЭО. Не найдены настройки для сегмента {segmentType}", segmentType);
			}

			return setting;
		}

		public ParameterDataDto GetEstimateParametersByKn(string kn, int tourId, int attributeId, MarketSegment segmentType, int registerId)
		{
			var unitsIds = ScoreCommonService.GetUnitsIdsByCadastralNumber(kn, tourId);
			var qsGroup = GetQsConditionForCostFactors(segmentType);

			if (unitsIds.Count == 0)
			{
				_log.Error("ЭО. Не надины юниты по кадастровому номеру для указанного тура");
			}
			return unitsIds.Count > 0 ? ScoreCommonService.GetParameters(unitsIds, attributeId, registerId, qsGroup) : null;
		}

		public ParameterDataDto GetEstimateParametersById(int objectId, int attributeId, int registerId, MarketSegment? segmentType = null)
		{
			QSConditionGroup qsGroup = null;

			if(segmentType != null)
			{
				qsGroup = GetQsConditionForCostFactors((MarketSegment) segmentType);
			}
			
			return ScoreCommonService.GetParameters(new List<long> { objectId }, attributeId, registerId, qsGroup);
		}

		public List<ComplexCostFactorForCalculateDto> GetCostFactorsForCalculate(string targetKn, int? targetMarketObjectId, MarketSegment segment)
		{
			List<ComplexCostFactorForCalculateDto> complexCostFactorsForCalculatePage = new List<ComplexCostFactorForCalculateDto>();

			var costFactors = GetCostFactorsBySegmentType(segment);
			if(costFactors == null)
			{
				throw new Exception($"Не найдены оценочные факторы для сегмента {segment.GetEnumDescription()}");
			}

			List<ComplexCostFactor> complexCostFactors;
			complexCostFactors = costFactors.ComplexCostFactors != null
				? costFactors.ComplexCostFactors.Where(x => x.ShowInCalculatePage).ToList()
				: new List<ComplexCostFactor>();

			var analogsFactors = GetAnalogCostFactors(costFactors, targetKn, targetMarketObjectId);
			var koFactors = GetObjectAndCostFactorsByUnitIds(GetSetting(segment), costFactors,
				OMUnit.Where(x => x.CadastralNumber == targetKn).Select(x => x.Id).Execute().Select(x => x.Id).ToList());
			foreach (var complexCostFactor in complexCostFactors)
			{
				if (IsAnalogAttribute(complexCostFactor.AttributeId.GetValueOrDefault()))
				{
					var val = analogsFactors?.FirstOrDefault(x => x.Id == complexCostFactor.AttributeId)?.Value;
					complexCostFactorsForCalculatePage.Add(new ComplexCostFactorForCalculateDto(complexCostFactor, val ?? complexCostFactor.DefaultValue));
					continue;
				}

				var koVal = koFactors?.Attributes?.FirstOrDefault(x => x.Id == complexCostFactor.AttributeId)?.Value;
				complexCostFactorsForCalculatePage.Add(new ComplexCostFactorForCalculateDto(complexCostFactor, koVal ?? complexCostFactor.DefaultValue));
			}

			return complexCostFactorsForCalculatePage;
		}

		/// <summary>
		/// Получаем целевой объект вместе со всемми оценочными параметрами включая параметры из аналогов
		/// </summary>
		/// <param name="setting"></param>
		/// <param name="costFactor"></param>
		/// <param name="unitIds"></param>
		/// <param name="Kn">Кадастровый номер целевого объекта для поиска его в аналогах, получаем с UI</param>
		/// <returns>TargetObjectDto</returns>
		public TargetObjectDto GetTargetObject(OMSettingsParams setting, CostFactorsDto costFactor, List<long> unitIds, string kn)
        {
	        var targetObject = GetObjectAndCostFactorsByUnitIds(setting, costFactor, unitIds);
            targetObject?.Attributes.AddRange(GetAnalogCostFactors(costFactor, kn));
            if (targetObject != null)
            {
	            GetAttributeFromEsTargetValue(ref targetObject);
			}
            return targetObject;
        }

		#region SearchAnalogs
		public QSCondition GetSearchConditionForAnalogs(List<SearchAttribute> searchAttributes, MarketSegment marketSegment, List<DealType> dealType, decimal? lng, decimal? lat)
		{
			var condition = new QSConditionSimple
			{
				ConditionType = QSConditionType.NotEqual,
				LeftOperand = OMCoreObject.GetColumn(x => x.ProcessType_Code),
				RightOperand = new QSColumnConstant(ProcessStep.Excluded)
			}.And(new QSConditionSimple
			{
				ConditionType = QSConditionType.Equal,
				LeftOperand = OMCoreObject.GetColumn(x => x.PropertyMarketSegment_Code),
				RightOperand = new QSColumnConstant(marketSegment)
			}).And(new QSConditionSimple
			{
				ConditionType = QSConditionType.IsNotNull,
				LeftOperand = OMCoreObject.GetColumn(x => x.Area)
			}).And(new QSConditionSimple
			{
				ConditionType = QSConditionType.In,
				LeftOperand = OMCoreObject.GetColumn(x => x.DealType_Code),
				RightOperand = new QSColumnConstant(dealType)
			}).And(new QSConditionSimple
			{
				ConditionType = QSConditionType.Less,
				LeftOperand = OMCoreObject.GetColumn(x => x.Lng),
				RightOperand = new QSColumnConstant(lng + (decimal)0.015791)
			}).And(new QSConditionSimple
			{
				ConditionType = QSConditionType.Less,
				LeftOperand = new QSColumnConstant(lng - (decimal)0.015791),
				RightOperand = OMCoreObject.GetColumn(x => x.Lng)
			}).And(new QSConditionSimple
			{
				ConditionType = QSConditionType.Less,
				LeftOperand = OMCoreObject.GetColumn(x => x.Lat),
				RightOperand = new QSColumnConstant(lat + (decimal)0.009057)
			}).And(new QSConditionSimple
			{
				ConditionType = QSConditionType.Less,
				LeftOperand = new QSColumnConstant(lat - (decimal)0.009057),
				RightOperand = OMCoreObject.GetColumn(x => x.Lat)
			});

			var searchAnalogAttributes = searchAttributes.Where(x => IsAnalogAttribute(x.IdAttribute)).ToList();
			condition.Add(GetConditionsBySearchAttributes(searchAnalogAttributes));

			return condition;
		}

		public QSCondition GetActualDateCondition(DateTime actualDate)
		{

			actualDate = actualDate + new TimeSpan(23, 59, 59);

			DateTime minSearchDate = new DateTime(actualDate.Year - 1, actualDate.Month, actualDate.Day);


			var actualDateCondition = new QSConditionSimple
			{
				ConditionType = QSConditionType.LessOrEqual,
				LeftOperand = OMPriceHistory.GetColumn(x => x.ChangingDate),
				RightOperand = new QSColumnConstant(actualDate)
			}.And(new QSConditionSimple
			{
				ConditionType = QSConditionType.GreaterOrEqual,
				LeftOperand = OMPriceHistory.GetColumn(x => x.ChangingDate),
				RightOperand = new QSColumnConstant(minSearchDate)
			}).Or(new QSConditionSimple
			{
				ConditionType = QSConditionType.LessOrEqual,
				LeftOperand = OMCoreObject.GetColumn(x => x.ParserTime),
				RightOperand = new QSColumnConstant(actualDate)
			}.And(new QSConditionSimple
			{
				ConditionType = QSConditionType.GreaterOrEqual,
				LeftOperand = OMCoreObject.GetColumn(x => x.ParserTime),
				RightOperand = new QSColumnConstant(minSearchDate)
			})).Or(new QSConditionSimple
			{
				ConditionType = QSConditionType.LessOrEqual,
				LeftOperand = OMCoreObject.GetColumn(x => x.LastDateUpdate),
				RightOperand = new QSColumnConstant(actualDate)
			}.And(new QSConditionSimple
			{
				ConditionType = QSConditionType.GreaterOrEqual,
				LeftOperand = OMCoreObject.GetColumn(x => x.LastDateUpdate),
				RightOperand = new QSColumnConstant(minSearchDate)
			}));

			return actualDateCondition;
		}
		public List<QSJoin> JoinPriceHistory()
		{
			return new List<QSJoin>()
			{
				new QSJoin
				{
					JoinCondition = new QSConditionSimple(OMPriceHistory.GetColumn(x => x.InitialId), QSConditionType.Equal,
						OMCoreObject.GetColumn(x => x.Id)),
					RegisterId = OMPriceHistory.GetRegisterId()
				}
			};
		}

		public List<CoordinatesDto> CheckAnalogsByKoFactors(List<OMCoreObject> analogs, OMSettingsParams settings, List<SearchAttribute> searchAttributes)
		{
			List<CoordinatesDto> res = new List<CoordinatesDto>();
			if (analogs == null || analogs.Count == 0) return res;

			long primaryKeyAttributeId = RegisterCache.RegisterAttributes.Values
				.FirstOrDefault(x => x.RegisterId == settings.Registerid && x.IsPrimaryKey)?.Id ?? 0;

			var qsConGroup = new QSConditionGroup(QSConditionGroupType.And);
			qsConGroup.Add(new QSConditionSimple
			{
				ConditionType = QSConditionType.In,
				LeftOperand = OMUnit.GetColumn(x => x.CadastralNumber),
				RightOperand = new QSColumnConstant(analogs.Select(x => x.CadastralNumber))
			});
			var searchKoAttributes = searchAttributes.Where(x => !IsAnalogAttribute(x.IdAttribute)).ToList();
			qsConGroup.Add(GetConditionsBySearchAttributes(searchKoAttributes));

			var query = new QSQuery
			{
				MainRegisterID = (int) settings.Registerid,
				Joins = new List<QSJoin>
				{
					new QSJoin
					{
						RegisterId = OMUnit.GetRegisterId(),
						JoinCondition = new QSConditionSimple
						{
							ConditionType = QSConditionType.Equal,
							LeftOperand = new QSColumnSimple(primaryKeyAttributeId),
							RightOperand = new QSColumnSimple(OMUnit.GetColumnAttributeId(x => x.Id))
						},
						JoinType = QSJoinType.Inner
					}
				},
				Condition = qsConGroup
			};
			query.AddColumn(primaryKeyAttributeId, nameof(CheckKoFactorDto.Id));
			query.AddColumn(OMUnit.GetColumnAttributeId(x => x.CadastralNumber), nameof(CheckKoFactorDto.Kn));

			var resQuery = query.ExecuteQuery<CheckKoFactorDto>();
			List<string> knNumbers = resQuery.Select(x => x.Kn).ToList();
			var successAnalogs = analogs.Where(x => knNumbers.Contains(x.CadastralNumber));
			res.AddRange(successAnalogs.Select(x => new CoordinatesDto{Id = x.Id, Lat = x.Lat.GetValueOrDefault(), Lng = x.Lng.GetValueOrDefault()}));
			return res;
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

				var delLng = Math.Pow(Math.Sin((lng - sLng) / 2), 2);
				var delLat = Math.Pow(Math.Sin((lat - sLat) / 2), 2);

				var d1 = 2 * r * Math.Asin(Math.Sqrt(delLat + Math.Cos(lat) * Math.Cos(sLat) * delLng)); // км - расстояние от исходной точки до одного из найденных объектов

				distances.Add(item.Key, d1);
				if (d1 <= searchRad) selectedCoordinates.Add(item.Key, item.Value);
			}

			if (selectedCoordinates.Count > 0 && selectedCoordinates.Count <= quantity) return selectedCoordinates.Values.ToList();

			if (selectedCoordinates.Count == 0) return GetNearestCoordinates(coordinates, distances, reservedCount);

			if (selectedCoordinates.Count > quantity) return GetNearestCoordinates(coordinates, distances, quantity);

			return new List<CoordinatesDto>();
		}
		#endregion



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
                    x.FloorNumber,
					x.BuildingYear,
					x.Address,
					x.DealType_Code,
                    x.Vat_Code,
                    x.IsOperatingCostsIncluded
				}).Execute().Select(x => new AnalogDto
				{
					Id = x.Id,
					Kn = x.CadastralNumber,
					Price = x.Price.GetValueOrDefault(),
					Square = x.Area.GetValueOrDefault(),
					Date = x.LastDateUpdate ?? x.ParserTime ?? DateTime.MinValue,
                    Floor = x.FloorNumber.GetValueOrDefault(),
					YearBuild = x.BuildingYear.GetValueOrDefault(),
					Address = x.Address,
					DealType = x.DealType_Code,
                    Vat = x.Vat_Code,
                    IsOperatingCostsIncluded = x.IsOperatingCostsIncluded.GetValueOrDefault()
                }).ToList();
		}

		public string CalculateExpressScore(InputCalculateDto inputParam, out ResultCalculateDto resultCalculate)
		{
			SetRequiredReportParameter(inputParam.TargetObjectId, inputParam.Square, inputParam.Analogs, inputParam.Segment, inputParam.Address, inputParam.Kn, inputParam.DealType);


			CalculateSquareCostDto calculateSquareCost = new CalculateSquareCostDto
			{
				Analogs = inputParam.Analogs,
				DealTypeShort = inputParam.DealType,
				MarketSegment = inputParam.Segment,
				ScenarioType = inputParam.ScenarioType,
				TargetMarketObjectId = inputParam.TargetMarketObjectId,
				TargetObjectFloor = inputParam.Floor,
				TargetObjectId = inputParam.TargetObjectId,
				Kn = inputParam.Kn,
				Square = inputParam.Square
			};

			resultCalculate = new ResultCalculateDto();
			var squarePerMeterCost = CalculateSquarePerMeterCost(calculateSquareCost, out string msg, out List<long> successAnalogIds);

			var summaryCost = Math.Round(squarePerMeterCost * inputParam.Square, 2);
			if (squarePerMeterCost == 0)
			{
				return string.IsNullOrEmpty(msg) ? "При расчете что то пошло не так" : msg;
			}

			DealType dealType = inputParam.DealType == DealTypeShort.Rent ? DealType.RentDeal : DealType.SaleDeal;
			msg = SaveSuccessExpressScore(inputParam.TargetObjectId, inputParam.TargetMarketObjectId, summaryCost, squarePerMeterCost, out int id, square: inputParam.Square, floor: inputParam.Floor, scenarioType: inputParam.ScenarioType, 
				segmentType: inputParam.Segment, dealType: dealType, address: inputParam.Address);
			if (!string.IsNullOrEmpty(msg)) return msg;

			msg = AddDependenceEsFromMarketCoreObject(id, successAnalogIds);

			resultCalculate.SquareCost = Math.Round(squarePerMeterCost, 2);
			resultCalculate.SummaryCost = summaryCost;
			resultCalculate.Id = id;
			resultCalculate.Address = inputParam.Address;
			resultCalculate.Area = inputParam.Square;
			resultCalculate.MarketSegment = inputParam.Segment;

			resultCalculate.ReportId = ReportService.GenerateReport(summaryCost, squarePerMeterCost, inputParam.DealType, inputParam.ScenarioType);

			var resultAnalogs = inputParam.Analogs.Where(x => successAnalogIds.Contains(x.Id)).Select(x => new AnalogResultDto
			{
				Id = x.Id,
				Address = x.Address,
				Floor = x.Floor,
				Square = x.Square,
				Source = inputParam.Segment.GetEnumDescription(),
				Price = x.Price,
				Kn = x.Kn
			}).ToList();

			resultCalculate. Analogs = resultAnalogs;
			resultCalculate.DealType = inputParam.DealType;
			resultCalculate.DataToGrid = JsonConvert.SerializeObject(GetDataToGrid(inputParam.Segment, resultAnalogs), new JsonSerializerSettings
			{
				ContractResolver = new CamelCasePropertyNamesContractResolver()
			}); 

			return msg;
		}

		public void GenerateLists(CostFactorsDto exCostFactors, ScenarioType? scenarioType = null)
        {
			dateDict = OMEsReferenceItem.Where(x => x.ReferenceId == exCostFactors.IndexDateDicId).SelectAll().Execute().Select(ScoreCommonService.ReferenceToDate).ToList();
			if (scenarioType != null && scenarioType == ScenarioType.Oks) dateNumb = OMEsReferenceItem.Where(x => x.ReferenceId == exCostFactors.LandShareDicId).SelectAll().Execute().Select(ScoreCommonService.ReferenceToNumber).ToList();
			floorDict = OMEsReferenceItem.Where(x => x.ReferenceId == exCostFactors.FloorDicId).SelectAll().Execute().Select(ScoreCommonService.ReferenceToNumber).OrderByDescending(x => x.Key).ToList().ToList();
		}

		public string RecalculateExpressScore(InputCalculateDto inputParam,  List<int> analogIds,
			  int expressScoreId,  out decimal cost, out decimal squarePerMeterCost, out long reportId)
		{
			cost = 0;
			squarePerMeterCost = 0;
			reportId = 0;

			SetRequiredReportParameter(inputParam.TargetObjectId, inputParam.Square, inputParam.Analogs, inputParam.Segment, inputParam.Address, inputParam.Kn, inputParam.DealType);

			CalculateSquareCostDto calculateSquareCost = new CalculateSquareCostDto
			{
				Analogs = inputParam.Analogs.Where(x => analogIds.Contains((int) x.Id)).ToList(),
				DealTypeShort = inputParam.DealType,
				MarketSegment = inputParam.Segment,
				ScenarioType = inputParam.ScenarioType,
				TargetMarketObjectId = inputParam.TargetMarketObjectId,
				TargetObjectFloor = inputParam.Floor,
				TargetObjectId = inputParam.TargetObjectId,
				Kn = inputParam.Kn,
				Square = inputParam.Square
			};
			squarePerMeterCost = CalculateSquarePerMeterCost(calculateSquareCost, out string msg, out var successAnalogIds);

			if (!string.IsNullOrEmpty(msg)) return msg;

			cost = Math.Round(squarePerMeterCost * inputParam.Square, 2);
			squarePerMeterCost = Math.Round(squarePerMeterCost, 2);

			reportId = ReportService.GenerateReport(cost, squarePerMeterCost, inputParam.DealType, inputParam.ScenarioType);
			msg = SaveSuccessExpressScore(inputParam.TargetObjectId, inputParam.TargetMarketObjectId, cost, squarePerMeterCost, out int id, expressScoreId);
			if (!string.IsNullOrEmpty(msg)) return msg;

			return msg;
		}

        private decimal CalculateSquarePerMeterCost(CalculateSquareCostDto calculateSquareCost, out string msg, out List<long> successAnalogIds)
		{
			msg = "";
			List<decimal> res = new List<decimal>();
			successAnalogIds = new List<long>();
			bool needWriteTargetObjectDataToReport = true;

			var exSettingsCostFactors = GetSetting(calculateSquareCost.MarketSegment);


			CostFactorsDto exCostFactors;

			if (exSettingsCostFactors == null)
			{
				msg = "Не найденны настройки для выбранного сегмента";
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
				msg = "Не найденны настройки для выбранного сегмента";
				return 0;
			}

			if (exCostFactors.LandShareDicId == null || exCostFactors.IndexDateDicId == null)
			{
				msg = @"Не задан словарь для ""Доля ЗУ"" или ""Индекс даты""";
				return 0;
			}

			// Список значений оценочных факторов для объектов аналогов
            List<Tuple<string, string>> costFactorsDataForReport = new List<Tuple<string, string>>();
			// Список значений оценочных факторов для объекта оценки
			List<string> costTargetObjectDataForReport = new List<string>();

			GenerateLists(exCostFactors, calculateSquareCost.ScenarioType);

			foreach (var analog in calculateSquareCost.Analogs)
            {
                decimal yPrice = 0; // Удельный показатель стоимости

				if (analog.Price != 0 && analog.Square != 0) yPrice = analog.Price / analog.Square;

				// Обязательные факторы

				#region Корректировка на дату

				var dateEstimate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
				costTargetObjectDataForReport.Add(DateTime.Now.ToShortDateString());

				var indexDateEstimate = dateDict.FirstOrDefault(x => x.Key == dateEstimate) ?? dateDict.OrderByDescending(x => x.Key).First();
				costTargetObjectDataForReport.Add(indexDateEstimate.Value.ToString(CultureInfo.InvariantCulture));

				DateReference indexAnalogDate = null;

				var text = new KeyValuePair<string,string>("Дата предложения", "");
				var dicText = new KeyValuePair<string,string>(@" Индекс ""Дата предложения""", "");
				
				if (analog.Date != DateTime.MinValue)
				{
					var analogDate = new DateTime(analog.Date.Year, analog.Date.Month, 1);
					text = new KeyValuePair<string, string>("Дата предложения", analog.Date.ToShortDateString());
					indexAnalogDate = dateDict.FirstOrDefault(x => x.Key == analogDate);
					dicText = new KeyValuePair<string, string>(@" Индекс ""Дата предложения""", indexAnalogDate?.Value.ToString() ?? "");
				}

				costFactorsDataForReport.Add(new Tuple<string, string>(text.Key, text.Value));
				costFactorsDataForReport.Add(new Tuple<string, string>(dicText.Key, dicText.Value));

				if (indexAnalogDate == null)
				{
					// TODO Временное решение на время показов
					indexAnalogDate = dateDict.OrderByDescending(x => x.Key).First();
					//continue;
				}

				decimal kDate = indexAnalogDate.Value / indexDateEstimate.Value; // Корректировка на дату

				var correctText = new KeyValuePair<string, string>(@"Корректировка на дату (Кдата)", Math.Round(kDate, 2).ToString(CultureInfo.InvariantCulture));

				costFactorsDataForReport.Add(new Tuple<string, string>(correctText.Key, correctText.Value));
				costTargetObjectDataForReport.Add("");

                decimal cost = 0;
                try
                {
                    cost = kDate * yPrice;
                }
                catch (OverflowException e)
                {
                    GenerateOverflowException(e, analog.Kn, "Корректировку на дату", kDate);
                }

                #endregion

				#region Корректировка на долю ЗУ

                var analogFloorsCount = GetAnalogFloorsCount(analog.Kn, calculateSquareCost.MarketSegment, exSettingsCostFactors);
                text = new KeyValuePair<string, string>("Этажность", analogFloorsCount == 0 ? string.Empty : analogFloorsCount.ToString());
				dicText = new KeyValuePair<string, string>("Корректировка на долю земельного участка (Кдзу)", value: "1");
				double fixedCoefflandShareZero = 0.8; // По требованию заказчика если этажность 0 то коэф 0,8

				if (calculateSquareCost.ScenarioType != null && calculateSquareCost.ScenarioType == ScenarioType.Oks)
				{
					var coefficient = dateNumb.FirstOrDefault(x => x.Key == analogFloorsCount)?.Value ?? dateNumb.Last()?.Value ?? null;

					if (analogFloorsCount == 0 && coefficient == null) coefficient = (decimal)fixedCoefflandShareZero;

					if (coefficient != null)
					{
						dicText = new KeyValuePair<string, string>("Корректировка на долю земельного участка (Кдзу)", value: Math.Round(coefficient.GetValueOrDefault(), 2).ToString("N"));
                        try
                        {
                            cost = cost * coefficient.GetValueOrDefault();
                        }
                        catch (OverflowException e)
                        {
                            GenerateOverflowException(e, analog.Kn, "Корректировку на долю ЗУ", coefficient);
                        }
                    }
                }
				costFactorsDataForReport.Add(new Tuple<string, string>(text.Key, text.Value));
				costFactorsDataForReport.Add(new Tuple<string, string>(dicText.Key, dicText.Value));
				costTargetObjectDataForReport.Add("");
				costTargetObjectDataForReport.Add("");

                #endregion

                if (calculateSquareCost.DealTypeShort == DealTypeShort.Rent)
                {
	                if (exCostFactors.IsVatIncluded.GetValueOrDefault())
	                {
		                cost = AddVat(exCostFactors.VatDictionaryId, calculateSquareCost.TargetMarketObjectId, analog, cost, costTargetObjectDataForReport, ref costFactorsDataForReport);
					}
					if (exCostFactors.IsOperatingCostsUsedInCalculations.GetValueOrDefault())
					{
						cost = AddOperatingCosts(exCostFactors.OperatingCostsCoef, calculateSquareCost.TargetMarketObjectId, analog, cost, costTargetObjectDataForReport, ref costFactorsDataForReport);
					}
				}

                #region Корректировка на этаж

                {
					var floor = (int)analog.Floor != 0 ? (int)analog.Floor : 1; // по условию из примера расчета
					text = new KeyValuePair<string, string>("Этаж расположения", floor.ToString());

					var floorFactor = floor != 0 && floorDict.Count > 0
						? floor > floorDict.FirstOrDefault()?.Key ? floorDict.FirstOrDefault()?.Value :
						floorDict.FirstOrDefault(x => x.Key == floor)?.Value
						: 0;
					dicText = new KeyValuePair<string, string>(@"Коэффициент ""Этаж расположения""", floorFactor?.ToString("N"));

					var targetObjectFloorFactor = floorDict.Count > 0
						? calculateSquareCost.TargetObjectFloor > floorDict.FirstOrDefault().Key
							? floorDict.FirstOrDefault()?.Value
							: floorDict.FirstOrDefault(x => x.Key == calculateSquareCost.TargetObjectFloor)?.Value : 0;

                    correctText = new KeyValuePair<string, string>("Корректировка на этаж расположения (K1)", "1");

					if (floorFactor != null && floorFactor != 0 && targetObjectFloorFactor != null &&
					    targetObjectFloorFactor != 0)
					{
						var factor = targetObjectFloorFactor / floorFactor;
						correctText = new KeyValuePair<string, string>("Корректировка на этаж расположения (K1)", Math.Round(factor.GetValueOrDefault(), 6).ToString("N"));

                        try
                        {
                            cost = cost * (decimal)factor;
                        }
                        catch (OverflowException e)
                        {
                            GenerateOverflowException(e, analog.Kn, "Корректировку на этаж расположения (K1)", factor);
                        }
                    }
					costFactorsDataForReport.Add(new Tuple<string, string>(text.Key, text.Value));
					costFactorsDataForReport.Add(new Tuple<string, string>(dicText.Key, dicText.Value));
					costFactorsDataForReport.Add(new Tuple<string, string>(correctText.Key, correctText.Value));
					costTargetObjectDataForReport.Add(calculateSquareCost.TargetObjectFloor.ToString());
					costTargetObjectDataForReport.Add(targetObjectFloorFactor?.ToString("N"));
					costTargetObjectDataForReport.Add("");
				}

				#endregion

				if (calculateSquareCost.DealTypeShort == DealTypeShort.Sale)
				{
					costFactorsDataForReport.Add(new Tuple<string, string>("Вид сделки (Предложение-продажа/Сделка купли-продажи)", analog.DealType.GetEnumDescription()));
					costTargetObjectDataForReport.Add("");
				}

				if (calculateSquareCost.DealTypeShort == DealTypeShort.Rent)
				{
					costFactorsDataForReport.Add(new Tuple<string, string>("Вид сделки (Предложение-аренда/Сделка-аренда)", analog.DealType.GetEnumDescription()));
					costTargetObjectDataForReport.Add("");
				}

				if (exCostFactors.IsCorrectionByBargainUsedInCalculations.GetValueOrDefault())
				{
					var coefficient = analog.DealType == DealType.RentDeal || analog.DealType == DealType.SaleDeal
						? 1
						: exCostFactors.CorrectionByBargainCoef;

					text = new KeyValuePair<string, string>(@"Корректировка ""Корректировка на торг""",
						coefficient?.ToString(DecimalFormatForCoefficientsFromConstructor));
					if (coefficient != null)
					{
						try
						{
							cost = cost * coefficient.GetValueOrDefault();
						}
						catch (OverflowException e)
						{
							GenerateOverflowException(e, analog.Kn, "Корректировку на торг", coefficient);
						}
					}
					costFactorsDataForReport.Add(new Tuple<string, string>(text.Key, text.Value));
					costTargetObjectDataForReport.Add("");
				}

				foreach (var simple in exCostFactors.SimpleCostFactors)
                {
	                text = new KeyValuePair<string, string>("Корректировка " + @"""" + simple.Name + @"""",
						 simple.Coefficient?.ToString(DecimalFormatForCoefficientsFromConstructor));

                    if (simple.Coefficient != null)
                    {
                        try
                        {
	                        cost = cost * simple.Coefficient.GetValueOrDefault();
                        }
                        catch (OverflowException e)
                        {
                            GenerateOverflowException(e, analog.Kn, $"Статичный коэффициент: {simple.Name}", simple.Coefficient);
                        }
                    }
                    costFactorsDataForReport.Add(new Tuple<string, string>(text.Key, text.Value));
                    costTargetObjectDataForReport.Add("");
                }


				bool isBreak = false;
				int amountSuccessComplexFactors = 2;
				var idAnalog = OMCoreObject.Where(x => x.CadastralNumber == calculateSquareCost.Kn).ExecuteFirstOrDefault()?.Id;


                int countRow = costFactorsDataForReport.Count + GetCountReportRowComplexFactors(exCostFactors.ComplexCostFactors);

                countRow++;/*Строка на скорректированную стоимость*/

                ReportService.InitCostFactorMatrix(countRow, calculateSquareCost.Analogs.Count + 1);

				foreach (var complex in exCostFactors.ComplexCostFactors)
				{
					var complexCoefficientStr =
						complex.Coefficient?.ToString(DecimalFormatForCoefficientsFromConstructor);

					ParameterDataDto targetObjectFactor = null;
					if (complex.ComplexCostFactorType == ComplexCostFactorSpecialization.SquareFactor)
					{
						targetObjectFactor = new ParameterDataDto(new PureParameterDataDto
						{
							Id = calculateSquareCost.TargetObjectId,
							Value = calculateSquareCost.Square
						});
					}
					else
					{
                        try
                        {
                            if (IsAnalogAttribute(complex.AttributeId.GetValueOrDefault()))
                            {
                                if (idAnalog != null)
                                {
                                    targetObjectFactor = GetEstimateParametersById((int)idAnalog,
                                        complex.AttributeId.GetValueOrDefault(), OMCoreObject.GetRegisterId());
                                }
                            }
                            else
                            {
                                targetObjectFactor = GetEstimateParametersById(calculateSquareCost.TargetObjectId,
                                    complex.AttributeId.GetValueOrDefault(), (int)exSettingsCostFactors.Registerid);
                            }

                            if (targetObjectFactor == null || targetObjectFactor.Value == null)
                            {
	                            var esTargetObjectValue = OMTargetObjectValue.Where(x => x.UnitId == calculateSquareCost.TargetObjectId).SelectAll()
		                            .ExecuteFirstOrDefault();

	                            var targetAttributeValue =
		                            esTargetObjectValue.AttributeValue.DeserializeFromXml<List<AttributeValueDto>>();

	                            var attributeValue = targetAttributeValue.FirstOrDefault(x => x.Id == complex.AttributeId).Value;
	                            targetObjectFactor = new ParameterDataDto(new PureParameterDataDto
	                            {
		                            Id = calculateSquareCost.TargetObjectId,
		                            Value = attributeValue
	                            });

							}
						}
                        catch (Exception e)
                        {
                            Console.WriteLine(e);
                            throw new Exception("Не найденны данные для объекта оценки");
                        }
					}
					ParameterDataDto analogFactor;
					if (IsAnalogAttribute(complex.AttributeId.GetValueOrDefault()))
					{
						analogFactor = GetEstimateParametersById((int) analog.Id,
							complex.AttributeId.GetValueOrDefault(), OMCoreObject.GetRegisterId());
					}
					else
					{
						analogFactor = GetEstimateParametersByKn(analog.Kn, (int) exSettingsCostFactors.TourId,
							complex.AttributeId.GetValueOrDefault(), calculateSquareCost.MarketSegment,
							(int) exSettingsCostFactors.Registerid);
					}

					if (analogFactor == null)
					{
						isBreak = true;
						_log.Error("ЭО.Не найден юнит по кад номеру {kn} или аналог с ид {id}", analog.Kn, analog.Id);
						break;
					}

					if(analogFactor.Value == null || analogFactor.Value == string.Empty)
					{
						_log.Error("ЭО. Для аналога с ид {id} не найдено значение оценочного фактора", analog.Id);
					}

					string valueToComplexName = analogFactor.NumberValue != 0 ? analogFactor.NumberValue.ToString("N") : analogFactor.Value?.ToString();

					AddReportDictValue(ref costFactorsDataForReport, new KeyValuePair<string, string>(complex.Name, valueToComplexName));
					costTargetObjectDataForReport.Add(targetObjectFactor?.Value != null ? targetObjectFactor?.Value.ToString() : "Не рассчитано");

					switch (analogFactor.Type)
					{
						case ParameterType.String:
						{
							if (complex.DictionaryId != null && complex.DictionaryId != 0)
							{
								decimal analogC = ScoreCommonService.GetCoefficientFromStringFactor(analogFactor,
									complex.DictionaryId.GetValueOrDefault());
								decimal targetObjectC = ScoreCommonService.GetCoefficientFromStringFactor(
									targetObjectFactor,
									complex.DictionaryId.GetValueOrDefault());

								if (analogC == 0 || targetObjectC == 0)
								{
									AddReportDictValue(ref costFactorsDataForReport,
										new KeyValuePair<string, string>("Метка " + @"""" + complex.Name + @"""", analogC != 0 ? analogC.ToString("N") : ""));
									costTargetObjectDataForReport.Add(targetObjectC != 0 ? targetObjectC.ToString("N") : "");
									AddReportDictValue(ref costFactorsDataForReport, new KeyValuePair<string, string>("Степень влияния", complexCoefficientStr));
									costTargetObjectDataForReport.Add(complexCoefficientStr); 
									AddReportDictValue(ref costFactorsDataForReport, new KeyValuePair<string, string>("Корректировка " + @""""+complex.Name + @"""" + $" K({amountSuccessComplexFactors})", "1"));
									costTargetObjectDataForReport.Add("");
									break;
								}

								AddReportDictValue(ref costFactorsDataForReport,
									new KeyValuePair<string, string>("Метка " + @"""" + complex.Name + @"""",
										analogC.ToString("N")));
								costTargetObjectDataForReport.Add(targetObjectC.ToString("N"));
								AddReportDictValue(ref costFactorsDataForReport,
									new KeyValuePair<string, string>("Степень влияния", complexCoefficientStr));
								costTargetObjectDataForReport.Add(complexCoefficientStr);

								var coeff =
									Math.Exp((double) (targetObjectC * complex.Coefficient.GetValueOrDefault())) /
									Math.Exp((double) (analogC * complex.Coefficient.GetValueOrDefault()));

								AddReportDictValue(ref costFactorsDataForReport, new KeyValuePair<string, string>("Корректировка " + @""""+complex.Name + @"""" + $" K({amountSuccessComplexFactors})", coeff.ToString("N")));

								try
								{
									cost = cost * (decimal) coeff;
								}
								catch (OverflowException e)
								{
									GenerateOverflowException(e, analog.Kn, $"Оценочный фактор: {complex.Name}",
										complex.Coefficient);
								}

								costTargetObjectDataForReport.Add("");
							}

							break;
						}
						case ParameterType.Date:
						{
							if (complex.DictionaryId != null && complex.DictionaryId != 0)
							{
								decimal analogC = ScoreCommonService.GetCoefficientFromDateFactor(analogFactor,
									complex.DictionaryId.GetValueOrDefault());
								decimal targetObjectC = ScoreCommonService.GetCoefficientFromDateFactor(
									targetObjectFactor,
									complex.DictionaryId.GetValueOrDefault());

								if (analogC == 0 || targetObjectC == 0)
								{
									AddReportDictValue(ref costFactorsDataForReport,
										new KeyValuePair<string, string>("Метка " + @"""" + complex.Name + @"""",
											analogC != 0 ? analogC.ToString("N") : ""));
									costTargetObjectDataForReport.Add(targetObjectC != 0
										? targetObjectC.ToString("N")
										: "");
									AddReportDictValue(ref costFactorsDataForReport,
										new KeyValuePair<string, string>("Степень влияния", complexCoefficientStr));
									costTargetObjectDataForReport.Add(complexCoefficientStr);
                                    AddReportDictValue(ref costFactorsDataForReport, new KeyValuePair<string, string>("Корректировка " + @""""+complex.Name+@"""" + $" K({amountSuccessComplexFactors})", "1"));
									costTargetObjectDataForReport.Add("");
									break;
								}

								AddReportDictValue(ref costFactorsDataForReport,
									new KeyValuePair<string, string>("Метка " + @"""" + complex.Name + @"""",
										analogC.ToString("N")));
								costTargetObjectDataForReport.Add(targetObjectC.ToString("N"));
								AddReportDictValue(ref costFactorsDataForReport,
									new KeyValuePair<string, string>("Степень влияния", complexCoefficientStr));
								costTargetObjectDataForReport.Add(complexCoefficientStr);

									var coeff = Math.Exp((double)(targetObjectC * complex.Coefficient.GetValueOrDefault())) / Math.Exp((double)(analogC * complex.Coefficient.GetValueOrDefault()));
								AddReportDictValue(ref costFactorsDataForReport, new KeyValuePair<string, string>("Корректировка " + @""""+complex.Name + @"""" + $" K({amountSuccessComplexFactors})", coeff.ToString("N")));
								costTargetObjectDataForReport.Add("");

								try
								{
									cost = cost * (decimal) coeff;
								}
								catch (OverflowException e)
								{
									GenerateOverflowException(e, analog.Kn, $"Оценочный фактор: {complex.Name}",
										complex.Coefficient);
								}
							}

							break;
						}
						case ParameterType.Number:
						{
							decimal analogC = ScoreCommonService.GetCoefficientFromNumberFactor(analogFactor,
								complex.DictionaryId.GetValueOrDefault());
							decimal targetObjectC =
								ScoreCommonService.GetCoefficientFromNumberFactor(targetObjectFactor,
									complex.DictionaryId.GetValueOrDefault());

							if (analogC == 0 || targetObjectC == 0)
							{
								if (complex.DictionaryId != null && complex.DictionaryId != 0)
								{
									AddReportDictValue(ref costFactorsDataForReport,
										new KeyValuePair<string, string>("Метка " + @"""" + complex.Name + @"""",
											analogC != 0 ? analogC.ToString("N") : ""));
									costTargetObjectDataForReport.Add(targetObjectC != 0
										? targetObjectC.ToString("N")
										: "");
								}

								AddReportDictValue(ref costFactorsDataForReport,
									new KeyValuePair<string, string>("Степень влияния", complexCoefficientStr));
								costTargetObjectDataForReport.Add(complexCoefficientStr);

								AddReportDictValue(ref costFactorsDataForReport, new KeyValuePair<string, string>("Корректировка " + @""""+complex.Name + @"""" + $" K({amountSuccessComplexFactors})", "1"));
								costTargetObjectDataForReport.Add("");
								break;
							}

							if (complex.DictionaryId != null && complex.DictionaryId != 0)
							{
								AddReportDictValue(ref costFactorsDataForReport,
									new KeyValuePair<string, string>("Метка " + @"""" + complex.Name + @"""",
										analogC.ToString("N")));
								costTargetObjectDataForReport.Add(targetObjectC != 0
									? targetObjectC.ToString("N")
									: "");
							}

							AddReportDictValue(ref costFactorsDataForReport,
								new KeyValuePair<string, string>("Степень влияния", complexCoefficientStr));
							costTargetObjectDataForReport.Add(complexCoefficientStr);

							var coeff = Math.Exp((double) (targetObjectC * complex.Coefficient.GetValueOrDefault())) /
							            Math.Exp((double) (analogC * complex.Coefficient.GetValueOrDefault()));

								AddReportDictValue(ref costFactorsDataForReport, new KeyValuePair<string, string>("Корректировка " + @""""+complex.Name + @"""" + $" K({amountSuccessComplexFactors})", coeff.ToString("N")));
								costTargetObjectDataForReport.Add("");
								
                            try
                            {
                                cost = cost * (decimal)coeff;
                            }
                            catch (OverflowException e)
                            {
                                GenerateOverflowException(e, analog.Kn, $"Оценочный фактор: {complex.Name}", complex.Coefficient);
                            }
                            break;
						}
						default:
						{
							_log.Warning("ЭО. Дефолная обработка оценочного фактора, т.к не был найден фактор в Ко части или в аналогах");
							if (complex.DictionaryId != null && complex.DictionaryId != 0)
							{
								AddReportDictValue(ref costFactorsDataForReport,
									new KeyValuePair<string, string>("Метка " + @"""" + complex.Name + @"""", ""));
								costTargetObjectDataForReport.Add("");
							}

							AddReportDictValue(ref costFactorsDataForReport,
								new KeyValuePair<string, string>("Степень влияния", complexCoefficientStr));
							costTargetObjectDataForReport.Add(complexCoefficientStr);
							AddReportDictValue(ref costFactorsDataForReport, new KeyValuePair<string, string>("Корректировка " + @""""+complex.Name + @"""" + $" K({amountSuccessComplexFactors})", "1"));
							costTargetObjectDataForReport.Add("");
							break;
						}
					}

					amountSuccessComplexFactors++;
				}


				if (isBreak)
				{
					WriteByColumnToComplexMatrix(costFactorsDataForReport, costTargetObjectDataForReport);
					costFactorsDataForReport.Clear();
					costTargetObjectDataForReport.Clear();
					continue;
				}

				res.Add(Math.Round(cost, 2));
				successAnalogIds.Add(analog.Id);

				if (calculateSquareCost.DealTypeShort == DealTypeShort.Sale)
				{
					AddReportDictValue(ref costFactorsDataForReport, new KeyValuePair<string, string>("Скорректированная стоимость руб/кв.м", Math.Round(cost, 2).ToString("N")));
				}

				if (calculateSquareCost.DealTypeShort == DealTypeShort.Rent)
				{
					AddReportDictValue(ref costFactorsDataForReport, new KeyValuePair<string, string>("Скорректированная арендная ставка объектов-аналогов, руб/кв.м/год", (Math.Round(cost, 2)* 12).ToString("N")));
				}
				costTargetObjectDataForReport.Add("");

				WriteByColumnToComplexMatrix(costFactorsDataForReport, costTargetObjectDataForReport, needWriteTargetObjectDataToReport);
				needWriteTargetObjectDataToReport = false;
				costFactorsDataForReport.Clear();
				costTargetObjectDataForReport.Clear();
			}

			if (res.Count == 0)
			{
					msg = "Ни один аналог не подошел для расчета.";
					return 0;
			}

			return Math.Round(res.Sum(x => x) / res.Count, 2);
		}

        #region Support For Cost Calculation

        private decimal AddVat(decimal? vatDictionaryId, long? targetMarketObjectId, AnalogDto analog, decimal cost,
            List<string> costTargetObjectDataForReport, ref List<Tuple<string, string>> costFactorsDataForReport)
        {
            var vatDictionaryValues = OMEsReferenceItem.Where(x => x.ReferenceId == vatDictionaryId).SelectAll()
                .Execute();

            var analogLabel = GetVatLabel(analog.Vat);
            var analogCorrection = vatDictionaryValues.FirstOrDefault(x => x.Value.ToLower() == analogLabel.ToLower())
                ?.CalculationValue;

            if (analogCorrection != null)
            {
                try
                {
                    cost = cost * analogCorrection.Value;
                }
                catch (OverflowException e)
                {
                    GenerateOverflowException(e, analog.Kn, "Корректировку на НДС", analogCorrection);
                }
            }

            AddReportDictValue(ref costFactorsDataForReport, new KeyValuePair<string, string>("Наличие НДС", analog.Vat?.GetEnumDescription()));
            AddReportDictValue(ref costFactorsDataForReport, new KeyValuePair<string, string>("Метка (С НДС/Без НДС)", analogLabel));
            AddReportDictValue(ref costFactorsDataForReport, new KeyValuePair<string, string>("Корректировка на НДС", analogCorrection?.ToString()));

            VatType? targetMarketObjectVat = null;
            var targetLabel = string.Empty;
            if (targetMarketObjectId != null)
            {
                targetMarketObjectVat = OMCoreObject.Where(x => x.Id == targetMarketObjectId).Select(x => x.Vat_Code)
                    .ExecuteFirstOrDefault()?.Vat_Code;

                targetLabel = GetVatLabel(targetMarketObjectVat);
            }
            costTargetObjectDataForReport.Add(targetMarketObjectVat?.GetEnumDescription());
            costTargetObjectDataForReport.Add(targetLabel);
            costTargetObjectDataForReport.Add(string.Empty);

            return cost;
        }

		private decimal AddOperatingCosts(decimal? operatingCostsCoef, long? targetMarketObjectId, AnalogDto analog, decimal cost,
			List<string> costTargetObjectDataForReport, ref List<Tuple<string, string>> costFactorsDataForReport)
		{
			var analogCorrection = analog.IsOperatingCostsIncluded.GetValueOrDefault() 
				? operatingCostsCoef 
				: 1;

			if (analogCorrection != null)
			{
				try
				{
					cost = cost * analogCorrection.Value;
				}
				catch (OverflowException e)
				{
					GenerateOverflowException(e, analog.Kn, "Корректировку на операционные расходы", analogCorrection);
				}
			}

			AddReportDictValue(ref costFactorsDataForReport, new KeyValuePair<string, string>("Наличие операционных расходов", 
				analog.IsOperatingCostsIncluded.GetValueOrDefault() ? "Включены" : "Не включены"));
			AddReportDictValue(ref costFactorsDataForReport, new KeyValuePair<string, string>("Корректировка на операционные расходы", analogCorrection?.ToString()));

			bool? targetMarketObjectIsOperatingCostsIncluded = null;
			if (targetMarketObjectId != null)
			{
				targetMarketObjectIsOperatingCostsIncluded = OMCoreObject.Where(x => x.Id == targetMarketObjectId).Select(x => x.IsOperatingCostsIncluded)
					.ExecuteFirstOrDefault()?.IsOperatingCostsIncluded;
			}
			costTargetObjectDataForReport.Add(targetMarketObjectIsOperatingCostsIncluded.GetValueOrDefault() ? "Включены" : "Не включены");
			costTargetObjectDataForReport.Add(string.Empty);

			return cost;
		}

		private string GetVatLabel(VatType? vat)
        {
            var isVatIncluded = vat == VatType.NDS;
            return isVatIncluded ? "С НДС" : "Без НДС";
        }

        private int GetAnalogFloorsCount(string kn, MarketSegment marketSegment, OMSettingsParams settings)
        {
            var floorsCountAttribute = RegisterAttributeService.GetActiveRegisterAttributes(settings.Registerid)
                .FirstOrDefault(x => x.Name.ToLower().Contains("этажность"));
            if (floorsCountAttribute == null)
                return 0;

            var analogFactor = GetEstimateParametersByKn(kn, (int)settings.TourId,
                (int)floorsCountAttribute.Id, marketSegment, (int)settings.Registerid);
            if (analogFactor == null)
                return 0;

            if (int.TryParse(analogFactor.Value?.ToString(), out int floorsCount))
                return floorsCount;

            return 0;
        }

        //генерируем новое исключение, чтобы не потерять стек
        private void GenerateOverflowException(Exception e, string cadastralNumber, string multipliedName, object multipliedValue)
        {
            var message = $"Во время обработки аналога '{cadastralNumber}' возникло переполнение при умножении Стоимости на '{multipliedName} ({multipliedValue})'.";

            throw (Exception)Activator.CreateInstance(e.GetType(), message, e);
        }

        private bool IsAnalogAttribute(long idAttribute)
        {
	        if (idAttribute == 0)
	        {
				_log.Error("ЭО. Ошибка проверки атрибута на принодлежность к аналогам idAttribute = {idAttribute}", idAttribute);
				throw new Exception("Ид атрибута равен 0");
	        }
			bool res = RegisterCache.GetAttributeData(idAttribute)?.RegisterId == OMCoreObject.GetRegisterId();
			return res;
        }

		/// <summary>
		/// Получение объекта со всем оценочными атрибутами только из Ко части
		/// </summary>
		/// <param name="setting"></param>
		/// <param name="costFactor"></param>
		/// <param name="unitIds"></param>
		/// <returns></returns>
        private TargetObjectDto GetObjectAndCostFactorsByUnitIds(OMSettingsParams setting, CostFactorsDto costFactor, List<long> unitIds)
		{
			if (setting == null || costFactor == null || unitIds.Count == 0)
			{
				_log.ForContext("setting", setting)
					.ForContext("costFactor", costFactor)
					.ForContext("unitIds", unitIds)
					.Error("ЭО. Ошибка при получении оценочных факторов из Ко части");

				throw new Exception("Не найдены настройки или оценочные факторы для выбранного сегмента.");
			}
			var results = new List<TargetObjectDto>();
			try
			{
				var tourRegisterPrimaryKeyId = RegisterCache.RegisterAttributes.Values
					.FirstOrDefault(x => x.RegisterId == setting.Registerid && x.IsPrimaryKey)?.Id;

				var query = ScoreCommonService.GetQsQuery((int)setting.Registerid, (int)tourRegisterPrimaryKeyId.GetValueOrDefault(), unitIds);

				query.AddColumn((long)costFactor.YearBuildId.GetValueOrDefault(), ((int)costFactor.YearBuildId.GetValueOrDefault()).ToString());
				foreach (var factor in costFactor.ComplexCostFactors)
				{
					if (!IsAnalogAttribute(factor.AttributeId.GetValueOrDefault()))
						query.AddColumn(factor.AttributeId.GetValueOrDefault(), factor.AttributeId.GetValueOrDefault().ToString());
				}

				var table = query.ExecuteQuery();
				foreach (DataRow row in table.Rows)
				{
					var rowId = row["Id"].ParseToLong();
					var rowAttributes = new List<AttributePure>();
					foreach (var factor in costFactor.ComplexCostFactors.Where(x => !IsAnalogAttribute(x.AttributeId.GetValueOrDefault())))
					{
						rowAttributes.Add(new AttributePure
						{
							Id = factor.AttributeId.GetValueOrDefault(),
							Value = row[factor.AttributeId.ToString()].ParseToStringNullable()
						});
					}
					results.Add(new TargetObjectDto(rowId, rowAttributes));
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				ErrorManager.LogError(e);
				return null;
			}
			
	        return results.OrderByDescending(x => x.UnitId).FirstOrDefault();
        }
		/// <summary>
		/// Получаем оценочные факторы из аналогов для целевого объекта по id или кад номеру
		/// </summary>
		/// <param name="costFactors"></param>
		/// <param name="kn"></param>
		/// <param name="id"></param>
		/// <returns></returns>
		private List<AttributePure> GetAnalogCostFactors(CostFactorsDto costFactors, string kn = null, int? id = null)
        {
	        List<AttributePure> analogCostFactors = new List<AttributePure>();
			try
	        {
		        if (costFactors == null || kn == null && id == null)
		        {
			        _log.ForContext("costFactors===>", costFactors).Error("ЭО.Получение оценочных факторов для аналогов");
			        return analogCostFactors;
		        }

		        if (kn.IsNullOrEmpty() && id == null)
		        {
			        return analogCostFactors;
		        }

		        QSQuery coreObject = new QSQuery
		        {
			        MainRegisterID = OMCoreObject.GetRegisterId(),
			        Condition = kn != null ? new QSConditionSimple
			        {
				        ConditionType = QSConditionType.Equal,
				        LeftOperand = OMCoreObject.GetColumn(x => x.CadastralNumber),
				        RightOperand = new QSColumnConstant(kn)
			        } : new QSConditionSimple
			        {
				        ConditionType = QSConditionType.Equal,
				        LeftOperand = OMCoreObject.GetColumn(x => x.Id),
				        RightOperand = new QSColumnConstant(id)
			        }
		        };

		        foreach (var complexCostFactor in costFactors.ComplexCostFactors)
		        {
			        if (IsAnalogAttribute(complexCostFactor.AttributeId.GetValueOrDefault()))
				        coreObject.AddColumn(complexCostFactor.AttributeId.GetValueOrDefault(), complexCostFactor.AttributeId.GetValueOrDefault().ToString());
		        }

		        var table = coreObject.ExecuteQuery();

		        if (table.Rows.Count > 0)
		        {
			        foreach (var factor in costFactors.ComplexCostFactors.Where(x => IsAnalogAttribute(x.AttributeId.GetValueOrDefault())))
			        {
				        analogCostFactors.Add(new AttributePure
				        {
					        Id = factor.AttributeId.GetValueOrDefault(),
					        Value = table.Rows[0][factor.AttributeId.ToString()].ParseToStringNullable()
				        });
			        }
		        }
		        else
		        {
			        foreach (var factor in costFactors.ComplexCostFactors.Where(x => IsAnalogAttribute(x.AttributeId.GetValueOrDefault())))
			        {
				        analogCostFactors.Add(new AttributePure
				        {
					        Id = factor.AttributeId.GetValueOrDefault(),
					        Value = string.Empty
				        });
			        }
		        }

		        return analogCostFactors;
			}
	        catch (Exception e)
	        {
		        Console.WriteLine(e);
				_log.Error("ЭО. Ошибки при получении оценочных факторов аналогов.");
		        return analogCostFactors;

	        }

        }

		/// <summary>
		/// Получение атрибутов для объекта оценки из таблицы OMTargetObjectValue
		/// </summary>
		/// <param name="targetObjectDto"></param>
		private void GetAttributeFromEsTargetValue(ref TargetObjectDto targetObjectDto)
		{
			if (targetObjectDto.Attributes.Any(x => x.Value.IsNullOrEmpty()))
			{
				long unitId = targetObjectDto.UnitId;
				var esTargetObjectValue =
					OMTargetObjectValue.Where(x => x.UnitId == unitId).SelectAll().ExecuteFirstOrDefault();

				if (esTargetObjectValue != null)
				{
					Dictionary<int, string> attributeValues = targetObjectDto.Attributes.DistinctBy(x => x.Id)
						.ToDictionary(x => x.Id, y => y.Value);

					var esTargetObjectAttributeValue =
						esTargetObjectValue.AttributeValue.DeserializeFromXml<List<AttributeValueDto>>();
					if (esTargetObjectAttributeValue != null && esTargetObjectAttributeValue.Count > 0)
					{
						var existedAttributeValue = esTargetObjectAttributeValue.Where(x => attributeValues.Keys.ToList().Contains(x.Id));
						foreach (var attributeValue in existedAttributeValue)
						{
							if (attributeValues[attributeValue.Id] == null ||
							    attributeValues[attributeValue.Id] == string.Empty)
							{
								attributeValues[attributeValue.Id] = attributeValue.Value.ToString();
							}
							
						}
						targetObjectDto.Attributes = attributeValues.Select(x => new AttributePure
						{
							Id = x.Key,
							Value = x.Value
						}).ToList();
					}
				}
			}

		}

		/// <summary>
		/// Собираем данные для грида с результатом расчета ЭО
		/// </summary>
		/// <param name="marketSegment"></param>
		/// <param name="analogResults"></param>
		/// <returns>DataToGrid</returns>
		public DataToGrid GetDataToGrid(MarketSegment marketSegment, List<AnalogResultDto> analogResults)
		{
			var res  = new DataToGrid();
			res.Headers.Add(new Header{DataField = OMCoreObject.GetColumnAttributeId(x => x.CadastralNumber).ToString(),
				Text = "Кадастровый номер", Width = 200});
			res.Headers.Add(new Header{DataField = OMCoreObject.GetColumnAttributeId(x => x.Address).ToString(),
				Text = "Адрес", Width = 300});
			res.Headers.Add(new Header
			{
				DataField = OMCoreObject.GetColumnAttributeId(x => x.Price).ToString(),
				Text = "Цена"
			});
			var costFactors = GetCostFactorsBySegmentType(marketSegment);

			var setting = GetSetting(marketSegment);
			if (costFactors != null)
			{
				var complexFactors = costFactors.ComplexCostFactors;
				foreach (var complexFactor in complexFactors)
				{
					res.Headers.Add(new Header { DataField = complexFactor.AttributeId.ToString(), Text = complexFactor.Name });
				}

				foreach (var analogResult in analogResults)
				{
					List<AttributePure> analogCostFactors = new List<AttributePure>();
					List<Cell> row = new List<Cell>
					{
						new Cell
						{
							Key = "id",
							CommonValue = analogResult.Id.ToString()
						}
					};
					analogCostFactors.Add(new AttributePure { Id = (int)OMCoreObject.GetColumnAttributeId(x => x.CadastralNumber), Value = analogResult.Kn });
					analogCostFactors.Add(new AttributePure { Id = (int)OMCoreObject.GetColumnAttributeId(x => x.Address), Value = analogResult.Address });
					analogCostFactors.Add(new AttributePure { Id = (int)OMCoreObject.GetColumnAttributeId(x => x.Price), Value = analogResult.Price.ToString() });
					analogCostFactors.AddRange(GetAnalogCostFactors(costFactors, id: (int)analogResult.Id));

					row.AddRange(analogCostFactors.Select(x => new Cell{Key = x.Id.ToString(), CommonValue = x.Value}));

					var unitsIds = ScoreCommonService.GetUnitsIdsByCadastralNumber(analogResult.Kn, (int)setting.TourId);
					var obj = unitsIds.Count > 0 ? GetObjectAndCostFactorsByUnitIds(setting, costFactors, unitsIds) : null;

					if (obj != null)
					{
						row.AddRange(obj.Attributes.Select(x => new Cell{Key = x.Id.ToString(), CommonValue = x.Value}));
					}
					res.Rows.Add(row);
				}
			}
			return res;
		}
		#endregion{


		public string SaveSuccessExpressScore(int targetObjectId, long? targetMarketObjectId, decimal summaryCost, decimal costSquareMeter, out int id, int? expressScoreId = null,
			decimal? square = null, int? floor = null, ScenarioType? scenarioType = null, MarketSegment? segmentType = null, DealType? dealType = null, string address = null)
		{
			id = 0;
			try
			{
				var kn = OMUnit.Where(x => x.Id == targetObjectId).Select(x => x.CadastralNumber).ExecuteFirstOrDefault()?.CadastralNumber;

				if (expressScoreId == null && (square == null || floor == null))
				{
					return "Невозможно выполнить сохранение. Не задана площадь или этаж";
				}

				id = expressScoreId == null
					? AddExpressScore(kn, summaryCost, costSquareMeter, square.Value, floor.Value, targetObjectId, targetMarketObjectId, scenarioType.GetValueOrDefault(), segmentType.GetValueOrDefault(), dealType.GetValueOrDefault(), address)
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

        private int AddExpressScore(string kn, decimal cost, decimal costSquareMeter, decimal square, int floor,
            int targetObjectId, long? targetMarketObjectId, ScenarioType scenarioType, MarketSegment segmentType,
            DealType dealType, string address)
        {
            return new OMExpressScore
            {
                KadastralNumber = kn,
                CostSquareMeter = costSquareMeter,
                DateCost = DateTime.Now.Date,
                SummaryCost = cost,
                Objectid = targetObjectId,
                TargetMarketObjectId = targetMarketObjectId,
                Floor = floor,
                Square = square,
                ScenarioType_Code = scenarioType,
                SegmentType_Code = segmentType,
                DealType_Code = dealType,
                Address = address
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


		public CostFactorsDto GetCostFactorsBySegmentType(MarketSegment segmentType)
		{
			var setting = OMSettingsParams.Where(x => x.SegmentType_Code == segmentType).SelectAll().ExecuteFirstOrDefault();

			if (setting == null || string.IsNullOrEmpty(setting.CostFacrors))
			{
				_log.Error("ЭО.Не найдены оценочные факторы для сегмента {segmentType}", segmentType);
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

				foreach (var factor in complexFactors.Where(x => !IsAnalogAttribute(x.AttributeId.GetValueOrDefault())))
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

		private void SetRequiredReportParameter(int targetObjectId, decimal targetSquare, List<AnalogDto> analogs, MarketSegment marketSegment, string address, string kn, DealTypeShort dealType)
		{
			ReportService.InitRequiredMatrix(7, analogs.Count + 1);

			var exSettingsCostFactors = OMSettingsParams.Where(x => x.SegmentType_Code == marketSegment).SelectAll()
				.ExecuteFirstOrDefault();

			var costFactor = GetCostFactorsBySegmentType(marketSegment);

			var targetObjectYear = GetEstimateParametersById(targetObjectId,
				(int)costFactor.YearBuildId.GetValueOrDefault(), (int)exSettingsCostFactors.Registerid, marketSegment);
			//Заполняем данные целевого объекта
			ReportService.AddNameCharacteristicRequiredParam("Сегмент");
			ReportService.AddValueRequiredParam(marketSegment.GetEnumDescription());

			ReportService.AddNameCharacteristicRequiredParam("Год постройки");
			ReportService.AddValueRequiredParam(targetObjectYear?.NumberValue.ToString(CultureInfo.InvariantCulture));

			ReportService.AddNameCharacteristicRequiredParam("Площадь, кв.м");
			ReportService.AddValueRequiredParam(targetSquare.ToString(CultureInfo.InvariantCulture));

			ReportService.AddNameCharacteristicRequiredParam("Адрес");
			ReportService.AddValueRequiredParam(address);

			ReportService.AddNameCharacteristicRequiredParam("КН");
			ReportService.AddValueRequiredParam(kn);
			ReportService.KnTargetObject = kn;

			ReportService.AddNameCharacteristicRequiredParam(dealType == DealTypeShort.Rent ? "Стоимость объекта-аналога, руб/год" : "Стоимость объекта-аналога, руб");
			ReportService.AddValueRequiredParam("-");

			ReportService.AddNameCharacteristicRequiredParam(dealType == DealTypeShort.Rent ? "Стоимость объекта-аналога, руб/кв.м/год" : "Стоимость объекта-аналога, руб/кв.м");
			ReportService.AddValueRequiredParam("-");

			foreach (AnalogDto analog in analogs)
			{
				ReportService.SetNextColumnRequiredParam();
				ReportService.AddValueRequiredParam(marketSegment.GetEnumDescription());
				ReportService.AddValueRequiredParam(analog.YearBuild.ToString(CultureInfo.InvariantCulture));
				ReportService.AddValueRequiredParam(analog.Square.ToString(CultureInfo.InvariantCulture));
				ReportService.AddValueRequiredParam(analog.Address.ToString(CultureInfo.InvariantCulture));
				ReportService.AddValueRequiredParam(analog.Kn.ToString(CultureInfo.InvariantCulture));
				if (dealType == DealTypeShort.Rent)
				{
					ReportService.AddValueRequiredParam((analog.Price * 12).ToString("N"));
					ReportService.AddValueRequiredParam(Math.Round(analog.Price * 12 / analog.Square, 2).ToString("N"));
				}
				else
				{
					ReportService.AddValueRequiredParam((analog.Price).ToString("N"));
					ReportService.AddValueRequiredParam(Math.Round(analog.Price / analog.Square, 2).ToString("N"));
				}
			}
		}

		private void AddReportDictValue(ref List<Tuple<string, string>> list, KeyValuePair<string, string> value)
		{
			list.Add(new Tuple<string, string>(value.Key, value.Value));
		}

		private int GetCountReportRowComplexFactors(List<ComplexCostFactor> complexCostFactors)
		{
			int res = 0;
			foreach (var factor in complexCostFactors)
			{
				res += 3;
				if (factor.DictionaryId != 0 && factor.DictionaryId != null)
				{
					res++;
				}
			}
			return res;
		}

		private void WriteByColumnToComplexMatrix(List<Tuple<string, string>> analogParams, List<string> targetObjectFactors, bool fullWrite = false)
		{
			try
			{
				if (fullWrite)
				{
					foreach (var analogParam in analogParams)
					{
						ReportService.AddNameCharacteristicCostMatrix(analogParam.Item1);
					}

					foreach (var factor in targetObjectFactors)
					{
						ReportService.AddValueTargetObjectCostMatrix(factor);
					}
				}
				foreach (var analogParam in analogParams)
				{
					ReportService.AddValueCostMatrix(analogParam.Item2);
				}
				ReportService.SetNextColumnComplexMatrix();

			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				ErrorManager.LogError(e);
				throw;
			}
        }


		#region preserving target object attributes

		public void SetTargetObjectAttribute(long unitId, List<AttributeValueDto> attributeValueDtos)
		{
			var unit = OMUnit.Where(x => x.Id == unitId)
				.ExecuteFirstOrDefault();
			if (unit == null)
			{
				throw new Exception($"Не найдена единица оценки с ИД {unitId}");
			}

			var targetObjectValue = OMTargetObjectValue.Where(x => x.UnitId == unitId).SelectAll().ExecuteFirstOrDefault();

			if (targetObjectValue == null)
			{
				new OMTargetObjectValue
				{
					UnitId = unitId,
					AttributeValue = attributeValueDtos.SerializeToXml()
				}.Save();
			} else
			{
				var targetObjectAttributeValues = targetObjectValue.AttributeValue.DeserializeFromXml<List<AttributeValueDto>>();

				foreach (var attributeValueDto in attributeValueDtos)
				{
					if (targetObjectAttributeValues.Any(x => x.Id != attributeValueDto.Id))
					{
						targetObjectAttributeValues.Add(attributeValueDto);
					}
				}

				targetObjectValue.AttributeValue = targetObjectAttributeValues.SerializeToXml();
				targetObjectValue.Save();
			}
		}
		#endregion

		#region Support For Search
		/// <summary>
		/// 
		/// </summary>
		/// <returns>
		/// Возвращает условия для поиска юнитов по оценочным факторам
		/// </returns>
		private QSCondition GetConditionsBySearchAttributes(List<SearchAttribute> searchAttributes)
		{
			var qsConditionGroup = new QSConditionGroup(QSConditionGroupType.And);
			foreach (var searchAttribute in searchAttributes)
			{
				qsConditionGroup.Add(new QSConditionSimple
				{
					ConditionType = QSConditionType.EqualNonCaseSensitive,
					LeftOperand = new QSColumnSimple(searchAttribute.IdAttribute),
					RightOperand = new QSColumnConstant(searchAttribute.Value)
				});
			}
			return qsConditionGroup;
		}
		

		#endregion
	}
}