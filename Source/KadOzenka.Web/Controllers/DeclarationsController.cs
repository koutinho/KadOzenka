using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Transactions;
using Core.ErrorManagment;
using Core.Register;
using Core.Register.RegisterEntities;
using Core.SRD;
using KadOzenka.Web.Models.Declarations;
using KadOzenka.Web.Models.Declarations.DeclarationTabModel;
using Microsoft.AspNetCore.Mvc;
using ObjectModel.Core.SRD;
using ObjectModel.Declarations;
using ObjectModel.Directory.Declarations;
using Core.Shared.Extensions;
using Core.UI.Registers.CoreUI.Registers;
using Core.UI.Registers.Models.CoreUi;
using KadOzenka.Dal.CommonFunctions;
using Newtonsoft.Json;
using ObjectModel.Core.Reports;
using Microsoft.AspNetCore.Http;
using KadOzenka.Web.Models.DataUpload;
using KadOzenka.Dal.DataExport;
using KadOzenka.Dal.DataImport;
using KadOzenka.Dal.Declarations;
using KadOzenka.Dal.GbuObject;
using KadOzenka.Web.Attributes;
using ObjectModel.Core.Shared;
using ObjectModel.SPD;

namespace KadOzenka.Web.Controllers
{
	public class DeclarationsController : KoBaseController
    {
	    private NotificationService _notificationService;
		private DeclarationService _declarationService;

		public DeclarationsController(IGbuObjectService gbuObjectService, IRegisterCacheWrapper registerCacheWrapper)
			: base(gbuObjectService, registerCacheWrapper)
		{
			_notificationService = new NotificationService();
			_declarationService = new DeclarationService();
		}


		#region Declarations

		[HttpGet]
		public ActionResult EditDeclaration(long declarationId)
		{
			var declaration = OMDeclaration
					.Where(x => x.Id == declarationId)
					.SelectAll()
					.ExecuteFirstOrDefault();

			OMSubject owner = null;
			if (declaration?.Owner_Id != null)
			{
				owner = OMSubject
					.Where(x => x.Id == declaration.Owner_Id)
					.SelectAll()
					.ExecuteFirstOrDefault();
			}
			OMSubject agent = null;
			if (declaration?.Agent_Id != null)
			{
				agent = OMSubject
					.Where(x => x.Id == declaration.Agent_Id)
					.SelectAll()
					.ExecuteFirstOrDefault();
			}
			OMBook book = null;
			if (declaration != null)
			{
				book = OMBook
					.Where(x => x.Id == declaration.Book_Id)
					.SelectAll()
					.ExecuteFirstOrDefault();
			}
			OMUser userIsp = null;
			if (declaration?.UserIsp_Id != null)
			{
				userIsp = OMUser
					.Where(x => x.Id == declaration.UserIsp_Id)
					.SelectAll()
					.ExecuteFirstOrDefault();
			}
			else
			{
				userIsp = OMUser
					.Where(x => x.Id == SRDSession.GetCurrentUserId())
					.SelectAll()
					.ExecuteFirstOrDefault();
			}
			OMResult result = null;
			if (declaration != null)
			{
				result = OMResult
					.Where(x => x.Declaration_Id == declaration.Id)
					.SelectAll()
					.ExecuteFirstOrDefault();
			}
			var model = declaration != null
				? DeclarationModel.FromEntity(declaration, owner, agent, book, userIsp, result == null ? new OMResult() : result)
				: DeclarationModel.FromEntity(null, null, null, null, userIsp, null);

			CheckDeclarationPermissions(ref model);


			return View(model);
		}

		[HttpPost]
		public ActionResult EditDeclaration(DeclarationModel declarationViewModel)
		{
			if (!ModelState.IsValid)
			{
				return GenerateMessageNonValidModel();
			}

			var omDeclaration = OMDeclaration
				.Where(x => x.Id == declarationViewModel.Id)
				.SelectAll()
				.ExecuteFirstOrDefault();
			var omResult = OMResult
				.Where(x => x.Declaration_Id == declarationViewModel.Id)
				.SelectAll()
				.ExecuteFirstOrDefault();
			omResult = omResult == null ? new OMResult() : omResult;
			if (declarationViewModel.Id != -1 && omDeclaration == null)
			{
				return NotFound();
			}
			if (omDeclaration == null)
			{
				SRDSession.Current.CheckAccessToFunction(ObjectModel.SRD.SRDCoreFunctions.DECLARATIONS_DECLARATION_CREATE, true, false, true);
				omDeclaration = new OMDeclaration();
				omDeclaration.UserReg_Id = SRDSession.GetCurrentUserId();
				omResult = new OMResult();
			}
			else
			{
				SRDSession.Current.CheckAccessToFunction(ObjectModel.SRD.SRDCoreFunctions.DECLARATIONS_DECLARATION_EDIT, true, false, true);
			}
			DeclarationModel.ToEntity(declarationViewModel, ref omDeclaration, ref omResult);

			long id;
			using (var ts = new TransactionScope())
			{
				try
				{
					id = omDeclaration.Save();
					omResult.Declaration_Id = id;
					omResult.Save();
					ts.Complete();
				}
				catch (Exception e)
				{
				    return SendErrorMessage(e.Message);
				}
			}

			declarationViewModel.Id = id;

			CheckDeclarationPermissions(ref declarationViewModel);
			return Json(new { Success = "Сохранено успешно", data = declarationViewModel });
		}

		[SRDFunction(Tag = ObjectModel.SRD.SRDCoreFunctions.DECLARATIONS_DECLARATION)]
		public IActionResult GetSynchronizedDates(DateTime? dateIn, DateTime? durationDateIn, StatusDec? status)
		{
			_declarationService.CalculateSynchronizedDates(dateIn, durationDateIn, status, out var newDurationDateIn,
				out var dateCheckPlan, out var checkTime);

			return Json(new { DurationDateIn = newDurationDateIn, DateCheckPlan = dateCheckPlan, CheckTime = checkTime });
		}

