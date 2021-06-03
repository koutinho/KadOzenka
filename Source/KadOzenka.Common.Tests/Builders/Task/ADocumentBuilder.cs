using System;
using ObjectModel.Core.TD;

namespace KadOzenka.Common.Tests.Builders.Task
{
	public abstract class ADocumentBuilder
	{
		protected readonly OMInstance _document;
		public long Id => _document.Id;

		protected ADocumentBuilder()
		{
			_document = new OMInstance
			{
				Description = RandomGenerator.GetRandomString(),
				AuthorId = RandomGenerator.GenerateRandomInteger(),
				RegNumber = RandomGenerator.GetRandomString(),
				CreateDate = DateTime.Now,
				ChangeDate = DateTime.Now,
				Status = 1,
				Priority = 1,
				ObjectId = RandomGenerator.GenerateRandomInteger(),
				ApproveDate = RandomGenerator.GenerateRandomDate(),
				ApproveUser = 1,
				RegisterId = RandomGenerator.GenerateRandomInteger()
			};
		}

		public abstract OMInstance Build();
	}
}
