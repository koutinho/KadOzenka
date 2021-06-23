using System.Collections.Generic;
using Core.Register.RegisterEntities;
using KadOzenka.Common.Tests.Builders.Cache;
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
		private string CacheAttributeName => $"\"{_cacheAttribute.Name}\"";


		[SetUp]
		public void SetUp()
		{
			_model = new ModelBuilder().Manual().AlgorithmType(KoAlgoritmType.Multi).Build();
			_factorBuilder = new FactorBuilder();
			_cacheAttribute = new RegisterAttributeBuilder().Id(_factorBuilder.Id).Build();
		}


		[Test]
		public void Can_Create_Formula_With_One_Factor_Without_Mark()
		{
			var factor = _factorBuilder.MarkType(MarkType.None).Build();
			MockDependencies(_model, factor, _cacheAttribute);

			var formula = ModelingService.GetFormula(_model, KoAlgoritmType.Multi);

			var expectedFormula = $"{_model.A0ForMultiplicativeInFormula}*({CacheAttributeName}+{factor.WeightInFormula})^{factor.B0InFormula}";
			Assert.That(ProcessFormula(formula), Is.EqualTo(ProcessFormula(expectedFormula)));
		}

		[Test]
		public void Can_Create_Formula_With_One_Factor_Of_Default_Mark()
		{
			var factor = _factorBuilder.MarkType(MarkType.Default).Build();
			MockDependencies(_model, factor, _cacheAttribute);

			var formula = ModelingService.GetFormula(_model, KoAlgoritmType.Multi);

			var expectedFormula = $"{_model.A0ForMultiplicativeInFormula}*(метка({CacheAttributeName})+{factor.WeightInFormula})^{factor.B0InFormula}";
			Assert.That(ProcessFormula(formula), Is.EqualTo(ProcessFormula(expectedFormula)));
		}

		[Test]
		public void Can_Create_Formula_With_One_Factor_Of_Straight_Mark()
		{
			var factor = _factorBuilder.MarkType(MarkType.Straight).Build();
			MockDependencies(_model, factor, _cacheAttribute);

			var formula = ModelingService.GetFormula(_model, KoAlgoritmType.Multi);

			var expectedFormula = $"{_model.A0ForMultiplicativeInFormula}*(({CacheAttributeName}+{factor.CorrectingTermInFormula})/{factor.KInFormula} + {factor.WeightInFormula})^{factor.B0InFormula}";
			Assert.That(ProcessFormula(formula), Is.EqualTo(ProcessFormula(expectedFormula)));
		}

		[Test]
		public void Can_Create_Formula_With_One_Factor_Of_Reverse_Mark()
		{
			var factor = _factorBuilder.MarkType(MarkType.Reverse).Build();
			MockDependencies(_model, factor, _cacheAttribute);

			var formula = ModelingService.GetFormula(_model, KoAlgoritmType.Multi);

			var expectedFormula = $"{_model.A0ForMultiplicativeInFormula}*({factor.KInFormula}/({CacheAttributeName}+{factor.CorrectingTermInFormula})+{factor.WeightInFormula})^{factor.B0InFormula}";
			Assert.That(ProcessFormula(formula), Is.EqualTo(ProcessFormula(expectedFormula)));
		}

		[Test]
		public void Can_Create_Formula_With_Several_Factors()
		{
			var noneMarkTypeFactor = new FactorBuilder().MarkType(MarkType.None).Build();
			var defaultMarkTypeFactor = new FactorBuilder().MarkType(MarkType.Default).Build();
			var straightMarkTypeFactor = new FactorBuilder().MarkType(MarkType.Straight).Build();
			var reverseMarkTypeFactor = new FactorBuilder().MarkType(MarkType.Reverse).Build();
			var noneMarkTypeAttribute = new RegisterAttributeBuilder().Id(noneMarkTypeFactor.Id).Build();
			var defaultMarkTypeAttribute = new RegisterAttributeBuilder().Id(defaultMarkTypeFactor.Id).Build();
			var straightMarkTypeAttribute = new RegisterAttributeBuilder().Id(straightMarkTypeFactor.Id).Build();
			var reverseMarkTypeAttribute = new RegisterAttributeBuilder().Id(reverseMarkTypeFactor.Id).Build();

			ModelFactorsService.Setup(x => x.GetFactors(_model.Id, It.IsAny<KoAlgoritmType>()))
				.Returns(new List<OMModelFactor> { noneMarkTypeFactor, defaultMarkTypeFactor, straightMarkTypeFactor, reverseMarkTypeFactor });

			RegisterCacheWrapper.Setup(x => x.GetAttributeData(noneMarkTypeFactor.FactorId.Value)).Returns(noneMarkTypeAttribute);
			RegisterCacheWrapper.Setup(x => x.GetAttributeData(defaultMarkTypeFactor.FactorId.Value)).Returns(defaultMarkTypeAttribute);
			RegisterCacheWrapper.Setup(x => x.GetAttributeData(straightMarkTypeFactor.FactorId.Value)).Returns(straightMarkTypeAttribute);
			RegisterCacheWrapper.Setup(x => x.GetAttributeData(reverseMarkTypeFactor.FactorId.Value)).Returns(reverseMarkTypeAttribute);

			var formula = ModelingService.GetFormula(_model, KoAlgoritmType.Multi);

			var baseFormulaPart = $"{_model.A0ForMultiplicativeInFormula}";
			var noneMarkTypeFormulaPart = $"*(\"{noneMarkTypeAttribute.Name}\"+{noneMarkTypeFactor.WeightInFormula})^{noneMarkTypeFactor.B0InFormula}";
			var defaultMarkTypeFormulaPart = $"*(метка(\"{defaultMarkTypeAttribute.Name}\")+{defaultMarkTypeFactor.WeightInFormula})^{defaultMarkTypeFactor.B0InFormula}";
			var straightMarkTypeFormulaPart = $"*((\"{straightMarkTypeAttribute.Name}\"+{straightMarkTypeFactor.CorrectingTermInFormula})/{straightMarkTypeFactor.KInFormula} + {straightMarkTypeFactor.WeightInFormula})^{straightMarkTypeFactor.B0InFormula}";
			var reverseMarkTypeFormulaPart = $"*({reverseMarkTypeFactor.KInFormula}/(\"{reverseMarkTypeAttribute.Name}\"+{reverseMarkTypeFactor.CorrectingTermInFormula})+{reverseMarkTypeFactor.WeightInFormula})^{reverseMarkTypeFactor.B0InFormula}";
			var expectedFormula = baseFormulaPart + noneMarkTypeFormulaPart + defaultMarkTypeFormulaPart + straightMarkTypeFormulaPart + reverseMarkTypeFormulaPart;

			Assert.That(ProcessFormula(formula), Is.EqualTo(ProcessFormula(expectedFormula)));
		}


		#region Support Methods

		private void MockDependencies(OMModel model, OMModelFactor factor, RegisterAttribute cacheAttribute)
		{
			ModelFactorsService.Setup(x => x.GetFactors(model.Id, It.IsAny<KoAlgoritmType>()))
				.Returns(new List<OMModelFactor> { factor });

			RegisterCacheWrapper.Setup(x => x.GetAttributeData(factor.FactorId.Value))
				.Returns(cacheAttribute);
		}

		#endregion
	}
}