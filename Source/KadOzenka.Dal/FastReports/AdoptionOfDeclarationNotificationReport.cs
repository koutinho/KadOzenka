using System.Collections.Specialized;
using Core.Shared.Extensions;
using ObjectModel.Declarations;
using ObjectModel.Directory.Declarations;

namespace KadOzenka.Dal.FastReports
{
	public class AdoptionOfDeclarationNotificationReport : DeclarationNotificationReport
	{
		protected override string TemplateName(NameValueCollection query)
		{
			return nameof(AdoptionOfDeclarationNotificationReport);
		}

		public override string GetTitle(long? objectId)
		{
			return ReportType.Title;
		}

		public override string GetMainData(OMDeclaration declaration, OMUved notification, SendUvedType uvedType)
		{
			var mainData =
				"\u0009Настоящим уведомлением сообщаем, что ГБУ «Центр имущественных платежей и\u00A0жилищного страхования» " +
				"на\u00A0основании приказа Минэкономразвития от\u00A004.06.2019 №\u00A0318 «Об\u00A0утверждении Порядка рассмотрения декларации о\u00A0характеристиках объекта " +
				"недвижимости, в\u00A0том числе ее\u00A0формы» была принята декларация о\u00A0характеристиках объекта недвижимости (далее - декларация) " +
				"на\u00A0" + GetObjectTypeString(declaration.TypeObj_Code) + " с\u00A0кадастровым номером " + declaration.CadastralNumObj + ".";
			if (uvedType != SendUvedType.No && uvedType != SendUvedType.None)
			{
				mainData += System.Environment.NewLine +
				            "\u0009Информация о\u00A0результатах рассмотрения декларации будет направлена Вам " +
				            PrepareText(uvedType.GetEnumDescription().ToLower()) + ".";
			}
			
			mainData = mainData.Replace(" ", "\u0020");

			return mainData;
		}

		public override string GetAnnex(OMUved notification)
		{
			string annex = null;
			if (!string.IsNullOrWhiteSpace(notification.Annex))
			{
				var annexDocs = notification.Annex.Split("\n");
				annex += PrepareText(annexDocs[0]);
				for (var i = 1; i < annexDocs.Length; i++)
				{
					annex += System.Environment.NewLine + PrepareText(annexDocs[i]);
				}
				annex = annex.Replace(" ", "\u0020");
			}

			return annex;
		}
	}
}
