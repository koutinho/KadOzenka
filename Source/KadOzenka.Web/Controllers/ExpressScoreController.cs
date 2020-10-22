using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CIPJS.Models.ExpressScore;
using Core.Register;
using Core.Register.QuerySubsystem;
using Core.Shared.Extensions;
using Core.SRD;
using Core.UI.Registers.CoreUI.Registers;
using KadOzenka.Dal.Enum;
using KadOzenka.Dal.ExpressScore;
using KadOzenka.Dal.ExpressScore.Dto;
using KadOzenka.Dal.GbuObject;
using KadOzenka.Dal.Registers;
using KadOzenka.Dal.ScoreCommon;
using KadOzenka.Dal.Tours;
using KadOzenka.Web.Attributes;
using KadOzenka.Web.Helpers;
using KadOzenka.Web.Models.ExpressScore;
using KadOzenka.Web.Models.MarketObject;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using ObjectModel.Directory;
using ObjectModel.Es;
using ObjectModel.ES;
using ObjectModel.Gbu;
using ObjectModel.KO;
using ObjectModel.Market;
using SRDCoreFunctions = ObjectModel.SRD.SRDCoreFunctions;

namespace KadOzenka.Web.Controllers
{
	public class ExpressScoreController : KoBaseController
	{
		public static readonly decimal OperatingCostsCoefDefaultValue = 0.81m;

		#region Init

		private ExpressScoreService _service;
		private ScoreCommonService ScoreCommonService { get; set; }
		private ViewRenderService _viewRenderService;
		private TourFactorService TourFactorService { get; set; }

		public RegisterAttributeService RegisterAttributeService { get; set; }

		public ExpressScoreController(ExpressScoreService service, ViewRenderService viewRenderService,
			ScoreCommonService scoreCommonService, TourFactorService tourFactorService, RegisterAttributeService registerAttributeService)
		{
			_service = service;
			_viewRenderService = viewRenderService;
            ScoreCommonService = scoreCommonService;
            TourFactorService = tourFactorService;
            RegisterAttributeService = registerAttributeService;
		}

		#endregion

        [SRDFunction(Tag = SRDCoreFunctions.EXPRESSSCORE_CALCULATE)]
		public ActionResult Index()
		{
			return View();
		}

        [SRDFunction(Tag = SRDCoreFunctions.EXPRESSSCORE)]
		public JsonResult GetKadNumber(string address = "")
		{
			QSQuery<OMYandexAddress> qSQuery = OMYandexAddress.Where(x => x.FormalizedAddress != null && x.FormalizedAddress.ToLower() == address.ToLower())
				.Select(x => x.CadastralNumber).Select(x => x.InitialId);

			var yandexAddress = qSQuery.ExecuteFirstOrDefault();

			if (yandexAddress == null) return SendErrorMessage("Кадастровый номер для объекта не найден");

			return Json(new { response = new { kadNumber = yandexAddress.CadastralNumber,  marketObjectId = yandexAddress.InitialId } });
		}

        [SRDFunction(Tag = SRDCoreFunctions.EXPRESSSCORE)]
		public JsonResult GetAddressByKadNumber(string kadNumber)
		{
			var yandexAddress = OMYandexAddress.Where(x => x.CadastralNumber == kadNumber)
                .Select(x => x.FormalizedAddress)
                .Select(x => x.InitialId)
                .ExecuteFirstOrDefault();

			if (yandexAddress == null)
			{
				var commonSetting = OMSettingsParams.Where(x => x.SegmentType_Code == MarketSegment.NoSegment)
					.SelectAll().ExecuteFirstOrDefault();

				if (commonSetting?.BuildCadNumber == null) 
				{
					return SendErrorMessage("Заполните атрибут для поиска кадастрового номера здания");
				}

				var obj = OMMainObject.Where(x => x.CadastralNumber == kadNumber).SelectAll().ExecuteFirstOrDefault();

				if (obj == null)
				{
					return SendErrorMessage("В ГБУ не найден объект с указанным кадастровым номером");
				}

				var attribute = new GbuObjectService().GetAllAttributes(obj.Id, null, new List<long> {commonSetting.BuildCadNumber.GetValueOrDefault()}, DateTime.Now.Date)?.FirstOrDefault();

				if (attribute == null || attribute.StringValue.IsNullOrEmpty())
				{
					return SendErrorMessage("Адрес для объекта не найден");
				}

				yandexAddress = OMYandexAddress.Where(x => x.CadastralNumber == attribute.StringValue)
                    .Select(x => x.FormalizedAddress)
                    .Select(x => x.InitialId)
                    .ExecuteFirstOrDefault();

				if (yandexAddress == null)
				{
					return SendErrorMessage("Адрес для объекта не найден");
				}

			}
			return Json(new { response = new
            {
                address = yandexAddress.FormalizedAddress,
                marketObjectId = yandexAddress.InitialId
            } });
		}