		[SRDFunction(Tag = ObjectModel.SRD.SRDCoreFunctions.DECLARATIONS_DECLARATION)]
		public IActionResult GetSynchronizedCheckTimeDate(DateTime? durationDateIn, StatusDec? status)
		{
			_declarationService.CalculateSynchronizedCheckTimeDate(durationDateIn, status, out var checkTime);
			return Json(new { CheckTime = checkTime });
		}

		[HttpGet]
		public IActionResult CreateFromSpd(long spdId)
		{
			if (spdId == 0)
			{
				return NoContent();
			}

			var userIsp = OMUser
				.Where(x => x.Id == SRDSession.GetCurrentUserId())
				.SelectAll()
				.ExecuteFirstOrDefault();

			var request = OMRequestRegistration.Where(x => x.Id == spdId).SelectAll().ExecuteFirstOrDefault();
			OMSubject subject = null;
			try
			{
				subject = _declarationService.GetOrCreateSubject(request);
			}
			catch (Exception e)
			{
				ErrorManager.LogError(e);
				Console.WriteLine(e);
				throw new Exception("Во время получения отправителя возникли ошибки. Подробнее в журнале ошибок");
			}

			var model = DeclarationModel.FromEntity(null, null, null, null, userIsp, null);
			model.SpdAppId = request.AppId;
			model.SpdAppDate = request.AppDate.GetValueOrDefault();
			model.SpdAppName = request.AppName;
			model.FromSpd = true;
			model.OwnerDisplay = subject != null
				? subject.Type_Code == SubjectType.Ul
					? subject.Name
					: $"{subject.F_Name} {subject.I_Name} {subject.O_Name}"
				: null;
			model.OwnerId = subject?.Id;

			CheckDeclarationPermissions(ref model);

			return View("EditDeclaration", model);
		}

		public IQueryable GetAutoCompleteSubject(string searchText)
		{
			return OMSubject
				.Where(x => ((x.Name.StartsWith(searchText) && (long)x.Type_Code == (long)SubjectType.Ul) || (x.F_Name.StartsWith(searchText) && (long)x.Type_Code == (long)SubjectType.Fl)))
				.SelectAll().Execute().Select(x => new
				{
					x.Id,
					Value = x.Type_Code == SubjectType.Ul ? x.Name : $"{x.F_Name} {x.I_Name} {x.O_Name}"
				}).AsQueryable();
		}

		public IQueryable GetAutoCompleteBook(string searchText)
		{
			return OMBook
				.Where(x => x.Prefics.StartsWith(searchText, true, CultureInfo.CurrentCulture))
				.SelectAll().Execute().Select(x => new
				{
					x.Id,
					Value = x.Prefics
				}).AsQueryable();
		}

		[HttpGet]
		public JsonResult GetSubjectData(int subjectId)
		{
			var subject = OMSubject
				.Where(x => x.Id == subjectId)
				.SelectAll()
				.Execute().Select(x => new
				{
					x.Id,
					Value = x.Type_Code == SubjectType.Ul ? x.Name : $"{x.F_Name} {x.I_Name} {x.O_Name}"
				}).FirstOrDefault();

			return Json(new { data = subject });
		}

		[HttpGet]
		public JsonResult GetBookData(int bookId)
		{
			var book = OMBook
				.Where(x => x.Id == bookId)
				.SelectAll()
				.Execute().Select(x => new
				{
					x.Id,
					Value = x.Prefics
				}).FirstOrDefault();

			return Json(new { data = book });
		}

		public ActionResult GetNotificationTabContent(long declarationId)
		{
			var declaration = OMDeclaration
				.Where(x => x.Id == declarationId)
				.SelectAll()
				.ExecuteFirstOrDefault();
			var model = DeclarationNotificationModel.FromEntity(declaration);
			model.IsEditApproveNotifications = SRDSession.Current.CheckAccessToFunction(ObjectModel.SRD.SRDCoreFunctions.DECLARATIONS_DECLARATION_EDIT_NOTIFICATIONS_APPROVE_NOTIFICATION);
			model.IsEditOtherNotifications = SRDSession.Current.CheckAccessToFunction(ObjectModel.SRD.SRDCoreFunctions.DECLARATIONS_DECLARATION_EDIT_NOTIFICATIONS_OTHER_NOTIFICATIONS);

			ViewBag.UseSpd = declaration != null && declaration.SpdAppId != null;
			return PartialView("~/Views/Declarations/DeclarationTabContent/NotificationContent.cshtml", model);
		}

		public ActionResult GetCharacteristicTabContent(long declarationId)
		{
			var declaration = OMDeclaration
				.Where(x => x.Id == declarationId)
				.SelectAll()
				.ExecuteFirstOrDefault();

			long? characteristicId = null;
			if (declaration != null)
			{
				if (declaration.TypeObj_Code == ObjectType.Site)
				{
					characteristicId = OMHarParcel
						.Where(x => x.Declaration_Id == declarationId)
						.SelectAll()
						.ExecuteFirstOrDefault()?.Id;
				}
				else
				{
					characteristicId = OMHarOKS
						.Where(x => x.Declaration_Id == declarationId)
						.SelectAll()
						.ExecuteFirstOrDefault()?.Id;
				}
			}

			var model = DeclarationCharacteristicModel.FromEntity(characteristicId, declaration);

			return PartialView("~/Views/Declarations/DeclarationTabContent/CharacteristicContent.cshtml", model);
		}

