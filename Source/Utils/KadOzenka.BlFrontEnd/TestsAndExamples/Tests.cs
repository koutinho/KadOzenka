using KadOzenka.Dal.GbuObject;

namespace DebugApplication.TestsAndExamples
{
    public static class Tests
    {
		public static void TestGetAllAttributes()
		{
			var test = new GbuObjectService().GetAllAttributes(344506);
		}
    }
}
