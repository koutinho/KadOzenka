﻿using KadOzenka.Dal.Modeling.Dto;
using KadOzenka.Dal.Modeling.Exceptions;
using KadOzenka.Dal.Modeling.Resources;
using Moq;
using NUnit.Framework;
using ObjectModel.Directory;
using ObjectModel.KO;

namespace KadOzenka.Dal.Tests.Modeling.Models
{
	[TestFixture]
	public class CreationTests : BaseModelTests
	{
		[Test]
		public void CanNot_Create_Model_With_Empty_Name()
		{
			var modelDto = GetModelInfoDto();
			modelDto.Name = null;
			
			var exception = Assert.Throws<ModelCrudException>(() => ModelingService.AddAutomaticModel(modelDto));

			StringAssert.Contains(Messages.EmptyName, exception.Message);
			ModelingRepository.Verify(foo => foo.Save(It.IsAny<OMModel>()), Times.Never);
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