		public ActionResult EditParcelCharacteristics(long declarationId)
		{
			var declaration = OMDeclaration
				.Where(x => x.Id == declarationId)
				.SelectAll()
				.ExecuteFirstOrDefault();
			if (declaration == null)
			{
				throw new Exception($"Декларация с ИД {declarationId} не найдена");
			}

			var characteristic = OMHarParcel
				.Where(x => x.Declaration_Id == declarationId)
				.SelectAll()
				.ExecuteFirstOrDefault();
			List<OMHarParcelAdditionalInfo> characteristicAdditionalInfo = null;
			if (characteristic != null)
			{
				characteristicAdditionalInfo = OMHarParcelAdditionalInfo.Where(x => x.HarParcelId == characteristic.Id)
					.SelectAll()
					.Execute();
			}


			var model = characteristic != null
				? ParcelCharacteristicsModel.FromEntity(characteristic, characteristicAdditionalInfo)
				: ParcelCharacteristicsModel.FromEntity(null, null);
			model.DeclarationId = declarationId;
			model.IsEditDeclarationCharacteristics =
				SRDSession.Current.CheckAccessToFunction(ObjectModel.SRD.SRDCoreFunctions.DECLARATIONS_DECLARATION_EDIT_CHARACTERISTICS);
			model.CanIncludeInFormalChecking = SRDSession.Current.CheckAccessToFunction(ObjectModel.SRD.SRDCoreFunctions.DECLARATIONS_DECLARATION_EDIT_CHARACTERISTICS_INCLUDE_IN_FORMAL_CHECKING);

			return View("~/Views/Declarations/ParcelCharacteristicsWindowContent.cshtml", model);
		}

		[HttpPost]
		public ActionResult EditParcelCharacteristics(ParcelCharacteristicsModel parcelCharacteristicsViewModel)
		{
			SRDSession.Current.CheckAccessToFunction(ObjectModel.SRD.SRDCoreFunctions.DECLARATIONS_DECLARATION_EDIT_CHARACTERISTICS, true, false, true);
			if (!ModelState.IsValid)
			{
				return GenerateMessageNonValidModel();
			}

			var characteristic = OMHarParcel
				.Where(x => x.Id == parcelCharacteristicsViewModel.Id)
				.SelectAll()
				.ExecuteFirstOrDefault();

			if (parcelCharacteristicsViewModel.Id != -1 && characteristic == null)
			{
				return NotFound();
			}

			List<OMHarParcelAdditionalInfo> characteristicAdditionalInfo = null;
			if (characteristic == null)
			{
				characteristic = new OMHarParcel();
				characteristicAdditionalInfo = new List<OMHarParcelAdditionalInfo>();
			}
			else
			{
				characteristicAdditionalInfo = OMHarParcelAdditionalInfo.Where(x => x.HarParcelId == characteristic.Id)
					.SelectAll()
					.Execute();
			}

			var result = OMResult
				.Where(x => x.Declaration_Id == parcelCharacteristicsViewModel.DeclarationId)
				.SelectAll()
				.ExecuteFirstOrDefault();
			if (result == null)
			{
			    return SendErrorMessage(
			        $"Не найдены результаты для декларации с идентификатором {parcelCharacteristicsViewModel.DeclarationId}");
			}

			ParcelCharacteristicsModel.ToEntity(parcelCharacteristicsViewModel, ref characteristic, ref characteristicAdditionalInfo);
			long id;
		    try
		    {
		        using (var ts = new TransactionScope())
		        {
		            id = characteristic.Save();
		            foreach (var omHarOksAdditionalInfo in characteristicAdditionalInfo)
		            {
		                omHarOksAdditionalInfo.HarParcelId = id;
		                omHarOksAdditionalInfo.Save();
		            }

					var textYes = parcelCharacteristicsViewModel.GetAcceptedCharacteristics();
					var textNo = parcelCharacteristicsViewModel.GetRejectedCharacteristics();

					if (result.TextYes != textYes
					    || result.TextNo != textNo)
					{
						SRDSession.Current.CheckAccessToFunction(ObjectModel.SRD.SRDCoreFunctions.DECLARATIONS_DECLARATION_EDIT_CHARACTERISTICS_INCLUDE_IN_FORMAL_CHECKING, true, false, true);
					}

					result.TextYes = textYes;
					result.TextNo = textNo;
					result.Save();

		            ts.Complete();
		        }
		    }
		    catch (Exception ex)
		    {
		        return SendErrorMessage(ex.Message);
		    }

		    parcelCharacteristicsViewModel.Id = id;
			parcelCharacteristicsViewModel.IsEditDeclarationCharacteristics =
				SRDSession.Current.CheckAccessToFunction(ObjectModel.SRD.SRDCoreFunctions.DECLARATIONS_DECLARATION_EDIT_CHARACTERISTICS);
			parcelCharacteristicsViewModel.CanIncludeInFormalChecking = SRDSession.Current.CheckAccessToFunction(ObjectModel.SRD.SRDCoreFunctions.DECLARATIONS_DECLARATION_EDIT_CHARACTERISTICS_INCLUDE_IN_FORMAL_CHECKING);
			return Json(new { Success = "Сохранено успешно", data = parcelCharacteristicsViewModel });
		}

		public ActionResult EditOksCharacteristics(long declarationId)
		{
			var declaration = OMDeclaration
				.Where(x => x.Id == declarationId)
				.SelectAll()
				.ExecuteFirstOrDefault();
			if (declaration == null)
			{
				throw new Exception($"Декларация с ИД {declarationId} не найдена");
			}

			var characteristic = OMHarOKS
				.Where(x => x.Declaration_Id == declarationId)
				.SelectAll()
				.ExecuteFirstOrDefault();
			List<OMHarOKSAdditionalInfo> characteristicAdditionalInfo = null;
			if (characteristic != null)
			{
				characteristicAdditionalInfo = OMHarOKSAdditionalInfo.Where(x => x.HarOKSId == characteristic.Id)
					.SelectAll()
					.Execute();
			}
			
			var model = characteristic != null
				? OksCharacteristicsModel.FromEntity(characteristic, characteristicAdditionalInfo)
				: OksCharacteristicsModel.FromEntity(null, null);
			model.DeclarationId = declarationId;
			model.IsEditDeclarationCharacteristics =
				SRDSession.Current.CheckAccessToFunction(ObjectModel.SRD.SRDCoreFunctions.DECLARATIONS_DECLARATION_EDIT_CHARACTERISTICS);
			model.CanIncludeInFormalChecking = SRDSession.Current.CheckAccessToFunction(ObjectModel.SRD.SRDCoreFunctions.DECLARATIONS_DECLARATION_EDIT_CHARACTERISTICS_INCLUDE_IN_FORMAL_CHECKING);

			return View("~/Views/Declarations/OksCharacteristicsWindowContent.cshtml", model);
		}

