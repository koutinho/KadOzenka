using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using KadOzenka.Dal.Modeling.Entities;
using KadOzenka.Dal.Modeling.Repositories;
using KadOzenka.Dal.Modeling.Resources;
using KadOzenka.Dal.UnitTests._Builders;
using KadOzenka.Dal.UnitTests._Builders.Modeling;
using Moq;
using NUnit.Framework;
using ObjectModel.KO;

namespace KadOzenka.Dal.Tests.Modeling.Models
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

			ModelingService.MakeModelActive(initialModel.Id);

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

			ModelingService.MakeModelActive(initialModel.Id);

			VerifyModelIsMakedActive(initialModel, updatedModel);
		}

		[Test]
		public void CanNot_Make_Automatic_Model_Active_If_There_Are_No_Model_Objects()
		{
			var initialModel = new ModelBuilder().Automatic().IsActive(false).Build();

			MoqModelingRepository_GetById(initialModel);
			ModelObjectsRepository.Setup(x => x.AreIncludedModelObjectsExist(initialModel.Id, IncludedObjectsMode.Training)).Returns(false);

			var exception = Assert.Throws<Exception>(() => ModelingService.MakeModelActive(initialModel.Id));

			ModelingRepository.Verify(foo => foo.Save(initialModel), Times.Never);
			Assert.That(exception.Message, Is.EqualTo(Messages.CanNotActivateNotPreparedAutomaticModel));
		}

		[Test]
		public void CanNot_Make_Automatic_Model_Active_If_There_Are_No_Training_Results()
		{
			var initialModel = new ModelBuilder().Automatic().IsActive(false)
				.LinearTrainingResult(null).ExponentialTrainingResult(null).MultiplicativeTrainingResult(null)
				.Build();

			MoqModelingRepository_GetById(initialModel);
			ModelObjectsRepository.Setup(x => x.AreIncludedModelObjectsExist(initialModel.Id, IncludedObjectsMode.Training)).Returns(true);

			var exception = Assert.Throws<Exception>(() => ModelingService.MakeModelActive(initialModel.Id));

			ModelingRepository.Verify(foo => foo.Save(initialModel), Times.Never);
			Assert.That(exception.Message, Is.EqualTo(Messages.CanNotActivateNotPreparedAutomaticModel));
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