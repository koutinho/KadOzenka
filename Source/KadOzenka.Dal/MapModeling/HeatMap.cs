using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Image = System.Drawing.Image;

namespace KadOzenka.Dal.MapModeling
{

	public abstract class HeatMap
	{
		private static readonly object LockObject = new object();

		protected abstract void ClearHeatMapInitialImages();
		protected abstract string GetHeatMapInitialImageFileName(int zoom);
		protected abstract Image GetHeatMapInitialImage(int zoom);

		public int MapMinZoom => MapTilesConfig.Current.MCMinZoom;
		public int MapMaxZoom => MapTilesConfig.Current.MCMaxZoom;

		public void GenerateHeatMapQuartalInitialImages(Dictionary<string, string> colorsByName)
		{
			ClearHeatMapInitialImages();

			Dictionary<int, int> zoomSettings = new Dictionary<int, int>();
			for (int i = MapTilesConfig.Current.MCMinZoom, mult = 1; i <= MapTilesConfig.Current.MCMaxZoom; i++, mult *= 2)
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
				List<dynamic> coords = MapTilesConfig.GetPixelCoordinatesFromJsonConfigFile(zoomSetting.Key);
				new ImageProccessor.MapDrawer().DrawMap(MapTilesConfig.Current.MCImgWidth * zoomSetting.Value,
					MapTilesConfig.Current.MCImgHeight * zoomSetting.Value,
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
					MapTilesConfig.Current.MCHorizontalStartTile,
					MapTilesConfig.Current.MCVerticalStartTile,
					MapTilesConfig.Current.MCMinZoom,
					z,
					MapTilesConfig.Current.MCTileSize);
			}

			return result;
		}
	}

}
