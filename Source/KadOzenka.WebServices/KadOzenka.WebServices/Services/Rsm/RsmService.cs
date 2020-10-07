using System.Collections.Generic;
using System.Linq;
using KadOzenka.WebServices.Domain.Context;
using KadOzenka.WebServices.Domain.Model;
using KadOzenka.WebServices.Exceptions;
using KadOzenka.WebServices.Services.ModelDto;

namespace KadOzenka.WebServices.Services.Rsm
{
	public class RsmService
	{
		private int taskReadyStatusCode = 2;
		private string taskReadyStatus = "Готово";
		private ApplicationContext _appContext;

		public RsmService(ApplicationContext appContext)
		{
			_appContext = appContext;
		}


		public RsmEvaluationResultDto GetEvaluationResults(string cadastralNumber, int tourYear)
		{
			var tour = GetTour(tourYear);

			var units = _appContext.Units.Where(x => x.CadastralNumber == cadastralNumber && x.TourId == tour.Id)
				.Join(_appContext.Tasks,
				unit => unit.TaskId,
				task => task.Id,
				(unit, task) => new
				{
					UnitId = unit.Id,
					unit.GroupId,
					unit.Upks,
					unit.CadastralCost,
					TaskId = task.Id,
					TaskDocumentID = task.DocumentId,
					TaskStatus = task.Status
				}).ToList();

			var unitsWithReadyTask = units.Where(x => x.TaskStatus == taskReadyStatusCode).ToList();
			if (unitsWithReadyTask.Count == 0)
				throw new NotFoundException($"В единицах оценки тура {tour.Year} года с кадастровым номером '{cadastralNumber}' не найдено задания на оценку со статусом '{taskReadyStatus}'");

			var readyTasksDocumentIds = unitsWithReadyTask.Select(x => x.TaskDocumentID).Distinct().ToList();
			var document = GetDocument(readyTasksDocumentIds, tour.Year);

			var resultUnit = unitsWithReadyTask.First(x => x.TaskDocumentID == document.Id);

			Group resultGroup = null;
			var resultSubGroup = _appContext.Groups.FirstOrDefault(x => x.Id == resultUnit.GroupId);
			if (resultSubGroup != null)
				resultGroup = _appContext.Groups.FirstOrDefault(x => x.Id == resultSubGroup.ParentId);

			return new RsmEvaluationResultDto
			{
				GroupNumber = resultGroup?.Number,
				SubGroupNumber = resultSubGroup?.Number,
				Upks = resultUnit.Upks,
				CadastralCost = resultUnit.CadastralCost
			};
		}


		#region Supprt Methods

		private Tour GetTour(int tourYear)
		{
			var tour = _appContext.Tours.OrderByDescending(x => x.Year).FirstOrDefault(x => x.Year <= tourYear);
			if (tour == null)
				throw new NotFoundException($"Тур '{tourYear}' не найден, туры ранее {tourYear} года также не найдены");

			return tour;
		}

		private TdInstance GetDocument(List<int> documentIds, long tourYear)
		{
			var document = _appContext.TdInstances.Where(x => documentIds.Contains(x.Id) && x.ApproveDate != null)
				.OrderByDescending(x => x.ApproveDate).FirstOrDefault();
			if (document == null)
				throw new NotFoundException($"Для тура {tourYear} не найдено документа");

			return document;
		}

		#endregion
	}
}
