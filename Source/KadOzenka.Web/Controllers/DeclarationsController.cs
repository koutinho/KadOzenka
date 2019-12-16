using System;
using System.Linq;
using System.Transactions;
using Core.UI.Registers.Controllers;
using KadOzenka.Web.Models.Declarations;
using Microsoft.AspNetCore.Mvc;
using ObjectModel.Declarations;

namespace KadOzenka.Web.Controllers
{
	public class DeclarationsController : BaseController
	{
		[HttpGet]
		public ActionResult EditDeclaration(long declarationId)
		{
			var declaration =  OMDeclaration
					.Where(x => x.Id == declarationId)
					.SelectAll()
					.ExecuteFirstOrDefault();
			var model = declaration != null
				? DeclarationModel.FromEntity(declaration)
				: DeclarationModel.FromEntity(null);

			return View(model);
		}

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
