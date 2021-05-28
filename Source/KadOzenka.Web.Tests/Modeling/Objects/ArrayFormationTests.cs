using System;
using KadOzenka.Common.Tests;
using KadOzenka.Dal.LongProcess.Modeling;
using KadOzenka.Web.Helpers;
using NUnit.Framework;

namespace KadOzenka.Web.Tests.Modeling.Objects
{
	public class ArrayFormationTests : BaseModelingTests
	{
		[Test]
		public void CanNot_Add_ObjectFormationProcess_To_Queue_If_The_Same_Process_Already_Was_Added()
		{
			var modelId = RandomGenerator.GenerateRandomInteger();
			LongProcessService.Setup(x => x.HasActiveProcessInQueue(ObjectFormationForModelingProcess.ProcessId, modelId)).Returns(true);

			var exception = Assert.Throws<Exception>(() => ModelingController.FormObjectArray(modelId));

			Assert.That(exception.Message, Is.EqualTo(Messages.ObjectFormationProcessAlreadyAdded));
		}
	}
}