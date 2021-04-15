using Moq;
using NUnit.Framework;
using ObjectModel.Common;
using ObjectModel.KO;

namespace KadOzenka.Dal.Tests.Cod.Dictionary
{
    [TestFixture]
    public class DeletionTests : BaseCodTests
    {
        [Test]
        public void Must_Use_Soft_Deletion()
        {
            var dictionary = new OMCodJob {Id = RandomGenerator.GenerateRandomInteger(), RegisterId = RandomGenerator.GenerateRandomInteger()};
            CodDictionaryRepository.Setup(x => x.GetById(dictionary.Id, null)).Returns(dictionary);

            CodDictionaryService.DeleteDictionary(dictionary.Id);

            RegisterService.Verify(x => x.RemoveRegister(dictionary.RegisterId, It.IsAny<long>()), Times.Once);
            RecycleBinService.Verify(x => x.Save(It.IsAny<OMRecycleBin>()), Times.Once);
            RecycleBinService.Verify(x => x.MoveObjectToRecycleBin(dictionary.Id, OMCodJob.GetRegisterId(), It.IsAny<long>()), Times.Once);
        }
    }
}