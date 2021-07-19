using KadOzenka.Common.Tests.Builders.Modeling.Dictionaries;
using KadOzenka.Dal.UnitTests._Builders.Modeling.Dictionaries;
using KadOzenka.Dal.UnitTests.Modeling.Models;
using ModelingBusiness.Dictionaries.Exceptions;
using Moq;
using NUnit.Framework;
using ObjectModel.Directory.KO;
using ObjectModel.KO;

namespace KadOzenka.Dal.UnitTests.Modeling.Dictionaries
{
	[TestFixture]
	public class MarkCreationTests : BaseModelTests
	{
		[Test]
		public void CanNot_Create_Mark_Without_Value()
		{
			var dictionary = new DictionaryBuilder().Build();
			var markDto = new MarkDtoBuilder().Dictionary(dictionary).Value(null).Build();
			ModelDictionaryRepository.Setup(x => x.GetById(markDto.DictionaryId, null)).Returns(dictionary);
			ModelMarksRepository.Setup(x => x.IsTheSameMarkExists(markDto.DictionaryId, markDto.Id, markDto.Value)).Returns(false);

			Assert.Throws<EmptyMarkValueException>(() => ModelDictionaryService.CreateMark(markDto));

			ModelMarksRepository.Verify(foo => foo.Save(It.IsAny<OMModelingDictionariesValues>()), Times.Never);
		}

		[Test]
		public void CanNot_Create_Mark_Without_CalculationValue()
		{
			var dictionary = new DictionaryBuilder().Build();
			var markDto = new MarkDtoBuilder().Dictionary(dictionary).CalculationValue(null).Build();
			ModelDictionaryRepository.Setup(x => x.GetById(markDto.DictionaryId, null)).Returns(dictionary);
			ModelMarksRepository.Setup(x => x.IsTheSameMarkExists(markDto.DictionaryId, markDto.Id, markDto.Value)).Returns(false);

			Assert.Throws<EmptyMarkCalculationValueException>(() => ModelDictionaryService.CreateMark(markDto));

			ModelMarksRepository.Verify(foo => foo.Save(It.IsAny<OMModelingDictionariesValues>()), Times.Never);
		}

		[Test]
		public void CanNot_Create_Mark_If_The_Same_Mark_Already_Exists()
		{
			var dictionary = new DictionaryBuilder().Build();
			var markDto = new MarkDtoBuilder().Dictionary(dictionary).Build();
			ModelDictionaryRepository.Setup(x => x.GetById(markDto.DictionaryId, null)).Returns(dictionary);
			ModelMarksRepository.Setup(x => x.IsTheSameMarkExists(markDto.DictionaryId, markDto.Id, markDto.Value)).Returns(true);

			Assert.Throws<TheSameMarkExistsException>(() => ModelDictionaryService.CreateMark(markDto));

			ModelMarksRepository.Verify(foo => foo.Save(It.IsAny<OMModelingDictionariesValues>()), Times.Never);
		}

		[Test]
		public void CanNot_Create_Mark_If_Value_Have_Different_Type_Then_Dictionary()
		{
			var dictionary = new DictionaryBuilder().Type(ModelDictionaryType.Integer).Build();
			var markDto = new MarkDtoBuilder().Dictionary(dictionary).Build();
			ModelDictionaryRepository.Setup(x => x.GetById(markDto.DictionaryId, null)).Returns(dictionary);
			ModelMarksRepository.Setup(x => x.IsTheSameMarkExists(markDto.DictionaryId, markDto.Id, markDto.Value)).Returns(false);

			Assert.Throws<MarkValueConvertingException>(() => ModelDictionaryService.CreateMark(markDto));

			ModelMarksRepository.Verify(foo => foo.Save(It.IsAny<OMModelingDictionariesValues>()), Times.Never);
		}

		[Test]
		public void Can_Create_Mark()
		{
			var dictionary = new DictionaryBuilder().Build();
			var markDto = new MarkDtoBuilder().Dictionary(dictionary).Build();
			ModelDictionaryRepository.Setup(x => x.GetById(markDto.DictionaryId, null)).Returns(dictionary);
			ModelMarksRepository.Setup(x => x.IsTheSameMarkExists(markDto.DictionaryId, markDto.Id, markDto.Value)).Returns(false);

			ModelDictionaryService.CreateMark(markDto);

			ModelMarksRepository.Verify(foo => foo.Save(It.IsAny<OMModelingDictionariesValues>()), Times.Once);
		}
	}
}