		[HttpPost]
		public ActionResult EditOksCharacteristics(OksCharacteristicsModel oksCharacteristicsViewModel)
		{
			SRDSession.Current.CheckAccessToFunction(ObjectModel.SRD.SRDCoreFunctions.DECLARATIONS_DECLARATION_EDIT_CHARACTERISTICS, true, false, true);
			if (!ModelState.IsValid)
			{
				return GenerateMessageNonValidModel();
			}

			var characteristic = OMHarOKS
				.Where(x => x.Id == oksCharacteristicsViewModel.Id)
				.SelectAll()
				.ExecuteFirstOrDefault();

			if (oksCharacteristicsViewModel.Id != -1 && characteristic == null)
			{
				return NotFound();
			}

			var result = OMResult
				.Where(x => x.Declaration_Id == oksCharacteristicsViewModel.DeclarationId)
				.SelectAll()
				.ExecuteFirstOrDefault();
			if (result == null)
			{
			    return SendErrorMessage(
			        $"Не найдены результаты для декларации с идентификатором {oksCharacteristicsViewModel.DeclarationId}");
            }

			List<OMHarOKSAdditionalInfo> characteristicAdditionalInfo = null;
			if (characteristic == null)
			{
				characteristic = new OMHarOKS();
				characteristicAdditionalInfo = new List<OMHarOKSAdditionalInfo>();
			}
			else
			{
				characteristicAdditionalInfo = OMHarOKSAdditionalInfo.Where(x => x.HarOKSId == characteristic.Id)
					.SelectAll()
					.Execute();
			}

			OksCharacteristicsModel.ToEntity(oksCharacteristicsViewModel, ref characteristic, ref characteristicAdditionalInfo);
			long id;
		    try
		    {
		        using (var ts = new TransactionScope())
		        {
		            id = characteristic.Save();

		            foreach (var omHarOksAdditionalInfo in characteristicAdditionalInfo)
		            {
		                omHarOksAdditionalInfo.HarOKSId = id;
		                omHarOksAdditionalInfo.Save();
		            }

					var textYes = oksCharacteristicsViewModel.GetAcceptedCharacteristics();
					var textNo = oksCharacteristicsViewModel.GetRejectedCharacteristics();
					
					if (result.TextYes != textYes
						|| result.TextNo != textNo)
					{
						SRDSession.Current.CheckAccessToFunction(ObjectModel.SRD.SRDCoreFunctions.DECLARATIONS_DECLARATION_EDIT_CHARACTERISTICS_INCLUDE_IN_FORMAL_CHECKING, true, false, true);
					}

					result.TextYes = textYes;
					result.TextNo = textNo;
		            result.Save();

		            ts.Complete();
		        }
		    }
		    catch (Exception ex)
		    {
		        return SendErrorMessage(ex.Message);
		    }

		    oksCharacteristicsViewModel.Id = id;
			oksCharacteristicsViewModel.IsEditDeclarationCharacteristics =
				SRDSession.Current.CheckAccessToFunction(ObjectModel.SRD.SRDCoreFunctions.DECLARATIONS_DECLARATION_EDIT_CHARACTERISTICS);
			oksCharacteristicsViewModel.CanIncludeInFormalChecking = SRDSession.Current.CheckAccessToFunction(ObjectModel.SRD.SRDCoreFunctions.DECLARATIONS_DECLARATION_EDIT_CHARACTERISTICS_INCLUDE_IN_FORMAL_CHECKING);
			return Json(new { Success = "Сохранено успешно", data = oksCharacteristicsViewModel });
		}

		[HttpGet]
		public ActionResult GetNewAcceptedRejectedCharacteristics(long declarationId)
		{
			var result = OMResult
				.Where(x => x.Declaration_Id == declarationId)
				.SelectAll()
				.ExecuteFirstOrDefault();
			if (result == null)
			{
				throw new Exception($"не найдены результаты для декларации с идентификатором {declarationId}");
			}

			return Content(
				JsonConvert.SerializeObject(new
					{acceptedCharacteristics = result.TextYes, rejectedCharacteristics = result.TextNo}),
				"application/json");
		}

		#region DataImport

		[HttpGet]
		public IActionResult DataImport()
		{
			ViewBag.RegisterViewId = "DeclarationsDeclaration";
			ViewBag.MainRegisterId = 501;

			return View();
		}

