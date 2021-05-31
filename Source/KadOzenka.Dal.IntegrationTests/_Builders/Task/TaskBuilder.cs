﻿using KadOzenka.Common.Tests.Builders.Task;
using ObjectModel.KO;

namespace KadOzenka.Dal.Integration._Builders.Task
{
	public class TaskBuilder : AGbuObjectBuilder
	{
		public override OMTask Build()
		{
			_task.Save();
			return _task;
		}
	}
}
