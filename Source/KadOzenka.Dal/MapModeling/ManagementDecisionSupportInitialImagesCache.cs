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
	public static class ManagementDecisionSupportInitialImagesCache
	{
		private static readonly Dictionary<int, ConcurrentDictionary<int, InitialImageCacheInstance>> ManagementDecisionSupportMapInitialImageCache
			= new Dictionary<int, ConcurrentDictionary<int, InitialImageCacheInstance>>();
		private static readonly object LockObject = new object();
		private static readonly TimeSpan ClearImageCacheInterval = TimeSpan.FromMinutes(1);

		private static int CurrentUserId => SRDSession.GetCurrentUserId().GetValueOrDefault();

		static ManagementDecisionSupportInitialImagesCache()
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
					foreach (var userImagesKeyValue in ManagementDecisionSupportMapInitialImageCache)
					{
						var value = userImagesKeyValue.Value;
						for (var i = MapTilesConfig.Current.MCMinZoom; i <= MapTilesConfig.Current.MCMaxZoom; i++)
						{
							if (value.ContainsKey(i))
							{
								if (value.TryGetValue(i, out var imageInstance) && imageInstance.ExpirationTime < DateTime.Now)
								{
									imageInstance.Image.Dispose();
									ManagementDecisionSupportMapInitialImageCache[userImagesKeyValue.Key].TryRemove(i, out _);
								}
							}
						}
					}

				}
			}, cancelSource.Token);
		}

		public static void ClearManagementDecisionSupportHeatMapInitialImages()
		{
			lock (LockObject)
			{
				if (ManagementDecisionSupportMapInitialImageCache.ContainsKey(CurrentUserId))
				{
					var value = ManagementDecisionSupportMapInitialImageCache[CurrentUserId];
					for (var i = MapTilesConfig.Current.MCMinZoom; i <= MapTilesConfig.Current.MCMaxZoom; i++)
					{
						if (value.ContainsKey(i))
						{
							value[i].Image.Dispose();
							ManagementDecisionSupportMapInitialImageCache[CurrentUserId].TryRemove(i, out _);
							if (File.Exists(MapTilesConfig.GetManagementDecisionSupportHeatMapInitialImageFileName(i)))
							{
								File.Delete(MapTilesConfig.GetManagementDecisionSupportHeatMapInitialImageFileName(i));
							}
						}
					}
				}
			}
		}

		public static Image GetManagementDecisionSupportHeatMapInitialImage(int currentZoom)
		{
			if (!ManagementDecisionSupportMapInitialImageCache.ContainsKey(CurrentUserId)
				|| !ManagementDecisionSupportMapInitialImageCache[CurrentUserId]
					.ContainsKey(currentZoom))
			{
				lock (LockObject)
				{
					if (!ManagementDecisionSupportMapInitialImageCache.ContainsKey(CurrentUserId)
						|| !ManagementDecisionSupportMapInitialImageCache[CurrentUserId]
							.ContainsKey(currentZoom))
					{
						AddManagementDecisionSupportHeatMapInitialImages(currentZoom);
					}
				}
			}

			if (ManagementDecisionSupportMapInitialImageCache[CurrentUserId].TryGetValue(currentZoom, out var imageInstance))
			{
				imageInstance.ExpirationTime = DateTime.Now.AddMinutes(MapTilesConfig.Current.InitialImageCacheLifeTime);
				return imageInstance.Image;
			}

			return null;
		}

		private static void AddManagementDecisionSupportHeatMapInitialImages(int zoom)
		{
			if (!ManagementDecisionSupportMapInitialImageCache.ContainsKey(CurrentUserId))
			{
				ManagementDecisionSupportMapInitialImageCache.Add(CurrentUserId,
					new ConcurrentDictionary<int, InitialImageCacheInstance>());
			}

			if (File.Exists(MapTilesConfig.GetManagementDecisionSupportHeatMapInitialImageFileName(zoom)))
			{
				var image = Image.FromFile(MapTilesConfig.GetManagementDecisionSupportHeatMapInitialImageFileName(zoom));
				var imageInstance = new InitialImageCacheInstance
				{
					Image = image,
					ExpirationTime = DateTime.Now.AddMinutes(MapTilesConfig.Current.InitialImageCacheLifeTime)
				};
				ManagementDecisionSupportMapInitialImageCache[CurrentUserId].AddOrUpdate(zoom, imageInstance, (key, oldValue) => oldValue);
			}
		}
	}
}