		public IActionResult GetImportAttributes()
		{
			var source = new List<object>();

			List<RegistersCommon.RegisterTemplateColumn> attributesList = RegistersCommon.BuildAttributesTree("DeclarationsDeclaration");

			if (attributesList.Any())
			{
				var filteredAttributesList =
					attributesList.Where(x =>
						x.ItemId == "501" ||
						(x.ParentId == "501" && x.Description != OMDeclaration.GetAttributeData(y => y.Id).Name
						                     && x.Description != OMDeclaration.GetAttributeData(y => y.UserIsp_Id).Name
						                     && x.Description !=
						                     OMDeclaration.GetAttributeData(y => y.UserReg_Id).Name)).ToList();
				foreach (var attributeItem in filteredAttributesList)
				{
					RegisterAttribute attribute = RegisterCache.RegisterAttributes.ContainsKey(attributeItem.AttributeId) ?
								RegisterCache.RegisterAttributes[attributeItem.AttributeId] : null;
					OMReference reference = attribute != null && attribute.ReferenceId.HasValue ?
						OMReference.Where(r => r.ReferenceId == attribute.ReferenceId.Value).Select(r => r.Description).Execute().FirstOrDefault() : null;
					RegisterRelation relation = attribute != null ?
						RegisterCache.RegisterRelations.Select(r => r.Value).FirstOrDefault(r => r.RegAttributeID == attribute.Id) : null;

					var type = Enum.GetName(typeof(RegisterAttributeType), attributeItem.DocumentType);
					var description = attributeItem.ParentId == null ? attributeItem.Description + $" ({attributeItem.ItemId})" : attributeItem.Description;
					source.Add(new
					{
						attributeItem.AttributeId,
						Description = description,
						DescriptionAttribute = attribute != null ? attribute.Description : string.Empty,
						attributeItem.ParentId,
						attributeItem.ItemId,
						ReferenceId = (attributeItem.ReferenceId != 0 ? attributeItem.ReferenceId : (int?)null),
						Type = type,
						DataTypeName = attribute != null ? attribute.Type.GetEnumDescription() : string.Empty,
						ReferenceName = reference != null ? reference.Description : string.Empty,
						IsPrimaryKey = attribute?.IsPrimaryKey ?? false,
						ForeignKey = (relation != null && RegisterCache.Registers[relation.ParentRegID] != null) ?
									RegisterCache.Registers[relation.ParentRegID].Description : string.Empty,
						IsVirtual = attribute?.IsVirtual ?? false,
						ColumnDbName = attribute != null ? (attribute.ValueField + (attribute.CodeField.IsNotEmpty() ?
									string.Format(" ({0})", attribute.CodeField) : string.Empty)) : string.Empty
					});
				}
			}

			return Json(source);
		}

		[HttpPost]
		public IActionResult AddImportToQueue(IFormFile file, List<DataColumnDto> columns)
		{
			try
			{
				using (var stream = file.OpenReadStream())
				{
					DataImporterDeclarations.AddImportToQueue(OMDeclaration.GetRegisterId(), "DeclarationsDeclaration", file.FileName, stream,
						columns.Select(x => new DataExportColumn
						{ AttributrId = x.AttributeId, ColumnName = x.ColumnName, IsKey = x.IsKey}).ToList());
				}
			}
			catch (Exception e)
			{
				ErrorManager.LogError(e);
				return BadRequest();
			}

			return NoContent();
		}

		#endregion DataImport

		#endregion Declarations

		#region Books

		[HttpGet]
		public ActionResult EditBook(long bookId)
		{
			var book = OMBook
				.Where(x => x.Id == bookId)
				.SelectAll()
				.ExecuteFirstOrDefault();
			var model = book != null 
				? BookModel.FromEntity(book) 
				: BookModel.FromEntity(null);

			model.IsCreateBook =
				SRDSession.Current.CheckAccessToFunction(ObjectModel.SRD.SRDCoreFunctions.DECLARATIONS_BOOK_CREATE);
			model.IsEditBook =
				SRDSession.Current.CheckAccessToFunction(ObjectModel.SRD.SRDCoreFunctions.DECLARATIONS_BOOK_EDIT);
			return View(model);
		}

		[HttpPost]
		public ActionResult EditBook(BookModel bookViewModel)
		{
			if (!ModelState.IsValid)
			{
				return GenerateMessageNonValidModel();
			}

			var omBook = OMBook
				.Where(x => x.Id == bookViewModel.Id)
				.SelectAll()
				.ExecuteFirstOrDefault();
			if (bookViewModel.Id != -1 && omBook == null)
			{
				return NotFound();
			}
			if (omBook == null)
			{
				SRDSession.Current.CheckAccessToFunction(ObjectModel.SRD.SRDCoreFunctions.DECLARATIONS_BOOK_CREATE, true, false, true);
				omBook = new OMBook();
			}
			else
			{
				SRDSession.Current.CheckAccessToFunction(ObjectModel.SRD.SRDCoreFunctions.DECLARATIONS_BOOK_EDIT, true, false, true);
			}
			BookModel.ToEntity(bookViewModel, ref omBook);

			long id;
			try
			{
				id = omBook.Save();
			}
			catch (Exception e)
			{
				return SendErrorMessage(e.Message);
			}

			bookViewModel.Id = id;
			bookViewModel.IsCreateBook =
				SRDSession.Current.CheckAccessToFunction(ObjectModel.SRD.SRDCoreFunctions.DECLARATIONS_BOOK_CREATE);
			bookViewModel.IsEditBook =
				SRDSession.Current.CheckAccessToFunction(ObjectModel.SRD.SRDCoreFunctions.DECLARATIONS_BOOK_EDIT);
			return Json(new { Success = "Сохранено успешно", data = bookViewModel });
		}

		#endregion Books

		#region Subjects

		[HttpGet]
		public ActionResult EditSubject(long subjectId)
		{
			var subject = OMSubject
				.Where(x => x.Id == subjectId)
				.SelectAll()
				.ExecuteFirstOrDefault();
			var model = subject != null
				? SubjectModel.FromEntity(subject)
				: SubjectModel.FromEntity(null);
			model.IsCreateSubject =
				SRDSession.Current.CheckAccessToFunction(ObjectModel.SRD.SRDCoreFunctions.DECLARATIONS_SUBJECT_CREATE);
			model.IsEditSubject =
				SRDSession.Current.CheckAccessToFunction(ObjectModel.SRD.SRDCoreFunctions.DECLARATIONS_SUBJECT_EDIT);

			return View(model);
		}

