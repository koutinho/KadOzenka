using KadOzenka.Dal.GbuObject;
using System;
using System.Collections.Generic;
using System.Text;

namespace KadOzenka.BlFrontEnd.GbuTest
{
    public static class GbuTests
    {


		public static void TestGetDataFromAllpri()
		{
			// Площадь из Росреестра
			new GbuObjectAttribute
			{
				ObjectId = 10680905,
				AttributeId = 2,
				S = new DateTime(2018, 1, 1),
				Ot = new DateTime(2018, 1, 13),
				NumValue = 1000,
				ChangeDocId = 1
			}.Save();

			new GbuObjectAttribute
			{
				ObjectId = 10680905,
				AttributeId = 2,
				S = new DateTime(2019, 1, 1),
				Ot = new DateTime(2019, 1, 12),
				NumValue = 1001,
				ChangeDocId = 1
			}.Save();


			// Пример 1: получить значение актуальное на 01.03.2019 (Test 3)
			var attributes = new GbuObjectService().GetAllAttributes(10680905, new List<long> { 1 }, new List<long> { 421 }, new DateTime(2019, 3, 1));


			// Пример 2: получить значение актуальное на 18.02.2018, без учета изменений, которые были после 01.02.2018 (Test 1)
			attributes = new GbuObjectService().GetAllAttributes(10680905, new List<long> { 1 }, new List<long> { 421 }, new DateTime(2018, 2, 18), new DateTime(2018, 2, 1));
		}
	}
}
