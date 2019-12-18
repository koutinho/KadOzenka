﻿using System;
using System.Globalization;
using System.Linq;
using System.Transactions;
using Core.SRD;
using Core.UI.Registers.Controllers;
using KadOzenka.Web.Models.Declarations;
using Microsoft.AspNetCore.Mvc;
using ObjectModel.Core.SRD;
using ObjectModel.Declarations;
using ObjectModel.Directory.Declarations;

namespace KadOzenka.Web.Controllers
{
	public class DeclarationsController : BaseController
	{
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
				: DeclarationModel.FromEntity(null, null, null, null, null, null);

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

		public IQueryable GetAutoCompleteUser(string searchText)
		{
			return OMUser
				.Where(x => x.FullName.StartsWith(searchText))
				.SelectAll().Execute().Select(x => new
				{
					x.Id,
					Value = x.FullName
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

		[HttpGet]
		public JsonResult GetUserData(int userId)
		{
			var user = OMUser
				.Where(x => x.Id == userId)
				.SelectAll()
				.Execute().Select(x => new
				{
					x.Id,
					Value = x.FullName
				}).FirstOrDefault();

			return Json(new { data = user });
		}

		public ActionResult GetNotificationTabContent(long declarationId)
		{
			return View("~/Views/Declarations/DeclarationTabContent/NotificationContent.cshtml", declarationId);
		}

		public ActionResult GetCharacteristicTabContent(long declarationId)
		{
			return View("~/Views/Declarations/DeclarationTabContent/CharacteristicContent.cshtml");
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

			var model = notificationId != 0 && uved != null && book != null
				? NotificationModel.FromEntity(uved, book)
				: NotificationModel.FromEntity(null, null);
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

			NotificationModel.ToEntity(notificationViewModel, ref omUved);
			long id;
			using (var ts = new TransactionScope())
			{
				id = omUved.Save();
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
			OMUved
				.Where(x => x.Id == notificationId)
				.ExecuteFirstOrDefault()
				.Destroy();
			return EmptyResponse();
		}

		#endregion Notifications
	}
}