		[HttpPost]
		public ActionResult EditSubject(SubjectModel subjectViewModel)
		{
			if (!ModelState.IsValid)
			{
				return GenerateMessageNonValidModel();
			}

			var omSubject = OMSubject
				.Where(x => x.Id == subjectViewModel.Id)
				.SelectAll()
				.ExecuteFirstOrDefault();
			if (subjectViewModel.Id != -1 && omSubject == null)
			{
				return NotFound();
			}
			if (omSubject == null)
			{
				SRDSession.Current.CheckAccessToFunction(ObjectModel.SRD.SRDCoreFunctions.DECLARATIONS_SUBJECT_CREATE, true, false, true);
				omSubject = new OMSubject();
			}
			else
			{
				SRDSession.Current.CheckAccessToFunction(ObjectModel.SRD.SRDCoreFunctions.DECLARATIONS_SUBJECT_EDIT, true, false, true);
			}
			SubjectModel.ToEntity(subjectViewModel, ref omSubject);

			long id;
			try
			{
				id = omSubject.Save();
			}
			catch (Exception e)
			{
				return SendErrorMessage(e.Message);
			}

			subjectViewModel.Id = id;
			subjectViewModel.IsCreateSubject =
				SRDSession.Current.CheckAccessToFunction(ObjectModel.SRD.SRDCoreFunctions.DECLARATIONS_SUBJECT_CREATE);
			subjectViewModel.IsEditSubject =
				SRDSession.Current.CheckAccessToFunction(ObjectModel.SRD.SRDCoreFunctions.DECLARATIONS_SUBJECT_EDIT);
			return Json(new { Success = "Сохранено успешно", data = subjectViewModel });
		}

		#endregion Subjects

		#region Notifications

		[HttpGet]
		public ActionResult EditNotification(long notificationId, long declarationId)
		{
			if (declarationId == 0)
			{
				throw new Exception("В указанном запросе отсутствует ИД декларации");
			}

			var declaration = OMDeclaration
				.Where(x => x.Id == declarationId)
				.SelectAll()
				.ExecuteFirstOrDefault();
			if (declaration == null)
			{
				throw new Exception($"Декларация с ИД {declarationId} не найдена");
			}

			var uved = OMUved
				.Where(x => x.Id == notificationId)
				.SelectAll()
				.Execute().FirstOrDefault();

			OMBook book = null;
			List<OMUvedRejectionReasonType> rejectionReasonTypes = null;
			if (uved != null)
			{
				book = OMBook
					.Where(x => x.Id == uved.Book_Id)
					.SelectAll()
					.ExecuteFirstOrDefault();
				if (uved.Type_Code == UvedType.Item5)
				{
					rejectionReasonTypes =
						OMUvedRejectionReasonType.Where(x => x.UvedId == uved.Id).SelectAll().Execute();
				}
			}
			else
			{
				book = OMBook
					.Where(x => x.Id == declaration.Book_Id)
					.SelectAll()
					.ExecuteFirstOrDefault();
			}

			var model = notificationId != 0 && uved != null
				? NotificationModel.FromEntity(uved, book, rejectionReasonTypes)
				: NotificationModel.FromEntity(null, book, rejectionReasonTypes);
			model.DeclarationId = declarationId;
			model.IsEditApproveNotifications = SRDSession.Current.CheckAccessToFunction(ObjectModel.SRD.SRDCoreFunctions.DECLARATIONS_DECLARATION_EDIT_NOTIFICATIONS_APPROVE_NOTIFICATION);
			model.IsEditOtherNotifications = SRDSession.Current.CheckAccessToFunction(ObjectModel.SRD.SRDCoreFunctions.DECLARATIONS_DECLARATION_EDIT_NOTIFICATIONS_OTHER_NOTIFICATIONS);

			return View(model);
		}

