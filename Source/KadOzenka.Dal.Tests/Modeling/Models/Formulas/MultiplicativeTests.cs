using System.Collections.Generic;
using KadOzenka.Common.Tests.Builders.Cache;
using KadOzenka.Dal.UnitTests._Builders.Modeling.Factors;
using NUnit.Framework;
using ObjectModel.Directory;
using ObjectModel.Directory.Ko;
using ObjectModel.KO;

namespace KadOzenka.Dal.UnitTests.Modeling.Models.Formulas
{
	public class MultiplicativeTests : BaseFormulasTests
	{
		protected override KoAlgoritmType AlgorithmType => KoAlgoritmType.Multi;



		[Test]
		public void Can_Create_Formula_With_One_Factor_Without_Mark()
		{
			var factor = FactorBuilder.MarkType(MarkType.None).Build();
			MockDependencies(Model, factor, CacheAttribute);

			var formula = ModelService.GetFormula(Model, KoAlgoritmType.Multi);

			var expectedFormula = $"{Model.A0ForMultiplicativeInFormula}*({CacheAttributeName}+{factor.CorrectionInFormula})^{factor.GetCoefficientInFormula(KoAlgoritmType.Multi)}";
			Assert.That(ProcessFormula(formula), Is.EqualTo(ProcessFormula(expectedFormula)));
		}

		[Test]
		public void Can_Create_Formula_With_One_Factor_With_Negative_Parameters()
		{
			var type = KoAlgoritmType.Multi;
			var factor = FactorBuilder.MarkType(MarkType.Straight)
				.Correction(-1d).Coefficient(-2d, type)
				.CorrectingTerm(-3d).K(-4d)
				.Build();
			MockDependencies(Model, factor, CacheAttribute);

			var formula = ModelService.GetFormula(Model, type);

			var expectedFormula = $"{Model.A0ForMultiplicativeInFormula}*(({CacheAttributeName}+({factor.CorrectingTermInFormula}))/({factor.KInFormula}) + ({factor.CorrectionInFormula}))^({factor.GetCoefficientInFormula(KoAlgoritmType.Multi)})";
			Assert.That(ProcessFormula(formula), Is.EqualTo(ProcessFormula(expectedFormula)));
		}

		[Test]
		public void Can_Create_Formula_With_One_Factor_Of_Default_Mark()
		{
			var factor = FactorBuilder.MarkType(MarkType.Default).Build();
			MockDependencies(Model, factor, CacheAttribute);

			var formula = ModelService.GetFormula(Model, KoAlgoritmType.Multi);

			var expectedFormula = $"{Model.A0ForMultiplicativeInFormula}*(метка({CacheAttributeName})+{factor.CorrectionInFormula})^{factor.GetCoefficientInFormula(KoAlgoritmType.Multi)}";
			Assert.That(ProcessFormula(formula), Is.EqualTo(ProcessFormula(expectedFormula)));
		}

		[Test]
		public void Can_Create_Formula_With_One_Factor_Of_Straight_Mark()
		{
			var factor = FactorBuilder.MarkType(MarkType.Straight).Build();
			MockDependencies(Model, factor, CacheAttribute);

			var formula = ModelService.GetFormula(Model, KoAlgoritmType.Multi);

			var expectedFormula = $"{Model.A0ForMultiplicativeInFormula}*(({CacheAttributeName}+{factor.CorrectingTermInFormula})/{factor.KInFormula} + {factor.CorrectionInFormula})^{factor.GetCoefficientInFormula(KoAlgoritmType.Multi)}";
			Assert.That(ProcessFormula(formula), Is.EqualTo(ProcessFormula(expectedFormula)));
		}

		[Test]
		public void Can_Create_Formula_With_One_Factor_Of_Reverse_Mark()
		{
			var factor = FactorBuilder.MarkType(MarkType.Reverse).Build();
			MockDependencies(Model, factor, CacheAttribute);

			var formula = ModelService.GetFormula(Model, KoAlgoritmType.Multi);

			var expectedFormula = $"{Model.A0ForMultiplicativeInFormula}*({factor.KInFormula}/({CacheAttributeName}+{factor.CorrectingTermInFormula})+{factor.CorrectionInFormula})^{factor.GetCoefficientInFormula(KoAlgoritmType.Multi)}";
			Assert.That(ProcessFormula(formula), Is.EqualTo(ProcessFormula(expectedFormula)));
		}

