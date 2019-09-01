
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace CIPJS.DAL
{
    public class CipjsDbManager : DBMngr
	{
		protected static Database DbDgi;

		public static Database Dgi
		{
			get
			{
				if (DbDgi == null)
				{
					DbDgi = MakeInstace("Dgi");
				}
				return DbDgi;
			}
		}
	}
}
