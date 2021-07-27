using System.Collections.Generic;
using System.Text.RegularExpressions;
using Core.Register.RegisterEntities;
using KadOzenka.Common.Tests.Builders.Cache;
using KadOzenka.Dal.UnitTests._Builders.Modeling;
using KadOzenka.Dal.UnitTests._Builders.Modeling.Factors;
using Moq;
using NUnit.Framework;
using ObjectModel.Directory;
using ObjectModel.KO;

namespace KadOzenka.Dal.UnitTests.Modeling.Models.Formulas
{
	public abstract class BaseFormulasTests : BaseModelTests
	{
		protected OMModel Model;
		protected FactorBuilder FactorBuilder;
		protected RegisterAttribute CacheAttribute;
		protected string CacheAttributeName => $"\"{CacheAttribute.Name}\"";
		protected abstract KoAlgoritmType AlgorithmType { get; }


		[SetUp]
		public void SetUp()
		{
			Model = new ModelBuilder().Manual().AlgorithmType(AlgorithmType).Build();
			FactorBuilder = new FactorBuilder();
			CacheAttribute = new RegisterAttributeBuilder().Id(FactorBuilder.Id).Build();
		}



		protected void MockDependencies(OMModel model, OMModelFactor factor, RegisterAttribute cacheAttribute)
		{
			ModelFactorsService.Setup(x => x.GetFactorsEntities(model.Id)).Returns(new List<OMModelFactor> { factor });

			RegisterCacheWrapper.Setup(x => x.GetAttributeData(factor.FactorId.Value)).Returns(cacheAttribute);
		}

		protected string ProcessFormula(string str)
		{
			var formulaWithoutSpaces = Regex.Replace(str.ToLower(), @"\s+", "");
			var formulaWithoutFloatNumbersSeparator = formulaWithoutSpaces.Replace(',', '|').Replace('.', '|');
			return formulaWithoutFloatNumbersSeparator;
		}
	}
}