		[HttpPost]
		public ActionResult EditNotification(NotificationModel notificationViewModel)
		{
			if (!ModelState.IsValid)
			{
				return GenerateMessageNonValidModel();
            }

			var omUved = OMUved
				.Where(x => x.Id == notificationViewModel.Id)
				.SelectAll()
				.Execute().FirstOrDefault();

			if (notificationViewModel.Id != -1 && omUved == null)
			{
				return NotFound();
			}

			if (omUved == null)
			{
				omUved = new OMUved();
				SRDSession.Current.CheckAccessToFunction(
					notificationViewModel.Type.GetValueOrDefault() == UvedType.Item1
						? ObjectModel.SRD.SRDCoreFunctions
							.DECLARATIONS_DECLARATION_EDIT_NOTIFICATIONS_APPROVE_NOTIFICATION
						: ObjectModel.SRD.SRDCoreFunctions
							.DECLARATIONS_DECLARATION_EDIT_NOTIFICATIONS_OTHER_NOTIFICATIONS, true, false, true);
			}
			else
			{
				SRDSession.Current.CheckAccessToFunction(
					omUved.Type_Code == UvedType.Item1
						? ObjectModel.SRD.SRDCoreFunctions
							.DECLARATIONS_DECLARATION_EDIT_NOTIFICATIONS_APPROVE_NOTIFICATION
						: ObjectModel.SRD.SRDCoreFunctions
							.DECLARATIONS_DECLARATION_EDIT_NOTIFICATIONS_OTHER_NOTIFICATIONS, true, false, true);
			}
			var isNotificationTypeChanged = notificationViewModel.Type.GetValueOrDefault() != omUved.Type_Code;

			NotificationModel.ToEntity(notificationViewModel, ref omUved);
			long id;
		    try
		    {
		        using (var ts = new TransactionScope())
		        {
		            id = omUved.Save();
		            if (isNotificationTypeChanged)
		            {
		                var registerId = OMUved.GetRegisterId();
		                var savedReports = OMSavedReport.Where(x =>
		                        x.ObjectId == id &&
		                        x.ObjectRegisterId == registerId &&
		                        (x.IsDeleted == null || x.IsDeleted == false))
		                    .SelectAll()
		                    .Execute();
		                foreach (var savedReport in savedReports)
		                {
		                    savedReport.IsDeleted = true;
		                    savedReport.Save();
		                }

		                if (notificationViewModel.Type.GetValueOrDefault() != UvedType.Item5)
		                {
		                    var rejectionReasonTypes =
		                        OMUvedRejectionReasonType.Where(x => x.UvedId == id).SelectAll().Execute();
		                    foreach (var rejectionReasonType in rejectionReasonTypes)
		                    {
		                        rejectionReasonType.Destroy();
		                    }
		                }
		            }

		            if (notificationViewModel.Type.GetValueOrDefault() == UvedType.Item5)
		            {
		                var existedRejectionReasonTypes =
		                    OMUvedRejectionReasonType.Where(x => x.UvedId == id).SelectAll().Execute();
		                var addedRejectionReasonTypes = notificationViewModel.RejectionReasonTypes?.Where(x =>
		                    existedRejectionReasonTypes.All(y => y.RejectionReasonType_Code != x));
		                var deletedRejectionReasonTypes = existedRejectionReasonTypes.Where(x =>
		                    notificationViewModel.RejectionReasonTypes != null &&
		                    notificationViewModel.RejectionReasonTypes.All(y => y != x.RejectionReasonType_Code));
		                foreach (var deletedRejectionReasonType in deletedRejectionReasonTypes)
		                {
		                    deletedRejectionReasonType.Destroy();
		                }

		                if (addedRejectionReasonTypes != null)
		                {
		                    foreach (var addedRejectionReasonType in addedRejectionReasonTypes)
		                    {
		                        var rejectionReasonType = new OMUvedRejectionReasonType
		                        {
		                            UvedId = id,
		                            RejectionReasonType_Code = addedRejectionReasonType
		                        };
		                        rejectionReasonType.Save();
		                    }
		                }
		            }

		            ts.Complete();
		        }
		    }
		    catch (Exception ex)
		    {
                return SendErrorMessage(ex.Message);
            }

		    notificationViewModel.Id = id;
			notificationViewModel.IsEditApproveNotifications = SRDSession.Current.CheckAccessToFunction(ObjectModel.SRD.SRDCoreFunctions.DECLARATIONS_DECLARATION_EDIT_NOTIFICATIONS_APPROVE_NOTIFICATION);
			notificationViewModel.IsEditOtherNotifications = SRDSession.Current.CheckAccessToFunction(ObjectModel.SRD.SRDCoreFunctions.DECLARATIONS_DECLARATION_EDIT_NOTIFICATIONS_OTHER_NOTIFICATIONS);
			return Json(new { Success = "Сохранено успешно", data = notificationViewModel });
		}

		[HttpGet, ActionName("DeleteNotification")]
		public IActionResult DeleteNotification(long notificationId)
		{
			var omUved = OMUved
				.Where(x => x.Id == notificationId)
				.SelectAll()
				.Execute().FirstOrDefault();
			if (omUved == null)
			{
				throw new Exception($"Уведомление с ИД {notificationId} не найдено");
			}
			var book = OMBook
				.Where(x => x.Id == omUved.Book_Id)
				.SelectAll()
				.ExecuteFirstOrDefault();

			return View(omUved.Id);
		}

		[HttpPost, ActionName("DeleteNotification")]
		public IActionResult Delete(long notificationId)
		{
			var omUved = OMUved
				.Where(x => x.Id == notificationId)
				.SelectAll()
				.Execute().FirstOrDefault();
			if (omUved == null)
			{
				throw new Exception($"Уведомление с ИД {notificationId} не найдено");
			}

			SRDSession.Current.CheckAccessToFunction(
				omUved.Type_Code == UvedType.Item1
					? ObjectModel.SRD.SRDCoreFunctions
						.DECLARATIONS_DECLARATION_EDIT_NOTIFICATIONS_APPROVE_NOTIFICATION
					: ObjectModel.SRD.SRDCoreFunctions
						.DECLARATIONS_DECLARATION_EDIT_NOTIFICATIONS_OTHER_NOTIFICATIONS, true, false, true);
			
			try
			{
				using (var ts = new TransactionScope())
				{
				    var registerId = OMUved.GetRegisterId();
				    var savedReports = OMSavedReport.Where(x =>
				            x.ObjectId == notificationId &&
				            x.ObjectRegisterId == registerId &&
				            (x.IsDeleted == null || x.IsDeleted == false))
				        .SelectAll()
				        .Execute();
				    foreach (var savedReport in savedReports)
				    {
				        savedReport.IsDeleted = true;
				        savedReport.Save();
				    }

				    var rejectionReasonTypes =
				        OMUvedRejectionReasonType.Where(x => x.UvedId == notificationId).SelectAll().Execute();
				    foreach (var rejectionReasonType in rejectionReasonTypes)
				    {
				        rejectionReasonType.Destroy();
				    }

				    omUved.Destroy();

				    ts.Complete();
				}
			}
			catch (Exception e)
			{
				return SendErrorMessage(e.Message);
			}
			
			return EmptyResponse();
		}

		[HttpGet]
		public IActionResult NotificationReportViewer(long notificationId)
		{
			var omUved = OMUved
				.Where(x => x.Id == notificationId)
				.SelectAll()
				.Execute().FirstOrDefault();
			if (omUved == null)
			{
				throw new Exception($"Уведомление с ИД {notificationId} не найдено");
			}
			var reportType = _notificationService.GetNotificationReportType(omUved);

			return RedirectToAction("Viewer", "Report", new {reportTypeId = reportType, reportRegisterId = OMUved.GetRegisterId(), reportObjectId = notificationId});
		}