		[Test]
		public void Can_Create_Formula_With_All_Possible_Factors_MarkType()
		{
			var noneMarkTypeFactor = new FactorBuilder().MarkType(MarkType.None).Build();
			var defaultMarkTypeFactor = new FactorBuilder().MarkType(MarkType.Default).Build();
			var straightMarkTypeFactor = new FactorBuilder().MarkType(MarkType.Straight).Build();
			var reverseMarkTypeFactor = new FactorBuilder().MarkType(MarkType.Reverse).Build();

			var noneMarkTypeAttribute = new RegisterAttributeBuilder().Id(noneMarkTypeFactor.Id).Name("none").Build();
			var defaultMarkTypeAttribute = new RegisterAttributeBuilder().Id(defaultMarkTypeFactor.Id).Name("def").Build();
			var straightMarkTypeAttribute = new RegisterAttributeBuilder().Id(straightMarkTypeFactor.Id).Name("straight").Build();
			var reverseMarkTypeAttribute = new RegisterAttributeBuilder().Id(reverseMarkTypeFactor.Id).Name("reverse").Build();

			ModelFactorsService.Setup(x => x.GetFactorsEntities(Model.Id))
				.Returns(new List<OMModelFactor> { noneMarkTypeFactor, defaultMarkTypeFactor, straightMarkTypeFactor, reverseMarkTypeFactor });

			RegisterCacheWrapper.Setup(x => x.GetAttributeData(noneMarkTypeFactor.FactorId.Value)).Returns(noneMarkTypeAttribute);
			RegisterCacheWrapper.Setup(x => x.GetAttributeData(defaultMarkTypeFactor.FactorId.Value)).Returns(defaultMarkTypeAttribute);
			RegisterCacheWrapper.Setup(x => x.GetAttributeData(straightMarkTypeFactor.FactorId.Value)).Returns(straightMarkTypeAttribute);
			RegisterCacheWrapper.Setup(x => x.GetAttributeData(reverseMarkTypeFactor.FactorId.Value)).Returns(reverseMarkTypeAttribute);

			var formula = ModelService.GetFormula(Model, Model.AlgoritmType_Code);

			var baseFormulaPart = $"{Model.A0ForMultiplicativeInFormula}";
			var noneMarkTypeFormulaPart = $"*(\"{noneMarkTypeAttribute.Name}\"+{noneMarkTypeFactor.CorrectionInFormula})^{noneMarkTypeFactor.GetCoefficientInFormula(KoAlgoritmType.Multi)}";
			var defaultMarkTypeFormulaPart = $"*(метка(\"{defaultMarkTypeAttribute.Name}\")+{defaultMarkTypeFactor.CorrectionInFormula})^{defaultMarkTypeFactor.GetCoefficientInFormula(KoAlgoritmType.Multi)}";
			var straightMarkTypeFormulaPart = $"*((\"{straightMarkTypeAttribute.Name}\"+{straightMarkTypeFactor.CorrectingTermInFormula})/{straightMarkTypeFactor.KInFormula} + {straightMarkTypeFactor.CorrectionInFormula})^{straightMarkTypeFactor.GetCoefficientInFormula(KoAlgoritmType.Multi)}";
			var reverseMarkTypeFormulaPart = $"*({reverseMarkTypeFactor.KInFormula}/(\"{reverseMarkTypeAttribute.Name}\"+{reverseMarkTypeFactor.CorrectingTermInFormula})+{reverseMarkTypeFactor.CorrectionInFormula})^{reverseMarkTypeFactor.GetCoefficientInFormula(KoAlgoritmType.Multi)}";
			var expectedFormula = baseFormulaPart + noneMarkTypeFormulaPart + defaultMarkTypeFormulaPart + straightMarkTypeFormulaPart + reverseMarkTypeFormulaPart;

			Assert.That(ProcessFormula(formula), Is.EqualTo(ProcessFormula(expectedFormula)), $"{noneMarkTypeAttribute.Name}, {defaultMarkTypeAttribute.Name}, {straightMarkTypeAttribute.Name}, {reverseMarkTypeAttribute.Name}");
		}
	}
}