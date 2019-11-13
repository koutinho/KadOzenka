using Core.Register;
using Core.Register.DAL;
using Core.Register.QuerySubsystem;
using Core.Shared.Extensions;
using Core.Shared.Misc;
using Core.SRD;
using Core.UI.Registers.CoreUI.Registers;
using Microsoft.Practices.EnterpriseLibrary.Data;
using ObjectModel.Cld;
using ObjectModel.Directory;
using ObjectModel.Ehd;
using ObjectModel.Finance.Flses;
using ObjectModel.Finance.Ufk;
using ObjectModel.Sud;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;

namespace RsmCloudService.Dal.Gadgets
{
    public class GadgetService
    {	
		private static QSQuery GetQuery(string registerViewId)
		{
            var registerView = RegisterCommonConfiguration.GetRegisterViewConfiguration(registerViewId);
			
			var layout = LayoutEditorDAL.GetLayoutWithDetails(registerView.DefaultLayoutId);

            QSQuery query = RegisterCommonConfiguration.GetLayoutQuery(HttpContextHelper.HttpContext, layout, registerView).GetCountQuery();

			query.Columns = new List<QSColumn>
			{
				new QSColumnFunction
				{
					FunctionType = QSColumnFunctionType.CountDistinct,
					Operands = new List<QSColumn>
					{
						new QSColumnSimple(RegisterCache.GetPKAttributeID(query.MainRegisterID))
					}
				}
			};

			return query;
		}

		/// <summary>
		/// 1. Объекты
		/// </summary>
		/// <returns></returns>
		public static DataTable RequestsWidget()
		{
			string linkParam = "Transition=1&31501000={Type}";
			
			var objects = OMObject.Where(GetQuery("SudObjects"))
				.GroupBy(x => x.Typeobj)
				.ExecuteSelect(x => new {
					x.Typeobj,
					Count = QSExtensions.Count<OMObject>(y => 1)
				});
			
			var data = new DataTable();

			data.Columns.AddRange(new[] { new DataColumn("LinkParam"), new DataColumn("Name"), new DataColumn("Value") });

			data.Rows.Add(
				linkParam.Replace("{Type}", ((long)SudObjectType.Building).ToString()),
				"Здание",
				objects.FirstOrDefault(x => x.Typeobj == (long)SudObjectType.Building)?.Count ?? 0);
			
			return data;
		}

		/// <summary>
		/// 2. Суды
		/// </summary>
		/// <returns></returns>
		public static DataTable EhdRightWidget()
        {
			var otchetCount = OMOtchet.Where(GetQuery("SudOtchet")).ExecuteCount();
			
			var data = new DataTable();
			data.Columns.AddRange(new [] { new DataColumn("LinkParam"), new DataColumn("Name"), new DataColumn("Value") });
            data.Rows.Add("SudOtchet", "Отчеты", otchetCount);
            
			return data;
        }
	}
}