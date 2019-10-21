using Core.Register;
using Core.Register.QuerySubsystem;
using Core.Shared.Attributes;
using Core.Shared.Extensions;
using Core.SRD;
using Core.UI.Registers.Controllers;
using Core.UI.Registers.CoreUI.Registers;
using Core.UI.Registers.Models.CoreUi;
using Core.UI.Registers.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ObjectModel.Core.Register;
using ObjectModel.Core.SRD;
using ObjectModel.Market;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;

namespace CIPJS.Controllers
{
	public class TestModel
	{
		public int? Unom { get; set; }
		public string Address { get; set; }
		public int? YearBuild { get; set; }
		public int? CountFloor { get; set; }
	}

	public class DashboardDtoTest
	{
		public long Id { get; set; }

		public long? UserId { get; set; }

		public long? CopyId { get; set; }

		[Display(Name = "Количество панелей")]
		public int LayoutType { get; set; }

		[Display(Name = "Наименование")]
		public string Name { get; set; }

		[Display(Name = "Описание")]
		public string Description { get; set; }

		[Display(Name = "Доступен всем")]
		public bool IsCommon { get; set; }

		public string UniqueSessionKey { get; set; }

		public List<List<DashboardPanelDtoTest>> DashboardPanelList { get; set; }
	}

	public class DashboardPanelDtoTest
	{
		public long Id { get; set; }

		public long DashboardId { get; set; }

		public long PanelTypeId { get; set; }

		[Display(Name = "Заголовок")]
		public string Title { get; set; }

		[Display(Name = "Индекс колонки")]
		public long ColumnIndex { get; set; }

		[Display(Name = "Порядок внутри колонки")]
		public long OrderInColumn { get; set; }

		[Display(Name = "Сериализованный XML")]
		public string Settings { get; set; }

		public DashboardPanelTypeDtoTest Panel { get; set; }
	}

	public class DashboardPanelTypeDtoTest
	{
		public long Id { get; set; }

		[Display(Name = "Наименование гаджета")]
		[Required(ErrorMessage = "Поле 'Наименование гаджета' обязательно для заполнения")]
		public string Name { get; set; }

		[Display(Name = "Описание")]
		[Required(ErrorMessage = "Поле 'Описание' обязательно для заполнения")]
		public string Description { get; set; }

		[Display(Name = "Ссылка на гаджет")]
		[Required(ErrorMessage = "Поле 'Ссылка на гаджет' обязательно для заполнения")]
		public string Url { get; set; }

		public object Obj { get; set; }

		[Display(Name = "Наименование класса Dto")]
		[Required(ErrorMessage = "Поле 'Наименование класса Dto' обязательно для заполнения")]
		public string DtoClassFullName { get; set; }
	}

	public enum DashboardLayoutTypeEnumTest
	{
		/// <summary>
		/// Одна панель
		/// </summary>
		[Description("singlePanel")]
		SinglePanel = 100,

		/// <summary>
		/// Одна панель
		/// </summary>
		[Description("twoMiddlePanel")]
		TwoMiddlePanel = 200,

		/// <summary>
		/// Одна панель
		/// </summary>
		[Description("leftMiddlePanel")]
		LeftMiddlePanel = 230,

		/// <summary>
		/// Одна панель
		/// </summary>
		[Description("middleRightPanel")]
		MiddleRightPanel = 260,

		/// <summary>
		/// Одна панель
		/// </summary>
		[Description("threePanel")]
		ThreePanel = 300,
	}

	public class TestController : BaseController
	{
        private readonly CoreUiService _service;

        public TestController(CoreUiService service)
        {
            _service = service;
        }

		[HttpGet]
		public IActionResult ObjectCard(long id)
		{
			var analogItem = OMCoreObject
				.Where()
				.SetPackageSize(10)
				//.SelectAll()
				.ExecuteFirstOrDefault();

			return Json(analogItem);
		}

		[HttpGet]
		public IActionResult UserCard(long id)
		{
			var test = OMUser
				.Where(x => x.Id == id)
				.SelectAll()
				.ExecuteFirstOrDefault();

			return Json(test);
		}

		// GET: /<controller>/
		public IActionResult Index()
        {
			var dto = new DashboardDtoTest
			{
				Id = -1,
				UserId = 233,
				DashboardPanelList = new List<List<DashboardPanelDtoTest>>(),
				LayoutType = (int)DashboardLayoutTypeEnumTest.SinglePanel
			};

			return View(dto);
        }

		[HttpPost]
		public JsonResult SaveDataTest(DashboardDtoTest data)
		{
			long id = 555;

			ModelState.Clear();

			return Json(id);
		}
    }
}
