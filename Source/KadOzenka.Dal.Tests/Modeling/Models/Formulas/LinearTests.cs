using System.Collections.Generic;
using KadOzenka.Common.Tests.Builders.Cache;
using KadOzenka.Dal.UnitTests._Builders.Modeling.Factors;
using NUnit.Framework;
using ObjectModel.Directory;
using ObjectModel.Directory.Ko;
using ObjectModel.KO;

namespace KadOzenka.Dal.UnitTests.Modeling.Models.Formulas
{
	public class LinearTests : BaseFormulasTests
	{
		protected override KoAlgoritmType AlgorithmType => KoAlgoritmType.Line;



		[Test]
		public void Can_Create_Formula_With_One_Factor_Without_Mark()
		{
			var factor = ModelFactorBuilder.MarkType(MarkType.None).Build();
			MockDependencies(Model, factor, CacheAttribute);

			var formula = ModelService.GetFormula(Model, KoAlgoritmType.Line);

			var expectedFormula = $"{Model.A0ForLinearInFormula}+{CacheAttributeName}*{factor.GetCoefficientInFormula(KoAlgoritmType.Line)}";
			Assert.That(ProcessFormula(formula), Is.EqualTo(ProcessFormula(expectedFormula)));
		}

		[Test]
		public void Can_Create_Formula_With_One_Factor_Of_Default_Mark()
		{
			var factor = ModelFactorBuilder.MarkType(MarkType.Default).Build();
			MockDependencies(Model, factor, CacheAttribute);

			var formula = ModelService.GetFormula(Model, KoAlgoritmType.Line);

			var expectedFormula = $"{Model.A0ForLinearInFormula}+метка({CacheAttributeName})*{factor.GetCoefficientInFormula(KoAlgoritmType.Line)}";
			Assert.That(ProcessFormula(formula), Is.EqualTo(ProcessFormula(expectedFormula)));
		}

		[Test]
		public void Can_Create_Formula_With_One_Factor_Of_Straight_Mark()
		{
			var factor = ModelFactorBuilder.MarkType(MarkType.Straight).Build();
			MockDependencies(Model, factor, CacheAttribute);

			var formula = ModelService.GetFormula(Model, KoAlgoritmType.Line);

			var expectedFormula = $"{Model.A0ForLinearInFormula}+({CacheAttributeName}+{factor.CorrectingTermInFormula})/{factor.KInFormula} * {factor.GetCoefficientInFormula(KoAlgoritmType.Line)}";
			Assert.That(ProcessFormula(formula), Is.EqualTo(ProcessFormula(expectedFormula)));
		}

		[Test]
		public void Can_Create_Formula_With_One_Factor_Of_Reverse_Mark()
		{
			var factor = ModelFactorBuilder.MarkType(MarkType.Reverse).Build();
			MockDependencies(Model, factor, CacheAttribute);

			var formula = ModelService.GetFormula(Model, KoAlgoritmType.Line);

			var expectedFormula = $"{Model.A0ForLinearInFormula}+{factor.KInFormula}/({CacheAttributeName}+{factor.CorrectingTermInFormula})*{factor.GetCoefficientInFormula(KoAlgoritmType.Line)}";
			Assert.That(ProcessFormula(formula), Is.EqualTo(ProcessFormula(expectedFormula)));
		}

		[Test]
		public void Can_Create_Formula_With_All_Possible_Factors_MarkType()
		{
			var noneMarkTypeFactor = new ModelFactorBuilder().MarkType(MarkType.None).Build();
			var defaultMarkTypeFactor = new ModelFactorBuilder().MarkType(MarkType.Default).Build();
			var straightMarkTypeFactor = new ModelFactorBuilder().MarkType(MarkType.Straight).Build();
			var reverseMarkTypeFactor = new ModelFactorBuilder().MarkType(MarkType.Reverse).Build();

			var noneMarkTypeAttribute = new RegisterAttributeBuilder().Id(noneMarkTypeFactor.Id).Name("none").Build();
			var defaultMarkTypeAttribute = new RegisterAttributeBuilder().Id(defaultMarkTypeFactor.Id).Name("def").Build();
			var straightMarkTypeAttribute = new RegisterAttributeBuilder().Id(straightMarkTypeFactor.Id).Name("straight").Build();
			var reverseMarkTypeAttribute = new RegisterAttributeBuilder().Id(reverseMarkTypeFactor.Id).Name("reverse").Build();

			ModelFactorsService.Setup(x => x.GetFactorsEntities(Model.Id))
				.Returns(new List<OMModelFactor> { noneMarkTypeFactor, defaultMarkTypeFactor, straightMarkTypeFactor, reverseMarkTypeFactor });

			RegisterCacheWrapper.Setup(x => x.GetAttributeData(noneMarkTypeFactor.FactorId)).Returns(noneMarkTypeAttribute);
			RegisterCacheWrapper.Setup(x => x.GetAttributeData(defaultMarkTypeFactor.FactorId)).Returns(defaultMarkTypeAttribute);
			RegisterCacheWrapper.Setup(x => x.GetAttributeData(straightMarkTypeFactor.FactorId)).Returns(straightMarkTypeAttribute);
			RegisterCacheWrapper.Setup(x => x.GetAttributeData(reverseMarkTypeFactor.FactorId)).Returns(reverseMarkTypeAttribute);

			var formula = ModelService.GetFormula(Model, KoAlgoritmType.Line);

			var baseFormulaPart = $"{Model.A0ForLinearInFormula}";
			var noneMarkTypeFormulaPart = $"+\"{noneMarkTypeAttribute.Name}\"*{noneMarkTypeFactor.GetCoefficientInFormula(KoAlgoritmType.Line)}";
			var defaultMarkTypeFormulaPart = $"+метка(\"{defaultMarkTypeAttribute.Name}\")*{defaultMarkTypeFactor.GetCoefficientInFormula(KoAlgoritmType.Line)}";
			var straightMarkTypeFormulaPart = $"+(\"{straightMarkTypeAttribute.Name}\"+{straightMarkTypeFactor.CorrectingTermInFormula})/{straightMarkTypeFactor.KInFormula} * {straightMarkTypeFactor.GetCoefficientInFormula(KoAlgoritmType.Line)}";
			var reverseMarkTypeFormulaPart = $"+{reverseMarkTypeFactor.KInFormula}/(\"{reverseMarkTypeAttribute.Name}\"+{reverseMarkTypeFactor.CorrectingTermInFormula})*{reverseMarkTypeFactor.GetCoefficientInFormula(KoAlgoritmType.Line)}";
			var expectedFormula = baseFormulaPart + noneMarkTypeFormulaPart + defaultMarkTypeFormulaPart + straightMarkTypeFormulaPart + reverseMarkTypeFormulaPart;

			Assert.That(ProcessFormula(formula), Is.EqualTo(ProcessFormula(expectedFormula)), $"{noneMarkTypeAttribute.Name}, {defaultMarkTypeAttribute.Name}, {straightMarkTypeAttribute.Name}, {reverseMarkTypeAttribute.Name}");
		}
	}
}