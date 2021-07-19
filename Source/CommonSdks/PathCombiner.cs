using System.IO;

namespace CommonSdks
{
	public static class PathCombiner
	{
		public static string GetFullPath(params string[] paths)
		{
			return Path.Combine(paths);
		}
	}
}
