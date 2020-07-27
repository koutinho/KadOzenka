using System.Collections.Specialized;
using ObjectModel.Declarations;
using ObjectModel.Directory.Declarations;

namespace KadOzenka.Dal.FastReports
{
	public class AccountingInfoFromDeclarationNotificationReport : DeclarationNotificationReport
	{
		protected override string TemplateName(NameValueCollection query)
		{
			return nameof(AccountingInfoFromDeclarationNotificationReport);
		}

		public override string GetTitle(long? objectId)
		{
			return ReportType.Title;
		}

		public override string GetMainData(OMDeclaration declaration, OMUved notification, SendUvedType uvedType)
		{
			var mainData =
				"\u0009В\u00A0соответствии с\u00A0пунктом 13 приказа Минэкономразвития от\u00A004.06.2019 №\u00A0318 «Об\u00A0утверждении Порядка рассмотрения декларации о\u00A0характеристиках объекта недвижимости, " +
				"в\u00A0том числе ее\u00A0формы» ГБУ «Центр имущественных платежей и\u00A0жилищного страхования» направляет уведомление " +
				"об\u00A0учете информации, содержащейся в\u00A0декларации о\u00A0характеристиках объекта недвижимости на\u00A0" + GetObjectTypeString(declaration.TypeObj_Code) + " с\u00A0кадастровым номером "
				+ declaration.CadastralNumObj + ".";
			mainData = mainData.Replace(" ", "\u0020");

			return mainData;
		}
	}
}
