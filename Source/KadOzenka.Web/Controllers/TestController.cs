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

        public ActionResult FilterQueryEditor(string registerViewId, long? queryId)
        {
            string filter = string.Empty;
            bool isReadOnly = false;

            RegistryFilterDto registryFilter = null;
            if (queryId.HasValue)
            {
                registryFilter = _service.GetRegistryFilter(queryId.Value);
                isReadOnly = registryFilter.UserId != SRDSession.GetCurrentUserId();
            }

            if (queryId.HasValue)
            {
                var query = OMQry.Where(l => l.QryId == queryId).SelectAll().Execute().FirstOrDefault();
                if (query != null)
                {
                    var qsQueryString = query.QSCondition;
                    if (!string.IsNullOrEmpty(qsQueryString))
                    {
                        var condition = qsQueryString.DeserializeFromXml<QSCondition>();
                        filter = DeserializeFiltr((QSConditionGroup)condition);
                    }
                }
            }

            var model = new ViewFilterDto
            {
                RegisterViewId = registerViewId,
                QueryId = queryId,
                Filter = filter,
                IsReadOnly = isReadOnly
            };

            if (registryFilter != null && registryFilter.IsUsingExtendedEditor.HasValue && registryFilter.IsUsingExtendedEditor.Value)
                return View("~/Views/CoreUi_/Registers/FilterQueryEditorEx.cshtml", model);
            else
                return View("~/Views/CoreUi_/Registers/FilterQueryEditor.cshtml", model);
        }

        private string DeserializeFiltr(QSConditionGroup conditionGroup)
        {
            string resultQuery = string.Empty;

            if (conditionGroup != null)
            {
                resultQuery += "{\"group\":[{\"operator\":\"" + conditionGroup.Type.ToString() + "\"";

                if (conditionGroup.Conditions.Any())
                {
                    List<string> conditions = new List<string>();
                    List<string> groups = new List<string>();

                    for (int i = 0; i < conditionGroup.Conditions.Count; i++)
                    {
                        var conditionItem = conditionGroup.Conditions[i];
                        var conditionString = string.Empty;

                        if (conditionItem is QSConditionSimple)
                        {
                            conditionString = "{";
                            var condition = (QSConditionSimple)conditionItem;
                            var leftOperand = condition.LeftOperand as QSColumnSimple;
                            var rightOperand = condition.RightOperand as QSColumnConstant;
                            var comparison = condition.ConditionType.ToString();

                            if (leftOperand != null)
                            {
                                conditionString += $"\"attribute\":\"{leftOperand.AttributeID}\"";
                            }

                            conditionString += $",\"comparison\":\"{comparison}\"";

                            if (rightOperand != null)
                            {
                                var conditionValue = rightOperand.Value.GetType().Name == "String" ?
                                                     EncodeJsonString((string)rightOperand.Value) :
                                                     rightOperand.Value;

                                conditionString += $",\"value\":\"{conditionValue}\"";
                            }

                            conditionString += "}";

                            conditions.Add(conditionString);
                        }
                        else if (conditionItem is QSConditionGroup)
                        {
                            conditionString = DeserializeFiltr((QSConditionGroup)conditionItem);
                            groups.Add(conditionString);
                        }
                    }

                    if (conditions != null && conditions.Any())
                    {
                        resultQuery += ",\"condition\":[";
                        for (int i = 0; i < conditions.Count; i++)
                        {
                            if (i > 0)
                                resultQuery += ",";

                            resultQuery += conditions[i];
                        }
                        resultQuery += "]";
                    }

                    if (groups != null && groups.Any())
                    {
                        resultQuery += ",\"group\":[";
                        for (int i = 0; i < groups.Count; i++)
                        {
                            if (i > 0)
                                resultQuery += ",";

                            resultQuery += groups[i];
                        }
                        resultQuery += "]";
                    }
                }

                resultQuery += "}]}";
            }

            return resultQuery;
        }

        /// <summary>
        /// Экранирование строки для JSON.
        /// </summary>
        /// <param name="s">Входная строка</param>
        /// <returns>Экранированная строка</returns>
        private string EncodeJsonString(string s)
        {
            StringBuilder sb = new StringBuilder();
            foreach (char c in s)
            {
                switch (c)
                {
                    case '\"':
                        sb.Append("\\\\\"");
                        break;
                    case '\\':
                        sb.Append("\\\\\\\\");
                        break;
                    default:
                        sb.Append(c);
                        break;
                }
            }

            return sb.ToString();
        }
    }
}
