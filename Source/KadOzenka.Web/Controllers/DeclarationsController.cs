using System;
using System.Globalization;
using System.Linq;
using System.Transactions;
using Core.SRD;
using Core.UI.Registers.Controllers;
using KadOzenka.Web.Models.Declarations;
using KadOzenka.Web.Models.Declarations.DeclarationTabModel;
using Microsoft.AspNetCore.Mvc;
using ObjectModel.Core.SRD;
using ObjectModel.Declarations;
using ObjectModel.Directory.Declarations;
using Core.Shared.Extensions;
using Newtonsoft.Json;
using ObjectModel.Core.Reports;

namespace KadOzenka.Web.Controllers
{
	public class DeclarationsController : BaseController
	{
		/// <summary>
		/// Срок рассмотрения декларации в соответствии с Приказом 318 составляет 50 рабочих дней со дня поступления декларации
		/// </summary>
		public static int DurationWorkDaysCount => 50;

		/// <summary>
		/// Срок рассмотрения декларации составляет 5 рабочих дней со дня поступления декларации eсли заявителю отказано в рассмотрении
		/// </summary>
		public static int DurationWorkDaysCountForRejectedDeclaration => 5;

		/// <summary>
		/// Плановая дата рассмотрения должна быть за 10 дней до окончания срока рассмотрения
		/// </summary>
		public static int DaysDiffBetweenDateCheckPlanAndDurationDateIn => 10;

		/// <summary>
		/// Контрольный срок составляет 5 рабочих дней со дня поступления декларации
		/// </summary>
		public static int CheckTimeDaysCount => 5;

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
				? DeclarationModel.FromEntity(declaration, owner, agent, book, userIsp, result)
				: DeclarationModel.FromEntity(null, null, null, null, userIsp, null);

			return View(model);
		}

		[HttpPost]
		public ActionResult EditDeclaration(DeclarationModel declarationViewModel)
		{
			if (!ModelState.IsValid)
			{
				return Json(new
				{
					Errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new
					{
						Control = x.Key,
						Message = string.Join("\n", x.Value.Errors.Select(e =>
						{
							if (e.ErrorMessage == "The value '' is invalid.")
							{
								return $"{e.ErrorMessage} Поле {x.Key}";
							}

							return e.ErrorMessage;
						}))
					})
				});
			}

