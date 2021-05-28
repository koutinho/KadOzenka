using System.IO;

namespace KadOzenka.Dal.Helpers
{
	public static class PathCombiner
	{
		public static string GetFullPath(params string[] paths)
		{
			return Path.Combine(paths);
		}
	}
}