		public ActionResult GetCostFactorsForCalculate(string targetKn, int? targetMarketObjectId, MarketSegment segment)
		{
			return PartialView("~/Views/ExpressScore/Partials/PartialComplexCostFactorsForCalculate.cshtml", _service.GetCostFactorsForCalculate(targetKn, targetMarketObjectId, segment));
		}

        [HttpGet]
        [SRDFunction(Tag = SRDCoreFunctions.EXPRESSSCORE_CALCULATE)]
        public ActionResult TargetObjectSubCard(string targetObjectStr)
        {
            var targetObject = JsonConvert.DeserializeObject<TargetObjectDto>(targetObjectStr);

            var model = TargetObjectModel.ToModel(targetObject);
       
            return PartialView("~/Views/ExpressScore/Partials/TargetObjectSubCard.cshtml", model);
        }

        [HttpPost]
        [SRDFunction(Tag = SRDCoreFunctions.EXPRESSSCORE_CALCULATE)]
        public JsonResult TargetObjectSubCard(TargetObjectModel targetObject)
        {
	        if (!ModelState.IsValid)
		        return GenerateMessageNonValidModel();

	        _service.SetTargetObjectAttribute(targetObject.UnitId, targetObject.Attributes.Select(x => x.ToDto()).ToList());
			return Json(new {success = true, message = "Значения атрибутов успешно сохранены"});
        }

        [SRDFunction(Tag = SRDCoreFunctions.EXPRESSSCORE_CALCULATE)]
		public ActionResult GetNearestObjects([FromQuery] NearestObjectViewModel param)
		{
            if (!ModelState.IsValid)  return GenerateMessageNonValidModel();

            var setting = OMSettingsParams.Where(x => x.SegmentType_Code == param.Segment.GetValueOrDefault()).SelectAll().ExecuteFirstOrDefault();
            if (setting == null) return SendErrorMessage("Не найдены настройки для выбранного сегмента");

            var unitsIds = ScoreCommonService.GetUnitsIdsByCadastralNumber(param.Kn, (int)setting.TourId);
            if (unitsIds.Count == 0) return SendErrorMessage("Выбранный объект не входит в тур или его параметры оценки не заполнены");

            var costFactor = setting.CostFacrors.DeserializeFromXml<CostFactorsDto>();
            if (costFactor.YearBuildId == null && costFactor.YearBuildId == 0) return SendErrorMessage("В настройках не задан атрибут для года постройки.");

            var targetObject = _service.GetTargetObject(setting, costFactor, unitsIds, param.Kn);
            if(targetObject == null) return SendErrorMessage("Не найдены данные для выбранного объекта.");

            List<OMCoreObject> analogs = new List<OMCoreObject>();

			var conditionAnalog = 
				_service.GetSearchConditionForAnalogs(param.DeserializeSearchParameters,param.Segment.GetValueOrDefault(), param.DealType, param.SelectedLng, param.SelectedLat );
			var actualDateCondition = _service.GetActualDateCondition(param.ActualDate.Value);

			QSQuery<OMCoreObject> qSQuery = OMCoreObject.Where(conditionAnalog.And(actualDateCondition))
				.SetJoins(_service.JoinPriceHistory())
				.Select(x => new { x.Id, x.Lat, x.Lng, x.CadastralNumber });

			analogs = qSQuery.Execute().ToList();

			List<CoordinatesDto> objects = _service.CheckAnalogsByKoFactors(analogs, setting, param.DeserializeSearchParameters);
			//objects = analogs.Select(x => new CoordinatesDto{ Id = x.Id, Lat = x.Lat.GetValueOrDefault(), Lng = x.Lng.GetValueOrDefault() }).ToList();
			if (objects.Count == 0) return SendErrorMessage("Объекты аналоги не найдены");

			var coordinatesInput = objects.ToDictionary(x => x.Id.GetValueOrDefault(), y => new CoordinatesDto { Id = y.Id, Lat = y.Lat, Lng = y.Lng });

			var coordinates = _service.GetCoordinatesPointAtSelectedDistance(coordinatesInput, param.SelectedLat.GetValueOrDefault(), param.SelectedLng.GetValueOrDefault(), param.Quality.GetValueOrDefault());

			if (coordinates.Count == 0) return SendErrorMessage("Объекты аналоги не найдены");

			BuildObjectCards(coordinates);

			return Json(new { response = new { coordinates, targetObjectId = targetObject.UnitId } });
		}