			var omDeclaration = OMDeclaration
				.Where(x => x.Id == declarationViewModel.Id)
				.SelectAll()
				.ExecuteFirstOrDefault();
			var omResult = OMResult
				.Where(x => x.Declaration_Id == declarationViewModel.Id)
				.SelectAll()
				.ExecuteFirstOrDefault();
			if (declarationViewModel.Id != -1 && omDeclaration == null)
			{
				return NotFound();
			}
			if (omDeclaration == null)
			{
				omDeclaration = new OMDeclaration();
				omDeclaration.UserReg_Id = SRDSession.GetCurrentUserId();
				omResult = new OMResult();

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
					return Json(new
					{
						Errors =
							new
							{
								Control = string.Empty,
								e.Message
							}
					});
				}
			}

			declarationViewModel.Id = id;
			return Json(new { Success = "Сохранено успешно", data = declarationViewModel });
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
			return PartialView("~/Views/Declarations/DeclarationTabContent/NotificationContent.cshtml", declarationId);
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

			var model = characteristic != null
				? ParcelCharacteristicsModel.FromEntity(characteristic)
				: ParcelCharacteristicsModel.FromEntity(null);
			model.DeclarationId = declarationId;

			return View("~/Views/Declarations/ParcelCharacteristicsWindowContent.cshtml", model);
		}

		[HttpPost]
		public ActionResult EditParcelCharacteristics(ParcelCharacteristicsModel parcelCharacteristicsViewModel)
		{
			if (!ModelState.IsValid)
			{
				return Json(new
				{
					Errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new
					{
						Control = x.Key,
						Message = string.Join("\n", x.Value.Errors.Select(e =>
						{
							if (e.ErrorMessage == "The value '' is invalid.")
							{
								return $"{e.ErrorMessage} Поле {x.Key}";
							}

							return e.ErrorMessage;
						}))
					})
				});
			}

			var characteristic = OMHarParcel
				.Where(x => x.Id == parcelCharacteristicsViewModel.Id)
				.SelectAll()
				.ExecuteFirstOrDefault();

			if (parcelCharacteristicsViewModel.Id != -1 && characteristic == null)
			{
				return NotFound();
			}

			if (characteristic == null)
			{
				characteristic = new OMHarParcel();
			}

			var result = OMResult
				.Where(x => x.Declaration_Id == parcelCharacteristicsViewModel.DeclarationId)
				.SelectAll()
				.ExecuteFirstOrDefault();
			if (result == null)
			{
				throw new Exception($"не найдены результаты для декларации с идентификатором {parcelCharacteristicsViewModel.DeclarationId}");
			}

			ParcelCharacteristicsModel.ToEntity(parcelCharacteristicsViewModel, ref characteristic);
			long id;
			using (var ts = new TransactionScope())
			{
				id = characteristic.Save();

				result.TextYes = parcelCharacteristicsViewModel.GetAcceptedCharacteristics();
				result.TextNo = parcelCharacteristicsViewModel.GetRejectedCharacteristics();
				result.Save();

				ts.Complete();
			}

			parcelCharacteristicsViewModel.Id = id;
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

			var model = characteristic != null
				? OksCharacteristicsModel.FromEntity(characteristic)
				: OksCharacteristicsModel.FromEntity(null);
			model.DeclarationId = declarationId;

			return View("~/Views/Declarations/OksCharacteristicsWindowContent.cshtml", model);
		}

		[HttpPost]
		public ActionResult EditOksCharacteristics(OksCharacteristicsModel oksCharacteristicsViewModel)
		{
			if (!ModelState.IsValid)
			{
				return Json(new
				{
					Errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new
					{
						Control = x.Key,
						Message = string.Join("\n", x.Value.Errors.Select(e =>
						{
							if (e.ErrorMessage == "The value '' is invalid.")
							{
								return $"{e.ErrorMessage} Поле {x.Key}";
							}

							return e.ErrorMessage;
						}))
					})
				});
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
				throw new Exception($"не найдены результаты для декларации с идентификатором {oksCharacteristicsViewModel.DeclarationId}");
			}

			if (characteristic == null)
			{
				characteristic = new OMHarOKS();
			}

			OksCharacteristicsModel.ToEntity(oksCharacteristicsViewModel, ref characteristic);
			long id;
			using (var ts = new TransactionScope())
			{
				id = characteristic.Save();

				result.TextYes = oksCharacteristicsViewModel.GetAcceptedCharacteristics();
				result.TextNo = oksCharacteristicsViewModel.GetRejectedCharacteristics();
				result.Save();

				ts.Complete();
			}

			oksCharacteristicsViewModel.Id = id;
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

			return View(model);
		}

		[HttpPost]
		public ActionResult EditBook(BookModel bookViewModel)
		{
			if (!ModelState.IsValid)
			{
				return Json(new
				{
					Errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new
					{
						Control = x.Key,
						Message = string.Join("\n", x.Value.Errors.Select(e =>
						{
							if (e.ErrorMessage == "The value '' is invalid.")
							{
								return $"{e.ErrorMessage} Поле {x.Key}";
							}

							return e.ErrorMessage;
						}))
					})
				});
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
				omBook = new OMBook();
			}
			BookModel.ToEntity(bookViewModel, ref omBook);

			long id;
			using (var ts = new TransactionScope())
			{
				try
				{
					id = omBook.Save();
					ts.Complete();
				}
				catch (Exception e)
				{
					return Json(new
					{
						Errors =
							new
							{
								Control = string.Empty,
								e.Message
							}
					});
				}
			}

			bookViewModel.Id = id;
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

			return View(model);
		}

		[HttpPost]
		public ActionResult EditSubject(SubjectModel subjectViewModel)
		{
			if (!ModelState.IsValid)
			{
				return Json(new
				{
					Errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new
					{
						Control = x.Key,
						Message = string.Join("\n", x.Value.Errors.Select(e =>
						{
							if (e.ErrorMessage == "The value '' is invalid.")
							{
								return $"{e.ErrorMessage} Поле {x.Key}";
							}

							return e.ErrorMessage;
						}))
					})
				});
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
				omSubject = new OMSubject();
			}
			SubjectModel.ToEntity(subjectViewModel, ref omSubject);

			long id;
			using (var ts = new TransactionScope())
			{
				try
				{
					id = omSubject.Save();
					ts.Complete();
				}
				catch (Exception e)
				{
					return Json(new
					{
						Errors =
							new
							{
								Control = string.Empty,
								e.Message
							}
					});
				}
			}

			subjectViewModel.Id = id;
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
			if (uved != null)
			{
				book = OMBook
					.Where(x => x.Id == uved.Book_Id)
					.SelectAll()
					.ExecuteFirstOrDefault();
			}
			else
			{
				book = OMBook
					.Where(x => x.Id == declaration.Book_Id)
					.SelectAll()
					.ExecuteFirstOrDefault();
			}

			var model = notificationId != 0 && uved != null
				? NotificationModel.FromEntity(uved, book)
				: NotificationModel.FromEntity(null, book);
			model.DeclarationId = declarationId;

			return View(model);
		}

		[HttpPost]
		public ActionResult EditNotification(NotificationModel notificationViewModel)
		{
			if (!ModelState.IsValid)
			{
				return Json(new
				{
					Errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new
					{
						Control = x.Key,
						Message = string.Join("\n", x.Value.Errors.Select(e =>
						{
							if (e.ErrorMessage == "The value '' is invalid.")
							{
								return $"{e.ErrorMessage} Поле {x.Key}";
							}

							return e.ErrorMessage;
						}))
					})
				});
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
			}
			var isNotificationTypeChanged = notificationViewModel.Type.GetValueOrDefault() != omUved.Type_Code;

			NotificationModel.ToEntity(notificationViewModel, ref omUved);
			long id;
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
				}

				ts.Complete();
			}

			notificationViewModel.Id = id;
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

			return View(NotificationModel.FromEntity(omUved, book));
		}

		[HttpPost, ActionName("DeleteNotification")]
		public IActionResult Delete(long notificationId)
		{
			using (var ts = new TransactionScope())
			{
				try
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

					OMUved
						.Where(x => x.Id == notificationId)
						.ExecuteFirstOrDefault()
						.Destroy();

					ts.Complete();
				}
				catch (Exception e)
				{
					return Json(new
					{
						Errors =
							new
							{
								Control = string.Empty,
								e.Message
							}
					});
				}
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
			var reportType = GetNotificationReportType(omUved);

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
			var reportType = GetNotificationReportType(omUved);
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

		private int GetNotificationReportType(OMUved omUved)
		{
			int reportType;
			switch (omUved?.Type_Code)
			{
				case UvedType.Item5:
					reportType = 1001;
					break;
				case UvedType.Item3:
					reportType = 1002;
					break;
				case UvedType.Item4:
					reportType = 1003;
					break;
				case UvedType.Item1:
					reportType = 1004;
					break;
				default:
					throw new Exception(
						$"Тип уведомления '{omUved?.Type_Code.GetEnumDescription()}' не поддерживает формирование по шаблону");
			}

			return reportType;
		}

		#endregion Notifications
	}
}
