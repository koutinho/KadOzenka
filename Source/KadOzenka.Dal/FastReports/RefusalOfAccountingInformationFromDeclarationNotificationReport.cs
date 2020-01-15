﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using Platform.Reports;
using System.Data;
using System.Linq;
using Core.Shared.Extensions;
using Core.SRD;
using NPetrovich;
using ObjectModel.Core.SRD;
using ObjectModel.Declarations;
using ObjectModel.Directory.Declarations;

namespace KadOzenka.Dal.FastReports
{
	public class RefusalOfAccountingInformationFromDeclarationNotificationReport : DeclarationNotificationReport
	{
		protected override string TemplateName(NameValueCollection query)
		{
			return "RefusalOfAccountingInformationFromDeclarationNotificationReport";
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
				"\u0009В\u00A0соответствии с\u00A0Приказом Минэкономразвития от\u00A004.06.2019 № 318 «Об\u00A0утверждении Порядка рассмотрения декларации о\u00A0характеристиках объекта недвижимости, " +
				"в\u00A0том числе ее\u00A0формы» (далее – Приказ) ГБУ «Центр имущественных платежей и\u00A0жилищного страхования» провело проверку декларации" +
				"о\u00A0характеристиках объекта недвижимости на\u00A0" + GetObjectTypeString(declaration.TypeObj_Code) +
				" с\u00A0кадастровым номером " + declaration.CadastralNumObj +
				" и\u00A0сообщает." + System.Environment.NewLine + "\u0009" + reason;
			mainData = mainData.Replace(" ", "\u0020");

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
