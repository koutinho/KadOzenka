//using System.Collections.Generic;
//using System.IO;
//using Core.SRD;
//using Newtonsoft.Json;

//namespace KadOzenka.Dal.MapModeling
//{
//	public class MapTilesConfig
//	{
//		private static readonly Dictionary<int, List<dynamic>> PixelCoordinatesFromJsonConfigFileCache = new Dictionary<int, List<dynamic>>();
//		private static readonly object LockObject = new object();

//		public static MapTilesConfig Current => Core.ConfigParam.Configuration.GetParam<MapTilesConfig>("MapTilesConfig");

//		public string PixelCoordinatesJsonConfigFilesFolder { get; set; }
//		public string PixelCoordinatesJsonConfigFilePrefix { get; set; }

//		public string MarketMapHeatMapLayerFolder { get; set; }
//		public string ManagementDecisionSupportHeatMapLayerFolder { get; set; }
		
//		public int MCMinZoom { get; set; }
//		public int MCMaxZoom { get; set; }

//		public int MCImgWidth { get; set; }
//		public int MCImgHeight { get; set; }

//		public int MCHorizontalStartTile { get; set; }
//		public int MCVerticalStartTile { get; set; }
//		public int MCTileSize { get; set; }

//		public int InitialImageCacheLifeTime { get; set; }

//		public static List<dynamic> GetPixelCoordinatesFromJsonConfigFile(int currentZoom)
//		{
//			if (!PixelCoordinatesFromJsonConfigFileCache.ContainsKey(currentZoom))
//			{
//				InitPixelCoordinatesCache(currentZoom);
//			}

//			return PixelCoordinatesFromJsonConfigFileCache[currentZoom];
//		}

//		public static string GetMarketHeatMapInitialImageFileName(int currentZoom)
//		{
//			if (!Directory.Exists(
//				$"{Current.MarketMapHeatMapLayerFolder}\\user_{SRDSession.GetCurrentUserId().GetValueOrDefault()}\\InitialImages"))
//			{
//				Directory.CreateDirectory($"{Current.MarketMapHeatMapLayerFolder}\\user_{SRDSession.GetCurrentUserId().GetValueOrDefault()}\\InitialImages");
//			}

//			return
//				$"{Current.MarketMapHeatMapLayerFolder}\\user_{SRDSession.GetCurrentUserId().GetValueOrDefault()}\\InitialImages\\{currentZoom}.png";
//		}

//		public static string GetManagementDecisionSupportHeatMapInitialImageFileName(int currentZoom)
//		{
//			if (!Directory.Exists(
//				$"{Current.ManagementDecisionSupportHeatMapLayerFolder}\\user_{SRDSession.GetCurrentUserId().GetValueOrDefault()}\\InitialImages"))
//			{
//				Directory.CreateDirectory($"{Current.ManagementDecisionSupportHeatMapLayerFolder}\\user_{SRDSession.GetCurrentUserId().GetValueOrDefault()}\\InitialImages");
//			}

//			return
//				$"{Current.ManagementDecisionSupportHeatMapLayerFolder}\\user_{SRDSession.GetCurrentUserId().GetValueOrDefault()}\\InitialImages\\{currentZoom}.png";
//		}

//		private static void InitPixelCoordinatesCache(int currentZoom)
//		{
//			lock (LockObject)
//			{
//				if (PixelCoordinatesFromJsonConfigFileCache.ContainsKey(currentZoom))
//				{
//					return;
//				}

//				var fullFileName = GetPixelCoordinatesJsonFileName(currentZoom);
//				var strFile = File.ReadAllText(fullFileName);
//				var coords = JsonConvert.DeserializeObject<List<dynamic>>(strFile);
//				PixelCoordinatesFromJsonConfigFileCache.Add(currentZoom, coords);
//			}
//		}

//		private static string GetPixelCoordinatesJsonFileName(int currentZoom)
//		{
//			return
//				$"{Current.PixelCoordinatesJsonConfigFilesFolder}\\{Current.PixelCoordinatesJsonConfigFilePrefix}_{currentZoom}.json";
//		}
//	}
//}
