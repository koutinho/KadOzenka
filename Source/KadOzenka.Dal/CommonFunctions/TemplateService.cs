using System;
using System.Collections.Generic;
using Core.SRD;
using ObjectModel.Common;
using ObjectModel.Directory.Common;

namespace KadOzenka.Dal.CommonFunctions
{
    public class TemplateService
    {
        public List<OMDataFormStorage> GetTemplates(DataFormStorege formType)
        {
	        return OMDataFormStorage
		        .Where(x => (x.UserId == SRDSession.GetCurrentUserId().GetValueOrDefault() || x.IsCommon == true) &&
		                    x.FormType_Code == formType)
		        .SelectAll()
		        .Execute();
        }

        public void CreateTemplate(string nameTemplate, bool isCommon, DataFormStorege formType, string serializeData)
        {
	        if (string.IsNullOrEmpty(nameTemplate))
	        {
				throw new Exception("Имя шаблона обязательное поле");
	        }

	        var userId = isCommon ? (int?)null : SRDSession.GetCurrentUserId().GetValueOrDefault();
	        new OMDataFormStorage
	        {
		        UserId = userId,
		        FormType_Code = formType,
		        Data = serializeData,
		        TemplateName = nameTemplate,
		        IsCommon = isCommon
	        }.Save();
		}

        public void UpdateTemplate(long id, string nameTemplate, bool isCommon, DataFormStorege formType, string serializeData)
        {
	        if (string.IsNullOrEmpty(nameTemplate))
	        {
		        throw new Exception("Имя шаблона обязательное поле");
	        }

	        var template = OMDataFormStorage.Where(x => x.Id == id).SelectAll().ExecuteFirstOrDefault();
	        if (template == null)
	        {
		        throw new Exception($"Не шаблон с ИД={id}");
			}

			template.UserId = isCommon ? (int?)null : SRDSession.GetCurrentUserId().GetValueOrDefault();
			template.FormType_Code = formType;
			template.Data = serializeData;
			template.TemplateName = nameTemplate;
			template.IsCommon = isCommon;
			template.Save();
        }

		public void RemoveTemplate(long templateId)
        {
	        var storage = OMDataFormStorage.Where(x =>
			        x.Id == templateId)
		        .SelectAll().ExecuteFirstOrDefault();
	        if (storage == null)
	        {
		        throw new Exception($"Не найден шаблон с ИД {templateId}");
	        }

	        storage.Destroy();
		}
    }
}
