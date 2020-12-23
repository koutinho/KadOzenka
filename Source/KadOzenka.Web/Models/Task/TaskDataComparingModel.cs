using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Core.Shared.Extensions;
using KadOzenka.Dal.DataComparing;
using KadOzenka.Dal.Tasks.Dto;
using Microsoft.AspNetCore.Mvc.Rendering;
using ObjectModel.Directory;

namespace KadOzenka.Web.Models.Task
{
	public class TaskDataComparingModel
	{
		public long Id { get; set; }
		public KoNoteType? NoteType { get; set; }
		[Display(Name = "Статус после сравнения протоколов загрузки")]
		public KoDataComparingTaskChangesStatus? DataComparingTaskChangesStatusCode { get; set; }

		[Display(Name = "Статус после сравнения протоколов КС")]
		public KoDataComparingCadastralCostStatus? DataComparingCadastralCostStatusCode { get; set; }

		public bool ContainsFdFilesComparingResult { get; set; }

		public bool IsTaskChangesPkkoFileUploaded { get; set; }
		public bool AreCostPkkoFilesUploaded { get; set; }
		public bool AreFdPkkoFilesUploaded { get; set; }

		public List<SelectListItem> PossibleUploadTypes { get; set; }

		public static TaskDataComparingModel ToModel(TaskDataComparingDto task)
		{
			var model =  new TaskDataComparingModel
			{
				Id = task.Id,
				NoteType = task.NoteType,
				DataComparingTaskChangesStatusCode = task.DataComparingTaskChangesStatusCode,
				DataComparingCadastralCostStatusCode = task.DataComparingCadastralCostStatusCode,
				ContainsFdFilesComparingResult = task.ContainsFdFilesComparingResult,
				IsTaskChangesPkkoFileUploaded = task.IsTaskChangesPkkoFileUploaded,
				AreCostPkkoFilesUploaded = task.AreCostPkkoFilesUploaded,
				AreFdPkkoFilesUploaded = task.AreFdPkkoFilesUploaded,
				PossibleUploadTypes = new List<SelectListItem>()
			};

			if(task.NoteType != KoNoteType.Initial)
				model.PossibleUploadTypes.Add(new SelectListItem(DataComparingFileType.TaskChangesPkkoFile.GetEnumDescription(), ((int)DataComparingFileType.TaskChangesPkkoFile).ToString()));
			if (task.NoteType == KoNoteType.Initial || task.NoteType != KoNoteType.Initial && task.DataComparingTaskChangesStatusCode != KoDataComparingTaskChangesStatus.ComparingWasNotPerformed)
				model.PossibleUploadTypes.Add(new SelectListItem(DataComparingFileType.CostPkkoFiles.GetEnumDescription(), ((int)DataComparingFileType.CostPkkoFiles).ToString()));
			if (task.DataComparingCadastralCostStatusCode == KoDataComparingCadastralCostStatus.ThereAreUnitCostsInconsistencies)
				model.PossibleUploadTypes.Add(new SelectListItem(DataComparingFileType.FdPkkoFiles.GetEnumDescription(), ((int)DataComparingFileType.FdPkkoFiles).ToString()));

			return model;
		}
    }
}
