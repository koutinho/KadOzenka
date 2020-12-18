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
using KadOzenka.Dal.Enum;
using KadOzenka.Dal.ExpressScore.Dto;
using KadOzenka.Dal.Registers;
using KadOzenka.Dal.ScoreCommon;
using KadOzenka.Dal.ScoreCommon.Dto;
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

		public delegate void CalculateProcessHandler(decimal progress);

		public event CalculateProcessHandler NotifyCalculateProgress;

		public decimal SummaryCalculateProgress;

		public ScoreCommonService ScoreCommonService { get; set; }
		public RegisterAttributeService RegisterAttributeService { get; set; }
        private string DecimalFormatForCoefficientsFromConstructor => "0.########";

        private const string ToStringFormatCoeffPrecision = "0.##########";
        private const string ToStringFormatCostPrecision = "0.##";
        private const int MathRoundCoeffPrecision = 10;
        private const int MathRoundCostPrecision = 2;

		/// <summary>
		/// Индекс дата словарь
		/// </summary>
		private List<DateReference> _dateDict;
		/// <summary>
		/// Словарь доля ЗУ
		/// </summary>
		private List<NumberReference> _dateNumb;

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
				return complexCostFactorsForCalculatePage;
			}

			var complexCostFactorsForPageCalculate = costFactors.ComplexCostFactors != null
				? costFactors.ComplexCostFactors.Where(x => x.ShowInCalculatePage).ToList()
				: new List<ComplexCostFactor>();

			var analogsFactors = GetAnalogCostFactors(costFactors, targetKn, targetMarketObjectId);
			var koFactors = GetObjectAndCostFactorsByUnitIds(GetSetting(segment), costFactors,
				OMUnit.Where(x => x.CadastralNumber == targetKn).Select(x => x.Id).Execute().Select(x => x.Id).ToList());
			foreach (var complexCostFactor in complexCostFactorsForPageCalculate)
			{
				if (IsAnalogAttribute(complexCostFactor.AttributeId.GetValueOrDefault()))
				{
					var val = analogsFactors?.FirstOrDefault(x => x.Id == complexCostFactor.AttributeId)?.Value;
					string dValue = GetCommonValueReference(complexCostFactor.DictionaryId.GetValueOrDefault(), val);
					complexCostFactorsForCalculatePage.Add(new ComplexCostFactorForCalculateDto(complexCostFactor, dValue ?? complexCostFactor.DefaultValue));
					continue;
				}

				var koVal = koFactors?.Attributes?.FirstOrDefault(x => x.Id == complexCostFactor.AttributeId)?.Value;
				string dKoValue = GetCommonValueReference(complexCostFactor.DictionaryId.GetValueOrDefault(), koVal);
				complexCostFactorsForCalculatePage.Add(new ComplexCostFactorForCalculateDto(complexCostFactor, dKoValue ?? complexCostFactor.DefaultValue));
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
			}).And(new QSConditionSimple
			{
				ConditionType = QSConditionType.IsNull,
				LeftOperand = OMCoreObject.GetColumn(x => x.LastDateUpdate),
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
					RegisterId = OMPriceHistory.GetRegisterId(),
					JoinType = QSJoinType.Left
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
			SetProgressCalculate(0);
			SetRequiredReportParameter(inputParam.TargetObjectId, inputParam.Analogs, inputParam.Segment, inputParam.Address, inputParam.Kn, inputParam.DealType);
			SetProgressCalculate(5);

			CalculateSquareCostDto calculateSquareCost = new CalculateSquareCostDto
			{
				Analogs = inputParam.Analogs,
				DealTypeShort = inputParam.DealType,
				MarketSegment = inputParam.Segment,
				ScenarioType = inputParam.ScenarioType,
				TargetMarketObjectId = inputParam.TargetMarketObjectId,
				TargetObjectId = inputParam.TargetObjectId,
				Kn = inputParam.Kn,
				ComplexCalculateParameters = inputParam.ComplexCalculateParameters
			};

			resultCalculate = new ResultCalculateDto();
			var squarePerMeterCost = CalculateSquarePerMeterCost(calculateSquareCost, out string msg, out List<long> successAnalogIds);
			if (squarePerMeterCost == 0)
			{
				return string.IsNullOrEmpty(msg) ? "При расчете что то пошло не так" : msg;
			}
			var summaryCost = Math.Round(squarePerMeterCost * inputParam.Square, MathRoundCostPrecision);
			
			DealType dealType = inputParam.DealType == DealTypeShort.Rent ? DealType.RentDeal : DealType.SaleDeal;

			SaveExpressScoreDto saveExpressScore = new SaveExpressScoreDto
			{
				Address = inputParam.Address,
				CostSquareMeter = squarePerMeterCost,
				DealType = dealType,
				Square = inputParam.Square,
				ScenarioType = inputParam.ScenarioType,
				SegmentType = inputParam.Segment,
				SummaryCost = summaryCost,
				TargetMarketObjectId = inputParam.TargetMarketObjectId,
				TargetObjectId = inputParam.TargetObjectId,
				ComplexCalculateParameters = inputParam.ComplexCalculateParameters.SerializeToXml()
			};

			msg = SaveSuccessExpressScore(saveExpressScore, out int id);
			if (!string.IsNullOrEmpty(msg)) return msg;

			msg = AddDependenceEsFromMarketCoreObject(id, successAnalogIds);

			SetProgressCalculate(5);
			resultCalculate.SquareCost = Math.Round(squarePerMeterCost, MathRoundCostPrecision);
			resultCalculate.SummaryCost = summaryCost;
			resultCalculate.Id = id;
			resultCalculate.Address = inputParam.Address;
			resultCalculate.Area = inputParam.Square;
			resultCalculate.MarketSegment = inputParam.Segment;

			resultCalculate.ReportId = ReportService.GenerateReport(summaryCost, squarePerMeterCost, inputParam.DealType, inputParam.ScenarioType);
			SetProgressCalculate(5);
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
			SetProgressCalculate(100);
			return msg;
		}

		public void GenerateLists(CostFactorsDto exCostFactors, ScenarioType? scenarioType = null)
        {
			_dateDict = OMEsReferenceItem.Where(x => x.ReferenceId == exCostFactors.IndexDateDicId).SelectAll().Execute().Select(ScoreCommonService.ReferenceToDate).ToList();
			_dateNumb = OMEsReferenceItem.Where(x => x.ReferenceId == exCostFactors.LandShareDicId).SelectAll().Execute().Select(ScoreCommonService.ReferenceToNumber).ToList();
        }

		public string RecalculateExpressScore(InputCalculateDto inputParam,  List<int> analogIds,
			  int expressScoreId,  out decimal summaryCost, out decimal squarePerMeterCost, out long reportId)
		{
			summaryCost = 0;
			squarePerMeterCost = 0;
			reportId = 0;
			SetProgressCalculate(0);
			SetRequiredReportParameter(inputParam.TargetObjectId, inputParam.Analogs, inputParam.Segment, inputParam.Address, inputParam.Kn, inputParam.DealType);
			SetProgressCalculate(5);

			CalculateSquareCostDto calculateSquareCost = new CalculateSquareCostDto
			{
				Analogs = inputParam.Analogs.Where(x => analogIds.Contains((int) x.Id)).ToList(),
				DealTypeShort = inputParam.DealType,
				MarketSegment = inputParam.Segment,
				ScenarioType = inputParam.ScenarioType,
				TargetMarketObjectId = inputParam.TargetMarketObjectId,
				TargetObjectId = inputParam.TargetObjectId,
				Kn = inputParam.Kn,
				ComplexCalculateParameters = inputParam.ComplexCalculateParameters
			};
			squarePerMeterCost = CalculateSquarePerMeterCost(calculateSquareCost, out string msg, out var successAnalogIds);

			if (!string.IsNullOrEmpty(msg)) return msg;

			summaryCost = Math.Round(squarePerMeterCost * inputParam.Square, MathRoundCostPrecision);
			squarePerMeterCost = Math.Round(squarePerMeterCost, MathRoundCostPrecision);

			reportId = ReportService.GenerateReport(summaryCost, squarePerMeterCost, inputParam.DealType, inputParam.ScenarioType);
			SetProgressCalculate(5);
			SaveExpressScoreDto saveExpressScore = new SaveExpressScoreDto
			{
				CostSquareMeter = squarePerMeterCost,
				SummaryCost = summaryCost,
				TargetMarketObjectId = inputParam.TargetMarketObjectId,
				TargetObjectId = inputParam.TargetObjectId,
				ExpressScoreId = expressScoreId
			};
			msg = SaveSuccessExpressScore(saveExpressScore, out int id);
			if (!string.IsNullOrEmpty(msg)) return msg;
			SetProgressCalculate(100);
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


			double percent = 100d / calculateSquareCost.Analogs.Count;
			//0.8 коэф, т.е расчет занимает 80% от всего
			int progress = (int)Math.Floor(percent * 0.8);
			foreach (var analog in calculateSquareCost.Analogs)
			{

				decimal yPrice = 0; // Удельный показатель стоимости

				if (analog.Price != 0 && analog.Square != 0) yPrice = analog.Price / analog.Square;

				// Обязательные факторы

				#region Корректировка на дату

				if (_dateDict.Count == 0 || _dateNumb.Count == 0)
				{
					msg = @"Словарь для ""Индекс даты"" или ""Доли ЗУ"" пустой";
					return 0;
				}

				var dateEstimate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
				costTargetObjectDataForReport.Add(DateTime.Now.ToShortDateString());

				var indexDateEstimate = _dateDict.FirstOrDefault(x => x.Key == dateEstimate) ?? _dateDict.OrderByDescending(x => x.Key).FirstOrDefault();
				costTargetObjectDataForReport.Add(indexDateEstimate?.Value.ToString(CultureInfo.InvariantCulture));

				DateReference indexAnalogDate = null;

				var text = new KeyValuePair<string,string>("Дата предложения", "");
				var dicText = new KeyValuePair<string,string>(@" Индекс ""Дата предложения""", "");

				if (analog.Date != DateTime.MinValue)
				{
					var analogDate = new DateTime(analog.Date.Year, analog.Date.Month, 1);
					text = new KeyValuePair<string, string>("Дата предложения", analog.Date.ToShortDateString());
					indexAnalogDate = _dateDict.FirstOrDefault(x => x.Key == analogDate);
					dicText = new KeyValuePair<string, string>(@" Индекс ""Дата предложения""", indexAnalogDate?.Value.ToString() ?? "");
				}

				costFactorsDataForReport.Add(new Tuple<string, string>(text.Key, text.Value));
				costFactorsDataForReport.Add(new Tuple<string, string>(dicText.Key, dicText.Value));

				if (indexAnalogDate == null)
				{
					// TODO Временное решение на время показов
					indexAnalogDate = _dateDict.OrderByDescending(x => x.Key).First();
					//continue;
				}

				decimal kDate = indexAnalogDate.Value / indexDateEstimate.Value; // Корректировка на дату

				var correctText = new KeyValuePair<string, string>(@"Корректировка на дату (Кдата)", Math.Round(kDate, MathRoundCoeffPrecision).ToString(CultureInfo.InvariantCulture));

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
					var coefficient = _dateNumb.FirstOrDefault(x => x.Key == analogFloorsCount)?.Value ?? _dateNumb.Last()?.Value ?? null;

					if (analogFloorsCount == 0 && coefficient == null) coefficient = (decimal)fixedCoefflandShareZero;

					if (coefficient != null)
					{
						dicText = new KeyValuePair<string, string>("Корректировка на долю земельного участка (Кдзу)", value: Math.Round(coefficient.GetValueOrDefault(), MathRoundCoeffPrecision).ToString(ToStringFormatCoeffPrecision));
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

				#region НДС

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

				#endregion

				#region Тип сделки

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

				#endregion

				#region Корректировка на торг

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

				#endregion


				#region simple cost factors

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

				#endregion



				bool isBreak = false;
				int amountSuccessComplexFactors = 1;
				var idAnalog = OMCoreObject.Where(x => x.CadastralNumber == calculateSquareCost.Kn).ExecuteFirstOrDefault()?.Id;


                int countRow = costFactorsDataForReport.Count + GetCountReportRowComplexFactors(exCostFactors.ComplexCostFactors);

                countRow++;/*Строка на скорректированную стоимость*/

                ReportService.InitCostFactorMatrix(countRow, calculateSquareCost.Analogs.Count + 1);

                try
                {
					foreach (var complex in exCostFactors.ComplexCostFactors)
					{
						var complexCoefficientStr =
							complex.Coefficient?.ToString(DecimalFormatForCoefficientsFromConstructor);

						ParameterDataDto targetObjectFactor = GetTargetObjectFactor(complex.AttributeId.GetValueOrDefault(),
							(int?)idAnalog, calculateSquareCost.TargetObjectId, (int)exSettingsCostFactors.Registerid, calculateSquareCost.ComplexCalculateParameters, out msg);

						if (targetObjectFactor == null)
						{
							return 0;
						}
						ParameterDataDto analogFactor;
						if (IsAnalogAttribute(complex.AttributeId.GetValueOrDefault()))
						{
							analogFactor = GetEstimateParametersById((int)analog.Id,
								complex.AttributeId.GetValueOrDefault(), OMCoreObject.GetRegisterId());
						}
						else
						{
							analogFactor = GetEstimateParametersByKn(analog.Kn, (int)exSettingsCostFactors.TourId,
								complex.AttributeId.GetValueOrDefault(), calculateSquareCost.MarketSegment,
								(int)exSettingsCostFactors.Registerid);
						}

						if (analogFactor == null)
						{
							isBreak = true;
							_log.Error("ЭО.Не найден юнит по кад номеру {kn} или аналог с ид {id}", analog.Kn, analog.Id);
							break;
						}

						if (analogFactor.Value == null || analogFactor.Value.ToString() == string.Empty)
						{
							_log.Warning("ЭО. Для аналога с ид {id} не найдено значение оценочного фактора {name}", analog.Id, complex.Name);
						}

						string valueToComplexName = analogFactor.NumberValue != 0 ? analogFactor.NumberValue.ToString(ToStringFormatCoeffPrecision) : analogFactor.Value?.ToString();

						AddReportDictValue(ref costFactorsDataForReport, new KeyValuePair<string, string>(complex.Name, valueToComplexName));
						string reportTargetObjVal = targetObjectFactor.ValueFromCalculateForm
							? targetObjectFactor.ValueCalculateForm
							: targetObjectFactor?.Value != null
								? targetObjectFactor?.Value.ToString()
								: "Не рассчитано";
						costTargetObjectDataForReport.Add(reportTargetObjVal);

						switch (analogFactor.Type)
						{
							case ParameterType.String:
								{
									if (complex.DictionaryId != null && complex.DictionaryId != 0)
									{
										decimal analogC = ScoreCommonService.GetCoefficientFromStringFactor(analogFactor,
											complex.DictionaryId.GetValueOrDefault());

										decimal targetObjectC = targetObjectFactor.ValueFromCalculateForm ? targetObjectFactor.NumberValue
											: ScoreCommonService.GetCoefficientFromStringFactor(targetObjectFactor, complex.DictionaryId.GetValueOrDefault());

										if (analogC == 0 || targetObjectC == 0)
										{
											AddReportDictValue(ref costFactorsDataForReport,
												new KeyValuePair<string, string>("Метка " + @"""" + complex.Name + @"""", analogC != 0 ? analogC.ToString(ToStringFormatCoeffPrecision) : ""));
											costTargetObjectDataForReport.Add(targetObjectC != 0 ? targetObjectC.ToString(ToStringFormatCoeffPrecision) : "");
											AddReportDictValue(ref costFactorsDataForReport, new KeyValuePair<string, string>("Степень влияния", complexCoefficientStr));
											costTargetObjectDataForReport.Add(complexCoefficientStr);
											AddReportDictValue(ref costFactorsDataForReport, new KeyValuePair<string, string>("Корректировка " + @"""" + complex.Name + @"""" + $" K({amountSuccessComplexFactors})", "1"));
											costTargetObjectDataForReport.Add("");
											break;
										}

										AddReportDictValue(ref costFactorsDataForReport,
											new KeyValuePair<string, string>("Метка " + @"""" + complex.Name + @"""",
												analogC.ToString(ToStringFormatCoeffPrecision)));
										costTargetObjectDataForReport.Add(targetObjectC.ToString(ToStringFormatCoeffPrecision));
										AddReportDictValue(ref costFactorsDataForReport,
											new KeyValuePair<string, string>("Степень влияния", complexCoefficientStr));
										costTargetObjectDataForReport.Add(complexCoefficientStr);

										var coeff =
											Math.Exp((double)(targetObjectC * complex.Coefficient.GetValueOrDefault())) /
											Math.Exp((double)(analogC * complex.Coefficient.GetValueOrDefault()));

										AddReportDictValue(ref costFactorsDataForReport, new KeyValuePair<string, string>("Корректировка " + @"""" + complex.Name + @"""" + $" K({amountSuccessComplexFactors})", coeff.ToString(ToStringFormatCoeffPrecision)));

										try
										{
											cost = cost * Convert.ToDecimal(Math.Round(coeff, MathRoundCoeffPrecision));
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
										decimal targetObjectC = targetObjectFactor.ValueFromCalculateForm ? targetObjectFactor.NumberValue
											: ScoreCommonService.GetCoefficientFromDateFactor(targetObjectFactor, complex.DictionaryId.GetValueOrDefault());

										if (analogC == 0 || targetObjectC == 0)
										{
											AddReportDictValue(ref costFactorsDataForReport,
												new KeyValuePair<string, string>("Метка " + @"""" + complex.Name + @"""",
													analogC != 0 ? analogC.ToString(ToStringFormatCoeffPrecision) : ""));
											costTargetObjectDataForReport.Add(targetObjectC != 0
												? targetObjectC.ToString(ToStringFormatCoeffPrecision)
												: "");
											AddReportDictValue(ref costFactorsDataForReport,
												new KeyValuePair<string, string>("Степень влияния", complexCoefficientStr));
											costTargetObjectDataForReport.Add(complexCoefficientStr);
											AddReportDictValue(ref costFactorsDataForReport, new KeyValuePair<string, string>("Корректировка " + @"""" + complex.Name + @"""" + $" K({amountSuccessComplexFactors})", "1"));
											costTargetObjectDataForReport.Add("");
											break;
										}

										AddReportDictValue(ref costFactorsDataForReport,
											new KeyValuePair<string, string>("Метка " + @"""" + complex.Name + @"""",
												analogC.ToString(ToStringFormatCoeffPrecision)));
										costTargetObjectDataForReport.Add(targetObjectC.ToString(ToStringFormatCoeffPrecision));
										AddReportDictValue(ref costFactorsDataForReport,
											new KeyValuePair<string, string>("Степень влияния", complexCoefficientStr));
										costTargetObjectDataForReport.Add(complexCoefficientStr);

										var coeff = Math.Exp((double)(targetObjectC * complex.Coefficient.GetValueOrDefault())) / Math.Exp((double)(analogC * complex.Coefficient.GetValueOrDefault()));
										AddReportDictValue(ref costFactorsDataForReport, new KeyValuePair<string, string>("Корректировка " + @"""" + complex.Name + @"""" + $" K({amountSuccessComplexFactors})", coeff.ToString(ToStringFormatCoeffPrecision)));
										costTargetObjectDataForReport.Add("");

										try
										{
											cost = cost * Convert.ToDecimal(Math.Round(coeff, MathRoundCoeffPrecision));
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
									decimal targetObjectC = targetObjectFactor.ValueFromCalculateForm ? targetObjectFactor.NumberValue
										: ScoreCommonService.GetCoefficientFromNumberFactor(targetObjectFactor, complex.DictionaryId.GetValueOrDefault());

									if (analogC == 0 || targetObjectC == 0)
									{
										if (complex.DictionaryId != null && complex.DictionaryId != 0)
										{
											AddReportDictValue(ref costFactorsDataForReport,
												new KeyValuePair<string, string>("Метка " + @"""" + complex.Name + @"""",
													analogC != 0 ? analogC.ToString(ToStringFormatCoeffPrecision) : ""));
											costTargetObjectDataForReport.Add(targetObjectC != 0
												? targetObjectC.ToString(ToStringFormatCoeffPrecision)
												: "");
										}

										AddReportDictValue(ref costFactorsDataForReport, new KeyValuePair<string, string>("Степень влияния", complexCoefficientStr));
										costTargetObjectDataForReport.Add(complexCoefficientStr);

										AddReportDictValue(ref costFactorsDataForReport, new KeyValuePair<string, string>("Корректировка " + @"""" + complex.Name + @"""" + $" K({amountSuccessComplexFactors})", "1"));
										costTargetObjectDataForReport.Add("");
										break;
									}

									if (complex.DictionaryId != null && complex.DictionaryId != 0)
									{
										AddReportDictValue(ref costFactorsDataForReport, new KeyValuePair<string, string>("Метка " + @"""" + complex.Name + @"""", analogC.ToString(ToStringFormatCoeffPrecision)));
										costTargetObjectDataForReport.Add(targetObjectC != 0 ? targetObjectC.ToString(ToStringFormatCoeffPrecision) : "");
									}

									AddReportDictValue(ref costFactorsDataForReport, new KeyValuePair<string, string>("Степень влияния", complexCoefficientStr));
									costTargetObjectDataForReport.Add(complexCoefficientStr);

									var coeff = Math.Exp((double)(targetObjectC * complex.Coefficient.GetValueOrDefault())) /
												Math.Exp((double)(analogC * complex.Coefficient.GetValueOrDefault()));

									AddReportDictValue(ref costFactorsDataForReport,
										new KeyValuePair<string, string>("Корректировка " + @"""" + complex.Name + @"""" + $" K({amountSuccessComplexFactors})", coeff.ToString(ToStringFormatCoeffPrecision)));
									costTargetObjectDataForReport.Add("");

									try
									{
										cost = cost * Convert.ToDecimal(Math.Round(coeff, MathRoundCoeffPrecision));
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
									AddReportDictValue(ref costFactorsDataForReport, new KeyValuePair<string, string>("Корректировка " + @"""" + complex.Name + @"""" + $" K({amountSuccessComplexFactors})", "1"));
									costTargetObjectDataForReport.Add("");
									break;
								}
						}

						amountSuccessComplexFactors++;
					}
				}
                catch (Exception e)
                {
					_log.ForContext("error message ==>", e.Message).Error("ЭО. Ошибка во время прохода по комплексным параметрам");
	                msg = "Ошибка во время прохода по комплексным параметрам";
	                return 0;
                }

				
				SetProgressCalculate(progress);
				if (isBreak)
				{
					WriteByColumnToComplexMatrix(costFactorsDataForReport, costTargetObjectDataForReport);
					costFactorsDataForReport.Clear();
					costTargetObjectDataForReport.Clear();
					continue;
				}

				res.Add(Math.Round(cost, MathRoundCostPrecision));
				successAnalogIds.Add(analog.Id);

				if (calculateSquareCost.DealTypeShort == DealTypeShort.Sale)
				{
					AddReportDictValue(ref costFactorsDataForReport, new KeyValuePair<string, string>("Скорректированная стоимость руб/кв.м", Math.Round(cost, MathRoundCostPrecision).ToString(ToStringFormatCostPrecision)));
				}

				if (calculateSquareCost.DealTypeShort == DealTypeShort.Rent)
				{
					AddReportDictValue(ref costFactorsDataForReport, new KeyValuePair<string, string>("Скорректированная арендная ставка объектов-аналогов, руб/кв.м/год", (Math.Round(cost, MathRoundCostPrecision)* 12).ToString(ToStringFormatCostPrecision)));
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

			return Math.Round(res.Sum(x => x) / res.Count, MathRoundCostPrecision);
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

		        var qsConGroup = new QSConditionGroup(QSConditionGroupType.And);
		        qsConGroup.Add(new QSConditionSimple
		        {
			        ConditionType = QSConditionType.NotEqual,
			        LeftOperand = OMCoreObject.GetColumn(x => x.ProcessType_Code),
			        RightOperand = new QSColumnConstant(ProcessStep.Excluded)
				});

				qsConGroup.Add(kn != null ? new QSConditionSimple
				{
					ConditionType = QSConditionType.Equal,
					LeftOperand = OMCoreObject.GetColumn(x => x.CadastralNumber),
					RightOperand = new QSColumnConstant(kn)
				} : new QSConditionSimple
				{
					ConditionType = QSConditionType.Equal,
					LeftOperand = OMCoreObject.GetColumn(x => x.Id),
					RightOperand = new QSColumnConstant(id)
				});

				QSQuery coreObject = new QSQuery
		        {
			        MainRegisterID = OMCoreObject.GetRegisterId(),
			        Condition = qsConGroup
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
			res.Headers.Add(new Header {DataField = "specificCost", Text = "Удельная стоимость за кв.м"});

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
					analogCostFactors.Add(new AttributePure { Id = (int)OMCoreObject.GetColumnAttributeId(x => x.CadastralNumber), Value = analogResult.Kn });
					analogCostFactors.Add(new AttributePure { Id = (int)OMCoreObject.GetColumnAttributeId(x => x.Address), Value = analogResult.Address });
					analogCostFactors.Add(new AttributePure { Id = (int)OMCoreObject.GetColumnAttributeId(x => x.Price), Value = analogResult.Price.ToString() });
					analogCostFactors.AddRange(GetAnalogCostFactors(costFactors, id: (int)analogResult.Id));

					List<Cell> row = new List<Cell>
					{
						new Cell
						{
							Key = "id",
							CommonValue = analogResult.Id.ToString()
						},
						new Cell
						{
							Key = "specificCost",
							CommonValue = analogResult.Square != 0 ? Math.Round(analogResult.Price/analogResult.Square, MathRoundCostPrecision).ToString(ToStringFormatCostPrecision) : "0"
						}
					};
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
		#endregion


		public string SaveSuccessExpressScore(SaveExpressScoreDto saveExpressScore,  out int id)
		{
			id = 0;
			try
			{
				var kn = OMUnit.Where(x => x.Id == saveExpressScore.TargetObjectId).Select(x => x.CadastralNumber).ExecuteFirstOrDefault()?.CadastralNumber;

				id = saveExpressScore.ExpressScoreId == null
					? AddExpressScore(saveExpressScore, kn)
					:  UpdateCostsExpressScore(saveExpressScore.ExpressScoreId.Value, saveExpressScore.SummaryCost, saveExpressScore.CostSquareMeter);
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				ErrorManager.LogError(e);
				return "Сохранение результатов оценки не выполненно. Подробнее в журнале ошибок";
			}

			return "";
		}

        private int AddExpressScore(SaveExpressScoreDto saveExpressScore, string kn)
        {
            return new OMExpressScore
            {
				Square = saveExpressScore.Square,
                KadastralNumber = kn,
                CostSquareMeter = saveExpressScore.CostSquareMeter,
                DateCost = DateTime.Now.Date,
                SummaryCost = saveExpressScore.SummaryCost,
                Objectid = saveExpressScore.TargetObjectId,
                TargetMarketObjectId = saveExpressScore.TargetMarketObjectId,
                ScenarioType_Code = saveExpressScore.ScenarioType.GetValueOrDefault(),
                SegmentType_Code = saveExpressScore.SegmentType.GetValueOrDefault(),
                DealType_Code = saveExpressScore.DealType.GetValueOrDefault(),
                Address = saveExpressScore.Address,
				CostCalculateFactors = saveExpressScore.ComplexCalculateParameters
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


		public CostFactorsDto GetCostFactorsBySegmentType(MarketSegment segmentType)
		{
			var setting = OMSettingsParams.Where(x => x.SegmentType_Code == segmentType).SelectAll().ExecuteFirstOrDefault();

			if (setting == null || string.IsNullOrEmpty(setting.CostFacrors))
			{
				_log.Error("ЭО.Не найдены настройки или оценочные факторы для сегмента {segmentType}", segmentType);
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

		private void SetRequiredReportParameter(int targetObjectId, List<AnalogDto> analogs, MarketSegment marketSegment, string address, string kn, DealTypeShort dealType)
		{
			try
			{
				ReportService.InitRequiredMatrix(5, analogs.Count + 1);


				//Заполняем данные целевого объекта
				ReportService.AddNameCharacteristicRequiredParam("Сегмент");
				ReportService.AddValueRequiredParam(marketSegment.GetEnumDescription());

				//ReportService.AddNameCharacteristicRequiredParam("Год постройки");
				//ReportService.AddValueRequiredParam(targetObjectYear?.NumberValue.ToString(CultureInfo.InvariantCulture));

				//ReportService.AddNameCharacteristicRequiredParam("Площадь, кв.м");
				//ReportService.AddValueRequiredParam(targetSquare.ToString(CultureInfo.InvariantCulture));

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
					//ReportService.AddValueRequiredParam(analog.YearBuild.ToString(CultureInfo.InvariantCulture));
					//ReportService.AddValueRequiredParam(analog.Square.ToString(CultureInfo.InvariantCulture));
					ReportService.AddValueRequiredParam(analog.Address?.ToString(CultureInfo.InvariantCulture));
					ReportService.AddValueRequiredParam(analog.Kn?.ToString(CultureInfo.InvariantCulture));
					if (dealType == DealTypeShort.Rent)
					{
						ReportService.AddValueRequiredParam((analog.Price * 12).ToString(ToStringFormatCostPrecision));
						ReportService.AddValueRequiredParam(Math.Round(analog.Price * 12 / analog.Square, MathRoundCostPrecision).ToString(ToStringFormatCostPrecision));
					}
					else
					{
						ReportService.AddValueRequiredParam((analog.Price).ToString(ToStringFormatCostPrecision));
						ReportService.AddValueRequiredParam(Math.Round(analog.Price / analog.Square, MathRoundCostPrecision).ToString(ToStringFormatCostPrecision));
					}
				}
			} catch (Exception e)
			{
				ErrorManager.LogError(e);
				_log.Error("ЭО. Ошибки при заполнении обязательных параметров для отчета.");
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

		#region Support For Search
		/// <summary>
		/// Создания условия для поиска
		/// </summary>
		/// <returns>
		/// Возвращает условия для поиска юнитов по оценочным факторам
		/// </returns>
		private QSCondition GetConditionsBySearchAttributes(List<SearchAttribute> searchAttributes)
		{

			#region localFunctions

			QSConditionSimple GetConditionSimpleForSearchAttribute(QSConditionType conditionType, long attributeId, string value, ReferenceItemCodeType referenceType)
			{
				QSConditionSimple res = new QSConditionSimple
				{
					ConditionType = conditionType,
					LeftOperand = new QSColumnSimple(attributeId)
				};

				switch (referenceType)
				{
					case ReferenceItemCodeType.Date:
					{
						if (DateTime.TryParse(value, out DateTime date))
						{
							res.RightOperand = new QSColumnConstant(date);
							break;
						}

						res = null;
						break;
					}

					case ReferenceItemCodeType.Number:
					{
						if (decimal.TryParse(value, out decimal decimalValue))
						{
							res.RightOperand = new QSColumnConstant(decimalValue);
							break;
						}

						res = null;
						break;
					}

					case ReferenceItemCodeType.String:
					{
						res.RightOperand = new QSColumnConstant(value);
						break;
					}
					default: res = null; break;
				}

				if (res == null)
				{
					_log.ForContext("value ==>", value)
						.ForContext("referenceType ==>", referenceType)
						.Warning("ЭО. Для атрибута с ИД {attributeId}, не было создано условие {conditionType}.", attributeId, conditionType.GetEnumDescription());
				}
				return res;
			}

			List<object> StrListToListObjects(List<string> strList, ReferenceItemCodeType referenceType)
			{
				List<object> res = new List<object>();

				switch (referenceType)
				{
					case ReferenceItemCodeType.Number:
					{
						res.AddRange(strList.Where(x => decimal.TryParse(x, out var dRes)).Select(x =>
						{
							decimal.TryParse(x, out var dRes);
							return (object)dRes;

						}).ToList());
						break;
					}
					case ReferenceItemCodeType.Date:
					{
						res.AddRange(strList.Where(x => DateTime.TryParse(x, out var dRes)).Select(x =>
						{
							DateTime.TryParse(x, out var dRes);
							return (object)dRes;

						}).ToList());
						break;
					}
					case ReferenceItemCodeType.String:
					{
						res.AddRange(strList);
						break;
					}
				}

				return res;
			}
			#endregion

			var qsConditionGroup = new QSConditionGroup(QSConditionGroupType.And);
			foreach (var searchAttribute in searchAttributes)
			{
				OMEsReference reference = OMEsReference.Where(x => x.Id == searchAttribute.ReferenceId)
					.Select(x => new {x.UseInterval, x.ValueType_Code}).ExecuteFirstOrDefault();

				if (reference != null && reference.UseInterval.GetValueOrDefault())
				{
					OMEsReferenceItem itemInterval = OMEsReferenceItem
						.Where(x => x.ReferenceId == searchAttribute.ReferenceId && x.CommonValue == searchAttribute.Value).SelectAll().ExecuteFirstOrDefault();

					if (itemInterval != null)
					{
						if (!itemInterval.ValueTo.IsNullOrEmpty())
						{
							qsConditionGroup.Add(GetConditionSimpleForSearchAttribute(QSConditionType.LessOrEqual,
								searchAttribute.IdAttribute, itemInterval.ValueTo, reference.ValueType_Code));
						}
						if(!itemInterval.ValueFrom.IsNullOrEmpty())
						{
							qsConditionGroup.Add(GetConditionSimpleForSearchAttribute(QSConditionType.GreaterOrEqual,
								searchAttribute.IdAttribute, itemInterval.ValueFrom, reference.ValueType_Code));
						}
					}
				} else if (reference != null)
				{
					List<OMEsReferenceItem> items = OMEsReferenceItem
						.Where(x => x.ReferenceId == searchAttribute.ReferenceId &&
						            x.CommonValue == searchAttribute.Value).Select(x => x.Value).Execute();

					List<string> valueList = items.Select(x => x.Value).ToList();

					var objList = StrListToListObjects(valueList, reference.ValueType_Code);

					if (objList.Count > 0)
					{
						qsConditionGroup.Add(new QSConditionSimple
						{
							ConditionType = QSConditionType.In,
							LeftOperand = new QSColumnSimple(searchAttribute.IdAttribute),
							RightOperand = new QSColumnConstant(objList)
						});
					}

				}

			}

			return qsConditionGroup;
		}


		#endregion

		#region support for calculate

		private string GetCommonValueReference(long referenceId, string attributeValue)
		{
			string res = null;
			var reference = OMEsReference.Where(x => x.Id == referenceId).SelectAll()
				.ExecuteFirstOrDefault();
			if (reference != null)
			{
				switch (reference.ValueType_Code)
				{
					case ReferenceItemCodeType.String:
						{
							res = OMEsReferenceItem.Where(x => x.ReferenceId == referenceId && x.Value == attributeValue).Select(x => x.CommonValue)
								.ExecuteFirstOrDefault()?.CommonValue;
							break;
						}
					case ReferenceItemCodeType.Date:
						{
							if (DateTime.TryParse(attributeValue, out var date))
							{
								if (reference.UseInterval.GetValueOrDefault())
								{
									List<DateReferenceInterval> dateReferenceIntervals = OMEsReferenceItem.Where(x => x.ReferenceId == referenceId).SelectAll().Execute()
										.Select(ScoreCommonService.IntervalReferenceToDate).ToList();

									res = dateReferenceIntervals.FirstOrDefault(x => x.KeyTo >= date && x.KeyFrom <= date)?
										.CommonValue;
									return res;
								}

								List<DateReference> dateReferenceItems = OMEsReferenceItem.Where(x => x.ReferenceId == referenceId).SelectAll().Execute()
									.Select(ScoreCommonService.ReferenceToDate).ToList();
								res = dateReferenceItems.FirstOrDefault(x => x.Key == date)?
										.CommonValue;
								return res;
							}
							break;
						}
					case ReferenceItemCodeType.Number:
					{
						if (decimal.TryParse(attributeValue, out var value))
						{
							if (reference.UseInterval.GetValueOrDefault())
							{
								List<NumberReferenceInterval> numberReferenceIntervals = OMEsReferenceItem.Where(x => x.ReferenceId == referenceId).SelectAll().Execute()
									.Select(ScoreCommonService.IntervalReferenceToNumber).ToList();

								res = numberReferenceIntervals.FirstOrDefault(x => x.KeyTo >= value && x.KeyFrom <= value)?
									.CommonValue;
								return res;
							}

							List<NumberReference> dateReferenceItems = OMEsReferenceItem.Where(x => x.ReferenceId == referenceId).SelectAll().Execute()
								.Select(ScoreCommonService.ReferenceToNumber).ToList();
							res = dateReferenceItems.FirstOrDefault(x => x.Key == value)?
								.CommonValue;
							return res;
						}
						break;
						}
				}

			}
			return res;
		}

		private ParameterDataDto GetTargetObjectFactor(long attributeId, int? idAnalog, int targetObjectId, int registerId, List<SearchAttribute> complexCalculateParameters, out string msg)
		{
			msg = "";
			ParameterDataDto targetObjectFactor = null;
			SearchAttribute currentValueFactor =
				complexCalculateParameters.FirstOrDefault(x => x.IdAttribute == attributeId);

			if (currentValueFactor != null)
			{
				var item = OMEsReferenceItem.Where(x => x.ReferenceId == currentValueFactor.ReferenceId
				                             && x.CommonValue == currentValueFactor.Value).SelectAll().ExecuteFirstOrDefault();

				if (item != null)
				{
					targetObjectFactor = new ParameterDataDto
					{
						Id = targetObjectId,
						Value = item.CalculationValue,
						ValueCalculateForm = currentValueFactor.Value,
						ValueFromCalculateForm = true
					};
					return targetObjectFactor;
				}
			}
			{
				try
				{
					if (IsAnalogAttribute(attributeId))
					{
						if (idAnalog != null)
						{
							targetObjectFactor = GetEstimateParametersById((int)idAnalog,
								(int)attributeId, OMCoreObject.GetRegisterId());
						}
					}
					else
					{
						targetObjectFactor = GetEstimateParametersById(targetObjectId,
							(int)attributeId, registerId);
					}

					if (targetObjectFactor == null && IsAnalogAttribute(attributeId))
					{
						targetObjectFactor = new ParameterDataDto();
					}
					else if (targetObjectFactor == null)
					{
						msg = "Во время расчетов в Ко части не был найден объект оценки.";
					}
				}
				catch (Exception e)
				{
					Console.WriteLine(e);
					throw new Exception("Не найденны данные для объекта оценки");
				}
			}

			return targetObjectFactor;
		}

		/// <summary>
		/// Дергается событие для передачи в сигнал р передачи состояния прогресса расчета
		/// </summary>
		/// <param name="progress"> число на которое надо увеличть текущий прогресс</param>
		private void SetProgressCalculate(int progress)
		{
			SummaryCalculateProgress = SummaryCalculateProgress + progress;
			if (SummaryCalculateProgress > 100)
			{
				SummaryCalculateProgress = 100;
			}
			NotifyCalculateProgress?.Invoke(SummaryCalculateProgress);
		}
		#endregion
	}
}