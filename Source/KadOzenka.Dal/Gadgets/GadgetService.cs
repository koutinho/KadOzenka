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
				.GroupBy(x => x.Typeobj)
				.ExecuteSelect(x => new
				{
					x.Typeobj,
					Count = QSExtensions.Count<OMObject>(y => 1)
				});

			var data = new DataTable();

			data.Columns.AddRange(new[] { new DataColumn("LinkParam"), new DataColumn("Name"), new DataColumn("Value") });

			data.Rows.Add(
				linkParam.Replace("{Type}", SudObjectType.Site.GetEnumCode()),
				"Участок",
				objects.FirstOrDefault(x => x.Typeobj == long.Parse(SudObjectType.Site.GetEnumCode()))?.Count ?? 0);

			data.Rows.Add(
				linkParam.Replace("{Type}", SudObjectType.Building.GetEnumCode()),
				"Здание",
				objects.FirstOrDefault(x => x.Typeobj == long.Parse(SudObjectType.Building.GetEnumCode()))?.Count ?? 0);

			data.Rows.Add(
				linkParam.Replace("{Type}", SudObjectType.Room.GetEnumCode()),
				"Помещение",
				objects.FirstOrDefault(x => x.Typeobj == long.Parse(SudObjectType.Room.GetEnumCode()))?.Count ?? 0);

			data.Rows.Add(
				linkParam.Replace("{Type}", SudObjectType.Construction.GetEnumCode()),
				"Сооружение",
				objects.FirstOrDefault(x => x.Typeobj == long.Parse(SudObjectType.Construction.GetEnumCode()))?.Count ?? 0);

			data.Rows.Add(
				linkParam.Replace("{Type}", SudObjectType.Ons.GetEnumCode()),
				"Онс",
				objects.FirstOrDefault(x => x.Typeobj == long.Parse(SudObjectType.Ons.GetEnumCode()))?.Count ?? 0);

			data.Rows.Add(
				linkParam.Replace("{Type}", SudObjectType.ParkingPlace.GetEnumCode()),
				"Онс",
				objects.FirstOrDefault(x => x.Typeobj == long.Parse(SudObjectType.ParkingPlace.GetEnumCode()))?.Count ?? 0);

			return data;
		}

		/// <summary>
		/// 2. Суды
		/// </summary>
		/// <returns></returns>
		public static DataTable СourtsWidget()
		{
			var data = new DataTable();

			var otchetCount = OMOtchet.Where(GetQuery("SudOtchet")).ExecuteCount();
			data.Columns.AddRange(new[] { new DataColumn("LinkParam"), new DataColumn("Name"), new DataColumn("Value") });
			data.Rows.Add("SudOtchet", "Отчеты", otchetCount);

			var reshCount = OMOtchet.Where(GetQuery("SudResh")).ExecuteCount();
			data.Columns.AddRange(new[] { new DataColumn("LinkParam"), new DataColumn("Name"), new DataColumn("Value") });
			data.Rows.Add("SudResh", "Решения", reshCount);

			var zakCount = OMOtchet.Where(GetQuery("SudZak")).ExecuteCount();
			data.Columns.AddRange(new[] { new DataColumn("LinkParam"), new DataColumn("Name"), new DataColumn("Value") });
			data.Rows.Add("SudResh", "Заключения", zakCount);

			return data;
		}
	}
}