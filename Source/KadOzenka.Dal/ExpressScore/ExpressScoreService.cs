using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using Core.ErrorManagment;
using Core.Register;
using Core.Register.QuerySubsystem;
using Core.Shared.Extensions;
using KadOzenka.Dal.Enum;
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

		private ExpressScoreReportService ReportService { get; }
		public ExpressScoreService(ScoreCommonService scoreCommonService)
		{
			ReportService = new ExpressScoreReportService();
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

        public TargetObjectDto GetTargetObject(OMSettingsParams setting, CostFactorsDto costFactor, List<long> unitIds)
        {
            var tourRegisterPrimaryKeyId = RegisterCache.RegisterAttributes.Values
                .FirstOrDefault(x => x.RegisterId == setting.Registerid && x.IsPrimaryKey)?.Id;

            var query = ScoreCommonService.GetQsQuery((int)setting.Registerid, (int)tourRegisterPrimaryKeyId.GetValueOrDefault(), unitIds);

            query.AddColumn((long)costFactor.YearBuildId.GetValueOrDefault(), ((int)costFactor.YearBuildId.GetValueOrDefault()).ToString());
            foreach (var factor in costFactor.ComplexCostFactors)
            {
                query.AddColumn(factor.AttributeId.GetValueOrDefault(), factor.AttributeId.GetValueOrDefault().ToString());
            }

            var table = query.ExecuteQuery();
            var results = new List<TargetObjectDto>();
            foreach (DataRow row in table.Rows)
            {
                var rowId = row["Id"].ParseToLong();
                var rowAttributes = new List<AttributePure>();
                foreach (var factor in costFactor.ComplexCostFactors)
                {
                    rowAttributes.Add(new AttributePure
                    {
                        Id = factor.AttributeId.GetValueOrDefault(),
                        Value = row[factor.AttributeId.ToString()].ParseToStringNullable()
                    });
                }
                results.Add(new TargetObjectDto(rowId, rowAttributes));
            }

            return results.OrderByDescending(x => x.UnitId).FirstOrDefault();
        }

        public QSCondition GetSearchCondition(OMYearConstruction yearRange, OMSquare squareRange, bool useYearBuild, bool useSquare, MarketSegment marketSegment, List<DealType> dealType)
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
			});

			if (useYearBuild)
			{
				condition.Add(new QSConditionSimple
				{
					ConditionType = QSConditionType.IsNotNull,
					LeftOperand = OMCoreObject.GetColumn(x => x.BuildingYear),
				}.And(new QSConditionSimple
				{
					ConditionType = QSConditionType.LessOrEqual,
					LeftOperand = OMCoreObject.GetColumn(x => x.BuildingYear),
					RightOperand = new QSColumnConstant(yearRange?.YearTo)
				}).And(new QSConditionSimple
				{
					ConditionType = QSConditionType.GreaterOrEqual,
					LeftOperand = OMCoreObject.GetColumn(x => x.BuildingYear),
					RightOperand = new QSColumnConstant(yearRange?.YearFrom)
				}));
			}

			if (useSquare)
			{
				condition.Add(new QSConditionSimple
				{
					ConditionType = QSConditionType.LessOrEqual,
					LeftOperand = OMCoreObject.GetColumn(x => x.Area),
					RightOperand = new QSColumnConstant(squareRange?.SquareTo)
				}.And(new QSConditionSimple
				{
					ConditionType = QSConditionType.GreaterOrEqual,
					LeftOperand = OMCoreObject.GetColumn(x => x.Area),
					RightOperand = new QSColumnConstant(squareRange?.SquareFrom)
				}));
			}

			return condition;
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
					x.FloorNumber,
					x.BuildingYear,
					x.Address,
					x.DealType_Code
				}).Execute().Select(x => new AnalogDto
				{
					Id = x.Id,
					Kn = x.CadastralNumber,
					Price = x.Price.GetValueOrDefault(),
					Square = x.Area.GetValueOrDefault(),
					Date = x.LastDateUpdate ?? x.ParserTime ?? DateTime.MinValue,
					FloorsCount = x.FloorsCount.GetValueOrDefault(),
					Floor = x.FloorNumber.GetValueOrDefault(),
					YearBuild = x.BuildingYear.GetValueOrDefault(),
					Address = x.Address,
					DealType = x.DealType_Code
				}).ToList();
		}

		public string CalculateExpressScore(InputCalculateDto inputParam, out ResultCalculateDto resultCalculate)
		{
			SetRequiredReportParameter(inputParam.TargetObjectId, inputParam.Square, inputParam.Analogs, inputParam.Segment, inputParam.Address, inputParam.Kn);

            resultCalculate = new ResultCalculateDto();
			var squareCost = CalculateSquareCost(inputParam.Analogs, inputParam.TargetObjectId, inputParam.Floor, inputParam.Segment,
				out string msg, out List<long> successAnalogIds, inputParam.DealType, inputParam.ScenarioType);

			var summaryCost = Math.Round(squareCost * inputParam.Square, 2);
			if (squareCost == 0)
			{
				return string.IsNullOrEmpty(msg) ? "При расчете что то пошло не так" : msg;
			}

			DealType dealType = inputParam.DealType == DealTypeShort.Rent ? DealType.RentDeal : DealType.SaleDeal;
			msg = SaveSuccessExpressScore(inputParam.TargetObjectId, summaryCost, squareCost, out int id, square: inputParam.Square, floor: inputParam.Floor, scenarioType: inputParam.ScenarioType, 
				segmentType: inputParam.Segment, dealType: dealType, address: inputParam.Address);
			if (!string.IsNullOrEmpty(msg)) return msg;

			msg = AddDependenceEsFromMarketCoreObject(id, successAnalogIds);

			resultCalculate.SquareCost = Math.Round(squareCost, 2);
			resultCalculate.SummaryCost = summaryCost;
			resultCalculate.Id = id;

			resultCalculate.ReportId = ReportService.GenerateReport(summaryCost, squareCost, inputParam.DealType, inputParam.ScenarioType);

			var resultAnalogs = inputParam.Analogs.Where(x => successAnalogIds.Contains(x.Id)).Select(x => new AnalogResultDto
			{
				Id = x.Id,
				Address = x.Address,
				Floor = x.Floor,
				Square = x.Square,
				Source = inputParam.Segment.GetEnumDescription(),
				Price = x.Price
			}).ToList();

			resultCalculate.Analogs = resultAnalogs;
			resultCalculate.DealType = inputParam.DealType;

			return msg;
		}

		public string RecalculateExpressScore(InputCalculateDto inputParam,  List<int> analogIds,
			  int expressScoreId,  out decimal cost, out decimal squareCost, out long reportId)
		{
			cost = 0;
			squareCost = 0;
			reportId = 0;

			SetRequiredReportParameter(inputParam.TargetObjectId, inputParam.Square, inputParam.Analogs, inputParam.Segment, inputParam.Address, inputParam.Kn);
			squareCost = CalculateSquareCost(inputParam.Analogs.Where(x => analogIds.Contains((int)x.Id)).ToList(),
				inputParam.TargetObjectId, inputParam.Floor,inputParam.Segment, out string msg, out var successAnalogIds, inputParam.DealType, inputParam.ScenarioType);
			if (!string.IsNullOrEmpty(msg)) return msg;

			cost = Math.Round(squareCost * inputParam.Square, 2);
			squareCost = Math.Round(squareCost, 2);

			reportId = ReportService.GenerateReport(cost, squareCost, inputParam.DealType, inputParam.ScenarioType);
			msg = SaveSuccessExpressScore(inputParam.TargetObjectId, cost, squareCost, out int id, expressScoreId);
			if (!string.IsNullOrEmpty(msg)) return msg;

			return msg;
		}

        private decimal CalculateSquareCost(List<AnalogDto> analogs, int targetObjectId, int targetObjectFloor,
			MarketSegment marketSegment, out string msg, out List<long> successAnalogIds, DealTypeShort dealTypeShort,
			ScenarioType? scenarioType = null)
		{
			msg = "";
			List<decimal> res = new List<decimal>();
			successAnalogIds = new List<long>();
			bool needWriteTargetObjectDataToReport = true;

			var exSettingsCostFactors = GetSetting(marketSegment);

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
				msg = "Не заданы обязательные настройки";
				return 0;
			}

            List<Tuple<string, string>> costFactorsDataForReport = new List<Tuple<string, string>>();
			List<string> costTargetObjectDataForReport = new List<string>();

			foreach (var analog in analogs)
			{
				decimal yPrice = 0; // Удельный показатель стоимости

				if (analog.Price != 0 && analog.Square != 0)
				{
					yPrice = analog.Price / analog.Square;
				}

				// Обязательные факторы

				#region Корректировка на дату

				var dateDict = OMEsReferenceItem.Where(x => x.ReferenceId == exCostFactors.IndexDateDicId).SelectAll().Execute()
					.Select(ScoreCommonService.ReferenceToDate).ToList();

				var dateEstimate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
				costTargetObjectDataForReport.Add(DateTime.Now.ToShortDateString());

				var indexDateEstimate = dateDict.FirstOrDefault(x => x.Key == dateEstimate) ??
				                        dateDict.OrderByDescending(x => x.Key).First();
				costTargetObjectDataForReport.Add(indexDateEstimate.Value.ToString(CultureInfo.InvariantCulture));

				DateReference indexAnalogDate = null;

				var text = new KeyValuePair<string,string>("Дата предложения", "");
				var dicText = new KeyValuePair<string,string>(@" Индекс ""Дата предложения""", "");
				
				if (analog.Date != DateTime.MinValue)
				{
					var analogDate = new DateTime(analog.Date.Year, analog.Date.Month,
						1);

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

				text = new KeyValuePair<string, string>("Этажность", value: analog.FloorsCount.ToString());
				dicText = new KeyValuePair<string, string>("Корректировка на долю земельного участка (Кдзу)", value: "1");
				double fixedCoefflandShareZero = 0.8; // По требованию заказчика если этажность 0 то коэф 0,8

				if (scenarioType != null && scenarioType == ScenarioType.Oks)
				{
					var dateNumb = OMEsReferenceItem.Where(x => x.ReferenceId == exCostFactors.LandShareDicId).SelectAll().Execute()
						.Select(ScoreCommonService.ReferenceToNumber).ToList();

					var coefficient = dateNumb.FirstOrDefault(x => x.Key == analog.FloorsCount)?.Value ??
					                  dateNumb.Last()?.Value ?? null;

					if (analog.FloorsCount == 0 && coefficient == null)
					{
						coefficient = (decimal)fixedCoefflandShareZero;
					}

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

				#region Корректировка на этаж

				{
					var floor = (int)analog.Floor != 0 ? (int)analog.Floor : 1; // по условию из примера расчета
					text = new KeyValuePair<string, string>("Этаж расположения", floor.ToString());

					var floorDict = OMEsReferenceItem.Where(x => x.ReferenceId == exCostFactors.FloorDicId).SelectAll().Execute()
						.Select(ScoreCommonService.ReferenceToNumber).OrderByDescending(x => x.Key).ToList().ToList();

					var floorFactor = floor != 0 && floorDict.Count > 0
						? floor > floorDict.FirstOrDefault()?.Key ? floorDict.FirstOrDefault()?.Value :
						floorDict.FirstOrDefault(x => x.Key == floor)?.Value
						: 0;
					dicText = new KeyValuePair<string, string>(@"Коэффициент ""Этаж расположения""", floorFactor?.ToString("N"));

					var targetObjectFloorFactor = floorDict.Count > 0
						? targetObjectFloor > floorDict.FirstOrDefault().Key
							? floorDict.FirstOrDefault()?.Value
							: floorDict.FirstOrDefault(x => x.Key == targetObjectFloor)?.Value : 0;

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
					costTargetObjectDataForReport.Add(targetObjectFloor.ToString());
					costTargetObjectDataForReport.Add(targetObjectFloorFactor?.ToString("N"));
					costTargetObjectDataForReport.Add("");
				}

				#endregion

				if (dealTypeShort == DealTypeShort.Sale)
				{
					costFactorsDataForReport.Add(new Tuple<string, string>("Вид сделки (Предложение-продажа/Сделка купли-продажи)", analog.DealType.GetEnumDescription()));
					costTargetObjectDataForReport.Add("");
				}

				if (dealTypeShort == DealTypeShort.Rent)
				{
					costFactorsDataForReport.Add(new Tuple<string, string>("Вид сделки (Предложение-аренда/Сделка-аренда)", analog.DealType.GetEnumDescription()));
					costTargetObjectDataForReport.Add("");
				}

				foreach (var simple in exCostFactors.SimpleCostFactors)
				{
                    text = new KeyValuePair<string, string>("Корректировка " + @"""" +simple.Name + @"""", simple.Coefficient?.ToString("N"));
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
				int kCount = 2;
				foreach (var complex in exCostFactors.ComplexCostFactors)
				{
					var targetObjectFactor = GetEstimateParametersById(targetObjectId,
						complex.AttributeId.GetValueOrDefault(), marketSegment, (int) exSettingsCostFactors.Registerid);

					if (targetObjectFactor == null)
					{
						msg = "Не найденны данные для объекта оценки";
						return 0;
					}

					var analogFactor = GetEstimateParametersByKn(analog.Kn, (int) exSettingsCostFactors.TourId,
						complex.AttributeId.GetValueOrDefault(), marketSegment, (int) exSettingsCostFactors.Registerid);

					if (analogFactor == null)
					{
						isBreak = true;
						break;
					}

					string valueToComplexName = analogFactor.NumberValue != 0 ? analogFactor.NumberValue.ToString("N") : analogFactor
						.Value.ToString();
					AddReportDictValue(ref costFactorsDataForReport, new KeyValuePair<string, string>(complex.Name, valueToComplexName));
					costTargetObjectDataForReport.Add(targetObjectFactor.Value.ToString());

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

                                if (analogC == 0 || targetObjectC == 0)
								{
									AddReportDictValue(ref costFactorsDataForReport, new KeyValuePair<string, string>("Метка " + @""""+complex.Name+ @"""", analogC != 0 ? analogC.ToString("N") : ""));
									costTargetObjectDataForReport.Add(targetObjectC != 0 ? targetObjectC.ToString("N") : "");
									AddReportDictValue(ref costFactorsDataForReport, new KeyValuePair<string, string>("Степень влияния", complex.Coefficient?.ToString("N")));
									costTargetObjectDataForReport.Add(complex.Coefficient?.ToString("N"));
										AddReportDictValue(ref costFactorsDataForReport, new KeyValuePair<string, string>("Корректировка " + @""""+complex.Name + @"""" + $" K({kCount})", "1"));
									costTargetObjectDataForReport.Add("");
									break;
								}

								AddReportDictValue(ref costFactorsDataForReport, new KeyValuePair<string, string>("Метка " + @""""+complex.Name + @"""", analogC.ToString("N")));
								costTargetObjectDataForReport.Add(targetObjectC.ToString("N"));
								AddReportDictValue(ref costFactorsDataForReport, new KeyValuePair<string, string>("Степень влияния", complex.Coefficient?.ToString("N")));
								costTargetObjectDataForReport.Add(complex.Coefficient?.ToString("N"));

									var coeff = Math.Exp((double) (targetObjectC * complex.Coefficient.GetValueOrDefault())) /
								            Math.Exp((double) (analogC * complex.Coefficient.GetValueOrDefault()));

								AddReportDictValue(ref costFactorsDataForReport, new KeyValuePair<string, string>("Корректировка " + @""""+complex.Name + @"""" + $" K({kCount})", coeff.ToString("N")));

                                try
                                {
                                    cost = cost * (decimal)coeff;
                                }
                                catch (OverflowException e)
                                {
                                    GenerateOverflowException(e, analog.Kn, $"Оценочный фактор: {complex.Name}", complex.Coefficient);
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
								decimal targetObjectC = ScoreCommonService.GetCoefficientFromDateFactor(targetObjectFactor,
									complex.DictionaryId.GetValueOrDefault());

								if (analogC == 0 || targetObjectC == 0)
								{
									AddReportDictValue(ref costFactorsDataForReport, new KeyValuePair<string, string>("Метка " + @""""+complex.Name + @"""", analogC != 0 ? analogC.ToString("N"):  ""));
									costTargetObjectDataForReport.Add(targetObjectC != 0 ? targetObjectC.ToString("N") : "");
									AddReportDictValue(ref costFactorsDataForReport, new KeyValuePair<string, string>("Степень влияния", complex.Coefficient.ToString()));
									costTargetObjectDataForReport.Add(complex.Coefficient.ToString());
										AddReportDictValue(ref costFactorsDataForReport, new KeyValuePair<string, string>("Корректировка " + @""""+complex.Name+@"""" + $" K({kCount})", "1"));
									costTargetObjectDataForReport.Add("");
									break;
								}

								AddReportDictValue(ref costFactorsDataForReport, new KeyValuePair<string, string>("Метка " + @""""+complex.Name+@"""", analogC.ToString("N")));
								costTargetObjectDataForReport.Add(targetObjectC.ToString("N"));
								AddReportDictValue(ref costFactorsDataForReport, new KeyValuePair<string, string>("Степень влияния", complex.Coefficient?.ToString("N")));
								costTargetObjectDataForReport.Add(complex.Coefficient?.ToString("N"));

									var coeff = Math.Exp((double)(targetObjectC * complex.Coefficient.GetValueOrDefault())) / Math.Exp((double)(analogC * complex.Coefficient.GetValueOrDefault()));
								AddReportDictValue(ref costFactorsDataForReport, new KeyValuePair<string, string>("Корректировка " + @""""+complex.Name + @"""" + $" K({kCount})", coeff.ToString("N")));
								costTargetObjectDataForReport.Add("");

                                try
                                {
                                    cost = cost * (decimal)coeff;
                                }
                                catch (OverflowException e)
                                {
                                    GenerateOverflowException(e, analog.Kn, $"Оценочный фактор: {complex.Name}", complex.Coefficient);
                                }
                            }

							break;
						}
						case ParameterType.Number:
						{
							decimal analogC = ScoreCommonService.GetCoefficientFromNumberFactor(analogFactor,
								complex.DictionaryId.GetValueOrDefault());
							decimal targetObjectC = ScoreCommonService.GetCoefficientFromNumberFactor(targetObjectFactor,
								complex.DictionaryId.GetValueOrDefault());

							if (analogC == 0 || targetObjectC == 0)
							{
								if (complex.DictionaryId != null && complex.DictionaryId != 0)
								{
									AddReportDictValue(ref costFactorsDataForReport, new KeyValuePair<string, string>("Метка " + @""""+complex.Name + @"""", analogC != 0 ? analogC.ToString("N") : ""));
									costTargetObjectDataForReport.Add(targetObjectC != 0 ? targetObjectC.ToString("N") : "");
								}

								AddReportDictValue(ref costFactorsDataForReport, new KeyValuePair<string, string>("Степень влияния", complex.Coefficient?.ToString("N")));
								costTargetObjectDataForReport.Add(complex.Coefficient?.ToString("N"));

								AddReportDictValue(ref costFactorsDataForReport, new KeyValuePair<string, string>("Корректировка " + @""""+complex.Name + @"""" + $" K({kCount})", "1"));
								costTargetObjectDataForReport.Add("");
									break;
							}

							if (complex.DictionaryId != null && complex.DictionaryId != 0)
							{
								AddReportDictValue(ref costFactorsDataForReport, new KeyValuePair<string, string>("Метка " + @""""+complex.Name + @"""", analogC.ToString("N")));
								costTargetObjectDataForReport.Add(targetObjectC != 0 ? targetObjectC.ToString("N") : "");
							}

							AddReportDictValue(ref costFactorsDataForReport, new KeyValuePair<string, string>("Степень влияния", complex.Coefficient?.ToString("N")));
							costTargetObjectDataForReport.Add(complex.Coefficient?.ToString("N"));

								var coeff = Math.Exp((double) (targetObjectC * complex.Coefficient.GetValueOrDefault())) /
							            Math.Exp((double) (analogC * complex.Coefficient.GetValueOrDefault()));

								AddReportDictValue(ref costFactorsDataForReport, new KeyValuePair<string, string>("Корректировка " + @""""+complex.Name + @"""" + $" K({kCount})", coeff.ToString("N")));
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
							if (complex.DictionaryId != null && complex.DictionaryId != 0)
							{
								AddReportDictValue(ref costFactorsDataForReport, new KeyValuePair<string, string>("Метка " + @""""+complex.Name + @"""", ""));
								costTargetObjectDataForReport.Add("");
							}
							AddReportDictValue(ref costFactorsDataForReport, new KeyValuePair<string, string>("Степень влияния", complex.Coefficient?.ToString("N")));
							costTargetObjectDataForReport.Add(complex.Coefficient?.ToString("N"));
							AddReportDictValue(ref costFactorsDataForReport, new KeyValuePair<string, string>("Корректировка " + @""""+complex.Name + @"""" + $" K({kCount})", "1"));
							costTargetObjectDataForReport.Add("");
							break;
						}
					}

					kCount++;
				}

				int countRow = isBreak
					? costFactorsDataForReport.Count + GetCountReportRowComplexFactors(exCostFactors.ComplexCostFactors) 
					: costFactorsDataForReport.Count;

				countRow++;/*Строка на скорректированную стоимость*/

				ReportService.InitCostFactorMatrix(countRow, analogs.Count + 1);

				if (isBreak)
				{
					WriteByColumnToComplexMatrix(costFactorsDataForReport, costTargetObjectDataForReport);
					costFactorsDataForReport.Clear();
					costTargetObjectDataForReport.Clear();
					continue;
				}

				res.Add(Math.Round(cost, 2));
				successAnalogIds.Add(analog.Id);

				if (dealTypeShort == DealTypeShort.Sale)
				{
					AddReportDictValue(ref costFactorsDataForReport, new KeyValuePair<string, string>("Скорректированная стоимость руб/кв.м", Math.Round(cost, 2).ToString("N")));
				}

				if (dealTypeShort == DealTypeShort.Rent)
				{
					AddReportDictValue(ref costFactorsDataForReport, new KeyValuePair<string, string>("Скорректированная арендная ставка объектов-аналогов, руб/кв.м/год", Math.Round(cost * 12, 2).ToString("N")));
				}
				costTargetObjectDataForReport.Add("");

				WriteByColumnToComplexMatrix(costFactorsDataForReport, costTargetObjectDataForReport, needWriteTargetObjectDataToReport);
				needWriteTargetObjectDataToReport = false;
				costFactorsDataForReport.Clear();
				costTargetObjectDataForReport.Clear();
			}

			if (res.Count == 0)
			{
					msg = "Не один аналог не подошел для расчета.";
					return 0;
			}

			return res.Sum(x => x) / res.Count;
		}

        //генерируем новое исключение, чтобы не потерять стек
        private void GenerateOverflowException(Exception e, string cadastralNumber, string multipliedName, object multipliedValue)
        {
            var message = $"Во время обработки аналога '{cadastralNumber}' возникло переполнение при умножении Стоимости на '{multipliedName} ({multipliedValue})'.";

            throw (Exception)Activator.CreateInstance(e.GetType(), message, e);
        }

        public string SaveSuccessExpressScore(int targetObjectId, decimal summaryCost, decimal costSquareMeter, out int id, int? expressScoreId = null,
			decimal? square = null, int? floor = null, ScenarioType? scenarioType = null, MarketSegment? segmentType = null, DealType? dealType = null, string address = null)
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
					? AddExpressScore(kn, summaryCost, costSquareMeter, square.Value, floor.Value, targetObjectId, scenarioType.GetValueOrDefault(), segmentType.GetValueOrDefault(), dealType.GetValueOrDefault(), address)
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

		private int AddExpressScore(string kn, decimal cost, decimal costSquareMeter, decimal square, int floor, int targetObjectId, ScenarioType scenarioType, MarketSegment segmentType, DealType dealType, string address)
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

		private void SetRequiredReportParameter(int targetObjectId, decimal targetSquare, List<AnalogDto> analogs, MarketSegment marketSegment, string address, string kn)
		{
			ReportService.InitRequiredMatrix(7, analogs.Count + 1);

			var exSettingsCostFactors = OMSettingsParams.Where(x => x.SegmentType_Code == marketSegment).SelectAll()
				.ExecuteFirstOrDefault();

			var costFactor = GetCostFactorsBySegmentType(marketSegment);

			var targetObjectYear = GetEstimateParametersById(targetObjectId,
				(int)costFactor.YearBuildId.GetValueOrDefault(), marketSegment, (int)exSettingsCostFactors.Registerid);
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

			ReportService.AddNameCharacteristicRequiredParam("Стоимость объекта-аналога, руб/год");
			ReportService.AddValueRequiredParam("-");

			ReportService.AddNameCharacteristicRequiredParam("Стоимость объекта-аналога, руб/кв.м/год");
			ReportService.AddValueRequiredParam("-");

			foreach (AnalogDto analog in analogs)
			{
				ReportService.SetNextColumnRequiredParam();
				ReportService.AddValueRequiredParam(marketSegment.GetEnumDescription());
				ReportService.AddValueRequiredParam(analog.YearBuild.ToString(CultureInfo.InvariantCulture));
				ReportService.AddValueRequiredParam(analog.Square.ToString(CultureInfo.InvariantCulture));
				ReportService.AddValueRequiredParam(analog.Address.ToString(CultureInfo.InvariantCulture));
				ReportService.AddValueRequiredParam(analog.Kn.ToString(CultureInfo.InvariantCulture));
				ReportService.AddValueRequiredParam((analog.Price  * 12).ToString("N"));
				ReportService.AddValueRequiredParam(Math.Round(analog.Price * 12 / analog.Square, 2).ToString("N"));
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
    }
}