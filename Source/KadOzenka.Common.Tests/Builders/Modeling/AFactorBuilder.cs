﻿using Core.Shared.Extensions;
using ObjectModel.Directory;
using ObjectModel.Directory.Ko;
using ObjectModel.KO;

namespace KadOzenka.Common.Tests.Builders.Modeling
{
	public abstract class AFactorBuilder
	{
		protected readonly OMModelFactor _factor;

		protected AFactorBuilder()
		{
			var algorithm = KoAlgoritmType.Line;
			var markType = MarkType.None;

			_factor = new OMModelFactor
			{
				ModelId = RandomGenerator.GenerateRandomInteger(),
				FactorId = RandomGenerator.GenerateRandomInteger(),
				MarkerId = RandomGenerator.GenerateRandomInteger(),
				Weight = RandomGenerator.GenerateRandomDecimal(),
				B0 = RandomGenerator.GenerateRandomDecimal(),
				SignDiv = true,
				SignAdd = true,
				SignMarket = true,
				DictionaryId = RandomGenerator.GenerateRandomInteger(),
				AlgorithmType = algorithm.GetEnumDescription(),
				AlgorithmType_Code = algorithm,
				PreviousWeight = RandomGenerator.GenerateRandomDecimal(),
				IsActive = true,
				MarkType = markType.GetEnumDescription(),
				MarkType_Code = markType,
				CorrectingTerm = RandomGenerator.GenerateRandomDecimal(),
				K = RandomGenerator.GenerateRandomDecimal()
			};
		}

		public abstract OMModelFactor Build();
	}
}