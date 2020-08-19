using System.Collections.Generic;
using System.IO;
using Core.SRD;
using Newtonsoft.Json;
using System.Drawing;

namespace KadOzenka.Dal.MapModeling
{
	public class MapTilesConfig
	{
		private static readonly Dictionary<int, List<dynamic>> PixelCoordinatesFromJsonConfigFileCache = new Dictionary<int, List<dynamic>>();
		private static readonly Dictionary<int, Dictionary<int, Image>> MarketMapInitialImageCache = new Dictionary<int, Dictionary<int, Image>>();

		private static readonly object LockObject = new object();

		public static MapTilesConfig Current => Core.ConfigParam.Configuration.GetParam<MapTilesConfig>("MapTilesConfig");

		public string PixelCoordinatesJsonConfigFilesFolder { get; set; }
		public string PixelCoordinatesJsonConfigFilePrefix { get; set; }

		public string MarketMapHeatMapLayerFolder { get; set; }
		
		public int MCMinZoom { get; set; }
		public int MCMaxZoom { get; set; }

		public int MCImgWidth { get; set; }
		public int MCImgHeight { get; set; }

		public int MCHorizontalStartTile { get; set; }
		public int MCVerticalStartTile { get; set; }
		public int MCTileSize { get; set; }

		public static List<dynamic> GetPixelCoordinatesFromJsonConfigFile(int currentZoom)
		{
			if (!PixelCoordinatesFromJsonConfigFileCache.ContainsKey(currentZoom))
			{
				InitPixelCoordinatesCache(currentZoom);
			}

			return PixelCoordinatesFromJsonConfigFileCache[currentZoom];
		}

		public static string GetMarketHeatMapInitialImageFileName(int currentZoom)
		{
			if (!Directory.Exists(
				$"{Current.MarketMapHeatMapLayerFolder}\\user_{SRDSession.GetCurrentUserId()}\\InitialImages"))
			{
				Directory.CreateDirectory($"{Current.MarketMapHeatMapLayerFolder}\\user_{SRDSession.GetCurrentUserId()}\\InitialImages");
			}

			return
				$"{Current.MarketMapHeatMapLayerFolder}\\user_{SRDSession.GetCurrentUserId()}\\InitialImages\\{currentZoom}.png";
		}

		public static Image GetMarketHeatMapInitialImage(int currentZoom)
		{
			if (MarketMapInitialImageCache.ContainsKey(SRDSession.GetCurrentUserId().GetValueOrDefault())
			    && MarketMapInitialImageCache[SRDSession.GetCurrentUserId().GetValueOrDefault()]
				    .ContainsKey(currentZoom))
			{
				return MarketMapInitialImageCache[SRDSession.GetCurrentUserId().GetValueOrDefault()][currentZoom];
			}

			return null;
		}

		public static void AddMarketHeatMapInitialImages(int zoom, Image image)
		{
			if (!MarketMapInitialImageCache.ContainsKey(SRDSession.GetCurrentUserId().GetValueOrDefault()))
			{
				lock (LockObject)
				{
					if (!MarketMapInitialImageCache.ContainsKey(SRDSession.GetCurrentUserId().GetValueOrDefault()))
					{
						MarketMapInitialImageCache.Add(SRDSession.GetCurrentUserId().GetValueOrDefault(),
							new Dictionary<int, Image>());
					}
				}
			}

			if (!MarketMapInitialImageCache[SRDSession.GetCurrentUserId().GetValueOrDefault()].ContainsKey(zoom))
			{
				lock (LockObject)
				{
					if (!MarketMapInitialImageCache[SRDSession.GetCurrentUserId().GetValueOrDefault()].ContainsKey(zoom))
					{
						MarketMapInitialImageCache[SRDSession.GetCurrentUserId().GetValueOrDefault()].Add(zoom, image);
					}
				}
			}
		}

		public static void ClearMarketHeatMapInitialImages()
		{
			if (MarketMapInitialImageCache.ContainsKey(SRDSession.GetCurrentUserId().GetValueOrDefault()))
			{
				lock (LockObject)
				{
					var value = MarketMapInitialImageCache[SRDSession.GetCurrentUserId().GetValueOrDefault()];
					for (var i = Current.MCMinZoom; i <= Current.MCMaxZoom; i++)
					{
						if (value.ContainsKey(i))
							value[i].Dispose();
					}

					MarketMapInitialImageCache[SRDSession.GetCurrentUserId().GetValueOrDefault()].Clear();
				}
			}
		}

		private static void InitPixelCoordinatesCache(int currentZoom)
		{
			lock (LockObject)
			{
				if (PixelCoordinatesFromJsonConfigFileCache.ContainsKey(currentZoom))
				{
					return;
				}

				var fullFileName = GetPixelCoordinatesJsonFileName(currentZoom);
				var strFile = File.ReadAllText(fullFileName);
				var coords = JsonConvert.DeserializeObject<List<dynamic>>(strFile);
				PixelCoordinatesFromJsonConfigFileCache.Add(currentZoom, coords);
			}
		}

		private static string GetPixelCoordinatesJsonFileName(int currentZoom)
		{
			return
				$"{Current.PixelCoordinatesJsonConfigFilesFolder}\\{Current.PixelCoordinatesJsonConfigFilePrefix}_{currentZoom}.json";
		}
	}
}
