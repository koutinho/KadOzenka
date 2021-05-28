//using System.Collections.Generic;
//using KadOzenka.Dal.ExpressScore;
//using KadOzenka.Dal.ExpressScore.Dto;
//using KadOzenka.Dal.Registers;
//using KadOzenka.Dal.ScoreCommon;
//using ObjectModel.Directory;
//using System;
//using MarketPlaceBusiness;
//using Microsoft.Rest.Serialization;
//using Newtonsoft.Json;
//using Newtonsoft.Json.Serialization;

//namespace KadOzenka.BlFrontEnd.ExpressScore
//{
//	public class TestServiceES
//	{
//		public static void TestDataResultGrid()
//		{
//			ScoreCommonService scoreCommonService = new ScoreCommonService();
//			RegisterAttributeService registerAttributeService = new RegisterAttributeService();
//			var esService = new ExpressScoreService(scoreCommonService, registerAttributeService, new MarketObjectsForExpressScoreService());

//			List<AnalogResultDto> analogResult = new List<AnalogResultDto>
//			{
//				new AnalogResultDto
//				{
//					Kn = "77:01:0001005:1025",
//					Address = "Россия, Москва, Моховая улица, 15/1",
//					Id = 41979103
//				}
//			}; 
//			var res  = JsonConvert.SerializeObject(esService.GetDataToGrid(MarketSegment.Office, analogResult), new JsonSerializerSettings
//			{
//				ContractResolver = new CamelCasePropertyNamesContractResolver()
//			});
//			if (res.Length > 0)
//			{
//				Console.WriteLine("Success");
//			}
//		}
//	}
//}