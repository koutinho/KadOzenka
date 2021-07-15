using KadOzenka.Common.Tests;
using KadOzenka.Dal.Integration._Builders.Model;
using NUnit.Framework;
using KadOzenka.Dal.Modeling.Dto;
using KadOzenka.Dal.Modeling.Model.Entities;
using ObjectModel.Directory;
using ObjectModel.KO;

namespace KadOzenka.Dal.IntegrationTests.Modeling.Models
{
	public class UpdatingTests : BaseModelingTests
	{
		[Test]
		public void Can_Update_Automatic_Model()
		{
			var existedModel = new ModelBuilder().Build();

			var updatingModelDto = new ModelDto
			{
				ModelId = existedModel.Id,
				Name = RandomGenerator.GetRandomString(),
				Description = RandomGenerator.GetRandomString(),
				AlgorithmType = KoAlgoritmType.Exp,
				A0 = RandomGenerator.GenerateRandomDecimal(),
				A0ForPreviousTour = RandomGenerator.GenerateRandomDecimal()
			};
			ModelService.UpdateAutomaticModel(updatingModelDto);

			var updatedModel = OMModel.Where(x => x.Id == existedModel.Id).SelectAll().ExecuteFirstOrDefault();
			Assert.That(updatedModel.Name, Is.EqualTo(updatingModelDto.Name));
			Assert.That(updatedModel.Description, Is.EqualTo(updatingModelDto.Description));
			Assert.That(updatedModel.A0ForExponential, Is.EqualTo(updatingModelDto.A0));
			Assert.That(updatedModel.A0ForExponentialTypeInPreviousTour, Is.EqualTo(updatingModelDto.A0ForPreviousTour));
		}
	}
}