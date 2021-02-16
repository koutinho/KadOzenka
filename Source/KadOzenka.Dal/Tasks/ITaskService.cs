using System;
using System.Collections.Generic;
using System.Text;

namespace KadOzenka.Dal.Tasks
{
	public interface ITaskService
	{
		string GetTemplateForTaskName(long taskId);
	}
}
