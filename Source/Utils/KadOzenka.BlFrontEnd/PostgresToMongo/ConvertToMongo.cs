//using System;
//using System.Text;
//using System.Linq;
//using System.Collections.Generic;
//using ObjectModel.Market;
//using MongoDB.Bson;
//using MongoDB.Driver;
//using Newtonsoft.Json;

//namespace KadOzenka.BlFrontEnd.PostgresToMongo
//{
//	public class ConvertToMongo
//    {
//	    public static void Convert(int count)
//        {
//            List<OMCoreObject> objects = OMCoreObject.Where(x => true).SelectAll(false).SetPackageSize(count).Execute();
//            objects.ForEach(x => x.PriceHistory = OMPriceHistory.Where(y => y.InitialId == x.Id).SelectAll().Execute());
//            //Console.WriteLine(JsonConvert.SerializeObject(objects, Formatting.Indented));
//            WriteToMongoDB(objects);
//        }

//        public static void WriteToMongoDB(List<OMCoreObject> objects)
//        {
//            MongoClient dbClient = new MongoClient("mongodb://localhost");
//            var database = dbClient.GetDatabase("MyDatabase");
//            if(database.GetCollection<BsonDocument>("KadOzenka")==null) database.CreateCollection("KadOzenka");
//            database.GetCollection<BsonDocument>("KadOzenka").InsertMany(objects.Select(x => x.ToBsonDocument()));
//            Console.WriteLine(DateTime.Now);
//        }
//    }
//}