		[HttpPost]
        [SRDFunction(Tag = SRDCoreFunctions.EXPRESSSCORE_CALCULATE)]
		public ActionResult CalculateCostTargetObject(CalculateCostTargetObjectViewModel viewModel)
		{
			if (!ModelState.IsValid)
			{
				return GenerateMessageNonValidModel();
			}

			var inputParam = new InputCalculateDto
			{
				Address = viewModel.Address,
				Analogs = _service.GetAnalogsByIds(viewModel.SelectedPoints),
				DealType = viewModel.DealType,
				Floor = viewModel.Floor.GetValueOrDefault(),
				Kn = viewModel.Kn,
				ScenarioType = viewModel.ScenarioType,
				Square = viewModel.Square.GetValueOrDefault(),
				Segment = viewModel.Segment,
				TargetObjectId = viewModel.TargetObjectId.GetValueOrDefault(),
                TargetMarketObjectId = viewModel.TargetMarketObjectId
            };

			string resMsg = _service.CalculateExpressScore(inputParam, out ResultCalculateDto resultCalculate);

			if (!string.IsNullOrEmpty(resMsg))
			{
				return SendErrorMessage(resMsg);
			}

			ViewBag.EsId = resultCalculate.Id;
			return PartialView("Partials/PartialGridResultExpressScore", resultCalculate);
		}

        [SRDFunction(Tag = SRDCoreFunctions.EXPRESSSCORE_HISTORY)]
		public ActionResult AnalogObjectsCard(int objectId)
		{
			var marketIds = OMEsToMarketCoreObject.Where(x => x.EsId == objectId).SelectAll().Execute().Select(x => x.MarketObjectId).ToList();

			ViewBag.Filter = $"10002000={string.Join(',', marketIds)}";
			ViewBag.EsId = objectId;

			return View();
		}

		#region Recalculate Analog
		[HttpGet]
        [SRDFunction(Tag = SRDCoreFunctions.EXPRESSSCORE_CALCULATE)]
		public ActionResult RecalculateAnalog(int esId)
		{
			var analogIds = RegistersVariables.CurrentList?.ToList() ?? new List<long>();
			ViewBag.DeleteAnalogIds = analogIds;
			ViewBag.EsId = esId;
			return View();
		}

		[HttpPost]
        [SRDFunction(Tag = SRDCoreFunctions.EXPRESSSCORE_CALCULATE)]
		public JsonResult RecalculateAnalog([FromForm]List<int> analogIds, [FromForm]int expressScoreId)
		{
			if (analogIds.Count == 0)
			{
				return SendErrorMessage("Выберите аналоги");
			}
			var obj = OMExpressScore.Where(x => x.Id == expressScoreId).SelectAll().ExecuteFirstOrDefault();

			if (obj == null)
			{
				return SendErrorMessage("Не найдена оценка");
            }

			var inputParam = new InputCalculateDto
			{
				Address = obj.Address,
				Kn = obj.KadastralNumber,
				Analogs = _service.GetAnalogsByIds(analogIds),
				DealType = obj.DealType_Code == DealType.RentDeal ? DealTypeShort.Rent : DealTypeShort.Sale,
				Floor = (int) obj.Floor,
				ScenarioType = obj.ScenarioType_Code,
				Square = obj.Square,
				Segment = obj.SegmentType_Code,
				TargetObjectId = (int) obj.Objectid,
                TargetMarketObjectId = obj.TargetMarketObjectId
			};

			string resMsg = _service.RecalculateExpressScore(inputParam, analogIds, expressScoreId, out decimal cost, out decimal squareCost, out long reportId);

			if (!string.IsNullOrEmpty(resMsg))
			{
				return SendErrorMessage(resMsg);
			}

			if (inputParam.DealType == DealTypeShort.Rent)
			{
				squareCost *= 12;
				cost *= 12;
			}

			return Json( new {success = new { cost, squareCost, reportId } });
		}

