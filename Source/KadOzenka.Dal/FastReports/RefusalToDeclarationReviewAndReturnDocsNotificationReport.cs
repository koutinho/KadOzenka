using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using Core.Shared.Extensions;
using Core.SRD;
using NPetrovich;
using ObjectModel.Core.SRD;
using ObjectModel.Declarations;
using ObjectModel.Directory.Declarations;
using Platform.Reports;

namespace KadOzenka.Dal.FastReports
{
	public class RefusalToDeclarationReviewAndReturnDocsNotificationReport : DeclarationNotificationReport
	{
		protected override string TemplateName(NameValueCollection query)
		{
			return "RefusalToDeclarationReviewAndReturnDocsNotificationReport";
		}

		public override string GetTitle(long? objectId)
		{
			return ReportType.Title;
		}

		protected override DataSet GetData(NameValueCollection query, HashSet<long> objectList = null)
		{
			DataSet dataSet = new DataSet();
			dataSet.Tables.Add("Data");
			dataSet.Tables[0].Columns.Add("OwnerName");
			dataSet.Tables[0].Columns.Add("OwnerAddress");
			dataSet.Tables[0].Columns.Add("DeclarationDateIn");
			dataSet.Tables[0].Columns.Add("DeclarationNumber");
			dataSet.Tables[0].Columns.Add("UserIspName");
			dataSet.Tables[0].Columns.Add("MainData");

			var notification = OMUved
				.Where(x => x.Id == ObjectId)
				.SelectAll()
				.Execute().FirstOrDefault();
			if (notification == null)
			{
				throw new Exception($"Уведомление с ИД {ObjectId} не найдено");
			}

			var declaration = OMDeclaration
				.Where(x => x.Id == notification.Declaration_Id)
				.SelectAll()
				.Execute().FirstOrDefault();
			if (declaration == null)
			{
				throw new Exception($"Декларация не найдена");
			}

			var owner = OMSubject
				.Where(x => x.Id == declaration.Owner_Id)
				.SelectAll()
				.Execute().FirstOrDefault();
			if (owner == null)
			{
				throw new Exception($"Заявитель не найден");
			}

			var ownerName = string.Empty;
			if (owner.Type_Code == SubjectType.Fl)
			{
				var petrovich = new Petrovich
				{
					FirstName = owner.I_Name,
					LastName = owner.F_Name,
					MiddleName = owner.O_Name,
					AutoDetectGender = true
				};
				var inflected = petrovich.InflectTo(Case.Dative);
				ownerName = inflected.LastName;
				if (!string.IsNullOrWhiteSpace(inflected.FirstName) &&
					!string.IsNullOrWhiteSpace(inflected.MiddleName))
				{
					ownerName += $" {inflected.FirstName.Trim()[0]}.{inflected.MiddleName.Trim()[0]}.";
				}
			}
			else
			{
				ownerName = owner.Name;
			}

			var book = OMBook
				.Where(x => x.Id == declaration.Book_Id)
				.SelectAll()
				.Execute().FirstOrDefault();

			var userIsp = OMUser
				.Where(x => x.Id == SRDSession.GetCurrentUserId())
				.SelectAll()
				.ExecuteFirstOrDefault();

			var userIspName = userIsp.Name;
			if (!string.IsNullOrWhiteSpace(userIsp.Surname) && !string.IsNullOrWhiteSpace(userIsp.Patronymic))
			{
				userIspName += $" {userIsp.Surname.Trim()[0]}.{userIsp.Patronymic.Trim()[0]}.";
			}

			var reason = PrepareText(notification.RejectionReason);

			var mainData =
					  "	В&nbsp;соответствии с&nbsp;пунктом 8 приказа Минэкономразвития от&nbsp;04.06.2019 № 318 «Об&nbsp;утверждении Порядка рассмотрения декларации о&nbsp;характеристиках объекта недвижимости, " +
					  "в&nbsp;том числе ее&nbsp;формы» (далее – Приказ) ГБУ «Центр имущественных платежей и&nbsp;жилищного страхования» провело проверку декларации" +
				"о&nbsp;характеристиках объекта недвижимости на&nbsp;" + GetObjectTypeString(declaration.TypeObj_Code) + " с&nbsp;кадастровым номером " + declaration.CadastralNumObj +
					  " и&nbsp;сообщает." + System.Environment.NewLine + "	Декларация проверку не&nbsp;прошла и&nbsp;не&nbsp;подлежит рассмотрению, т.к. " + reason + @"."
				+ System.Environment.NewLine + System.Environment.NewLine + "Приложение: перечень документов";

			dataSet.Tables[0].Rows.Add(
				ownerName,
				FormAddress(owner),
				declaration.DateIn?.ToString("dd.MM.yyyy"),
				$"{declaration.NumIn}/{book?.Prefics}",
				userIspName,
				mainData);

			return dataSet;
		}
	}
}
