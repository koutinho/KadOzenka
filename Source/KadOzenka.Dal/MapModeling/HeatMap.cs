using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using CommonSdks.ConfigurationManagers;
using Image = System.Drawing.Image;

namespace KadOzenka.Dal.MapModeling
{

	public abstract class HeatMap
	{
		private static readonly object LockObject = new object();

		protected abstract void ClearHeatMapInitialImages();
		protected abstract string GetHeatMapInitialImageFileName(int zoom);
		protected abstract Image GetHeatMapInitialImage(int zoom);

		public int MapMinZoom => ConfigurationManager.KoConfig.MapTilesConfig.MCMinZoom;
		public int MapMaxZoom => ConfigurationManager.KoConfig.MapTilesConfig.MCMaxZoom;

		public void GenerateHeatMapQuartalInitialImages(Dictionary<string, string> colorsByName)
		{
			ClearHeatMapInitialImages();

			Dictionary<int, int> zoomSettings = new Dictionary<int, int>();
			for (int i = ConfigurationManager.KoConfig.MapTilesConfig.MCMinZoom, mult = 1; i <= ConfigurationManager.KoConfig.MapTilesConfig.MCMaxZoom; i++, mult *= 2)
			{
				zoomSettings.Add(i, mult);
			}

			var cancelTokenSource = new CancellationTokenSource();
			var options = new ParallelOptions
			{
				CancellationToken = cancelTokenSource.Token,
				MaxDegreeOfParallelism = 20
			};
			Parallel.ForEach(zoomSettings, options, zoomSetting =>
			{
				ConfigurationManager.KoConfig.MapTilesConfig.GetPixelCoordinatesFromJsonConfigFile(zoomSetting.Key);
				List<dynamic> coords = ConfigurationManager.KoConfig.MapTilesConfig.GetPixelCoordinatesFromJsonConfigFile(zoomSetting.Key);
				new ImageProccessor.MapDrawer().DrawMap(ConfigurationManager.KoConfig.MapTilesConfig.MCImgWidth * zoomSetting.Value,
					ConfigurationManager.KoConfig.MapTilesConfig.MCImgHeight * zoomSetting.Value,
					coords,
					GetHeatMapInitialImageFileName(zoomSetting.Key),
					zoomSetting.Key > 12,
					colorsByName ?? new Dictionary<string, string>());
			});
		}

		public Stream GetHeatMapTile(int x, int y, int z)
		{
			Stream result = null;
			var initialImage = GetHeatMapInitialImage(z);

			if (initialImage == null)
				return null;

			lock (LockObject)
			{
				result = new ImageProccessor.MapDrawer().ChopTileFromImage(
					initialImage,
					x,
					y,
					ConfigurationManager.KoConfig.MapTilesConfig.MCHorizontalStartTile,
					ConfigurationManager.KoConfig.MapTilesConfig.MCVerticalStartTile,
					ConfigurationManager.KoConfig.MapTilesConfig.MCMinZoom,
					z,
					ConfigurationManager.KoConfig.MapTilesConfig.MCTileSize);
			}

			return result;
		}
	}

}
