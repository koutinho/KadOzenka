using System.Collections.Generic;
using System.Linq;
using KadOzenka.Dal.GbuObject.Dto;
using ObjectModel.Core.LongProcess;
using ObjectModel.Directory.Core.LongProcess;

namespace KadOzenka.Dal.GbuObject
{
	public class GbuLongProcessesService
	{
		public const long SetPriorityGroupProcessTypeId = 12;
		public const long HarmonizationProcessTypeId = 13;

		public List<LongProcessDto> GetCurrentLongProcessesList()
		{
			return OMQueue.Where(x => 
				(x.ProcessTypeId == SetPriorityGroupProcessTypeId ||
				   x.ProcessTypeId == HarmonizationProcessTypeId)
				&& (x.Status_Code == Status.Added
					  || x.Status_Code == Status.PrepareToRun
					  || x.Status_Code == Status.Running))
				.Select(x => new
				{
					x.Id,
					x.ProcessTypeId,
					x.ParentProcessType.Description,
					x.Status_Code,
					x.Status,
					x.Progress,
					x.LastCheckDate
				})
				.OrderBy(x => x.LastCheckDate).Execute()
				.Select(x => new LongProcessDto
				{
					Id = x.Id,
					ProcessTypeId = x.ProcessTypeId,
					Name = x.ParentProcessType.Description,
					StatusCode = x.Status_Code,
					StatusName = x.Status,
					Progress = x.Progress
				})
				.ToList();
		}
	}
}
