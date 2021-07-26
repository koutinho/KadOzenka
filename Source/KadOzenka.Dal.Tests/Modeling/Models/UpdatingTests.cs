using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using KadOzenka.Dal.UnitTests._Builders.Modeling;
using ModelingBusiness.Objects.Entities;
using Moq;
using NUnit.Framework;
using ObjectModel.KO;

namespace KadOzenka.Dal.UnitTests.Modeling.Models
{
	[TestFixture]
	public class UpdatingTests : BaseModelTests
	{
		[Test]
		public void Can_Make_Manual_Model_Active_If_There_Are_No_Other_Models_For_Group()
		{
			OMModel updatedModel = null;
			var initialModel = new ModelBuilder().Manual().IsActive(false).Build();

			MoqModelingRepository_GetById(initialModel);
			MoqModelingRepository_GetOtherGroupModels(new List<OMModel>());
			ModelingRepository.Setup(x => x.Save(initialModel)).Callback<OMModel>(inputModel => updatedModel = inputModel);

			ModelService.ActivateModel(initialModel.Id);

			VerifyModelIsMakedActive(initialModel, updatedModel);
		}

		[Test]
		public void Can_Make_Automatic_Model_Active_If_There_Are_No_Other_Models_For_Group()
		{
			OMModel updatedModel = null;
			var initialModel = new ModelBuilder().Automatic().IsActive(false).Build();

			MoqModelingRepository_GetById(initialModel);
			MoqModelingRepository_GetOtherGroupModels(new List<OMModel>());
			ModelObjectsRepository.Setup(x => x.AreIncludedModelObjectsExist(initialModel.Id, IncludedObjectsMode.Training)).Returns(true);
			ModelingRepository.Setup(x => x.Save(initialModel)).Callback<OMModel>(inputModel => updatedModel = inputModel);

			ModelService.ActivateModel(initialModel.Id);

			VerifyModelIsMakedActive(initialModel, updatedModel);
		}

		[Test]
		public void CanNot_Make_Automatic_Model_Active_If_There_Are_No_Model_Objects()
		{
			var initialModel = new ModelBuilder().Automatic().IsActive(false).Build();

			MoqModelingRepository_GetById(initialModel);
			ModelObjectsRepository.Setup(x => x.AreIncludedModelObjectsExist(initialModel.Id, IncludedObjectsMode.Training)).Returns(false);

			var exception = Assert.Throws<Exception>(() => ModelService.ActivateModel(initialModel.Id));

			ModelingRepository.Verify(foo => foo.Save(initialModel), Times.Never);
			Assert.That(exception.Message, Is.EqualTo(ModelingBusiness.Messages.CanNotActivateNotPreparedAutomaticModel));
		}

		[Test]
		public void CanNot_Make_Automatic_Model_Active_If_There_Are_No_Training_Results()
		{
			var initialModel = new ModelBuilder().Automatic().IsActive(false)
				.LinearTrainingResult(null).ExponentialTrainingResult(null).MultiplicativeTrainingResult(null)
				.Build();

			MoqModelingRepository_GetById(initialModel);
			ModelObjectsRepository.Setup(x => x.AreIncludedModelObjectsExist(initialModel.Id, IncludedObjectsMode.Training)).Returns(true);

			var exception = Assert.Throws<Exception>(() => ModelService.ActivateModel(initialModel.Id));

			ModelingRepository.Verify(foo => foo.Save(initialModel), Times.Never);
			Assert.That(exception.Message, Is.EqualTo(ModelingBusiness.Messages.CanNotActivateNotPreparedAutomaticModel));
		}

		[Test]
		public void Can_Deactivate_Model_For_Group()
		{
			OMModel updatedModel = null;
			var previouslyActivatedModel = new ModelBuilder().Manual().IsActive(true).Build();

			MoqModelingRepository_GetOtherGroupModels(new List<OMModel>{previouslyActivatedModel});
			ModelingRepository.Setup(x => x.Save(previouslyActivatedModel)).Callback<OMModel>(inputModel => updatedModel = inputModel);

			ModelService.DeactivateModel(previouslyActivatedModel.GroupId.GetValueOrDefault());

			ModelingRepository.Verify(foo => foo.Save(previouslyActivatedModel), Times.Once);
			Assert.That(updatedModel.Id, Is.EqualTo(previouslyActivatedModel.Id));
			Assert.That(updatedModel.IsActive, Is.False);
		}


		#region Support Methods

		private void MoqModelingRepository_GetById(OMModel output)
		{
			ModelingRepository.Setup(x => x.GetById(It.IsAny<long>(), It.IsAny<Expression<Func<OMModel, object>>>()))
				.Returns(output);
		}

		private void MoqModelingRepository_GetOtherGroupModels(List<OMModel> output)
		{
			ModelingRepository.Setup(x => x.GetEntitiesByCondition(It.IsAny<Expression<Func<OMModel, bool>>>(),
					It.IsAny<Expression<Func<OMModel, object>>>()))
				.Returns(output);
		}

		private void VerifyModelIsMakedActive(OMModel initialModel, OMModel updatedModel)
		{
			ModelingRepository.Verify(foo => foo.Save(initialModel), Times.Once);
			Assert.That(updatedModel.Id, Is.EqualTo(initialModel.Id));
			Assert.That(updatedModel.IsActive, Is.True);
		}

		#endregion
	}
}