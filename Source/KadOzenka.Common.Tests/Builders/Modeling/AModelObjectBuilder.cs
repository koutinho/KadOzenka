using System.Collections.Generic;
using Core.Shared.Extensions;
using ObjectModel.Directory;
using ObjectModel.KO;
using ObjectModel.Modeling;

namespace KadOzenka.Common.Tests.Builders.Modeling
{
	public abstract class AModelObjectBuilder
	{
		protected readonly OMModelToMarketObjects _modelObject;

		
		protected AModelObjectBuilder()
		{
			var type = PropertyTypes.Building;
			var coefficients = new List<CoefficientForObject>
			{
				new(RandomGenerator.GenerateRandomId())
				{
					Coefficient = RandomGenerator.GenerateRandomDecimal(),
					Value = RandomGenerator.GetRandomString()
				}
			};

			_modelObject = new OMModelToMarketObjects
			{
				CadastralNumber = RandomGenerator.GetRandomString(),
				Price = RandomGenerator.GenerateRandomDecimal(),
				IsExcluded = false,
				ModelId = RandomGenerator.GenerateRandomId(),
				Coefficients = coefficients.SerializeCoefficient(),
				IsForTraining = true,
				IsForControl = true,
				PriceFromModel = RandomGenerator.GenerateRandomDecimal(),
				MarketObjectId = RandomGenerator.GenerateRandomId(),
				UnitId = RandomGenerator.GenerateRandomId(),
				UnitPropertyType = type.GetEnumDescription(),
				UnitPropertyType_Code = type
			};
		}


		public AModelObjectBuilder Id(long id)
		{
			_modelObject.Id = id;
			return this;
		}

		public AModelObjectBuilder Model(long modelId)
		{
			_modelObject.ModelId = modelId;
			return this;
		}

		public AModelObjectBuilder Model(OMModel model)
		{
			_modelObject.ModelId = model.Id;
			return this;
		}

		public AModelObjectBuilder Coefficients(List<CoefficientForObject> coefficients)
		{
			_modelObject.Coefficients = coefficients.SerializeCoefficient();
			return this;
		}

		public AModelObjectBuilder ForControl(bool isForControl)
		{
			_modelObject.IsForControl = isForControl;
			return this;
		}

		public AModelObjectBuilder Excluded(bool isExcluded)
		{
			_modelObject.IsExcluded = isExcluded;
			return this;
		}

		public AModelObjectBuilder Price(decimal price)
		{
			_modelObject.Price = price;
			return this;
		}


		public abstract OMModelToMarketObjects Build();
	}
}