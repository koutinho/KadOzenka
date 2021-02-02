using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using KadOzenka.Dal.Modeling.Dto;
using Moq;
using NUnit.Framework;
using ObjectModel.Directory;
using ObjectModel.KO;

namespace KadOzenka.Dal.Tests.Modeling.Models
{
	[TestFixture]
	public class UpdatingTests : BaseModelTests
	{
		[Test]
		public void Can_Make_Manual_Model_Active_If_There_No_Other_Models_For_Group()
		{
			OMModel updatedModel = null;
			var initialModel = new OMModel
			{
				Id = Random.Next(),
				Type_Code = KoModelType.Manual,
				IsActive = false
			};
			ModelingRepository.Setup(x => x.GetModelById(It.IsAny<long>(), It.IsAny<Expression<Func<OMModel, object>>>()))
				.Returns(initialModel);
			ModelingRepository.Setup(x => x.GetModelsByCondition(It.IsAny<Expression<Func<OMModel, bool>>>(), It.IsAny<Expression<Func<OMModel, object>>>()))
				.Returns(new List<OMModel>());
			ModelingRepository.Setup(x => x.Save(It.IsAny<OMModel>())).Callback<OMModel>(inputModel => updatedModel = inputModel);

			ModelingService.MakeModelActive(initialModel.Id);

			ModelingRepository.Verify(foo => foo.Save(initialModel), Times.Once);
			Assert.That(updatedModel.Id, Is.EqualTo(initialModel.Id));
			Assert.That(updatedModel.IsActive, Is.True);
		}


		#region Support Methods

		private ModelingModelDto GetModelInfoDto()
		{
			return new ModelingModelDto
			{
				Name = GetRandomString(),
				Description = GetRandomString(),
				GroupId = Random.Next(),
				IsOksObjectType = true,
				AlgorithmTypeForCadastralPriceCalculation = KoAlgoritmType.Line,
				Type = KoModelType.Automatic
			};
		}

		#endregion
	}
}