		#endregion

		#region Setting ExpressScore

        [SRDFunction(Tag = SRDCoreFunctions.EXPRESSSCORE_CONSTRUCTOR)]
		public ActionResult ConstructorExpressScore()
		{
            return View();
		}

        [SRDFunction(Tag = SRDCoreFunctions.EXPRESSSCORE_CONSTRUCTOR)]
		public ActionResult SettingsExpressScore(int segmentId)
		{
			var model = new SettingsExpressScoreViewModel();
			var esSetting = OMSettingsParams.Where(x => x.SegmentType_Code == (MarketSegment)segmentId).SelectAll()
				.ExecuteFirstOrDefault();

			if (esSetting == null)
			{
				esSetting = new OMSettingsParams();
			}
			model.TourId = esSetting.TourId;
			model.FactorRegisterId = esSetting.Registerid;
			model.CostFactors = esSetting.CostFacrors?.DeserializeFromXml<CostFactorsDto>() ?? new CostFactorsDto
			{
				ComplexCostFactors = new List<ComplexCostFactor>(),
				SimpleCostFactors = new List<SimpleCostFactor>()
			};
			model.SegmentType = (MarketSegment) segmentId;
			model.IsEdit =
				SRDSession.Current.CheckAccessToFunction(ObjectModel.SRD.SRDCoreFunctions.EXPRESSSCORE_CONSTRUCTOR_EDIT);
			return PartialView("Partials/SettingsExpressScore", model);
		}

		[HttpPost]
        [SRDFunction(Tag = SRDCoreFunctions.EXPRESSSCORE_CONSTRUCTOR_EDIT)]
		public JsonResult SettingsExpressScore(SettingsExpressScoreViewModel viewModel)
		{
			if (!ModelState.IsValid)
			{
				return GenerateMessageNonValidModel();
			}
			var setting = OMSettingsParams.Where(x => x.SegmentType_Code == viewModel.SegmentType.GetValueOrDefault()).SelectAll()
				.ExecuteFirstOrDefault();

			if (setting == null)
			{
				setting = new OMSettingsParams
				{
					SegmentType_Code = viewModel.SegmentType.GetValueOrDefault(),
					CostFacrors = viewModel.CostFactors.SerializeToXml(),
					Registerid = viewModel.FactorRegisterId.GetValueOrDefault(),
					TourId = viewModel.TourId.GetValueOrDefault()
				};
			}
			else
			{
				setting.CostFacrors = viewModel.CostFactors.SerializeToXml();
				setting.Registerid = viewModel.FactorRegisterId.GetValueOrDefault();
				setting.TourId = viewModel.TourId.GetValueOrDefault();
			}

			setting.Save();

			return Json(new {success = true});
		}

        [SRDFunction(Tag = SRDCoreFunctions.EXPRESSSCORE)]
		public JsonResult GetDictionaries()
		{
			var dictionaries = OMEsReference.Where(x => x).SelectAll().Execute().Select(x => new
			{
				Text = x.Name,
				Value = (int)x.Id
			}).ToList();

			dictionaries.Insert(0, new { Text = "",  Value = 0});

			return Json(dictionaries);
		}

		[SRDFunction(Tag = SRDCoreFunctions.EXPRESSSCORE)]
		public JsonResult GetAttributesKo(int registerId)
		{
			var attributes = RegisterCache.RegisterAttributes.Values.Where(x => x.RegisterId == registerId && !x.IsPrimaryKey).Select(x => new
			{
				Text = x.Name,
				Value = x.Id
			}).ToList();

			return Json(attributes);
		}

