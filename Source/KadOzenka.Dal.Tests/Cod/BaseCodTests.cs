using System.Collections.Generic;
using KadOzenka.Common.Tests;
using KadOzenka.Dal.CodDictionary;
using KadOzenka.Dal.CodDictionary.Entities;
using KadOzenka.Dal.CommonFunctions;
using KadOzenka.Dal.RecycleBin;
using KadOzenka.Dal.Registers;
using KadOzenka.Dal.Registers.Entities;
using KadOzenka.Dal.Tests;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;
using Platform.Configurator.DbConfigurator;

namespace KadOzenka.Dal.UnitTests.Cod
{
    [TestFixture]
    public class BaseCodTests : BaseTests
    {
        protected ICodDictionaryService CodDictionaryService => Provider.GetService<ICodDictionaryService>();
        protected Mock<ICodDictionaryRepository> CodDictionaryRepository { get; set; }
        protected Mock<IRegisterAttributeRepository> RegisterAttributeRepository { get; set; }
        protected Mock<IRegisterAttributeService> RegisterAttributeService { get; set; }
        protected Mock<IRegisterConfiguratorWrapper> RegisterConfiguratorWrapper { get; set; }
        protected Mock<IRegisterObjectWrapper> RegisterObjectWrapper { get; set; }
        protected Mock<IRecycleBinService> RecycleBinService { get; set; }


        [SetUp]
        public void BaseTourSetUp()
        {
            RegisterService = new Mock<IRegisterService>();
            RegisterAttributeService = new Mock<IRegisterAttributeService>();
            RegisterObjectWrapper = new Mock<IRegisterObjectWrapper>();
            RecycleBinService = new Mock<IRecycleBinService>();
            CodDictionaryRepository = new Mock<ICodDictionaryRepository>();
            RegisterAttributeRepository = new Mock<IRegisterAttributeRepository>();
        }

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            RegisterConfiguratorWrapper = new Mock<IRegisterConfiguratorWrapper>();
            RegisterConfiguratorWrapper.Setup(x => x.GetDbConfigurator()).Returns(new DbConfiguratorPostgres(RandomGenerator.GetRandomString()));
        }


        protected override void AddServicesToContainer(ServiceCollection container)
        {
            container.AddTransient(typeof(ICodDictionaryService), typeof(CodDictionaryService));
            container.AddTransient(typeof(ICodDictionaryRepository), sp => CodDictionaryRepository.Object);
            container.AddTransient(typeof(IRegisterAttributeService), sp => RegisterAttributeService.Object);
            container.AddTransient(typeof(IRegisterConfiguratorWrapper), sp => RegisterConfiguratorWrapper.Object);
            container.AddTransient(typeof(IRegisterObjectWrapper), sp => RegisterObjectWrapper.Object);
            container.AddTransient(typeof(IRecycleBinService), sp => RecycleBinService.Object);
            container.AddTransient(typeof(IRegisterAttributeRepository), sp => RegisterAttributeRepository.Object);
        }

        protected CodDictionaryDto CreateDictionaryDto(string name = null, int numberOfValues = 1)
        {
            var values = new List<AttributePure>(numberOfValues);
            for (var i = 0; i < numberOfValues; i++)
            {
                values.Add(new AttributePure(RandomGenerator.GenerateRandomInteger(), RandomGenerator.GetRandomString()));
            }

            return new CodDictionaryDto
            {
                Name = name ?? RandomGenerator.GetRandomString(),
                Values = values
            };
        }

        protected CodDictionaryValue CreateDictionaryValue(string code = null, int numberOfValues = 1)
        {
            var resultCode = code ?? RandomGenerator.GetRandomString();
            
            var values = new List<CodDictionaryValuePure>(numberOfValues);
            for (var i = 0; i < numberOfValues; i++)
            {
                values.Add(new CodDictionaryValuePure(RandomGenerator.GenerateRandomInteger(), RandomGenerator.GetRandomString()));
            }

            return new CodDictionaryValue(RandomGenerator.GenerateRandomInteger(), resultCode, values);
        }
    }
}