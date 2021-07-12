using System.Collections.Generic;
using DevExpress.Office.Utils;
using KadOzenka.Common.Tests.Builders.Cache;
using KadOzenka.Dal.UnitTests._Builders.Modeling.Factors;
using NUnit.Framework;
using ObjectModel.Directory;
using ObjectModel.Directory.Ko;
using ObjectModel.KO;

namespace KadOzenka.Dal.UnitTests.Modeling.Models.Formulas
{
	public class ExponentialTests : BaseFormulasTests
	{
		protected override KoAlgoritmType AlgorithmType => KoAlgoritmType.Exp;



		[Test]
		public void Can_Create_Formula_With_One_Factor_Without_Mark()
		{
			var factor = FactorBuilder.MarkType(MarkType.None).Build();
			MockDependencies(Model, factor, CacheAttribute);

			var formula = ModelService.GetFormula(Model, KoAlgoritmType.Exp);

			var expectedFormula = $"{Model.A0ForExponentialInFormula}*exp({CacheAttributeName}*{factor.B0InFormula})";
			Assert.That(ProcessFormula(formula), Is.EqualTo(ProcessFormula(expectedFormula)));
		}

		[Test]
		public void Can_Create_Formula_With_One_Factor_Of_Default_Mark()
		{
			var factor = FactorBuilder.MarkType(MarkType.Default).Build();
			MockDependencies(Model, factor, CacheAttribute);

			var formula = ModelService.GetFormula(Model, KoAlgoritmType.Exp);

			var expectedFormula = $"{Model.A0ForExponentialInFormula}*exp(метка({CacheAttributeName})*{factor.B0InFormula})";
			Assert.That(ProcessFormula(formula), Is.EqualTo(ProcessFormula(expectedFormula)));
		}

		[Test]
		public void Can_Create_Formula_With_One_Factor_Of_Straight_Mark()
		{
			var factor = FactorBuilder.MarkType(MarkType.Straight).Build();
			MockDependencies(Model, factor, CacheAttribute);

			var formula = ModelService.GetFormula(Model, KoAlgoritmType.Exp);

			var expectedFormula = $"{Model.A0ForExponentialInFormula}*exp(({CacheAttributeName}+{factor.CorrectingTermInFormula})/{factor.KInFormula} * {factor.B0InFormula})";
			Assert.That(ProcessFormula(formula), Is.EqualTo(ProcessFormula(expectedFormula)));
		}

		[Test]
		public void Can_Create_Formula_With_One_Factor_Of_Reverse_Mark()
		{
			var factor = FactorBuilder.MarkType(MarkType.Reverse).Build();
			MockDependencies(Model, factor, CacheAttribute);

			var formula = ModelService.GetFormula(Model, KoAlgoritmType.Exp);

			var expectedFormula = $"{Model.A0ForExponentialInFormula}*exp({factor.KInFormula}/({CacheAttributeName}+{factor.CorrectingTermInFormula})*{factor.B0InFormula})";
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

			ModelFactorsService.Setup(x => x.GetFactors(Model.Id, Model.AlgoritmType_Code))
				.Returns(new List<OMModelFactor> { noneMarkTypeFactor, defaultMarkTypeFactor, straightMarkTypeFactor, reverseMarkTypeFactor });

			RegisterCacheWrapper.Setup(x => x.GetAttributeData(noneMarkTypeFactor.FactorId.Value)).Returns(noneMarkTypeAttribute);
			RegisterCacheWrapper.Setup(x => x.GetAttributeData(defaultMarkTypeFactor.FactorId.Value)).Returns(defaultMarkTypeAttribute);
			RegisterCacheWrapper.Setup(x => x.GetAttributeData(straightMarkTypeFactor.FactorId.Value)).Returns(straightMarkTypeAttribute);
			RegisterCacheWrapper.Setup(x => x.GetAttributeData(reverseMarkTypeFactor.FactorId.Value)).Returns(reverseMarkTypeAttribute);

			var formula = ModelService.GetFormula(Model, Model.AlgoritmType_Code);

			var baseFormulaPart = $"{Model.A0ForExponentialInFormula}*exp";
			var noneMarkTypeFormulaPart = $"\"{noneMarkTypeAttribute.Name}\"*{noneMarkTypeFactor.B0InFormula}";
			var defaultMarkTypeFormulaPart = $"+ метка(\"{defaultMarkTypeAttribute.Name}\")*{defaultMarkTypeFactor.B0InFormula}";
			var straightMarkTypeFormulaPart = $"+ (\"{straightMarkTypeAttribute.Name}\"+{straightMarkTypeFactor.CorrectingTermInFormula})/{straightMarkTypeFactor.KInFormula} * {straightMarkTypeFactor.B0InFormula}";
			var reverseMarkTypeFormulaPart = $"+ {reverseMarkTypeFactor.KInFormula}/(\"{reverseMarkTypeAttribute.Name}\"+{reverseMarkTypeFactor.CorrectingTermInFormula})*{reverseMarkTypeFactor.B0InFormula}";
			var expectedFormula = baseFormulaPart + "(" + noneMarkTypeFormulaPart + defaultMarkTypeFormulaPart + straightMarkTypeFormulaPart + reverseMarkTypeFormulaPart + ")";

			Assert.That(ProcessFormula(formula), Is.EqualTo(ProcessFormula(expectedFormula)), $"{noneMarkTypeAttribute.Name}, {defaultMarkTypeAttribute.Name}, {straightMarkTypeAttribute.Name}, {reverseMarkTypeAttribute.Name}");
		}
	}
}