using KadOzenka.Common.Tests.Builders.Task;
using ObjectModel.KO;

namespace KadOzenka.Dal.IntegrationTests.Task._Builders
{
	public class TaskBuilder : ATaskBuilder
	{
		public override OMTask Build()
		{
			_task.Save();
			return _task;
		}
	}
}
