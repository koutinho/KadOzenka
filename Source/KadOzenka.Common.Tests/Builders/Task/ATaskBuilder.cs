using System;
using ObjectModel.Core.TD;
using ObjectModel.Directory;
using ObjectModel.KO;

namespace KadOzenka.Common.Tests.Builders.Task
{
	public abstract class ATaskBuilder
	{
		protected readonly OMTask _task;

		protected ATaskBuilder()
		{
			_task = new OMTask
			{
				CreationDate = DateTime.Now,
				TourId = RandomGenerator.GenerateRandomInteger(),
				NoteType_Code = KoNoteType.Day,
				Status_Code = KoTaskStatus.InWork,
				ResponseDocId = RandomGenerator.GenerateRandomInteger(),
				DocumentId = RandomGenerator.GenerateRandomInteger(),
				EstimationDate = RandomGenerator.GenerateRandomDate()
			};
		}

		public abstract OMTask Build();


		public ATaskBuilder Document(long documentId)
		{
			_task.DocumentId = documentId;
			return this;
		}

		public ATaskBuilder Tour(long tourId)
		{
			_task.TourId = tourId;
			return this;
		}
	}
}
