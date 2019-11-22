using System.Collections.Generic;
using System.Data;
using System.Linq;
using Core.Register;
using Core.Register.DAL;
using Core.Register.QuerySubsystem;
using Core.Shared.Extensions;
using Core.Shared.Misc;
using Core.UI.Registers.CoreUI.Registers;
using ObjectModel.Directory;
using ObjectModel.Directory.Sud;
using ObjectModel.Sud;

namespace KadOzenka.Dal.Gadgets
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
		public static DataTable ObjectsWidget()
		{
			string linkParam = "Transition=1&31501000={Type}";

			var objects = OMObject.Where(GetQuery("SudObjects"))
				.GroupBy(x => x.Typeobj_Code)
				.ExecuteSelect(x => new
				{
					x.Typeobj_Code,
					Count = QSExtensions.Count<OMObject>(y => 1)
				});

			var data = new DataTable();

			data.Columns.AddRange(new[] { new DataColumn("LinkParam"), new DataColumn("Name"), new DataColumn("Value") });

			data.Rows.Add(
				linkParam.Replace("{Type}", SudObjectType.Site.GetEnumCode()),
				"Участки",
				objects.FirstOrDefault(x => x.Typeobj_Code == SudObjectType.Site)?.Count ?? 0);

			data.Rows.Add(
				linkParam.Replace("{Type}", SudObjectType.Building.GetEnumCode()),
				"Здания",
				objects.FirstOrDefault(x => x.Typeobj_Code == SudObjectType.Building)?.Count ?? 0);

			data.Rows.Add(
				linkParam.Replace("{Type}", SudObjectType.Room.GetEnumCode()),
				"Помещения",
				objects.FirstOrDefault(x => x.Typeobj_Code == SudObjectType.Room)?.Count ?? 0);

			data.Rows.Add(
				linkParam.Replace("{Type}", SudObjectType.Construction.GetEnumCode()),
				"Сооружения",
				objects.FirstOrDefault(x => x.Typeobj_Code == SudObjectType.Construction)?.Count ?? 0);

			data.Rows.Add(
				linkParam.Replace("{Type}", SudObjectType.Ons.GetEnumCode()),
				"Онс",
				objects.FirstOrDefault(x => x.Typeobj_Code == SudObjectType.Ons)?.Count ?? 0);

			data.Rows.Add(
				linkParam.Replace("{Type}", SudObjectType.ParkingPlace.GetEnumCode()),
				"Машиноместа",
				objects.FirstOrDefault(x => x.Typeobj_Code == SudObjectType.ParkingPlace)?.Count ?? 0);

			return data;
		}

		/// <summary>
		/// 2. Суды
		/// </summary>
		/// <returns></returns>
		public static DataTable СourtsWidget()
		{
			var data = new DataTable();
			data.Columns.AddRange(new[] { new DataColumn("LinkParam"), new DataColumn("Name"), new DataColumn("Value") });

			var otchetCount = OMOtchet.Where(GetQuery("SudOtchet")).ExecuteCount();
			data.Rows.Add("SudOtchet", "Отчеты", otchetCount);

			var reshCount = OMOtchet.Where(GetQuery("SudResh")).ExecuteCount();
			data.Rows.Add("SudResh", "Решения", reshCount);

			var zakCount = OMOtchet.Where(GetQuery("SudZak")).ExecuteCount();
			data.Rows.Add("SudZak", "Заключения", zakCount);

			return data;
		}
	}
}