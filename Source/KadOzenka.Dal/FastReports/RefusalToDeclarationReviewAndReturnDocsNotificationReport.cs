using System.Collections.Generic;
using System.Collections.Specialized;
using Core.Shared.Extensions;
using ObjectModel.Declarations;
using ObjectModel.Directory.Declarations;

namespace KadOzenka.Dal.FastReports
{
	public class RefusalToDeclarationReviewAndReturnDocsNotificationReport : DeclarationNotificationReport
	{
		protected override string TemplateName(NameValueCollection query)
		{
			return nameof(RefusalToDeclarationReviewAndReturnDocsNotificationReport);
		}

		public override string GetTitle(long? objectId)
		{
			return ReportType.Title;
		}

		public override string GetMainData(OMDeclaration declaration, OMUved notification)
		{
			var reason = GetRejectionReason(notification);
			var mainData =
				"\u0009В\u00A0соответствии с\u00A0пунктом 8 приказа Минэкономразвития от\u00A004.06.2019 №\u00A0318 «Об\u00A0утверждении Порядка рассмотрения декларации о\u00A0характеристиках объекта недвижимости, " +
				"в\u00A0том числе ее\u00A0формы» (далее – Приказ о\u00A0декларациях) ГБУ «Центр имущественных платежей и\u00A0жилищного страхования» провело проверку декларации " +
				"о\u00A0характеристиках объекта недвижимости на\u00A0" + GetObjectTypeString(declaration.TypeObj_Code) +
				" с\u00A0кадастровым номером " + declaration.CadastralNumObj +
				" и\u00A0сообщает." + System.Environment.NewLine +
				"\u0009Декларация проверку не\u00A0прошла и\u00A0не\u00A0подлежит рассмотрению, т.к. " + reason + @".";
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

		private string GetRejectionReason(OMUved notification)
		{
			var reasons = new List<string>();
			var existedRejectionReasonTypes =
				OMUvedRejectionReasonType.Where(x => x.UvedId == notification.Id).OrderBy(x => x.RejectionReasonType_Code).SelectAll().Execute();
			foreach (var existedRejectionReasonType in existedRejectionReasonTypes)
			{
				reasons.Add(existedRejectionReasonType.RejectionReasonType_Code != RejectionReasonType.Other
					? existedRejectionReasonType.RejectionReasonType_Code.GetEnumDescription().ToLower()[0] + existedRejectionReasonType.RejectionReasonType_Code.GetEnumDescription().Substring(1)
					: notification.RejectionReason);
			}

			return PrepareText(string.Join("; ", reasons));
		}
	}
}
