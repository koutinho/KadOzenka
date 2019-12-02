using Core.Register;
using Core.Shared.Extensions;
using Core.SRD;
using ObjectModel.Sud;
using Platform.RefLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web.Mvc;

namespace KadOzenka.BlFrontEnd.SudTests
{
	public class ParamToApply
	{
		public string Name { get; set; }
		public long Id { get; set; }
	}

	public static class SudTestApi
	{
		public static void TestAll()
		{
			var table = OMTableParam.Object;
			var paramName = "kc";
			var objectId = 14674;


			List<SelectListItem> items = new List<SelectListItem>();
			bool comboBoxDisabled = false;

			var param = OMParam.GetActual(table, objectId, paramName);

			if (param != null)
			{
				// Блокируем комбобокс и выводим в нем одну строку
				comboBoxDisabled = true;
				items.Add(new SelectListItem
				{
					Value = $"{param.Pid}",
					Text = $"({(SRDCache.Users.ContainsKey((int)param.IdUser) ? SRDCache.Users[(int)param.IdUser].FullName : String.Empty)}, {param.DateUser.ToString("dd.MM.yyyy")}) {param}"
				});
			}
			else
			{
				var paramValues = OMParam.GetParams(table, objectId, paramName);

				items = paramValues.Select(x => new SelectListItem
				{
					Value = $"{x.Pid}",
					Text = $"({(SRDCache.Users.ContainsKey((int)x.IdUser) ? SRDCache.Users[(int)x.IdUser].FullName : String.Empty)}, {x.DateUser.ToString("dd.MM.yyyy")}) {x}"
				}).ToList();
			}

			Console.ReadLine();


			// Согласование - на входе массив поле - параметр
			List<ParamToApply> paramsToApply = new List<ParamToApply>
			{
				new ParamToApply
				{
					Name = "kn",
					Id = 1
				},
				new ParamToApply
				{
					Name = "kc",
					Id = 1
				},
				// И другие поля
			};

			OMObject sudObject = OMObject.Where(x => x.Id == objectId).ExecuteFirstOrDefault();



			OMParam pKn = OMParam.Where(x => x.Id == paramsToApply.FirstOrDefault(y => y.Name == GetObjectFieldName(z => z.Kn)).Id).ExecuteFirstOrDefault();
			OMParam pType = OMParam.Where(x => x.Id == paramsToApply.FirstOrDefault(y => y.Name == ParamName.TypeObj.GetEnumDescription()).Id).ExecuteFirstOrDefault();
			OMParam pSquare = null;
			OMParam pKc = null;
			OMParam pDate = null;
			OMParam pNameCenter = null;
			OMParam pStatDgi = null;
			OMParam pAdres = null;
			OMParam pOwner = null;

			sudObject.UpdateAndCheckParam(pKn, pType, pSquare, pKc, pDate, pNameCenter, pStatDgi, pAdres, pOwner);
		}

		public static string GetObjectFieldName<TResult>(Expression<Func<OMObject, TResult>> expr)
		{
			return RegisterCache.GetAttributeData(OMObject.GetColumnAttributeId(expr)).ValueField.ToLower();
		}
    }

	public enum ParamName
	{
		[Description("kn")]
		Kn,

		[Description("typeobj")]
		TypeObj,
	}
}
