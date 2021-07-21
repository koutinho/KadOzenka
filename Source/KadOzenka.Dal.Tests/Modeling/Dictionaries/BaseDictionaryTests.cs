using KadOzenka.Dal.Tests;
using Microsoft.Extensions.DependencyInjection;
using ModelingBusiness.Dictionaries;
using ModelingBusiness.Dictionaries.Repositories;
using ModelingBusiness.Model;
using Moq;
using NUnit.Framework;

namespace KadOzenka.Dal.UnitTests.Modeling.Dictionaries
{
	public class BaseDictionaryTests : BaseTests
	{
		protected ModelDictionaryService ModelDictionaryService => Provider.GetService<ModelDictionaryService>();
		protected Mock<IModelDictionaryRepository> ModelDictionaryRepository { get; set; }
		protected Mock<IModelMarksRepository> ModelMarksRepository { get; set; }


		[SetUp]
		public void BaseDictionaryTestsSetUp()
		{
			ModelDictionaryRepository = new Mock<IModelDictionaryRepository>();
			ModelMarksRepository = new Mock<IModelMarksRepository>();
		}


		protected override void AddServicesToContainer(ServiceCollection container)
		{
			container.AddTransient<ModelService>();
			container.AddTransient<ModelDictionaryService>();
			container.AddTransient(typeof(IModelDictionaryRepository), sp => ModelDictionaryRepository.Object);
			container.AddTransient(typeof(IModelMarksRepository), sp => ModelMarksRepository.Object);
		}
	}
}