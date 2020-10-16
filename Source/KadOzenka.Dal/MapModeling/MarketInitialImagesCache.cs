using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Core.SRD;

namespace KadOzenka.Dal.MapModeling
{
	public static class MarketInitialImagesCache
	{
		private static readonly Dictionary<int, ConcurrentDictionary<int, InitialImageCacheInstance>> MarketMapInitialImageCache
			= new Dictionary<int, ConcurrentDictionary<int, InitialImageCacheInstance>>();
		private static readonly object LockObject = new object();
		private static readonly TimeSpan ClearImageCacheInterval = TimeSpan.FromMinutes(1);

		private static int CurrentUserId => SRDSession.GetCurrentUserId().GetValueOrDefault();

		static MarketInitialImagesCache()
		{
			StartWatch();
		}

		private static void StartWatch()
		{
			var cancelSource = new CancellationTokenSource();
			Task.Run(() =>
			{
				while (true)
				{
					Thread.Sleep(ClearImageCacheInterval);
					foreach (var userImagesKeyValue in MarketMapInitialImageCache)
					{
						var value = userImagesKeyValue.Value;
						for (var i = MapTilesConfig.Current.MCMinZoom; i <= MapTilesConfig.Current.MCMaxZoom; i++)
						{
							if (value.ContainsKey(i))
							{
								if (value.TryGetValue(i, out var imageInstance) && imageInstance.ExpirationTime < DateTime.Now)
								{
									imageInstance.Image.Dispose();
									MarketMapInitialImageCache[userImagesKeyValue.Key].TryRemove(i, out _);
								}
							}
						}
					}

				}
			}, cancelSource.Token);
		}

		public static void ClearMarketHeatMapInitialImages()
		{
			lock (LockObject)
			{
				if (MarketMapInitialImageCache.ContainsKey(CurrentUserId))
				{
					var value = MarketMapInitialImageCache[CurrentUserId];
					for (var i = MapTilesConfig.Current.MCMinZoom; i <= MapTilesConfig.Current.MCMaxZoom; i++)
					{
						if (value.ContainsKey(i))
						{
							value[i].Image.Dispose();
							MarketMapInitialImageCache[CurrentUserId].TryRemove(i, out _);
							if (File.Exists(MapTilesConfig.GetMarketHeatMapInitialImageFileName(i)))
							{
								File.Delete(MapTilesConfig.GetMarketHeatMapInitialImageFileName(i));
							}
						}
					}
				}
			}
		}

		public static Image GetMarketHeatMapInitialImage(int currentZoom)
		{
			if (!MarketMapInitialImageCache.ContainsKey(CurrentUserId)
				|| !MarketMapInitialImageCache[CurrentUserId]
					.ContainsKey(currentZoom))
			{
				lock (LockObject)
				{
					if (!MarketMapInitialImageCache.ContainsKey(CurrentUserId)
						|| !MarketMapInitialImageCache[CurrentUserId]
							.ContainsKey(currentZoom))
					{
						AddMarketHeatMapInitialImages(currentZoom);
					}
				}
			}

			if (MarketMapInitialImageCache[CurrentUserId].TryGetValue(currentZoom, out var imageInstance))
			{
				imageInstance.ExpirationTime = DateTime.Now.AddMinutes(MapTilesConfig.Current.InitialImageCacheLifeTime);
				return imageInstance.Image;
			}

			return null;
		}

		private static void AddMarketHeatMapInitialImages(int zoom)
		{
			if (!MarketMapInitialImageCache.ContainsKey(CurrentUserId))
			{
				MarketMapInitialImageCache.Add(CurrentUserId,
					new ConcurrentDictionary<int, InitialImageCacheInstance>());
			}

			if (File.Exists(MapTilesConfig.GetMarketHeatMapInitialImageFileName(zoom)))
			{
				var image = Image.FromFile(MapTilesConfig.GetMarketHeatMapInitialImageFileName(zoom));
				var imageInstance = new InitialImageCacheInstance
				{
					Image = image,
					ExpirationTime = DateTime.Now.AddMinutes(MapTilesConfig.Current.InitialImageCacheLifeTime)
				};
				MarketMapInitialImageCache[CurrentUserId].AddOrUpdate(zoom, imageInstance, (key, oldValue) => oldValue);
			}
		}
	}
}
