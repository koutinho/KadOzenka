using System;
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
			var model = declaration != null
				? DeclarationModel.FromEntity(declaration, owner, agent, book, userIsp)
				: DeclarationModel.FromEntity(null, null, null, null, null);

			return View(model);
			//return View(DeclarationModel.FromEntity(null, null, null, null, null));
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
			if (declarationViewModel.Id != -1 && omDeclaration == null)
			{
				return NotFound();
			}
			if (omDeclaration == null)
			{
				omDeclaration = new OMDeclaration();
				omDeclaration.UserReg_Id = SRDSession.GetCurrentUserId();
			}
			DeclarationModel.ToEntity(declarationViewModel, ref omDeclaration);

			long id;
			using (var ts = new TransactionScope())
			{
				try
				{
					id = omDeclaration.Save();
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
				.Where(x => x.Name.StartsWith(searchText) || x.F_Name.StartsWith(searchText))
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

	}
}
