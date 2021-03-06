using System.Collections.Generic;
using System.IO;
using Core.Shared.Misc;
using Core.SRD;
using Newtonsoft.Json;

namespace CommonSdks.ConfigurationManagers.KadOzenkaConfigManager.Models
{
	public class MapTilesConfig
	{
		private readonly Dictionary<int, List<dynamic>> _pixelCoordinatesFromJsonConfigFileCache = new();

		private static readonly object LockObject = new();

		public string PixelCoordinatesJsonConfigFilesFolder { get; set; }
		public string PixelCoordinatesJsonConfigFilePrefix { get; set; }

		public string MarketMapHeatMapLayerFolder { get; set; }
		public string ManagementDecisionSupportHeatMapLayerFolder { get; set; }

		public int MCMinZoom { get; set; }
		public int MCMaxZoom { get; set; }

		public int MCImgWidth { get; set; }
		public int MCImgHeight { get; set; }

		public int MCHorizontalStartTile { get; set; }
		public int MCVerticalStartTile { get; set; }
		public int MCTileSize { get; set; }

		public int InitialImageCacheLifeTime { get; set; }

		public List<dynamic> GetPixelCoordinatesFromJsonConfigFile(int currentZoom)
		{
			lock (LockObject)
			{
				if (!_pixelCoordinatesFromJsonConfigFileCache.ContainsKey(currentZoom))
				{
					InitPixelCoordinatesCache(currentZoom);
				}
			}

			lock (LockObject)
			{
				return _pixelCoordinatesFromJsonConfigFileCache[currentZoom];
			}
		}

		public string GetMarketHeatMapInitialImageFileName(int currentZoom)
		{
			var directory = FileSystemHelper.CombinePath(MarketMapHeatMapLayerFolder,
				$"user_{SRDSession.GetCurrentUserId().GetValueOrDefault()}", "InitialImages");
			if (!Directory.Exists(directory))
			{
				Directory.CreateDirectory(directory);
			}

			return FileSystemHelper.CombinePath(directory, $"{currentZoom}.png");
		}

		public string GetManagementDecisionSupportHeatMapInitialImageFileName(int currentZoom)
		{
			var directory = FileSystemHelper.CombinePath(ManagementDecisionSupportHeatMapLayerFolder,
				$"user_{SRDSession.GetCurrentUserId().GetValueOrDefault()}", "InitialImages");
			if (!Directory.Exists(directory))
			{
				Directory.CreateDirectory(directory);
			}

			return FileSystemHelper.CombinePath(directory, $"{currentZoom}.png");
		}

		private void InitPixelCoordinatesCache(int currentZoom)
		{
			lock (LockObject)
			{
				if (_pixelCoordinatesFromJsonConfigFileCache.ContainsKey(currentZoom))
				{
					return;
				}

				var fullFileName = GetPixelCoordinatesJsonFileName(currentZoom);
				var strFile = File.ReadAllText(fullFileName);
				var coords = JsonConvert.DeserializeObject<List<dynamic>>(strFile);
				_pixelCoordinatesFromJsonConfigFileCache.Add(currentZoom, coords);
			}
		}

		private string GetPixelCoordinatesJsonFileName(int currentZoom)
		{
			return FileSystemHelper.CombinePath(PixelCoordinatesJsonConfigFilesFolder,
				$"{PixelCoordinatesJsonConfigFilePrefix}_{currentZoom}.json");
		}
	}
}