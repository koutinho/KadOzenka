using System.Collections.Generic;
using KadOzenka.Common.Tests.Builders.Cache;
using KadOzenka.Dal.UnitTests._Builders.Modeling.Factors;
using Moq;
using NUnit.Framework;
using ObjectModel.Directory;
using ObjectModel.Directory.Ko;
using ObjectModel.KO;

namespace KadOzenka.Dal.UnitTests.Modeling.Models.Formulas
{
	[Ignore("asd")]
	public class ExponentialTests : BaseFormulasTests
	{
		[Test]
		public void Can_Create_Formula_With_One_Factor_Without_Mark()
		{
			var factor = FactorBuilder.MarkType(MarkType.None).Build();
			MockDependencies(Model, factor, CacheAttribute);

			var formula = ModelingService.GetFormula(Model, KoAlgoritmType.Exp);

			var expectedFormula = $"{Model.A0ForExponential}*exp({CacheAttributeName}*{factor.B0InFormula})";
			Assert.That(ProcessFormula(formula), Is.EqualTo(ProcessFormula(expectedFormula)));
		}

		//[Test]
		//public void Can_Create_Formula_With_One_Factor_With_Negative_Parameters()
		//{
		//	var factor = FactorBuilder.MarkType(MarkType.Straight)
		//		.Correction(-1d).Coefficient(-2d)
		//		.CorrectingTerm(-3d).K(-4d)
		//		.Build();
		//	MockDependencies(Model, factor, CacheAttribute);

		//	var formula = ModelingService.GetFormula(Model, KoAlgoritmType.Exp);

		//	var expectedFormula = $"{Model.A0ForExponential}*exp(({CacheAttributeName}+({factor.CorrectingTermInFormula}))/({factor.KInFormula}) + ({factor.WeightInFormula}))^({factor.B0InFormula})";
		//	Assert.That(ProcessFormula(formula), Is.EqualTo(ProcessFormula(expectedFormula)));
		//}

		[Test]
		public void Can_Create_Formula_With_One_Factor_Of_Default_Mark()
		{
			var factor = FactorBuilder.MarkType(MarkType.Default).Build();
			MockDependencies(Model, factor, CacheAttribute);

			var formula = ModelingService.GetFormula(Model, KoAlgoritmType.Exp);

			var expectedFormula = $"{Model.A0ForExponential}*exp(метка({CacheAttributeName})*{factor.B0InFormula})";
			Assert.That(ProcessFormula(formula), Is.EqualTo(ProcessFormula(expectedFormula)));
		}

		[Test]
		public void Can_Create_Formula_With_One_Factor_Of_Straight_Mark()
		{
			var factor = FactorBuilder.MarkType(MarkType.Straight).Build();
			MockDependencies(Model, factor, CacheAttribute);

			var formula = ModelingService.GetFormula(Model, KoAlgoritmType.Exp);

			var expectedFormula = $"{Model.A0ForExponential}*exp(({CacheAttributeName}+{factor.CorrectingTermInFormula})/{factor.KInFormula} * {factor.B0InFormula})";
			Assert.That(ProcessFormula(formula), Is.EqualTo(ProcessFormula(expectedFormula)));
		}

		[Test]
		public void Can_Create_Formula_With_One_Factor_Of_Reverse_Mark()
		{
			var factor = FactorBuilder.MarkType(MarkType.Reverse).Build();
			MockDependencies(Model, factor, CacheAttribute);

			var formula = ModelingService.GetFormula(Model, KoAlgoritmType.Exp);

			var expectedFormula = $"{Model.A0ForExponential}*exp({factor.KInFormula}/({CacheAttributeName}+{factor.CorrectingTermInFormula})*{factor.B0InFormula})";
			Assert.That(ProcessFormula(formula), Is.EqualTo(ProcessFormula(expectedFormula)));
		}

		//[Test]
		//public void Can_Create_Formula_With_All_Possible_Factors_MarkType()
		//{
		//	var noneMarkTypeFactor = new FactorBuilder().MarkType(MarkType.None).Build();
		//	var defaultMarkTypeFactor = new FactorBuilder().MarkType(MarkType.Default).Build();
		//	var straightMarkTypeFactor = new FactorBuilder().MarkType(MarkType.Straight).Build();
		//	var reverseMarkTypeFactor = new FactorBuilder().MarkType(MarkType.Reverse).Build();
		//	var noneMarkTypeAttribute = new RegisterAttributeBuilder().Id(noneMarkTypeFactor.Id).Build();
		//	var defaultMarkTypeAttribute = new RegisterAttributeBuilder().Id(defaultMarkTypeFactor.Id).Build();
		//	var straightMarkTypeAttribute = new RegisterAttributeBuilder().Id(straightMarkTypeFactor.Id).Build();
		//	var reverseMarkTypeAttribute = new RegisterAttributeBuilder().Id(reverseMarkTypeFactor.Id).Build();

		//	ModelFactorsService.Setup(x => x.GetFactors(Model.Id, It.IsAny<KoAlgoritmType>()))
		//		.Returns(new List<OMModelFactor> { noneMarkTypeFactor, defaultMarkTypeFactor, straightMarkTypeFactor, reverseMarkTypeFactor });

		//	RegisterCacheWrapper.Setup(x => x.GetAttributeData(noneMarkTypeFactor.FactorId.Value)).Returns(noneMarkTypeAttribute);
		//	RegisterCacheWrapper.Setup(x => x.GetAttributeData(defaultMarkTypeFactor.FactorId.Value)).Returns(defaultMarkTypeAttribute);
		//	RegisterCacheWrapper.Setup(x => x.GetAttributeData(straightMarkTypeFactor.FactorId.Value)).Returns(straightMarkTypeAttribute);
		//	RegisterCacheWrapper.Setup(x => x.GetAttributeData(reverseMarkTypeFactor.FactorId.Value)).Returns(reverseMarkTypeAttribute);

		//	var formula = ModelingService.GetFormula(Model, KoAlgoritmType.Exp);

		//	var baseFormulaPart = $"{Model.A0ForExponential}*exp";
		//	var noneMarkTypeFormulaPart = $"*(\"{noneMarkTypeAttribute.Name}\"+{noneMarkTypeFactor.WeightInFormula})^{noneMarkTypeFactor.B0InFormula}";
		//	var defaultMarkTypeFormulaPart = $"*(метка(\"{defaultMarkTypeAttribute.Name}\")+{defaultMarkTypeFactor.WeightInFormula})^{defaultMarkTypeFactor.B0InFormula}";
		//	var straightMarkTypeFormulaPart = $"*((\"{straightMarkTypeAttribute.Name}\"+{straightMarkTypeFactor.CorrectingTermInFormula})/{straightMarkTypeFactor.KInFormula} + {straightMarkTypeFactor.WeightInFormula})^{straightMarkTypeFactor.B0InFormula}";
		//	var reverseMarkTypeFormulaPart = $"*({reverseMarkTypeFactor.KInFormula}/(\"{reverseMarkTypeAttribute.Name}\"+{reverseMarkTypeFactor.CorrectingTermInFormula})+{reverseMarkTypeFactor.WeightInFormula})^{reverseMarkTypeFactor.B0InFormula}";
		//	var expectedFormula = baseFormulaPart + noneMarkTypeFormulaPart + defaultMarkTypeFormulaPart + straightMarkTypeFormulaPart + reverseMarkTypeFormulaPart;

		//	Assert.That(ProcessFormula(formula), Is.EqualTo(ProcessFormula(expectedFormula)));
		//}
	}
}