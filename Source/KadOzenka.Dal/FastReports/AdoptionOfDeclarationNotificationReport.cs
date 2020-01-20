﻿using System.Collections.Specialized;
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

		public override string GetMainData(OMDeclaration declaration, OMUved notification)
		{
			var mainData =
				"\u0009Настоящим уведомлением сообщаем, что ГБУ «Центр имущественных платежей и\u00A0жилищного страхования» " +
				"на\u00A0основании приказа Минэкономразвития от\u00A004.06.2019 №\u00A0318 «Об\u00A0утверждении Порядка рассмотрения декларации о\u00A0характеристиках объекта " +
				"недвижимости, в\u00A0том числе ее\u00A0формы» была принята декларация о\u00A0характеристиках объекта недвижимости (далее - декларация) " +
				"на\u00A0" + GetObjectTypeString(declaration.TypeObj_Code) + " с\u00A0кадастровым номером " + declaration.CadastralNumObj + ".";
			if (declaration.UvedTypeOwner_Code != SendUvedType.No && declaration.UvedTypeOwner_Code != SendUvedType.None)
			{
				mainData += System.Environment.NewLine +
				            "\u0009Информация о\u00A0результатах рассмотрения декларации будет направлена Вам " +
				            PrepareText(declaration.UvedTypeOwner_Code.GetEnumDescription().ToLower());
			}
			if (!string.IsNullOrWhiteSpace(notification.Annex))
			{
				mainData += System.Environment.NewLine + System.Environment.NewLine + "\u0009Приложение: " + PrepareText(notification.Annex);
			}
			mainData = mainData.Replace(" ", "\u0020");

			return mainData;
		}
	}
}
