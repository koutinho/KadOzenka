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
			MoqModelingRepository_GetById(initialModel);
			MoqModelingRepository_GetEntitiesByCondition(new List<OMModel>());
			ModelingRepository.Setup(x => x.Save(It.IsAny<OMModel>())).Callback<OMModel>(inputModel => updatedModel = inputModel);

			ModelingService.MakeModelActive(initialModel.Id);

			VerifyModelIsMakedActive(initialModel, updatedModel);
		}

		[Test]
		public void Can_Make_Automatic_Model_Active_If_There_No_Other_Models_For_Group()
		{
			OMModel updatedModel = null;
			var initialModel = new OMModel
			{
				Id = Random.Next(),
				Type_Code = KoModelType.Automatic,
				LinearTrainingResult = GetRandomString(),
				IsActive = false
			};
			MoqModelingRepository_GetById(initialModel);
			MoqModelingRepository_GetEntitiesByCondition(new List<OMModel>());
			ModelObjectsRepository.Setup(x => x.AreIncludedModelObjectsExist(initialModel.Id, true)).Returns(true);
			ModelingRepository.Setup(x => x.Save(It.IsAny<OMModel>())).Callback<OMModel>(inputModel => updatedModel = inputModel);

			ModelingService.MakeModelActive(initialModel.Id);

			VerifyModelIsMakedActive(initialModel, updatedModel);
		}


		#region Support Methods

		private void MoqModelingRepository_GetById(OMModel output)
		{
			ModelingRepository.Setup(x => x.GetById(It.IsAny<long>(), It.IsAny<Expression<Func<OMModel, object>>>()))
				.Returns(output);
		}

		private void MoqModelingRepository_GetEntitiesByCondition(List<OMModel> output)
		{
			ModelingRepository.Setup(x => x.GetEntitiesByCondition(It.IsAny<Expression<Func<OMModel, bool>>>(),
					It.IsAny<Expression<Func<OMModel, object>>>()))
				.Returns(output);
		}

		private void VerifyModelIsMakedActive(OMModel initialModel, OMModel updatedModel)
		{
			ModelingRepository.Verify(foo => foo.Save(initialModel), Times.Once);
			Assert.That(updatedModel.Id, Is.EqualTo(initialModel.Id));
			Assert.That(initialModel.IsActive, Is.True);
		}

		#endregion
	}
}