		[SRDFunction(Tag = SRDCoreFunctions.EXPRESSSCORE)]
		public JsonResult GetAttributesKoAndAnalogs(int registerId)
		{
			var attributes = RegisterCache.RegisterAttributes.Values.Where(x => x.RegisterId == registerId && !x.IsPrimaryKey).Select(x => new
			{
				Text = x.Name,
				Value = x.Id,
				x.RegisterId,
			}).ToList();

			var availableAttributeTypes = new[]
			{
				Consts.IntegerAttributeType, Consts.DecimalAttributeType,
				Consts.StringAttributeType, Consts.DateAttributeType
			};

			var marketObjectAttributes = RegisterAttributeService
				.GetActiveRegisterAttributes(OMCoreObject.GetRegisterId())
				.Where(x => availableAttributeTypes.Contains(x.Type)).ToList();

			var tourFactorsRegister = RegisterCache.Registers.Values.FirstOrDefault(x => x.Id == attributes.FirstOrDefault()?.RegisterId);
			var tourAttributesTree = new DropDownTreeItemModel
			{
				Value = Guid.NewGuid().ToString(),
				Text = tourFactorsRegister?.Description,
				HasChildren = attributes.Count > 0,
				Items = attributes.Select(x => new DropDownTreeItemModel
				{
					Value = x.Value.ToString(),
					Text = x.Text
				}).ToList()
			};

			var marketObjectsRegister = RegisterCache.Registers.Values.FirstOrDefault(x => x.Id == OMCoreObject.GetRegisterId());
			var marketObjectsRegisterAttributes = new DropDownTreeItemModel
			{
				Value = Guid.NewGuid().ToString(),
				Text = marketObjectsRegister?.Description,
				HasChildren = marketObjectAttributes.Count > 0,
				Items = marketObjectAttributes.Select(x => new DropDownTreeItemModel
				{
					Value = x.Id.ToString(),
					Text = x.Name
				}).ToList()
			};

			var fullTree = new List<DropDownTreeItemModel>
			{
				tourAttributesTree,
				marketObjectsRegisterAttributes
			};

			return Json(fullTree);
		}

		[SRDFunction(Tag = SRDCoreFunctions.EXPRESSSCORE)]
		public JsonResult GetFactorRegisters(int tourId)
		{
			var registerFactors = OMTourFactorRegister.Where(x => x.TourId == tourId).SelectAll().Execute().Select(x => new SelectListItem
			{
				Text = RegisterCache.Registers.Values.FirstOrDefault(y => y.Id == x.RegisterId)?.Description,
				Value = x.RegisterId.ToString()
			}).GroupBy(x => x.Value).Select(x => new SelectListItem
			{
				Value = x.Key,
				Text = x.Select(y => y.Text).FirstOrDefault()
			}).ToList();
			return Json(registerFactors);
		}

        [SRDFunction(Tag = SRDCoreFunctions.EXPRESSSCORE)]
		public ActionResult AddNewComplexCard(int count, ComplexCostFactorSpecialization factorSpecialization = ComplexCostFactorSpecialization.Common)
		{
			ViewBag.Count = count;
			ViewBag.IsEdit = SRDSession.Current.CheckAccessToFunction(ObjectModel.SRD.SRDCoreFunctions.EXPRESSSCORE_CONSTRUCTOR_EDIT);
			return PartialView("Partials/PartialComplexFactorCard", new ComplexCostFactor(factorSpecialization));
		}

        [SRDFunction(Tag = SRDCoreFunctions.EXPRESSSCORE)]
		public ActionResult AddNewSimpleCard(int count)
		{
			ViewBag.Count = count;
			ViewBag.IsEdit = SRDSession.Current.CheckAccessToFunction(ObjectModel.SRD.SRDCoreFunctions.EXPRESSSCORE_CONSTRUCTOR_EDIT);
			return PartialView("Partials/PartialSimpleFactorCard", new SimpleCostFactor());
		}