		public IActionResult DownloadSavedNotificationReport(long notificationId)
		{
			var omUved = OMUved
				.Where(x => x.Id == notificationId)
				.SelectAll()
				.Execute().FirstOrDefault();
			if (omUved == null)
			{
				throw new Exception($"Уведомление с ИД {notificationId} не найдено");
			}

			var registerId = OMUved.GetRegisterId();
			var reportType = _notificationService.GetNotificationReportType(omUved);
			var savedReport = OMSavedReport.Where(x =>
					x.ObjectId == notificationId &&
					x.ObjectRegisterId == registerId &&
					x.Code == reportType &&
					(x.IsDeleted == null || x.IsDeleted == false))
				.Execute()
				.FirstOrDefault();

			if (savedReport == null)
			{
				throw new Exception("Сохраненный отчет не найден.");
			}

			return RedirectToAction("DownloadSavedReport", "Report", new { savedReportId = savedReport.Id });
		}

		public IActionResult SendNotificationToSpd(long notificationId)
		{
			try
			{
				_notificationService.SendNotificationToSpd(notificationId);
			}
			catch (Exception e)
			{
				ErrorManager.LogError(e);
				return View("~/Views/Shared/ModalDialogDetails.cshtml", new ModalDialogDetails
				{
					Message = "Уведомление не было отправлено. Подробнее в журнале ошибок.",
					Icon = ModalDialogDetails.IconType.Warning,
					Buttons = ModalDialogDetails.ButtonType.Ok,
					Action = ModalDialogDetails.ActionType.Reload
				});
			}

			return View("~/Views/Shared/ModalDialogDetails.cshtml", new ModalDialogDetails
			{
				Message = "Уведомление было отправлено",
				Icon = ModalDialogDetails.IconType.Ok,
				Buttons = ModalDialogDetails.ButtonType.Ok,
				Action = ModalDialogDetails.ActionType.Reload
			});
		}

		#endregion Notifications

		#region Signatories

		[HttpGet]
		public ActionResult EditSignatory(long signatoryId)
		{
			var signatory = OMSignatory
				.Where(x => x.Id == signatoryId)
				.SelectAll()
				.ExecuteFirstOrDefault();
			var model = signatory != null
				? SignatoryModel.FromEntity(signatory)
				: SignatoryModel.FromEntity(null);
			model.IsCreateSignatory =
				SRDSession.Current.CheckAccessToFunction(ObjectModel.SRD.SRDCoreFunctions.DECLARATIONS_SIGNATORY_CREATE);
			model.IsEditSignatory =
				SRDSession.Current.CheckAccessToFunction(ObjectModel.SRD.SRDCoreFunctions.DECLARATIONS_SIGNATORY_EDIT);

			return View(model);
		}

		[HttpPost]
		public ActionResult EditSignatory(SignatoryModel signatoryViewModel)
		{
			if (!ModelState.IsValid)
			{
				return GenerateMessageNonValidModel();
			}

			var signatory = OMSignatory
				.Where(x => x.Id == signatoryViewModel.Id)
				.SelectAll()
				.ExecuteFirstOrDefault();
			if (signatoryViewModel.Id != -1 && signatory == null)
			{
				return NotFound();
			}
			if (signatory == null)
			{
				SRDSession.Current.CheckAccessToFunction(ObjectModel.SRD.SRDCoreFunctions.DECLARATIONS_SIGNATORY_CREATE, true, false, true);
				signatory = new OMSignatory();
			}
			else
			{
				SRDSession.Current.CheckAccessToFunction(ObjectModel.SRD.SRDCoreFunctions.DECLARATIONS_SIGNATORY_EDIT, true, false, true);
			}
			SignatoryModel.ToEntity(signatoryViewModel, ref signatory);

			long id;
			try
			{
				id = signatory.Save();
			}
			catch (Exception e)
			{
				return SendErrorMessage(e.Message);
			}

			signatoryViewModel.Id = id;
			signatoryViewModel.IsCreateSignatory =
				SRDSession.Current.CheckAccessToFunction(ObjectModel.SRD.SRDCoreFunctions.DECLARATIONS_SIGNATORY_CREATE);
			signatoryViewModel.IsEditSignatory =
				SRDSession.Current.CheckAccessToFunction(ObjectModel.SRD.SRDCoreFunctions.DECLARATIONS_SIGNATORY_EDIT);
			return Json(new { Success = "Сохранено успешно", data = signatoryViewModel });
		}

		#endregion Signatories

		#region support methods

		public void CheckDeclarationPermissions(ref DeclarationModel model)
		{
			model.IsCreateDeclaration =
				SRDSession.Current.CheckAccessToFunction(ObjectModel.SRD.SRDCoreFunctions.DECLARATIONS_DECLARATION_CREATE);
			model.IsEditDeclaration =
				SRDSession.Current.CheckAccessToFunction(ObjectModel.SRD.SRDCoreFunctions.DECLARATIONS_DECLARATION_EDIT);
			model.IsEditDeclarationSupplyBlock =
				SRDSession.Current.CheckAccessToFunction(ObjectModel.SRD.SRDCoreFunctions.DECLARATIONS_DECLARATION_EDIT_SUPPLY_BLOCK);
			model.IsEditDeclarationProcessingBlock =
				SRDSession.Current.CheckAccessToFunction(ObjectModel.SRD.SRDCoreFunctions.DECLARATIONS_DECLARATION_EDIT_PROCESSING_BLOCK);
			model.IsEditDeclarationStatus =
				SRDSession.Current.CheckAccessToFunction(ObjectModel.SRD.SRDCoreFunctions.DECLARATIONS_DECLARATION_EDIT_STATUS);
			model.IsEditDeclarationFormalChecking =
				SRDSession.Current.CheckAccessToFunction(ObjectModel.SRD.SRDCoreFunctions.DECLARATIONS_DECLARATION_EDIT_FORMAL_CHECKING);
			model.IsEditDeclarationAttachments =
				SRDSession.Current.CheckAccessToFunction(ObjectModel.SRD.SRDCoreFunctions.DECLARATIONS_DECLARATION_EDIT_ATTACHMENTS);
		}

		#endregion
	}
}
