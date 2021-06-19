using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Core.Register.RegisterEntities;
using KadOzenka.Common.Tests.Builders.Cache;
using KadOzenka.Dal.Tests.Modeling.Models;
using KadOzenka.Dal.UnitTests._Builders.Modeling;
using KadOzenka.Dal.UnitTests._Builders.Modeling.Factors;
using Moq;
using NUnit.Framework;
using ObjectModel.Directory;
using ObjectModel.Directory.Ko;
using ObjectModel.KO;

namespace KadOzenka.Dal.UnitTests.Modeling.Models.Formulas
{
	public class MultiplicativeFormulaTests : BaseModelTests
	{
		private OMModel _model;
		private FactorBuilder _factorBuilder;
		private RegisterAttribute _cacheAttribute;


		[SetUp]
		public void SetUp()
		{
			_model = new ModelBuilder().AlgorithmType(KoAlgoritmType.Multi).Build();
			_factorBuilder = new FactorBuilder();
			_cacheAttribute = new RegisterAttributeBuilder().Id(_factorBuilder.Id).Build();
		}


		[Test]
		public void Can_Create_Formula_With_One_Factor_Without_Mark()
		{
			var factor = _factorBuilder.MarkType(MarkType.None).Build();
			MockDependencies(_model, factor, _cacheAttribute);

			var formula = ModelingService.GetFormula(_model.Id);

			var expectedResult = $"{_model.A0ForMultiplicativeInFormula}*({_cacheAttribute.Name}+{factor.CorrectionInFormula})^{factor.CoefficientInFormula}";
			Assert.That(ProcessFormula(formula), Is.EqualTo(ProcessFormula(expectedResult)));
		}

		[Test]
		public void Can_Create_Formula_With_One_Factor_With_Default_Mark()
		{
			var factor = _factorBuilder.MarkType(MarkType.Default).Build();
			MockDependencies(_model, factor, _cacheAttribute);

			var formula = ModelingService.GetFormula(_model.Id);

			var expectedResult = $"{_model.A0ForMultiplicativeInFormula}*(метка({_cacheAttribute.Name})+{factor.CorrectionInFormula})^{factor.CoefficientInFormula})";
			Assert.That(ProcessFormula(formula), Is.EqualTo(ProcessFormula(expectedResult)));
		}


		#region Support Methods

		private void MockDependencies(OMModel model, OMModelFactor factor, RegisterAttribute cacheAttribute)
		{
			ModelingRepository.Setup(x => x.GetById(model.Id, It.IsAny<Expression<Func<OMModel, object>>>()))
				.Returns(model);

			ModelFactorsService.Setup(x => x.GetFactors(model.Id, model.AlgoritmType_Code))
				.Returns(new List<OMModelFactor> { factor });

			RegisterCacheWrapper.Setup(x => x.GetAttributeData(factor.FactorId.Value))
				.Returns(cacheAttribute);
		}

		#endregion
	}
}