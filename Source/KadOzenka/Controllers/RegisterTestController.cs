using CIPJS.Models.RegisterTest;
using Core.Register;
using Core.Register.QuerySubsystem;
using Core.Shared.Extensions;
using Core.UI.Registers.Controllers;
using Core.UI.Registers.CoreUI.Registers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using ObjectModel.Core.Register;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace CIPJS.Controllers
{
    public class RegisterTestController : BaseController
    {
        public IActionResult RegisterViewModel(int registerId, long objectId)
        {
            List<OMAttribute> attributes = OMAttribute.Where(x => x.RegisterId == registerId && (x.ValueField != null || x.CodeField != null)).SelectAll().OrderBy(x => x.Id).Execute().ToList();
            List<RegisterAttributeField> list = new List<RegisterAttributeField>();
			
			DataTable dataRecord = null;

			if(objectId > 0 )
			{
				QSQuery query = new QSQuery
				{
					MainRegisterID = registerId,
					Columns = new List<QSColumn>(),
					Condition = new QSConditionSimple
					{
						ConditionType = QSConditionType.Equal,
						LeftOperand = new QSColumnSimple(RegisterCache.GetRegisterData(registerId).PKAttributeID),
						RightOperand = new QSColumnConstant(objectId)
					}
				};

				foreach(var attribute in attributes)
				{
					query.AddColumn((int)attribute.Id);
				}
				
				dataRecord = query.ExecuteQuery();

				if(dataRecord.Rows.Count == 0)
				{
					throw new Exception("Не найден объект с идентификатором" + objectId);
				}
			}
			
            foreach (var attribute in attributes)
            {
                RegisterAttributeField registerAttributeField = new RegisterAttributeField
                {
                    AttributeId = attribute.Id,
                    IsPrimaryKey = attribute.IsPrimaryKey == true,
                    Title = attribute.Name,
                    ReferenceId = attribute.ReferenceId,
                    Value = objectId > 0 ? dataRecord.Rows[0][attribute.Id.ToString()] : null
                };
                switch (attribute.Type)
                {
                    case 1:
                        OMRelation relation = OMRelation.Where(x => x.ChildRegisterId == registerId && x.AttributeId == attribute.Id).SelectAll().Execute().FirstOrDefault();
                        if (relation == null)
                        {
                            registerAttributeField.Type = RegisterAttributeType.INTEGER;
                        }
                        else
                        {
                            registerAttributeField.Type = RegisterAttributeType.LINK;
                            registerAttributeField.ParentRegisterId = relation.ParentRegisterId;
                        }
                        break;
                    case 2:
                        registerAttributeField.Type = RegisterAttributeType.DECIMAL;
                        break;
                    case 3:
                        registerAttributeField.Type = RegisterAttributeType.BOOLEAN;
                        break;
                    case 4:
                        if (attribute.CodeField.IsNotEmpty())
                        {
                            registerAttributeField.Type = RegisterAttributeType.REFERENCE;
                        }
                        else
                        {
                            registerAttributeField.Type = RegisterAttributeType.STRING;
                        }
                        break;
                    case 5:
                        registerAttributeField.Type = RegisterAttributeType.DATE;
                        break;
                    default:
                        throw new Exception($"Неизвестный тип {attribute.Type}");
                }

				ViewBag.RegisterId = registerId;
				ViewBag.ObjectId = objectId;

				list.Add(registerAttributeField);
            }
            return View(list);
        }
		
		[HttpPost]
        [Consumes("application/json")]
        public ActionResult SaveObject([FromBody]RegisterObjectResult registerObjectResult)
        {
			RegisterObject registerObject = new RegisterObject(registerObjectResult.RegisterId, registerObjectResult.ObjectId);

			foreach (var attributeValue in registerObjectResult.List)
			{
				registerObject.SetAttributeValue((int)attributeValue.AttributeId, attributeValue.Value);
			}

			int savedObjectId = RegisterStorage.Save(registerObject);

			return Json(new
			{
				ObjectId = savedObjectId
			});
		}

        public ActionResult GetItems(int registerId)
        {
            List<SelectListItem> list = new List<SelectListItem>()
            {
                new SelectListItem
                {
                    Text = "-",
                    Value = "0"
                }
            };
            list.AddRange(RegisterHelpServices.GetUserKeyStrings(registerId).Select(x => new SelectListItem { Text = x.Value, Value = x.Key.ToString() }));

            return Content(JsonConvert.SerializeObject(list), "application/json");
        }
    }
}