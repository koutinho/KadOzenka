﻿using System.Collections.Generic;
using System.Data;
using System.Linq;
using Core.Register;
using Core.Register.DAL;
using Core.Register.QuerySubsystem;
using Core.Shared.Extensions;
using Core.Shared.Misc;
using Core.SRD;
using Core.UI.Registers.CoreUI.Registers;
using ObjectModel.Declarations;
using ObjectModel.Directory.Declarations;
using ObjectModel.Directory.Sud;
using ObjectModel.Market;
using ObjectModel.Sud;

namespace KadOzenka.Dal.Gadgets
{
	public class GadgetService
	{
        private static string _moveToPageSymbol = "→";

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

			if (SRDSession.Current.CheckAccessToFunction(ObjectModel.SRD.SRDCoreFunctions.SUD_OBJECTS))
			{
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
			}
			
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

			if (SRDSession.Current.CheckAccessToFunction(ObjectModel.SRD.SRDCoreFunctions.SUD_OTCHET))
			{
				var otchetCount = OMOtchet.Where(GetQuery("SudOtchet")).ExecuteCount();
				data.Rows.Add("SudOtchet", "Отчеты", otchetCount);
			}

			if (SRDSession.Current.CheckAccessToFunction(ObjectModel.SRD.SRDCoreFunctions.SUD_ZAK))
			{
				var zakCount = OMOtchet.Where(GetQuery("SudZak")).ExecuteCount();
				data.Rows.Add("SudZak", "Заключения", zakCount);

			}

			if (SRDSession.Current.CheckAccessToFunction(ObjectModel.SRD.SRDCoreFunctions.SUD_RESH))
			{
				var reshCount = OMOtchet.Where(GetQuery("SudResh")).ExecuteCount();
				data.Rows.Add("SudResh", "Решения", reshCount);
			}
			
			return data;
		}

		/// <summary>
		/// Декларации по типам объектов
		/// </summary>
		/// <returns></returns>
		public static DataTable DeclarationsObjectTypesWidget()
		{
			string linkParam = "Transition=1&50101200={Type}";

			var objects = OMDeclaration.Where(GetQuery("DeclarationsDeclaration"))
				.GroupBy(x => x.TypeObj_Code)
				.ExecuteSelect(x => new
				{
					x.TypeObj_Code,
					Count = QSExtensions.Count<OMDeclaration>(y => 1)
				});

			var data = new DataTable();
			data.Columns.AddRange(new[] { new DataColumn("LinkParam"), new DataColumn("Name"), new DataColumn("Value") });

			if (SRDSession.Current.CheckAccessToFunction(ObjectModel.SRD.SRDCoreFunctions.DECLARATIONS))
			{
				data.Rows.Add(
					linkParam.Replace("{Type}", ObjectType.Site.GetEnumCode()),
					"Земельные участки",
					objects.FirstOrDefault(x => x.TypeObj_Code == ObjectType.Site)?.Count ?? 0);

				data.Rows.Add(
					linkParam.Replace("{Type}", ObjectType.Building.GetEnumCode()),
					"Здания",
					objects.FirstOrDefault(x => x.TypeObj_Code == ObjectType.Building)?.Count ?? 0);

				data.Rows.Add(
					linkParam.Replace("{Type}", ObjectType.Room.GetEnumCode()),
					"Помещения",
					objects.FirstOrDefault(x => x.TypeObj_Code == ObjectType.Room)?.Count ?? 0);

				data.Rows.Add(
					linkParam.Replace("{Type}", ObjectType.Construction.GetEnumCode()),
					"Сооружения",
					objects.FirstOrDefault(x => x.TypeObj_Code == ObjectType.Construction)?.Count ?? 0);

				data.Rows.Add(
					linkParam.Replace("{Type}", ObjectType.ParkingPlace.GetEnumCode()),
					"Машино-места",
					objects.FirstOrDefault(x => x.TypeObj_Code == ObjectType.ParkingPlace)?.Count ?? 0);

				data.Rows.Add(
					linkParam.Replace("{Type}", ObjectType.Ons.GetEnumCode()),
					"ОНС",
					objects.FirstOrDefault(x => x.TypeObj_Code == ObjectType.Ons)?.Count ?? 0);

				data.Rows.Add(
					linkParam.Replace("{Type}", ObjectType.Ens.GetEnumCode()),
					"Единые недвижимые комплексы",
					objects.FirstOrDefault(x => x.TypeObj_Code == ObjectType.Ens)?.Count ?? 0);

				data.Rows.Add(
					linkParam.Replace("{Type}", ObjectType.Pik.GetEnumCode()),
					"Производственно имущественные комплексы",
					objects.FirstOrDefault(x => x.TypeObj_Code == ObjectType.Pik)?.Count ?? 0);

				data.Rows.Add(
					linkParam.Replace("{Type}", ObjectType.Other.GetEnumCode()),
					"Иное",
					objects.FirstOrDefault(x => x.TypeObj_Code == ObjectType.Other)?.Count ?? 0);
			}

			return data;
		}

        /// <summary>
        /// Объекты недвижимости (карточка основного рабочего стола)
        /// </summary>
        /// <returns></returns>
        public static DataTable GbuObjects()
        {
            var data = new DataTable();
            data.Columns.AddRange(new[] { new DataColumn("LinkParam"), new DataColumn("Name"), new DataColumn("Value") });

            var gbuObjectsCount = OMCoreObject.Where(GetQuery("GbuObjects")).ExecuteCount();
            var codCount = OMCoreObject.Where(GetQuery("GbuCodJob")).ExecuteCount();

            data.Rows.Add("/RegistersView/GbuObjects", "Объекты недвижимости", gbuObjectsCount);
            data.Rows.Add("/GbuObject/Harmonization", "Выполнить гармонизацию", _moveToPageSymbol);
            data.Rows.Add("/GbuObject/HarmonizationCOD", "Выполнить гармонизацию по классификатору ЦОД", _moveToPageSymbol);
            data.Rows.Add("~/GbuObject/GroupingObject", "Выполнить нормализацию", _moveToPageSymbol);
            data.Rows.Add("~/GbuObject/Inheritance", "Выполнить наследование", _moveToPageSymbol);
            data.Rows.Add("/RegistersView/GbuCodJob", "Справочники ЦОД", codCount);

            return data;
        }

        /// <summary>
        /// Объекты аналоги (карточка основного рабочего стола)
        /// </summary>
        /// <returns></returns>
        public static DataTable MarketObjects()
        {
            var data = new DataTable();
            data.Columns.AddRange(new[] { new DataColumn("LinkParam"), new DataColumn("Name"), new DataColumn("Value") });

            var marketObjectsCount = OMCoreObject.Where(GetQuery("MarketObjects")).ExecuteCount();

            data.Rows.Add("/RegistersView/MarketObjects", "Реестр объектов аналогов", marketObjectsCount);
            data.Rows.Add("/Map", "На карту", _moveToPageSymbol);

            return data;
        }
    }
}