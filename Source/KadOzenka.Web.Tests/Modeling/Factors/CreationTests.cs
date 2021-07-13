﻿using System.Collections.Generic;
using Core.Register.RegisterEntities;
using KadOzenka.Common.Tests;
using KadOzenka.Web.Models.Modeling;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace KadOzenka.Web.UnitTests.Modeling.Factors
{
	public class CreationTests : BaseModelingTests
	{
		[Ignore("Продумать, как замокать константы в проекте для MarketPlace")]
		[Test]
		public void Can_Return_View_During_Automatic_Factor_Addition()
		{
			var modelId = RandomGenerator.GenerateRandomInteger();
			var result = ModelingController.AddAutomaticModelFactor(modelId);

			var view = result as ViewResult;
			var modelFromView = view?.ViewData.Model as AutomaticFactorModel;

			Assert.IsNotNull(modelFromView);
			Assert.That(modelFromView.Id, Is.EqualTo(-1));
			Assert.That(modelFromView.ModelId, Is.EqualTo(modelId));
		}
	}
}