		[HttpGet]
		[SRDFunction(Tag = SRDCoreFunctions.EXPRESSSCORE_CONSTRUCTOR_EDIT)]
		public ActionResult SetCommonSetting()
		{
			ViewData["TreeAttributes"] = new GbuObjectService().GetGbuAttributesTree()
				.Select(x => new DropDownTreeItemModel
				{
					Value = Guid.NewGuid().ToString(),
					Text = x.Text,
					Items = x.Items.Select(y => new DropDownTreeItemModel
					{
						Value = y.Value,
						Text = y.Text
					}).ToList()
				}).AsEnumerable();

			var setting = OMSettingsParams.Where(x => x.SegmentType_Code == MarketSegment.NoSegment).SelectAll()
				.ExecuteFirstOrDefault();

			return View(new SetCommonAttributeEsViewModel {CadastralNumbeGbuAttributeId = setting?.BuildCadNumber});
		}

		[HttpPost]
		[SRDFunction(Tag = SRDCoreFunctions.EXPRESSSCORE_CONSTRUCTOR_EDIT)]
		public JsonResult SetCommonSetting(SetCommonAttributeEsViewModel model)
		{
			if (!ModelState.IsValid)
			{
				return GenerateMessageNonValidModel();
			}

			var setting = OMSettingsParams.Where(x => x.SegmentType_Code == MarketSegment.NoSegment).SelectAll()
				.ExecuteFirstOrDefault();

			if (setting == null)
			{
				setting = new OMSettingsParams
				{
					SegmentType_Code = MarketSegment.NoSegment,
					TourId = 0,
					Registerid = 0,
					BuildCadNumber = model.CadastralNumbeGbuAttributeId
				};
			}
			else
			{
				setting.BuildCadNumber = model.CadastralNumbeGbuAttributeId;
			}

			setting.Save();

			return Json(new
			{
				Success = "Сохранено успешно"
			});
		}


		#endregion

		#region Support Methods

		private void BuildObjectCards(List<CoordinatesDto> coordinates)
		{
			var resultObjectIds = coordinates.Select(x => x.Id).ToList();
			var resultObjects = 
				OMCoreObject
				.Where(x => resultObjectIds.Contains(x.Id))
				.Select(x => new { x.Id, x.Images, x.Price, x.PricePerMeter, x.Area, x.Address, x.CadastralNumber, x.PropertyMarketSegment, x.DealType, x.Market_Code, x.PropertyTypesCIPJS_Code })
				.Execute();

			coordinates.ForEach(x =>
			{
				var resultObject = resultObjects.FirstOrDefault(y => y.Id == x.Id);
				x.ObjectMiniCard = _viewRenderService.ToString("MarketObjects/ObjectMiniCard", CoreObjectDto.MapToMiniCard(resultObject));
				x.ObjectMiniCardContent = _viewRenderService.ToString("MarketObjects/ObjectMiniCardContent", CoreObjectDto.MapToMiniCard(resultObject));
				x.Images = resultObject.Images;
				x.Price = resultObject.Price;
				x.PricePerMeter = resultObject.PricePerMeter;
				x.Area = resultObject.Area;
				x.Address = resultObject.Address;
				x.CadastralNumber = resultObject.CadastralNumber;
				x.PropertyMarketSegment = resultObject.PropertyMarketSegment;
				x.DealType = resultObject.DealType;
				x.Market_Code = resultObject.Market_Code;
				x.PropertyTypesCIPJS_Code = resultObject.PropertyTypesCIPJS_Code;
			});
		}

		public JsonResult GetEsDictionary(int? dictionaryId)
		{
			List<SelectListItem> res = new List<SelectListItem>();

			if (dictionaryId != null)
			{
				var dictionaries = OMEsReferenceItem.Where(x => x.ReferenceId == dictionaryId).SelectAll().Execute();
				if (dictionaries != null)
				{
					res.AddRange(dictionaries.Where(x => !x.CommonValue.IsNullOrEmpty()).Select(x => new SelectListItem
					{
						Text = x.CommonValue,
						Value = x.CommonValue
					}));
				}
			}

			return Json(res.DistinctBy(x => x.Text));

		}

		#endregion
	}
}

